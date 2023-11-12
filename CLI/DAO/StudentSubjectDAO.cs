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

/*        private int GenerateId()
        {
            if (_studsub.Count == 0) return 0;
            return _studsub[^1].StudentPredmetId + 1;
        }*/

/*        public StudentPredmet AddAdress(StudentPredmet studentPredmet)
        {
            studentPredmet.StudentPredmetId = GenerateId();
            _studsub.Add(studentPredmet);
            _storage.Save(_studsub);
            return studentPredmet;
        }*/

/*        public StudentPredmet? UpdateAdress(StudentPredmet studentPredmet)
        {
            StudentPredmet? oldStudentPredmet = GetAdressById(studentPredmet.StudentPredmetId);
            if (oldStudentPredmet is null) return null;

            oldStudentPredmet.Ulica = studentPredmet.Ulica;
            oldStudentPredmet.Broj = studentPredmet.Broj;
            oldStudentPredmet.Grad = studentPredmet.Grad;
            oldStudentPredmet.Drzava = studentPredmet.Drzava;

            _storage.Save(_studsub);
            return oldStudentPredmet;
        }*/

/*        public StudentPredmet? RemoveAdress(int id)
        {
            StudentPredmet? studentPredmet = GetAdressById(id);
            if (studentPredmet == null) return null;

            _studsub.Remove(studentPredmet);
            _storage.Save(_studsub);
            return studentPredmet;
        }*/

/*        private StudentPredmet? GetAdressById(int id)
        {
            return _studsub.Find(s => s.StudentPredmetId == id);
        }*/

        public List<StudentPredmet> GetAllStudentSubject()
        {
            return _studsub;
        }
    }
}
