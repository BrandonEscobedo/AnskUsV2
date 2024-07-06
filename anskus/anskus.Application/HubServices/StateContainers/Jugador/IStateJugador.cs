using anskus.Application.DTOs;

namespace anskus.Application.HubServices.StateContainers.Jugador
{
    public interface IStateJugador
    {
        int Posicion { get; set; }
        public DatosPregunta DatosPregunta { get; set; }
        public List<ParticipanteEnCuestDTO> participantes { get; set; }
        public ParticipanteEnCuestDTO participante { get; set; }
        public void AddParticipanteRanking(ParticipanteEnCuestDTO participante);
        public void GenerarPuestoRanking();
    }
}
