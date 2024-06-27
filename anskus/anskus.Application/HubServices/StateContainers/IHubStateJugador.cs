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
        private readonly IStateContainerOnPreg _stateContainer;
        public HubStateJugador(HubConnection hubConnection, IStateContainerOnPreg stateContainer)
        {
            _hubConnection = hubConnection;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<Pregunta>("SiguientePregunta", OnSiguientePregunta);
            _stateContainer = stateContainer;
        }
        private void OnSiguientePregunta(Pregunta pregunta)
        {
            _stateContainer.SetPregunta(pregunta);
        }

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
            _stateContainer.SetTituloPregunta(Titulo, pregunta);
        }
    }
}
