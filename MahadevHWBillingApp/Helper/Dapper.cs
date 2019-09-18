﻿using System;
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

        internal static object Get<T>(object getPurchase)
        {
            throw new NotImplementedException();
        }

        public static T GetById<T>(string query) where T : class
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                return con.QueryFirst<T>(query);
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