using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FesteLOC
{
    public class AES
    {

        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            try
            {
                using (var rijndaelManaged = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC, KeySize = 256 })
                using (var memoryStream = new MemoryStream(cipherText))
                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
            catch (CryptographicException e)
            {
                return null;
            }
        }
    }
}
