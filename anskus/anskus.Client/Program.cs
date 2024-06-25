using anskus.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using anskus.Application.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using CurrieTechnologies.Razor.SweetAlert2;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.ApplicationClientService();
builder.Services.AddScoped(sp =>
{
    return new HubConnectionBuilder()
   .WithUrl("https://localhost:7229/ChatCuest")
   .WithAutomaticReconnect()
   .Build();

});
builder.Services.AddSweetAlert2();
    
await builder.Build().RunAsync();
