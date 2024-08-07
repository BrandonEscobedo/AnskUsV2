﻿using anskus.Application.DTOs;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using anskus.Application.HubServices;
using System.Net.Http.Json;
using System.Text.Json;

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
                    var resultHub = await _hubconnectionService.CreateRoom(result);
                    if (resultHub)
                        return result.IdcuestionarioActivo;
                    else
                        await EliminarCuestionarioActivoAsync(IdCuestionario);
                }
            }
            return Guid.Empty;
        }

        public async Task<(Guid IdCuestionario, Dictionary<string, string[]> errors)> AddUserToRoom(int Codigo, string Nombre)
        {

            var response = await (await PrivateClient()).PostAsync($"{Constant.CuestionarioActivoRoute}/Sala?code={Codigo}&nombre={Nombre}", null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Guid>();
                if (result != Guid.Empty)
                {
                    return (result, null);
                }
                return (Guid.Empty, null);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ValidationErrors = JsonSerializer.Deserialize<ValidationProblems>(content);
                return (Guid.Empty, ValidationErrors?.errors);
            }
            else
            {
                throw new Exception("Ocurrio un  error inesperado");
            }
        }
        public async Task EliminarCuestionarioActivoAsync(Guid? idcuestionario)
        {
            var response = await (await PrivateClient()).DeleteAsync($"{Constant.CuestionarioActivoRoute}/{idcuestionario}");
            if (response.IsSuccessStatusCode)
            {

            }
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
