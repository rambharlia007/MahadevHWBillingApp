using System;
using System.Security.Cryptography;
using System.Text;


namespace MahadevHWBillingApp.Models
{
    public class EncryptDecryptData
    {
        private static string key = "bharliaramraswinbharatravirakievjipunipraveengeekay";
        public static string Encrypt(string data)
        {
            RijndaelManaged objrij = new RijndaelManaged
            {
                //set the mode for operation of the algorithm
                Mode = CipherMode.CBC,
                //set the padding mode used in the algorithm.
                Padding = PaddingMode.PKCS7,
                //set the size, in bits, for the secret key.
                KeySize = 0x80,
                //set the block size in bits for the cryptographic operation.
                BlockSize = 0x80
            };
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

            return finalData;

        }

        public static string Decrypt(string data)
        {
            RijndaelManaged objrij = new RijndaelManaged
            {
                Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, KeySize = 0x80, BlockSize = 0x80
            };

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
            return finalData;
        }
    }
}