using anskus.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using anskus.Application.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
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
await builder.Build().RunAsync();
