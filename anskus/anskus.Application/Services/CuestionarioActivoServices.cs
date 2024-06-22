using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using System.Net.Http.Json;

namespace anskus.Application.Services
{
    public class CuestionarioActivoServices : ICuestionarioActivoServices
    {
        private readonly HttpClientServices _httpClientServices;
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());

        public CuestionarioActivoServices(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }

        public async Task<CuestionarioActivoResponse> ActivarCuestionario(Guid? IdCuestionario)
        {
            var response = await (await PrivateClient()).PostAsync($"{Constant.CuestionarioActivoRoute}?idcuestionario={IdCuestionario}", null);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CuestionarioActivoResponse>();
                return result!;
            }
            return null!;
        }
    }
}
