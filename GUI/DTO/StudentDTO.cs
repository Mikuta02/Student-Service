using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged
    {
        public int StudentId { get; set; }
        private string ime { get; set; }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        private string prezime { get; set; }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                if (prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }
        private DateTime datumRodjenja { get; set; }
        public DateTime DatumRodjenja
        {
            get
            {
                return datumRodjenja;
            }
            set
            {
                if (datumRodjenja != value)
                {
                    datumRodjenja = value;
                    OnPropertyChanged("DatumRodjenja");
                }
            }
        }
        //public Adresa Adresa { get; set; }
        private int adresaId { get; set; } 
        public int AdresaId
        {
            get
            {
                return adresaId;
            }
            set
            {
                if (adresaId != value)
                {
                    adresaId = value;
                    OnPropertyChanged("AdresaId");
                }
            }
        }
        private string kontaktTelefon { get; set; }
        public string KontaktTelefon
        {
            get
            {
                return kontaktTelefon;
            }
            set
            {
                if (kontaktTelefon != value)
                {
                    kontaktTelefon = value;
                    OnPropertyChanged("KontaktTelefon");
                }
            }
        }
        private string email { get; set; }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string brojIndeksa { get; set; }
        public string BrojIndeksa
        {
            get
            {
                return brojIndeksa;
            }
            set
            {
                if (brojIndeksa != value)
                {
                    brojIndeksa = value;
                    OnPropertyChanged("BrojIndeksa");
                }
            }
        }
        private int trenutnaGodinaStudija {  get; set; }
        public int TrenutnaGodinaStudija
        {
            get
            {
                return trenutnaGodinaStudija;
            }
            set
            {
                if (trenutnaGodinaStudija != value)
                {
                    trenutnaGodinaStudija = value;
                    OnPropertyChanged("TrenutnaGodinaStudija");
                }
            }
        }
        private string statusStudenta { get; set; }
        public string StatusStudenta
        {
            get
            {
                return statusStudenta;
            }
            set
            {
                if (statusStudenta != value)
                {
                    statusStudenta = value;
                    
                    OnPropertyChanged("StatusStudenta");
                }
            }
        }
        private float prosecnaOcena {  get; set; }
        public float ProsecnaOcena
        {
            get
            {
                return prosecnaOcena;
            }
            set
            {
                if (prosecnaOcena != value)
                {
                    prosecnaOcena = value;
                    OnPropertyChanged("ProsecnaOcena");
                }
            }
        }

        public Student toStudent()
        {
            return new Student(ime, prezime, datumRodjenja, adresaId, kontaktTelefon, email, brojIndeksa, trenutnaGodinaStudija, statusStudenta, prosecnaOcena);
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
