using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using GUI.View.DialogWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View.Predmet
{
    /// <summary>
    /// Interaction logic for EditPredmet.xaml
    /// </summary>
    public partial class EditPredmet : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public PredmetDTO Predmet { get; set; }
        private SubjectDAO predmetsDAO { get; set; }
        public ProfesorDTO Profesor { get; set; }

        public EditPredmet(SubjectDAO predmetsDAO, PredmetDTO selectPredmet)
        {
            InitializeComponent();
            DataContext = this;
            this.predmetsDAO = predmetsDAO;
            Predmet = selectPredmet;
        }

        void Update()
        {
            Predmet = new PredmetDTO(predmetsDAO.GetPredmetById(Predmet.PredmetId));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                // System.Console.WriteLine(Predmet.PredmetId);
                CLI.Model.Predmet predmetForEdit = Predmet.toPredmet();
                predmetForEdit.PredmetId = Predmet.PredmetId;
                predmetsDAO.UpdatePredmet(predmetForEdit);
                MessageBox.Show("Subject updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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


        private void AddProfessor_Click(object sender, RoutedEventArgs e)
        {
            if (Predmet.ProfesorID == -1)
            {
                var selectProfesorWindow = new AddProfessorToSubject(predmetsDAO, Predmet);
                selectProfesorWindow.Owner = this;
                selectProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                selectProfesorWindow.ShowDialog();
                Update();
            }
        }

        private void RemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            if (Predmet.ProfesorID != -1)
            {

                var confirmationDialog = new ConfirmationWindow("Professor");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    Predmet.ImeProfesoraKojiPredaje = "N/A";
                    CLI.Model.Predmet pred = Predmet.toPredmet();
                    pred.PredmetId = Predmet.PredmetId;
                    pred.ProfesorID = -1;

                    predmetsDAO.UpdatePredmet(pred);
                }
            }
            else
            {
                MessageBox.Show("Predmet ne predaje nijedan profesor", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Update();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtSifraPredmeta.Text) &&
                   !string.IsNullOrWhiteSpace(txtNaziv.Text) &&
                   cmbSemestar.SelectedItem != null &&
                   cmbGodinaStudija.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtESPB.Text);
        }

    }
}
