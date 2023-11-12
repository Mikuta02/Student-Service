using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    internal class AdressConsoleView
    {

        private readonly AdressDAO _adressesDAO;

        public AdressConsoleView(AdressDAO adressesDAO)
        {
            _adressesDAO = adressesDAO;
        }
        private void PrintAdresses(List<Adresa> adresses)
        {
            System.Console.WriteLine("Adrese: ");
            string header = $"Ulica {"",12} | Broj {"",4} | Grad {"",11} | Drzava {"",13}";
            System.Console.WriteLine(header);
            foreach (Adresa adresa in adresses)
            {
                System.Console.WriteLine(adresa);
            }
        }

        private Adresa InputAdress()
        {
            System.Console.WriteLine("Uneti naziv ulice: ");
            string Ulica = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti broj ulice: ");
            int broj = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Uneti grad: ");
            string grad = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Uneti drzava: ");
            string drzava = System.Console.ReadLine() ?? string.Empty;

            

            return new Adresa(Ulica,broj,grad,drzava);
        }

        private int InputId()
        {
            System.Console.WriteLine("Uneti ID adrese: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        public void RunAdressMenu()
        {
            while (true)
            {
                ShowAdressMenu();
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
                    ShowAllAdresses();
                    break;
                case "2":
                    AddAdress();
                    break;
                case "3":
                    UpdateAdress();
                    break;
                case "4":
                    RemoveAdress();
                    break;
                case "9":
                    return;
            }
        }

        private void ShowAllAdresses()
        {
            PrintAdresses(_adressesDAO.GetAllAdress());
        }

        private void RemoveAdress()
        {
            int id = InputId();
            Adresa? removedAdresa = _adressesDAO.RemoveAdress(id);
            if (removedAdresa is null)
            {
                System.Console.WriteLine("Nema adrese");
                return;
            }
            System.Console.WriteLine("adresa izbrisana");

        }

        private void UpdateAdress()
        {
            int id = InputId();
            Adresa adresa = InputAdress();
            adresa.AdresaId = id;
            Adresa? updateAdresa = _adressesDAO.UpdateAdress(adresa);
            if (updateAdresa == null)
            {
                System.Console.WriteLine("Adresa nije pronadjena");
                return;
            }

            System.Console.WriteLine("adresa azurirana");
        }

        private void AddAdress()
        {
            Adresa adresa = InputAdress();
            _adressesDAO.AddAdress(adresa);
            System.Console.WriteLine("Adresa dodata");
        }
        private void ShowAdressMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati adresu");
            System.Console.WriteLine("2: Dodati adresu");
            System.Console.WriteLine("3: Azurirati adresu");
            System.Console.WriteLine("4: Izbrisati adresu");
            System.Console.WriteLine("9: Nazad");
            System.Console.WriteLine("0: Close");
        }

    }


}
