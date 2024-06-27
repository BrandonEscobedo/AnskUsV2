using anskus.Application.DTOs;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateJugador: IStateBase
    {
        public ParticipanteEnCuestDTO participante { get; set; }
       
    }
}
