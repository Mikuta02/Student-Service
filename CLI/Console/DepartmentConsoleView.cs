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

        private readonly DepartmentsDAO _departmentsDAO;

        public DepartmentConsoleView(DepartmentsDAO departmentsDAO)
        {
            _departmentsDAO = departmentsDAO;
        }
        private void PrintDepartments(List<Katedra> deparments)
        {
            System.Console.WriteLine("Profesori: ");
            string header = $"Sifra katedre {"",12} | Naziv katedre {"",12} | Sef {"",2}";
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
            _departmentsDAO.UpdateDepartment(department);
            System.Console.WriteLine("Katedra dodana");
        }
        private void ShowDepartmentMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati katedru");
            System.Console.WriteLine("2: Dodati katedru");
            System.Console.WriteLine("3: Azurirati katedru");
            System.Console.WriteLine("4: Izbrisati katedru");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }
}
