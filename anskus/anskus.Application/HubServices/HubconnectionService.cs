using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.HubServices.StateContainers;
using anskus.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubServices
{
    public class HubconnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubStateCreador _hubStateCreador;
        public HubconnectionService(HubConnection hubConnection, IHubStateCreador hubStateCreador)
        {
            _hubConnection = hubConnection;
            _hubStateCreador = hubStateCreador;
            _hubConnection.On<string, Pregunta>("IniciarCuestionario",OnIniciarCuestionario);
        }

        private void OnIniciarCuestionario(string Titulo, Pregunta pregunta)
        {
            throw new NotImplementedException();
        }

        public async Task CreateRoom(CuestionarioActivoResponse cuestionarioActivo)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", cuestionarioActivo.Codigo, cuestionarioActivo);      
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

        }
    }
}
