using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Utils
{
    class Helper
    {
        public static void Print(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }


        public static Pharmacy AddName(string name)
        {
            while (true)
            {

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Enter basmayin");
                }
                if (name.StartsWith(' '))
                {
                    Console.WriteLine("Ad boshlugla bashlaya bilmez");
                }
                return new Pharmacy(name);
            }

        }
    }
}
