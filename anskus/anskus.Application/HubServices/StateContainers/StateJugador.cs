using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class StateJugador : IStateJugador
    {
        public ParticipanteEnCuestDTO participante { get; set; } = new ParticipanteEnCuestDTO();

        public List<ParticipanteEnCuestDTO> participantes { get; set; } = new List<ParticipanteEnCuestDTO>();
    }
}
