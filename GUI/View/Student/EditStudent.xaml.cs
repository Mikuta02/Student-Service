using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View.DialogWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class EditStudent : Window, INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<ExamGradeDTO> Exams { get; set; }
        private ExamGradesDAO examGradesDAO { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public StudentDTO Student { get; set; }
        private StudentDAO studentsDAO { get; set; }
        private StudentSubjectDAO studentSubjectDAO {  get; set; }
        private List<CLI.Model.Predmet> SpisakNepolozenihPredmeta { get; set; }

        private List<CLI.Model.Predmet> AvailableSubjects { get; set; }

        public EditStudent(StudentDAO studentsDAO, StudentDTO selectedStudent, StudentSubjectDAO sbDAO)
        {
            InitializeComponent();
            DataContext = this;

            Exams = new ObservableCollection<ExamGradeDTO>();
            examGradesDAO = new ExamGradesDAO();
            examGradesDAO.ExamGradeSubject.Subscribe(this);

            Students = new ObservableCollection<StudentDTO>();
            this.studentsDAO = studentsDAO;
            studentSubjectDAO = sbDAO;
            Student = selectedStudent;
            studentsDAO.StudentSubject.Subscribe(this);

            SpisakNepolozenihPredmeta = studentsDAO.LoadSpisakNepolozenihPredmeta(selectedStudent.StudentId);
            NepolozeniDataGrid.ItemsSource = SpisakNepolozenihPredmeta;
            Update();
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
            // Check if an item is selected in the NepolozeniDataGrid
            if (NepolozeniDataGrid.SelectedItem != null)
            {
                CLI.Model.Predmet selectedSubject = (CLI.Model.Predmet)NepolozeniDataGrid.SelectedItem;

                // Show confirmation dialog
                var confirmationDialog = new ConfirmationWindow("Subject");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    // Remove the selected subject from the data source and update the UI
                    SpisakNepolozenihPredmeta.Remove(selectedSubject);
                    studentSubjectDAO.RemoveSubjectFromStudent(selectedSubject.PredmetId, Student.StudentId);
                    NepolozeniDataGrid.Items.Refresh(); // Refresh the DataGrid
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to remove.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var subjectSelectionDialog = new SelectSubjectDialog(GetAvailableSubjects());
            subjectSelectionDialog.Owner = this;
            subjectSelectionDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (subjectSelectionDialog.ShowDialog() == true)
            {
                // Add the selected subject to the data source
                CLI.Model.Predmet selectedSubject = subjectSelectionDialog.SelectedSubject;
                SpisakNepolozenihPredmeta.Add(selectedSubject);
                studentSubjectDAO.AddSubjectToStudent(selectedSubject.PredmetId, Student.StudentId);
                //SpisakNepolozenihPredmeta = studentsDAO.LoadSpisakNepolozenihPredmeta(Student.StudentId);
                NepolozeniDataGrid.Items.Refresh(); // Refresh the DataGrid
            }
        }

        private List<CLI.Model.Predmet> GetAvailableSubjects()
        {
            return studentSubjectDAO.GetAvailableSubjects(SpisakNepolozenihPredmeta, Student.StudentId, Student.TrenutnaGodinaStudija);
        }

        public void Update()
        {
            Students.Clear();
            foreach (CLI.Model.Student student in studentsDAO.GetAllStudents()) Students.Add(new StudentDTO(student));

            Exams.Clear();
            foreach (OcenaNaIspitu exam in examGradesDAO.GetAllGrades()) Exams.Add(new ExamGradeDTO(exam));
        }

        private void NepolozeniDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PolozeniDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPolaganje_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPonistiOcenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
