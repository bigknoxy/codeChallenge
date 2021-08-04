using System;
using System.IO;
using System.Security.Cryptography;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.Service.Services
{
    internal class EncryptionService : IEncryptionService
    {

        //todo pull these from a config/secrets file in real life
        private readonly string key = @"AXe8YwuIn1zxt3FPWTZFlAa14EHdPAdN9FaZ9RQWihc=";
        private readonly string IV = @"bsxnWolsAyO7kCfWuyrnqg==";
        public byte[] Encrypt(byte[] toEncrypt)
        {
            // Check arguments.
            if (toEncrypt == null || toEncrypt.Length <= 0)
                throw new ArgumentNullException("toEncrypt");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;            

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(IV);       
                
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(toEncrypt);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
    }
}
