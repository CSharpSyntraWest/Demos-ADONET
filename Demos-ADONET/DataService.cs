using Bieren.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
//Oefening ADO.NET Sql Connectie - Vul de code aan: 
//1. Maak een methode die alle Brouwers
//   teruggeeft uit de Bierendatabase roep deze aan vanuit Program.cs en schrijf de brouwergegevens naar de console
//2. Maak een methode die aan de hand van de BrouwerID de gegevens van deze brouwer teruggeeft (zonder stored procedure)
//3. Maak een methode die een stored procedure aanroept die de omzet van de brouwers uit Brussel halveert
//4. Maak een methode die het aantal bieren van een bepaalde soort teruggeeft (soortnaam meegeven als input parameter aan stored procedure)

namespace Demos_ADONET
{
    public class BierenDataService
    {     
        private readonly string _connectionString;
        public BierenDataService()
        {
           // _connectionString = "Data Source=.;Initial Catalog=BierenDb;Integrated Security=True";
            //Haal connectiestring op uit config bestand bv
            _connectionString = ConfigurationManager.ConnectionStrings["BierenDbCon"].ConnectionString;
            _sqlConnectie = new SqlConnection(_connectionString);
        }

        public IList<Bier> GeefAlleBieren()
        {
            IList<Bier> bieren = new List<Bier>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("Select BierNr,Naam,BrouwerNr,SoortNr,Alcohol from Bieren", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())// rij per rij aflopen van resultaat, waarde uit kolom opvragen via sqlReader("kolomnaam")
                {
                    //sqlReader.GetValues() geeft alle waarden van één rij terug in een array
                    Bier bier = new Bier()
                    {
                        BierNr = (int)sqlReader["BierNr"],
                        Naam = sqlReader["Naam"].ToString(),// of sqlReader.GetFloat(4)
                        BrouwerNr = (sqlReader["BrouwerNr"] == DBNull.Value) ? null : (int?)sqlReader["BrouwerNr"],
                        SoortNr = (sqlReader["SoortNr"] == DBNull.Value) ? null : (int?)sqlReader["SoortNr"],
                        Alcohol = (sqlReader["Alcohol"] == DBNull.Value) ? null : (double?)sqlReader["Alcohol"]
                    };
                    bieren.Add(bier);
                }
                sqlReader.Close();//wordt automatisch afgesloten binnen using(...) wanneer connectie wordt afgesloten
            }
            return bieren;
        }

        public int? GeefAantalBrouwersVoorPostCode(short postcode)
        {
            int? aantalBrouwers = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_GeefAantalBrouwersVoorPostcode", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlParameter = new SqlParameter("@postcode", System.Data.SqlDbType.SmallInt);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = postcode;//9000; //Postcode Gent bv
                cmd.Parameters.Add(sqlParameter);
                aantalBrouwers = (int?)cmd.ExecuteScalar();
            }
            return aantalBrouwers;
        }
        public void UpdateBierenAlcoholPercentage()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateBierAlcoholPerc", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
