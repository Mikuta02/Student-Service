using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class StudentConsoleView
    {

        private readonly StudentsDAO _studentsDao;

        public StudentConsoleView(StudentsDAO studentsDao )
        {
            _studentsDao = studentsDao;
        }
        private void PrintStudents(List<Student> students)
        {
            System.Console.WriteLine("Studenti: ");
            string header = $"Ime {"",21} | Prezime {"",21} | Datum Rodjenja {"",6} | Adresa {"",12} | Kontakt {"",12} | Email {"",12} | Broj Indeksa {"",7} | Trenutna Godina {"",8} | Status Studenta {"",6} | Prosecna Ocena {"",5} |";
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

            System.Console.WriteLine("Uneti datum rodjena studenta: ");
            string DatumRodjena = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti adresu studenta: ");
            string Adresa = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti kontakt studenta: ");
            string Kontakt = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti email studenta: ");
            string Email = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti indeks studenta: ");
            string Indeks = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti godinu studija studenta: ");
            int Godina = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti prosecnu ocenu studenta: ");
            float Prosjecna = ConsoleViewUtils.SafeInputFloat();

            return new Student(Ime,Prezime,DatumRodjena, Adresa, Kontakt, Email, Indeks, Godina, Prosjecna);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID studenta: ");
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
            _studentsDao.AddStudent(student);
            System.Console.WriteLine("Student dodat");
        }
        private void ShowStudentMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati studente");
            System.Console.WriteLine("2: Dodati studenta");
            System.Console.WriteLine("3: Azurirati studenta");
            System.Console.WriteLine("4: Izbrisati studenta");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
