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
            IList<Brouwer> brouwers = bierenDataService.GeefAlleBrouwers();
            foreach(Brouwer b in brouwers)
            {
                Console.WriteLine($"{b.BrouwerNr} : {b.BrNaam} - {b.Adres}, {b.PostCode} {b.Gemeente} Omzet: {b.Omzet}");

            }
            Brouwer brouwer = bierenDataService.GeefBrouwer(1);
            if(brouwer==null)
            {
                Console.WriteLine("Brouwer met ID=1 niet gevonden");
            }
            else
            {
                Console.WriteLine($"{brouwer.BrouwerNr} : {brouwer.BrNaam} - {brouwer.Adres}, {brouwer.PostCode} {brouwer.Gemeente} Omzet: {brouwer.Omzet}");

            }
            //bierenDataService.OpenEnSluitConnectie();

            //    bierenDataService.UpdateBierenAlcoholPercentage();
            //IList<Bier> bierenUitDb =  bierenDataService.GeefAlleBieren();
            //IList<Soort> soortenUitDb = bierenDataService.GeefAlleSoorten();
            //foreach(Soort soort in soortenUitDb)
            //{
            //    Console.WriteLine($"{soort.SoortNr} : {soort.SoortNaam}");

            //}
            // "Select SoortNr,Soort from Soorten"
            //int? aantalBrouwers = bierenDataService.GeefAantalBrouwersVoorPostCode(9000);//vb Gent
            //Console.WriteLine("Aantal brouwers in Gent:{0}" , aantalBrouwers);

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
