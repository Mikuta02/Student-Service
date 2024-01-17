using CLI.DAO;
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

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for AddKatedra.xaml
    /// </summary>
    public partial class AddKatedra : Window, INotifyPropertyChanged
    {
        private DepartmentDAO departmentsDAO { get; set; }
        public KatedraDTO Katedra { get; set; }
        public AddKatedra(DepartmentDAO departmentsDAO)
        {
            InitializeComponent();
            DataContext = this;

            Katedra = new KatedraDTO();
            this.departmentsDAO = departmentsDAO;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                departmentsDAO.AddDepartment(Katedra.toKatedra());
                MessageBox.Show("Department added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            return !string.IsNullOrWhiteSpace(txtSifraKatedre.Text) &&
                   !string.IsNullOrWhiteSpace(txtNazivKatedre.Text);
        }
    }
}
