using System;
using System.Security.Cryptography;
using System.Text;

namespace _0_Framework.Application
{
    public class Encryption : IEncryption
    {
        private SymmetricAlgorithm CreateAlgorithm()
        {
            var rijndael = new RijndaelManaged
            {
                BlockSize = 128,
                KeySize = 128,
                Padding = PaddingMode.ISO10126,
                Mode = CipherMode.CBC
            };

            var rfc = new Rfc2898DeriveBytes("Vjy*!a49Z52yqF!zr2Os", Encoding.UTF8.GetBytes("1298LangoTop.ir1298"));

            rijndael.Key = rfc.GetBytes(rijndael.KeySize / 8);
            rijndael.IV = rfc.GetBytes(rijndael.BlockSize / 8);
            return rijndael;
        }

        public string Encrypt(string plainText)
        {
            var algorithm = CreateAlgorithm();

            var encryptor = algorithm.CreateEncryptor();

            var textByte = Encoding.UTF8.GetBytes(plainText);

            var cipherByte = encryptor.TransformFinalBlock(textByte, 0, textByte.Length);

            var cipherText = Convert.ToBase64String(cipherByte);
            return cipherText;
        }

        public string Decrypt(string cipherText)
        {
            var algorithm = CreateAlgorithm();

            var decryptor = algorithm.CreateDecryptor();

            var cipherByte = Convert.FromBase64String(cipherText);

            var plainTextBytes = decryptor.TransformFinalBlock(cipherByte, 0, cipherByte.Length);

            var plainText = Encoding.UTF8.GetString(plainTextBytes);
            return plainText;
        }
    }
}
