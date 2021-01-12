using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scaffold_BierenDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BierenDbContext bierenDb = new BierenDbContext())
            {
                List<Bieren> bieren = bierenDb.Bieren.ToList();
                foreach (Bieren bier in bieren)
                {
                    Console.WriteLine($"{bier.BierNr} : {bier.Naam}");
                }
            }

            using (BierenDbContext bierenDb = new BierenDbContext())
            {
                List<Brouwers> brouwers = bierenDb.Brouwers.Include(b => b.Bieren).ToList();
                foreach (Brouwers brouwer in brouwers)
                {
                    Console.WriteLine($"{brouwer.BrNaam} :");
                    foreach (Bieren bier in brouwer.Bieren)
                    {
                        Console.WriteLine($"\t{bier.Naam}");
                    }
                }
            }

            //Oefening: Geef alle soorten en voor elke soort alle bieren
            Console.ReadKey();
        }
    }
}
