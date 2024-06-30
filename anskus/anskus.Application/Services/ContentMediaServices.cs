using anskus.Application.Extensions;
using anskus.Domain.Models;
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
        public Task<DatosMedia> UploadContent(MultipartFormDataContent file);
        public Task<bool> EliminarImagenAsync(Guid idFile);
    }
    public class ContentMediaServices : IContentMediaServices
    {
        private readonly HttpClientServices _httpClientServices;
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());

        public ContentMediaServices(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }
        public async Task<bool> EliminarImagenAsync(Guid idFile)
        {
          var result=  await (await PrivateClient()).DeleteAsync($"{Constant.ContentMedia}/{idFile}");
           if(result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<DatosMedia> UploadContent(MultipartFormDataContent file)
        {
            var response = await (await PrivateClient()).PostAsync(Constant.ContentMedia, file);
            var result =await response.Content.ReadFromJsonAsync<DatosMedia>();
            if (result != null)
            {
                return result;
            }
            else return null!;
        }
    }
}
