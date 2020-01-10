using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
    public class CharacterMagic
    {
    public int CharacterMagicId { get; set; }
    public int CharacterId { get; set; }
    public int SpellId { get; set; }
    public string Notes { get; set; }
    public MagicType MagicType { get; set; }

    public virtual Character Character { get; set; }
    public virtual Spell Spell { get; set; }
  }
}
