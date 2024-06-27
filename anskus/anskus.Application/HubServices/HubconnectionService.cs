using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.HubServices.StateContainers;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubServices
{
    public class HubconnectionService:IHubconnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubStateCreador _hubStateCreador;
        private readonly IStateParticipantes _stateParticipantes;
        public HubconnectionService(HubConnection hubConnection, IHubStateCreador hubStateCreador, IStateParticipantes stateParticipantes)
        {
            _hubConnection = hubConnection;
            _hubStateCreador = hubStateCreador;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario", OnIniciarCuestionario);
            _hubConnection.On<ParticipanteEnCuestionario>("NewParticipante", OnNewParticipante);
            _hubConnection.On<ParticipanteEnCuestionario>("IniciarCuestionario", OnRemoveParticipante);
            _stateParticipantes = stateParticipantes;
        }

        private void OnRemoveParticipante(ParticipanteEnCuestionario participante) => _stateParticipantes.RemoveParticipanteToList(participante);

        private void OnNewParticipante(ParticipanteEnCuestionario participante) => _stateParticipantes.AddParticipanteToList(participante);

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
          //En Otra Interfaz
        }

        public async Task CreateRoom(CuestionarioActivoResponse cuestionarioActivo)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", cuestionarioActivo.Codigo,cuestionarioActivo.IdcuestionarioActivo);      
            if (result)
                _hubStateCreador.SetCuestionario(cuestionarioActivo.Cuestionario, cuestionarioActivo.Codigo);
        }
        public async Task IniciarCuestionario()
        {
            await _hubConnection.InvokeAsync("IniciarCuestionario",_hubStateCreador.Codigo,_hubStateCreador.MandarSiguientePregunta(),
                _hubStateCreador.Cuestionario.Titulo);
        }
        public async Task SiguientePregunta()
        {
            await _hubConnection.InvokeAsync("SiguientePregunta", _hubStateCreador.Codigo, _hubStateCreador.MandarSiguientePregunta());
        }   
    }
}
