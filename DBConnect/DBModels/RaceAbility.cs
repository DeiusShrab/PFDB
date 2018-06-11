﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect.DBModels
{
  public class RaceAbility
  {
    public int RaceAbilityId { get; set; }
    public int RaceId { get; set; }
    public string ReplacesRaceAbilities { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual Race Race { get; set; }
    public virtual List<int> ReplacesRaceAbilitiesList
    {
      get
      {
        var ret = new List<int>();
        if (!string.IsNullOrWhiteSpace(ReplacesRaceAbilities))
        {
          int i = 0;
          foreach (var id in ReplacesRaceAbilities.Split(','))
          {
            if (int.TryParse(id.Trim(), out i))
              ret.Add(i);
          }
        }

        return ret;
      }
    }
  }
}