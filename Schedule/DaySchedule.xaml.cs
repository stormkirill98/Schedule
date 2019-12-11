using Database;
using System;
using System.Collections.Generic;
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
    public partial class DaySchedule : UserControl
    {
        public static readonly DependencyProperty DayProperty = DependencyProperty.Register("Day", typeof(string), typeof(DaySchedule));
        public string Day
        {
            get { return (string)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        public DaySchedule()
        {
            InitializeComponent();

            DataContext = this;
        }

        public void Init(List<Discipline> disciplines, List<DisciplineType> disciplineTypes,
                 List<Cabinet> cabinets, List<Teacher> teachers)
        {
            FirstLesson.Init(disciplines, disciplineTypes, cabinets, teachers);
            SecondLesson.Init(disciplines, disciplineTypes, cabinets, teachers);
            ThirdLesson.Init(disciplines, disciplineTypes, cabinets, teachers);
            FourthLesson.Init(disciplines, disciplineTypes, cabinets, teachers);
            FifthLesson.Init(disciplines, disciplineTypes, cabinets, teachers);
        }

        public Database.DaySchedule GetDaySchedule()
        {
            Database.DaySchedule daySchedule = new Database.DaySchedule();

            daySchedule.AddLesson(FirstLesson.GetLessonInfo());
            daySchedule.AddLesson(SecondLesson.GetLessonInfo());
            daySchedule.AddLesson(ThirdLesson.GetLessonInfo());
            daySchedule.AddLesson(FourthLesson.GetLessonInfo());
            daySchedule.AddLesson(FifthLesson.GetLessonInfo());

            return daySchedule;
        }
    }
}
