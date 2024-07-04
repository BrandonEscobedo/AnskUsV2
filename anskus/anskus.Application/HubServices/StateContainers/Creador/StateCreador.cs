using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers.Creador
{
    public class StateCreador : IStateCreador
    {
        public List<ParticipanteEnCuestDTO> participantes { get; set; } = new();
        public event Action? OnUsuarioContesto;
        public void OnParticipantesContestado(ParticipanteEnCuestDTO participante)
        {
            participantes.Add(participante);
            OnUsuarioContesto?.Invoke();
        }
    }
}
