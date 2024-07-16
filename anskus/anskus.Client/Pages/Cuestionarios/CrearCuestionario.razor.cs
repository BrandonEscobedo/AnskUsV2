using anskus.Application.Extensions;
using anskus.Domain.Models;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
namespace anskus.Client.Pages.Cuestionarios
{
    public partial class CrearCuestionario
    {
     
        public Cuestionario Cuestionario { get; set; } = new Cuestionario();
        private Pregunta Pregunta = new();
        [Parameter]
        public Guid? Id { get; set; }
        public string FileType { get; set; } = "";
        private const long MaxFileSize = 20 * 1024 * 1024;
        private async Task AgregarImagenPregunta(InputFileChangeEventArgs e)
        {
            var allowedFileTypes = new[] { "image/jpeg", "image/png", "image/gif", "video/mp4", "video/x-msvideo", "video/quicktime" };
            var browserFile = e.File;
            if (browserFile != null)
            {
                if (browserFile.Size > MaxFileSize)
                {
                    await _sweetAlert.FireAsync("Oops...", "El tamaño del archio es demaciado grande, ingresa otro documento", SweetAlertIcon.Warning);
                    return;
                }
                if (allowedFileTypes.Contains(browserFile.ContentType))
                {
                    FileType = browserFile.ContentType;

                    using var stream = browserFile.OpenReadStream(MaxFileSize);
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    await UploadFileToServer(fileBytes, Guid.NewGuid(), browserFile.ContentType);
                }
                else
                {
                    await _sweetAlert.FireAsync("Oops...", "Este formato no esta permitido", SweetAlertIcon.Warning);
                }
            }
        }
        private async Task UploadFileToServer(byte[] fileBytes, Guid fileName, string contentType)
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            content.Add(fileContent, "file", fileName.ToString());
            var result = await _contentMediaService.UploadContent(content);
            if (result != null)
            {
                Pregunta.Imagen = $"{Constant.URLServerImagen}/{result.IdImagen}";
                Pregunta.DatosMedia = result;
                await GuardarCuestionario(Pregunta);
            }
        }
        public async ValueTask DisposeAsync()
        {
            if (Cuestionario.IdCuestionario != null)
            {
                await GuardarCuestionario(Pregunta);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var cuest = await CuestionarioService.GetCuestionarioByIdAsync(Id);
                if (cuest != null)
                {
                    Cuestionario = cuest;
                }
            }
            if (Cuestionario.Pregunta.Count == 0)
            {
                Cuestionario.Pregunta.Add(Pregunta);

            }
                Pregunta = Cuestionario.Pregunta.First();
            
            StateHasChanged();
            IniciarRespuestasObligatorias();

        }
        private void IniciarRespuestasObligatorias()
        {
            while (Pregunta.Respuesta.Count < 4)
                Pregunta.Respuesta.Add(new Respuesta());
        }
        private async Task AgregarPregunta()
        {
            await GuardarCuestionario(Pregunta);
            Pregunta = new Pregunta();
            Cuestionario.Pregunta.Add(Pregunta);
            IniciarRespuestasObligatorias();
            await GuardarCuestionario();
        }
        private async Task EliminarImagenPregunta(Pregunta pregunta)
        {
            Guid IdString = pregunta.DatosMedia.IdImagen;
            if (IdString != Guid.Empty)
            {
                bool result = await _contentMediaService.EliminarImagenAsync(IdString);
                if (result)
                {
                    Pregunta.Imagen = "";
                    Pregunta.DatosMedia = new();
                    await GuardarCuestionario(Pregunta);
                }
            }
        }
        private async Task ActualizarPregunta(Pregunta pregunta)
        {
            await GuardarCuestionario(Pregunta);
            Pregunta = pregunta;
            StateHasChanged();
        }
        private async Task MandarCambios()
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
                CancellationTokenSource.Dispose();
            }
            CancellationTokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.Token;
            try
            {
                await Task.Delay(300, token);
                await GuardarCuestionario(Pregunta);

            }
            catch (TaskCanceledException) { }
        }
        private CancellationTokenSource CancellationTokenSource;
        private async void AgregarRespuesta()
        {
            if (Pregunta.Respuesta.Count < 6)
            {
                Pregunta.Respuesta.Add(new Respuesta());
                Pregunta.Respuesta.Add(new Respuesta());
                await GuardarCuestionario(Pregunta);
            }
        }
        private async void EliminarRespuesta()
        {
            if (Pregunta.Respuesta.Count > 4)
            {
                Pregunta.Respuesta.RemoveRange(Pregunta.Respuesta.Count - 2, 2);

                await GuardarCuestionario(Pregunta);
            }

        }

        private async void EliminarPregunta(Pregunta pregunta)
        {

            var preg = Cuestionario.Pregunta.FirstOrDefault(x => x.IdPregunta == pregunta.IdPregunta);
            if (preg != null)
            {
                await EliminarImagenPregunta(preg);
                Cuestionario.Pregunta.Remove(preg);
                await GuardarCuestionario(preg);
            }

        }
        private bool ItemSelector(Pregunta item, string dropzone)
        {
            return item.IdPregunta.ToString() == dropzone;

        }

        private async void ItemUpdated(MudItemDropInfo<Pregunta> dropItem)
        {
            var item = dropItem.Item;
            var oldIndex = Cuestionario.Pregunta.IndexOf(item);
            Cuestionario.Pregunta.RemoveAt(oldIndex);
            Cuestionario.Pregunta.Insert(dropItem.IndexInZone, item);
            await GuardarCuestionario(Pregunta);
        }
        private async Task GuardarCuestionario(Pregunta? pregunta = null)
        {
            if (!Cuestionario.Pregunta.Where(x => x.IdPregunta == Pregunta.IdPregunta).Any())
            {
                Cuestionario.Pregunta.Add(Pregunta);
            }
            if (Cuestionario.IdCuestionario == null)
            {
                var nuevoCuestionario = await CuestionarioService.CrearCuestionario(Cuestionario);
                Cuestionario = nuevoCuestionario;
            }
            else
            {
                if (pregunta != null)
                {
                    var preguntaCuest = Cuestionario.Pregunta.FindIndex(x => x.IdPregunta == pregunta.IdPregunta);
                    Cuestionario.Pregunta[preguntaCuest] = Pregunta;
                }
                if (Cuestionario.Pregunta.Count == 1)
                {
                    Cuestionario.Pregunta[0] = Pregunta;
                }

                var cuestionarioActualizado = await CuestionarioService.ActualizarCuestionario(Cuestionario);
                Cuestionario.Estado = cuestionarioActualizado.Estado;
            }
            StateHasChanged();
        }
        private async void GuardarCuestionarioModal()
        {
            await GuardarCuestionario();
            CloseModal();
        }
        private void CloseModal()
        {
            JS.InvokeVoidAsync("closeModal");
        }
    }

}
