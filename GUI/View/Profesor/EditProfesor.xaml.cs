﻿using System;
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

namespace GUI.View.Profesor
{
    /// <summary>
    /// Interaction logic for EditProfesor.xaml
    /// </summary>
    public partial class EditProfesor : Window, INotifyPropertyChanged
    {
        
        public ProfesorDTO Profesor { get; set; }
        private ProfessorDAO profesorsDAO;
        public event PropertyChangedEventHandler? PropertyChanged;

        public EditProfesor (ProfessorDAO profesorsDAO,ProfesorDTO selectedProfesor) 
        {
            InitializeComponent ();
            DataContext = this;
            this.profesorsDAO = profesorsDAO;
            Profesor = selectedProfesor;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                System.Console.WriteLine(Profesor.ProfesorId);
                profesorsDAO.UpdateProfessor(Profesor.toProfesor());
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


    }
}
