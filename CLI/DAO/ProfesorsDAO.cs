using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
    class ProfesorsDAO
    {
        private readonly List<Profesor> _profesors;
        private readonly Storage<Profesor> _storage;

        public ProfesorsDAO()
        {
            _storage = new Storage<Profesor>("profesors.txt");
            _profesors = _storage.Load();
        }

        private int GenerateId()
        {
            if (_profesors.Count == 0) return 0;
            return _profesors[^1].ProfesorId + 1;
        }

        public Profesor AddProfesor(Profesor profesor)
        {
            profesor.ProfesorId = GenerateId();
            _profesors.Add(profesor);
            _storage.Save(_profesors);
            return profesor;
        }

        public Profesor? UpdateProfesor(Profesor profesor)
        {
            Profesor? oldProfesor = GetProfesorById(profesor.ProfesorId);
            if (oldProfesor is null) return null;

            oldProfesor.Ime = profesor.Ime;
            oldProfesor.Prezime = profesor.Prezime;
            oldProfesor.DatumRodjenja = profesor.DatumRodjenja;
            oldProfesor.Adresa = profesor.Adresa;
            oldProfesor.KontaktTelefon = profesor.KontaktTelefon;
            oldProfesor.Email = profesor.Email;
            oldProfesor.BrojLicneKarte = profesor.BrojLicneKarte;
            oldProfesor.Zvanje = profesor.Zvanje;
            oldProfesor.GodineStaza = profesor.GodineStaza;

            _storage.Save(_profesors);
            return oldProfesor;
        }
        public Profesor? RemoveProfesor(int id)
        {
            Profesor? profesor = GetProfesorById(id);
            if (profesor == null) return null;

            _profesors.Remove(profesor);
            _storage.Save(_profesors);
            return profesor;
        }
        private Profesor? GetProfesorById(int id)
        {
            return _profesors.Find(s => s.ProfesorId == id);
        }
        public List<Profesor> GetAllProfesors()
        {
            return _profesors;
        }


    }   
}
