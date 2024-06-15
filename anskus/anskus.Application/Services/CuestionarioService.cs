using anskus.Application.Extensions;
using anskus.Domain.Models;
using System.Net.Http.Json;

namespace anskus.Application.Services
{
    public class CuestionarioService : ICuestionarioService
    {
        private readonly HttpClientServices _httpClientServices;
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());
        public CuestionarioService(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }

        public async Task<Cuestionario> ActualizarCuestionario(Cuestionario cuestionario)
        {
            var response =await(await PrivateClient()).PutAsJsonAsync(Constant.CuestionarioRoute,cuestionario);
            if (response.IsSuccessStatusCode)
            {
                var result =await response.Content.ReadFromJsonAsync<Cuestionario>();
                return result!;

            }
            var errorContent=await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar el cuestionario: {errorContent}");

          
        }

        public async Task<Cuestionario> CrearCuestionario(Cuestionario cuestionario)
        {
            var response = await(await PrivateClient()).PostAsJsonAsync(Constant.CuestionarioRoute,cuestionario);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Cuestionario>();
                return result!;
            }
            
            var errorContent=await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al crear el cuestionario: {errorContent}");
        }

        public Task EliminarCuestionario(Cuestionario cuestionario)
        {
            throw new NotImplementedException();
        }
    }

}
