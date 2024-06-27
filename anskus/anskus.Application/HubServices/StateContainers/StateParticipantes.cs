using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class StateParticipantes : IStateParticipantes
    {
        public List<ParticipanteEnCuestDTO> participantesEnCuestionario { get; set; } = new();

        public event Action? OnParticipante;

        public void AddParticipanteToList(ParticipanteEnCuestDTO participante)
        {
            participantesEnCuestionario.Add(participante);
            OnParticipante?.Invoke();
        }
        public void RemoveParticipanteToList(ParticipanteEnCuestDTO participante)
        {
            var user= participantesEnCuestionario.FirstOrDefault(x=>x.IdPeC== participante.IdPeC);  
            if (user!=null)
            {
                participantesEnCuestionario.Remove(user);
                OnParticipante?.Invoke();
            }
         
        }
    }

}
