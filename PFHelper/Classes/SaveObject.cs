using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
  class SaveObject
  {
    public List<CombatEffectItem> CombatEffects { get; set; }
    public List<CombatGridItem> CombatGridItems { get; set; }
    public FantasyDate Date { get; set; }
    public Weather Weather { get; set; }
    public string ApiPass { get; set; }
    public int Apl { get; set; }
    public int Rations { get; set; }
    public int ContinentId { get; set; }
    public int CombatRound { get; set; }
    public bool CbxInfRations { get; set; }
    public bool CbxGroup { get; set; }
    public bool CbxZone { get; set; }
    public bool CbxTime { get; set; }
    public bool CbxNpc { get; set; }
  }
}
