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
    
    public partial class Location
    {
        public int Locationid { get; set; }
        public Nullable<int> ContinentId { get; set; }
        public string Name { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }
        public Nullable<int> FactionId { get; set; }
        public Nullable<int> TerritoryId { get; set; }
    }
}
