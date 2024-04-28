using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLibrary
{
   public class SqlDataAccess
   {
      // Read operation
      public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
      {
         // Open the connection to the DB
         //use using statement to always close the connection properly 
         using (IDbConnection connection = new SqlConnection(connectionString))
         // IDbConnection -> using System.Data;
         // SqlConnection -> using System.Data.SqlClient;
         {
            // Instead of getting back a data table which we have to parse, type, cast and box, etc. (mapping)
            // Query (Dapper) will take a model that represnt a 1 row of data (will do mapping for us)
            // sqlStatement -> select // parameters such as ID 
            List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
            // Query -> Dapper                  // ToList -> System.Linq
            return rows;
         }
      }

      // Insert operation  // We can use it for delete and update as well passing parameters
      public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
      {
         using (IDbConnection connection = new SqlConnection(connectionString))
         {
            connection.Execute(sqlStatement, parameters);
         }
      }
   }
}
