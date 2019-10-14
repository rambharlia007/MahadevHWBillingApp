using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MahadevHWBillingApp.Helper
{
    public static class Query
    {
        public static readonly string GetItem = "Select * From Items Where IsDelete = 0";

        public static string GetItemCount = "Select count(Id) From Items Where IsDelete = 0";
        public static string GetContacts = "Select * From Contacts Where IsDelete = 0";
        public static string GetPurchase(string fromDate, string toDate)
        {
            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            return $"Select * From Purchase Where Date >= '{from}' and Date <= '{to}' Order By Date DESC";
        }

        public static string GetSale(string fromDate, string toDate, int customerId)
        {
            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            var cond = string.Empty;
            if (customerId > 0)
                cond = $"And C.Id = {customerId}";
            return $@"Select 
                        S.Id,
                        S.TotalCGSTAmount, 
                        S.TotalSGSTAmount,
                        S.Date,
                        S.SubTotal,
                        S.TotalAmount,
                        C.Name CustomerName,
                        C.GSTIN CustomerGSTIN,
                        C.Id CustomerId,
                        S.Invoice
                        From Sales S Inner Join Contacts C on S.CustomerId = C.Id 
                        Where S.Date >= '{from}' and S.Date <= '{to}' {cond} Order By S.Id";
        }

        public static string GetSaleExcelDownloadQuery(string fromDate, string toDate, List<decimal> gstSlots)
        {
            

            var builder = new StringBuilder();
            var index = 1;
            foreach(var data in gstSlots)
            {
                builder.Append($@",SUM(CASE 
			                        WHEN TotalTaxSlot = {data}
				                        THEN TotalTaxAmount
			                        END) AS [Tax{index++}]");
            }


            var from = fromDate.ToCustomFormat();
            var to = toDate.ToCustomFormat();
            return $@"
                        SELECT DATE
	                        ,Invoice
	                        ,BusinessName
	                        ,CustomerGSTIN
	                        ,CustomerName
	                        ,SubTotal
	                        {builder.ToString()}
                        FROM (
	                        SELECT S.BusinessName
		                        ,S.CustomerName
		                        ,S.CustomerGSTIN
		                        ,S.Invoice
		                        ,S.DATE
		                        ,S.SubTotal
		                        ,SUM(SI.TotalCGSTAmount) + SUM(SI.TotalSGSTAmount) TotalTaxAmount
		                        ,SI.SGST + SI.CGST TotalTaxSlot
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
                $"Select DistributorName Name, DistributorGSTIN, SubAmount, TotalCGSTAmount CGST, TotalSGSTAmount SGST, Date, TotalAmount Amount, Invoice From Purchase Where Date >= '{from}' and Date <= '{to}' Order By Date";
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
            return $"Select * From Items Where Id in ({id}) And IsDelete = 0";
        }

        public static string GetSaleAndProducts(int id)
        {
            return
                $@"
                    SELECT * 
                    FROM   sales 
                    WHERE  id = {id}; 

                    SELECT SI.*, 
                           I.NAME 
                    FROM   saleitems SI 
                           INNER JOIN items I 
                                   ON SI.itemid = I.id 
                    WHERE  SI.saleid = {id}; 

                    SELECT C.* 
                    FROM   Contacts C 
                           INNER JOIN sales S 
                                   ON C.id = S.customerid 
                    WHERE  S.id = {id} ";
        }

        public static string GetProductsByBill(int id)
        {
            return
                $"Select SI.ItemId, SI.Quantity, SI.Id SaleItemId From SaleItems SI inner join Items I on SI.ItemId = I.Id Where SaleId = {id}";
        }

        public static string GetItemBySearch(string filter)
        {
            return $"Select * From Items Where IsDelete = 0 AND Name like '%{filter}%'";
        }
        public static string GetContactsBySearch(string filter)
        {
            return $"Select * From Contacts Where IsDelete = 0 AND Name like '%{filter}%'";
        }
        public static string GetPurchaseById(int id)
        {
            return $"Select * From Purchase Where Id in ({id})";
        }
    }
}