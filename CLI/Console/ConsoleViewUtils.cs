using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    class ConsoleViewUtils
    {
        public static int SafeInputInt()
        {
            int input;

            string rawInput = System.Console.ReadLine() ?? string.Empty;

            while (!int.TryParse(rawInput, out input))
            {
                System.Console.WriteLine("Nije ispravan broj, pokusati opet: ");

                rawInput = System.Console.ReadLine() ?? string.Empty;
            }

            return input;
        }

        public static int SafeInputGrade()
        {
            int input;

            string rawInput = System.Console.ReadLine() ?? string.Empty;
           
            while (!int.TryParse(rawInput, out input) || input < 6 || input > 10)
            {
                System.Console.WriteLine("Nije ispravan broj, pokusati opet: ");

                rawInput = System.Console.ReadLine() ?? string.Empty;
            }
            

            return input;
        }

        public static float SafeInputFloat()
        {
            float input;

            string rawInput = System.Console.ReadLine() ?? string.Empty;

            while (!float.TryParse(rawInput, out input))
            {
                System.Console.WriteLine("Nije ispravan broj, pokusati opet: ");

                rawInput = System.Console.ReadLine() ?? string.Empty;
            }

            return input;
        }
    }
}
