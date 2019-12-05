using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtils
{
    public class Class1
    {
        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Projects\Visual Studio\Schedule\ScheduleBD.accdb";
        private static OleDbConnection thisConnection = new OleDbConnection(connectionString);

        public static void readScheduleTable()
        {
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter("SELECT * FROM Schedule", thisConnection);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "Schedule");

            DataRowCollection rows = thisDataSet.Tables["Schedule"].Rows;
        }

    }
}
