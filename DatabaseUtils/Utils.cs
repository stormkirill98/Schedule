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
        private static OleDbConnection connection = new OleDbConnection(connectionString);

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
            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("SELECT * FROM {0}", tableName), connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, tableName);

            return dataSet.Tables[tableName].Rows;
        }

        public static WeekSchedule readSchedule(StudyGroup studyGroup, Parity parity)
        {
            WeekSchedule weekSchedule = new WeekSchedule(studyGroup, parity);

            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("SELECT * FROM Schedule WHERE StudyGroupID = {0} AND ParityID = {1}", studyGroup.Id, parity.Id), connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Schedule");

            DataRowCollection rows = dataSet.Tables["Schedule"].Rows;

            foreach (DataRow row in rows)
            {
                DayOfWeek dayOfWeek = GetDayOfWeekById((int)row["DayOfWeekID"]);
                Lesson lesson = GetLessonById((int)row["LessonID"]);

                Discipline discipline = GetDisciplineById((int)row["DisciplineID"]);
                DisciplineType disciplineType = GetDisciplineTypeById((int)row["DisciplineTypeID"]);
                Cabinet cabinet = GetCabinetById((int)row["CabinetID"]);
                Teacher teacher = GetTeacherById((int)row["TeacherId"]);

                LessonInfo lessonInfo = new LessonInfo((int)row["ScheduleID"], discipline, disciplineType, cabinet, teacher, lesson, studyGroup);

                weekSchedule.DaySchedules[dayOfWeek.Id - 1].Lessons[lesson.Number - 1] = lessonInfo;
            }

            return weekSchedule;
        }

        private static StudyGroup GetStudyGroupById(int id)
        {
            DataRow row = GetById("StudyGroup", id);

            return new StudyGroup(row);
        }

        private static Discipline GetDisciplineById(int id)
        {
            DataRow row = GetById("Discipline", id);

            return new Discipline(row);
        }

        private static DisciplineType GetDisciplineTypeById(int id)
        {
            DataRow row = GetById("DisciplineType", id);

            return new DisciplineType(row);
        }

        private static Cabinet GetCabinetById(int id)
        {
            DataRow row = GetById("Cabinet", id);

            return new Cabinet(row);
        }

        private static Teacher GetTeacherById(int id)
        {
            DataRow row = GetById("Teacher", id);

            return new Teacher(row);
        }

        private static DayOfWeek GetDayOfWeekById(int id)
        {
            DataRow row = GetById("DayOfWeek", id);

            return new DayOfWeek(row);
        }

        private static Parity GetParityById(int id)
        {
            DataRow row = GetById("Parity", id);

            return new Parity(row);
        }

        private static Lesson GetLessonById(int id)
        {
            DataRow row = GetById("Lesson", id);

            return new Lesson(row);
        }

        private static DataRow GetById(string tableName, int id)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("SELECT * FROM {0} WHERE Key = {1}", tableName, id), connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, tableName);

            DataRowCollection rows = dataSet.Tables[tableName].Rows;

            return rows.Count == 0 ? null : rows[0];
        }

        public static bool SaveTeacher(Teacher teacher)
        {
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Teacher ([FirstName], [Patronymic], [LastName], [Cathedra]) VALUES (@FirstName, @Patronymic, @LastName, @Cathedra)", connection);

            cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
            cmd.Parameters.AddWithValue("@Patronymic", teacher.Patronymic);
            cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
            cmd.Parameters.AddWithValue("@Cathedra", teacher.Cathedra);

            connection.Open();
            int countRows = cmd.ExecuteNonQuery();
            connection.Close();

            return countRows > 0;
        }

        public static bool SaveDiscipline(Discipline discipline)
        {
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Discipline ([Discipline]) VALUES (@Discipline)", connection);
            cmd.Parameters.AddWithValue("@Discipline", discipline.Name);

            connection.Open();
            int countRows = cmd.ExecuteNonQuery();
            connection.Close();

            return countRows > 0;
        }

        public static bool SaveStudyGroup(StudyGroup studyGroup)
        {
            OleDbCommand cmd = new OleDbCommand("INSERT INTO StudyGroup ([Group]) VALUES (@Group)", connection);
            cmd.Parameters.AddWithValue("@Group", studyGroup.Name);

            connection.Open();
            int countRows = cmd.ExecuteNonQuery();
            connection.Close();

            return countRows > 0;
        }

        public static void SaveWeekSchedule(WeekSchedule weekSchedule)
        {
            for (int dayNumber = 0; dayNumber < weekSchedule.DaySchedules.Count; dayNumber++)
            {
                DaySchedule daySchedule = weekSchedule.DaySchedules[dayNumber];

                for (int lessonNumber = 0; lessonNumber < daySchedule.Lessons.Count; lessonNumber++)
                {
                    LessonInfo lessonInfo = daySchedule.Lessons[lessonNumber];
                    if (lessonInfo == null) continue;

                    if (lessonInfo.Id == 0)
                    {
                        InsertScheduleRow(dayNumber + 1, weekSchedule.Parity.Id, lessonNumber + 1,
                                            weekSchedule.StudyGroup.Id, lessonInfo.Discipline.Id, lessonInfo.DisciplineType.Id,
                                            lessonInfo.Cabinet.Id, lessonInfo.Teacher.Id);
                    } else
                    {
                        if (lessonInfo.Remove)
                        {
                            DeleteScheduleRow(lessonInfo.Id);
                        }
                        else
                        {
                            UpdateScheduleRow(lessonInfo.Id, lessonInfo.Discipline.Id, lessonInfo.DisciplineType.Id,
                                                lessonInfo.Cabinet.Id, lessonInfo.Teacher.Id);
                        }
                    }
                }
            }
        }

        private static void InsertScheduleRow(int dayOfWeekId, int parityId,
                                            int lessonId, int studyGroupId, int disciplineId,
                                            int disciplineTypeId, int cabinetId, int teacherId)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Schedule", connection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Schedule");

            DataRow newRow = dataSet.Tables["Schedule"].NewRow();

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

        private static void UpdateScheduleRow(int scheduleId, int disciplineId,
                                            int disciplineTypeId, int cabinetId, int teacherId)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM Schedule WHERE ScheduleID = {scheduleId}", connection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Schedule");

            DataRowCollection rows = dataSet.Tables["Schedule"].Rows;
            if (rows.Count == 0) return;

            DataRow row = rows[0];

            row["DisciplineID"] = disciplineId;
            row["DisciplineTypeID"] = disciplineTypeId;
            row["CabinetID"] = cabinetId;
            row["TeacherId"] = teacherId;

            builder.GetUpdateCommand();
            adapter.Update(dataSet, "Schedule");
        }

        private static void DeleteScheduleRow(int scheduleId)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Schedule", connection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Schedule");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = dataSet.Tables["Schedule"].Columns["ScheduleID"];
            dataSet.Tables["Schedule"].PrimaryKey = keys;

            DataRow findRow = dataSet.Tables["Schedule"].Rows.Find(scheduleId);

            if (findRow != null)
            {
                findRow.Delete();
            }

            builder.GetUpdateCommand();
            adapter.Update(dataSet, "Schedule");
        }
    }
}
