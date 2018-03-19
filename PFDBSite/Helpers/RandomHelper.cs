using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFDAL.ConnectModels;
using PFDAL.Models;

namespace PFDBSite.Helpers
{
    public class RandomHelper
    {
        private PFDBContext context = new PFDBContext();
        private Random random = new Random();

        public RandomEncounterResult GenerateRandomEncounters(RandomEncounterRequest request)
        {
            var encounterList = new List<RandomEncounterItem>();
            var groupMon = new List<Bestiary>();
            var validSpawns = context.Bestiary.Where(x => x.CompanionFlag == request.Npc);
            
            var paramContinent = new SqlParameter("@ContinentId", request.ContinentId);

            foreach (var cr in request.Crs)
            {
                Bestiary mon = new Bestiary();
                if (request.Group && groupMon.Select(x => x.Cr).Contains(cr))
                {
                    mon = groupMon.First(x => x.Cr == cr);
                }
                else
                {
                    var monList = validSpawns.Where(x => x.Cr == cr);
                    mon = monList.ElementAt(random.Next(monList.Count()));
                }

                if (mon != null)
                {
                    groupMon.Add(mon);
                    encounterList.Add(new RandomEncounterItem() { BestiaryId = mon.BestiaryId, Cr = mon.Cr ?? 0, Name = mon.Name });
                }
            }

            return new RandomEncounterResult()
            {
                EncounterItems = encounterList,
                Message = $"Generated {encounterList.Count} items",
                Success = encounterList.Any()
            };
        }
    }
}
