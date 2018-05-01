using System;
using System.Linq;
using System.Text.RegularExpressions;
using DBConnect;
using DBConnect.DBModels;

namespace PFDALTest
{
  class Program
  {
    // Split string by comma UNLESS that comma is within parentheses
    private static string REGEX_SPLIT = @"(?:\s)?([^,\r\n\s(][^,\r\n(]*(?<dq>\()?(?(dq)[^\(\r\n]+\)[^\(\)\r\n,]*))";
    static void Main(string[] args)
    {
      Console.WriteLine("Remember to make sure the PFDBContext is in LIVE mode!");
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
            //UpdateBestiary();
            Console.WriteLine("Complete");
            break;
          case 3:
            //PopulateTables();
            Console.WriteLine("Complete");
            break;
          case 4:
            //UpdateTables();
            Console.WriteLine("Complete");
            break;
          case 5:
            //UpdateSpellSchools();
            Console.WriteLine("Complete");
            break;
          case 6:
            UpdateNPC();
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
      Console.WriteLine("4 - Update Tables");
      Console.WriteLine("5 - Update SpellSchools");
      Console.WriteLine("6 - Translate NPCs to Bestiary");
      Console.WriteLine("0 - EXIT");
      Console.Write("> ");

      var input = Console.ReadLine();

      int.TryParse(input, out int ret);

      return ret;
    }

    // Menu 1
    private static void Test()
    {
      var context = PFDAL.GetContext(false);
      Console.WriteLine(context.Bestiary.First().Name);
    }

    // Menu 2
    private static void UpdateBestiary()
    {
      var context = PFDAL.GetContext(false);
      //int i = 0;
      int[] idList = { 473, 474, 475, 476, 477, 478 };
      var bList = PFDAL.GetContext(false).Bestiary.Where(x => idList.Contains(x.BestiaryId));
      /*
      foreach (var z in bList)
      {
        try
        {
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
          foreach (Match item in Regex.Matches(b.Ac, REGEX_SPLIT))
          {
            var ac = item.Groups[1].Value;
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
          context = PFDAL.GetContext(false);
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
          context = PFDAL.GetContext(false);
        }
      }
      */
      context.SaveChanges();
    }

    // Menu 3
    private static void PopulateTables()
    {
      var bList = PFDAL.GetContext(false).Bestiary.Select(x => x);
      foreach (var b in bList)
      {
        var context = PFDAL.GetContext(false);

        // Feat
        // Improved Initiative, Iron Will, Lightning Reflexes, Skill Focus (Perception)
        if (!string.IsNullOrWhiteSpace(b.Feats))
        {
          foreach (Match m in Regex.Matches(b.Feats, REGEX_SPLIT))
          {
            string feat = m.Groups[1].Value;
            string notes = null;
            if (feat.Contains("("))
            {
              notes = feat.Split('(')[1].TrimEnd(')');
              feat = feat.Split('(')[0].Trim();
            }
            if (feat.EndsWith('B'))
              feat = feat.Substring(0, feat.Length - 1);
            var f = new BestiaryFeat
            {
              BestiaryId = b.BestiaryId,
              FeatId = context.Feat.FirstOrDefault(x => feat.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.FeatId ?? 0,
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
          foreach (Match m in Regex.Matches(b.Skills, REGEX_SPLIT))
          {
            string skill = m.Groups[1].Value;
            string notes = null;
            int bonus = 0;

            if (skill.Contains("("))
            {
              var idxA = skill.IndexOf('(') + 1;
              var idxB = skill.LastIndexOf(')');
              notes = skill.Substring(idxA, idxB - idxA);
              var newName = skill.Substring(0, --idxA) + skill.Substring(++idxB, skill.Length - idxB); // -- and ++ to make sure parentheses are dropped
              skill = newName.Trim();
            }

            if (skill.Contains("+"))
            {
              var skillsplit = skill.Split('+');
              if (skillsplit[1].Contains(" "))
                bonus = Convert.ToInt32(skillsplit[1].Split(' ')[0]);
              else
                bonus = Convert.ToInt32(skillsplit[1]);

              skill = skill.Split('+')[0].Trim();
            }
            else if (skill.Contains("-"))
            {
              bonus = Convert.ToInt32(skill.Split('-')[1]) * -1;
              skill = skill.Split('-')[0].Trim();
            }

            if (!context.Skill.Select(x => x.Name.ToLower()).Contains(skill.ToLower()))
            {
              context.Skill.Add(new Skill()
              {
                Name = skill,
                Description = "UPDATE ME",
                TrainedOnly = false
              });
              context.SaveChanges();
            }

            var s = new BestiarySkill
            {
              BestiaryId = b.BestiaryId,
              SkillId = context.Skill.FirstOrDefault(x => skill.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.SkillId ?? 0,
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

    // Menu 4
    private static void UpdateTables()
    {
      foreach (var env in PFDAL.GetContext(false).Environment)
      {
        Console.WriteLine(env.Name);
        var context = PFDAL.GetContext(false);
        var e = context.Environment.First(x => x.EnvironmentId == env.EnvironmentId);
        //context.Environment.Attach(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        if (e.Name.StartsWith(" "))
          e.Name = e.Name.Substring(1);
        if (e.Name.StartsWith("or "))
          e.Name = e.Name.Substring(3);
        e.Name = e.Name.Replace(")", "").Trim();
        context.SaveChanges();
      }
    }

    // Menu 5
    private static void UpdateSpellSchools()
    {
      /*
      var spellList = PFDAL.GetContext().Spell.Where(x => true);

      foreach (var spell in spellList)
      {
        var context = PFDAL.GetContext();
        context.Spell.Attach(spell);
        SpellSchool school = null;
        SpellSubSchool subSchool = null;

        if (!string.IsNullOrWhiteSpace(spell.SchoolId))
        {
          school = context.SpellSchool.FirstOrDefault(x => x.Name.Equals(spell.SchoolId));
          if (school == null || school.SpellSchoolId == 0)
          {
            school = new SpellSchool();
            school.Name = spell.SchoolId;
            context.SpellSchool.Add(school);
            context.SaveChanges();
          }

          spell.SchoolId = school.SpellSchoolId.ToString();
        }
        else
          spell.SchoolId = "0";

        if (!string.IsNullOrWhiteSpace(spell.SubSchoolId))
        {
          subSchool = context.SpellSubSchool.FirstOrDefault(x => x.Name.Equals(spell.SubSchoolId));
          if (subSchool == null || subSchool.SpellSubSchoolId == 0)
          {
            subSchool = new SpellSubSchool();
            subSchool.Name = spell.SubSchoolId;
            if (school == null)
              subSchool.SpellSchoolId = 0;
            else
              subSchool.SpellSchoolId = school.SpellSchoolId;
            context.SpellSubSchool.Add(subSchool);
            context.SaveChanges();
          }

          spell.SubSchoolId = subSchool.SpellSubSchoolId.ToString();
        }
        else
          spell.SubSchoolId = "0";

        context.SaveChanges();
      }
      */
    }

    // Menu 6
    private static void UpdateNPC()
    {
      int i = 0;
      var npcList = PFDAL.GetContext(false).Npc;
      foreach (var z in npcList)
      {
        try
        {
          var context = PFDAL.GetContext(false);
          var npc = context.Npc.First(x => x.Npcid == z.Npcid);
          var b = new Bestiary();
          context.Bestiary.Attach(b);

          // AbilityScores - split
          // Str 12, Dex 14, Con 13, Int 9, Wis 10, Cha 9
          // Handle creatures with no Con, Int, etc like undead or plants
          foreach (var item in npc.AbilityScores.Split(','))
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

          npc.AbilityScores = null;

          // AC - split, make sure AC is an int
          // 17, touch 17, flat-footed 12
          foreach (Match item in Regex.Matches(npc.Ac, REGEX_SPLIT))
          {
            var ac = item.Groups[1].Value;
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
                b.Ac = val;
            }
          }

          // HD - remove parenthesis, make sure it's in AdB+C format
          b.Hd = npc.Hd.Replace("(", "").Replace(")", "");
          if (!b.Hd.Contains("d"))
            b.Hd = "1d" + b.Hd;
          if (!b.Hd.Contains("+") && !b.Hd.Contains("-"))
            b.Hd = b.Hd + "+0";

          b.BaseAtk = npc.BaseAtk ?? 0;

          b.CharacterFlag = true;

          b.Cmb = npc.Cmb ?? b.BaseAtk + GetAbilityMod(b.Str);

          b.Cmd = npc.Cmd ?? 10 + b.BaseAtk + GetAbilityMod(b.Str) + GetAbilityMod(b.Dex);

          b.CompanionFlag = npc.CompanionFlag ?? false;

          if (npc.Cr.HasValue)
            b.Cr = npc.Cr.Value;
          else
          {
            int cr = -10;
            int.TryParse(Regex.Match(npc.Hd, "[0-9]+").ToString(), out cr);
            b.Cr = cr;
          }

          b.DontUseRacialHd = false;

          b.Fortitude = npc.Fort ?? GetAbilityMod(b.Con);

          b.Hp = npc.Hp ?? 0;

          b.Init = npc.Init ?? GetAbilityMod(b.Dex);

          b.IsTemplate = npc.IsTemplate ?? false;

          b.Mr = npc.Mr ?? 0;

          b.Mt = npc.Mt ?? 0;

          b.Reflex = npc.Ref ?? GetAbilityMod(b.Dex);

          int.TryParse(npc.Sr, out int sr);
          b.Sr = sr;

          b.UniqueMonster = npc.UniqueMonster ?? false;

          b.Will = npc.Will ?? GetAbilityMod(b.Wis);

          b.Xp = npc.Xp ?? 0;

          context.SaveChanges(); // Make sure we have a BestiaryId for our new NPC

          // Spawn
          context.MonsterSpawn.Add(new MonsterSpawn() { BestiaryId = b.BestiaryId });

          // Feat
          // Improved Initiative, Iron Will, Lightning Reflexes, Skill Focus (Perception)
          if (!string.IsNullOrWhiteSpace(b.Feats))
          {
            foreach (Match m in Regex.Matches(b.Feats, REGEX_SPLIT))
            {
              string feat = m.Groups[1].Value;
              string notes = null;
              if (feat.Contains("("))
              {
                notes = feat.Split('(')[1].TrimEnd(')');
                feat = feat.Split('(')[0].Trim();
              }
              if (feat.EndsWith('B'))
                feat = feat.Substring(0, feat.Length - 1);
              var f = new BestiaryFeat
              {
                BestiaryId = b.BestiaryId,
                FeatId = context.Feat.FirstOrDefault(x => feat.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.FeatId ?? 0,
                Notes = notes
              };

              context.BestiaryFeat.Add(f);
            }
          }

          // Skill
          if (!string.IsNullOrWhiteSpace(b.Skills))
          {
            foreach (Match m in Regex.Matches(b.Skills, REGEX_SPLIT))
            {
              string skill = m.Groups[1].Value;
              string notes = null;
              int bonus = 0;

              if (skill.Contains("("))
              {
                var idxA = skill.IndexOf('(') + 1;
                var idxB = skill.LastIndexOf(')');
                notes = skill.Substring(idxA, idxB - idxA);
                var newName = skill.Substring(0, --idxA) + skill.Substring(++idxB, skill.Length - idxB); // -- and ++ to make sure parentheses are dropped
                skill = newName.Trim();
              }

              if (skill.Contains("+"))
              {
                var skillsplit = skill.Split('+');
                bonus = Convert.ToInt32(Regex.Match(skillsplit[1], "[0-9]+").Value);

                skill = skill.Split('+')[0].Trim();
              }
              else if (skill.Contains("-"))
              {
                var skillsplit = skill.Split('-');
                bonus = Convert.ToInt32(Regex.Match(skillsplit[1], "[0-9]+").Value) * -1;
                skill = skillsplit[0].Trim();
              }

              if (!context.Skill.Select(x => x.Name.ToLower()).Contains(skill.ToLower()))
              {
                context.Skill.Add(new Skill()
                {
                  Name = skill,
                  Description = "UPDATE ME",
                  TrainedOnly = false
                });
                context.SaveChanges();
              }

              var s = new BestiarySkill
              {
                BestiaryId = b.BestiaryId,
                SkillId = context.Skill.FirstOrDefault(x => skill.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.SkillId ?? 0,
                Notes = notes
              };

              context.BestiarySkill.Add(s);
              context.SaveChanges();
            }
          }

          context.SaveChanges();

          if (i++ >= 5)
          {
            i = 0;
            Console.WriteLine("Finished NPCID " + npc.Npcid.ToString());
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"[{z.Npcid}] - {ex.Message}");
          if (ex.InnerException != null)
            Console.WriteLine("InnerException: " + ex.InnerException.Message);
        }
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
