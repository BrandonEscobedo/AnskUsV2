using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IHubStateJugador
    {
        
    }
    public class HubStateJugador: IHubStateJugador
    {
        private readonly HubConnection _hubConnection;
        private readonly IStateJugador _stateJugador;
        public HubStateJugador(HubConnection hubConnection, IStateJugador stateJugador)
        {
            _hubConnection = hubConnection;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<Pregunta>("SiguientePregunta", OnSiguientePregunta);
            _stateJugador = stateJugador;
        }
        private void OnSiguientePregunta(Pregunta pregunta)
        {
            _stateJugador.SetPregunta(pregunta);
        }

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
         _stateJugador.SetTituloPregunta(Titulo, pregunta);
        }
    }
}
