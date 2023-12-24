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
    public class SubjectDAO
    {
        private readonly List<Predmet> _subjects;
        private readonly Storage<Predmet> _storage;
        public Subject PredmetSubject;

        public SubjectDAO()
        {
            _storage = new Storage<Predmet>("predmet.txt");
            _subjects = _storage.Load();
        }

        private int GenerateId()
        {
            if (_subjects.Count == 0) return 0;
            return _subjects[^1].PredmetId + 1;
        }

        public Predmet? AddPredmet(Predmet predmet)
        {
            predmet.PredmetId = GenerateId();

            if(!AddProfessorToSubject(predmet)) return null;
            _subjects.Add(predmet);
            _storage.Save(_subjects);
            PredmetSubject.NotifyObservers();
            return predmet;
        }

       public bool AddProfessorToSubject(Predmet predmet)
        {
            ProfessorDAO professorsDAO = new();
            List<Profesor> _professors = professorsDAO.GetAllProfessors(); 

            Profesor? profesor = _professors.Find(p => p.ProfesorId == predmet.ProfesorID);//professorsDAO.GetProfessorById(s.ProfesorID);
            if (profesor == null) return false;
            profesor.Predmeti.Add(predmet);
            predmet.ProfesorPredmeta = profesor;
            return true;
        }

        public Predmet? UpdatePredmet(Predmet predmet)
        {
            Predmet? oldPredmet = GetPredmetById(predmet.PredmetId);
            if (oldPredmet is null) return null;

            oldPredmet.SifraPredmeta = predmet.SifraPredmeta;
            oldPredmet.Naziv = predmet.Naziv;
            oldPredmet.Semestar = predmet.Semestar;
            oldPredmet.GodinaStudija = predmet.GodinaStudija;
            oldPredmet.ProfesorID = predmet.ProfesorID;
            oldPredmet.ESPB = predmet.ESPB;
            AddProfessorToSubject(predmet);
            _storage.Save(_subjects);
            PredmetSubject.NotifyObservers();
            return oldPredmet;
        }

        public Predmet? RemovePredmet(int id)
        {
            Predmet? predmet = GetPredmetById(id);
            if (predmet == null) return null;

            _subjects.Remove(predmet);
            _storage.Save(_subjects);
            PredmetSubject.NotifyObservers();
            return predmet;
        }

        private Predmet? GetPredmetById(int id)
        {
            return _subjects.Find(s => s.PredmetId == id);
        }

        public List<Predmet> GetAllPredmets()
        {
            return _subjects;
        }

        internal void fillObjectsAndLists(StudentDAO studentsDao, StudentSubjectDAO studentSubjectDao, ProfessorDAO profesDao)
        {
            List<Student> students = studentsDao.GetAllStudents();
            List<StudentPredmet> studentSubjects = studentSubjectDao.GetAllStudentSubject();
            List<Profesor> professors = profesDao.GetAllProfessors();

            //spisak koji nisu polozili
            foreach (StudentPredmet sp in studentSubjects)
            {
                Predmet? predmet = GetPredmetById(sp.SubjectId);
                Student? student = students.Find(s => s.StudentId == sp.StudentId);
                if (student != null && predmet != null)
                {
                    predmet.StudentiNepolozili.Add(student);
                }
            }
            //profesor
            foreach(Predmet pred in _subjects)
            {
                Profesor? profesor = professors.Find(p => p.ProfesorId == pred.ProfesorID);
                if (profesor == null) continue;
                pred.ProfesorPredmeta = profesor;
            }
        }

/*        internal void showall()
        {
            foreach(Predmet sub in _subjects)
            {
                foreach(Student stud in sub.StudentiNepolozili)
                {
                    System.Console.WriteLine($"ID OVOG PREDMETA{sub.PredmetId}\t{stud}\n");
                }
            }
        }*/
    }
}
