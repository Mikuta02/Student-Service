using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    public class OcenaNaIspitu : Serialization.ISerializable
    {
        public int OcenaNaIspituId { get; set; }
        public int StudentId { get; set; }
        public int PredmetId { get; set; }

        public Student StudentPolozio {  get; set; }
        public Predmet PredmetStudenta { get; set; }
        public int Ocena { get; set; }
        public DateTime DatumPolaganja {  get; set; }

        public OcenaNaIspitu(Student student, Predmet predmet,int ocena,string datum,int studentID,int profaId)
        {
            StudentPolozio = student;
            PredmetStudenta = predmet;
            Ocena = ocena;
            DatumPolaganja = DateTime.Parse(datum); 
            StudentId = studentID;
            PredmetId = profaId;

        }

        public OcenaNaIspitu() { }

        public OcenaNaIspitu(int studentId, int predmetId, int ocena, string datumPolaganja)
        {
            StudentId = studentId;
            PredmetId = predmetId;
            Ocena = ocena;
            DatumPolaganja = DateTime.Parse(datumPolaganja);
        }

        public OcenaNaIspitu(int studentId, int predmetId, int ocena, DateTime datumPolaganja)
        {
            StudentId = studentId;
            PredmetId = predmetId;
            Ocena = ocena;
            DatumPolaganja = datumPolaganja;
        }

        public override string ToString()
        {
            return $"ID {OcenaNaIspituId,2} | Student id {StudentId,2} | Predmet id {PredmetId,2} | Ocena {Ocena,2} | Datum polaganja {DatumPolaganja.ToString("dd/MM/yyyy"),10}"; ;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            OcenaNaIspituId.ToString(),
            StudentId.ToString(),
            PredmetId.ToString(),
            Ocena.ToString(),
            DatumPolaganja.ToString("dd/MM/yyyy"),

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            OcenaNaIspituId = int.Parse(values[0]);
            StudentId = int.Parse(values[1]);
            PredmetId = int.Parse(values[2]);
            Ocena = int.Parse(values[3]);
            DatumPolaganja = DateTime.Parse(values[4]);
        }

    }
}
