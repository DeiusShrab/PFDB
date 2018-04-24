using System;

namespace PFHelper.Classes
{
  public class FantasyDate
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
      FromNumDate(parseDate);
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
      if (d >= 0)
      {
        Day += d;
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
        Day += d;
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

    public string ToNumDate()
    {
      // yy...yymmdd

      var ret = $"{Year.ToString()}-{Month.ToString("D2")}-{Day.ToString("D2")}";

      return ret;
    }

    public FantasyDate FromNumDate(string fromDate)
    {
      Day = Convert.ToInt32(fromDate.Substring(fromDate.Length - 2, 2));
      Month = Convert.ToInt32(fromDate.Substring(fromDate.Length - 4, 2));
      Year = Convert.ToInt32(fromDate.Substring(0, fromDate.Length - 4));

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
  }
}
