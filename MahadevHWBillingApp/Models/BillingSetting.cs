namespace MahadevHWBillingApp.Models
{
    public class BillingSetting
    {
        public int Id { get; set; }
        public string ProductColumn { get; set; }
        public string BillColumn { get; set; }
        public bool IsHSNRequired { get; set; }
        public bool IsPerRequired { get; set; }
        public bool IsDiscountRequired { get; set; }
        public bool IsGstRate { get; set; }
        public int PaperSize { get; set; }
        public int CopyType { get; set; }
    }
}