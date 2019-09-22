using System;
using System.Collections.Generic;
using System.Globalization;

namespace MahadevHWBillingApp.Helper
{
    public static class Query
    {
        public static readonly string GetItem = "Select * From Items";
        public static readonly string GetSale = "select * From Sales";

        public static string GetPurchase(string fromDate, string toDate)
        {
            if (fromDate == null && toDate == null)
            {
                var currentDate = DateTime.Now.Date;
                var to = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                var from = currentDate.AddDays(-90).Date.ToString("yyyy-MM-dd HH:mm:ss");
                return $"Select * From Purchase Where Date >= '{from}' and Date <= '{to}'";
            }
            else
            {
                var from = DateTime.ParseExact(fromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                    .ToString("yyyy-MM-dd HH:mm:ss");
                var to = DateTime.ParseExact(toDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                    .ToString("yyyy-MM-dd HH:mm:ss");
                return $"Select * From Purchase Where Date >= '{from}' and Date <= '{to}'";
            }
        }

        public static string DeleteItem(IList<int> ids)
        {
            return $"Delete From Items Where Id in ({string.Join(",", ids)})";
        }
        public static string DeletePurchase(IList<int> ids)
        {
            return $"Delete From Purchase Where Id in ({string.Join(",", ids)})";
        }
        public static string GetItemById(int id)
        {
            return $"Select * From Items Where Id in ({id})";
        }

        internal static string GetSaleAndProducts(int id)
        {
            return
                $"Select * from Sales Where Id = {id}; Select SI.*, I.Name From SaleItems SI inner join Items I on SI.ItemId = I.Id Where SaleId = {id}";
        }

        public static string GetItemBySearch(string filter)
        {
            return $"Select * From Items Where Name like '%{filter}%'";
        }

        public static string GetPurchaseById(int id)
        {
            return $"Select * From Purchase Where Id in ({id})";
        }
    }
}