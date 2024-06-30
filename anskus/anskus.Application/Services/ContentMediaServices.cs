using anskus.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface IContentMediaServices
    {
        public Task<Guid> UploadContent(MultipartFormDataContent file);
        public Task EliminarPreguntaAsync(Guid idPregunta);

    }
    public class ContentMediaServices : IContentMediaServices
    {
        private readonly HttpClientServices _httpClientServices;
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());

        public ContentMediaServices(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }
        public async Task EliminarPreguntaAsync(Guid idPregunta)
        {
            await (await PrivateClient()).DeleteAsync($"{Constant.ContentMedia}/{idPregunta}");
        }
        public async Task<Guid> UploadContent(MultipartFormDataContent file)
        {
            var response = await (await PrivateClient()).PostAsync(Constant.ContentMedia, file);
            var result =await response.Content.ReadFromJsonAsync<Guid>();
            return result;
        }
    }
}
