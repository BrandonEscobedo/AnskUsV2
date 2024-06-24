using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using anskus.Application.HubServices;
using System.Net.Http.Json;

namespace anskus.Application.Services
{
    public class CuestionarioActivoServices : ICuestionarioActivoServices
    {
        private readonly HttpClientServices _httpClientServices;
        private readonly IHubconnectionService _hubconnectionService;
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());
        public CuestionarioActivoServices(HttpClientServices httpClientServices, IHubconnectionService hubconnectionService)
        {
            _httpClientServices = httpClientServices;
            _hubconnectionService = hubconnectionService;
        }

        public async Task<Guid> ActivarCuestionario(Guid? IdCuestionario)
        {
            var response = await (await PrivateClient()).PostAsync($"{Constant.CuestionarioActivoRoute}?idcuestionario={IdCuestionario}", null);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CuestionarioActivoResponse>();
                if (result != null)
                {
                    await _hubconnectionService.CreateRoom(result);
                    return result.IdcuestionarioActivo;
                }
            }
            return Guid.Empty;
        }
        public async Task<bool> VerificarCodigo(int Code)
        {
            var response = await (await PrivateClient()).GetAsync($"{Constant.CuestionarioActivoRoute}?Code={Code}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}
