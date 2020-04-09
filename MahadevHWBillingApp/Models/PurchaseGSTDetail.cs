namespace MahadevHWBillingApp.Models
{
    public class PurchaseGSTDetail
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int SGST { get; set; }
        public int CGST { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
    }
}