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
    class SubjectsDAO
    {
        private readonly List<Predmet> _subjects;
        private readonly Storage<Predmet> _storage;


        public SubjectsDAO()
        {
            _storage = new Storage<Predmet>("predmet.txt");
            _subjects = _storage.Load();
        }

        private int GenerateId()
        {
            if (_subjects.Count == 0) return 0;
            return _subjects[^1].PredmetId + 1;
        }

        public Predmet AddPredmet(Predmet predmet)
        {
            predmet.PredmetId = GenerateId();
            _subjects.Add(predmet);
            _storage.Save(_subjects);

            UveziSaProfesorom(predmet);

            return predmet;
        }

       public void UveziSaProfesorom(Predmet predmet)
        {
            List<Profesor> _professors;
            ProfessorsDAO professorsDAO = new ProfessorsDAO();
            _professors = professorsDAO.GetAllProfessors();

            foreach (Predmet s in _subjects)
            {
                Profesor? profesor = _professors.Find(p => p.ProfesorId == s.ProfesorID);//professorsDAO.GetProfessorById(s.ProfesorID);
                profesor.Predmeti.Add(s);
                s.ProfesorPredmeta = profesor;
            }

/*           foreach (Predmet s in _subjects)
            {
            Profesor profesor = _professors.Find(p => p.ProfesorId == s.ProfesorID);
                profesor.Predmeti.Add(s);
                s.ProfesorPredmeta = profesor;
            }*/
        }

        public Predmet? UpdatePredmet(Predmet predmet)
        {
            Predmet? oldPredmet = GetPredmetById(predmet.PredmetId);
            if (oldPredmet is null) return null;

            oldPredmet.SifraPredmeta = predmet.SifraPredmeta;
            oldPredmet.Naziv = predmet.Naziv;
            //oldPredmet.Semestar = predmet.Semestar;
            oldPredmet.GodinaStudija = predmet.GodinaStudija;
            oldPredmet.ProfesorID = predmet.ProfesorID;
            oldPredmet.ESPB = predmet.ESPB;

            _storage.Save(_subjects);
            return oldPredmet;
        }

        public Predmet? RemovePredmet(int id)
        {
            Predmet? predmet = GetPredmetById(id);
            if (predmet == null) return null;

            _subjects.Remove(predmet);
            _storage.Save(_subjects);
            return predmet;
        }

        private Predmet? GetPredmetById(int id)
        {
            return _subjects.Find(s => s.PredmetId == id);
        }

        public List<Predmet> GetAllPredmets()
        {
            return _subjects;
        }
    }
}
