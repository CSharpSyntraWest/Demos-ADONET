using Bieren.Data.Models;
using System;
using System.Collections.Generic;

namespace Demos_ADONET
{
    class Program
    {
       
        static void Main(string[] args)
        {
            BierenDataService bierenDataService = new BierenDataService();
            //bierenDataService.OpenEnSluitConnectie();

            //bierenDataService.UpdateBierenAlcoholPercentage();
            IList<Bier> bierenUitDb =  bierenDataService.GeefAlleBieren();
            IList<Soort> soortenUitDb = bierenDataService.GeefAlleSoorten();
            foreach(Soort soort in soortenUitDb)
            {
                Console.WriteLine($"{soort.SoortNr} : {soort.SoortNaam}");

            }
           // "Select SoortNr,Soort from Soorten"
            //int? aantalBrouwers = bierenDataService.GeefAantalBrouwersVoorPostCode(9000);//vb Gent
            //Console.WriteLine("Aantal brouwers in Gent:{0}" + aantalBrouwers);

            //foreach (Bier bier in bierenUitDb)
            //{
            //    Console.WriteLine($"{bier.BierNr} : {bier.Naam}, Alcohol = {bier.Alcohol}");
            //}
            //bierenDataService.GeefBierenVoorBrouwerId(99);
            //bierenDataService.GeefBierGegevensVoorBierId(85);
            //bierenDataService.VoegBierToe(Bier);
            //bierenDataService.VerwijderBier(Bier);
            Console.ReadKey();
        }
    }
}
