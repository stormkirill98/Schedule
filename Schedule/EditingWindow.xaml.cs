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
            Parity parity = GetParity();

            WeekSchedule weekSchedule = Utils.readSchedule(studyGroup, parity);

            MondaySchedule.Fill(weekSchedule.DaySchedules[0]);
            TuesdaySchedule.Fill(weekSchedule.DaySchedules[1]);
            WednesdaySchedule.Fill(weekSchedule.DaySchedules[2]);
            ThursdaySchedule.Fill(weekSchedule.DaySchedules[3]);
            FridaySchedule.Fill(weekSchedule.DaySchedules[4]);
            SutardaySchedule.Fill(weekSchedule.DaySchedules[5]);
        }

        private WeekSchedule GetWeekSchedule()
        {
            if (StudyGroupComboBox.SelectedItem == null)
                return null;

            StudyGroup studyGroup = (StudyGroup)StudyGroupComboBox.SelectedItem;
            Parity parity = GetParity();


            WeekSchedule weekSchedule = new WeekSchedule(studyGroup, parity);

            weekSchedule.SetDaySchedule(0, MondaySchedule.GetDaySchedule());
            weekSchedule.SetDaySchedule(1, TuesdaySchedule.GetDaySchedule());
            weekSchedule.SetDaySchedule(2, WednesdaySchedule.GetDaySchedule());
            weekSchedule.SetDaySchedule(3, ThursdaySchedule.GetDaySchedule());
            weekSchedule.SetDaySchedule(4, FridaySchedule.GetDaySchedule());
            weekSchedule.SetDaySchedule(5, SutardaySchedule.GetDaySchedule());

            return weekSchedule;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            WeekSchedule weekSchedule = GetWeekSchedule();
            if (weekSchedule == null)
                return;

            Utils.SaveWeekSchedule(weekSchedule);

            FillWeekSchedule();
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherWindow teacherWindow = new TeacherWindow();
            bool result = (bool)teacherWindow.ShowDialog();
            if (result) Init();
        }

        private void AddStudyGroup_Click(object sender, RoutedEventArgs e)
        {
            StudyGroupWindow studyGroupWindow = new StudyGroupWindow();
            bool result = (bool)studyGroupWindow.ShowDialog();
            if (result) StudyGroupList.AddRange(Utils.readStudyGroups());
        }

        private void AddDiscipline_Click(object sender, RoutedEventArgs e)
        {
            DisciplineWindow disciplineWindow = new DisciplineWindow();
            bool result = (bool)disciplineWindow.ShowDialog();
            if (result) Init();
        }

        private Parity GetParity()
        {
            bool isParity = ParityInput.IsChecked == null ? false : (bool)ParityInput.IsChecked;
            return isParity ? new Parity(1, "Числитель") : new Parity(2, "Знаменатель");
        }
    }
}
