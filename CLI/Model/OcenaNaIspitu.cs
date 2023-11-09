using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    class OcenaNaIspitu : Serialization.ISerializable
    {
        public int OcenaNaIspituId { get; set; }
        public int StudentId { get; set; }
        public int PredmetId { get; set; }

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
            PredmetId = profaId;

        }

        public OcenaNaIspitu() { }

        public OcenaNaIspitu(int studentId, int predmetId, int ocena, string datumPolaganja)
        {
            StudentId = studentId;
            PredmetId = predmetId;
            Ocena = ocena;
            DatumPolaganja = datumPolaganja;
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
            OcenaNaIspituId.ToString(),
            Ocena.ToString(),
            DatumPolaganja,
            StudentId.ToString(),
            PredmetId.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            OcenaNaIspituId = int.Parse(values[0]);
            Ocena = int.Parse(values[1]);
            DatumPolaganja = values[2];
            StudentId = int.Parse(values[3]);
            PredmetId = int.Parse(values[4]);
        }

    }
}
