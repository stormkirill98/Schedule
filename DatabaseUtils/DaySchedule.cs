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

            for (int i = 0; i < 5; i++)
            {
                Lessons.Add(new LessonInfo());
            }
        }

        public void SetLesson(int num, LessonInfo lessonInfo) {
            if (num >= 5) return; 

            Lessons[num] = lessonInfo;
        }
    }
}
