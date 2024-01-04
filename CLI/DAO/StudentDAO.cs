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

        private Student? GetStudentById(int id)
        {
            return _students.Find(s => s.StudentId == id);
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public void fillObjectsAndLists(StudentSubjectDAO studentSubjectDao, SubjectDAO subjectsDao, AdressDAO addressesDao)
        {
            List<Predmet> subjects = subjectsDao.GetAllPredmets();
            List<StudentPredmet> studentSubjects = studentSubjectDao.GetAllStudentSubject();
            List<Adresa> adresses = addressesDao.GetAllAdress();
            //spisak nepolozenih
            foreach(StudentPredmet sp in studentSubjects)
            {
                Student? student = GetStudentById(sp.StudentId);
                Predmet? predmet = subjects.Find(s => s.PredmetId == sp.SubjectId);
                if (student != null && predmet!=null)
                {
                    student.SpisakNepolozenihPredmeta.Add(predmet);
                }
            }
            //adresa
            foreach(Student s in _students)
            {
                Adresa? adresa = adresses.Find(p => p.AdresaId == s.AdresaId);
                s.Adresa = adresa;
            }
            
        }

        internal void PassOrFailExam(int studentId, int predmetId, OcenaNaIspitu ocena, bool v)
        {
            Student? student = GetStudentById(studentId);
            Predmet? predmet = student.SpisakNepolozenihPredmeta.Find(s => s.PredmetId == predmetId);
            if (v == true)
            {
                student.SpisakNepolozenihPredmeta.Remove(predmet);
                student.SpisakPolozenihIspita.Add(ocena);
            }
            else
            {
                student.SpisakPolozenihIspita.Remove(ocena);
                student.SpisakNepolozenihPredmeta.Add(predmet);
            }

        }

        public List<Predmet>? LoadSpisakNepolozenihPredmeta(int studentId)
        {
            Student? student = _students.Find(s => s.StudentId == studentId);
            return student.SpisakNepolozenihPredmeta;
        }
    }
}
