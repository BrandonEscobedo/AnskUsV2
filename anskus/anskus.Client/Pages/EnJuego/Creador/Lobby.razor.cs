using anskus.Domain.Models;

namespace anskus.Client.Pages.EnJuego.Creador
{
    public partial class Lobby
    {
        public int Codigo { get; set; }
        private Cuestionario Cuestionario { get; set; }=new Cuestionario();

    }
}
