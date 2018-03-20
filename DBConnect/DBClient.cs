using System;
using System.Collections.Generic;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace DBConnect
{
    public static class DBClient
    {
        private static int MAX_CACHE_SIZE = 10;
        private static Dictionary<int, BestiaryDetail> DetailCache = new Dictionary<int, BestiaryDetail>();

        public static RandomEncounterResult GetEncounters(RandomEncounterRequest request)
        {
            var list = new List<RandomEncounterItem>() {
                new RandomEncounterItem(){ Name="Derpsaur", Cr=1, BestiaryId=9999 }
            };

            var ret = new RandomEncounterResult
            {
                EncounterItems = list,
                Success = true
            };


            // API Call for GetEncounter

            return ret;
        }

        public static Bestiary GetBestiary(int bestiaryId)
        {
            var ret = new Bestiary();

            // API Call for GetBestiary

            return ret;
        }

        public static BestiaryDetail GetBestiaryDetail(int bestiaryId)
        {
            var ret = new BestiaryDetail();

            if (DetailCache.ContainsKey(bestiaryId))
                ret = DetailCache[bestiaryId];
            else
            {
                if (DetailCache.Count >= MAX_CACHE_SIZE)
                    DetailCache.Clear();

                // API Call for GetBestiaryDetail
                // Add BestiaryDetail to cache
            }


            return ret;
        }

        public static List<Continent> GetContinents()
        {
            // API Call
            return new List<Continent>();
        }

        public static List<Season> GetSeasons()
        {
            // API Call
            return new List<Season>();
        }
    }
}
