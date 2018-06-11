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
      if (message.StartsWith('/'))
      {
        var cmd = string.Empty;
        if (message.Contains(' '))
        {
          cmd = message.Substring(1, message.IndexOf(' ') - 1);
          message = message.Substring(cmd.Length + 2);
        }
        else
        {
          cmd = message.Substring(1);
          message = string.Empty;
        }

        if (cmd.Equals("roll", StringComparison.InvariantCultureIgnoreCase) || cmd.Equals("r", StringComparison.InvariantCultureIgnoreCase))
        {
          // Parse roll
        }
        else if (cmd.Equals("whisper", StringComparison.InvariantCultureIgnoreCase) || cmd.Equals("w", StringComparison.InvariantCultureIgnoreCase))
        {
          // Whisper
        }
        else if (cmd.Equals("dmwhisper", StringComparison.InvariantCultureIgnoreCase) || cmd.Equals("dw", StringComparison.InvariantCultureIgnoreCase))
        {
          // DM Whisper
        }
        else if (cmd.Equals("flip", StringComparison.InvariantCultureIgnoreCase) || cmd.Equals("f", StringComparison.InvariantCultureIgnoreCase))
        {
          // Flip table
          message = $"**{user} flips the table in a fit of rage!**";
        }
      }

      message = message.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

      // Emojis?

      // Tags = [T]text[/] - B, I, U, S, numbers for 16 colors

      // Bold = **text**
      // Italic = *text* - make sure it comes after bold

      await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendDrawing(string user, string drawing)
    {
      await Clients.All.SendAsync("ReceiveDrawing", user, drawing);
    }

    public async Task UpdateCombat(string combatJson)
    {
      await Clients.All.SendAsync("UpdateCombat", combatJson);
    }
  }
}
