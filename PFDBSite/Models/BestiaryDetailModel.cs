using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PFDAL.Models;

namespace PFDBSite.Models
{
    public class BestiaryDetailModel
    {
        
        public BestiaryDetailModel(Bestiary b)
        {
            Bestiary = b;
        }

        private Bestiary Bestiary;

        [DisplayName("BID")]
        public int BestiaryId
        {
            get { return Bestiary.BestiaryId; }
            set { Bestiary.BestiaryId = value; }
        }
    }
}
