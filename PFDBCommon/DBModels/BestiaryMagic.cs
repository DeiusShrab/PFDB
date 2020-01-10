using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PFDBCommon.DBModels
{
  public partial class BestiaryMagic
  {
    public int BestiaryMagicId { get; set; }
    public int BestiaryId { get; set; }
    public int SpellId { get; set; }
    public string Notes { get; set; }
    public int CasterLevel { get; set; }
    public int UsesPerDay { get; set; }
    public MagicType MagicTypeId { get; set; }

    public virtual Bestiary Bestiary { get; set; }
    public virtual Spell Spell { get; set; }
  }
}
