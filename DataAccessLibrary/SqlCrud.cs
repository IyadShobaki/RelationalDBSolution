using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLibrary
{
   public class SqlCrud
   {
      private readonly string _connectionString;
      private SqlDataAccess db = new SqlDataAccess(); // so we can use it as many as we need

      public SqlCrud(string connectionString)
      {
         _connectionString = connectionString;
      }

      public List<BasicContactModel> GetAllContacts()
      {
         string sql = "select Id, FirstName, LastName from dbo.Contacts";

         // Create connection to our DataAccessLibrary
         return db.LoadData<BasicContactModel, dynamic>(sql, new { }, _connectionString);
      }

      public FullContactModel GetFullContactById(int id)
      {
         string sql = "select Id, FirstName, LastName from dbo.Contacts where Id = @Id";

         FullContactModel output = new FullContactModel();

         output.BasicInfo = db.LoadData<BasicContactModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

         if (output.BasicInfo == null)
         {
            // do something to tell the user that the record was not found
            // throw new Exception("User not found");

            return null;
         }

         sql = @"select e.* from dbo.EmailAddresses e inner join dbo.ContactEmail ce
                  on ce.EmailAddressId = e.Id where ce.ContactId = @Id";

         output.EmailAddresses = db.LoadData<EmailAddressModel, dynamic>(sql, new { Id = id }, _connectionString);

         sql = @"select p.* from dbo.PhoneNumbers p inner join dbo.ContactPhoneNumbers cp
                  on cp.PhoneNumberId = p.Id where cp.ContactId = @Id";

         output.PhoneNumbers = db.LoadData<PhoneNumberModel, dynamic>(sql, new { Id = id }, _connectionString);


         return output;
      }
   }
}
