using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{

    public class Student : Serialization.ISerializable
    {
        public int StudentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Adresa Adresa { get; set; }
        public int AdresaId { get; set; }   
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public Indeks BrojIndeksa { get; set; }
        public int TrenutnaGodinaStudija { get; set; }
        public EnumUt.StatusType StatusStudenta { get; set; }
        public float ProsecnaOcena { get; set; }

        public List<OcenaNaIspitu> SpisakPolozenihIspita { get; set; }
        public List<Predmet> SpisakNepolozenihPredmeta { get; set; }

        public Student(string ime, string prezime, string datumRodjenja, int adresaId, string kontaktTelefon, string email, Indeks brojIndeksa, int trenutnaGodinaStudija, EnumUt.StatusType statusStudenta, float prosecnaOcena)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = DateTime.Parse(datumRodjenja);
            AdresaId = adresaId;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojIndeksa = brojIndeksa;
            TrenutnaGodinaStudija = trenutnaGodinaStudija;
            StatusStudenta = statusStudenta;
            ProsecnaOcena = prosecnaOcena;
            SpisakPolozenihIspita = new List<OcenaNaIspitu>();
            SpisakNepolozenihPredmeta = new List<Predmet>();
        }

        public Student(string ime, string prezime, DateTime datumRodjenja, int adresaId, string kontaktTelefon, string email, Indeks brojIndeksa, int trenutnaGodinaStudija, EnumUt.StatusType statusStudenta, float prosecnaOcena)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaId = adresaId;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojIndeksa = brojIndeksa;
            TrenutnaGodinaStudija = trenutnaGodinaStudija;
            StatusStudenta = statusStudenta;
            ProsecnaOcena = prosecnaOcena;
            SpisakPolozenihIspita = new List<OcenaNaIspitu>();
            SpisakNepolozenihPredmeta = new List<Predmet>();
        }

        public Student(string ime, string prezime, DateTime datumRodjenja, int adresaId, string kontaktTelefon, string email, string brojIndeksa, int trenutnaGodinaStudija, string statusStudenta, float prosecnaOcena)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            AdresaId = adresaId;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojIndeksa = makeIndex(brojIndeksa);
            TrenutnaGodinaStudija = trenutnaGodinaStudija;
            Enum.TryParse(statusStudenta, out EnumUt.StatusType statusStudentaEnum);
            StatusStudenta = statusStudentaEnum;
            ProsecnaOcena = prosecnaOcena;
            SpisakPolozenihIspita = new List<OcenaNaIspitu>();
            SpisakNepolozenihPredmeta = new List<Predmet>();
        }

        public Student()
        {
            SpisakPolozenihIspita = new List<OcenaNaIspitu>();
            SpisakNepolozenihPredmeta = new List<Predmet>();
        }

        public override string? ToString()
        {
            return $"ID {StudentId,2} | Ime {Ime,12} | Prezime {Prezime,12} | Datum Rodjenja {DatumRodjenja.ToString("dd/MM/yyyy"),11} | Adresa {AdresaId,2} | Kontakt {KontaktTelefon,10} | Email {Email,12} | Broj Indeksa {BrojIndeksa,11} | Trenutna Godina {TrenutnaGodinaStudija,2} | Status Studenta {StatusStudenta,2} | Prosecna Ocena {ProsecnaOcena,5} |";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            StudentId.ToString(),
            Ime,
            Prezime,
            DatumRodjenja.ToString("dd/MM/yyyy"),
            AdresaId.ToString(),
            KontaktTelefon,
            Email,
            BrojIndeksa.ToString(),
            TrenutnaGodinaStudija.ToString(),
            StatusStudenta.ToString(),
            ProsecnaOcena.ToString()
        };
            return csvValues;
        }

        private Indeks makeIndex(string input)
        {
            string[] parts = input.Split('/');

            string oznaka = input.Substring(0, 2);
            int upis = int.Parse(parts[0].Substring(3)); 
            int godina = int.Parse(parts[1]);

            Indeks indeks = new Indeks(oznaka, upis, godina);

            return indeks;
        }

        public void FromCSV(string[] values)
        {
            StudentId = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DatumRodjenja = DateTime.Parse(values[3]);
            AdresaId = int.Parse(values[4]);
            KontaktTelefon = values[5];
            Email = values[6];
            BrojIndeksa = makeIndex(values[7]);
            TrenutnaGodinaStudija = int.Parse(values[8]);
            if (values[9] == "S")
                StatusStudenta = EnumUt.StatusType.S;
            else
                StatusStudenta = EnumUt.StatusType.B;
            ProsecnaOcena = float.Parse(values[10]);
        }
    }
}
