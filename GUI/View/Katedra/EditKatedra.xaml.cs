using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using GUI.View.Predmet;
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

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for EditKatedra.xaml
    /// </summary>
    public partial class EditKatedra : Window, INotifyPropertyChanged
    {
        public KatedraDTO Katedra { get; set; }
        private DepartmentDAO departmentsDAO { get; set; }
        private ProfessorDAO professorDAO { get; set; }
        private SubjectDAO subjectDAO { get; set; }
        public ObservableCollection<ProfesorDTO> Profesori { get; set; }
        public ObservableCollection<ProfesorPredmetKatedraDTO> Predmeti { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        public ProfesorDTO SefKatedre { get; set; } 

        public EditKatedra(DepartmentDAO departmentsDAO, KatedraDTO selectedKatedra, ProfessorDAO professorDAO)
        {
            InitializeComponent();
            DataContext = this;
            this.Katedra = selectedKatedra;
            this.departmentsDAO = departmentsDAO;
            this.professorDAO = professorDAO;

            Profesori = new ObservableCollection<ProfesorDTO>();
            Predmeti = new ObservableCollection<ProfesorPredmetKatedraDTO>();
            subjectDAO = new SubjectDAO();
            SelectedProfesor = new ProfesorDTO();
            SefKatedre = new ProfesorDTO();
            Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        void Update()
        {
            professorDAO.fillObjectsAndLists();
            departmentsDAO.fillObjectsAndLists();
            subjectDAO.fillObjectsAndLists();

            if (Katedra.spisakIDProfesora != null)
            {
                Profesori.Clear();
                foreach (int i in Katedra.spisakIDProfesora)
                {
                    Profesori.Add(new ProfesorDTO(professorDAO.GetProfessorById(i)));
                }
            }

            foreach(CLI.Model.Profesor profesor in professorDAO.GetAllProfessors())
            {
                foreach(CLI.Model.Predmet predmet in profesor.Predmeti)
                {
                    if(Katedra.spisakIDProfesora.Contains(profesor.ProfesorId))
                    {
                        Predmeti.Add(new ProfesorPredmetKatedraDTO(profesor, predmet));
                    }
                }
            }

            SefKatedre = new ProfesorDTO(professorDAO.GetProfessorById(SefKatedre.ProfesorId));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                // System.Console.WriteLine(Predmet.PredmetId);
                CLI.Model.Katedra katedraForEdit = Katedra.toKatedra();
                katedraForEdit.KatedraId = Katedra.katedraId;

                departmentsDAO.UpdateDepartment(katedraForEdit);
                MessageBox.Show("Department updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
        private void btnUkloni_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
        private void btnPostavi_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            if (SelectedProfesor == null)
            {
                MessageBox.Show(this, "Izaberi profesora.");
            }
            else
            {
                if (isProfessorEligible(SelectedProfesor) && SelectedProfesor != null)
                {
                    CLI.Model.Katedra kat = Katedra.toKatedra();
                    kat.KatedraId = Katedra.katedraId;
                    kat.SefId = SelectedProfesor.ProfesorId;

                    departmentsDAO.UpdateDepartment(kat);
                    Update();
                }
                else
                {
                    MessageBox.Show(this, "Profesor kog ste izabrali ne moze da bude sef katedre");
                }
            }

        }

        private bool isProfessorEligible(ProfesorDTO? selectedProfesor)
        {
            bool retVal = professorDAO.isProfessorEligible(selectedProfesor.ProfesorId);
            return retVal;
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtNazivKatedre.Text) &&
                   !string.IsNullOrWhiteSpace(txtSifraKatedre.Text);
        }
    }
}
