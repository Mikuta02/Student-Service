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

namespace GUI.View.DialogWindows
{
    /// <summary>
    /// Interaction logic for SelectSubjectDialog.xaml
    /// </summary>
    public partial class SelectSubjectDialog : Window
    {
        // Add a property for the selected subject
        public CLI.Model.Predmet SelectedSubject { get; private set; }

        // Add a property for the list of available subjects
        public List<CLI.Model.Predmet> AvailableSubjects { get; set; }

        public SelectSubjectDialog(List<CLI.Model.Predmet> availableSubjects)
        {
            InitializeComponent();
            AvailableSubjects = availableSubjects;
            dgAvailableSubjects.ItemsSource = AvailableSubjects;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected subject from the DataGrid
            SelectedSubject = dgAvailableSubjects.SelectedItem as CLI.Model.Predmet;
            DialogResult = true; // Close the dialog with a positive result
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Close the dialog with a negative result
        }
    }
}
