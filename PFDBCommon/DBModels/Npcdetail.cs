﻿using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Npcdetail
  {
    public int Npcid { get; set; }
    public string MonsterSource { get; set; }
    public string Description { get; set; }
    public string FullText { get; set; }

    public virtual Npc Npc { get; set; }
  }
}
