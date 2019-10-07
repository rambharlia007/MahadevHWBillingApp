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