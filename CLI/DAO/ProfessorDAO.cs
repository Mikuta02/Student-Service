using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
    class ProfessorDAO
    {
        private readonly List<Profesor> _profesors;
        private readonly Storage<Profesor> _storage;

        public ProfessorDAO()
        {
            _storage = new Storage<Profesor>("profesors.txt");
            _profesors = _storage.Load();
        }

        private int GenerateId()
        {
            if (_profesors.Count == 0) return 0;
            return _profesors[^1].ProfesorId + 1;
        }

        public Profesor? AddProfessor(Profesor profesor)
        {
            profesor.ProfesorId = GenerateId();

            Adresa? adresa = AddAdresaToProfessor(profesor);
            if (adresa == null)
            {
                return null;
            }
            profesor.Adresa = adresa;

            _profesors.Add(profesor);
            _storage.Save(_profesors);
            return profesor;
        }

        private Adresa? AddAdresaToProfessor(Profesor profesor)
        {
            AdressDAO adressDAO = new AdressDAO();
            List<Adresa> adresses = adressDAO.GetAllAdress();

            Adresa? adresa = adresses.Find(p => p.AdresaId == profesor.AdresaId);
            if (adresa != null)
            {
                return adresa;
            }
            else
            {
                System.Console.WriteLine("Error: Address not found");
                return null;
            }

        }

        public Profesor? UpdateProfessor(Profesor profesor)
        {
            Profesor? oldProfesor = GetProfessorById(profesor.ProfesorId);
            if (oldProfesor is null) return null;

            Adresa? adresa = AddAdresaToProfessor(profesor);
            if (adresa == null)
            {
                return null;
            }

            oldProfesor.Ime = profesor.Ime;
            oldProfesor.Prezime = profesor.Prezime;
            oldProfesor.DatumRodjenja = profesor.DatumRodjenja;
            oldProfesor.Adresa = adresa;
            oldProfesor.AdresaId = profesor.AdresaId;
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

/*        public bool ShowSubjects(int profID)
        {
            Profesor? profesor = GetProfessorById(profID);
            if (profesor != null)
            {
                int i = 0;
                foreach (Predmet p in profesor.Predmeti)
                {
                    System.Console.WriteLine($"Predmet[{i}]: {p}");
                    i++;
                }
                System.Console.WriteLine($"test!!!!!!!!!!!1\n\n\n!!!!!!!!!!!!!!!!!");
                return true;
            }
            return false;
        }*/

        public List<Profesor> GetAllProfessors()
        {
            return _profesors;
        }

        internal void fillObjectsAndLists(SubjectDAO subjectsDao, AdressDAO addressesDao)
        {
            List<Predmet> subjects = subjectsDao.GetAllPredmets();
            List<Adresa> adresses = addressesDao.GetAllAdress();

            //adresa
            foreach (Profesor prof in _profesors)
            {
                Adresa? adresa = adresses.Find(p => p.AdresaId == prof.AdresaId);
                if(adresa == null) continue;
                prof.Adresa = adresa;
            }

            //predmeti
            foreach(Predmet predmet in subjects)
            {
                Profesor? profesor = GetProfessorById(predmet.ProfesorID);
                if(profesor == null) continue;
                profesor.Predmeti.Add(predmet);
            }
        }
    }   
}
