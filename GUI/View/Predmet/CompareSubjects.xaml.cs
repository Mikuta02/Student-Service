using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View.Predmet;
using GUI.View.Predmet.CompareSubjectsWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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

namespace GUI.View.Predmet
{
    /// <summary>
    /// Interaction logic for CompareSubjects.xaml
    /// </summary>
    public partial class CompareSubjects : Window, IObserver
    {

        private StudentDAO studentDAO { get; set; }
        private PredmetDTO Subject { get; set; }
        private SubjectDAO subjectDAO { get; set; }
        public PredmetDTO SelectedSubjectInCompare { get; set; }
        public ExamGradesDAO examGradesDAO { get; set; }
        public ObservableCollection<PredmetDTO> Subjects  { get; set; }
        public ObservableCollection<StudentDTO> AllStudentsList { get; set; }
        public ObservableCollection<StudentDTO> StudentsFirst { get; set; }
        public ObservableCollection<StudentDTO> StudentsSecond { get; set; }
        public CompareSubjects(SubjectDAO subjectDAO, PredmetDTO selectedSubject, StudentDAO studentDAO)
        {
            InitializeComponent();
            DataContext = this;

            Subject = selectedSubject;
            this.studentDAO = studentDAO;
            this.subjectDAO = subjectDAO;
            Subjects = new ObservableCollection<PredmetDTO>(); 
            SelectedSubjectInCompare = new PredmetDTO();
            AllStudentsList = new ObservableCollection<StudentDTO>();
            StudentsFirst = new ObservableCollection<StudentDTO>();
            StudentsSecond = new ObservableCollection<StudentDTO>();
            examGradesDAO = new ExamGradesDAO();

            Update();
        }

        public void Update()
        {
            subjectDAO.fillObjectsAndLists();
            studentDAO.fillObjectsAndLists();
            examGradesDAO.fillObjectsAndLists();

            Subjects.Clear();
            foreach (CLI.Model.Predmet predmet in subjectDAO.GetAllPredmets())
            {
                if(predmet.PredmetId != Subject.PredmetId)
                {
                    Subjects.Add(new PredmetDTO(predmet));
                }   
            }
            
        }

        private void btnAllPassed_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedSubjectInCompare == null)
            {
                MessageBox.Show(this, "Please choose a subject to compare!");
            }
            else
            {
                PredmetDTO firstSubject = Subject.Clone();
                int counter = 0;
                if (firstSubject.spisakIDStudenataNepolozili != null && SelectedSubjectInCompare.spisakIDStudenataNepolozili != null)
                {
                    foreach (int i in firstSubject.spisakIDStudenataNepolozili)
                    {
                        if (SelectedSubjectInCompare.spisakIDStudenataNepolozili.Contains(i))
                        {
                            StudentDTO dto = new StudentDTO(studentDAO.GetStudentById(i));
                            if (dto != null)
                            {
                                MessageBox.Show($"Ime: {dto.Ime}\nPrezime: {dto.Prezime}\nIndeks: {dto.BrojIndeksa}");
                                ++counter;
                                AllStudentsList.Add(dto);
                            }

                        }
                    }
                    if (counter == 0)
                    {
                        MessageBox.Show($"NEMA TAKVIH");
                    }
                }
            }
        }

        private void btnOnlySelectedPassed_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubjectInCompare == null)
            {
                MessageBox.Show(this, "Please choose a subject to compare!");
            }
            else
            {
                PredmetDTO firstSubject = Subject.Clone();
                int counter1 = 0;
                int counter2 = 0;
                if (firstSubject.spisakIDStudenataNepolozili != null && SelectedSubjectInCompare.spisakIDStudenataNepolozili != null
                   && firstSubject.spisakIDStudenataPolozili != null && SelectedSubjectInCompare.spisakIDStudenataPolozili != null)
                {
                    MessageBox.Show($"Studenti koji su polozili {firstSubject.Naziv} predmet a nisu {SelectedSubjectInCompare.Naziv}:");
                    foreach (int i in firstSubject.spisakIDStudenataPolozili)
                    {
                        if (!SelectedSubjectInCompare.spisakIDStudenataPolozili.Contains(i))
                        {
                            StudentDTO dto = new StudentDTO(studentDAO.GetStudentById(i));
                            if (dto != null)
                            {
                                MessageBox.Show($"Ime: {dto.Ime}\nPrezime: {dto.Prezime}\nIndeks: {dto.BrojIndeksa}");
                                ++counter1;
                                AllStudentsList.Add(dto);
                            }

                        }
                    }
                    if (counter1 == 0)
                    {
                        MessageBox.Show($"NEMA TAKVIH");
                    }

                    MessageBox.Show($"Studenti koji su polozili {SelectedSubjectInCompare.Naziv} a nisu {firstSubject.Naziv}:");
                    foreach (int i in SelectedSubjectInCompare.spisakIDStudenataPolozili)
                    {
                        if (!firstSubject.spisakIDStudenataPolozili.Contains(i))
                        {
                            StudentDTO dto = new StudentDTO(studentDAO.GetStudentById(i));
                            if (dto != null)
                            {
                                MessageBox.Show($"Ime: {dto.Ime}\nPrezime: {dto.Prezime}\nIndeks: {dto.BrojIndeksa}");
                                ++counter2;
                                AllStudentsList.Add(dto);
                            }

                        }
                    }
                    if (counter2 == 0)
                    {
                        MessageBox.Show($"NEMA TAKVIH");
                    }
                }
            }
        }

        private void PredmetiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
