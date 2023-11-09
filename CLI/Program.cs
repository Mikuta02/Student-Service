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
        view.RunMenu();
    }
}
