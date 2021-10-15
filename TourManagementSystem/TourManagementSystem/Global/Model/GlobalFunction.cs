using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourManagementSystem.Global.Model
{
    public static class GlobalFunction
    {
        /*
         * 
         */

        /*
         * Base64Encode to encode text by Base64
         */
        public static string Base64Encode(string plainText)
        {
            if (plainText != String.Empty)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            return String.Empty;
        }

        /*
         * Security Password with MD5
         */
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /*
         * Convert Image from byte[] to bitmap
         */
        public static BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
