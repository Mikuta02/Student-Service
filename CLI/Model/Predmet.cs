using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    class Predmet : Serialization.ISerializable
    {
        public string SifraPredmeta { get; set; }
        public string Naziv { get; set; }
        public EnumUt.SemestarType Semestar { get; set; }
        public string GodinaStudija { get; set; }
        public Profesor ProfesorPredmeta { get; set; }
        public int ProfesorID { get; set; } 
        public int ESPB { get; set; }
        public List<Student> StudentiPolozili { get; set; }
        public List<Student> StudentiNepolozili { get; set; }

        public Predmet(string sifraPredmeta, string naziv, EnumUt.SemestarType semestar, string godinaStudija, Profesor profesorPredmeta, int profesorId, int eSPB)
        {
            SifraPredmeta = sifraPredmeta;
            Naziv = naziv;
            Semestar = semestar;
            GodinaStudija = godinaStudija;
            ProfesorPredmeta = profesorPredmeta;
            ProfesorID = profesorId;
            ESPB = eSPB;
        }

        public override string? ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SIFRA PREDMETA: " + SifraPredmeta + ", ");
            sb.Append("NAZIV: " + Naziv + ", ");
            if (Semestar.Equals(EnumUt.SemestarType.Letnji))
            {
                sb.Append("STATUS STUDENTA: " + "Letnji" + ", ");
            }
            else
            {
                sb.Append("STATUS STUDENTA: " + "Zimski" + ", ");
            }
            sb.Append("PROFESOR: " + ProfesorPredmeta.Ime + " " + ProfesorPredmeta.Prezime + ", ");
            sb.Append("ESPB: " + ESPB + ", ");
            sb.Append("SPISAK STUDENATA KOJI SU POLOZILI: ");
            foreach (Student s in StudentiPolozili)
            {
                sb.Append($"{s.Ime}, {s.Prezime}");
            }
            sb.Append("SPISAK STUDENATA KOJI SU NISU POLOZILI: ");
            foreach (Student s in StudentiNepolozili)
            {
                sb.Append($"{s.Ime}, {s.Prezime}");
            }
            return sb.ToString();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
             SifraPredmeta,
             Naziv,
             Semestar.ToString(),
             GodinaStudija,
             ESPB.ToString(),
             ProfesorID.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            SifraPredmeta = values[0];
            Naziv = values[1];
            if (values[2] == "Letnji")
                Semestar = EnumUt.SemestarType.Letnji;
            else
                Semestar = EnumUt.SemestarType.Zimski;
            GodinaStudija = values[3];
            ESPB = int.Parse(values[4]);
            ProfesorID = int.Parse(values[5]);
        }
    }
}
