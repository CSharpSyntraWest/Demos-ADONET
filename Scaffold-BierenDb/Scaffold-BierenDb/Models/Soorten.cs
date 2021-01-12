using System;
using System.Collections.Generic;

namespace Scaffold_BierenDb
{
    public partial class Soorten
    {
        public Soorten()
        {
            Bieren = new HashSet<Bieren>();
        }

        public int SoortNr { get; set; }
        public string Soort { get; set; }

        public virtual ICollection<Bieren> Bieren { get; set; }
    }
}
