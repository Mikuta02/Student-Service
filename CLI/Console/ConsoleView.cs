using CLI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class ConsoleView
    {
        private readonly StudentsDAO _studentsDao;
        private readonly ProfessorsDAO _profesDao;
        private readonly SubjectsDAO _subjectsDao;
        private readonly ExamGradesDAO _examGradesDao;
        private readonly AdressesDAO _addressesDao;
        private readonly DepartmentsDAO _departmentsDao;

        public ConsoleView(StudentsDAO studentsDao, ProfessorsDAO profesDao, SubjectsDAO subjectsDao, ExamGradesDAO examGradesDao, AdressesDAO addressesDao, DepartmentsDAO departmentsDao)
        {
            _studentsDao = studentsDao;
            _profesDao = profesDao;
            _subjectsDao = subjectsDao;
            _examGradesDao = examGradesDao;
            _addressesDao = addressesDao;
            _departmentsDao = departmentsDao;
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
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
                    StudentConsoleView viewStudent = new StudentConsoleView(_studentsDao);
                    viewStudent.RunStudentMenu();
                    break;
                case "2":
                    ProfessorConsoleView viewProfessor = new ProfessorConsoleView(_profesDao); //itd
                    viewProfessor.RunProfessorMenu();
                    break;
                case "3":
                    SubjectConsoleView viewSubject = new SubjectConsoleView(_subjectsDao); //itd
                    viewSubject.RunSubjectMenu();
                    break;
/*                case "4":
                    RemoveStudent();
                    break;*/
            }
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati studente");
            System.Console.WriteLine("2: Prikazati profesore");
            System.Console.WriteLine("3: Prikazati predmete");
            System.Console.WriteLine("4: Prikazati ocene");
            System.Console.WriteLine("5: Prikazati indekse");
            System.Console.WriteLine("6: Prikazati katedre");
            System.Console.WriteLine("7: Prikazati adrese");
            System.Console.WriteLine("0: Close");
        }
    }
}
