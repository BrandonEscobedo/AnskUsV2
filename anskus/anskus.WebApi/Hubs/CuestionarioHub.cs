using anskus.Application.DTOs;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace anskus.WebApi.Hubs
{
    public class CuestionarioHub : Hub<InotificationClient>
    {


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
         
        }
        public async Task<bool> CreateRoom(int code, Cuestionario Cuestionario)
        {
            Context.Items["Codigo"] = code;
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            return true;
        }
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            Context.Items["UserData"] = participante;
            await Groups.AddToGroupAsync(Context.ConnectionId, participante.Codigo.ToString());
            await Clients.Clients(participante.Nombre).NewParticipante(participante);
            await Clients.Group(participante.Codigo.ToString()).NewParticipante(participante);        
        }
        public async Task IniciarCuestionario(int Codigo ,string Titulo, Pregunta pregunta)
        {         
            await Clients.Group(Codigo.ToString()).IniciarCuestionario(Titulo,pregunta);       
        }
        public async Task SiguientePregunta(int Codigo, Pregunta pregunta)
        {
            await Clients.Group(Codigo.ToString()!).SiguientePregunta(pregunta);
        }
    }
    public interface InotificationClient
    {

        Task ListaRanking(List<ParticipanteEnCuestDTO> participantes);
        Task PreguntaContestada(ParticipanteEnCuestDTO participante);
        Task IniciarCuestionario(string Titulo,  Pregunta pregunta);
        Task SiguientePregunta(Pregunta pregunta);
        Task MensajePrueba(string mensaje);
        Task RemoveUser(ParticipanteEnCuestDTO participante);
        Task NewParticipante(ParticipanteEnCuestDTO participante);
        Task UsuariosEnLaSala(List<string> usuarios);
        Task getUsers(int code);
    }
}
