using CLI.DAO;
using CLI.Model;
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
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public StudentDTO Student { get; set; }
        private StudentDAO studentsDAO { get; set; }
        private List<CLI.Model.Predmet> SpisakNepolozenihPredmeta { get; set; }

        public EditStudent(StudentDAO studentsDAO, StudentDTO selectedStudent)
        {
            InitializeComponent();
            DataContext = this;
            this.studentsDAO = studentsDAO;
            Student = selectedStudent;

            SpisakNepolozenihPredmeta = studentsDAO.LoadSpisakNepolozenihPredmeta(selectedStudent.StudentId);
            dgNepolozeni.ItemsSource = SpisakNepolozenihPredmeta;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Student studentForEdit = Student.toStudent();
                studentForEdit.StudentId = Student.StudentId;
                studentsDAO.UpdateStudent(studentForEdit);
                MessageBox.Show("Student updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all fields before confirming.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
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
                   !string.IsNullOrWhiteSpace(txtIndeks.Text) && IsValidIndex(txtIndeks.Text) &&
                   cmbGodinaStudija.SelectedItem != null &&
                   cmbStatusStudenta.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtProsecnaOcena.Text);
        }

        private bool IsValidIndex(string input)
        {
            try
            {
                makeIndex(input);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid index format. Please enter a valid index.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private Indeks makeIndex(string input)
        {
            string[] parts = input.Split('/');

            string oznaka = input.Substring(0, 2);
            int upis = int.Parse(parts[0].Substring(3));
            int godina = int.Parse(parts[1]);

            Indeks indeks = new Indeks(oznaka, upis, godina);

            return indeks;
        }


        private void btnUkloni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnPolaganje_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
