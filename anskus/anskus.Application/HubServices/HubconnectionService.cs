using anskus.Application.DTOs;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.HubServices.StateContainers.Creador;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubServices
{
    public class HubconnectionService : IHubconnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubStateCreador _hubStateCreador;
        public HubconnectionService(IHubStateCreador hubStateCreador, HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
            _hubStateCreador = hubStateCreador;
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                _hubConnection.StartAsync();
            }
        }
        public async Task NavegarARanking()
        {
            try
            {
                if (_hubStateCreador.Cuestionario.Pregunta.Count == 0)
                {
                    await _hubConnection.InvokeAsync("NavegarAClasificacion", _hubStateCreador.Codigo);
                }
                else if (_hubStateCreador.Cuestionario.Pregunta.Count > 0)
                {
                    await _hubConnection.InvokeAsync("NavegarARanking", _hubStateCreador.Codigo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en NavegarARanking: {ex.Message}");
            }
        }
        public async Task<bool> CreateRoom(CuestionarioActivoResponse cuestionarioActivo)
        {
            bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", cuestionarioActivo.Codigo, cuestionarioActivo.IdcuestionarioActivo,
                cuestionarioActivo.IdUsuario);
            if (result)
            {
                _hubStateCreador.SetCuestionario(cuestionarioActivo.Cuestionario, cuestionarioActivo.Codigo);
                return true;
            }
            return false;
        }
        public async Task IniciarCuestionario()
        {
            await _hubConnection.InvokeAsync("IniciarCuestionario", _hubStateCreador.Codigo, _hubStateCreador.Cuestionario.Titulo, _hubStateCreador.MandarSiguientePregunta());
        }

        public async Task TiempoTermino()
        {
            try
            {
                await _hubConnection.InvokeAsync("TerminoTiempo", _hubStateCreador.Codigo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en TiempoTermino: {ex.Message}");
            }
        }
        public async Task SiguientePregunta()
        {
            await _hubConnection.InvokeAsync("SiguientePregunta", _hubStateCreador.Codigo, _hubStateCreador.MandarSiguientePregunta());
        }
    }
}
