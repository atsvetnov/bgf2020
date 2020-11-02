using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Data.SqlClient;
using bgfadmin.Models;

namespace bgfadmin
{
 public class Utils
    {

        public static string GenerateAccessCode(int Seed)
        {
            string ABC = "ADEBCFRGHLXYMNIGKOPTUQRSVWZ3710458124395606723891579753";
            Random rand = new Random(Seed);
            return (ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    );
        }
        public static string GenerateURV(int Seed)
        {
            string ABC = "ADEBCFGHLXYMNIGKOPTUQASVWZ10458124395606723891579753";
            Random rand = new Random(Seed);
            return (ABC[rand.Next(0, 40)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    + ABC[rand.Next(0, 50)].ToString()
                    );
        }

        public static string GenerateLoginPasswordRandomString(int Seed)
        {
            string abc = "abfghwxiclasdejnmyzopqktuv";
            string ABC = "ADEBCFGHLXYMNIGKOPTUQASVWZ";
            string digit = "10458124395606723891579753";
            string spec = "~!@#&*($%^}{:<>?)_+|}{:<>?";
            Random rand = new Random(Seed);
            return (abc[rand.Next(0, 25)].ToString() + ABC[rand.Next(0, 25)].ToString() + digit[rand.Next(0, 25)].ToString() + spec[rand.Next(0, 25)].ToString()
                    + abc[rand.Next(0, 25)].ToString() + ABC[rand.Next(0, 25)].ToString() + digit[rand.Next(0, 25)].ToString() + spec[rand.Next(0, 25)].ToString());
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
    public class SqlHelper
    {
        public static DataTable GetDatatable(DbContext context, string query)
        {
            //SqlConnection cnn = new SqlConnection(Globals.Configuration.GetConnectionString("BgfAdminContext"));

            var dbconnection = context.Database.GetDbConnection();
            try
            {
                dbconnection.Open();
                var command = dbconnection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                var datatable = new System.Data.DataTable();
                System.Data.Common.DbDataReader reader = command.ExecuteReader();
                datatable.Load(reader);
                dbconnection.Close();
                return datatable;
            }
            catch (Exception exc)
            {

                throw (exc);
            }
            finally
            {
                if (dbconnection != null && dbconnection.State == ConnectionState.Open)
                {
                    dbconnection.Close();
                }
            }

        }

        public static void ExecuteSqlCommand(DbContext context, string sqlcmd)
        {
                var dbconnection = context.Database.GetDbConnection();
            try
            {
                dbconnection.Open();
                var command = dbconnection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sqlcmd;
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {

                throw (exc);
            }
            finally
            {
                if (dbconnection != null && dbconnection.State == ConnectionState.Open)
                {
                    dbconnection.Close();
                }
            }
        }

        public static string SqlEncode(string str)
        {
            return str.Replace("'", "''");
        }

    }

}
