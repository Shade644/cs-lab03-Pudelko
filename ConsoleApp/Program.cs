using PudelkoLibrary;
using System;
using System.Collections.Generic;

namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {
            var List = new List<Pudelko>
            {
                new Pudelko(5.2020, 9, 10),
                new Pudelko(5.5, 7.12, 6, UnitOfMeasure.meter),
                new Pudelko(8, 14.3, 6.2, UnitOfMeasure.centimeter),
                new Pudelko(5, 25, 34, UnitOfMeasure.milimeter),



            };
           
            foreach (var n in List)
            {
                Console.WriteLine(n.ToString());
            }

            Console.WriteLine("Objetość " + (new Pudelko(1, 2, 3)).Objetosc);
            Console.WriteLine("Pole " + (new Pudelko(4, 5, 6)).Pole);

            var p1 = new Pudelko(1, 2.1, 3.05);
            var p2 = new Pudelko(1, 2.1, 3.05);
            Console.WriteLine("Porownanie " + p1.Equals(p2));
            


        }
    }
}
