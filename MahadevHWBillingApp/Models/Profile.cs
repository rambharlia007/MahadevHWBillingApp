namespace MahadevHWBillingApp.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string Owner { get; set; }
        public string Email { get; set; }
        public string GSTIN { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public bool EnableStockCount { get; set; }
        public static Profile GetDummyProfile()
        {
            return new Profile()
            {
                BusinessName = "Demo Business",
                Owner = "Demo User",
                MobileNumber = "4242553252",
                GSTIN = "2552552325",
                Email = "test@gmail.com",
                Address = "101A Kr puram",
            };
        }
    }
}