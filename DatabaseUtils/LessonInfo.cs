using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class LessonInfo
    {
        public Lesson Lesson { get; set; }
        public Discipline Discipline { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public Cabinet Cabinet { get; set; }
        public Teacher Teacher { get; set; }
        public StudyGroup StudyGroup { get; set; }

        public LessonInfo(Lesson lesson, Discipline discipline, DisciplineType disciplineType, 
            Cabinet cabinet, Teacher teacher, StudyGroup studyGroup)
        {
            Lesson = lesson;
            Discipline = discipline;
            DisciplineType = disciplineType;
            Cabinet = cabinet;
            Teacher = teacher;
            StudyGroup = studyGroup;
        }
    }
}
