using PFDBCommon.ConnectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PFDBCommon
{
  public static class Extensions
  {
    public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
    {
      if (enumerable != null)
      {
        foreach (T item in enumerable)
        {
          collection.Add(item);
        }
      }

      return collection;
    }

    public static string EnumToCommaString(this IEnumerable<object> enumerable)
    {
      if (enumerable == null)
        return null;

      var sb = new StringBuilder();
      foreach (var item in enumerable)
      {
        sb.Append(item.ToString());
        sb.Append(", ");
      }

      if (sb.Length > 0)
        sb.Remove(sb.Length - 2, 2);

      return sb.ToString();
    }

    public static string EnumToNewlineString(this IEnumerable<object> enumerable)
    {
      if (enumerable == null)
        return null;

      var sb = new StringBuilder();
      foreach (var item in enumerable)
      {
        sb.Append(item.ToString());
        sb.Append("\n");
      }

      if (sb.Length > 0)
        sb.Remove(sb.Length - 1, 1);

      return sb.ToString();
    }

    public static FantasyDate AddDays(this FantasyDate date, int d)
    {
      var outDate = new FantasyDate(date);
      outDate.IncrementDays(d);
      return outDate;
    }

    public static FantasyDate AddMonths(this FantasyDate date, int m)
    {
      var outDate = new FantasyDate(date);
      outDate.IncrementMonths(m);
      return outDate;
    }

    public static FantasyDate AddYears(this FantasyDate date, int y)
    {
      var outDate = new FantasyDate(date);
      outDate.IncrementYears(y);
      return outDate;
    }

    public static FantasyDate AddDate(this FantasyDate date, int years, int months, int days)
    {
      var outDate = new FantasyDate(date);
      outDate.IncrementYears(years);
      outDate.IncrementMonths(months);
      outDate.IncrementDays(days);
      return outDate;
    }

    public static string CRToString(int cr)
    {
      switch (cr)
      {
        case (int)CRSpecial.Half:
          return "1/2";
        case (int)CRSpecial.Third:
          return "1/3";
        case (int)CRSpecial.Fourth:
          return "1/4";
        case (int)CRSpecial.Sixth:
          return "1/6";
        case (int)CRSpecial.Eighth:
          return "1/8";
        default:
          return cr.ToString();
      }
    }
  }
}
