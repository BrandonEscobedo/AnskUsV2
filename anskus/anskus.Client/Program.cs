using anskus.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using anskus.Application.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using CurrieTechnologies.Razor.SweetAlert2;
using MudBlazor.Services;
using anskus.Application.Account;
using anskus.Application.Extensions;
using System.Net.Http.Headers;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.ApplicationClientService();
builder.Services.AddMudServices();
//builder.Services.AddScoped(async sp =>
//{
//    var localStorageService = sp.GetRequiredService<IAuthService>();
//    var tokenModel = await localStorageService.GetAccessTokenAsync();
//    var token = tokenModel;
//    Console.WriteLine("Token: " + token);

//    var hubconnection = new HubConnectionBuilder()
//        .WithUrl("https://localhost:7229/ChatCuest", options =>
//        {
//            options.UseDefaultCredentials = true;


//        })
//       .WithAutomaticReconnect()
//        .Build();

//    return hubconnection;


//});
builder.Services.AddScoped( sp =>
{   
    var hubconnection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7229/ChatCuest")
       .WithAutomaticReconnect()
        .Build();

    return hubconnection;


});
builder.Services.AddSweetAlert2();
    
await builder.Build().RunAsync();
