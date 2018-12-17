using Microsoft.AspNetCore.Mvc;

namespace PFAPI.Controllers
{
  /* //////////////////////// *
   * None of the Live Display stuff is intended to be secure at the moment
   * It's just a quick and dirty chatroom + map because {Popular Online Tabletop} uses too much memory and has too many limitations
   * Also because I just wanted to make one :D
   * /////////////////////// */
  public class LiveMapController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public string GetMessageHistory(int chatRoomId, int playerId)
    {
      // Load previous messages from DB
      // Compile them into list, json it
      // Return it

      return string.Empty;
    }
  }
}