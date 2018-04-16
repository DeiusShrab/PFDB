using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFDBSite.Helpers
{
  public class QueryHelper
  {
    public SpellLookupResponse SpellLookup(SpellLookupRequest request)
    {
      var response = new SpellLookupResponse() { SpellDetails = new List<SpellDetail>(), Spells = new List<Spell>() };

      var context = PFDAL.GetContext();
      var levelList = new List<int>();
      if (request.Lv0)
        levelList.Add(0);
      if (request.Lv1)
        levelList.Add(1);
      if (request.Lv2)
        levelList.Add(2);
      if (request.Lv3)
        levelList.Add(3);
      if (request.Lv4)
        levelList.Add(4);
      if (request.Lv5)
        levelList.Add(5);
      if (request.Lv6)
        levelList.Add(6);
      if (request.Lv7)
        levelList.Add(7);
      if (request.Lv8)
        levelList.Add(8);
      if (request.Lv9)
        levelList.Add(9);

      var spellList = from s in context.Spell
                      where (
                        (request.Sor && levelList.Contains(s.Sor ?? -1))
                        || (request.Wiz && levelList.Contains(s.Wiz ?? -1))
                        || (request.Cleric && levelList.Contains(s.Cleric ?? -1))
                        || (request.Druid && levelList.Contains(s.Druid ?? -1))
                        || (request.Ranger && levelList.Contains(s.Ranger ?? -1))
                        || (request.Bard && levelList.Contains(s.Bard ?? -1))
                        || (request.Paladin && levelList.Contains(s.Paladin ?? -1))
                        || (request.Alchemist && levelList.Contains(s.Alchemist ?? -1))
                        || (request.Summoner && levelList.Contains(s.Summoner ?? -1))
                        || (request.Witch && levelList.Contains(s.Witch ?? -1))
                        || (request.Inquisitor && levelList.Contains(s.Inquisitor ?? -1))
                        || (request.Oracle && levelList.Contains(s.Oracle ?? -1))
                        || (request.Antipaladin && levelList.Contains(s.Antipaladin ?? -1))
                        || (request.Magus && levelList.Contains(s.Magus ?? -1))
                        || (request.Adept && levelList.Contains(s.Adept ?? -1))
                        || (request.BloodRager && levelList.Contains(s.BloodRager ?? -1))
                        || (request.Shaman && levelList.Contains(s.Shaman ?? -1))
                        || (request.Psychic && levelList.Contains(s.Psychic ?? -1))
                        || (request.Medium && levelList.Contains(s.Medium ?? -1))
                        || (request.Mesmerist && levelList.Contains(s.Mesmerist ?? -1))
                        || (request.Occultist && levelList.Contains(s.Occultist ?? -1))
                        || (request.Spiritualist && levelList.Contains(s.Spiritualist ?? -1))
                        || (request.Skald && levelList.Contains(s.Skald ?? -1))
                        || (request.Investigator && levelList.Contains(s.Investigator ?? -1))
                        || (request.Hunter && levelList.Contains(s.Hunter ?? -1))
                        )
                      select s;

      if (spellList != null)
      {
        var spellDetails = from sd in context.SpellDetail
                           where spellList.Select(x => x.SpellId).Contains(sd.SpellId)
                           select sd;

        response.SpellDetails = spellDetails.ToList();
        response.Spells = spellList.ToList();
      }

      return response;
    }
  }
}
