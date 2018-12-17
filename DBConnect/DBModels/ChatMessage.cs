using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class ChatMessage
  {
    public int MessageId { get; set; }
    public int SenderId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Contents { get; set; }
    public int ChatRoom { get; set; }
    public int WhisperTo { get; set; } // 0 if public message
  }
}
