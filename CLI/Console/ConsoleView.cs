using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class ConsoleView
    {
        private readonly StudentDAO _studentsDao;
        private readonly ProfessorDAO _profesDao;
        private readonly SubjectDAO _subjectsDao;
        private readonly ExamGradesDAO _examGradesDao;
        private readonly AdressDAO _addressesDao;
        private readonly DepartmentDAO _departmentsDao;
        private readonly StudentSubjectDAO _studentSubjectDao;

        public ConsoleView(StudentDAO studentsDao, ProfessorDAO profesDao, SubjectDAO subjectsDao, ExamGradesDAO examGradesDao, AdressDAO addressesDao, DepartmentDAO departmentsDao, StudentSubjectDAO studentSubjectDao)
        {
            _studentsDao = studentsDao;
            _profesDao = profesDao;
            _subjectsDao = subjectsDao;
            _examGradesDao = examGradesDao;
            _addressesDao = addressesDao;
            _departmentsDao = departmentsDao;
            _studentSubjectDao = studentSubjectDao;
            fillObjects(studentsDao, profesDao, subjectsDao, examGradesDao, addressesDao,departmentsDao,studentSubjectDao);
            List<OcenaNaIspitu> ocene = examGradesDao.GetAllGrades();
            List<Student> students = studentsDao.GetAllStudents();
            List<Profesor> profesors = profesDao.GetAllProfessors();
            List<Katedra> deps = departmentsDao.GetAllDepartments();
            foreach (OcenaNaIspitu o in ocene)
            {
                System.Console.WriteLine(o.PredmetStudenta);
                System.Console.WriteLine("\nKURAC");
                System.Console.WriteLine(o.StudentPolozio);
            }
            foreach (Student o in students)
            {
                foreach(Predmet p in o.SpisakNepolozenihPredmeta)
                {
                    //System.Console.WriteLine(p);
                }
                foreach(OcenaNaIspitu oi in o.SpisakPolozenihIspita)
                {
                    //System.Console.WriteLine(oi);
                }
            }
            foreach (Profesor p in profesors)
            {
                foreach(Predmet pred in p.Predmeti)
                {
                    //System.Console.WriteLine(pred);
                }
            }
            foreach (Katedra k in deps)
            {
                System.Console.WriteLine(k.Sef);
                /*                foreach(Profesor p in k.Profesori)
                                {
                                    System.Console.WriteLine(p);
                                }*/
            }
        }

        private void fillObjects(StudentDAO studentsDao, ProfessorDAO profesDao, SubjectDAO subjectsDao, ExamGradesDAO examGradesDao, AdressDAO addressesDao, DepartmentDAO departmentsDao, StudentSubjectDAO studentSubjectDao)
        {
            studentsDao.fillObjectsAndLists();
            profesDao.fillObjectsAndLists();
            subjectsDao.fillObjectsAndLists();
            departmentsDao.fillObjectsAndLists();
            examGradesDao.fillObjectsAndLists(studentsDao, subjectsDao);
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
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
                    StudentConsoleView viewStudents = new StudentConsoleView(_studentsDao, _studentSubjectDao);
                    viewStudents.RunStudentMenu();
                    break;
                case "2":
                    ProfessorConsoleView viewProfessors = new ProfessorConsoleView(_profesDao); 
                    viewProfessors.RunProfessorMenu();
                    break;
                case "3":
                    SubjectConsoleView viewSubjects = new SubjectConsoleView(_subjectsDao, _studentSubjectDao); 
                    viewSubjects.RunSubjectMenu();
                    break;
                case "4":
                    GradesConsoleView viewGrades = new GradesConsoleView(_examGradesDao); 
                    viewGrades.RunExamGradeMenu();
                    break;
                case "5":
                    DepartmentConsoleView viewDepartments = new DepartmentConsoleView(_departmentsDao); 
                    viewDepartments.RunDepartmentMenu();
                    break;
                case "6":
                    AdressConsoleView viewAdresses = new AdressConsoleView(_addressesDao); 
                    viewAdresses.RunAdressMenu();
                    break;
            }
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazati studente");
            System.Console.WriteLine("2: Prikazati profesore");
            System.Console.WriteLine("3: Prikazati predmete");
            System.Console.WriteLine("4: Prikazati ocene");
            System.Console.WriteLine("5: Prikazati katedre");
            System.Console.WriteLine("6: Prikazati adrese");
            System.Console.WriteLine("0: Close");
        }
    }
}
