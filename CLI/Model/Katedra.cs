using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    class Katedra : Serialization.ISerializable
    {
        public int KatedraId { get; set; }  
        public string SifraKatedre { get; set; }
        public string NazivKatedre { get; set; }

        public string Sef {  get; set; } //Pitati asistente sta dodje sef
        public List<Profesor> Profesori { get; set; }

        public Katedra()
        {
            Profesori = new List<Profesor>();
        }

        public Katedra(string sifraKatedre, string nazivKatedre, string sef)
        {
            SifraKatedre = sifraKatedre;
            NazivKatedre = nazivKatedre;
            Sef = sef;
        }

        public Katedra(string sifra, string nazivKatedre)
        {
            SifraKatedre = sifra;
            NazivKatedre = nazivKatedre;
            Profesori = new List<Profesor>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SIFRA KATEDRE: " + SifraKatedre + ", ");
            sb.Append("NAZIV KATEDRE: " + NazivKatedre + ", ");
            sb.Append("PROFESORI: ");
            sb.AppendJoin(", ", Profesori.Select(profesor => profesor.Ime));
           

            return sb.ToString();
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            KatedraId.ToString(),
            SifraKatedre,
            NazivKatedre,
            Sef
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            KatedraId = int.Parse(values[0]);
            SifraKatedre = values[1];
            NazivKatedre = values[2];
            Sef = values[3];
        }


    }
}
