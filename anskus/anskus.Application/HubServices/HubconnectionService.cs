using anskus.Application.DTOs;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.HubServices.StateContainers;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubServices
{
    public class HubconnectionService : IHubconnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubStateCreador _hubStateCreador;

        public HubconnectionService(HubConnection hubConnection, IHubStateCreador hubStateCreador)
        {
            _hubConnection = hubConnection;
            _hubStateCreador = hubStateCreador;
        }



        public async Task CreateRoom(CuestionarioActivoResponse cuestionarioActivo)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", cuestionarioActivo.Codigo, cuestionarioActivo.IdcuestionarioActivo);
            if (result)
                _hubStateCreador.SetCuestionario(cuestionarioActivo.Cuestionario, cuestionarioActivo.Codigo);
        }
        public async Task IniciarCuestionario()
        {
            await _hubConnection.InvokeAsync("IniciarCuestionario", _hubStateCreador.Codigo, _hubStateCreador.MandarSiguientePregunta(),
                _hubStateCreador.Cuestionario.Titulo);
        }
        public async Task SiguientePregunta()
        {
            await _hubConnection.InvokeAsync("SiguientePregunta", _hubStateCreador.Codigo, _hubStateCreador.MandarSiguientePregunta());
        }
    }
}
