using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahadevHWBillingApp.Models
{
    public class ProductQuantityTrack
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int SaleItemId { get; set; }
    }
    public class Bill
    {
        public Contact Customer { get; set; }
        public Sale SaleDetail { get; set; }
        public List<SaleItem> SaleItems { get; set; }
    }

    public class EstimateBill
    {
        public Estimate Estimate { get; set; }
        public List<EstimateItem> EstimateItems { get; set; }
    }
        
}