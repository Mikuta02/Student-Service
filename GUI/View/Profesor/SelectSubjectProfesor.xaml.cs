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
using System.Xml.Linq;

namespace GUI.View.Profesor
{
    /// <summary>
    /// Interaction logic for SelectSubjectProfesor.xaml
    /// </summary>
    public partial class SelectSubjectProfesor : Window, IObserver
    {
        public ObservableCollection<PredmetDTO> Subjects { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        public ProfesorDTO Profesor { get; set; }
        private ProfessorDAO profesorDAO { get; set; }

        private SubjectDAO predmetDAO { get; set; }



        public SelectSubjectProfesor(ProfessorDAO profesorDAO, SubjectDAO predmetDao, ProfesorDTO profesor)
        {
            InitializeComponent();
            DataContext = this;

            this.Profesor = profesor;
            this.profesorDAO = profesorDAO;

            Subjects = new ObservableCollection<PredmetDTO>();
            predmetDAO = predmetDao;

            Update();

        }



        public void Update()
        {
            Subjects.Clear();

            foreach (CLI.Model.Predmet pr in predmetDAO.GetAllPredmets())
            {
                if (!Profesor.PredmetiListaId.Contains(pr.PredmetId))
                {
                    Subjects.Add(new PredmetDTO(pr));
                }
            }
        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet == null)
            {
                MessageBox.Show(this, "Izaberi predmet.");
            }

            CLI.Model.Profesor prof = Profesor.toProfesor();
            prof.ProfesorId = Profesor.ProfesorId;
            prof.AdresaId = Profesor.AdresaId;
            prof.Adresa.AdresaId = Profesor.AdresaId;

            CLI.Model.Predmet pred = SelectedPredmet.toPredmet();
            pred.PredmetId = SelectedPredmet.PredmetId;
            pred.ProfesorID = Profesor.ProfesorId;

            Profesor.PredmetiListaId.Add(SelectedPredmet.PredmetId);

            predmetDAO.UpdatePredmet(pred);
            profesorDAO.UpdateProfessor(prof);
            Update();

            MessageBox.Show("Predmet je uspesno dodat.", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
