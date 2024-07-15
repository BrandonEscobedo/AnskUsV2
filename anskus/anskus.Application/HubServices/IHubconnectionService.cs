using anskus.Application.DTOs.Response.Cuestionarios;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices
{
    public interface IHubconnectionService
    {
        public Task<bool> CreateRoom(CuestionarioActivoResponse cuestionarioActivo);
        public Task IniciarCuestionario();
        public Task SiguientePregunta();
        public Task NavegarARanking();
        public Task TiempoTermino();
    }
}
