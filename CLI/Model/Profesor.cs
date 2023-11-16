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
        public int ProfesorId {  get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Adresa Adresa { get; set; }
        public int AdresaId {  get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public int BrojLicneKarte { get; set; }
        public string Zvanje { get; set; }
        public int GodineStaza { get; set; }
        public List<Predmet> Predmeti { get; set; }

        public Profesor(string ime, string prezime, string datumRodjenja, int adresaId, string kontaktTelefon, string email, int brojLicneKarte, string zvanje, int godineStaza)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = DateTime.Parse(datumRodjenja);
            AdresaId = adresaId;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojLicneKarte = brojLicneKarte;
            Zvanje = zvanje;
            GodineStaza = godineStaza;
            Predmeti = new List<Predmet>();
        }

        public void addPredmet(Predmet predmet)
        {
            Predmeti.Add(predmet);
        }

        public override string? ToString()
        {
            return $"ID {ProfesorId,2} | Ime {Ime,12} | Prezime {Prezime,12} | Datum Rodjenja {DatumRodjenja.ToString("dd/MM/yyyy"),11} | Adresa {AdresaId,2} | Kontakt {KontaktTelefon,10} | Email {Email,20} | Broj Licne {BrojLicneKarte,7} | Zvanje {Zvanje,8} | Godina Staza {GodineStaza,3} |";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            ProfesorId.ToString(),
            Ime,
            Prezime,
            DatumRodjenja.ToString("dd/MM/yyyy"),
            AdresaId.ToString(),
            KontaktTelefon,
            Email,
            BrojLicneKarte.ToString(),
            Zvanje,
            GodineStaza.ToString()
        };
            return csvValues;
        }

        public Profesor()
        {
            Predmeti = new List<Predmet>();
        }

        public void FromCSV(string[] values)
        {
            ProfesorId = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.Parse(values[3]); 
            AdresaId = int.Parse(values[4]);
            KontaktTelefon = values[5];
            Email = values[6];
            BrojLicneKarte = int.Parse(values[7]);
            Zvanje = values[8];
            GodineStaza = int.Parse(values[9]);
        }
    }
}
