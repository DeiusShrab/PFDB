using System;

namespace DBConnect.ConnectModels
{
  public class FantasyDate : IComparable<FantasyDate>
  {
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }

    public virtual string ShortDate
    {
      get { return this.ToNumDate(); }
    }

    private int MonthsInYear;
    private int DaysInMonth;

    public FantasyDate()
    {
      MonthsInYear = 13;
      DaysInMonth = 28;
    }

    public FantasyDate(string parseDate, int monthsInYear = 13, int daysInMonth = 28)
    {
      UpdateFromNumDate(parseDate);
      MonthsInYear = monthsInYear;
      DaysInMonth = daysInMonth;
    }

    public FantasyDate(int monthsInYear, int daysInMonth)
    {
      MonthsInYear = monthsInYear;
      DaysInMonth = daysInMonth;
    }

    public FantasyDate AddDays(int d)
    {
      Day += d;

      if (d >= 0)
      {
        while (Day > DaysInMonth)
        {
          Day -= DaysInMonth;
          Month++;
        }
        while (Month > MonthsInYear)
        {
          Month -= MonthsInYear;
          Year++;
        }
      }
      else
      {
        while (Day <= 0)
        {
          Day += DaysInMonth;
          Month--;
        }
        while (Month <= 0)
        {
          Month += MonthsInYear;
          Year--;
        }

        if (Year < 0)
          Year = 0;
      }

      return this;
    }

    public FantasyDate AddMonths(int m)
    {
      Month += m;

      if (m >= 0)
      {
        while (Month > MonthsInYear)
        {
          Month -= MonthsInYear;
          Year++;
        }
      }
      else
      {
        while (Month <= 0)
        {
          Month += MonthsInYear;
          Year--;
        }

        if (Year < 0)
          Year = 0;
      }

      return this;
    }

    public FantasyDate AddYears(int y)
    {
      Year += y;

      if (Year < 0)
        Year = 0;

      return this;
    }

    public string ToNumDate()
    {
      // yy...yy-mm-dd

      var ret = $"{Year.ToString()}-{Month.ToString("D2")}-{Day.ToString("D2")}";

      return ret;
    }

    public FantasyDate UpdateFromNumDate(string fromDate)
    {
      if (fromDate.Contains("-"))
      {
        var split = fromDate.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length == 3)
        {
          Day = Convert.ToInt32(split[2]);
          Month = Convert.ToInt32(split[1]);
          Year = Convert.ToInt32(split[0]);
        }
      }
      else if (fromDate.Contains("/"))
      {
        var split = fromDate.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length == 3)
        {
          Day = Convert.ToInt32(split[2]);
          Month = Convert.ToInt32(split[1]);
          Year = Convert.ToInt32(split[0]);
        }
      }
      else if (fromDate.Contains("\\"))
      {
        var split = fromDate.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length == 3)
        {
          Day = Convert.ToInt32(split[2]);
          Month = Convert.ToInt32(split[1]);
          Year = Convert.ToInt32(split[0]);
        }
      }
      else if (fromDate.Contains("."))
      {
        var split = fromDate.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length == 3)
        {
          Day = Convert.ToInt32(split[2]);
          Month = Convert.ToInt32(split[1]);
          Year = Convert.ToInt32(split[0]);
        }
      }
      else if (fromDate.Length > 4)
      {
        Day = Convert.ToInt32(fromDate.Substring(fromDate.Length - 2, 2));
        Month = Convert.ToInt32(fromDate.Substring(fromDate.Length - 4, 2));
        Year = Convert.ToInt32(fromDate.Substring(0, fromDate.Length - 4));
      }

      return this;
    }

    public int DaysSince(FantasyDate otherDate)
    {
      if (otherDate.DaysInMonth != DaysInMonth || otherDate.MonthsInYear != MonthsInYear)
        throw new ArgumentException("Incompatible date formats! Check MonthsInYear and DaysInMonth");

      var y = Year - otherDate.Year;
      var m = (y * MonthsInYear) + Month - otherDate.Month;
      var d = (m * DaysInMonth) + Day - otherDate.Day;

      return d;
    }

    public int CompareTo(FantasyDate other)
    {
      if (other == null)
        return 1;

      if (this > other)
        return 1;
      else if (this < other)
        return -1;

      return 0;
    }

    public override bool Equals(object obj)
    {
      var date = obj as FantasyDate;
      return date != null &&
             Year == date.Year &&
             Month == date.Month &&
             Day == date.Day &&
             MonthsInYear == date.MonthsInYear &&
             DaysInMonth == date.DaysInMonth;
    }

    public override int GetHashCode()
    {
      var hashCode = -1683302929;
      hashCode = hashCode * -1521134295 + Year.GetHashCode();
      hashCode = hashCode * -1521134295 + Month.GetHashCode();
      hashCode = hashCode * -1521134295 + Day.GetHashCode();
      hashCode = hashCode * -1521134295 + MonthsInYear.GetHashCode();
      hashCode = hashCode * -1521134295 + DaysInMonth.GetHashCode();
      return hashCode;
    }

    public static bool operator <(FantasyDate d1, FantasyDate d2)
    {
      if (d1 == null || d2 == null)
        return false;

      if (d1.DaysInMonth != d2.DaysInMonth || d1.MonthsInYear != d2.MonthsInYear)
        throw new ArgumentException("Incompatible date formats! Check MonthsInYear and DaysInMonth");

      if (d1.Year < d2.Year)
        return true;

      if (d1.Year == d2.Year && d1.Month < d2.Month)
        return true;

      if (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day < d2.Day)
        return true;

      return false;
    }

    public static bool operator >(FantasyDate d1, FantasyDate d2)
    {
      if (d1 == null || d2 == null)
        return false;

      if (d1.DaysInMonth != d2.DaysInMonth || d1.MonthsInYear != d2.MonthsInYear)
        throw new ArgumentException("Incompatible date formats! Check MonthsInYear and DaysInMonth");

      if (d1.Year > d2.Year)
        return true;

      if (d1.Year == d2.Year && d1.Month > d2.Month)
        return true;

      if (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day > d2.Day)
        return true;

      return false;
    }

    public static bool operator ==(FantasyDate d1, FantasyDate d2)
    {
      if (object.ReferenceEquals(null, d2))
        return object.ReferenceEquals(null, d1);
      
      if (d1.DaysInMonth != d2.DaysInMonth || d1.MonthsInYear != d2.MonthsInYear)
        throw new ArgumentException("Incompatible date formats! Check MonthsInYear and DaysInMonth");

      if (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day)
        return true;

      return false;
    }

    public static bool operator !=(FantasyDate d1, FantasyDate d2)
    {
      return !(d1 == d2);
    }

    public static bool operator <=(FantasyDate d1, FantasyDate d2)
    {
      return d1 == d2 || d1 < d2;
    }

    public static bool operator >=(FantasyDate d1, FantasyDate d2)
    {
      return d1 == d2 || d1 > d2;
    }
  }
}
