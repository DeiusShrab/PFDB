using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect
{
  public class DBCache<T>
  {
    private Dictionary<CacheHeader, T> _dict;
    private int _cacheSize;

    public DBCache(int cacheSize = 32)
    {
      _dict = new Dictionary<CacheHeader, T>();
      _cacheSize = cacheSize;
    }

    public T this[int key]
    {
      get
      {
        var header = _dict.Keys.FirstOrDefault(x => x.Id == key);
        if (header != null)
        {
          header.DateAccessed = DateTime.Now;
          header.TimesAccessed++;
          return _dict[header];
        }

        throw new IndexOutOfRangeException(string.Format("The key {0} was not found in the collection", key));
      }
      set
      {
        var header = _dict.Keys.FirstOrDefault(x => x.Id == key);
        if (header != null)
        {
          header.DateAccessed = DateTime.Now;
          header.TimesAccessed++;
          _dict[header] = value;
        }
        else
        {
          _dict.Add(new CacheHeader(key), value);
          CheckCacheSize();
        }
      }
    }

    public void Add(int key, T value)
    {
      _dict.Add(new CacheHeader(key), value);
      CheckCacheSize();
    }

    public bool Remove(int key)
    {
      var header = _dict.Keys.FirstOrDefault(x => x.Id == key);
      if (header != null)
        return _dict.Remove(header);

      return false;
    }

    public bool ContainsKey(int key)
    {
      var header = _dict.Keys.FirstOrDefault(x => x.Id == key);
      if (header != null)
        return true;

      return false;
    }

    private void CheckCacheSize()
    {
      if (_dict.Keys.Count > _cacheSize)
      {
        var list = _dict.Keys.OrderByDescending(x => x.DateAccessed).ToList();
        var newD = new Dictionary<CacheHeader, T>();
        for(int i = 0; i < _cacheSize / 2; i++)
        {
          newD.Add(list[i], _dict[list[i]]);
        }
        _dict = newD;
      }
    }

    private class CacheHeader
    {
      public int Id { get; set; }
      public DateTime DateAccessed { get; set; }
      public int TimesAccessed { get; set; }

      public CacheHeader(int id)
      {
        Id = id;
        DateAccessed = DateTime.Now;
      }
    }
  }
}
