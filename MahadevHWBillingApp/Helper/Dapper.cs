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
        public static IEnumerable<T> Get<T>(string query) where T : class
        {
            using (var con = new SQLiteConnection(Generic.GetConnectionString()))
            {
                return con.Query<T>(query);
            }
        }

        public static IEnumerable<T> GetPrimitive<T>(string query)
        {
            using (var con = new SQLiteConnection(Generic.GetConnectionString()))
            {
                return con.Query<T>(query);
            }
        }

        public static Bill GetBillDetails(string query)
        {
            using (var con = new SQLiteConnection(Generic.GetConnectionString()))
            {
                var gridReader = con.QueryMultiple(query);
                var result = new Bill
                {
                    SaleDetail = gridReader.ReadSingle<Sale>(),
                    SaleItems = gridReader.Read<SaleItem>().ToList(),
                    Customer = gridReader.ReadSingle<Contact>()
                };
                return result;
            }
        }

        public static T GetById<T>(string query) where T : class
        {
            using (var con = new SQLiteConnection(Generic.GetConnectionString()))
            {
                return con.QueryFirst<T>(query);
            }
        }
        public static int GetCount(string query)
        {
            using (var con = new SQLiteConnection(Generic.GetConnectionString()))
            {
                return con.QueryFirst<int>(query);
            }
        }
        public static void Execute(string query)
        {
            using (var con = new SQLiteConnection(Generic.GetConnectionString()))
            {
                con.Execute(query);
            }
        }
    }
}