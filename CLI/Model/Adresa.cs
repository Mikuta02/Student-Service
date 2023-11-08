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
    class Adresa : Serialization.ISerializable
    {

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ULICA: {Ulica}, ");
            sb.Append("BROJ: {Broj}, ");
            sb.Append("GRAD: {Grad}, ");
            sb.Append("DRZAVA: {Drzava}");
            return base.ToString();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Ulica,
            Broj.ToString(),
            Grad,
            Drzava
        };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Ulica = values[0];
            Broj = int.Parse(values[1]);
            Grad = values[2];
            Drzava = values[3];
        }

    }
}
