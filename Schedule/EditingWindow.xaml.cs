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
        private List<StudyGroup> StudyGroupList;

        public EditingWindow()
        {
            StudyGroupList = Utils.readStudyGroups();

            InitializeComponent();

            StudyGroupComboBox.ItemsSource = StudyGroupList;
        }

        private void StudyGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isParity = parity.IsChecked == null ? false : (bool) parity.IsChecked;
            getWeekSchedule((StudyGroup)StudyGroupComboBox.SelectedItem, isParity);
        }

        private void Parity_Checked(object sender, RoutedEventArgs e)
        {
            getWeekSchedule((StudyGroup)StudyGroupComboBox.SelectedItem, true);

        }

        private void Parity_Unchecked(object sender, RoutedEventArgs e)
        {
            getWeekSchedule((StudyGroup)StudyGroupComboBox.SelectedItem, false);
        }

        private void getWeekSchedule(StudyGroup studyGroup, bool parity)
        {
            Console.WriteLine(studyGroup.Name + " " + parity);
        }

    }
}
