using GUI.DTO;
using CLI.DAO;
using CLI.Model;
using GUI.View.MenuBar;
using GUI.View.Student;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CLI.Observer;
using GUI.View.DialogWindows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<StudentDTO> Students { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentDAO studentsDAO { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeStatusBar();
            DataContext = this;
            Students = new ObservableCollection<StudentDTO>();
            studentsDAO = new StudentDAO();
            studentsDAO.StudentSubject.Subscribe(this);
            Update();
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
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) && e.Key == Key.P)
            {
                MenuItem_Predmeti_Click();
            }
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) && e.Key == Key.S)
            {
                MenuItem_Studenti_Click();
            }
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) && e.Key == Key.R)
            {
                MenuItem_Profesori_Click();
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
            aboutWindow.Owner = this;
            aboutWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutWindow.ShowDialog();
        }

        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            OpenAboutWindow();
        }

        private void MenuItem_Predmeti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Predmeti"));
        }
        private void MenuItem_Predmeti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Predmeti_Click();
        }

        private void MenuItem_Profesori_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Profesori"));
        }
        private void MenuItem_Profesori_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Profesori_Click();
        }

        private void MenuItem_Studenti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Studenti"));
        }
        private void MenuItem_Studenti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Studenti_Click();
        }

        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityButton_Click();
        }

        private void CreateEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                var addStudentWindow = new AddStudent(studentsDAO);
                addStudentWindow.Owner = this;
                addStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addStudentWindow.ShowDialog();
                //Update();
            }
            else if (MainTabControl.SelectedItem == ProffesorsTab)
            {
                // Create professor logic
                // Add your logic to create professors here
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                // Create subject logic
                // Add your logic to create subjects here
            }
        }

        private void DeleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                // Delete student logic
                if (SelectedStudent == null)
                {
                    MessageBox.Show(this, "Please choose a student to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Student");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        studentsDAO.RemoveStudent(SelectedStudent.StudentId);
                    }
                }
            }
            else if (MainTabControl.SelectedItem == ProffesorsTab)
            {
                // Delete professor logic
                // Add your logic to delete professors here
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                // Delete subject logic
                // Add your logic to delete subjects here
            }
        }

        private void EditEntityButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedStudent == null)
            {
                MessageBox.Show(this,"Please choose a student to edit!");
            }
            else
            {
               // System.Console.WriteLine(SelectedStudent.StudentId);
                var editStudentWindow = new EditStudent(studentsDAO, SelectedStudent);
                editStudentWindow.Owner = this;
                editStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                editStudentWindow.ShowDialog();
            }
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

        public void Update()
        {
            Students.Clear();
            foreach (Student student in studentsDAO.GetAllStudents()) Students.Add(new StudentDTO(student));
        }
    }
}
