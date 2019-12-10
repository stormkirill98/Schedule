using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class WeekSchedule
    {
        public List<DaySchedule> DaySchedules { get; }

        public WeekSchedule()
        {
            DaySchedules = new List<DaySchedule>(7);
        }

        public void AddDaySchedule(DaySchedule daySchedule)
        {
            if (DaySchedules.Count == 7) return;

            DaySchedules.Add(daySchedule);
        }
    }
}
