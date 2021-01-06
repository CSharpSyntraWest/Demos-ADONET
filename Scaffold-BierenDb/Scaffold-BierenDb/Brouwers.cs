using System;
using System.Collections.Generic;

namespace Scaffold_BierenDb
{
    public partial class Brouwers
    {
        public Brouwers()
        {
            Bieren = new HashSet<Bieren>();
        }

        public int BrouwerNr { get; set; }
        public string BrNaam { get; set; }
        public string Adres { get; set; }
        public short? PostCode { get; set; }
        public string Gemeente { get; set; }
        public int? Omzet { get; set; }

        public virtual ICollection<Bieren> Bieren { get; set; }
    }
}
