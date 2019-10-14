namespace MahadevHWBillingApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string GSTIN { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public int IsDelete { get; set; }
        public bool IsSaveNewCustomer { get; set; }
    }
}