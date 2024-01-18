using CLI.DAO;
using CLI.Observer;
using GUI.DTO;
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

namespace GUI.View.Predmet
{
    /// <summary>
    /// Interaction logic for AddProfessorToSubject.xaml
    /// </summary>
    public partial class AddProfessorToSubject : Window, IObserver
    {
        public List<ProfesorDTO> Profesors { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }

        private ProfessorDAO profesorDAO;
        private PredmetDTO subject;
        private SubjectDAO predmetDAO;

        public void Update()
        {
            Profesors.Clear();
            foreach (CLI.Model.Profesor profesor in profesorDAO.GetAllProfessors())
                Profesors.Add(new ProfesorDTO(profesor));
        }
        public AddProfessorToSubject(SubjectDAO predmetDAO, PredmetDTO subjectDTO)
        {
            InitializeComponent();
            DataContext = this;
            Profesors = new List<ProfesorDTO>();
            profesorDAO = new ProfessorDAO();
            this.predmetDAO = predmetDAO;
            subject = subjectDTO;

            Update();
        }


        public void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfesor == null)
            {
                MessageBox.Show(this, "Izaberi profesora.");
            }
            else
            {
                CLI.Model.Predmet pr = subject.toPredmet();
                pr.PredmetId = subject.PredmetId;
                pr.ProfesorID = SelectedProfesor.ProfesorId;

                //SelectedProfesor.PredmetiListaId.Add(pr.IdPredmet);

                predmetDAO.UpdatePredmet(pr);
                this.Close();
            }
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
