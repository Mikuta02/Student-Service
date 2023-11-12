using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    class StudentDAO
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;


        public StudentDAO()
        {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
        }

        private int GenerateId()
        {
            if (_students.Count == 0) return 0;
            return _students[^1].StudentId + 1;
        }

        public Student AddStudent(Student student)
        {
            student.StudentId = GenerateId();
            _students.Add(student);
            _storage.Save(_students);
            return student;
        }

/*        public void AddSubjectToStudent()
        {
            StudentSubjectDAO studentSubjectDAO = new StudentSubjectDAO();
            List<StudentPredmet> studentSubjects = studentSubjectDAO.GetAllStudentSubject();
            SubjectDAO subjectDAO = new SubjectDAO();
            List<Predmet> _subjects = subjectDAO.GetAllPredmets();

            Student student = _students.Find(s => s.StudentId == studentSubject.StudentId);
            Predmet subject = _subjects.Find(s => s.PredmetId == studentSubject.SubjectId);
            student.Nepolo.Add(subject);
            subject.Students.Add(student);
            
        }*/

        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetStudentById(student.StudentId);
            if (oldStudent is null) return null;

            oldStudent.Ime = student.Ime;
            oldStudent.Prezime = student.Prezime;
            oldStudent.DatumRodjenja = student.DatumRodjenja;
            oldStudent.Adresa = student.Adresa;
            oldStudent.KontaktTelefon = student.KontaktTelefon;
            oldStudent.Email = student.Email;
            oldStudent.BrojIndeksa = student.BrojIndeksa;
            oldStudent.TrenutnaGodinaStudija = student.TrenutnaGodinaStudija;
            oldStudent.ProsecnaOcena = student.ProsecnaOcena;

            _storage.Save(_students);
            return oldStudent;
        }

        public Student? RemoveStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return null;

            _students.Remove(student);
            _storage.Save(_students);
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

        public List<Student> GetAllStudents(int page, int pageSize, string sortCriteria)
        {
            IEnumerable<Student> students = _students;

            // sortiraj vehicles ukoliko je sortCriteria naveden
            switch (sortCriteria)
            {
                case "Id":
                    students = _students.OrderBy(x => x.StudentId);
                    break;
                case "Ime":
                    students = _students.OrderBy(x => x.Ime);
                    break;
                case "Prezime":
                    students = _students.OrderBy(x => x.Prezime);
                    break;
                case "Datum Rodjenja":
                    students = _students.OrderBy(x => x.DatumRodjenja);
                    break;
                case "Adresa":
                    students = _students.OrderBy(x => x.Adresa);
                    break;
                case "Kontakt":
                    students = _students.OrderBy(x => x.KontaktTelefon);
                    break;
                case "Email":
                    students = _students.OrderBy(x => x.Email);
                    break;
                case "Indeks":
                    students = _students.OrderBy(x => x.BrojIndeksa);
                    break;
            }

            // promeni redosled ukoliko ima potrebe za tim
            //if (sortDirection == SortDirection.Descending)
             //   vehicles = vehicles.Reverse();

            // paginacija
            students = students.Skip((page - 1) * pageSize).Take(pageSize);

            return students.ToList();
        }
    }
}
