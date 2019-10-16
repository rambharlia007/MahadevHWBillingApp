using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class BillCreditDetail
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
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

    public class RecordPaymentDto
    {
        public int CustomerId { get; set; }
        public string FormatDate { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string Invoice { get; set; }
        public decimal Amount { get; set; }

        public void SetAmount(int previousAmount)
        {
             Amount = previousAmount + Credit - Debit;
        }
    }

    public class RecordPaymentSaleDto
    {
        public string Invoice { get; set; }
        public decimal Amount { get; set; }
        public string FormatDate { get; set; }
    }

}