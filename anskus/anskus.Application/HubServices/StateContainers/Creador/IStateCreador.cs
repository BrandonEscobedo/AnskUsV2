using anskus.Application.DTOs;

namespace anskus.Application.HubServices.StateContainers.Creador
{
    public interface IStateCreador
    {
        public List<ParticipanteEnCuestDTO> participantes { get; set; }
        public event Action? OnUsuarioContesto;
        public void OnParticipantesContestado(ParticipanteEnCuestDTO participante);
    }
}
