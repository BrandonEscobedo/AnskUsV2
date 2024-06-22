using anskus.Domain.Models;
using System.Security.Cryptography;

namespace anskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby
    {
        public int Codigo { get; set; }
        private Cuestionario Cuestionario { get; set; }=new Cuestionario();
        protected override void OnInitialized()
        {
            base.OnInitialized();
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
