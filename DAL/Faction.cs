//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Faction
    {
        public int FactionId { get; set; }
        public Nullable<int> ContinentId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public Nullable<int> Parent_Faction { get; set; }
        public string Primary_Race { get; set; }
        public string Type { get; set; }
    }
}
