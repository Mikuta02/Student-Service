using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class ProfesorKatedra : Serialization.ISerializable
    {
        public int KatedraId { get; set; }
        public int ProfesorId { get; set; }

        public ProfesorKatedra()
        {

        }

        public ProfesorKatedra(int katedraid, int profesorid)
        {
            this.KatedraId = katedraid;
            this.ProfesorId = profesorid;
        }

        public override string ToString()
        {
            return $"IDK {KatedraId,2} | IDP {ProfesorId,2}";
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            KatedraId.ToString(),
            ProfesorId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            KatedraId = int.Parse(values[0]);
            ProfesorId = int.Parse(values[1]);
        }

    }
}
