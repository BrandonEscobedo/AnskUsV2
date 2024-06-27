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
    public class HubCreadorServices:IHubCreadorServices
    {
        private readonly HubConnection _hubConnection;
        private readonly IStateParticipantes _stateParticipantes;
        private readonly IStateContainerOnPreg _stateContainer;
        public HubCreadorServices(HubConnection hubConnection, IStateParticipantes stateParticipantes, IStateContainerOnPreg stateCreador)
        {
            _hubConnection = hubConnection;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", OnNewParticipante);
            _hubConnection.On<ParticipanteEnCuestDTO>("LeftParticipante", OnRemoveParticipante);
            _hubConnection.On<Pregunta>("SiguientePregunta", OnSiguientePregunta);
            _stateParticipantes = stateParticipantes;
            _stateContainer = stateCreador;
        }
        private void OnRemoveParticipante(ParticipanteEnCuestDTO participante) => _stateParticipantes.RemoveParticipanteToList(participante);

        private void OnNewParticipante(ParticipanteEnCuestDTO participante) => _stateParticipantes.AddParticipanteToList(participante);

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
            _stateContainer.SetTituloPregunta(Titulo, pregunta);
        }
        private void OnSiguientePregunta(Pregunta pregunta)
        {
            _stateContainer.SetPregunta(pregunta);
        }
    }
}
