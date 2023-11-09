// See https://aka.ms/new-console-template for more information
using CLI.Console;
using CLI.DAO;
using CLI.Model;
using System.Data;

class Program
{
    static void Main()
    {
        StudentsDAO students = new StudentsDAO();
        ConsoleView view = new ConsoleView(students);
      //  view.RunMenu();

        Profesor profesor1 = new Profesor("Nebojsa","ralevic","12.10.1011","New Now","091237","Rcma123@gmail.com",123123,"Kurton",123);
        ProfesorsDAO profesors = new ProfesorsDAO();
       // profesors.AddProfesor(profesor1);

        Predmet predmet1 = new Predmet("A1", "Analiza 1", "prva", 0, 9);
        SubjectsDAO subjects = new SubjectsDAO();
        //subjects.AddPredmet(predmet1);

        Katedra katedra1 = new Katedra("MN", "Departman za opšte discipline u tehnici", "Rale");
        DepartmentsDAO departments = new DepartmentsDAO();
        departments.AddKatedra(katedra1);

        Adresa adresa1 = new Adresa("Rade Kondica",9,"Derventa","Republika Srpska");
        AdressesDAO adresses = new AdressesDAO();
        adresses.AddAdresa(adresa1);

    }
}
