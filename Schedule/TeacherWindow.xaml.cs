using Database;
using System.Windows;
using System.Windows.Controls;

namespace Schedule
{
    public partial class TeacherWindow : Window
    {
        public TeacherWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!Valid()) return;

            string firstName = FirstNameInput.Text;
            string patronymic = PatronymicInput.Text;
            string lastName = LastNameInput.Text;
            string cathedra = CathedraInput.Text;

            Teacher teacher = new Teacher(firstName, patronymic, lastName, cathedra);
            bool isSuccess = Utils.SaveTeacher(teacher);
            if (!isSuccess)
                MessageBox.Show("Teacher was not added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            DialogResult = isSuccess;
        }

        private bool Valid()
        {
            if (!ValidTextBox(FirstNameInput, "First Name")) return false;
            if (!ValidTextBox(PatronymicInput, "Patronymic")) return false;
            if (!ValidTextBox(LastNameInput, "Last Name")) return false;
            if (!ValidTextBox(CathedraInput, "Cathedra")) return false;

            return true;
        }

        private bool ValidTextBox(TextBox textBox, string name)
        {
            if (textBox.Text.Length == 0)
            {
                MessageBox.Show($"{name} can not be blank", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
