using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Helper
{
    public class FooterTotal
    {
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal Amount { get; set; }
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
        public static FooterTotal GetTotalForPurchase(this IEnumerable<Purchase> input)
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
    }
}