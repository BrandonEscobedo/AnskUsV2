using anskus.Application.DTOs;
using anskus.Application.HubServices.StateContainers;
using anskus.Application.HubServices.StateContainers.Jugador;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubServices
{
    public class HubJugadorServices : IHubJugadorServices
    {
        private readonly HubConnection _hubConnection;
        private readonly IStateContainerOnPreg _stateContainer;
        private readonly IStateJugador _stateJugador;
        public HubJugadorServices(HubConnection hubConnection, IStateContainerOnPreg StateContainerOnPreg, IStateJugador stateJugador)
        {
            _hubConnection = hubConnection;
            _stateContainer = StateContainerOnPreg;
            _stateJugador = stateJugador;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<Pregunta>("SiguientePregunta", OnSiguientePregunta);
            _hubConnection.On<ParticipanteEnCuestDTO>("PreguntaContestada", OnPreguntaContestada);
            _hubConnection.On("NavegarARanking", OnNavegarARanking);
            _hubConnection.On("NavegarAClasificacion", OnNavegarClasificacion);
            _hubConnection.On("TerminoTiempo", OnTerminoTiempo);

        }
        private void OnTerminoTiempo()
        {
            _stateContainer.TiempoTermino();
        }

        private void OnNavegarARanking()
        {
            _stateContainer.NavegarARanking();
        }
        private void OnNavegarClasificacion()
        {
            _stateContainer.NavegarAClasificacion();
        }
        private void OnPreguntaContestada(ParticipanteEnCuestDTO participante)
        {
            _stateJugador.AddParticipanteRanking(participante);
        }

        private void OnSiguientePregunta(Pregunta pregunta)
        {
            _stateJugador.participantes=new();
            _stateJugador.DatosPregunta = new();
            _stateContainer.SetPregunta(pregunta);
           
        }
       
        private  void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
            _stateContainer.SetTituloPregunta(Titulo, pregunta);
        }
        public async Task MandarPreguntaContestada()
        {
            await _hubConnection.InvokeAsync("ContestarPregunta", _stateJugador.participante);
        }
        
        public async Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            await _hubConnection.InvokeAsync("AddUserToRoom", participante);
            _stateJugador.participante = participante;
            return true;
        }
    }
}
