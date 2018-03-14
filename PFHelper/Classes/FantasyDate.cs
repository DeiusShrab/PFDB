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
            get { return (Month - 1) % 4; }
        }

        private int MonthsInYear;
        private int DaysInMonth;

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
    }
}
