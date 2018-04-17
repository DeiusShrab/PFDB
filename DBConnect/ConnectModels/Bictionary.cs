using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.ConnectModels
{
  public class Bictionary<T1, T2>
  {
    private Dictionary<T1, T2> dictA;
    private Dictionary<T2, T1> dictB;

    public Bictionary()
    {
      dictA = new Dictionary<T1, T2>();
      dictB = new Dictionary<T2, T1>();
    }

    public T2 this[T1 key]
    {
      get { return dictA[key]; }
      set
      {
        if (key == null)
          throw new ArgumentNullException();

        if (!dictA.ContainsKey(key) && !dictB.ContainsValue(key))
        {
          var x = dictA[key];
          dictA[key] = value;
          dictB.Remove(x);
          dictB.Add(value, key);
        }
        else
          throw new ArgumentException("Bictionary duplicate key conflict");
      }
    }

    public T1 this[T2 key]
    {
      get { return dictB[key]; }
      set
      {
        if (key == null)
          throw new ArgumentNullException();

        if (!dictB.ContainsKey(key) && !dictA.ContainsValue(key))
        {
          var x = dictB[key];
          dictB[key] = value;
          dictA.Remove(x);
          dictA.Add(value, key);
        }
        else
          throw new ArgumentException("Bictionary duplicate key conflict");
      }
    }

    public int Count => dictA.Count;

    public void Add(T1 key, T2 value)
    {
      if (key == null || value == null)
        throw new ArgumentNullException();

      if (!dictA.ContainsKey(key) && !dictB.ContainsValue(key))
      {
        var x = dictA[key];
        dictA[key] = value;
        dictB.Remove(x);
        dictB.Add(value, key);
      }
      else
        throw new ArgumentException("Bictionary duplicate key conflict");
    }

    public void Add(T2 key, T1 value)
    {
      if (key == null || value == null)
        throw new ArgumentNullException();

      if (!dictB.ContainsKey(key) && !dictA.ContainsValue(key))
      {
        var x = dictB[key];
        dictB[key] = value;
        dictA.Remove(x);
        dictA.Add(value, key);
      }
      else
        throw new ArgumentException("Bictionary duplicate key conflict");
    }

    public void Clear()
    {
      dictA.Clear();
      dictB.Clear();
    }

    public bool Contains(T1 key) => dictA.ContainsKey(key);

    public bool Contains(T2 key) => dictB.ContainsKey(key);

    public IEnumerable<T1> KeysPrimary => dictA.Keys;

    public IEnumerable<T2> KeysSecondary => dictB.Keys;

    public void Remove(T1 key)
    {
      if (dictA.ContainsKey(key))
      {
        var x = dictA[key];
        dictA.Remove(key);
        dictB.Remove(x);
      }
    }

    public void Remove(T2 key)
    {
      if (dictB.ContainsKey(key))
      {
        var x = dictB[key];
        dictB.Remove(key);
        dictA.Remove(x);
      }
    }
  }
}
