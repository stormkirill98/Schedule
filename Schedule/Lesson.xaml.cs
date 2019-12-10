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
        public ObservableCollection<Cabinet> CabinetList = new ObservableCollection<Cabinet>() { new Cabinet() };
        public ObservableCollection<Teacher> TeacherList = new ObservableCollection<Teacher>() { new Teacher() };

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
            if (lessonInfo.Lesson.Number != Number)
                return;

            // TODO fill lesson
        }

        private void EmptyCheckbox_Click(object sender, RoutedEventArgs e)
        {
            bool isNotEmpty = EmptyCheckbox.IsChecked == null ? false : (bool)EmptyCheckbox.IsChecked;

            Visibility visibility = isNotEmpty ? Visibility.Visible : Visibility.Hidden;

            DisciplineInput.Visibility = visibility;
            DisciplineTypeInput.Visibility = visibility;
            CabinetInput.Visibility = visibility;
            TeacherInput.Visibility = visibility;
        }
    }
}
