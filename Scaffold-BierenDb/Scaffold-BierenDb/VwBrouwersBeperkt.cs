using System;
using System.Collections.Generic;

namespace Scaffold_BierenDb
{
    public partial class VwBrouwersBeperkt
    {
        public int BrouwerNr { get; set; }
        public string BrNaam { get; set; }
        public string Adres { get; set; }
        public short? PostCode { get; set; }
        public string Gemeente { get; set; }
    }
}
