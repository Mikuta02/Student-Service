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

        public Katedra AddKatedra(Katedra katedra)
        {
            katedra.KatedraId = GenerateId();
            _departments.Add(katedra);
            _storage.Save(_departments);
            return katedra;
        }

        public Katedra? UpdateKatedra(Katedra katedra)
        {
            Katedra? oldKatedra = GetKatedraById(katedra.KatedraId);
            if (oldKatedra is null) return null;

            oldKatedra.SifraKatedre = katedra.SifraKatedre;
            oldKatedra.NazivKatedre = katedra.NazivKatedre;
            oldKatedra.Sef = katedra.Sef;

            _storage.Save(_departments);
            return oldKatedra;
        }

        public Katedra? RemoveKatedra(int id)
        {
            Katedra? katedra = GetKatedraById(id);
            if (katedra == null) return null;

            _departments.Remove(katedra);
            _storage.Save(_departments);
            return katedra;
        }

        private Katedra? GetKatedraById(int id)
        {
            return _departments.Find(s => s.KatedraId == id);
        }

        public List<Katedra> GetAllKatedras()
        {
            return _departments;
        }
    }
}
