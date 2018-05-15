using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
    public class Player
    {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
  }
}
