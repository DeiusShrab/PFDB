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
  }
}
