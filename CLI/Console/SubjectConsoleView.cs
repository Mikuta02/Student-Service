using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class SubjectConsoleView
    {

        private readonly SubjectDAO _subjectsDAO;
        private readonly StudentSubjectDAO _studsub;

        public SubjectConsoleView(SubjectDAO subjectsDAO, StudentSubjectDAO studsub)
        {
            _subjectsDAO = subjectsDAO;
            _studsub = studsub;
        }
        private void PrintSubjects(List<Predmet> subjects)
        {
            System.Console.WriteLine("Predmeti: ");
            string header = $"Sifra {"",5} | Naziv {"",30} | Godina {"",2} | ProfID {"",2} | ESPB {"",2} |";
            System.Console.WriteLine(header);
            foreach (Predmet subject in subjects)
            {
                System.Console.WriteLine(subject);
            }
        }

        private Predmet InputSubject()
        {
            System.Console.WriteLine("Uneti sifru predmeta: ");
            string Sifra = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti naziv predmeta: ");
            string Naziv = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti semestar (Letnji ili Zimski)");
            Enum.TryParse(System.Console.ReadLine(), out EnumUt.SemestarType Semestar);

            System.Console.WriteLine("Uneti godinu (broj): ");
            int Godina = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti profesor id: ");
            int ProfID = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti espb: ");
            int ESPB = ConsoleViewUtils.SafeInputInt();

            return new Predmet(Sifra, Naziv, Semestar, Godina, ProfID, ESPB);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID predmeta: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private int InputStudentId()
        {
            System.Console.WriteLine("Uneti ID studenta: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        public void RunSubjectMenu()
        {
            while (true)
            {
                ShowSubjectMenu();
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
                    ShowAllSubjects();
                    break;
                case "2":
                    AddSubject();
                    break;
                case "3":
                    UpdateSubject();
                    break;
                case "4":
                    RemoveSubject();
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

        private void ShowAllSubjects()
        {
            PrintSubjects(_subjectsDAO.GetAllPredmets());
           // _subjectsDAO.showall();
        }

        private void RemoveSubject()
        {
            int id = InputId();
            Predmet? removedSubject = _subjectsDAO.RemovePredmet(id);
            if (removedSubject is null)
            {
                System.Console.WriteLine("Predmet nije pronadjen");
                return;
            }
            _studsub.RemoveBySubjectID(id);
            System.Console.WriteLine("Predmet izbrisan");

        }

        private void RemoveSubjectFromStudent()
        {
            int studID = InputStudentId();
            int subjID = InputId();

            if (!_studsub.RemoveSubjectFromStudent(subjID, studID))
            {
                System.Console.WriteLine("Student ne slusa dati predmet");
                return;
            }
            System.Console.WriteLine("Predmet uklonjen");
        }

        private void UpdateSubject()
        {
            int id = InputId();
            Predmet subject = InputSubject();
            subject.PredmetId = id;
            Predmet? updatePredmet = _subjectsDAO.UpdatePredmet(subject);
            if (updatePredmet == null)
            {
                System.Console.WriteLine("Predmet nije pronadjen");
                return;
            }

            System.Console.WriteLine("Predmet azuriran");
        }

        private void AddSubject()
        {
            Predmet subject = InputSubject();
            if (_subjectsDAO.AddPredmet(subject) is null)
            {
                System.Console.WriteLine("Profesor ne postoji");
                return;
            }
            System.Console.WriteLine("Predmet dodat");
        }

        private void AddSubjectToStudent()
        {
            int studID = InputStudentId();
            int subjID = InputId();

            if (!_studsub.AddSubjectToStudent(subjID, studID))
            {
                System.Console.WriteLine("Student ili Predmet ne postoje ili je Predmet vec dodan");
                return;
            }
            System.Console.WriteLine($"Student sa ID  {studID} slusa predmet sa id {subjID}");
        }

        private void ShowSubjectMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati predmete");
            System.Console.WriteLine("2: Dodati predmete");
            System.Console.WriteLine("3: Azurirati predmete");
            System.Console.WriteLine("4: Izbrisati predmete");
            System.Console.WriteLine("5: Dodati predmet studentu");
            System.Console.WriteLine("6: Skloniti studenta sa premeta");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
