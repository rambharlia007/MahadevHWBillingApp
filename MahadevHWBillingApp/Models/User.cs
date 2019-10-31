using MahadevHWBillingApp.Helper;
using System;
using System.Linq;

namespace MahadevHWBillingApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public string BusinessName { get; set; }
        public string OwnerName { get; set; }


        public int IsEligible { get; set; }
        public string Key { get; set; }
        public string K1 { get; set; }
        public string GrantedBy { get; set; }
        public int IsFreeTrial
        {
            get
            {
                if (string.IsNullOrEmpty(Key) && string.IsNullOrEmpty(K1))
                    return 0;
                else if (IsEligible != 1)
                    return 0;

                var value = EncryptDecryptData.Decrypt(Key);
                var K1Value = EncryptDecryptData.Decrypt(K1);
                if (!K1Value.Equals(System.Net.Dns.GetHostName()))
                {
                    using (var context = new CoreContext())
                    {
                        var admin = context.Users.Where(e => e.AccountType.Equals(Models.AccountType.Admin)).FirstOrDefault();
                        admin.IsEligible = 0;
                        context.SaveChanges();
                    }
                    return 0;
                }

                try
                {
                    var parseDate = value.Trim().ToCustomDateTimeFormat();
                    if (DateTime.Now.Date > parseDate)
                    {
                        return 2;
                    }
                }
                catch (Exception ex)
                {
                    using (var context = new CoreContext())
                    {
                        var admin = context.Users.Where(e => e.AccountType.Equals(Models.AccountType.Admin)).FirstOrDefault();
                        admin.IsEligible = 0;
                        context.SaveChanges();
                    }
                    return 2;
                }
                return 0;
            }
        }
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