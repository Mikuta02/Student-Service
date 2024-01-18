using GUI.DTO;
using CLI.DAO;
using GUI.Localization;
using CLI.Model;
using GUI.View.MenuBar;
using GUI.View.Student;
using GUI.View.Profesor;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CLI.Observer;
using GUI.View.DialogWindows;
using GUI.View.Predmet;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using GUI.View.Katedra;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";

        public static readonly DependencyProperty CurrentTabProperty =
            DependencyProperty.Register(
                nameof(CurrentTab),
                typeof(string),
                typeof(MainWindow),
                new PropertyMetadata("Studenti", OnCurrentTabChanged));


        public string CurrentTab
        {
            get { return (string)GetValue(CurrentTabProperty); }
            set { SetValue(CurrentTabProperty, value); }
        }


        public ObservableCollection<StudentDTO> Students { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentDAO studentsDAO { get; set; }
        public ObservableCollection<ProfesorDTO> Profesors { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        private ProfessorDAO profesorsDAO { get; set; }
        public ObservableCollection<PredmetDTO> Predmets {  get; set; }
        public PredmetDTO SelectedPredmet {  get; set; }
        private SubjectDAO predmetsDAO { get; set; }
        public ObservableCollection<KatedraDTO> Katedre { get; set; }
        public KatedraDTO SelectedKatedra { get; set; }
        private DepartmentDAO departmentsDAO { get; set; }
        private ExamGradesDAO examGradesDAO { get; set; }
        private AdressDAO adressesDAO { get; set; }
        private StudentSubjectDAO studentSubjectDAO { get; set; }
        private ProfessorDepartmentDAO professorDepartmentDAO { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;

            app = (App)Application.Current;
            app.ChangeLanguage(SRB);
           

            InitializeStatusBar();
            DataContext = this;

            Students = new ObservableCollection<StudentDTO>();
            studentsDAO = new StudentDAO();
            studentsDAO.StudentSubject.Subscribe(this);

            Profesors = new ObservableCollection<ProfesorDTO>();
            profesorsDAO = new ProfessorDAO();
            profesorsDAO.ProfesorSubject.Subscribe(this);

            Predmets = new ObservableCollection<PredmetDTO>();
            predmetsDAO = new SubjectDAO();
            predmetsDAO.PredmetSubject.Subscribe(this);

            Katedre = new ObservableCollection<KatedraDTO>();
            departmentsDAO = new DepartmentDAO();
            departmentsDAO.DepartmentSubject.Subscribe(this);

            examGradesDAO = new ExamGradesDAO();
            adressesDAO = new AdressDAO();
            studentSubjectDAO = new StudentSubjectDAO();
            professorDepartmentDAO = new ProfessorDepartmentDAO();
            fillObjects();
            CurrentTab = "Studenti";
            UpdateTabStatus();
            Update();
        }

        private void fillObjects()
        {
            studentsDAO.fillObjectsAndLists();
            profesorsDAO.fillObjectsAndLists();
            predmetsDAO.fillObjectsAndLists();
            departmentsDAO.fillObjectsAndLists();
            examGradesDAO.fillObjectsAndLists();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H)
            {
                OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                CreateEntityButton_Click();
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
                EditEntityButton_Click();
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
                DeleteEntityButton_Click();
            }
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
                var addProfesorWindow = new AddProfesor(profesorsDAO);
                addProfesorWindow.Owner = this;
                addProfesorWindow.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                addProfesorWindow.ShowDialog();
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                var addPredmetWindow = new AddPredmet(predmetsDAO);
                addPredmetWindow.Owner = this;
                addPredmetWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addPredmetWindow.ShowDialog();
            }
            else if (MainTabControl.SelectedItem == DepartmentsTab)
            {
                var addKatedraWindow = new AddKatedra(departmentsDAO);
                addKatedraWindow.Owner = this;
                addKatedraWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addKatedraWindow.ShowDialog();
            }
        }

        private void DeleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteEntityButton_Click();
        }

        private void DeleteEntityButton_Click()
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
                if(SelectedProfesor == null)
                {
                    MessageBox.Show(this, "Please choose a profesor to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Profesor");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        profesorsDAO.RemoveProfessor(SelectedProfesor.ProfesorId);
                    }
                }
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                if (SelectedPredmet == null)
                {
                    MessageBox.Show(this, "Please choose a subject to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Subject");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if(confirmationDialog.UserConfirmed)
                    {
                        predmetsDAO.RemovePredmet(SelectedPredmet.PredmetId);
                    }
                }
            }
            else if (MainTabControl.SelectedItem == DepartmentsTab)
            {
                if (SelectedKatedra == null)
                {
                    MessageBox.Show(this, "Please choose a department to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Department");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        departmentsDAO.RemoveDepartment(SelectedKatedra.katedraId);
                    }
                }
            }
        }

        private void EditEntityButton_Click(object sender, RoutedEventArgs e)
        {
            EditEntityButton_Click();
        }

        private void EditEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            { 
                if (SelectedStudent == null)
                {
                    MessageBox.Show(this, "Please choose a student to edit!");
                }
                else
                {
                    var editStudentWindow = new EditStudent(studentsDAO, SelectedStudent.Clone(), studentSubjectDAO);
                    editStudentWindow.Owner = this;
                    editStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editStudentWindow.ShowDialog();
                }
            }else if(MainTabControl.SelectedItem == ProffesorsTab) 
            {
                if(SelectedProfesor == null)
                {
                    MessageBox.Show(this, "Please choose a professor to edit!");
                }
                else
                {
                    var editsProfesorWindow = new EditProfesor(profesorsDAO, predmetsDAO, SelectedProfesor.Clone());
                    editsProfesorWindow.Owner = this;
                    editsProfesorWindow.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                    editsProfesorWindow.ShowDialog();
                }
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                if(SelectedPredmet == null)
                {
                    MessageBox.Show(this, "Please choose a subject to edit!");
                }
                else
                {
                    var editSubjectWindow = new EditPredmet(predmetsDAO, SelectedPredmet.Clone());
                    editSubjectWindow.Owner = this;
                    editSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editSubjectWindow.ShowDialog();
                }
            }
            else if (MainTabControl.SelectedItem == DepartmentsTab)
            {
                if (SelectedKatedra == null)
                {
                    MessageBox.Show(this, "Please choose a department to edit!");
                }
                else
                {
                    var editDepartmentWindpw = new EditKatedra(departmentsDAO, SelectedKatedra.clone(), profesorsDAO);
                    editDepartmentWindpw.Owner = this;
                    editDepartmentWindpw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editDepartmentWindpw.ShowDialog();
                }
            }
            Update();
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
            //studentsDAO.fillObjectsAndLists();

            Students.Clear();
            foreach (Student student in studentsDAO.GetAllStudents()) Students.Add(new StudentDTO(student));

            Predmets.Clear();
            foreach(Predmet predmet in predmetsDAO.GetAllPredmets()) Predmets.Add(new PredmetDTO(predmet));

            Profesors.Clear();
            foreach(Profesor profesor in profesorsDAO.GetAllProfessors()) Profesors.Add(new ProfesorDTO(profesor));

            Katedre.Clear();
            foreach (Katedra katedra in departmentsDAO.GetAllDepartments()) Katedre.Add(new KatedraDTO(katedra));

        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProfesorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedItem != null)
            {
                CurrentTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();
            }
        }
        private void UpdateTabStatus()
        {
            statusTab.Text = $"Tab: {CurrentTab}";
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Postavite početni tab
            // CurrentTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();
        }

        private static void OnCurrentTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MainWindow mainWindow)
            {
                mainWindow.UpdateTabStatus();
            }
        }
        private void DataGridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            DataGridColumnHeader columnHeader = e.OriginalSource as DataGridColumnHeader;
            if (columnHeader != null)
            {
                // Postavite logiku za sortiranje ovde

                // Ako sortirate po ovoj koloni, promenite vidljivost strelice
                Image sortArrow = FindChild<Image>(columnHeader, "SortArrowIme");
                if (sortArrow != null)
                {
                    sortArrow.Visibility = Visibility.Visible;
                }
            }
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;

                if (controlName == childName)
                {
                    return child as T;
                }
                else
                {
                    T result = FindChild<T>(child, childName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;



            if (MainTabControl.SelectedItem == StudentsTab)
            {
                StudentsDataGrid.ItemsSource = FilterStudent(Students, searchTerm);
            }
            else if (MainTabControl.SelectedItem == ProffesorsTab)
            {
                ProfesorsDataGrid.ItemsSource = FilterProfesor(Profesors, searchTerm);
            }
            else
            {
                SubjectsDataGrid.ItemsSource = FilterSubject(Predmets, searchTerm);
            }

        }

        private ObservableCollection<StudentDTO> FilterStudent(ObservableCollection<StudentDTO> originalCollection, string searchTerm)
        {
            // Razdvajanje unetog upita na reči i konverzija u mala slova
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();

            // Filtriranje na osnovu broja unetih reči
            switch (terms.Count)
            {
                case 1: // Samo prezime
                    return new ObservableCollection<StudentDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Prezime.ToLower().Contains(terms[0]))
                    );

                case 2: // Prezime i ime
                    return new ObservableCollection<StudentDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Prezime.ToLower().Contains(terms[0]) &&
                            studentDto.Ime.ToLower().Contains(terms[1]))
                    );

                case 3: // Indeks, ime i prezime
                    return new ObservableCollection<StudentDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.BrojIndeksa.ToLower().Contains(terms[0]) &&
                            studentDto.Ime.ToLower().Contains(terms[1]) &&
                            studentDto.Prezime.ToLower().Contains(terms[2]))
                    );

                default:
                    return originalCollection;
            }
        }
        private ObservableCollection<ProfesorDTO> FilterProfesor(ObservableCollection<ProfesorDTO> originalCollection, string searchTerm)
        {
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();


            switch (terms.Count)
            {
                case 1: // Samo prezime
                    return new ObservableCollection<ProfesorDTO>(
                        originalCollection.Where(profesorDTO =>
                            profesorDTO.Prezime.ToLower().Contains(terms[0]))
                    );

                case 2: // Prezime i ime
                    return new ObservableCollection<ProfesorDTO>(
                        originalCollection.Where(profesorDTO =>
                            profesorDTO.Prezime.ToLower().Contains(terms[0]) &&
                            profesorDTO.Ime.ToLower().Contains(terms[1]))
                    );

                default:
                    return originalCollection;
            }
        }
        private ObservableCollection<PredmetDTO> FilterSubject(ObservableCollection<PredmetDTO> originalCollection, string searchTerm)
        {
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();

            switch (terms.Count())
            {
                case 1: // Samo sifra predmeta
                    return new ObservableCollection<PredmetDTO>(
                        originalCollection.Where(s => s.SifraPredmeta.ToLower().Contains(terms[0]))
                        );

                case 2: // Sifra i naziv predmeta
                    return new ObservableCollection<PredmetDTO>(
                        originalCollection.Where(s => s.SifraPredmeta.ToLower().Contains(terms[0]) &&
                        s.Naziv.ToLower().Contains(terms[1]))
                        );

                default:
                    return originalCollection;
            }
        }

        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
        }


        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

    }
}
