using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MahadevHWBillingApp.Helper;

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
                    using (var context = new MahadevHWContext())
                    {
                        var profile = context.Profiles.First();
                        profile.IsEligible = 0;
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
                catch (Exception e)
                {
                    using (var context = new MahadevHWContext())
                    {
                        var profile = context.Profiles.First();
                        profile.IsEligible = 0;
                        context.SaveChanges();
                    }
                    return 2;
                }
                return 0;
            }
        }
    }
}