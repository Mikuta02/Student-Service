using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class DepartmentConsoleView
    {

        private readonly DepartmentDAO _departmentsDAO;

        public DepartmentConsoleView(DepartmentDAO departmentsDAO)
        {
            _departmentsDAO = departmentsDAO;
        }
        private void PrintDepartments(List<Katedra> deparments)
        {
            System.Console.WriteLine("Katedre: ");
            string header = $"ID {"",2} | Sifra katedre {"",5} | Naziv katedre {"",25} | Sef {"",2}";
            System.Console.WriteLine(header);
            foreach (Katedra department in deparments)
            {
                System.Console.WriteLine(department);
            }
        }

        private Katedra InputDepartment()
        {
            System.Console.WriteLine("Uneti sifru katedre: ");
            string Sifra = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti naziv katedre: ");
            string Naziv = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti Id sefa: ");
            int SefId = ConsoleViewUtils.SafeInputInt();

            return new Katedra(Sifra, Naziv, SefId);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID katedre: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private int InputProfessorId()
        {
            System.Console.WriteLine("Uneti ID profesora: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        public void RunDepartmentMenu()
        {
            while (true)
            {
                ShowDepartmentMenu();
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
                    ShowAllDepartments();
                    break;
                case "2":
                    AddDepartment();
                    break;
                case "3":
                    UpdateDepartment();
                    break;
                case "4":
                    RemoveDepartment();
                    break;
                case "5":
                    AddProfesorToDepartment();
                    break;
                case "6":
                    RemoveProfessorFromDepartment();
                    break;
                case "7":
                    ShowProfessors();
                    break;
                case "9":
                    return;
            }
        }

        private void ShowAllDepartments()
        {
            PrintDepartments(_departmentsDAO.GetAllDepartments());
        }

        private void RemoveDepartment()
        {
            int id = InputId();
            Katedra? removedDepartment = _departmentsDAO.RemoveDepartment(id);
            if (removedDepartment is null)
            {
                System.Console.WriteLine("Katedra nije pronadjena");
                return;
            }
            System.Console.WriteLine("Katedra izbrisana");

        }

        private void UpdateDepartment()
        {
            int id = InputId();
            Katedra department = InputDepartment();
            department.KatedraId = id;
            Katedra? updateDepartment = _departmentsDAO.UpdateDepartment(department);
            if (updateDepartment == null)
            {
                System.Console.WriteLine("Katedra nije pronadjena");
                return;
            }

            System.Console.WriteLine("Katedra azurirana");
        }

        private void AddDepartment()
        {
            Katedra department = InputDepartment();
            if (_departmentsDAO.AddDepartment(department) is null)
            {
                System.Console.WriteLine("Profesor nije pronadjen");
                return;
            }
            System.Console.WriteLine("Katedra dodana");
        }


        private void AddProfesorToDepartment()
        {
            int depID = InputId();
            int profID = InputProfessorId();

            if (!_departmentsDAO.AddProfessorToDepartment(profID, depID))
            {
                System.Console.WriteLine("Profesor ili Katedra ne postoje ili je Profesor vec dodan");
                return;
            }
            System.Console.WriteLine($"Profesor sa ID {profID} dodan na katedru sa id {depID}");
        }

        private void RemoveProfessorFromDepartment()
        {
            int depID = InputId();
            int profID = InputProfessorId();

            if (!_departmentsDAO.RemoveProfessorFromDepartment(profID, depID))
            {
                System.Console.WriteLine("Dati profesor nije na katedri ili katedra ne postoji");
                return;
            }
            System.Console.WriteLine("Profesor uklonjen");
        }

        private void ShowProfessors()
        {
            int depID = InputId();

            if(!_departmentsDAO.ShowProfessors(depID)) System.Console.WriteLine("Datakatedra ne postoji"); 
        }

        private void ShowDepartmentMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati katedru");
            System.Console.WriteLine("2: Dodati katedru");
            System.Console.WriteLine("3: Azurirati katedru");
            System.Console.WriteLine("4: Izbrisati katedru");
            System.Console.WriteLine("5: Dodati profesora na katedru");
            System.Console.WriteLine("6: Ukloniti profesora sa katedre");
            System.Console.WriteLine("7: Prikazati profesore na katedri");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
