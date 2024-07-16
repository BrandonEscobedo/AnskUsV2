using anskus.Application.DTOs;
using anskus.Application.HubServices.StateContainers;
using anskus.Application.HubServices.StateContainers.Creador;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
namespace anskus.Application.HubServices
{
    public class HubCreadorServices : IHubCreadorServices
    {
        private readonly IStateParticipantes _stateParticipantes;
        private readonly IStateContainerOnPreg _stateContainer;
        private readonly IStateCreador _stateCreador;
        private readonly HubConnection _hubConnection;
        public HubCreadorServices(IStateParticipantes stateParticipantes, IStateContainerOnPreg stateContainer, IStateCreador stateCreador, HubConnection hubConnection)
        {
          
            _stateParticipantes = stateParticipantes;
            _stateContainer = stateContainer;
            _stateCreador = stateCreador;
            _hubConnection = hubConnection;
            _hubConnection.On<string, Pregunta,DatosCuestionario>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<ParticipanteEnCuestDTO>("NewParticipante", OnNewParticipante);
            _hubConnection.On<ParticipanteEnCuestDTO>("LeftParticipante", OnRemoveParticipante);
            _hubConnection.On<Pregunta>("SiguientePregunta", OnSiguientePregunta);
            _hubConnection.On<ParticipanteEnCuestDTO>("PreguntaContestada", OnPreguntaContestada);
            _hubConnection.On("NavegarARanking", OnNavegarARanking);
            _hubConnection.On("NavegarAClasificacion", OnNavegarClasificacion);
            _hubConnection.On("TerminoTiempo", OnTerminoTiempo);
        }
        private async Task OnPreguntaContestada(ParticipanteEnCuestDTO participante)
        {
            await _stateCreador.OnParticipantesContestado(participante);
        }
        private void OnRemoveParticipante(ParticipanteEnCuestDTO participante) => _stateParticipantes.RemoveParticipanteToList(participante);

        private void OnNewParticipante(ParticipanteEnCuestDTO participante) => _stateParticipantes.AddParticipanteToList(participante);

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta,DatosCuestionario datosCuestionario)
        {
            _stateContainer.SetTituloPregunta(Titulo, pregunta, datosCuestionario);
        }

        private void OnSiguientePregunta(Pregunta pregunta)
        {
            _stateCreador.participantes = new();
            _stateContainer.SetPregunta(pregunta);
        }

        private void OnTerminoTiempo()
        {
            _stateContainer.TiempoTermino();
        }

        private void OnNavegarClasificacion()
        {
            _stateContainer.NavegarAClasificacion();
        }

        private void OnNavegarARanking()
        {
            _stateContainer.NavegarARanking();
        }
    }
}
