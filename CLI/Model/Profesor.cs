using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    class Profesor : Serialization.ISerializable
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

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Ime,
            Prezime,
            DatumRodjenja,
            Adresa,
            KontaktTelefon,
            Email,
            BrojLicneKarte.ToString(),
            Zvanje,
            GodineStaza.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Ime = values[0];
            Prezime = values[1];
            DatumRodjenja = values[2];
            Adresa = values[3];
            KontaktTelefon = values[4];
            Email = values[5];
            BrojLicneKarte = int.Parse(values[6]);
            Zvanje = values[7];
            GodineStaza = int.Parse(values[8]);
        }
    }
}
