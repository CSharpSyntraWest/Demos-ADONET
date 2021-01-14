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
            /*
             * Oefening BierenDb. Voeg een nieuw bier toe met naam ”TESTBIER” met soort “Alcoholvrij” 
             * (zoek eerst het SoortNr op in Soorten) aan de Brouwer “Zwingel” (zoek eerst deze Brouwer op via BrNaam). 
             * Bewaar de gegevens in de BierenDb
            */

            //zoek eerst het SoortNr op in Soorten met naam “Alcoholvrij” 
            using (BierenDbContext db = new BierenDbContext())
            {
                Soorten soort = db.Soorten.Where(s => s.Soort.ToLower() == "alcoholvrij").FirstOrDefault();
                if (soort == null)
                {
                    Console.WriteLine("Soort 'alcoholvrij' niet gevonden");
                }
                else
                {
                    //Zoek BrouwerNr voor Brouwer “Zwingel” (zoek eerst deze Brouwer op via BrNaam)
                    Brouwers brouwer = db.Brouwers.Where(b => b.BrNaam.ToLower() == "zwingel").Single();
                    //Nieuw Bier toevoegen met naam "TESTBIER" van brouwer "Zwingel" en soort "alcoholvrij"
                    int newBierNr = db.Bieren.Max(b => b.BierNr) + 1;
                    Bieren bier = new Bieren()
                    {
                        BierNr = newBierNr, //99999,
                        Naam = "TESTBIER",
                        Alcohol = 0.0,
                        BrouwerNr = brouwer.BrouwerNr,
                        SoortNr = soort.SoortNr
                    };
                    //Toevoegen aan de collectie Bierens
                    db.Bieren.Add(bier);
                    //Nieuw bier bewaren in database
                    db.SaveChanges();
                }

                //}

                Console.ReadKey();
        }
}
}
