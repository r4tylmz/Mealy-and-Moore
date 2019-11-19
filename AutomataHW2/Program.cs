using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutomataHW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            do
            {
                Console.WriteLine("1-) Moore Makinesi\n2-) Mealy Makinesi\nMakine Secimi:");
                var select = Console.ReadLine();
                if (@select == "1")
                    new Moore().RunMoore();
                else if (@select == "2")
                    new Mealy().RunMealy();
                else
                    Console.WriteLine("Hatali secim yaptiniz");
            } while (true);
        }
    }
}