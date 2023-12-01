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
    class StudentSubjectDAO
    {
        private readonly List<StudentPredmet> _studsub;
        private readonly Storage<StudentPredmet> _storage;


        public StudentSubjectDAO()
        {
            _storage = new Storage<StudentPredmet>("student_subject.txt");
            _studsub = _storage.Load();
        }

        public bool AddSubjectToStudent(int subjID, int studID)
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
            student.SpisakNepolozenihPredmeta.Add(predmet);
            StudentPredmet studentPredmet = new StudentPredmet(studID, subjID);
            //studentPredmet.StudentSubjectId = GenerateId();
            _studsub.Add(studentPredmet);
            _storage.Save(_studsub);
            return true;
        }

/*        private int GenerateId()
        {
            if (_studsub.Count == 0) return 0;
            return _studsub[^1].StudentSubjectId + 1;
        }*/

        public bool RemoveSubjectFromStudent(int subjID, int studID)
        {
            int indexToRemove = _studsub.FindIndex(ss => ss.StudentId == studID && ss.SubjectId == subjID);
            if (indexToRemove == -1) return false;

            _studsub.RemoveAt(indexToRemove);
            _storage.Save(_studsub);
            return true;
        }

        private bool DoesStudSubExist(int studID, int subjID)
         {
            return _studsub.Any(ss => ss.StudentId == studID && ss.SubjectId == subjID);
         }

        public List<StudentPredmet> GetAllStudentSubject()
        {
            return _studsub;
        }

        public void RemoveByStudentID(int id)
        {
            foreach (StudentPredmet sp in _studsub)
            {
                if(sp.StudentId == id)
                {
                    _studsub.Remove(sp);
                    _storage.Save(_studsub); 
                }
            }
        }
        public void RemoveBySubjectID(int id)
        {
            foreach (StudentPredmet sp in _studsub)
            {
                if (sp.SubjectId == id)
                {
                    _studsub.Remove(sp);
                    _storage.Save(_studsub);
                }
            }
        }
    }
}
