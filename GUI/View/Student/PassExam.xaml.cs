using CLI.DAO;
using CLI.Model;
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

namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for PassExam.xaml
    /// </summary>
    public partial class PassExam : Window
    {
        private SubjectDAO subjectDAO;
        public int Ocena { get; set; }
        public DateTime Datum { get; set; }   
        public PassExam(string sifraPredmeta, string nazivPredmeta)
        {
            InitializeComponent();
            subjectDAO = new SubjectDAO();
            txtSifra.Text = sifraPredmeta;
            txtNaziv.Text = nazivPredmeta;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Ocena = (int)cmbOcena.SelectedItem;
                Datum = dpDatumRodjenja.SelectedDate ?? DateTime.Now;

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please fill in all fields before confirming.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateFields()
        {
            return cmbOcena.SelectedItem != null && dpDatumRodjenja.SelectedDate.HasValue;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
