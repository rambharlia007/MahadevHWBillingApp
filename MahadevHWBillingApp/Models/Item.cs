using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
    }
}