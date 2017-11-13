using System.Security.Cryptography;
using System.Text;

namespace OtomatikMuhendis.Kutuphane.Web.Helpers
{
    public class CryptographyHelper
    {
        /// <summary>
        /// The following code example computes the MD5 hash value of a string and returns the hash as a 32-character, hexadecimal-formatted string.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>32-character, hexadecimal-formatted hash string</returns>
        public static string GetMd5Hash(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}
