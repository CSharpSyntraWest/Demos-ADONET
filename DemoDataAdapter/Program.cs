using System;
using System.Data;
using System.Data.SqlClient;

namespace DemoDataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Oefening DataAdapter:
            //Maak een SqlDataAdapter die alle gegevens uit de tabel soorten uit de bierenDb haalt en in
            //een DataTable 'soortentabel' van de 'bierenDataSet' plaatst
            //Schrijf dan via een DataTableReader alle soort-gegevens uit naar de console
            string sConnectionString = "Initial Catalog= BierenDb;Data Source=localhost;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(sConnectionString))
            {
                SqlDataAdapter sqlAdp = new SqlDataAdapter("Select * from Bieren", conn);
                DataSet sqlDS = new DataSet(" bierenDataSet");
                sqlAdp.Fill(sqlDS, "bierentabel");
                DataTableReader dtReader = sqlDS.Tables["bierentabel"].CreateDataReader();

                while (dtReader.Read())
                {
                    for (int i = 0; i < dtReader.FieldCount; i++) //FieldCount geeft het aantal kolommen terug
                    {  // ………   
                        Console.Write(dtReader[i] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
