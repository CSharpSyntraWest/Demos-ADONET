using System;
using System.Data;

namespace DemoDataSet
{
    class Program
    {
        static void Main(string[] args)
        {

            //DataKolom kolomVoornaam en familienaam
            DataColumn kolomVoornaam = new DataColumn("Voornaam");
            kolomVoornaam.DataType = Type.GetType("System.String");
            DataColumn kolomFamilieNaam = new DataColumn("Familienaam");
            kolomFamilieNaam.DataType = Type.GetType("System.String");
            //  AutoIncrementele kolom definiëren:
            DataColumn KlantIDKolom = new DataColumn("KlantID", Type.GetType("System.Int32"));
            KlantIDKolom.AutoIncrement = true;
            KlantIDKolom.AutoIncrementSeed = 100;
            KlantIDKolom.AutoIncrementStep = 10;


            DataTable wnTable = new DataTable("Klant");
            wnTable.Columns.Add(KlantIDKolom);
            wnTable.Columns.Add(kolomVoornaam);
            wnTable.Columns.Add(kolomFamilieNaam);
            DataColumn[] PKKlant = new DataColumn[1];
            PKKlant[0] = wnTable.Columns["KlantID"];
            wnTable.PrimaryKey = PKKlant;
            // kolommen ID, Voornaam, Familienaam
            DataRow row1 = wnTable.NewRow(); // !
            row1["Voornaam"] = "Jos";
            row1["Familienaam"] = "De Klos";
            wnTable.Rows.Add(row1);
            DataRow row2 = wnTable.NewRow(); // !
            row2["Voornaam"] = "Jan";
            row2["Familienaam"] = "Jansens";
            wnTable.Rows.Add(row2);
            try
            {
                wnTable.Rows[0].Delete(); //eerste rij terug verwijderen
                wnTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (DataRow row in wnTable.Rows)
            {
                Console.WriteLine($"{row[0]} : {row[1]} {row[2]}");
            }
            DataRow row3 = wnTable.NewRow(); // !
            row3["Voornaam"] = "Piet";
            row3["Familienaam"] = "Pieters";
            wnTable.Rows.Add(row3);
            string filterStr = "Voornaam = 'Jan'";
            DataRow[] rijenMetVoornaamJan = wnTable.Select(filterStr);
            foreach (DataRow row in rijenMetVoornaamJan)
            {
                Console.WriteLine($"{row[0]} : {row[1]} {row[2]}");
            }
            filterStr = "Voornaam like '%'";
            DataRow[] gesorteerd = wnTable.Select(filterStr, "Familienaam DESC");


            DataRow dr;
            for (int i = 0; i < gesorteerd.Length; i++)
            {
                dr = gesorteerd[i];
                Console.WriteLine(dr["Familienaam"]);
            }
            DataTableReader dtReader = wnTable.CreateDataReader();

            while (dtReader.Read())
            {
                for (int i = 0; i < dtReader.FieldCount; i++) //FieldCount geeft het aantal kolommen terug
                {  // ………   
                    Console.Write(dtReader[i] + " ");
                }
                Console.WriteLine();
            }
            dtReader.Close();

            //Voorbeeld dataset autos

            DataTable autosTable = new DataTable("Auto");
            DataTable klantenTable = wnTable; //new DataTable("Klant"); (zie code hierboven)
            DataTable bestellingenTable = new DataTable("Bestelling");
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "BestID", DataType= typeof(Int32) ,AutoIncrement = true });
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "KlantID", DataType = typeof(Int32) });
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "AutoID", DataType = typeof(int) });
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "BestelDatum", DataType = typeof(DateTime) });

            DataColumn[] PKBestelling = new DataColumn[1];
            PKBestelling[0] = bestellingenTable.Columns["BestID"];
            bestellingenTable.PrimaryKey = PKBestelling;

            //  Data Set
            DataSet autosDataSet =
                           new DataSet("AutosDataSet");
            // tabellen toevoegen aan dataset
            autosDataSet.Tables.Add(bestellingenTable);
            autosDataSet.Tables.Add(klantenTable);
            autosDataSet.Tables.Add(autosTable);

          //  autosDataSet.WriteXml("autos.xml");
            autosDataSet.WriteXml("autos.xml", XmlWriteMode.WriteSchema);

          //  autosDataSet.ReadXml("autos.xml");
            //autosDataSet.ReadXml("autos.xml", XmlReadMode.ReadSchema);
            DataRelation dr1 = new DataRelation("KlantenBestelling",
                // parent 
             autosDataSet.Tables["Klant"].Columns["KlantID"],
             // child	 
            autosDataSet.Tables["Bestelling"]. Columns["BestID"]);
            autosDataSet.Relations.Add(dr1); // Exception
            //DataRelation dr2 = new DataRelation("AutosBestelling", autosDataSet.Tables["Auto"].Columns["AutoID"],
            //   autosDataSet.Tables["Bestelling"].Columns["AutoID"]);
            //autosDataSet.Relations.Add(dr2);

            // Haal rij op 
            DataRow drKlant = autosDataSet.Tables["Klant"].Rows[0];
            Console.WriteLine( "Klant Naam: " + drKlant["Voornaam"]);

            // navigeer van klanten tabel naar //bestelling tabel
            DataRow[] drsBestellingen = drKlant.GetChildRows(autosDataSet.Relations["KlantenBestelling"]);

            // haal de bestellingen van een klant op

            if (drsBestellingen != null)
                foreach (DataRow r in drsBestellingen)
                    Console.WriteLine("Bestelling Id: " + r["BestID"]);

        }


    }
}