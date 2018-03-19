using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.ConnectModels
{
    public class RandomEncounterResult
    {
        public List<RandomEncounterItem> EncounterItems { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class RandomEncounterItem
    {
        public string Name { get; set; }
        public int Cr { get; set; }
        public int BestiaryId { get; set; }
    }
}
