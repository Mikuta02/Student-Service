using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    class Predmet : Serialization.ISerializable
    {
        public int PredmetId { get; set; } 
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
            StudentiPolozili = new List<Student>();
            StudentiNepolozili = new List<Student>();
        }

        public Predmet(string sifraPredmeta, string naziv, string godinaStudija, int profesorID, int eSPB)
        {
            SifraPredmeta = sifraPredmeta;
            Naziv = naziv;
            GodinaStudija = godinaStudija;
            ProfesorID = profesorID;
            ESPB = eSPB;
            Semestar = EnumUt.SemestarType.Letnji;
            StudentiPolozili = new List<Student>();
            StudentiNepolozili = new List<Student>();
        }

        public Predmet()
        {
            StudentiPolozili = new List<Student>();
            StudentiNepolozili = new List<Student>();
        }

        public override string? ToString()
        {
            return $"Sifra {SifraPredmeta,5} | Naziv {Naziv,30} | Godina {GodinaStudija,6} | ProfID {ProfesorID,2} | ESPB {ESPB,2} |";
        }



        /*        public override string? ToString()
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
                }*/

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            PredmetId.ToString(),
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
            PredmetId = int.Parse(values[0]);
            SifraPredmeta = values[1];
            Naziv = values[2];
            if (values[3] == "Letnji")
                Semestar = EnumUt.SemestarType.Letnji;
            else
                Semestar = EnumUt.SemestarType.Zimski;
            GodinaStudija = values[4];
            ESPB = int.Parse(values[5]);
            ProfesorID = int.Parse(values[6]);
        }
    }
}
