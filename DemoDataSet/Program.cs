using System;
using System.Data;
//Oefening ADO.NET DataSet - Vul de dataset autosDataSet aan: 
//1. Maak kolommen aan in de DataTable "Auto" (maak kolom AutoID een autoincrementele kolom)
//2. Zet de kolom AutoID als PK kolom in deze tabel
//3. Voeg enkele rijen toe aan de tabel "Auto"
//4. Voeg een rij toe aan de tabel "Bestelling"
//5. Leg een relatie tussen de tabel Auto en tabel Bestelling (kolommen AutoID)
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
            //Oefening ADO.NET DataSet - Vul de dataset autosDataSet aan: 
            //1. Maak kolommen aan in de DataTable "Auto" (maak kolom AutoD een autoincrementele kolom)
            autosTable.Columns.Add(new DataColumn() { ColumnName = "AutoID", DataType = typeof(Int32), AutoIncrement = true });
            autosTable.Columns.Add(new DataColumn() { ColumnName = "Merk", DataType = typeof(String)});
            autosTable.Columns.Add(new DataColumn() { ColumnName = "Kleur", DataType = typeof(String) });
            autosTable.Columns.Add(new DataColumn() { ColumnName = "Model", DataType = typeof(String) });
            //2. Zet de kolom AutoID als PK kolom in deze tabel
            DataColumn[] PKAuto = new DataColumn[1];
            PKAuto[0] = autosTable.Columns["AutoID"];
            autosTable.PrimaryKey = PKAuto;
            //3. Voeg enkele rijen toe aan de tabel "Auto"
            DataRow drow1= autosTable.NewRow();
            drow1 = autosTable.NewRow();
            drow1["Merk"] = "Opel";
            drow1["Kleur"] = "Zwart";
            drow1["Model"] = "Astra";
            autosTable.Rows.Add(drow1);
            DataRow drow2 = autosTable.NewRow(); // !
            drow2["Merk"] = "Toyota";
            drow2["Kleur"] = "Blauw";
            drow2["Model"] = "Corolla";
            autosTable.Rows.Add(drow2);
            DataTableReader dReader = autosTable.CreateDataReader();
            while (dReader.Read())
            {
                for (int i = 0; i < dReader.FieldCount; i++) //FieldCount geeft het aantal kolommen terug
                {  // ………   
                    Console.Write(dReader[i] + " ");
                }
                Console.WriteLine();
            }
            dReader.Close();
            //4. Voeg een rij toe aan de tabel "Bestelling"

            //5. Leg een relatie tussen de tabel Auto en tabel Bestelling (kolommen AutoID)
            DataTable klantenTable = wnTable; //new DataTable("Klant"); (zie code hierboven)
            DataTable bestellingenTable = new DataTable("Bestelling");
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "BestID", DataType= typeof(Int32) ,AutoIncrement = true });
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "KlantID", DataType = typeof(Int32) });
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "AutoID", DataType = typeof(int) });
            bestellingenTable.Columns.Add(new DataColumn() { ColumnName = "BestelDatum", DataType = typeof(DateTime) });

            DataColumn[] PKBestelling = new DataColumn[1];
            PKBestelling[0] = bestellingenTable.Columns["BestID"];
            bestellingenTable.PrimaryKey = PKBestelling;
            DataRow bestelRow = bestellingenTable.NewRow();
            bestelRow["KlantID"] = 110;
            bestelRow["AutoID"] = 1;
            bestelRow["BestelDatum"] = DateTime.Today;
            bestellingenTable.Rows.Add(bestelRow);
            //  Data Set
            DataSet autosDataSet =
                           new DataSet("AutosDataSet");
            // tabellen toevoegen aan dataset
            autosDataSet.Tables.Add(bestellingenTable);
            autosDataSet.Tables.Add(klantenTable);
            autosDataSet.Tables.Add(autosTable);
            

            DataRelation dr1 = new DataRelation("KlantenBestelling",
                // parent 
             autosDataSet.Tables["Klant"].Columns["KlantID"],
             // child	 
            autosDataSet.Tables["Bestelling"]. Columns["KlantID"]);
           // autosDataSet.Relations.Add(dr1); // Exception

            DataRelation dr2 = new DataRelation("AutosBestelling", autosDataSet.Tables["Auto"].Columns["AutoID"],
               autosDataSet.Tables["Bestelling"].Columns["AutoID"]);
            autosDataSet.Relations.Add(dr2);

            // Haal rij op 
            DataRow drKlant = autosDataSet.Tables["Klant"].Rows[0];
            Console.WriteLine( "Klant Naam: " + drKlant["Voornaam"]);

            // navigeer van klanten tabel naar //bestelling tabel
            DataRow[] drsBestellingen = drKlant.GetChildRows(autosDataSet.Relations["KlantenBestelling"]);

            // haal de bestellingen van een klant op

            if (drsBestellingen != null)
                foreach (DataRow r in drsBestellingen)
                    Console.WriteLine("Bestelling Id: " + r["BestID"]);

            autosDataSet.WriteXml("autos.xml", XmlWriteMode.WriteSchema);

            Console.ReadKey();
        }


    }
}