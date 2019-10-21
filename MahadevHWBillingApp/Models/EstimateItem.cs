namespace MahadevHWBillingApp.Models
{
    public class EstimateItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int EstimateId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string MeasuringUnit { get; set; }
    }
}