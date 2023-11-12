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
        //StudentConsoleView view = new StudentConsoleView(students);
        //view.RunMenu();

        Profesor profesor1 = new Profesor("Nebojsa","ralevic","12.10.1011","New Now","091237","Rcma123@gmail.com",123123,"Kurton",123);
        Profesor profesor2 = new Profesor("Srdjan", "Popov", "01.10.2011", "Big Obarska", "dsads", "dyunimakaroni@gmail.com", 333, "Kuriton", 123);
        ProfessorsDAO professors = new ProfessorsDAO();
       //professors.AddProfesor(profesor2);

        Predmet predmet1 = new Predmet("A1", "Analiza 1", "prva", 0, 9);
        Predmet predmet2 = new Predmet("PJISP", "Programski jezici i strukture podataka", "prva", 1, 9);
        SubjectsDAO subjects = new SubjectsDAO();
        //subjects.AddPredmet(predmet2);

        Katedra katedra1 = new Katedra("MN", "Departman za opšte discipline u tehnici", "Rale");
        DepartmentsDAO departments = new DepartmentsDAO();
       // departments.AddKatedra(katedra1);

        Adresa adresa1 = new Adresa("Rade Kondica",9,"Derventa","Republika Srpska");
        AdressesDAO adresses = new AdressesDAO();
        ///  adresses.AddAdresa(adresa1);
        ///  
        OcenaNaIspitu ocena1 = new OcenaNaIspitu(0, 0, 7, "06.03.1945");
        ExamGradesDAO grades = new ExamGradesDAO();
        //grades.AddOcenaNaIspitu(ocena1);

        Indeks indeks1 = new Indeks("rarara",666,1945);
        IndexesDAO indexes = new IndexesDAO();
        //indexes.AddIndeks(indeks1);

        ConsoleView view = new ConsoleView(students, professors, subjects, grades, indexes, adresses, departments);
        //view.RunMenu();
    }
}
