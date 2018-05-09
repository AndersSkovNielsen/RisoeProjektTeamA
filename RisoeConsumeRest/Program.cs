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
            //Dette project er blevet lavet for at kunne teste de forskellige Rest-Service metoder.
            //For at det kan bruges skal der:
            //
            //Oprettes forbindelse til Class library
            //Kopieres kode fra vores rest her ind.
            //
            //Rest kode bliver testet her, hvorefter den kan kopieres tilbage i vores rest Solution, hvis det virker som det skal.
            //Kode til Database gemt i tekst strings for at eliminere relaterede syntakstfejl
            Console.WriteLine(@"ConsumeDatabase Database = new ConsumeDatabase();");
            Console.WriteLine(@"Database.Main();");
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("End of Program. Press any key to close.");
            // Her ligger ConsumeRest

            ConsumeRest RestService = new ConsumeRest();
            RestService.Test();
            Console.Clear();
            Console.WriteLine("Slut på Test");
            Console.ReadKey();
        }
    }
}
