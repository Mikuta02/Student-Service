using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    internal class OcenaNaIspitu
    {
        public Student StudentPolozio {  get; set; }
        public Predmet PredmetStudenta { get; set; }
        public int Ocena { get; set; }
        public string DatumPolaganja {  get; set; }

        public OcenaNaIspitu(Student student, Predmet predmet,int ocena,string datum)
        {
            StudentPolozio = student;
            PredmetStudenta = predmet;
            Ocena = ocena;
            DatumPolaganja = datum; 

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("STUDENT KOJI JE POLOZIO: " + StudentPolozio + ", ");
            sb.Append("POLOZEN PREDMET: " + PredmetStudenta + ", ");
            sb.Append("OCENA: " + Ocena + ", ");
            sb.Append("DATUM POLAGANJA: " + DatumPolaganja);
            return base.ToString();
        }



    }
}
