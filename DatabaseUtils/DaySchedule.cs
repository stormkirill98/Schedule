using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DaySchedule
    {
        public List<LessonInfo> Lessons { get; }

        public DaySchedule()
        {
            Lessons = new List<LessonInfo>(5);
        }

        public void AddLesson(LessonInfo lessonInfo) {
            if (Lessons.Count == 5) return; 

            Lessons.Add(lessonInfo);
        }
    }
}
