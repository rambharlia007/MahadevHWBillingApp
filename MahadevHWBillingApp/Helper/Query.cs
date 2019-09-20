using System;
using System.Collections.Generic;

namespace MahadevHWBillingApp.Helper
{
    public static class Query
    {
        public static readonly string GetItem = "Select * From Items";
        public static readonly string GetPurchase = "Select * From Purchase";
        public static readonly string GetSale = "select * From Sales";

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
           return $"Select * from Sales Where Id = {id}; Select * From SaleItems Where SaleId = {id}";
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