using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class DataController : BaseController
    {

        public  string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }
            return Name;
        }

        // GET: Data
        public JsonResult Purchase()
        {
            try
            {
                var data = new List<Purchase>();
                var gen = new Random();
                for (int i = 0; i < 12; i++)
                {
                    var monthStartDate = DateTime.Now.AddMonths(-i).Date;

                    for (int j = 0; j < 30; j++)
                    {
                        var date = monthStartDate.AddDays(-j).Date;
                        for (int k = 0; k < 5; k++)
                        {
                            var am = gen.Next(5000);
                            decimal tax = (decimal)(am * 0.09);
                            data.Add(new Purchase()
                            {
                                Date = date,
                                BusinessName = GenerateName(5+k),
                                Invoice = date.ToString("yyyyMMddHHmmss")+k.ToString(),
                                TotalAmount = am,
                                TotalCGSTAmount = tax,
                                TotalSGSTAmount = tax
                            });
                        }
                    }
                }
                _mahadevHwContext.Purchase.AddRange(data);
                _mahadevHwContext.SaveChanges();
                return Json("Done", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        
        }

        public JsonResult Product()
        {
            try
            {
                var taxPer = new int[] { 5, 9, 15, 18 };
                var data = new List<Item>();
                var gen = new Random();
                for (int i = 0; i < 2000; i++)
                {
                    var t = taxPer[gen.Next(4)];
                    var p = (decimal) gen.Next(5000);
                    var d =  gen.Next(40);
                    var dp = p - (decimal)(d * p) / 100;
                    data.Add(new Item
                    {
                        CGST = t,
                        SGST = t,
                        Price = p,
                        Discount = d,
                        DiscountPrice = dp,
                        Quantity = gen.Next(100, 1000),
                        Name = GenerateName(5 + gen.Next(5)) + i.ToString(),
                        MeasuringUnit = "1 U",
                    });
                }
                _mahadevHwContext.Items.AddRange(data);
                _mahadevHwContext.SaveChanges();
                return Json("Done", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }

        }


    }
}