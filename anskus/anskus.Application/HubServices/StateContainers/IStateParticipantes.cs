using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateParticipantes
    {

        List<ParticipanteEnCuestDTO> participantesEnCuestionario { get; set; }
        public event Action? OnParticipante;
        public void AddParticipanteToList(ParticipanteEnCuestDTO participante);
        public void RemoveParticipanteToList(ParticipanteEnCuestDTO participante);

    }
    
}
