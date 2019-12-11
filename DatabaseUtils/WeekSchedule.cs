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
        public StudyGroup StudyGroup { get; }
        public Parity Parity { get; }

        public WeekSchedule()
        {
            DaySchedules = new List<DaySchedule>(6);

            for (int i = 0; i < 6; i++)
            {
                DaySchedules.Add(new DaySchedule());
            }
        }

        public WeekSchedule(StudyGroup studyGroup, Parity parity) : this()
        {
            StudyGroup = studyGroup;
            Parity = parity;
        }

        public void AddDaySchedule(DaySchedule daySchedule)
        {
            if (DaySchedules.Count == 6) return;

            DaySchedules.Add(daySchedule);
        }
    }
}
