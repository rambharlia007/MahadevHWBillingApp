using System;
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
        public int IsFreeTrial
        {
            get
            {
                var value = string.IsNullOrEmpty(Key) ? null : EncryptDecryptData.Decrypt(Key);
                if (!string.IsNullOrEmpty(value) && IsEligible == 1)
                {
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
                }
                return 0;
            }
        }
    }
}