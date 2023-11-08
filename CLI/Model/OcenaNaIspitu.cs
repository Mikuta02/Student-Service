using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    internal class OcenaNaIspitu : ISerializable
    {
        public int StudentId { get; set; }
        public int ProfesorId { get; set; }

        public Student StudentPolozio {  get; set; }
        public Predmet PredmetStudenta { get; set; }
        public int Ocena { get; set; }
        public string DatumPolaganja {  get; set; }

        public OcenaNaIspitu(Student student, Predmet predmet,int ocena,string datum,int studentID,int profaId)
        {
            StudentPolozio = student;
            PredmetStudenta = predmet;
            Ocena = ocena;
            DatumPolaganja = datum; 
            StudentId = studentID;
            ProfesorId = profaId;

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

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Ocena.ToString(),
            DatumPolaganja,
            StudentId.ToString(),
            ProfesorId.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Ocena = int.Parse(values[0]);
            DatumPolaganja = values[1];
            StudentId = int.Parse(values[2]);
            ProfesorId = int.Parse(values[3]);
        }

    }
}
