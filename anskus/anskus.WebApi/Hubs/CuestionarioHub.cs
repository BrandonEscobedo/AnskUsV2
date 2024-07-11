using anskus.Application.DTOs;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using MediatR;
using anskus.Application.CuestionarioActivo.Command;
namespace anskus.WebApi.Hubs
{
    public class CuestionarioHub(IMediator sender) : Hub<InotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            if (Context.Items.TryGetValue("IdCuestionario", out var idCuestionarioObj) &&
       idCuestionarioObj is Guid idCuestionario &&
       idCuestionario != Guid.Empty)
            {
                await sender.Send(new RemoveCuestionarioActivoCommand(idCuestionario));
                Context.Items.Clear();
            }
            else if (Context.Items.TryGetValue("Codigo", out var codigoObj) &&
                  codigoObj is int codigo &&
                  codigo != 0)
            {
                if (Context.Items.TryGetValue("participante", out var idParticipanteObj) &&
                    idParticipanteObj is ParticipanteEnCuestDTO Participante &&
                    Participante != null)
                {
                    await sender.Send(new RemoveParticipanteCommand(Participante.IdPeC, codigo));
                    await Clients.Groups(codigo.ToString()).LeftParticipante(Participante);
                    Context.Items.Clear();
                }
            }
        }
        public async Task<bool> CreateRoom(int code, Guid Idcuestionario)
        {
            Context.Items["Codigo"] = code;
            Context.Items["IdCuestionario"] = Idcuestionario;
            await Groups.AddToGroupAsync(Context.ConnectionId, code.ToString());
            return true;
        }
        public async Task AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            Context.Items["Codigo"] = participante.Codigo;
            Context.Items["participante"] = participante;
            await Groups.AddToGroupAsync(Context.ConnectionId, participante.Codigo.ToString());
            await Clients.Clients(participante.Nombre).NewParticipante(participante);
            await Clients.Group(participante.Codigo.ToString()).NewParticipante(participante);
        }
        public async Task IniciarCuestionario(int Codigo, string Titulo, Pregunta pregunta)
        {
            await Clients.Group(Codigo.ToString()).IniciarCuestionario(Titulo, pregunta);
        }
        public async Task SiguientePregunta(int Codigo, Pregunta pregunta)
        {
            await Clients.Group(Codigo.ToString()!).SiguientePregunta(pregunta);
        }
        public async Task ContestarPregunta(ParticipanteEnCuestDTO participante)
        {
            await Clients.Groups(participante.Codigo.ToString(), Context.ConnectionId).PreguntaContestada(participante);
        }
        public async Task NavegarARanking(int codigo)
        {
            await Clients.Group(codigo.ToString()).NavegarARanking();
        }
        public async Task NavegarAClasificacion(int codigo)
        {
            await Clients.Group(codigo.ToString()).NavegarAClasificacion();
        }

        public async Task TerminoTiempo(int codigo)
        {
            await Clients.Group(codigo.ToString()).TerminoTiempo();
        }
    }
    public interface InotificationClient
    {

        Task ListaRanking(List<ParticipanteEnCuestDTO> participantes);
        Task PreguntaContestada(ParticipanteEnCuestDTO participante);
        Task IniciarCuestionario(string Titulo, Pregunta pregunta);
        Task SiguientePregunta(Pregunta pregunta);
        Task MensajePrueba(string mensaje);
        Task NavegarARanking();
        Task NavegarAClasificacion();
        Task TerminoTiempo();
        Task LeftParticipante(ParticipanteEnCuestDTO participante);
        Task NewParticipante(ParticipanteEnCuestDTO participante);
        Task UsuariosEnLaSala(List<string> usuarios);
        Task getUsers(int code);
    }
}
