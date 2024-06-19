using anskus.Application.DTOs.Response.Cuestionarios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace anskus.Application.HubServices
{
    public class HubconnectionService
    {
        private readonly HubConnection _hubConnection;
        public HubconnectionService(HubConnection hubConnection)
        {
           _hubConnection = hubConnection;
        }
        public async Task NewRom(CuestionarioActivoResponse cuestionarioActivo)
        {
            if (cuestionarioActivo != null)
            {   
                bool result = await _hubConnection.InvokeAsync<bool>("CreateRoom", cuestionarioActivo.Codigo, cuestionarioActivo);            
                //await _stateConteiner.SetCuestionario(cuestionarioActivo.Cuestionario, cuestionarioActivo.codigo);
                //if (result)
                //{
                //    navigationManager.NavigateTo($"/Lobby/{cuestionarioActivo.codigo}");
                //}
            }
        }
    }
}
