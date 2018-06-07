using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PFDBSite.Services
{
  public class LiveDisplayHub : Microsoft.AspNetCore.SignalR.Hub
  {
    public async Task SendMessage(string user, string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendDrawing(string user, string drawing)
    {
      await Clients.All.SendAsync("ReceiveDrawing", user, drawing);
    }
  }
}
