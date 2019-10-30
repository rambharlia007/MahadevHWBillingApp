using System;
using System.Web;

namespace MahadevHWBillingApp.Helper
{
    public class Generic
    {
        public static string GetConnectionString()
        {

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["AccountId"] != null)
            {
                var accountId = HttpContext.Current.Session["AccountId"].ToString();
                return $@"Data Source=C:\SqlServerDataBase\DataBase\GSTBillingAccount{accountId}.db";
            }
            return $@"Data Source=C:\SqlServerDataBase\DataBase\GSTBillingAccount1.db";
        }

        public static string GetCoreConnectionString()
        {
            return @"Data Source=C:\SqlServerDataBase\DataBase\GSTCore.db";
        }
        public static string Invoice(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return "A-1";
            }

            var alphabets = "";
            var splitData = data.Split('-');
            var dataAlphabets = splitData[0];
            var dataNumbers = splitData[1];

            for (int i = dataAlphabets.Length - 1; i >= 0; i--)
            {
                if (dataAlphabets[i] == 'Z' && i != 0)
                {
                    alphabets += (char)'A';
                }
                else if (dataAlphabets[i] == 'Z' && i == 0)
                {
                    alphabets += (char)'A';
                    alphabets += (char)'A';
                }
                else
                {
                    alphabets += (char)(dataAlphabets[i] + 1);
                    for (int j = i - 1; j >= 0; j--)
                    {
                        alphabets += (char)(dataAlphabets[j]);
                    }
                    break;
                }
            }

            string invoiceNumber = "";
            if (int.TryParse(dataNumbers, out int seriesNumber))
            {
                if (seriesNumber == 10000)
                {
                    var reverseAlp = alphabets.ToCharArray();
                    Array.Reverse(reverseAlp);
                    invoiceNumber = new string(reverseAlp) + "-" + "1";
                }
                else
                {
                    var increment = seriesNumber + 1;
                    invoiceNumber = dataAlphabets + "-" + increment.ToString();
                }
            }
            return invoiceNumber;
        }
    }
}