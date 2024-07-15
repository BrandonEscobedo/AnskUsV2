using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices
{
    public class HubConnectionManager : IHubConnectionManager
    {
        //private readonly IServiceProvider _serviceProvider;
        private readonly HubConnection _hubConnection;
        public HubConnectionManager(HubConnection hubConnection )
        {
            _hubConnection= hubConnection;
        }

        public HubConnection HubConnection => _hubConnection;

        public async Task GetHubConnectionAsync()
        {
            //if (_hubConnection == null)
            //{
            //    _hubConnection = await _serviceProvider.GetRequiredService<Task<HubConnection>>();
            //}

            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _hubConnection.StartAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting hub connection: {ex.Message}");
                }
            }       
        }
    }
}
