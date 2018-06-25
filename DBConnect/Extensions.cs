using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DBConnect
{
  public static class Extensions
  {
    public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> observable, IEnumerable<T> enumerable)
    {
      if (enumerable != null)
      {
        foreach (T item in enumerable)
        {
          observable.Add(item);
        }
      }

      return observable;
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
  }
}
