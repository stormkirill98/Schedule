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
    public partial class Lesson : UserControl
    {
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(Lesson));
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public Lesson()
        {
            InitializeComponent();

            DataContext = this;
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
