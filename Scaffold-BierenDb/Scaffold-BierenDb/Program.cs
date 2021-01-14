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
            //PrintAlleBieren();

            //PrintAlleBrouwersMetBieren();

            //Oefening: Geef alle soorten en voor elke soort alle bieren
            /*
             * Oefening BierenDb. Voeg een nieuw bier toe met naam ”TESTBIER” met soort “Alcoholvrij” 
             * (zoek eerst het SoortNr op in Soorten) aan de Brouwer “Zwingel” (zoek eerst deze Brouwer op via BrNaam). 
             * Bewaar de gegevens in de BierenDb
            */
            // VoegBierToe();
            /*Wijzig de naam van TESTBIER in TESTALCOHOLVRIJ Bier en bewaar de wijziging in de database. 
             * Zoek eerst TESTBIER op in de database*/
            WijzigBier();

            PrintAlleBrouwersMetBieren();

            Console.ReadKey();
        }
        private static void WijzigBier()
        {
            using (BierenDbContext bierenDb = new BierenDbContext())
            {
                //Zoek bier met naam "TESTBIER"
                Bieren bier = bierenDb.Bieren.Where(b => b.Naam.ToUpper() == "TESTBIER").FirstOrDefault();
                if (bier == null)
                {
                    Console.WriteLine("Bier met naam 'TESTBIER' niet gevonden");
                    return;
                }
                bier.Naam = "TESTALCOHOLVRIJ";
                bierenDb.Bieren.Update(bier);
                bierenDb.SaveChanges();
            }
        }


        private static void PrintAlleBrouwersMetBieren()
        {
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
        }

        private static void PrintAlleBieren()
        {
            using (BierenDbContext bierenDb = new BierenDbContext())
            {
                List<Bieren> bieren = bierenDb.Bieren.ToList();
                foreach (Bieren bier in bieren)
                {
                    Console.WriteLine($"{bier.BierNr} : {bier.Naam}");
                }
            }
        }


        private static void VoegBierToe()
        {
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
            }

        }
    }
}