using System.Collections.Generic;

namespace DBConnect.ConnectModels
{
  public class ListItemResult
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }

    public override bool Equals(object obj)
    {
      var result = obj as ListItemResult;
      return result != null &&
             Id == result.Id &&
             Name == result.Name &&
             Notes == result.Notes;
    }

    public override int GetHashCode()
    {
      var hashCode = 371347040;
      hashCode = hashCode * -1521134295 + Id.GetHashCode();
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Notes);
      return hashCode;
    }

    public static bool operator== (ListItemResult a, ListItemResult b)
    {
      if (a == null && b == null)
        return true;

      if (a != null && b != null)
      {
        return a.Id == b.Id && a.Name.Equals(b.Name) && a.Notes.Equals(b.Notes);
      }
      else
      {
        return false;
      }
    }

    public static bool operator !=(ListItemResult a, ListItemResult b)
    {
      if (a == null && b == null)
        return false;

      if (a != null && b != null)
      {
        return a.Id != b.Id || !a.Name.Equals(b.Name) || !a.Notes.Equals(b.Notes);
      }
      else
      {
        return true;
      }
    }
  }
}
