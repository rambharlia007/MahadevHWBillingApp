using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Dapper;

namespace MahadevHWBillingApp.Helper
{
    public static class Dapper
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["GstContext"].ConnectionString;
        public static IEnumerable<T> Get<T>(string query) where T : class
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                return con.Query<T>(query);
            }
        }

        public static void Execute(string query)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Execute(query);
            }
        }
    }
}