namespace MahadevHWBillingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int AccountId { get; set; }
        public string AccountType { get; set; }
    }

    public static class AccountType
    {
        public static string TaxAccount = "TaxAccount";
        public static string NonTaxAccount = "NonTaxAccount";
    }

    public static class Keys
    {
        public static string MasterPassword = "R7V@5Qrg#2ev$dZEA";
        public static string MasterUserName = "master";
    }
}   