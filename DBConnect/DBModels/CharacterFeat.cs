using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class CharacterFeat
  {
    public int CharacterFeatId { get; set; }
    public int CharacterId { get; set; }
    public int FeatId { get; set; }
    public string Notes { get; set; }

    public virtual Character Character { get; set; }
    public virtual Feat Feat { get; set; }
  }
}
