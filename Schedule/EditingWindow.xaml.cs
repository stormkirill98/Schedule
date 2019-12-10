using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Database;

namespace Schedule
{
    public partial class EditingWindow : Window
    {
        private ObservableCollection<StudyGroup> StudyGroupList { get; set; }

        public EditingWindow()
        {
            StudyGroupList = new ObservableCollection<StudyGroup>(Utils.readStudyGroups());

            InitializeComponent();

            studyGroupComboBox.ItemsSource = StudyGroupList;
        }
    }
}
