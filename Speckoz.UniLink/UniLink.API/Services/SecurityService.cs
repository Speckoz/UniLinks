using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UniLink.API.Services
{
    public class SecurityService
    {
        private readonly IConfiguration _configuration;

        public SecurityService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Criptografa um determinado texto para um hash usando SHA256, o uso deste metodo é recomendado para as senhas.
        /// </summary>
        /// <param name="password">É o texto que que deseja criptografar, no caso, uma senha crua.</param>
        public static string EncryptToSHA256(string password)
        {
            using (var sha = new SHA256Managed())
            {
                var builder = new StringBuilder();

                sha.ComputeHash(new UTF8Encoding()
                    .GetBytes(password)).ToList()
                    .ForEach(a => builder.Append(a.ToString("x2")));

                return builder.ToString();
            }
        }

        public static string EncryptToMD5(string password)
        {
            using (var md5 = MD5.Create())
            {
                var builder = new StringBuilder();
                md5.ComputeHash(new UTF8Encoding()
                    .GetBytes(password)).ToList()
                    .ForEach(a => builder.Append(a.ToString("x2")));

                return builder.ToString();
            }
        }

        /// <param name="key">[SecurityKey] no AppSettings.</param>
        public string EncryptText(string text, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;

                byte[] aesKey = new byte[32];
                Array.Copy(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 32);
                aes.Key = aesKey;
                aes.IV = InitVector();

                using (var memoryStream = new MemoryStream())
                {
                    ICryptoTransform crypto = aes.CreateEncryptor();

                    using (var cryptoStream = new CryptoStream(memoryStream, crypto, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(text);

                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);

                        cryptoStream.FlushFinalBlock();

                        byte[] cipherBytes = memoryStream.ToArray();

                        return Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
                    }
                }
            }
        }

        /// <param name="key">[SecurityKey] no AppSettings.</param>
        public bool TryDecryptText(string text, string key, out string result)
        {
            try
            {
                using (var aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;

                    byte[] aesKey = new byte[32];
                    Array.Copy(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 32);
                    aes.Key = aesKey;
                    aes.IV = InitVector();

                    using (var memoryStream = new MemoryStream())
                    {
                        ICryptoTransform decrypto = aes.CreateDecryptor();

                        var cryptoStream = new CryptoStream(memoryStream, decrypto, CryptoStreamMode.Write);
                        byte[] cipherBytes = Convert.FromBase64String(text);

                        cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                        cryptoStream.FlushFinalBlock();

                        byte[] plainBytes = memoryStream.ToArray();

                        result = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);

                        return true;
                    }
                }
            }
            catch
            {
                result = null;
                return false;
            }
        }

        private byte[] InitVector() => _configuration.GetSection("InitVector").Get<byte[]>();
    }
}