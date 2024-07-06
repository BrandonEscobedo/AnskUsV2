using anskus.Application.DTOs;
using anskus.Application.HubServices.StateContainers;
using anskus.Application.HubServices.StateContainers.Creador;
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
        private readonly IStateCreador _stateCreador;
        public HubCreadorServices(HubConnection hubConnection, IStateParticipantes stateParticipantes, IStateContainerOnPreg stateContainer, IStateCreador stateCreador)
        {
            _hubConnection = hubConnection;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", OnNewParticipante);
            _hubConnection.On<ParticipanteEnCuestDTO>("LeftParticipante", OnRemoveParticipante);
            _hubConnection.On<Pregunta>("SiguientePregunta", OnSiguientePregunta);
            _hubConnection.On<ParticipanteEnCuestDTO>("PreguntaContestada", OnPreguntaContestada);
            _hubConnection.On("NavegarARanking", OnNavegarARanking);
            _stateParticipantes = stateParticipantes;
            _stateContainer = stateContainer;
            _stateCreador = stateCreador;
        }

        private void OnNavegarARanking()
        {
            _stateContainer.NavegarARanking();
        }

        private async Task OnPreguntaContestada(ParticipanteEnCuestDTO participante)
        {
         await   _stateCreador.OnParticipantesContestado(participante);
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
            _stateCreador.participantes = new();
        }
    }
}
