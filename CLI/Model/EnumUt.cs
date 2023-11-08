using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class EnumUt
    {
        public static implicit operator StatusType(EnumUt v)
        {
            throw new NotImplementedException();
        }

        public enum StatusType { S, B };
        public enum SemestarType { Letnji, Zimski };
    }
}
