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

    public class AdminUser: User
    {
        public string MasterPassword { get; set; }
    }

    public static class AccountType
    {
        public static string TaxAccount = "TaxAccount";
        public static string NonTaxAccount = "NonTaxAccount";
        public static string Admin = "Admin";
    }

    public static class Keys
    {
        public static string MasterPassword = "hdst58Mb2orw9J+oeJpifxNEu92maDyhfLYhzepxgoc=";
        public static string MasterUserName = "master";
    }
}   