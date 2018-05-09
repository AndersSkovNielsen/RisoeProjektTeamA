using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisoeConsumeRest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Her ligger ConsumeRest

            ConsumeRest RestService = new ConsumeRest();
            RestService.Test();
            Console.Clear();
            Console.WriteLine("Slut på Test");
            Console.ReadKey();
        }
    }
}
