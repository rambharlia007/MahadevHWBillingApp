using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class BillCreditDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
    public class BillCreditDetailDto : BillCreditDetail
    {
        public string FormatDate
        {
            get
            {
                return Date.ToString("dd-MM-yyyy");
            }
        }
        public string UIDateFormat { get; set; }
    }
}