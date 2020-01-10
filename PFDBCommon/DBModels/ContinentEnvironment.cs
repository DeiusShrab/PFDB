using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class ContinentEnvironment
  {
    public int ContinentId { get; set; }
    public int EnvironmentId { get; set; }

    public virtual Continent Continent { get; set; }
    public virtual Environment Environment { get; set; }
  }
}
