using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    public class Katedra : Serialization.ISerializable
    {
        public int KatedraId { get; set; }  
        public string SifraKatedre { get; set; }
        public string NazivKatedre { get; set; }

        public Profesor Sef {  get; set; } //Pitati asistente sta dodje sef
        public int SefId;
        public List<Profesor> Profesori { get; set; }

        public Katedra()
        {
            Profesori = new List<Profesor>();
        }

        public Katedra(string sifraKatedre, string nazivKatedre, int sefId)
        {
            SifraKatedre = sifraKatedre;
            NazivKatedre = nazivKatedre;
            SefId = sefId;
            Profesori = new List<Profesor>();
        }

        public Katedra(string sifra, string nazivKatedre)
        {
            SifraKatedre = sifra;
            NazivKatedre = nazivKatedre;
            Profesori = new List<Profesor>();
        }

        public override string ToString()
        {
            return $"ID {KatedraId,2} | Sifra katedre {SifraKatedre,5} | Naziv katedre {NazivKatedre,25} | SefID {SefId,2}";
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            KatedraId.ToString(),
            SifraKatedre,
            NazivKatedre,
            SefId.ToString(),
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            KatedraId = int.Parse(values[0]);
            SifraKatedre = values[1];
            NazivKatedre = values[2];
            SefId = int.Parse(values[3]);
        }


    }
}
