using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class FavoredClass
  {
    public int FavoredClassId { get; set; }
    public int ClassId { get; set; }
    public int RaceId { get; set; }
    public string Description { get; set; }

    public virtual Class Class { get; set; }
    public virtual Race Race { get; set; }
  }
}
