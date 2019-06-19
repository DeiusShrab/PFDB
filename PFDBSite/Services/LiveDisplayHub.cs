using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace PFDBSite.Services
{
  public class LiveDisplayHub : Microsoft.AspNetCore.SignalR.Hub
  {
    private IHostingEnvironment _env;
    private List<Season> _seasons;
    private List<Month> _months;

    public LiveDisplayHub(IHostingEnvironment env)
    {
      _env = env;
      var context = PFDAL.GetContext();
      _seasons = context.Season.ToList();
      _months = context.Month.ToList();
    }

    public async Task SendMessage(string user, string message)
    {
      var chatMessage = new ChatMessage();
      chatMessage.Contents = message;

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
          message = message.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
        else if (cmd.Equals("dmwhisper", StringComparison.InvariantCultureIgnoreCase) || cmd.Equals("dw", StringComparison.InvariantCultureIgnoreCase))
        {
          // DM Whisper
          message = message.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
        else if (cmd.Equals("flip", StringComparison.InvariantCultureIgnoreCase) || cmd.Equals("f", StringComparison.InvariantCultureIgnoreCase))
        {
          // Flip table
          message = $"**{user} flips the table in a fit of rage!**";
        }
      }
      else
      {
        message = message.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
      }

      chatMessage.Contents = message;

      await Clients.All.SendAsync("ReceiveMessage", chatMessage);
    }

    public async Task SendDrawing(string user, string drawing)
    {
      await Clients.All.SendAsync("ReceiveDrawing", user, drawing);
    }

    public async Task UpdateCombat(string combatJson)
    {
      await Clients.All.SendAsync("UpdateCombat", combatJson);
    }

    public async Task UpdateDate(string date)
    {
      var CurrentDate = new FantasyDate(date);
      var CurrentMonth = _months.FirstOrDefault(x => x.MonthOrder == CurrentDate.Month);
      var grandDate = $"YEAR {CurrentDate.Year} AA, Season of {_seasons.FirstOrDefault(x => x.SeasonId == CurrentMonth.SeasonId)?.Name}, Month of {CurrentMonth?.Name}, Day {CurrentDate.Day}";
      await Clients.All.SendAsync("UpdateDate", new { grandDate, date, monthName = CurrentMonth.Name, day = CurrentDate.Day });
    }

    internal class Emoji
    {
      public int Id { get; set; }
      public string Path { get; set; }
    }
  }
}
