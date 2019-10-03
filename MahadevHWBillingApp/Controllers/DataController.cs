﻿using MahadevHWBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MahadevHWBillingApp.Controllers
{
    public class DataController : BaseController
    {

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
                                BusinessName = $"Busn-{i}{j}{k}",
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

        public JsonResult EDate(string data, string key)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.
            byte[] passBytes = Encoding.UTF8.GetBytes(key);
            //set the initialization vector (IV) for the symmetric algorithm
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            //Creates symmetric AES object with the current key and initialization vector IV.
            ICryptoTransform objtransform = objrij.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(data);
            //Final transform the test string.
            var finalData = Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));

            return Json(finalData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DDate(string data, string key)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            objrij.Mode = CipherMode.CBC;
            objrij.Padding = PaddingMode.PKCS7;
            objrij.KeySize = 0x80;
            objrij.BlockSize = 0x80;
            byte[] encryptedTextByte = Convert.FromBase64String(data);
            byte[] passBytes = Encoding.UTF8.GetBytes(key);
            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            var finalData = Encoding.UTF8.GetString(TextByte);  //it will return readable string
            return Json(finalData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Sales()
        {
            using (var transaction = _mahadevHwContext.Database.BeginTransaction())
            {
                try
                {


                    var data = new List<Sale>();
                    for (int i = 0; i < 12; i++)
                    {
                        var monthStartDate = DateTime.Now.AddMonths(-i).Date;

                        for (int j = 0; j < 30; j++)
                        {
                            var date = monthStartDate.AddDays(-j).Date;
                            for (int k = 0; k < 2; k++)
                            {


                                var gst = new[] {5, 9, 15, 18};
                                var gen = new Random();
                                var products = new List<SaleItem>();

                                for (int m = 0; m < 5; m++)
                                {
                                    var price = gen.Next(5000, 15000);
                                    var taxper = gst[gen.Next(0, 3)];
                                    products.Add(new SaleItem()
                                    {
                                        ItemId = m + 1,
                                        Name = $"item-{m + 1}",
                                        Quantity = 10,
                                        Price = price,
                                        TotalAmount = price * 10,
                                        CGST = taxper,
                                        TotalCGSTAmount = (taxper * price * 10) / 100,
                                        SGST = taxper,
                                        TotalSGSTAmount = (taxper * price * 10) / 100
                                    });
                                }

                                var ta = products.Sum(e => e.TotalAmount);
                                var tta = products.Sum(e => e.TotalCGSTAmount);

                                var saleDetails = new Sale()
                                {
                                    Date = date,
                                    BusinessName = $"B-{i}-{j}-{k}",
                                    Invoice = date.ToString("yyyyMMddHHmmss") + k.ToString(),
                                    TotalAmount = ta + tta + tta,
                                    TotalCGSTAmount = tta,
                                    TotalSGSTAmount = tta,
                                    CustomerGSTIN = "AK123" + date.ToString("yyyyMMddHHmmss") + k.ToString(),
                                    CustomerName = "RGDJSJ" + k.ToString(),
                                    SubTotal = ta
                                };
                                _mahadevHwContext.Sales.Add(saleDetails);
                                _mahadevHwContext.SaveChanges();

                                foreach (var saleItem in products)
                                {
                                    saleItem.SaleId = saleDetails.Id;
                                    _mahadevHwContext.SaleItems.Add(saleItem);
                                }

                                _mahadevHwContext.SaveChanges();

                            }
                        }
                    }

                    transaction.Commit();
                    return Json("Done", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult Product()
        {
            try
            {
                var taxPer = new int[] { 5, 9, 15, 18 };
                var data = new List<Item>();
                var gen = new Random();
                for (int i = 1; i <= 10000; i++)
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
                        Name = $"Pro-{i}",
                        MeasuringUnit = "1 U",
                        HSN = $"HSN-{i}"
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