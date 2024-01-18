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
        private ExamGradesDAO examGradesDAO { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public StudentDTO Student { get; set; }
        private StudentDAO studentsDAO { get; set; }
        private StudentSubjectDAO studentSubjectDAO { get; set; }
        private SubjectDAO subjectDAO { get; set; }
        private ProfessorDAO professorDAO { get; set; }
        public ObservableCollection<PredmetDTO> NepolozeniPredmeti { get; set; }
        public ObservableCollection<ExamGradeDTO> Ocene { get; set; }
        public ObservableCollection<ProfesorPredmetDTO> Profesori { get; set; }
        public ExamGradeDTO SelectedOcena { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        public ProfesorPredmetDTO SelectedProfesorPredmet { get; set; }
        private double _averageGrade { get; set; }

        public double AverageGrade
        {
            get { return _averageGrade; }
            set
            {
                if (_averageGrade != value)
                {
                    _averageGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _espbSum { get; set; }

        public int ESPBSum
        {
            get { return _espbSum; }
            set
            {
                if (_espbSum != value)
                {
                    _espbSum = value;
                    OnPropertyChanged();
                }
            }
        }

        public EditStudent(StudentDAO studentsDAO, StudentDTO selectedStudent, StudentSubjectDAO sbDAO)
        {
            InitializeComponent();
            DataContext = this;

            examGradesDAO = new ExamGradesDAO();

            Students = new ObservableCollection<StudentDTO>();
            this.studentsDAO = studentsDAO;
            studentSubjectDAO = sbDAO;
            subjectDAO = new SubjectDAO();
            Student = selectedStudent;

            NepolozeniPredmeti = new ObservableCollection<PredmetDTO>();
            SelectedPredmet = new PredmetDTO();
            Ocene = new ObservableCollection<ExamGradeDTO>();
            SelectedOcena = new ExamGradeDTO();
            Profesori = new ObservableCollection<ProfesorPredmetDTO>();
            professorDAO = new ProfessorDAO();

            //CalculateAverageGradeAndESPBSum();
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
            if (SelectedPredmet != null)
            {
                var confirmationDialog = new ConfirmationWindow("Subject");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    // NepolozeniPredmeti.Remove(SelectedPredmet);
                    studentSubjectDAO.RemoveSubjectFromStudent(SelectedPredmet.PredmetId, Student.StudentId);
                    Student.spisakIDNepolozenihPredmeta.Remove(SelectedPredmet.PredmetId);
                    // NepolozeniPredmeti.Remove(SelectedPredmet);  
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to remove.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Update();
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var subjectSelectionDialog = new AddSubjectToStudent(Student, subjectDAO, studentSubjectDAO);
            subjectSelectionDialog.Owner = this;
            subjectSelectionDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            subjectSelectionDialog.ShowDialog();
            Update();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public void Update()
        {
            studentsDAO.fillObjectsAndLists();
            examGradesDAO.fillObjectsAndLists();


            if (Student.spisakIDNepolozenihPredmeta != null)
            {
                NepolozeniPredmeti.Clear();
                foreach (int i in Student.spisakIDNepolozenihPredmeta)
                {
                    NepolozeniPredmeti.Add(new PredmetDTO(subjectDAO.GetPredmetById(i)));
                }
            }

            if (Student.spisakIDOcena != null)
            {
                Ocene.Clear();
                foreach (int i in Student.spisakIDOcena)
                {

                    if (examGradesDAO.GetGradeById(i) != null && examGradesDAO.GetGradeById(i).PredmetStudenta != null)
                    {
                        //MessageBox.Show("nasao je ocenuuu");
                        Ocene.Add(new ExamGradeDTO(examGradesDAO.GetGradeById(i)));
                    }
                    else
                    {
                        //MessageBox.Show("ocena nema predmet");
                    }
                }
            }

            Profesori.Clear();
            foreach (int predmetID in Student.spisakIDNepolozenihPredmeta)
            {
                CLI.Model.Predmet? predmet = subjectDAO.GetPredmetById(predmetID);
                if (predmet != null)
                {
                    CLI.Model.Profesor? profesor = professorDAO.GetProfessorById(predmet.ProfesorID);
                    if (profesor != null)
                    {
                        // MessageBox.Show($"{predmet.Naziv}");
                        Profesori.Add(new ProfesorPredmetDTO(profesor, predmet));
                    }
                }
            }

            CalculateAverageGradeAndESPBSum();
        }

        private void NepolozeniDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PolozeniDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPolaganje_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet != null)
            {
                var takeExamWindow = new PassExam(SelectedPredmet.SifraPredmeta, SelectedPredmet.Naziv);
                takeExamWindow.Owner = this;
                takeExamWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (takeExamWindow.ShowDialog() == true)
                {
                    CLI.Model.OcenaNaIspitu newExam = new CLI.Model.OcenaNaIspitu
                    {
                        PredmetId = SelectedPredmet.PredmetId,
                        StudentId = Student.StudentId,
                        Ocena = takeExamWindow.Ocena,
                        DatumPolaganja = takeExamWindow.Datum
                    };
                    examGradesDAO.AddExamGrade(newExam);
                    //examGradesDAO.fillObjectsAndLists();

                    // Ocene.Add(new ExamGradeDTO(newExam));
                    Student.spisakIDPolozenihPredmeta.Add(SelectedPredmet.PredmetId);
                    Student.spisakIDOcena.Add(newExam.OcenaNaIspituId);

                    Student.spisakIDNepolozenihPredmeta.Remove(SelectedPredmet.PredmetId);
                    //CalculateAverageGradeAndESPBSum();
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to take exam.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Update();
        }


        private void btnPonistiOcenu_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOcena != null)
            {
                var confirmationDialog = new ConfirmationWindow("Exam");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    examGradesDAO.RemoveExamGrade(SelectedOcena.OcenaNaIspituId);
                    Student.spisakIDNepolozenihPredmeta.Add(SelectedOcena.PredmetId);
                    Student.spisakIDOcena.Remove(SelectedOcena.OcenaNaIspituId);
                    Student.spisakIDPolozenihPredmeta.Remove(SelectedOcena.PredmetId);
                    //CalculateAverageGradeAndESPBSum();
                }
            }
            else
            {
                MessageBox.Show("Please select a failed exam to take.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Update();
        }

        private void CalculateAverageGradeAndESPBSum()
        {
            if (Ocene.Any())
            {
                double sumaOcena = 0;
                int sumaESPB = 0;
                int count = 0;
                foreach (int i in Student.spisakIDOcena)
                {
                    if (examGradesDAO.GetGradeById(i) != null)
                    {
                        sumaOcena += examGradesDAO.GetGradeById(i).Ocena;
                        sumaESPB += examGradesDAO.GetGradeById(i).PredmetStudenta.ESPB;
                        ++count;
                    }
                }
                AverageGrade = sumaOcena / count;
                ESPBSum = sumaESPB;
            }
            else
            {
                AverageGrade = 0;
                ESPBSum = 0;
            }
        }
    }
}
