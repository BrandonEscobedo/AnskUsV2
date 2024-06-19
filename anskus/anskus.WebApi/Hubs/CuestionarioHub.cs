using anskus.Application.DTOs;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace anskus.WebApi.Hubs
{
    public class CuestionarioHub : Hub<InotificationClient>
    {

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public override async Task OnConnectedAsync()
        {
           await base.OnConnectedAsync();
        }
        public async Task<bool> CreateRoom(int code, Cuestionario Cuestionario)
        {

            Context.Items["Codigo"] = code;
            Context.Items["Cuestionario"] = Cuestionario;
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            return true;
        }
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            Context.Items["UserData"] = participante;
            await Clients.Clients(participante.Nombre).NewParticipante(participante);
            await Clients.Group(participante.Codigo.ToString()).NewParticipante(participante);
            await Groups.AddToGroupAsync(Context.ConnectionId, participante.Codigo.ToString());

        }
    }
    public interface InotificationClient
    {

        Task ListaRanking(List<ParticipanteEnCuestDTO> participantes);
        Task PreguntaContestada(ParticipanteEnCuestDTO participante);

        Task MensajePrueba(string mensaje);
        Task RemoveUser(ParticipanteEnCuestDTO participante);
        Task NewParticipante(ParticipanteEnCuestDTO participante);
        Task UsuariosEnLaSala(List<string> usuarios);
        Task getUsers(int code);
    }
}
