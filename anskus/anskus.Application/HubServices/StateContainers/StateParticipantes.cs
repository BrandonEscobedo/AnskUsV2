using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class StateParticipantes : IStateParticipantes
    {
        public List<ParticipanteEnCuestionario> participantesEnCuestionario { get; set; } = new();

        public event Action? OnParticipante;

        public void AddParticipanteToList(ParticipanteEnCuestionario participante)
        {
            participantesEnCuestionario.Add(participante);
            OnParticipante?.Invoke();
        }
        public void RemoveParticipanteToList(ParticipanteEnCuestionario participante)
        {
            participantesEnCuestionario.Remove(participante);
            OnParticipante?.Invoke();
        }
    }

}
