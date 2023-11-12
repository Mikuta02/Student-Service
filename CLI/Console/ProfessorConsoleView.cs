using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class ProfessorConsoleView
    {

        private readonly ProfessorsDAO _professorsDAO;

        public ProfessorConsoleView(ProfessorsDAO professorsDAO)
        {
            _professorsDAO = professorsDAO;
        }
        private void PrintProfessors(List<Profesor> professors)
        {
            System.Console.WriteLine("Profesori: ");
            string header = $"Ime {"",12} | Prezime {"",12} | Datum Rodjenja {"",11} | Adresa {"",13} | Kontakt {"",10} | Email {"",20} | Broj Licne {"",7} | Zvanje {"",8} | Godina Staza {"",3} |";
            System.Console.WriteLine(header);
            foreach (Profesor professor in professors)
            {
                System.Console.WriteLine(professor);
            }
        }

        private Profesor InputProfessor()
        {
            System.Console.WriteLine("Uneti ime profesora: ");
            string Ime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti prezime profesora: ");
            string Prezime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti datum rodjena profesora: ");
            string DatumRodjena = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti adresu profesora: ");
            string Adresa = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti kontakt profesora: ");
            string Kontakt = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti email profesora: ");
            string Email = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti broj licne profesora: ");
            int Licna = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti zvanje profesora: ");
            string Zvanje = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti staz profesora: ");
            int Staz = ConsoleViewUtils.SafeInputInt();

            return new Profesor(Ime, Prezime, DatumRodjena, Adresa, Kontakt, Email, Licna, Zvanje, Staz);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID profesora: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        public void RunProfessorMenu()
        {
            while (true)
            {
                ShowProfessorMenu();
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
                    ShowAllProfessors();
                    break;
                case "2":
                    AddProfessor();
                    break;
                case "3":
                    UpdateProfessor();
                    break;
                case "4":
                    RemoveProfessor();
                    break;
                case "9":
                    return;
            }
        }

        private void ShowAllProfessors()
        {
            PrintProfessors(_professorsDAO.GetAllProfessors());
        }

        private void RemoveProfessor()
        {
            int id = InputId();
            Profesor? removedProfessor = _professorsDAO.RemoveProfessor(id);
            if (removedProfessor is null)
            {
                System.Console.WriteLine("Profesor nije pronadjen");
                return;
            }
            System.Console.WriteLine("Profesor izbrisan");

        }

        private void UpdateProfessor()
        {
            int id = InputId();
            Profesor professor = InputProfessor();
            professor.ProfesorId = id;
            Profesor? updateProfesor = _professorsDAO.UpdateProfessor(professor);
            if (updateProfesor == null)
            {
                System.Console.WriteLine("Profesor nije pronadjen");
                return;
            }

            System.Console.WriteLine("Profesor azuriran");
        }

        private void AddProfessor()
        {
            Profesor professor = InputProfessor();
            _professorsDAO.AddProfessor(professor);
            System.Console.WriteLine("Profesor dodat");
        }
        private void ShowProfessorMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati professore");
            System.Console.WriteLine("2: Dodati professora");
            System.Console.WriteLine("3: Azurirati professora");
            System.Console.WriteLine("4: Izbrisati professora");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
