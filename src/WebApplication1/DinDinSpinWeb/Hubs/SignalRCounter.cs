﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DinDinSpinWeb.Hubs
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

        private Task InvokeAsync(string action)
        {
            var ConnectionIDToIgnore = new List<string> { Context.ConnectionId };
            
            return Clients.AllExcept(ConnectionIDToIgnore).SendAsync(action);
        }
    }
}