using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    internal class Adresa
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
    }
}
