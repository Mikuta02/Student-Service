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

namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for AddSubjectToStudent.xaml
    /// </summary>
    public partial class AddSubjectToStudent : Window, IObserver
    {
        public PredmetDTO SelectedSubject { get; set; }
        public ObservableCollection<PredmetDTO> Predmeti { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private SubjectDAO subjectDAO { get; set; }
        private StudentSubjectDAO studentSubjectDAO { get; set; }

        public AddSubjectToStudent(StudentDTO student, SubjectDAO subjectDAO, StudentSubjectDAO studentSubjectDAO)
        {
            InitializeComponent();
            DataContext = this;

            this.SelectedStudent = student;
            this.subjectDAO = subjectDAO;
            this.studentSubjectDAO = studentSubjectDAO;

            SelectedSubject = new PredmetDTO();
            Predmeti = new ObservableCollection<PredmetDTO>();

            Update();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            studentSubjectDAO.AddSubjectToStudent(SelectedSubject.PredmetId, SelectedStudent.StudentId);
            MessageBox.Show("Predmet je uspesno dodat!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedStudent.spisakIDNepolozenihPredmeta.Add(SelectedSubject.PredmetId);
            Update();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public void Update()
        {
            Predmeti.Clear();
            foreach (CLI.Model.Predmet predmet in subjectDAO.GetAllPredmets())
            {
                if (SelectedStudent.TrenutnaGodinaStudija >= predmet.GodinaStudija &&
                    !SelectedStudent.spisakIDPolozenihPredmeta.Contains(predmet.PredmetId) &&
                    !SelectedStudent.spisakIDNepolozenihPredmeta.Contains(predmet.PredmetId))
                        Predmeti.Add(new PredmetDTO(predmet));
            }
        }
    }
}
