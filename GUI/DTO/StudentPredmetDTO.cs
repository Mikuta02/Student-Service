using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class StudentPredmetDTO : INotifyPropertyChanged
    {
        private string ime { get; set; }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if(ime != value) 
                { 
                ime = value;
                OnPropertyChanged(nameof(ime));
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
                    OnPropertyChanged(nameof(prezime));
                }
            }
        }

        private string indeks { get; set; }
        public string Indeks
        {
            get
            {
                return indeks;
            }
            set
            {
                if (indeks != value)
                {
                    indeks = value;
                    OnPropertyChanged(nameof(indeks));
                }
            }
        }

        private string predmet { get; set; }
        public string Predmet
        {
            get
            {
                return predmet;
            }
            set
            {
                if (predmet != value)
                {
                    predmet = value;
                    OnPropertyChanged(nameof(predmet));
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

        public StudentPredmetDTO()
        {

        }
        public StudentPredmetDTO(Student student, Predmet predmet)
        {
            Ime = student.Ime;
            Prezime = student.Prezime;
            Indeks = student.BrojIndeksa.ToString();
            Predmet = predmet.SifraPredmeta;

        }

    }
}
