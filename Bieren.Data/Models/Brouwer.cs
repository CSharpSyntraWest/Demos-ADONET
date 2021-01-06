using System;
using System.Collections.Generic;

namespace Bieren.Data.Models
{
    public partial class Brouwer
    {
        public Brouwer()
        {
            Bieren = new List<Bier>();
        }

        public int BrouwerNr { get; set; }
        public string BrNaam { get; set; }
        public string Adres { get; set; }
        public short? PostCode { get; set; }
        public string Gemeente { get; set; }
        public int? Omzet { get; set; }

        public virtual IList<Bier> Bieren { get; set; }
    }
}
