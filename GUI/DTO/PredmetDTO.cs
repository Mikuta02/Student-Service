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
        public string  SifraPredmeta
        {
            get
            {
                return sifraPredmeta;
            }
            set
            {
                if(sifraPredmeta != value)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged("SifraPredmeta");
                }
            }
        }
        private string naziv {  get; set; }
        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if(naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }
        private string semestar {  get; set; }
        public string Semestar
        {
            get
            {
                return semestar;
            }
            set
            {
                if(semestar != value)
                {
                    semestar = value;
                    OnPropertyChanged("Semestar");
                }
            }
        }
        private int godinaStudija {  get; set; }
        public int GodinaStudija
        {
            get
            {
                return godinaStudija;
            }
            set
            {
                if(godinaStudija != value)
                {
                    godinaStudija = value;
                    OnPropertyChanged("GodinaStudija");
                }
            }
        }
        private int profesorID {  get; set; }
        public int ProfesorID
        {
            get
            {
                return profesorID;
            }
            set
            {
                if(profesorID != value)
                {
                    profesorID = value;
                    OnPropertyChanged("ProfesorID");
                }
            }
        }
        private int espb {  get; set; }
        public int ESPB
        {
            get
            {
                return espb;
            }
            set
            {
                if(espb != value)
                {
                    espb = value;
                    OnPropertyChanged("ESPB");
                }
            }
        }

        public Predmet toPredmet()
        {
            return new Predmet(sifraPredmeta, naziv, semestar, godinaStudija, profesorID, espb);
        }

        public PredmetDTO()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;


        public PredmetDTO(Predmet predmet)
        {
            PredmetId = this.PredmetId;
            sifraPredmeta = this.SifraPredmeta;
            naziv = this.Naziv;
            semestar = this.Semestar;
            godinaStudija = this.GodinaStudija;
            profesorID=this.ProfesorID;
            espb = this.ESPB;


        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public PredmetDTO clone()
        {
            return new PredmetDTO
            {
                SifraPredmeta = this.sifraPredmeta,
                Naziv = this.Naziv,
                Semestar = this.Semestar,
                GodinaStudija = this.GodinaStudija,
                ProfesorID = this.ProfesorID,
                ESPB = this.ESPB,
            };
        }
    }
}
