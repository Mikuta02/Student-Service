using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfesorPredmetKatedraDTO : INotifyPropertyChanged
    {
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
                    OnPropertyChanged(nameof(sifraPredmeta));
                }
            }
        }

        private string nazivPredmeta { get; set; }
        public string NazivPredmeta
        {
            get
            {
                return nazivPredmeta;
            }
            set
            {
                if (nazivPredmeta != value)
                {
                    nazivPredmeta = value;
                    OnPropertyChanged(nameof(nazivPredmeta));
                }
            }
        }

        private string profesor { get; set; }
        public string Profesor
        {
            get
            {
                return profesor;
            }
            set
            {
                if (profesor != value)
                {
                    profesor = value;
                    OnPropertyChanged(nameof(profesor));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ProfesorPredmetKatedraDTO()
        {

        }

        public ProfesorPredmetKatedraDTO(Profesor profesor, Predmet predmet)
        {
            SifraPredmeta = predmet.SifraPredmeta;
            NazivPredmeta = predmet.Naziv;
            Profesor = profesor.Ime + " " + profesor.Prezime;
        }
    }
}
