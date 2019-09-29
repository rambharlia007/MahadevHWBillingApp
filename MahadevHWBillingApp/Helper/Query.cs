using System;
using System.Collections.Generic;
using System.Globalization;

namespace MahadevHWBillingApp.Helper
{
    public static class Query
    {
        public static readonly string GetItem = "Select * From Items";

        public static string GetPurchase(string fromDate, string toDate)
        {
            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            return $"Select * From Purchase Where Date >= '{from}' and Date <= '{to}' Order By Date";
        }

        public static string GetSale(string fromDate, string toDate)
        {
            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            return $"Select * From Sales Where Date >= '{from}' and Date <= '{to}' Order By Date";
        }

        public static string GetSaleExcelDownloadQuery(string fromDate, string toDate)
        {
            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            return $@"
                        SELECT DATE
	                        ,Invoice
	                        ,BusinessName
	                        ,CustomerGSTIN
	                        ,CustomerName
	                        ,SubTotal
	                        ,SUM(CASE 
			                        WHEN SGST = 5.0
				                        THEN T2
			                        END) AS [Tax1]
	                        ,SUM(CASE 
			                        WHEN SGST = 9.0
				                        THEN T2
			                        END) AS [Tax2]
                            ,SUM(CASE 
			                        WHEN SGST = 15.0
				                        THEN T2
			                        END) AS [Tax3]
	                        ,SUM(CASE 
			                        WHEN SGST = 18.0
				                        THEN T2
			                        END) AS [Tax4]
                        FROM (
	                        SELECT S.BusinessName
		                        ,S.CustomerName
		                        ,S.CustomerGSTIN
		                        ,S.Invoice
		                        ,S.DATE
		                        ,S.SubTotal
		                        ,SUM(SI.TotalCGSTAmount) T2
		                        ,SUM(SI.TotalSGSTAmount) T3
		                        ,SI.SGST
		                        ,SI.CGST
	                        FROM Sales S
	                        INNER JOIN SaleItems SI ON S.Id = SI.SaleId
                        Where S.Date >= '{from}' and S.Date <= '{to}'
	                        GROUP BY SI.SGST
		                        ,SI.CGST
		                        ,S.BusinessName
		                        ,S.CustomerName
		                        ,S.CustomerGSTIN
		                        ,S.Invoice
		                        ,S.DATE
	                        ) sourceData
                        GROUP BY sourceData.Invoice
	                        ,sourceData.BusinessName
	                        ,sourceData.CustomerName
	                        ,sourceData.CustomerGSTIN
	                        ,sourceData.Invoice
	                        ,sourceData.Date
                        ORDER BY SourceData.Date";
        }

        public static string GetPurchaseExcelDownload(string fromDate, string toDate)
        {

            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            return
                $"Select BusinessName Name,TotalCGSTAmount CGST, TotalSGSTAmount SGST, Date, TotalAmount Amount, Invoice From Purchase Where Date >= '{from}' and Date <= '{to}' Order By Date";
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