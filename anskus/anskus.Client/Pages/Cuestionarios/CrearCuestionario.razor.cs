using anskus.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace anskus.Client.Pages.Cuestionarios
{
    public partial class CrearCuestionario
    {

        [Parameter]
        public Cuestionario Cuestionario { get; set; } = new Cuestionario();

        private Pregunta Pregunta = new();

        protected override void OnInitialized()
        {
            if (Cuestionario.Pregunta.Count == 0)
            {
                Cuestionario.Pregunta.Add(Pregunta);
             
                Pregunta = Cuestionario.Pregunta.First();  
            }
            else
            {
                Pregunta = Cuestionario.Pregunta.First();
            }
            IniciarRespuestasObligatorias();
        }
        private void IniciarRespuestasObligatorias()
        {
            while (Pregunta.Respuesta.Count < 4)
                Pregunta.Respuesta.Add(new Respuesta());            
        }
        private async void AgregarPregunta()
        {
            await GuardarCuestionario(Pregunta);
            Pregunta = new Pregunta();
            Cuestionario.Pregunta.Add(Pregunta);
            IniciarRespuestasObligatorias();
            await GuardarCuestionario(Pregunta);
        }
        private async void ActualizarPregunta(Pregunta pregunta)
        {
            await GuardarCuestionario();
            Pregunta = pregunta;
            StateHasChanged();
        }
        private async void MandarCambios( )
        {
            await GuardarCuestionario(Pregunta);
        }

        private async void AgregarRespuesta()
        {
            if (Pregunta.Respuesta.Count < 6)
            {
                Pregunta.Respuesta.Add(new Respuesta());
                Pregunta.Respuesta.Add(new Respuesta());
                await GuardarCuestionario(Pregunta);
            }
        }

        private async void EliminarRespuesta( )
        {
            if (Pregunta.Respuesta.Count > 4)
            {
                Pregunta.Respuesta.RemoveRange(Pregunta.Respuesta.Count - 2, 2);

                await GuardarCuestionario(Pregunta);
            }
         
        }

        private async void EliminarPregunta(Pregunta pregunta)
        {
            Cuestionario.Pregunta.Remove(pregunta);
            await GuardarCuestionario();
        }

        private async Task GuardarCuestionario(Pregunta? pregunta=null)
        {
            if (!Cuestionario.Pregunta.Where(x=>x.IdPregunta==Pregunta.IdPregunta).Any())
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
                Cuestionario = cuestionarioActualizado;
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
