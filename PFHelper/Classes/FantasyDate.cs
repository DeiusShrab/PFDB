using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFHelper.Classes
{
  public class FantasyDate
  {
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public virtual int Season
    {
      get { return (Month - 1) % MonthsInSeason; }
    }
    public virtual string ShortDate
    {
      get { return Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString(); }
    }

    private int MonthsInYear;
    private int DaysInMonth;
    private int MonthsInSeason;

    public FantasyDate()
    {
      MonthsInYear = 12;
      DaysInMonth = 28;
      MonthsInSeason = 4;
    }

    public FantasyDate(int monthsInYear, int daysInMonth, int monthsInSeason)
    {
      MonthsInYear = monthsInYear;
      DaysInMonth = daysInMonth;
      MonthsInSeason = monthsInSeason;
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

    public ulong ToNumDate()
    {
      // yy...yymmdd

      ulong ret = (ulong)Day;
      ret += 100 * (ulong)Month;
      ret += 10000 * (ulong)Year;

      return ret;
    }

    public FantasyDate FromNumDate(ulong fromDate)
    {
      Day = Convert.ToInt32(fromDate % 100);
      Month = Convert.ToInt32((fromDate / 100) % 100);
      Year = Convert.ToInt32(fromDate / 10000);

      return this;
    }
  }
}
