using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Helper
{
    public class FooterTotal
    {
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal Amount { get; set; }

        public decimal SubAmount
        {
            get { return Amount - CGST - SGST; }
        }

        public decimal GST
        {
            get
            {
                return CGST + SGST;
            }
        }
    }
    public static class Extension
    {
        public static List<RecordPayment> CalculateRunningBalance(this List<RecordPayment> recordPayments, List<RecordPaymentSaleDto> bills)
        {
            var result = new List<RecordPayment>();
            var customerId = bills[0].CustomerId;
            decimal balance = 0;
            int index = 0;

            bills.GroupBy(e => e.Date).ToList().ForEach((groupData) =>
            {
                groupData.Select(e => e).ToList().ForEach((data) =>
                {
                    balance += data.TotalAmount;
                    result.Add(new RecordPayment()
                    {
                        Date = data.Date,
                        Debit = data.TotalAmount,
                        Balance = balance,
                        CustomerId = customerId,
                        Invoice = data.Invoice,
                        Particulars = "Sales"
                    });
                });
                var recordsWithBillDate = ((index == bills.Count - 2) || bills.Count == 1) ? recordPayments.Where(e => e.Date >= groupData.Key).ToList() : recordPayments.Where(e => e.Date >= groupData.Key && e.Date < bills[++index].Date).ToList();
                if (recordsWithBillDate.Any())
                {
                    recordsWithBillDate.ForEach((recordWithBillDate) =>
                    {
                        balance -= recordWithBillDate.Credit;
                        recordWithBillDate.Balance = balance;
                    });
                    result.AddRange(recordsWithBillDate);
                }
            });

            //bills.ForEach((bill) =>
            //{
            //    balance += bill.TotalAmount;
            //    result.Add(new RecordPayment()
            //    {
            //        Date = bill.Date,
            //        Debit = bill.TotalAmount,
            //        Balance = balance,
            //        CustomerId = customerId,
            //        Type = "Sales"
            //    });
            //    var recordsWithBillDate = ((index == bills.Count - 2) || bills.Count == 1) ? recordPayments.Where(e => e.Date >= bill.Date).ToList() : recordPayments.Where(e => e.Date >= bill.Date && e.Date < bills[++index].Date).ToList();
            //    if (recordsWithBillDate.Any())
            //    {
            //        recordsWithBillDate.ForEach((recordWithBillDate) =>
            //        {
            //            balance -= recordWithBillDate.Credit;
            //            recordWithBillDate.Balance = balance;
            //        });
            //        result.AddRange(recordsWithBillDate);
            //    }
            //});
            return result;
        }

        public static FooterTotal FooterSum(this IEnumerable<Purchase> input)
        {
            var footer = new FooterTotal();
            input.ToList().ForEach((data) =>
            {
                footer.CGST += data.TotalCGSTAmount;
                footer.SGST += data.TotalSGSTAmount;
                footer.Amount += data.TotalAmount;
            });
            return footer;
        }

        public static FooterTotal FooterSum(this IEnumerable<Sale> input)
        {
            var footer = new FooterTotal();
            input.ToList().ForEach((data) =>
            {
                footer.CGST += data.TotalCGSTAmount;
                footer.SGST += data.TotalSGSTAmount;
                footer.Amount += data.TotalAmount;
            });
            return footer;
        }

        public static FooterTotal FooterSum(this IEnumerable<SaleDto> input)
        {
            var footer = new FooterTotal();
            input.ToList().ForEach((data) =>
            {
                footer.CGST += data.TotalCGSTAmount;
                footer.SGST += data.TotalSGSTAmount;
                footer.Amount += data.TotalAmount;
            });
            return footer;
        }

        public static string ToCustomFormat(this string date)
        {
            return DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                .ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static DateTime ToCustomDateTimeFormat(this string date)
        {
            return DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }
    }
}