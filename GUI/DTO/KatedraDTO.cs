using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class KatedraDTO : INotifyPropertyChanged
    {
        public int katedraId { get; set; }

        private string sifraKatedre { get; set; }
        public string SifraKatedre
        {
            get
            {
                return sifraKatedre;
            }
            set
            {
                if (sifraKatedre != value)
                {
                    sifraKatedre = value;
                    OnPropertyChanged("SifraKatedre");
                }
            }
        }

        private string nazivKatedre { get; set; }
        public string NazivKatedre
        {
            get
            {
                return nazivKatedre;
            }
            set
            {
                if (nazivKatedre != value)
                {
                    nazivKatedre = value;
                    OnPropertyChanged("NazivKatedre");
                }
            }
        }

        private int sefId { get; set; }
        public int SefId
        {
            get
            {
                return sefId;
            }
            set
            {
                if (sefId != value)
                {
                    sefId = value;
                    OnPropertyChanged("SefId");
                }
            }
        }

        private string sef { get; set; }
        public string Sef
        {
            get
            {
                return sef;
            }
            set
            {
                if (sef != value)
                {
                    sef = value;
                    OnPropertyChanged("Sef");
                }
            }
        }

        public List<int> spisakIDProfesora { get; set; }

        public KatedraDTO()
        {
            spisakIDProfesora = new List<int>();
        }

        public Katedra toKatedra()
        {
            return new Katedra(sifraKatedre, nazivKatedre, sefId);
        }

        public KatedraDTO(Katedra katedra)
        {
            katedraId = katedra.KatedraId;
            sifraKatedre = katedra.SifraKatedre;
            nazivKatedre = katedra.NazivKatedre;
            sefId = katedra.SefId;

            if(katedra.Sef == null || katedra.Sef.ProfesorId == -1)
            {
                sef = "N/A";
            }
            else
            {
                sef = katedra.Sef.Ime + " " + katedra.Sef.Prezime;
            }

            spisakIDProfesora = new List<int>();

            if (katedra.Profesori.Any())
            {
                foreach (Profesor p in katedra.Profesori)
                {
                    if (!spisakIDProfesora.Contains(p.ProfesorId))
                    {
                        spisakIDProfesora.Add(p.ProfesorId);
                    }
                }
            }
        }

        public KatedraDTO clone()
        {
            KatedraDTO k = new KatedraDTO();

            k.katedraId = this.katedraId;
            k.sefId = this.sefId;
            k.sifraKatedre = this.sifraKatedre;
            k.nazivKatedre = this.nazivKatedre;
            k.sefId = this.sefId;
            k.spisakIDProfesora = this.spisakIDProfesora;
            k.sef = this.sef;

            return k;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
