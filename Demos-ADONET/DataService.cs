using Bieren.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
//Oefening ADO.NET Sql Connectie - Vul de code aan: 
//1. Maak een methode die alle Brouwers
//   teruggeeft uit de Bierendatabase roep deze aan vanuit Program.cs en schrijf de brouwergegevens naar de console
//2. Maak een methode die aan de hand van de BrouwerID de gegevens van deze brouwer teruggeeft
//(zonder stored procedure)
//3. Maak een methode die een stored procedure aanroept die de omzet van de brouwers uit Brussel halveert 
//(eerst backup maken van BierenDb!!!)
//4. Maak een methode die het aantal bieren van een bepaalde soort teruggeeft 
//(soortnaam meegeven als input parameter aan stored procedure)

namespace Demos_ADONET
{
    public class BierenDataService
    {     
        private readonly string _connectionString;
        public BierenDataService()
        {
            _connectionString = "Data Source=.;Initial Catalog=BierenDb;Integrated Security=True";
            //Haal connectiestring op uit config bestand bv
            //_connectionString = ConfigurationManager.ConnectionStrings["BierenDbCon"].ConnectionString;
            //_sqlConnectie = new SqlConnection(_connectionString);
        }

        public void 
        public IList<Brouwer> GeefAlleBrouwers()
        {
            //Geef alle brouwers terug
            return GeefBrouwers();
        }
        public Brouwer GeefBrouwer(int BrouwerNr)
        {
            Brouwer brouwer = null;           
            IList<Brouwer> brouwers = GeefBrouwers(BrouwerNr);
            if (brouwers != null && brouwers.Count > 0) brouwer = brouwers[0];
            return brouwer;
        }
        private IList<Brouwer> GeefBrouwers(int BrouwerID = 0) //VoorGemeente(string gemeente)
        {
            IList<Brouwer> brouwers = new List<Brouwer>();
            string sqlQuery = $"select* from Brouwers";
            if (BrouwerID != 0)
            {
                //Geef enkel brouwers voor BrouwerID terug
                sqlQuery += " where BrouwerNr=" +BrouwerID;
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // where gemeente = '{gemeente}'
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader();
                while(sqlReader.Read())
                {
                    Brouwer brouwer = new Brouwer()
                    {
                        BrouwerNr = (int)sqlReader["BrouwerNr"],
                        BrNaam = (sqlReader["BrNaam"] == DBNull.Value) ? null : sqlReader["BrNaam"].ToString(),
                        Adres = (sqlReader["Adres"] == DBNull.Value) ? null : sqlReader["Adres"].ToString(),
                        Gemeente= (sqlReader["Gemeente"] == DBNull.Value) ? null : sqlReader["Gemeente"].ToString(),
                        PostCode = (sqlReader["PostCode"] == DBNull.Value) ? null : (short?)sqlReader["PostCode"],
                        Omzet = (sqlReader["Omzet"] == DBNull.Value) ? null : (int?)sqlReader["Omzet"]
                    };
                    brouwers.Add(brouwer);
                }
                sqlReader.Close();
            }

            return brouwers;
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
        public int? GeefAantalBierenVoorSoort(string soort)
        {
            int? aantalBieren = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_GeefAantalBierenVoorSoort", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlParameter = new SqlParameter("@soort", System.Data.SqlDbType.VarChar);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = soort;
                sqlParameter.Size = 50;//Varchar(50)
                cmd.Parameters.Add(sqlParameter);
                aantalBieren = (int?)cmd.ExecuteScalar();
            }
            return aantalBieren;
        }
        public void HalveerOmzetVoorBrouwers(short postcode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateOmzetVoorBrouwers", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlParameter = new SqlParameter("@postcode", System.Data.SqlDbType.SmallInt);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = postcode;//bv postcode 1000 = Brussel
                command.Parameters.Add(sqlParameter);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
                //USE[BierenDb]
                //GO

                //CREATE PROCEDURE[dbo].[sp_UpdateOmzetVoorBrouwers]
                //@postcode smallint
                //AS
                //BEGIN

                //    UPDATE BROUWERS SET Omzet = Omzet / 2

                //    where Postcode = 1000
                //END
                //--EXEC sp_UpdateOmzetVoorBrouwers 1000
                //--select* from Brouwers where postcode = 1000

                //--BV
                //INSERT INTO BROUWERS(BrouwerNr, BrNaam, Adres, PostCode, Gemeente, Omzet)
                //VALUES(9999, 'MijnBrouwer', 'Biervatlaan 99', 1000, 'Brussel', 2400)
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
            //USE[BierenDb]
            //    GO

            //    CREATE PROCEDURE[dbo].[sp_UpdateBierAlcoholPerc]
            //    AS
            //    BEGIN

            //        UPDATE BIEREN

            //        set Alcohol = Alcohol * 2

            //        where BierNr = 4
            //    END
        }

        public IList<Soort> GeefAlleSoorten()
        {
            IList<Soort> soorten = new List<Soort>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("Select SoortNr,Soort from Soorten", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())// rij per rij aflopen van resultaat, waarde uit kolom opvragen via sqlReader("kolomnaam")
                {
                    //sqlReader.GetValues() geeft alle waarden van één rij terug in een array
                    Soort soort = new Soort()
                    {
                        SoortNr = (int)sqlReader["SoortNr"],
                        SoortNaam = (sqlReader["Soort"] == DBNull.Value) ? null : sqlReader["Soort"].ToString()
                    };
                    soorten.Add(soort);
                }
                sqlReader.Close();//wordt automatisch afgesloten binnen using(...) wanneer connectie wordt afgesloten
            }
            return soorten;
        }
    }
}
