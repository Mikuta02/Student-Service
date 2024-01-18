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
        private double prosecnaOcena {  get; set; }
        public double ProsecnaOcena
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

        public List<int> spisakIDNepolozenihPredmeta { get; set; }
        public List<int> spisakIDPolozenihPredmeta { get; set; }
        public List<int> spisakIDOcena { get; set; }

        public Student toStudent()
        {
            return new Student(ime, prezime, datumRodjenja, adresaId, kontaktTelefon, email, brojIndeksa, trenutnaGodinaStudija, statusStudenta);
        }

        public StudentDTO()
        {
            spisakIDNepolozenihPredmeta = new List<int>();
            spisakIDPolozenihPredmeta = new List<int>();
            spisakIDOcena = new List<int>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public StudentDTO(Student student)
        {
            StudentId = student.StudentId;
            ime = student.Ime;
            prezime = student.Prezime;
            datumRodjenja = student.DatumRodjenja;
            adresaId = student.AdresaId;
            kontaktTelefon = student.KontaktTelefon;
            email = student.Email;
            brojIndeksa = student.BrojIndeksa.ToString();
            trenutnaGodinaStudija = student.TrenutnaGodinaStudija;
            statusStudenta = student.StatusStudenta.ToString();

            spisakIDNepolozenihPredmeta = new List<int>();
            spisakIDPolozenihPredmeta = new List<int>();
            spisakIDOcena = new List<int>();

            if (student.SpisakNepolozenihPredmeta.Any())
            {
                foreach (Predmet p in student.SpisakNepolozenihPredmeta)
                {
                    if (!spisakIDNepolozenihPredmeta.Contains(p.PredmetId))
                    {
                        spisakIDNepolozenihPredmeta.Add(p.PredmetId);
                    }
                }
            }

            if (student.SpisakPolozenihIspita.Any())
            {
                foreach (OcenaNaIspitu oi in student.SpisakPolozenihIspita)
                {
                    if (!spisakIDPolozenihPredmeta.Contains(oi.PredmetId))
                    {
                        spisakIDPolozenihPredmeta.Add(oi.PredmetId);
                    }
                    if (!spisakIDOcena.Contains(oi.OcenaNaIspituId))
                    {
                        spisakIDOcena.Add(oi.OcenaNaIspituId);
                    }
                }
            }

            double sum = 0;
            int count = 0;
            if (student.SpisakPolozenihIspita.Any())
            {
                foreach (OcenaNaIspitu oi in student.SpisakPolozenihIspita)
                {
                    ++count;
                    sum += oi.Ocena;
                }
            }
            prosecnaOcena = sum/count;

        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public StudentDTO Clone()
        {
            StudentDTO student = new StudentDTO();

            student.StudentId = this.StudentId;
            student.ime = this.Ime;
            student.prezime = this.Prezime;
            student.datumRodjenja = this.DatumRodjenja;
            student.adresaId = this.AdresaId;
            student.kontaktTelefon = this.KontaktTelefon;
            student.email = this.Email;
            student.brojIndeksa = this.BrojIndeksa;
            student.trenutnaGodinaStudija = this.TrenutnaGodinaStudija;
            student.statusStudenta = this.StatusStudenta;
            student.prosecnaOcena = this.ProsecnaOcena;
            student.spisakIDNepolozenihPredmeta = this.spisakIDNepolozenihPredmeta;
            student.spisakIDPolozenihPredmeta = this.spisakIDPolozenihPredmeta;
            student.spisakIDOcena = this.spisakIDOcena;

            return student;
        }
    }
}
