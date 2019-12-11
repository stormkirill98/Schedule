using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Utils
    {
        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Projects\Visual Studio\Schedule\ScheduleBD.accdb";
        private static OleDbConnection thisConnection = new OleDbConnection(connectionString);

        public static List<StudyGroup> readStudyGroups()
        {
            List<StudyGroup> studyGroupList = new List<StudyGroup>();

            DataRowCollection rows = readTable("StudyGroup");

            foreach (DataRow row in rows)
            {
                studyGroupList.Add(new StudyGroup(row));
            }

            return studyGroupList;
        }

        public static List<Discipline> readDisciplines()
        {
            List<Discipline> disciplineList = new List<Discipline>();

            DataRowCollection rows = readTable("Discipline");

            foreach (DataRow row in rows)
            {
                disciplineList.Add(new Discipline(row));
            }

            return disciplineList;
        }

        public static List<DisciplineType> readDisciplineTypes()
        {
            List<DisciplineType> disciplineTypeList = new List<DisciplineType>();

            DataRowCollection rows = readTable("DisciplineType");

            foreach (DataRow row in rows)
            {
                disciplineTypeList.Add(new DisciplineType(row));
            }

            return disciplineTypeList;
        }

        public static List<Cabinet> readCabinets()
        {
            List<Cabinet> cabinetList = new List<Cabinet>();

            DataRowCollection rows = readTable("Cabinet");

            foreach (DataRow row in rows)
            {
                cabinetList.Add(new Cabinet(row));
            }

            return cabinetList;
        }

        public static List<Teacher> readTeachers()
        {
            List<Teacher> teacherList = new List<Teacher>();

            DataRowCollection rows = readTable("Teacher");

            foreach (DataRow row in rows)
            {
                teacherList.Add(new Teacher(row));
            }

            return teacherList;
        }

        private static DataRowCollection readTable(string tableName)
        {
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM {0}", tableName), thisConnection);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, tableName);

            return thisDataSet.Tables[tableName].Rows;
        }

        public static void SaveWeekSchedule(WeekSchedule weekSchedule)
        {
            for (int dayNumber = 0; dayNumber < weekSchedule.DaySchedules.Count; dayNumber++)
            {
                DaySchedule daySchedule = weekSchedule.DaySchedules[dayNumber];

                for (int lessonNumber = 0; lessonNumber < daySchedule.Lessons.Count; lessonNumber++)
                {
                    LessonInfo lesson = daySchedule.Lessons[lessonNumber];
                    if (lesson == null) continue;

                    InsertScheduleRow(dayNumber + 1, weekSchedule.Parity.Id, lessonNumber + 1,
                        weekSchedule.StudyGroup.Id, lesson.Discipline.Id, lesson.DisciplineType.Id,
                        lesson.Cabinet.Id, lesson.Teacher.Id);
                }
            }
        }

        private static void InsertScheduleRow(int dayOfWeekId, int parityId, 
            int lessonId, int studyGroupId, int disciplineId, 
            int disciplineTypeId, int cabinetId, int teacherId)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Schedule", thisConnection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Schedule");

            DataRow newRow = dataSet.Tables["Schedule"].NewRow();

            newRow["ScheduleID"] = 3;
            newRow["DayOfWeekID"] = dayOfWeekId;
            newRow["ParityID"] = parityId;
            newRow["LessonID"] = lessonId;
            newRow["StudyGroupID"] = studyGroupId;
            newRow["DisciplineID"] = disciplineId;
            newRow["DisciplineTypeID"] = disciplineTypeId;
            newRow["CabinetID"] = cabinetId;
            newRow["TeacherId"] = teacherId;

            dataSet.Tables["Schedule"].Rows.Add(newRow);

            builder.GetUpdateCommand();
            adapter.Update(dataSet, "Schedule");
        }
    }
}
