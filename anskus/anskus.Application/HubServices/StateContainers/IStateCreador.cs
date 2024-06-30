using anskus.Application.DTOs;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateCreador
    {
        public List<ParticipanteEnCuestDTO> participantes { get; set; } 
        public event Action? OnUsuarioContesto;
        public void OnParticipantesContestado(ParticipanteEnCuestDTO participante);
    }
}
