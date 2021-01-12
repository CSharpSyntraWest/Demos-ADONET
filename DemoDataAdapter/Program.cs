using System;
using System.Data;
using System.Data.SqlClient;

namespace DemoDataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string sConnectionString = "Initial Catalog= BierenDb;Data Source=localhost;Integrated Security=SSPI;";
            using (SqlConnection conn = new SqlConnection(sConnectionString))
            {
                SqlDataAdapter sqlAdp = new SqlDataAdapter("Select * from Bieren", conn);
                DataSet sqlDS = new DataSet(" bierentabel");
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
