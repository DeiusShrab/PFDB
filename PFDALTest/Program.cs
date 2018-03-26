using System;
using System.Linq;
using System.Text.RegularExpressions;
using PFDAL.Models;

namespace PFDALTest
{
  class Program
  {
    static void Main(string[] args)
    {
      int menu = -1;
      while (menu != 0)
      {
        menu = MainMenu();
        switch (menu)
        {
          case 1:
            Test();
            break;
          case 2:
            UpdateBestiary();
            break;
          case 3:
            PopulateTables();
            break;
        }
      }
    }

    private static int MainMenu()
    {
      Console.WriteLine();
      Console.WriteLine("MAIN MENU");
      Console.WriteLine();
      Console.WriteLine("1 - Test");
      Console.WriteLine("2 - PFDB Bestiary Update");
      Console.WriteLine("3 - PFDB Feat/Skill Update");
      Console.WriteLine("0 - EXIT");
      Console.Write("> ");

      var input = Console.ReadLine();

      int.TryParse(input, out int ret);

      return ret;
    }

    // Menu 1
    private static void Test()
    {
      var context = new PFDBContext();
      Console.WriteLine(context.Bestiary.First().Name);
    }

    // Menu 2
    private static void UpdateBestiary()
    {
      var context = new PFDBContext();
      //int i = 0;
      int[] idList = { 473, 474, 475, 476, 477, 478 };
      var bList = new PFDBContext().Bestiary.Where(x => idList.Contains(x.BestiaryId));
      foreach (var z in bList)
      {
        try
        {
          if (string.IsNullOrWhiteSpace(z.AbilityScores))
            continue;

          var b = context.Bestiary.First(x => x.BestiaryId == z.BestiaryId);

          // AbilityScores - split
          // Str 12, Dex 14, Con 13, Int 9, Wis 10, Cha 9
          // Handle creatures with no Con, Int, etc like undead or plants
          foreach (var item in b.AbilityScores.Split(','))
          {
            var stat = item.Trim();
            if (stat.StartsWith("Str", StringComparison.InvariantCultureIgnoreCase))
            {
              if (int.TryParse(Regex.Match(stat, "[0-9]+").ToString(), out int val))
                b.Str = val;
            }
            else if (stat.StartsWith("Dex", StringComparison.InvariantCultureIgnoreCase))
            {
              if (int.TryParse(Regex.Match(stat, "[0-9]+").ToString(), out int val))
                b.Dex = val;
            }
            else if (stat.StartsWith("Con", StringComparison.InvariantCultureIgnoreCase))
            {
              if (int.TryParse(Regex.Match(stat, "[0-9]+").ToString(), out int val))
                b.Con = val;
            }
            else if (stat.StartsWith("Int", StringComparison.InvariantCultureIgnoreCase))
            {
              if (int.TryParse(Regex.Match(stat, "[0-9]+").ToString(), out int val))
                b.Int = val;
            }
            else if (stat.StartsWith("Wis", StringComparison.InvariantCultureIgnoreCase))
            {
              if (int.TryParse(Regex.Match(stat, "[0-9]+").ToString(), out int val))
                b.Wis = val;
            }
            else if (stat.StartsWith("Cha", StringComparison.InvariantCultureIgnoreCase))
            {
              if (int.TryParse(Regex.Match(stat, "[0-9]+").ToString(), out int val))
                b.Cha = val;
            }
          }

          if (!b.Str.HasValue)
            b.Str = 0;
          if (!b.Dex.HasValue)
            b.Dex = 0;
          if (!b.Con.HasValue)
            b.Con = 0;
          if (!b.Int.HasValue)
            b.Int = 0;
          if (!b.Wis.HasValue)
            b.Wis = 0;
          if (!b.Cha.HasValue)
            b.Cha = 0;

          b.AbilityScores = null;

          // AC - split, make sure AC is an int
          // 17, touch 17, flat-footed 12
          foreach (var item in b.Ac.Split(','))
          {
            var ac = item.Trim();
            if (ac.StartsWith("touch"))
            {
              if (int.TryParse(Regex.Match(ac, "[0-9]+").ToString(), out int val))
                b.Actouch = val;
            }
            else if (ac.StartsWith("flat"))
            {
              if (int.TryParse(Regex.Match(ac, "[0-9]+").ToString(), out int val))
                b.Acflat = val;
            }
            else
            {
              if (int.TryParse(Regex.Match(ac, "[0-9]+").ToString(), out int val))
                b.Ac = val.ToString();
            }
          }

          // HD - remove parenthesis, make sure it's in AdB+C format
          b.Hd = b.Hd.Replace("(", "").Replace(")", "");
          if (!b.Hd.Contains("d"))
            b.Hd = "1d" + b.Hd;
          if (!b.Hd.Contains("+") && !b.Hd.Contains("-"))
            b.Hd = b.Hd + "+0";

          if (!b.BaseAtk.HasValue)
            b.BaseAtk = 0;

          // Speed - split, make sure base speed is an int
          // WAIT UNTIL ALT SPEEDS CHANGED TO INT
          // Burrow, Climb, Fly, Land, Swim
          //b.Speed;

          if (!b.CharacterFlag.HasValue)
            b.CharacterFlag = false;

          if (!b.Cmb.HasValue)
            b.Cmd = b.BaseAtk.Value + GetAbilityMod(b.Str.Value);

          if (!b.Cmd.HasValue)
            b.Cmd = 10 + b.BaseAtk.Value + GetAbilityMod(b.Str.Value) + GetAbilityMod(b.Dex.Value);

          if (!b.CompanionFlag.HasValue)
            b.CompanionFlag = false;

          if (!b.Cr.HasValue)
          {
            int cr = -10;
            int.TryParse(Regex.Match(b.Hd, "[0-9]+").ToString(), out cr);
            b.Cr = cr;
          }

          if (!b.DontUseRacialHd.HasValue)
            b.DontUseRacialHd = false;

          if (!b.Fortitude.HasValue)
            b.Fortitude = GetAbilityMod(b.Con.Value);

          if (!b.Hp.HasValue)
            b.Hp = 0;

          if (!b.Init.HasValue)
            b.Init = GetAbilityMod(b.Dex.Value);

          if (!b.IsTemplate.HasValue)
            b.IsTemplate = false;

          if (!b.Mr.HasValue)
            b.Mr = 0;

          if (!b.Mt.HasValue)
            b.Mt = 0;

          if (!b.Reflex.HasValue)
            b.Reflex = GetAbilityMod(b.Dex.Value);

          if (!b.Sr.HasValue)
            b.Sr = 0;

          if (!b.UniqueMonster.HasValue)
            b.UniqueMonster = false;

          if (!b.Will.HasValue)
            b.Will = GetAbilityMod(b.Wis.Value);

          if (!b.Xp.HasValue)
            b.Xp = 0;

          // Spawn
          context.MonsterSpawn.Add(new MonsterSpawn() { BestiaryId = b.BestiaryId });

          //if (i++ >= 5)
          //{
            context.SaveChanges();
            context.Dispose();
            context = new PFDBContext();
            //i = 0;
            Console.WriteLine("Finished BID " + b.BestiaryId.ToString());
          //}
        }
        catch (Exception ex)
        {
          Console.WriteLine($"[{z.BestiaryId}] - {ex.Message}");
          if (ex.InnerException != null)
            Console.WriteLine("InnerException: " + ex.InnerException.Message);

          context.Dispose();
          context = new PFDBContext();
        }
      }

      context.SaveChanges();
    }

    // Menu 3
    private static void PopulateTables()
    {
      var bList = new PFDBContext().Bestiary.Select(x => x);
      foreach (var b in bList)
      {
        var context = new PFDBContext();

        // Feat
        // Improved Initiative, Iron Will, Lightning Reflexes, Skill Focus (Perception)
        if (!string.IsNullOrWhiteSpace(b.Feats))
        {
          foreach (var feat in b.Feats.Split(','))
          {
            string featName = feat.Trim();
            string notes = null;
            if (featName.Contains("("))
            {
              notes = featName.Split('(')[1].TrimEnd(')');
              featName = featName.Split('(')[0].Trim();
            }
            if (featName.EndsWith('B'))
              featName = featName.Substring(0, feat.Length - 1);
            var f = new BestiaryFeat
            {
              BestiaryId = b.BestiaryId,
              FeatId = context.Feat.FirstOrDefault(x => featName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.FeatId ?? 0,
              Notes = notes
            };

            context.BestiaryFeat.Add(f);
          }
        }

        // Subtype - COMPLETE
        // (chaotic, evil, extraplanar)
        //if (!string.IsNullOrWhiteSpace(b.SubType))
        //{
        //  foreach (var item in b.SubType.Replace("(", "").Replace(")", "").Split(','))
        //  {
        //    if (!context.BestiaryType.Any(x => x.Name.Equals(item.Trim(), StringComparison.InvariantCultureIgnoreCase)))
        //    {
        //      context.BestiaryType.Add(new BestiaryType() { Name = item.Trim() });
        //      context.SaveChanges();
        //    }

        //    var bType = context.BestiaryType.First(x => x.Name.Equals(item.Trim(), StringComparison.InvariantCultureIgnoreCase));
        //    context.BestiarySubType.Add(new BestiarySubType() { BestiaryId = b.BestiaryId, BestiaryTypeId = bType.BestiaryTypeId });
        //    context.SaveChanges();
        //  }

        //  b.SubType = null;
        //}

        // Environment - ALMOST COMPLETE
        //if (!string.IsNullOrWhiteSpace(b.Environment))
        //{
        //  foreach (var environment in b.Environment.Split(','))
        //  {
        //    string envName = environment.Trim();
        //    string notes = null;
        //    if (envName.Contains("("))
        //    {
        //      notes = envName.Split('(')[1].TrimEnd(')');
        //      envName = envName.Split('(')[0].Trim();
        //    }

        //    if (!context.Environment.Select(x => x.Name.ToLower()).Contains(envName.ToLower()))
        //    {
        //      context.Environment.Add(new PFDAL.Models.Environment()
        //      {
        //        Name = envName,
        //        Notes = null,
        //        Temperature = string.Empty,
        //        TravelSpeed = 0
        //      });
        //      context.SaveChanges();
        //    }

        //    var e = new BestiaryEnvironment
        //    {
        //      BestiaryId = b.BestiaryId,
        //      EnvironmentId = context.Environment.FirstOrDefault(x => envName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.EnvironmentId ?? 0,
        //      Notes = notes
        //    };

        //    context.BestiaryEnvironment.Add(e);
        //    context.SaveChanges();
        //  }
        //}

        // Skill - NEEDS WORK
        if (!string.IsNullOrWhiteSpace(b.Skills))
        {
          foreach (var skill in b.Skills.Split(','))
          {
            string skillName = skill.Trim();
            string notes = null;
            int bonus = 0;

            if (skillName.Contains("("))
            {
              notes = skillName.Split('(')[1].TrimEnd(')');
              skillName = skillName.Split('(')[0].Trim();
            }

            if (skillName.Contains("+"))
            {
              var skillsplit = skillName.Split('+');
              if (skillsplit[1].Contains(" "))
                bonus = Convert.ToInt32(skillsplit[1].Split(' ')[0]);
              else
                bonus = Convert.ToInt32(skillsplit[1]);

              skillName = skillName.Split('+')[0].Trim();
            }
            else if (skillName.Contains("-"))
            {
              bonus = Convert.ToInt32(skillName.Split('-')[1]);
              skillName = skillName.Split('-')[0].Trim();
            }

            if (!context.Skill.Select(x => x.Name.ToLower()).Contains(skillName.ToLower()))
            {
              context.Skill.Add(new Skill()
              {
                Name = skillName,
                Description = "UPDATE ME",
                TrainedOnly = false
              });
              context.SaveChanges();
            }

            var s = new BestiarySkill
            {
              BestiaryId = b.BestiaryId,
              SkillId = context.Skill.FirstOrDefault(x => skillName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.SkillId ?? 0,
              Notes = notes
            };

            context.BestiarySkill.Add(s);
            context.SaveChanges();
          }
        }

        // Language - COMPLETE
        //if (!string.IsNullOrWhiteSpace(b.Languages))
        //{
        //  foreach (var language in b.Languages.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries))
        //  {
        //    string langName = language.Trim();
        //    string notes = null;
        //    if (langName.Contains("("))
        //    {
        //      notes = langName.Split('(')[1].TrimEnd(')');
        //      langName = langName.Split('(')[0].Trim();
        //    }

        //    if (!context.Language.Select(x => x.Name.ToLower()).Contains(langName.ToLower()))
        //    {
        //      context.Language.Add(new Language()
        //      {
        //        Name = langName,
        //        Notes = null
        //      });
        //      context.SaveChanges();
        //    }

        //    var l = new BestiaryLanguage
        //    {
        //      BestiaryId = b.BestiaryId,
        //      LanguageId = context.Language.FirstOrDefault(x => langName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.LanguageId ?? 0,
        //      Notes = notes
        //    };

        //    context.BestiaryLanguage.Add(l);
        //    context.SaveChanges();
        //  }
        //}
      }
    }

    private static int GetAbilityMod(int score)
    {
      if (score > 0)
        return (int)Math.Floor((score - 10) / 2.0);
      else
        return 0;
    }
  }
}
