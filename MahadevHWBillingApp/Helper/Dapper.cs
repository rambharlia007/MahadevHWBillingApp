using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Dapper;
using MahadevHWBillingApp.Models;
using static Dapper.SqlMapper;

namespace MahadevHWBillingApp.Helper
{
    public static class Dapper
    {
        private static string _connectionString =  Generic.GetConnectionString();
        public static IEnumerable<T> Get<T>(string query) where T : class
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                return con.Query<T>(query);
            }
        }

        public static Bill GetBillDetails(string query)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                var gridReader = con.QueryMultiple(query);
                var result = new Bill
                {
                    SaleDetail = gridReader.ReadSingle<Sale>(),
                    SaleItems = gridReader.Read<SaleItem>().ToList()
                };
                return result;
            }
        }

        public static T GetById<T>(string query) where T : class
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                return con.QueryFirst<T>(query);
            }
        }
        public static int GetCount(string query) 
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                return con.QueryFirst<int>(query);
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