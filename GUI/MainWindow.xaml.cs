using GUI.View.MenuBar;
using GUI.View.Student;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand MyCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            InitializeStatusBar();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H)
            {
                OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
               // OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                //OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
            {
                //OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.E)
            {
                //OpenAboutWindow();
            }
            if (e.Key == Key.Delete)
            {
               // OpenAboutWindow();
            }
        }

        private void AddNewEntity(object sender, RoutedEventArgs e)
        {

        }

        private void SaveApp(object sender, RoutedEventArgs e)
        {

        }

        private void CloseApp_Execution(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenAboutWindow()
        {
            var aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            OpenAboutWindow();
        }

        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityButton_Click();
        }

        private void CreateEntityButton_Click()
        {
            var addStudentWindow = new AddStudent();
            addStudentWindow.Owner = this;
            addStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addStudentWindow.ShowDialog();
        }

        private void DeleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete_Entity button clicked!");
            // Add logic to delete the selected entity
        }

        private void EditEntityButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit_Entity button clicked!");
            // Add logic to open the dialog for editing an entity
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Search button clicked!");
            // Add logic to perform the search based on the criteria
        }

        private void InitializeStatusBar()
        {
            // Update date and time every second
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, e) =>
            {
                UpdateDate();
                UpdateTime();
            };
            timer.Start();
        }

        private void UpdateDate()
        {
            statusDate.Text = $"Date: {DateTime.Now.ToString("yyyy-MM-dd")}";
        }

        private void UpdateTime()
        {
            statusTime.Text = $"Time: {DateTime.Now.ToString("HH:mm:ss")}";
        }

    }
}
