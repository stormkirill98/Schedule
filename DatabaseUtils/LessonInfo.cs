using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class LessonInfo
    {
        public int Id { get; }
        public Lesson Lesson { get; set; }
        public Discipline Discipline { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public Cabinet Cabinet { get; set; }
        public Teacher Teacher { get; set; }
        public StudyGroup StudyGroup { get; set; }

        public LessonInfo(Discipline discipline, DisciplineType disciplineType,
            Cabinet cabinet, Teacher teacher, 
            Lesson lesson = new Lesson(),
            StudyGroup studyGroup = new StudyGroup())
        {
            Id = -1;

            Lesson = lesson;
            Discipline = discipline;
            DisciplineType = disciplineType;
            Cabinet = cabinet;
            Teacher = teacher;
            StudyGroup = studyGroup;
        }

        public LessonInfo(int id, Lesson lesson, Discipline discipline, DisciplineType disciplineType, 
            Cabinet cabinet, Teacher teacher, StudyGroup studyGroup)
            : this (discipline, disciplineType, cabinet, teacher, lesson, studyGroup)
        {
            Id = id;
        }
    }
}
