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

        private readonly SubjectsDAO _subjectsDAO;

        public SubjectConsoleView(SubjectsDAO subjectsDAO)
        {
            _subjectsDAO = subjectsDAO;
        }
        private void PrintSubjects(List<Predmet> subjects)
        {
            System.Console.WriteLine("Predmeti: ");
            string header = $"Ime {"",12} | Prezime {"",12} | Datum Rodjenja {"",11} | Adresa {"",13} | Kontakt {"",10} | Email {"",20} | Broj Licne {"",7} | Zvanje {"",8} | Godina Staza {"",3} |";
            System.Console.WriteLine(header);
            foreach (Predmet subject in subjects)
            {
                System.Console.WriteLine(subjects);
            }
        }

        private Predmet InputSubject()
        {
            System.Console.WriteLine("Uneti sifru predmeta: ");
            string Sifra = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti naziv predmeta: ");
            string Naziv = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti godinu: ");
            string Godina = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti profesor id: ");
            int ProfID = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti espb: ");
            int ESPB = ConsoleViewUtils.SafeInputInt();

            return new Predmet(Sifra, Naziv, Godina, ProfID, ESPB);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID predmeta: ");
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
                case "9":
                    return;
            }
        }

        private void ShowAllSubjects()
        {
            PrintSubjects(_subjectsDAO.GetAllPredmets());
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
            System.Console.WriteLine("Predmet izbrisan");

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
            _subjectsDAO.AddPredmet(subject);
            System.Console.WriteLine("Predmet dodat");
        }
        private void ShowSubjectMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati predmete");
            System.Console.WriteLine("2: Dodati predmete");
            System.Console.WriteLine("3: Azurirati predmete");
            System.Console.WriteLine("4: Izbrisati predmete");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
