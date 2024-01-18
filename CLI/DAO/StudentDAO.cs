using CLI.Model;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    public class StudentDAO
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;
        public Subject StudentSubject;


        public StudentDAO()
        {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
            StudentSubject = new Subject();
        }

        private int GenerateId()
        {
            if (_students.Count == 0) return 0;
            return _students[^1].StudentId + 1;
        }

        public Student? AddStudent(Student student)
        {
            student.StudentId = GenerateId();

            Adresa? adresa = AddAdresaToStudent(student);
            if (adresa == null)
            {
                return null;
            }
            student.Adresa = adresa;
            _students.Add(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }

        private Adresa? AddAdresaToStudent(Student student)
        {
            AdressDAO adressDAO = new AdressDAO();
            List<Adresa> adresses = adressDAO.GetAllAdress();

            Adresa? adresa = adresses.Find(p => p.AdresaId == student.AdresaId);
            if (adresa != null)
            {
                return adresa;
            }
            else
            {
                System.Console.WriteLine("Error: Address not found");
                return null;
            }

        }

        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetStudentById(student.StudentId);
            if (oldStudent is null) return null;

            Adresa? adresa = AddAdresaToStudent(student);
            if (adresa == null)
            {
                return null;
            }

            oldStudent.Ime = student.Ime;
            oldStudent.Prezime = student.Prezime;
            oldStudent.DatumRodjenja = student.DatumRodjenja;
            oldStudent.AdresaId = student.AdresaId;
            oldStudent.Adresa = adresa;
            oldStudent.KontaktTelefon = student.KontaktTelefon;
            oldStudent.Email = student.Email;
            oldStudent.BrojIndeksa = student.BrojIndeksa;
            oldStudent.TrenutnaGodinaStudija = student.TrenutnaGodinaStudija;
            oldStudent.ProsecnaOcena = student.ProsecnaOcena;

            fillObjectsAndLists();
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return oldStudent;
        }

        public Student? RemoveStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return null;

            _students.Remove(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }

        public Student? GetStudentById(int id)
        {
            return _students.Find(s => s.StudentId == id);
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public void fillObjectsAndLists()
        {
            Storage<Predmet> _predmetiStorage = new Storage<Predmet>("predmet.txt");
            List<Predmet> subjects = _predmetiStorage.Load();

            Storage<StudentPredmet> _studentSubStorage = new Storage<StudentPredmet>("student_subject.txt");
            List<StudentPredmet> studentSubjects = _studentSubStorage.Load();

            Storage<Adresa> _adresaStorage = new Storage<Adresa>("adresses.txt");
            List<Adresa> adresses = _adresaStorage.Load();

            ExamGradesDAO examGradesDAO = new ExamGradesDAO();

            //spisak nepolozenih, polozenih
            foreach (StudentPredmet sp in studentSubjects)
            {
                Student? student = GetStudentById(sp.StudentId);
                Predmet? predmet = subjects.Find(s => s.PredmetId == sp.SubjectId);
                if (student != null && predmet!=null)
                {
                    if (!examGradesDAO.didStudentPass(student.StudentId, predmet.PredmetId))
                    {
                        student.SpisakNepolozenihPredmeta.Add(predmet);
                    }
                    else
                    {
                        OcenaNaIspitu ocena = examGradesDAO.GetGradeByStudentAndSubjectIds(student.StudentId, predmet.PredmetId);
                        student.SpisakPolozenihIspita.Add(ocena);
                    }   
                }
            }

            //prosjecna
            double sumaOcena = 0;
            int count = 0;
            foreach(Student s in _students)
            {
                foreach (OcenaNaIspitu O in s.SpisakPolozenihIspita)
                {
                    if (O != null)
                    {
                        sumaOcena += O.Ocena;
                        ++count;
                    }
                }
                if (count!=0)
                {
                    s.ProsecnaOcena = sumaOcena / count;
                }
                else
                {
                    s.ProsecnaOcena = 0;
                }
                
            }


            //adresa
            foreach (Student s in _students)
            {
                Adresa? adresa = adresses.Find(p => p.AdresaId == s.AdresaId);
                s.Adresa = adresa;
            }
            StudentSubject.NotifyObservers();
        }

        internal void PassOrFailExam(int studentId, int predmetId, OcenaNaIspitu ocena, bool v)
        {
            Student? student = GetStudentById(studentId);
            
            if (v == true)
            {
                Predmet? predmet = student.SpisakNepolozenihPredmeta.Find(s => s.PredmetId == predmetId);
                student.SpisakNepolozenihPredmeta.Remove(predmet);
                student.SpisakPolozenihIspita.Add(ocena);
            }
            else
            {
                Predmet? predmet = ocena.PredmetStudenta;
                student.SpisakPolozenihIspita.Remove(ocena);
                student.SpisakNepolozenihPredmeta.Add(predmet);
            }

        }

        public List<Predmet>? LoadSpisakNepolozenihPredmeta(int studentId)
        {
            Student? student = _students.Find(s => s.StudentId == studentId);
            return student.SpisakNepolozenihPredmeta;
        }

        internal void AddSubjectToNonPassed(Predmet predmet, int studentId)
        {
            Student? student = GetStudentById(studentId);
            if(student != null)
            {
                student.SpisakNepolozenihPredmeta.Add(predmet);
            }
            //StudentSubject.NotifyObservers();
        }

        public List<OcenaNaIspitu>? LoadSpisakPolozenihPredmeta(int studentId)
        {
            Student? student = _students.Find(s => s.StudentId == studentId);
            return student.SpisakPolozenihIspita;
        }


    }
}
