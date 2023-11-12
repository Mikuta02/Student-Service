using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
     class ExamGradesDAO
    {
        private readonly List<OcenaNaIspitu> _ocene;
        private readonly Storage<OcenaNaIspitu> _storage;


        public ExamGradesDAO()
        {
            _storage = new Storage<OcenaNaIspitu>("grades.txt");
            _ocene = _storage.Load();
        }

        private int GenerateId()
        {
            if (_ocene.Count == 0) return 0;
            return _ocene[^1].OcenaNaIspituId + 1;
        }

        public OcenaNaIspitu AddExamGrade(OcenaNaIspitu ocena)
        {
            ocena.OcenaNaIspituId = GenerateId();
            _ocene.Add(ocena);
            _storage.Save(_ocene);
            return ocena;
        }
        public OcenaNaIspitu? UpdateExamGrade(OcenaNaIspitu ocena)
        {
            OcenaNaIspitu? oldOcenaNaIspitu = GetGradeById(ocena.OcenaNaIspituId);
            if (oldOcenaNaIspitu is null) return null;

            oldOcenaNaIspitu.StudentId = ocena.StudentId;
            oldOcenaNaIspitu.PredmetId = ocena.PredmetId;
            oldOcenaNaIspitu.Ocena = ocena.Ocena;
            oldOcenaNaIspitu.DatumPolaganja = ocena.DatumPolaganja;

            _storage.Save(_ocene);
            return oldOcenaNaIspitu;
        }
        public OcenaNaIspitu? RemoveExamGrade(int id)
        {
            OcenaNaIspitu? ocena = GetGradeById(id);
            if (ocena == null) return null;

            _ocene.Remove(ocena);
            _storage.Save(_ocene);
            return ocena;
        }
        private OcenaNaIspitu? GetGradeById(int id)
        {
            return _ocene.Find(s => s.OcenaNaIspituId == id);
        }

        public List<OcenaNaIspitu> GetAllGrades()
        {
            return _ocene;
        }
    }
}
