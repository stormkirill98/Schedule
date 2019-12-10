using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
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
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(String.Format("SELECT * FROM {0}", tableName), thisConnection);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, tableName);

            return thisDataSet.Tables[tableName].Rows;
        }
    }
}
