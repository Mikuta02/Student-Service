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
    class DepartmentsDAO
    {
        private readonly List<Katedra> _departments;
        private readonly Storage<Katedra> _storage;


        public DepartmentsDAO()
        {
            _storage = new Storage<Katedra>("departments.txt");
            _departments = _storage.Load();
        }

        private int GenerateId()
        {
            if (_departments.Count == 0) return 0;
            return _departments[^1].KatedraId + 1;
        }

        public Katedra AddDepartment(Katedra katedra)
        {
            katedra.KatedraId = GenerateId();
            _departments.Add(katedra);
            _storage.Save(_departments);
            return katedra;
        }

        public Katedra? UpdateDepartment(Katedra katedra)
        {
            Katedra? oldKatedra = GetDepartmentById(katedra.KatedraId);
            if (oldKatedra is null) return null;

            oldKatedra.SifraKatedre = katedra.SifraKatedre;
            oldKatedra.NazivKatedre = katedra.NazivKatedre;
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
    }
}
