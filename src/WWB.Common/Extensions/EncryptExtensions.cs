using System.Security.Cryptography;
using System.Text;

namespace WWB.Common.Extensions
{
    public static class EncryptExtensions
    {
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lowerCase"></param>
        /// <returns></returns>
        public static string ToMd5(this string txt, bool lowerCase = true)
        {
            var format = lowerCase ? "x2" : "X2";
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(txt);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString(format));
                }

                return sb.ToString();
            }
        }
    }
}