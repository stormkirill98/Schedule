using Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Lesson : UserControl
    {
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(Lesson));
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public ObservableCollection<Discipline> DisciplineList = new ObservableCollection<Discipline>() { new Discipline() };
        public ObservableCollection<DisciplineType> DisciplineTypeList = new ObservableCollection<DisciplineType>() { new DisciplineType() };
        public ObservableCollection<Cabinet> CabinetList = new ObservableCollection<Cabinet>() { };
        public ObservableCollection<Teacher> TeacherList = new ObservableCollection<Teacher>() { new Teacher() };

        private LessonInfo currentLessonInfo = null;

        public Lesson()
        {
            InitializeComponent();

            DataContext = this;

            DisciplineInput.ItemsSource = DisciplineList;
            DisciplineInput.DisplayMemberPath = "Name";

            DisciplineTypeInput.ItemsSource = DisciplineTypeList;
            DisciplineTypeInput.DisplayMemberPath = "Type";

            CabinetInput.ItemsSource = CabinetList;
            CabinetInput.DisplayMemberPath = "Number";

            TeacherInput.ItemsSource = TeacherList;
            TeacherInput.DisplayMemberPath = "FullName";
        }

        public void Init(List<Discipline> disciplines, List<DisciplineType> disciplineTypes,
                         List<Cabinet> cabinets, List<Teacher> teachers)
        {
            DisciplineList.AddRange(disciplines);
            DisciplineTypeList.AddRange(disciplineTypes);
            CabinetList.AddRange(cabinets);
            TeacherList.AddRange(teachers);
        }

        public void Fill(LessonInfo lessonInfo)
        {
            currentLessonInfo = lessonInfo;

            if (lessonInfo.Lesson.Id == 0)
            {
                Clear();
                Hide();
                return;
            }

            Show();

            DisciplineInput.SelectedValue = lessonInfo.Discipline;
            DisciplineTypeInput.SelectedValue = lessonInfo.DisciplineType;
            CabinetInput.SelectedValue = lessonInfo.Cabinet;
            TeacherInput.SelectedValue = lessonInfo.Teacher;
        }

        private void Hide()
        {
            EmptyCheckbox.IsChecked = false;
            SetVisible(Visibility.Hidden);
        }

        private void Show()
        {
            EmptyCheckbox.IsChecked = true;
            SetVisible(Visibility.Visible);
        }

        public void Clear()
        {
            currentLessonInfo = null;
            DisciplineInput.SelectedValue = 0;
            DisciplineTypeInput.SelectedValue = 0;
            CabinetInput.SelectedValue = -1;
            TeacherInput.SelectedValue = 0;
        }

        public LessonInfo GetLessonInfo()
        {
            bool isNotEmpty = GetEmptyCheckboxValue();

            if (currentLessonInfo != null && !isNotEmpty && currentLessonInfo.Id != 0)
            {
                currentLessonInfo.Remove = true;
                return currentLessonInfo;
            }

            if (DisciplineInput.SelectedValue == null 
                || DisciplineTypeInput.SelectedValue == null 
                || CabinetInput.SelectedValue == null 
                || TeacherInput.SelectedValue == null)
            {
                return null;
            }

            Discipline disciplineValue = (Discipline)DisciplineInput.SelectedValue;
            DisciplineType disciplineTypeValue = (DisciplineType)DisciplineTypeInput.SelectedValue;
            Cabinet cabinetValue = (Cabinet)CabinetInput.SelectedValue;
            Teacher teacherValue = (Teacher)TeacherInput.SelectedValue;


            if (disciplineValue.Name == ""
                || disciplineTypeValue.Type == ""
                || cabinetValue.Number == 0
                || teacherValue.FullName == "  ")
            {
                return null;
            }

            LessonInfo resulLessonInfo = currentLessonInfo == null 
                ? new LessonInfo(disciplineValue, disciplineTypeValue, cabinetValue, teacherValue)
                : new LessonInfo(currentLessonInfo.Id, disciplineValue, disciplineTypeValue, cabinetValue, teacherValue);

            return resulLessonInfo;
        }

        private void EmptyCheckbox_Click(object sender, RoutedEventArgs e)
        {
            bool isEmpty = GetEmptyCheckboxValue();

            Visibility visibility = isEmpty ? Visibility.Visible : Visibility.Hidden;
            SetVisible(visibility);
        }

        private void SetVisible(Visibility visibility)
        {
            DisciplineInput.Visibility = visibility;
            DisciplineTypeInput.Visibility = visibility;
            CabinetInput.Visibility = visibility;
            TeacherInput.Visibility = visibility;
        }

        private bool GetEmptyCheckboxValue()
        {
            return EmptyCheckbox.IsChecked == null ? false : (bool)EmptyCheckbox.IsChecked;
        }
    }
}
