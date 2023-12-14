using CLI.DAO;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO Student { get; set; }

        //private StudentDAO studentsDAO;

        public event PropertyChangedEventHandler? PropertyChanged;

        public AddStudent()
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
          //  studentsDAO = new StudentDAO();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Perform validation
            if (ValidateFields())
            {
                // Add logic to save student data
                // For example, you can create a Student class and store the data there
                // Student newStudent = new Student(txtIme.Text, txtPrezime.Text, ...);
                // SaveStudent(newStudent);
                //studentsDAO.AddStudent(Student.toStudent());
                MessageBox.Show("Student added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all fields before confirming.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Add logic to handle cancel button click
            this.Close();
        }

        private bool ValidateFields()
        {
            // Add validation logic for each field
            return !string.IsNullOrWhiteSpace(txtIme.Text) &&
                   !string.IsNullOrWhiteSpace(txtPrezime.Text) &&
                   dpDatumRodjenja.SelectedDate.HasValue &&
                   !string.IsNullOrWhiteSpace(txtAdresa.Text) &&
                   !string.IsNullOrWhiteSpace(txtKontakt.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtIndeks.Text) &&
                   cmbGodinaStudija.SelectedItem != null &&
                   cmbStatusStudenta.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtProsecnaOcena.Text);
        }
    }
}
