using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    internal class Profesor
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public int BrojLicneKarte { get; set; }
        public string Zvanje { get; set; }
        public int GodineStaza { get; set; }
        public List<Predmet> Predmeti { get; set; }

        public Profesor(string ime, string prezime, string datumRodjenja, string adresa, string kontaktTelefon, string email, int brojLicneKarte, string zvanje, int godineStaza)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Adresa = adresa;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojLicneKarte = brojLicneKarte;
            Zvanje = zvanje;
            GodineStaza = godineStaza;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IME: " + Ime + ", ");
            sb.Append("PREZIME: " + Prezime + ", ");
            sb.Append("DATUM RODJENJA: " + DatumRodjenja + ", ");
            sb.Append("ADRESA: " + Adresa + ", ");
            sb.Append("KONTAKT TELEFON: " + KontaktTelefon + ", ");
            sb.Append("EMAIL: " + Email + ", ");
            sb.Append("BROJ LICNE KARTE: " + BrojLicneKarte + ", ");
            sb.Append("ZVANJE: " + Zvanje + ", ");
            sb.Append("GODINE STAZA: " + GodineStaza + ", ");
            sb.Append("SUBJECTS: ");
            sb.AppendJoin(", ", Predmeti.Select(predmet => predmet.Naziv));
            // ili
            // foreach (Subject s in Subjects)
            // {
            //     sb.Append($"{s.Name}, ");
            // }
            return sb.ToString();
        }
    }
}
