using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
    public class ClassSkill
    {
    public int ClassId { get; set; }
    public int SkillId { get; set; }

    public Class Class { get; set; }
    public Skill Skill { get; set; }
  }
}
