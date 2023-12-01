using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    class DepartmentDAO
    {
        private readonly List<Katedra> _departments;
        private readonly Storage<Katedra> _storage;


        public DepartmentDAO()
        {
            _storage = new Storage<Katedra>("departments.txt");
            _departments = _storage.Load();
        }

        private int GenerateId()
        {
            if (_departments.Count == 0) return 0;
            return _departments[^1].KatedraId + 1;
        }

        public Katedra? AddDepartment(Katedra katedra)
        {
            katedra.KatedraId = GenerateId();
            if (!AddHeadOfDepartment(katedra)) return null;
            _departments.Add(katedra);
            _storage.Save(_departments);
            return katedra;
        }

        public bool AddHeadOfDepartment(Katedra katedra)
        {
            ProfessorDAO professorsDAO = new ProfessorDAO();
            List<Profesor>  _professors = professorsDAO.GetAllProfessors();


            Profesor? sef = _professors.Find(p => p.ProfesorId == katedra.SefId);
            if (sef != null)
            {
                katedra.Sef = sef;
                return true;
            }
            else
            {
                System.Console.WriteLine("Error: Professor not found");
                return false;
            }
           
        }

        public bool AddProfessorToDepartment(int profID, int depID)
        {
            ProfessorDAO professorsDAO = new ProfessorDAO();
            List<Profesor> _professors = professorsDAO.GetAllProfessors();

            Profesor? profesor = _professors.Find(p => p.ProfesorId == profID);
            Katedra? katedra = _departments.Find(p => p.KatedraId == depID);
            if (profesor != null && katedra != null)
            {
                if (katedra.Profesori.Find(p => p.ProfesorId == profID) == null)
                {
                    katedra.Profesori.Add(profesor);
                    _storage.Save(_departments);
                    return true;
                }            
            }
            
            return false;
        }

        public bool RemoveProfessorFromDepartment(int profID, int depID)
        {
            Katedra? katedra = _departments.Find(p => p.KatedraId == depID);
            if (katedra != null)
            {
                int indexToRemove = katedra.Profesori.FindIndex(ss => ss.ProfesorId == profID);
                if (indexToRemove == -1) return false;

                katedra.Profesori.RemoveAt(indexToRemove);
                _storage.Save(_departments);
            }
            return true;
        }

        public Katedra? UpdateDepartment(Katedra katedra)
        {
            Katedra? oldKatedra = GetDepartmentById(katedra.KatedraId);
            if (oldKatedra is null) return null;
            if (!AddHeadOfDepartment(katedra)) return null;

            oldKatedra.SifraKatedre = katedra.SifraKatedre;
            oldKatedra.NazivKatedre = katedra.NazivKatedre;
            oldKatedra.SefId = katedra.SefId;
            oldKatedra.Sef = katedra.Sef;
            
            _storage.Save(_departments);
            return oldKatedra;
        }

        public Katedra? RemoveDepartment(int id)
        {
            Katedra? katedra = GetDepartmentById(id);
            if (katedra == null) return null;

            _departments.Remove(katedra);
            _storage.Save(_departments);
            return katedra;
        }

        private Katedra? GetDepartmentById(int id)
        {
            return _departments.Find(s => s.KatedraId == id);
        }

        public List<Katedra> GetAllDepartments()
        {
            return _departments;
        }

        internal bool ShowProfessors(int depID)
        {
            Katedra? katedra = GetDepartmentById(depID);
            if (katedra != null)
            {
                int i = 0;
                foreach (Profesor p in katedra.Profesori)
                {
                    System.Console.WriteLine($"Profesor[{i}]: {p}");
                    i++;
                }
                return true;
            }
            return false;
        }

        internal void fillObjectsAndLists(ProfessorDAO profesDao)
        {
            List<Profesor> professors = profesDao.GetAllProfessors();

            foreach(Katedra dep in _departments)
            {
                Profesor? sef = professors.Find(p => p.ProfesorId == dep.SefId);
                if (sef == null) continue;
                dep.Sef = sef;
            }
        }
    }
}
