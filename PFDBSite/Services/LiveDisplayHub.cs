using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeKicker.BBCode;
using DBConnect.DBModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace PFDBSite.Services
{
  public class LiveDisplayHub : Microsoft.AspNetCore.SignalR.Hub
  {
    // Tags = [T]text[/T] - B, I, U, S, numbers for 16 colors
    // Emojis? [E:name]
    private List<BBTag> _BBTags = new List<BBTag>
    {
      new BBTag("b", "<b>", "</b>"),
      new BBTag("i", "<span style=\"font-style:italic;\">", "</span>"),
      new BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>"),
      new BBTag("s", "<span style=\"text-decoration:strikethrough;\">", "</span>"),
      new BBTag("0", "<span style=\"color:Black;\">", "</span>"),
      new BBTag("1", "<span style=\"color:Navy;\">", "</span>"),
      new BBTag("2", "<span style=\"color:Green;\">", "</span>"),
      new BBTag("3", "<span style=\"color:Teal;\">", "</span>"),
      new BBTag("4", "<span style=\"color:Maroon;\">", "</span>"),
      new BBTag("5", "<span style=\"color:Purple;\">", "</span>"),
      new BBTag("6", "<span style=\"color:Olive;\">", "</span>"),
      new BBTag("7", "<span style=\"color:Silver;\">", "</span>"),
      new BBTag("8", "<span style=\"color:Gray;\">", "</span>"),
      new BBTag("9", "<span style=\"color:Blue;\">", "</span>"),
      new BBTag("10", "<span style=\"color:Lime;\">", "</span>"),
      new BBTag("11", "<span style=\"color:Aqua;\">", "</span>"),
      new BBTag("12", "<span style=\"color:Red;\">", "</span>"),
      new BBTag("13", "<span style=\"color:Fuchsia;\">", "</span>"),
      new BBTag("14", "<span style=\"color:Yellow;\">", "</span>"),
      new BBTag("15", "<span style=\"color:White;\">", "</span>"),
    };

    private readonly BBCodeParser _parser;
    private IHostingEnvironment _env;

    public LiveDisplayHub(IHostingEnvironment env)
    {
      _env = env;

      // Load emoji BBTags from file
      try
      {
        var path = Path.Combine(_env.WebRootPath, "data", "chatEmojis.json");
        var emojis = JsonConvert.DeserializeObject<List<Emoji>>(File.ReadAllText(path));
        foreach (var emoji in emojis)
        {
          _BBTags.Add(new BBTag($"[E:{emoji.Id}]", $"<img src=\"{Path.Combine(_env.WebRootPath, "data", emoji.Path)}\" />", "", true, false));
        }
      }
      catch (Exception ex)
      {

      }

      _parser = new BBCodeParser(_BBTags);
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

      if (message.Contains('['))
      {
        message = _parser.ToHtml(message);
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

    internal class Emoji
    {
      public int Id { get; set; }
      public string Path { get; set; }
    }
  }
}
