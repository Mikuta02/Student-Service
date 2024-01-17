using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using GUI.View.DialogWindows;
using System.Collections.ObjectModel;

namespace GUI.View.Profesor
{
    /// <summary>
    /// Interaction logic for EditProfesor.xaml
    /// </summary>
    public partial class EditProfesor : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ProfesorDTO Profesor { get; set; }
        private ProfessorDAO profesorsDAO { get; set; }
        private SubjectDAO subjectsDAO { get; set; }
        private StudentDAO studentsDAO { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        public ObservableCollection<PredmetDTO> Predmeti { get; set; }


        public EditProfesor(ProfessorDAO profesorsDAO, SubjectDAO predmetsDAO, ProfesorDTO selectedProfesor)
        {
            InitializeComponent();
            DataContext = this;
            this.profesorsDAO = profesorsDAO;
            this.subjectsDAO = predmetsDAO;
            Profesor = selectedProfesor;

            Predmeti = new ObservableCollection<PredmetDTO>();

            studentsDAO = new StudentDAO();
            SelectedPredmet = new PredmetDTO();
            Update();
        }

        void Update()
        {
            //studentsDAO.fillObjectsAndLists(studentSubjectDAO, predmetsDAO, adressesDAO, examGradesDAO);
            profesorsDAO.fillObjectsAndLists();
            subjectsDAO.fillObjectsAndLists();

            if (Profesor.spisakIDPredmeta != null)
            {
                Predmeti.Clear();
                foreach (int i in Profesor.spisakIDPredmeta)
                {
                    Predmeti.Add(new PredmetDTO(subjectsDAO.GetPredmetById(i)));
                }
            }

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Profesor profForEdit = Profesor.toProfesor();
                profForEdit.ProfesorId = Profesor.ProfesorId;
                profesorsDAO.UpdateProfessor(profForEdit);
                MessageBox.Show("Profesor updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                   !string.IsNullOrWhiteSpace(txtBrojLicneKarte.Text) &&
                   !string.IsNullOrWhiteSpace(txtZvanje.Text) &&
                   !string.IsNullOrWhiteSpace(txtGodineStaza.Text);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var selectSubjectProfesorWindow = new SelectSubjectProfesor(profesorsDAO, subjectsDAO, Profesor);
            selectSubjectProfesorWindow.Owner = this;
            selectSubjectProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectSubjectProfesorWindow.ShowDialog();

            Update();
        }

        private List<CLI.Model.Predmet> GetAvailableSubjects()
        {
            return subjectsDAO.GetAvailableSubjects(Profesor.ProfesorId);
        }

        private void btnUkloni_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet == null)
            {
                MessageBox.Show(this, "Izaberite predmet za brisanje!");
            }
            else
            {
                var confirmationDialog = new ConfirmationWindow("subject");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    CLI.Model.Predmet subject = SelectedPredmet.toPredmet();
                    subject.PredmetId = SelectedPredmet.PredmetId;
                    subject.ProfesorID = -1;


                    Profesor.spisakIDPredmeta.Remove(SelectedPredmet.PredmetId);
                    CLI.Model.Profesor prof = Profesor.toProfesor();

                    prof.ProfesorId = Profesor.ProfesorId;
                    prof.AdresaId = Profesor.AdresaId;

                    subjectsDAO.UpdatePredmet(subject);
                    profesorsDAO.UpdateProfessor(prof);
                    Update();
                }
            }
        }

        private void PredmetiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
