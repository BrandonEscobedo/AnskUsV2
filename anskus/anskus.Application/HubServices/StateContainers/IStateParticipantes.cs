using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateParticipantes
    {

        List<ParticipanteEnCuestionario> participantesEnCuestionario { get; set; }
        public event Action? OnParticipante;
        public void AddParticipanteToList(ParticipanteEnCuestionario participante);
        public void RemoveParticipanteToList(ParticipanteEnCuestionario participante);

    }
    
}
