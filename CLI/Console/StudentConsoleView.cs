using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class StudentConsoleView //implementirati dodaj studenta na predmet, i ukloni
    {

        private readonly StudentDAO _studentsDao;
        private readonly StudentSubjectDAO _studsub;

        public StudentConsoleView(StudentDAO studentsDao, StudentSubjectDAO studsub)
        {
            _studentsDao = studentsDao;
            _studsub = studsub;
        }
        private void PrintStudents(List<Student> students)
        {
            System.Console.WriteLine("Studenti: ");
            string header = $"ID {"",2} | Ime {"",12} | Prezime {"",12} | Datum Rodjenja {"",11} | Adresa {"",2} | Kontakt {"",10} | Email {"",12} | Broj Indeksa {"",11} | Trenutna Godina {"",2} | Status Studenta {"",2} | Prosecna Ocena {"",5} |";
            System.Console.WriteLine(header);
            foreach (Student student in students)
            {
                System.Console.WriteLine(student);
            }
        }

        private Student InputStudent()
        {
            System.Console.WriteLine("Uneti ime studenta: ");
            string Ime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti prezime studenta: ");
            string Prezime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti datum rodjena (dd.MM.yyyy) studenta: ");
            string DatumRodjena = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti id adrese studenta: ");
            int Adresa = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti kontakt studenta: ");
            string Kontakt = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti email studenta: ");
            string Email = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti indeks studenta:\nUneti oznaku smera: ");
            string OznakaSmera = System.Console.ReadLine() ?? string.Empty;
            System.Console.WriteLine("Uneti broj upisa: ");
            int BrojUpisa = ConsoleViewUtils.SafeInputInt(); 
            System.Console.WriteLine("Uneti broj godine: ");
            int BrojGodine = ConsoleViewUtils.SafeInputInt();
            Indeks indeks = new Indeks(OznakaSmera, BrojUpisa, BrojGodine);

            System.Console.WriteLine("Uneti godinu studija studenta (broj): ");
            int Godina = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unijeti nacin finansiranja (S za samofinansiranje - B za budzet)");
            Enum.TryParse(System.Console.ReadLine(), out EnumUt.StatusType StatusStudenta);

            System.Console.WriteLine("Uneti prosecnu ocenu studenta: ");
            float Prosjecna = ConsoleViewUtils.SafeInputFloat();

            return new Student(Ime,Prezime,DatumRodjena, Adresa, Kontakt, Email, indeks, Godina, StatusStudenta, Prosjecna);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID studenta: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private int InputSubjectId()
        {
            System.Console.WriteLine("Uneti ID predmeta: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        public void RunStudentMenu()
        {
            while(true)
            {
                ShowStudentMenu();
                string userInput = System.Console.ReadLine() ?? "0";
                if (userInput == "0") break;
                HandleMenuInput(userInput);
            }

        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllStudents();
                    break;
                case "2":
                    AddStudent();
                    break;
                case "3":
                    UpdateStudent();
                    break;
                case "4":
                    RemoveStudent();
                    break;
                case "5":
                    AddSubjectToStudent();
                    break;
                case "6":
                    RemoveSubjectFromStudent();
                    break;
                case "9":
                    return;
            }
        }

        private void ShowAllStudents()
        {
            PrintStudents(_studentsDao.GetAllStudents());
        }

        private void RemoveStudent()
        {
            int id = InputId();
            Student? removedStuden = _studentsDao.RemoveStudent(id);
            if (removedStuden is null) 
            {
                System.Console.WriteLine("Student nije pronadjen");
                return;
            }
            System.Console.WriteLine("Student  izbrisan");

        }

        private void RemoveSubjectFromStudent()
        {
            int studID = InputId();
            int subjID = InputSubjectId();

            if (!_studsub.RemoveSubjectFromStudent(subjID, studID))
            {
                System.Console.WriteLine("Student ne slusa dati predmet");
                return;
            }
            System.Console.WriteLine("Predmet uklonjen");
        }

        private void UpdateStudent()
        {
            int id = InputId();
            Student student = InputStudent();
            student.StudentId = id;
            Student? updateStudent = _studentsDao.UpdateStudent(student);
            if (updateStudent == null)
            {
                System.Console.WriteLine("Student nije pronadjen");
                return;
            }

            System.Console.WriteLine("Student azuriran");
        }

        private void AddStudent()
        {
            Student student = InputStudent();
            if(_studentsDao.AddStudent(student) is null) return;
            System.Console.WriteLine("Student dodat");
        }

        private void AddSubjectToStudent()
        {
            int studID = InputId();
            int subjID = InputSubjectId();

            if (!_studsub.AddSubjectToStudent(subjID, studID)) {
                System.Console.WriteLine("Student ili Predmet ne postoje ili je Predmet vec dodan");
                return;
            }
            System.Console.WriteLine($"Student sa ID  {studID} slusa predmet sa id {subjID}");
        }

        private void ShowStudentMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati studente");
            System.Console.WriteLine("2: Dodati studenta");
            System.Console.WriteLine("3: Azurirati studenta");
            System.Console.WriteLine("4: Izbrisati studenta");
            System.Console.WriteLine("5: Dodati predmet studentu");
            System.Console.WriteLine("6: Skloniti studenta sa premeta");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
