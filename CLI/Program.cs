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
        profesors.AddProfesor(profesor1);

    }
}
