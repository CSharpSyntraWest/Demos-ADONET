using System;
using System.Collections.Generic;

namespace Bieren.Data.Models
{
    public class Soort
    {
        public Soort()
        {
            Bieren = new List<Bier>();
        }

        public int SoortNr { get; set; }
        public string SoortNaam { get; set; }

        public virtual IList<Bier> Bieren { get; set; }
    }
}
