using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DemoDataSet
{
   public class InMemoryDb
   {

        public InMemoryDb()
        {
            //DataKolom aanmaken
            DataColumn colName = new DataColumn();
            colName.DataType = Type.GetType("System.String");

          //  AutoIncrementele kolom definiëren:
            DataColumn KlantIDKolom = new DataColumn("KlantID", Type.GetType("System.Int32"));
            KlantIDKolom.AutoIncrement = true;
            KlantIDKolom.AutoIncrementSeed = 100;
            KlantIDKolom.AutoIncrementStep = 10;
            DataTable wnTable = new DataTable("Werknemer");
            // kolommen ID, Voornaam, Familienaam
            DataRow row = wnTable.NewRow(); // !
            row["Voornaam"] = "Jos";
            row["Familienaam"] = "De Klos";
            wnTable.Rows.Add(row);

        }
    }
}
