using CLI.DAO;
using CLI.Observer;
using GUI.DTO;
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
using System.Windows.Shapes;

namespace GUI.View.Predmet.CompareSubjectsWindows
{
    /// <summary>
    /// Interaction logic for AllStudents.xaml
    /// </summary>
    public partial class AllStudents : Window, IObserver
    {
        private ObservableCollection<StudentDTO> Studenti { get; set; }

        public PredmetDTO firstSubject { get; set; }
        public PredmetDTO secondSubject { get; set; }
        private SubjectDAO subjectDAO { get; set; }
        private ExamGradesDAO examGradesDAO { get; set; }
        private StudentDAO studentDAO { get; set; }
/*        public AllStudents(ObservableCollection<StudentDTO> Studenti)
        {
            InitializeComponent();
            DataContext = this;
            this.Studenti = Studenti;
        }*/

        public AllStudents(PredmetDTO firstSubject, PredmetDTO secondSubject, SubjectDAO subjectDAO)
        {
            InitializeComponent();
            DataContext = this;
            Studenti = new ObservableCollection<StudentDTO>();
            this.firstSubject = firstSubject;
            this.secondSubject = secondSubject;
            this.subjectDAO = subjectDAO;

            examGradesDAO = new ExamGradesDAO();
            studentDAO = new StudentDAO();
            Update();
        }

        public void Update()
        {
            studentDAO.fillObjectsAndLists();
            subjectDAO.fillObjectsAndLists();
            examGradesDAO.fillObjectsAndLists();

            foreach (int i in firstSubject.spisakIDStudenataNepolozili)
            {
               // MessageBox.Show($"{i}");
               // MessageBox.Show($"EVO JEDAN COUNT PRVI{firstSubject.spisakIDStudenataNepolozili.Count()}");
            }

            foreach (int i in secondSubject.spisakIDStudenataNepolozili)
            {
                //MessageBox.Show($"{i}");
               // MessageBox.Show($"EVO JEDAN COUNT DRUGI{secondSubject.spisakIDStudenataNepolozili.Count()}");
            }

                if (firstSubject.spisakIDStudenataNepolozili != null && secondSubject.spisakIDStudenataNepolozili != null)
            {
                //MessageBox.Show("udje");
                //MessageBox.Show($"EVO JEDAN COUNT {firstSubject.spisakIDStudenataNepolozili.Count()}");
                Studenti.Clear();
                foreach (int i in firstSubject.spisakIDStudenataNepolozili)
                {
                    if (secondSubject.spisakIDStudenataNepolozili.Contains(i))
                    {
                        //MessageBox.Show("udje li ovdje");
                        
                        StudentDTO dto = new StudentDTO(studentDAO.GetStudentById(i));
                        if (dto!=null)
                        {
                            MessageBox.Show($"Ime: {dto.Ime}, Prezime: {dto.Prezime}, Indeks: {dto.BrojIndeksa}");
                            Studenti.Add(dto);
                            if (Studenti.Any())
                            {
                               // MessageBox.Show("ako udje i ovdje ja onda stvarno vise ne znam");
                            }
                        }
                       
                    }
                    //NepolozeniPredmeti.Add(new PredmetDTO(subjectDAO.GetPredmetById(i)));
                }
            }
            else
            {
                MessageBox.Show("ne udje");
            }
        }

        private void StudentiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
