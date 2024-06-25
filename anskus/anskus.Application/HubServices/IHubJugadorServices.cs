using anskus.Application.DTOs;

namespace anskus.Application.HubServices
{
    public interface IHubJugadorServices
    {
        Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante);
    }
}
