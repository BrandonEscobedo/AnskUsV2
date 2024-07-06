using anskus.Application.DTOs;

namespace anskus.Application.HubServices.StateContainers.Creador
{
    public interface IStateCreador
    {
        public List<ParticipanteEnCuestDTO> participantes { get; set; }
        public Task<List<ParticipanteEnCuestDTO>> GetParticipantes();
            public event Action? OnUsuarioContesto;
        public Task OnParticipantesContestado(ParticipanteEnCuestDTO participante);
    }
}
