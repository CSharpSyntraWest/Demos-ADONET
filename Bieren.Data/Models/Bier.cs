using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.Data.Models
{
    public class Bier
    {
        public int BierNr { get; set; }
        public string Naam { get; set; }
        public int? BrouwerNr { get; set; }
        public int? SoortNr { get; set; }
        public double? Alcohol { get; set; }

    }
}
