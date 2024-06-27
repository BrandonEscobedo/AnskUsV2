using anskus.Application.DTOs;
using anskus.Application.HubServices.StateContainers;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices
{
    public class HubJugadorServices: IHubJugadorServices
    {
        private readonly HubConnection _hubConnection;
        private readonly IStateJugador _stateJugador;
        public HubJugadorServices(HubConnection hubConnection, IStateJugador stateJugador)
        {
            _hubConnection = hubConnection;
            _stateJugador = stateJugador;
            _hubConnection.On<string, Pregunta>("", OnIniciarCuestionario);
        }

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
            _stateJugador.SetTituloPregunta(Titulo, pregunta);

        }

        public async Task<bool> AddUserToRoom(ParticipanteEnCuestDTO participante)
        {
            await _hubConnection.InvokeAsync("AddUserToRoom", participante);
            _stateJugador.participante=participante;
            return true;
        }
    }
}
