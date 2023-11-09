using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    class Indeks : Serialization.ISerializable
    {
        public int IndeksId { get; set; }
        public string OznakaSmera {  get; set; }
        public int BrojUpisa { get; set; }
        public int GodinaUpisa { get; set; }


        public Indeks(string oznakaSmera, int brojUpisa, int godinaUpisa)
        {
            OznakaSmera = oznakaSmera;
            BrojUpisa = brojUpisa;
            GodinaUpisa = godinaUpisa;
        }
        
        public Indeks() { }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("OZNAKA SMERA: {OznakaSmera}, ");
            sb.Append("BROJ UPISA: {BrojUpisa}, ");
            sb.Append("GODINA UPISA: {GodinaUpisa}");
            return base.ToString();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            IndeksId.ToString(),
            OznakaSmera,
            BrojUpisa.ToString(),
            GodinaUpisa.ToString()
        };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            IndeksId = int.Parse(values[0]);
            OznakaSmera = values[1];
            BrojUpisa = int.Parse(values[2]);
            GodinaUpisa = int.Parse(values[3]);
        }
    }
}
