using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
    class ProfessorsDAO
    {
        private readonly List<Profesor> _profesors;
        private readonly Storage<Profesor> _storage;

        public ProfessorsDAO()
        {
            _storage = new Storage<Profesor>("profesors.txt");
            _profesors = _storage.Load();
        }

        private int GenerateId()
        {
            if (_profesors.Count == 0) return 0;
            return _profesors[^1].ProfesorId + 1;
        }

        public Profesor AddProfessor(Profesor profesor)
        {
            profesor.ProfesorId = GenerateId();
            _profesors.Add(profesor);
            _storage.Save(_profesors);
            return profesor;
        }

        public Profesor? UpdateProfessor(Profesor profesor)
        {
            Profesor? oldProfesor = GetProfessorById(profesor.ProfesorId);
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
        public Profesor? RemoveProfessor(int id)
        {
            Profesor? profesor = GetProfessorById(id);
            if (profesor == null) return null;

            _profesors.Remove(profesor);
            _storage.Save(_profesors);
            return profesor;
        }
        private Profesor? GetProfessorById(int id)
        {
            return _profesors.Find(s => s.ProfesorId == id);
        }
        public List<Profesor> GetAllProfessors()
        {
            return _profesors;
        }


    }   
}
