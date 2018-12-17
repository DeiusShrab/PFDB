using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class ChatRoom
  {
    public int ChatRoomId { get; set; }
    public string ChatRoomName { get; set; }
    public int MaxCapacity { get; set; }
    public bool Public { get; set; } // True means all players can use, False means a permission is required
    public int MessageHistoryOnJoin { get; set; } // Number of previous messages to show on join, 0 for none
    public int PlayerOwner { get; set; } // PlayerId of the chat room creator, 0 for default rooms
    public string WelcomeMessage { get; set; }
  }
}
