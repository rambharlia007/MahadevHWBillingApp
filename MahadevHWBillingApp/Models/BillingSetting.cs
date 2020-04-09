using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class BillingSetting
    {
        public int Id { get; set; }
        public string ProductColumn { get; set; }
        public string BillColumn { get; set; }
    }
}