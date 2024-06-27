using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Models;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;

namespace anskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby
    {
        [Parameter]
        public Guid IdCuestionarioActivo { get; set; } = Guid.Empty;
        public int Codigo { get; set; }
        private CuestionarioResponse Cuestionario { get; set; }=new CuestionarioResponse();
        protected override void OnInitialized()
        {

            Cuestionario = _hubStateCreador.Cuestionario;
            Codigo = _hubStateCreador.Codigo;
            _stateContainer.OnIniciarCuestionario += OnIniciar;
        }
        private void OnIniciar()
        {
            _navigationManager.NavigateTo("/Titulo");
        }
        private  void Salir()
        {

        }
        private async void Iniciar()
        {
           await _hubConnectionService.IniciarCuestionario();
        }
    }
}
