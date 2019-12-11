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
        private ObservableCollection<StudyGroup> StudyGroupList;

        public EditingWindow()
        {
            InitializeComponent();

            // TODO when is better
            Init();

            StudyGroupComboBox.ItemsSource = StudyGroupList;
            StudyGroupComboBox.DisplayMemberPath = "Name";
        }

        private void Init()
        {
            StudyGroupList = new ObservableCollection<StudyGroup>(Utils.readStudyGroups());

            List<Discipline> disciplines = Utils.readDisciplines();
            List<DisciplineType> disciplineTypes = Utils.readDisciplineTypes();
            List<Cabinet> cabinets = Utils.readCabinets();
            List<Teacher> teachers = Utils.readTeachers();

            MondaySchedule.Init(disciplines, disciplineTypes, cabinets, teachers);
            TuesdaySchedule.Init(disciplines, disciplineTypes, cabinets, teachers);
            WednesdaySchedule.Init(disciplines, disciplineTypes, cabinets, teachers);
            ThursdaySchedule.Init(disciplines, disciplineTypes, cabinets, teachers);
            FridaySchedule.Init(disciplines, disciplineTypes, cabinets, teachers);
            SutardaySchedule.Init(disciplines, disciplineTypes, cabinets, teachers);
        }

        private void StudyGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillWeekSchedule();
        }

        private void Parity_Click(object sender, RoutedEventArgs e)
        {
            FillWeekSchedule();
        }

        private void FillWeekSchedule()
        {
            if (StudyGroupComboBox.SelectedItem == null)
                return;

            StudyGroup studyGroup = (StudyGroup)StudyGroupComboBox.SelectedItem;
            bool isParity = ParityInput.IsChecked == null ? false : (bool)ParityInput.IsChecked;
            Console.WriteLine(studyGroup.Name + " " + isParity);
        }

        private WeekSchedule GetWeekSchedule()
        {
            if (StudyGroupComboBox.SelectedItem == null)
                return null;

            StudyGroup studyGroup = (StudyGroup)StudyGroupComboBox.SelectedItem;

            bool isParity = ParityInput.IsChecked == null ? false : (bool)ParityInput.IsChecked;
            Parity parity = isParity ? new Parity(1, "Числитель") : new Parity(2, "Знаменатель");

            WeekSchedule weekSchedule = new WeekSchedule(studyGroup, parity);

            weekSchedule.AddDaySchedule(MondaySchedule.GetDaySchedule());
            weekSchedule.AddDaySchedule(TuesdaySchedule.GetDaySchedule());
            weekSchedule.AddDaySchedule(WednesdaySchedule.GetDaySchedule());
            weekSchedule.AddDaySchedule(ThursdaySchedule.GetDaySchedule());
            weekSchedule.AddDaySchedule(FridaySchedule.GetDaySchedule());
            weekSchedule.AddDaySchedule(SutardaySchedule.GetDaySchedule());

            return weekSchedule;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            WeekSchedule weekSchedule = GetWeekSchedule();
            if (weekSchedule == null)
                return;

            Utils.SaveWeekSchedule(weekSchedule);
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddStudyGroup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
