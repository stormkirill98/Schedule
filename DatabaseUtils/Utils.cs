using System;
using System.Collections.Generic;
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

        private static DataRowCollection readTable(string tableName)
        {
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(String.Format("SELECT * FROM {0}", tableName), thisConnection);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, tableName);

            return thisDataSet.Tables[tableName].Rows;
        }
    }
}
