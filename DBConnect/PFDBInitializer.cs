using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect
{
  public static class PFDBInitializer
  {
    public static void Initialize(PFDBContext context)
    {
      context.Database.EnsureCreated();

      if (context.Bestiary.Any())
        return;

      context.Bestiary.Add(new DBModels.Bestiary()
      {
        BestiaryId = 1,
        Name = "TestBeast",
        Hd = "1d1",
      });
    }
  }
}
