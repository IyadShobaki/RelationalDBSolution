using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SQLServerUI
{
   internal class Program
   {
      static void Main(string[] args)
      {
         SqlCrud sql = new SqlCrud(GetConnectionString());

         //ReadAllContacts(sql);

         //ReadContact(sql, 1);

         //CreateNewContact(sql);

         //UpdateContact(sql);

         RemovePhoneNumberFromContact(sql, 1, 1);

         Console.WriteLine("Done processing");

         Console.ReadLine();
      }

      private static void RemovePhoneNumberFromContact(SqlCrud sql, int contactId, int phoneNumber)
      {
         sql.RemovePhoneNumberFromContact(contactId, phoneNumber);
      }
      private static void UpdateContact(SqlCrud sql)
      {
         BasicContactModel contact = new BasicContactModel
         {
            Id = 3,
            FirstName = "Sueee",
            LastName = "Stormmmm"
         };

         sql.UpdateContactName(contact);
      }
      private static void CreateNewContact(SqlCrud sql)
      {
         FullContactModel user = new FullContactModel
         {
            BasicInfo = new BasicContactModel
            {
               FirstName = "Sue",
               LastName = "Storm"
            }
         };
         user.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "sue@storm.com" });
         user.EmailAddresses.Add(new EmailAddressModel { Id = 2, EmailAddress = "me@shobaki.com" });

         user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-9876" });
         user.PhoneNumbers.Add(new PhoneNumberModel { Id = 1, PhoneNumber = "555-1212" });

         sql.CreateContact(user);
      }
      private static void ReadContact(SqlCrud sql, int contactId)
      {
         var contact = sql.GetFullContactById(contactId);


         Console.WriteLine($"{contact.BasicInfo.Id}: {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");

      }
      private static void ReadAllContacts(SqlCrud sql)
      {
         var rows = sql.GetAllContacts();

         foreach (var row in rows)
         {
            Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
         }
      }
      private static string GetConnectionString(string connectionStringName = "Default")
      {
         string output = "";

         var builder = new ConfigurationBuilder()  //ConfigurationBuilder using Microsoft.Extensions.Configuration;
            .SetBasePath(Directory.GetCurrentDirectory())  // Directory -> using System.IO;
            .AddJsonFile("appsettings.json");

         var config = builder.Build();

         output = config.GetConnectionString(connectionStringName);

         return output;
      }


   }
}
