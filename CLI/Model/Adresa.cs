using CLI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    public class Adresa : Serialization.ISerializable
    {
        public int AdresaId { get; set; }   
        public string Ulica {  get; set; }
        public int Broj { get; set; }
        public string Grad {  get; set; }
        public string Drzava { get; set; }

        public Adresa(string ulica, int broj, string grad, string drzava)
        {
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;
        }

        public Adresa() { }

        public override string ToString()
        {
            return $"ID {AdresaId,2} | Ulica {Ulica,12} | Broj {Broj,4} | Grad {Grad,11} | Drzava {Drzava,13}";
        }

        public string ToStringConsole()
        {
            return $"{Ulica} {Broj}, {Grad}, {Drzava}";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            AdresaId.ToString(),
            Ulica,
            Broj.ToString(),
            Grad,
            Drzava
        };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            AdresaId = int.Parse(values[0]);
            Ulica = values[1];
            Broj = int.Parse(values[2]);
            Grad = values[3];
            Drzava = values[4];
        }

    }
}
