using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class PredmetDTO : INotifyPropertyChanged
    {
        public int PredmetId { get; set; } //dodati get set zbog edita
        private string sifraPredmeta { get; set; }
        public string SifraPredmeta
        {
            get
            {
                return sifraPredmeta;
            }
            set
            {
                if (sifraPredmeta != value)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged("SifraPredmeta");
                }
            }
        }
        private string naziv { get; set; }
        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }
        private string semestar { get; set; }
        public string Semestar
        {
            get
            {
                return semestar;
            }
            set
            {
                if (semestar != value)
                {
                    semestar = value;
                    OnPropertyChanged("Semestar");
                }
            }
        }
        private int godinaStudija { get; set; }
        public int GodinaStudija
        {
            get
            {
                return godinaStudija;
            }
            set
            {
                if (godinaStudija != value)
                {
                    godinaStudija = value;
                    OnPropertyChanged("GodinaStudija");
                }
            }
        }
        private int profesorID { get; set; }
        public int ProfesorID
        {
            get
            {
                return profesorID;
            }
            set
            {
                if (profesorID != value)
                {
                    profesorID = value;
                    OnPropertyChanged("ProfesorID");
                }
            }
        }
        private int espb { get; set; }
        public int ESPB
        {
            get
            {
                return espb;
            }
            set
            {
                if (espb != value)
                {
                    espb = value;
                    OnPropertyChanged("ESPB");
                }
            }
        }

        private string imeProfesoraKojiPredaje { get; set; }
        public string ImeProfesoraKojiPredaje
        {
            get
            {
                return imeProfesoraKojiPredaje;
            }
            set
            {
                if (imeProfesoraKojiPredaje != value)
                {
                    imeProfesoraKojiPredaje = value;
                    OnPropertyChanged("ImeProfesoraKojiPredaje");
                }
            }
        }

        public List<int> spisakIDStudenataPolozili { get; set; }
        public List<int> spisakIDStudenataNepolozili { get; set; }

        public Predmet toPredmet()
        {
            return new Predmet(sifraPredmeta, naziv, semestar, godinaStudija, profesorID, espb);
        }

        public PredmetDTO()
        {
            spisakIDStudenataPolozili = new List<int>();
            spisakIDStudenataNepolozili = new List<int>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        public PredmetDTO(Predmet predmet)
        {
            PredmetId = predmet.PredmetId;
            sifraPredmeta = predmet.SifraPredmeta;
            naziv = predmet.Naziv;
            semestar = predmet.Semestar.ToString();
            godinaStudija = predmet.GodinaStudija;
            profesorID = predmet.ProfesorID;
            espb = predmet.ESPB;

            if (predmet.ProfesorID == -1 || predmet.ProfesorPredmeta == null)
            {
                imeProfesoraKojiPredaje = "N/A";
            }
            else
            {
                imeProfesoraKojiPredaje = predmet.ProfesorPredmeta.Ime + " " + predmet.ProfesorPredmeta.Prezime;
            }

            spisakIDStudenataPolozili = new List<int>();
            spisakIDStudenataNepolozili = new List<int>();

            if (predmet.StudentiPolozili.Any())
            {
                foreach (Student s in predmet.StudentiPolozili)
                {
                    if (!spisakIDStudenataPolozili.Contains(s.StudentId))
                    {
                        spisakIDStudenataPolozili.Add(s.StudentId);
                    }
                }
            }

            if (predmet.StudentiNepolozili.Any())
            {
                foreach (Student s in predmet.StudentiNepolozili)
                {
                    if (!spisakIDStudenataNepolozili.Contains(s.StudentId))
                    {
                        spisakIDStudenataNepolozili.Add(s.StudentId);
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public PredmetDTO Clone()
        {
            PredmetDTO predmet = new PredmetDTO();

            predmet.PredmetId = this.PredmetId;
            predmet.sifraPredmeta = this.SifraPredmeta;
            predmet.naziv = this.Naziv;
            predmet.semestar = this.Semestar;
            predmet.godinaStudija = this.GodinaStudija;
            predmet.profesorID = this.ProfesorID;
            predmet.imeProfesoraKojiPredaje = this.imeProfesoraKojiPredaje;
            predmet.espb = this.ESPB;
            predmet.spisakIDStudenataPolozili = this.spisakIDStudenataPolozili;
            predmet.spisakIDStudenataNepolozili = this.spisakIDStudenataNepolozili;

            return predmet;
        }
    }
}
