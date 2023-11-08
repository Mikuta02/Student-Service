using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{

    public class Student
    {

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string BrojIndeksa { get; set; }
        public int TrenutnaGodinaStudija { get; set; }
        public EnumUt.StatusType StatusStudenta { get; set; }
        public float ProsecnaOcena { get; set; }
        List<OcenaNaIspitu> SpisakPolozenihIspita { get; set; }
        List<OcenaNaIspitu> SpisakNepolozenihIspita { get; set; }

        public Student(string ime, string prezime, string datumRodjenja, string adresa, string kontaktTelefon, string email, string brojIndeksa, int trenutnaGodinaStudija, EnumUt statusStudenta, float prosecnaOcena)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Adresa = adresa;
            KontaktTelefon = kontaktTelefon;
            Email = email;
            BrojIndeksa = brojIndeksa;
            TrenutnaGodinaStudija = trenutnaGodinaStudija;
            StatusStudenta = statusStudenta;
            ProsecnaOcena = prosecnaOcena;
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
            sb.Append("SPISAK POLOZENIH ISPITA: ");
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
    }
}
