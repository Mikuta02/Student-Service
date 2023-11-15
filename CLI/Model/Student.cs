using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{

    class Student : Serialization.ISerializable
    {
        public int StudentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public Indeks BrojIndeksa { get; set; }
        public int TrenutnaGodinaStudija { get; set; }
        public EnumUt.StatusType StatusStudenta { get; set; }
        public float ProsecnaOcena { get; set; }

        List<OcenaNaIspitu> SpisakPolozenihIspita { get; set; }
        List<OcenaNaIspitu> SpisakNepolozenihIspita { get; set; }

        public Student(string ime, string prezime, string datumRodjenja, string adresa, string kontaktTelefon, string email, Indeks brojIndeksa, int trenutnaGodinaStudija, EnumUt.StatusType statusStudenta, float prosecnaOcena)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = DateTime.Parse(datumRodjenja);
            Adresa = adresa;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojIndeksa = brojIndeksa;
            TrenutnaGodinaStudija = trenutnaGodinaStudija;
            StatusStudenta = statusStudenta;
            ProsecnaOcena = prosecnaOcena;
            SpisakPolozenihIspita = new List<OcenaNaIspitu>();
            SpisakNepolozenihIspita = new List<OcenaNaIspitu>();
        }

        public Student()
        {
            SpisakPolozenihIspita = new List<OcenaNaIspitu>();
            SpisakNepolozenihIspita = new List<OcenaNaIspitu>();
        }

        public override string? ToString()
        {
            return $"ID {StudentId,2} | Ime {Ime,12} | Prezime {Prezime,12} | Datum Rodjenja {DatumRodjenja.ToString("dd/MM/yyyy"),11} | Adresa {Adresa,12} | Kontakt {KontaktTelefon,10} | Email {Email,12} | Broj Indeksa {BrojIndeksa,11} | Trenutna Godina {TrenutnaGodinaStudija,2} | Status Studenta {StatusStudenta,2} | Prosecna Ocena {ProsecnaOcena,5} |";
        }



        /* public override string ToString()
         {
             StringBuilder sb = new StringBuilder();
             sb.Append("IME: " + Ime + ", ");
             sb.Append("PREZIME: " + Prezime + ", ");
             sb.Append("DATUM RODJENJA: " + DatumRodjenja + ", ");
             sb.Append("ADRESA: " + Adresa + ", ");
             sb.Append("KONTAKT TELEFON: " + KontaktTelefon + ", ");
             sb.Append("EMAIL: " + Email + ", ");
             sb.Append("BROJ INDEKSA: " + BrojIndeksa + ", ");
             sb.Append("TRENUTNA GODINA STUDIJA: " + TrenutnaGodinaStudija + ", ");
             if (StatusStudenta.Equals(EnumUt.StatusType.S))
             {
                 sb.Append("STATUS STUDENTA: " + "Samofinansiranje" + ", ");
             }
             else
             {
                 sb.Append("STATUS STUDENTA: " + "Budzet" + ", ");
             }
             sb.Append("PROSECNA OCENA: " + ProsecnaOcena + ", ");
 /*            sb.Append("SPISAK POLOZENIH ISPITA: ");
             foreach (OcenaNaIspitu oni in SpisakPolozenihIspita)
             {
                 sb.Append($"{oni.PredmetStudenta.Naziv}, {oni.Ocena}");
             }
             sb.Append("SPISAK NEPOLOZENIH ISPITA: ");
             foreach (OcenaNaIspitu oni in SpisakPolozenihIspita)
             {
                 sb.Append($"{oni.PredmetStudenta.Naziv}, {oni.Ocena}");
             }
             return sb.ToString();
         }
     */

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            StudentId.ToString(),
            Ime,
            Prezime,
            DatumRodjenja.ToString("dd/MM/yyyy"),
            Adresa,
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
            Adresa = values[4];
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
