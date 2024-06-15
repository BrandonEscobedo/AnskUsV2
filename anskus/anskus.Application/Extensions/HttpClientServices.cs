using anskus.Application.DTOs.Request.Account;
using anskus.Application.Extensions;
using NetcodeHub.Packages.Extensions.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace anskus.Application.Extensions
{
    public class HttpClientServices(IHttpClientFactory httpClientFactory, LocalStorageServices localStorageServices)
    {
        private HttpClient CreateClient() => httpClientFactory!.CreateClient(Constant.HttpClientName);
        public HttpClient GetPublicClient() => CreateClient();
        public async Task<HttpClient> GetPrivateClient()
        {
            try
            {
                var client = CreateClient();
                var localStorageDTo = await localStorageServices.GetModelFromToken();

                if (string.IsNullOrEmpty(localStorageDTo.Token))
                {
                    return client;
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constant.HttpClientHadersSchame, localStorageDTo.Token);
                return client;
            }
            catch (Exception ex)
            {
                return new HttpClient();
            }


            //}     public async Task<HttpClient> GetPrivateClient()
            //{
            //    try
            //    {
            //        var client = CreateClient();
            //        var localStorageDTo = await localStorageServices.GetModelFromToken();
            //        if(string.IsNullOrEmpty(localStorageDTo.Token))
            //        {
            //            return client;
            //        }
            //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constant.HttpClientHadersSchame, localStorageDTo.Token);
            //        return client;
            //    }
            //    catch (Exception ex)
            //    {
            //        return new HttpClient();
            //    }
            //}
        }
    }
}
