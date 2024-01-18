using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
    public class ProfessorDepartmentDAO
    {
        private readonly List<ProfesorKatedra> _profkat;
        private readonly Storage<ProfesorKatedra> _storage;


        public ProfessorDepartmentDAO()
        {
            _storage = new Storage<ProfesorKatedra>("professor_department.txt");
            _profkat = _storage.Load();
        }

/*        public bool AddSubjectToStudent(int subjID, int studID)
        {
            SubjectDAO subjectDAO = new SubjectDAO();
            List<Predmet> _subjects = subjectDAO.GetAllPredmets();
            StudentDAO studentDAO = new StudentDAO();
            List<Student> _students = studentDAO.GetAllStudents();

            Predmet? predmet = _subjects.Find(s => s.PredmetId == subjID);
            Student? student = _students.Find(s => s.StudentId == studID);
            if (predmet is null || student is null || DoesStudSubExist(subjID, studID))
            {
                return false;
            }
            predmet.StudentiNepolozili.Add(student);
            studentDAO.AddSubjectToNonPassed(predmet, student.StudentId);
            //student.SpisakNepolozenihPredmeta.Add(predmet);
            StudentPredmet studentPredmet = new StudentPredmet(studID, subjID);
            //studentPredmet.StudentSubjectId = GenerateId();
            _profkat.Add(studentPredmet);
            _storage.Save(_profkat);
            return true;
        }*/

        /*        private int GenerateId()
                {
                    if (_studsub.Count == 0) return 0;
                    return _studsub[^1].StudentSubjectId + 1;
                }*/

/*        public bool RemoveSubjectFromStudent(int subjID, int studID)
        {
            int indexToRemove = _profkat.FindIndex(ss => ss.StudentId == studID && ss.SubjectId == subjID);
            if (indexToRemove == -1) return false;

            _profkat.RemoveAt(indexToRemove);
            _storage.Save(_profkat);
            return true;
        }*/

/*        private bool DoesStudSubExist(int studID, int subjID)
        {
            return _profkat.Any(ss => ss.StudentId == studID && ss.SubjectId == subjID);
        }*/

        public List<ProfesorKatedra> GetAllStudentSubject()
        {
            return _profkat;
        }

/*        public void RemoveByStudentID(int id)
        {
            foreach (StudentPredmet sp in _profkat)
            {
                if (sp.StudentId == id)
                {
                    _profkat.Remove(sp);
                    _storage.Save(_profkat);
                }
            }
        }*/

/*        public void RemoveBySubjectID(int id)
        {
            foreach (StudentPredmet sp in _profkat)
            {
                if (sp.SubjectId == id)
                {
                    _profkat.Remove(sp);
                    _storage.Save(_profkat);
                }
            }
        }*/

/*        public List<Predmet>? GetAvailableSubjects(List<Predmet>? spisakNepolozenihPredmeta, int studentId, int trenutnaGodinaStudija)
        {
            // mozada bude bug sa id studenta jer prije kloniranja se salje
            SubjectDAO subjectDAO = new SubjectDAO();
            List<Predmet> _subjects = subjectDAO.GetAllPredmets();
            List<Predmet> availableSubjects = new List<Predmet>();
            foreach (Predmet sp in _subjects)
            {
                if (!DoesStudSubExist(studentId, sp.PredmetId) && trenutnaGodinaStudija >= sp.GodinaStudija)
                {
                    availableSubjects.Add(sp);
                }
            }
            return availableSubjects;
        }*/
    }
}
