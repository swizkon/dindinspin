using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class SignalRCounter : Hub
    {
        public Task IncrementCounter()
        {
            return InvokeAsync("IncrementCounter");
        }

        public Task DecrementCounter()
        {
            return InvokeAsync("DecrementCounter");
        }

        public Task ResetCounter()
        {
            return InvokeAsync("ResetCounter");
        }

        public Task InvokeAsync(string action)
        {
            var ConnectionIDToIgnore = new List<string> { Context.ConnectionId };
            
            return Clients.AllExcept(ConnectionIDToIgnore).SendAsync(action);
        }
    }
}