using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow()
        {
            InitializeComponent();

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Projects\Visual Studio\Schedule\ScheduleBD.accdb";

            OleDbConnection thisConnection = new OleDbConnection(connectionString);

            // Create command for this connection
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(
                             "SELECT ScheduleID, DayOfWeekID FROM Schedule", thisConnection);

            // Create DataSet to contain related data tables, rows, and columns
            DataSet thisDataSet = new DataSet();

            // Fill DataSet using query defined previously for DataAdapter
            thisAdapter.Fill(thisDataSet, "Schedule");

            // Show data before change
            Console.WriteLine("name before change: {0}",
                 thisDataSet.Tables["Schedule"].Rows[0]);
        }
    }
}
