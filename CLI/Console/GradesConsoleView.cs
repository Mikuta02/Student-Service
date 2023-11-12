using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class GradesConsoleView
    {

        private readonly ExamGradesDAO _examgradesDAO;

        public GradesConsoleView(ExamGradesDAO examgradesDAO)
        {
            _examgradesDAO = examgradesDAO;
        }
        private void PrintExamGrades(List<OcenaNaIspitu> examgrades)
        {
            System.Console.WriteLine("Profesori: ");
            string header = $"Student id {"",12} | Predmet id {"",12} | Ocena {"",11} | Datum polaganja{"",13}";
            System.Console.WriteLine(header);
            foreach (OcenaNaIspitu examgrade in examgrades)
            {
                System.Console.WriteLine(examgrade);
            }
        }

        private OcenaNaIspitu InputExamGrade()
        {
            System.Console.WriteLine("Uneti Id studenta: ");
            int StudId = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti Id predmeta: ");
            int PredId = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti ocenu: ");
            int Ocena = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti datum polaganja: ");
            string datum = System.Console.ReadLine() ?? string.Empty;


            return new OcenaNaIspitu(StudId,PredId,Ocena,datum);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        public void RunExamGradeMenu()
        {
            while (true)
            {
                ShowExamGradeMenu();
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
                    ShowAllGrades();
                    break;
                case "2":
                    AddExamGrade();
                    break;
                case "3":
                    UpdateExamGrade();
                    break;
                case "4":
                    RemoveExamGrade();
                    break;
                case "9":
                    return;
            }
        }

        private void ShowAllGrades()
        {
            PrintExamGrades(_examgradesDAO.GetAllGrades());
        }

        private void RemoveExamGrade()
        {
            int id = InputId();
            OcenaNaIspitu? removedGrade = _examgradesDAO.RemoveExamGrade(id);
            if (removedGrade is null)
            {
                System.Console.WriteLine("Ocena nije pronadjena");
                return;
            }
            System.Console.WriteLine("Ocena izbrisana");

        }

        private void UpdateExamGrade()
        {
            int id = InputId();
            OcenaNaIspitu grade = InputExamGrade();
            grade.OcenaNaIspituId = id;
            OcenaNaIspitu? updateGrade = _examgradesDAO.UpdateExamGrade(grade);
            if (updateGrade == null)
            {
                System.Console.WriteLine("Ocena nije pronadjena");
                return;
            }

            System.Console.WriteLine("Ocena azurirana");
        }

        private void AddExamGrade()
        {
            OcenaNaIspitu grade = InputExamGrade();
            _examgradesDAO.UpdateExamGrade(grade);
            System.Console.WriteLine("Ocena dodata");
        }
        private void ShowExamGradeMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati ocene");
            System.Console.WriteLine("2: Dodati ocenu");
            System.Console.WriteLine("3: Azurirati ocenu");
            System.Console.WriteLine("4: Izbrisati ocenu");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
