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
using System.Windows.Shapes;

namespace Schedule
{
    /// <summary>
    /// Логика взаимодействия для DisciplineWindow.xaml
    /// </summary>
    public partial class DisciplineWindow : Window
    {
        public DisciplineWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!Valid()) return;

            string disciplineName = DisciplineInput.Text;

            Discipline discipline = new Discipline(disciplineName);
            bool isSuccess = Utils.SaveDiscipline(discipline);
            if (!isSuccess)
                MessageBox.Show("Discipline was not added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            DialogResult = isSuccess;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool Valid()
        {
            if (DisciplineInput.Text.Length == 0)
            {
                MessageBox.Show("Discipline Name can not be blank", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
