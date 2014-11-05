using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Apokas
{
    public class Consola
    {
        Room objRoom = new Room();
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        public void Write(int[,] mundo, Room cuarto)
        {
            AllocConsole();
            Console.WriteLine("#| 0 1 2 3 4 5");
            Console.WriteLine("----------");
            for (int a = 0; a < 6; a++)
                Console.WriteLine(Convert.ToString(a) + "| " + Convert.ToString(mundo[0, a]) + " " + Convert.ToString(mundo[1, a]) + " " + Convert.ToString(mundo[2, a]) + " " + Convert.ToString(mundo[3, a]) + " " + Convert.ToString(mundo[4, a]) + " " + Convert.ToString(mundo[5, a]));
            Console.WriteLine("____________________");
            Console.WriteLine(Convert.ToString(cuarto.Roomx) + ";" + Convert.ToString(cuarto.Roomy));
        }

        public void cout(string a, string b, string c, string d)
        {
            string space = " ";
            AllocConsole();
            Console.WriteLine(a + space + b + space + c + space + d);
        }
    }
}
