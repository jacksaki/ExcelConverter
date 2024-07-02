using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConverter.Extensions
{
    public static class StringExtension
    {

        public static string ToSha256(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value).ToSha256();
        }

        public static string ToSha256(this byte[] value)
        {
            var sha256 = SHA256.Create();

            var bytes = sha256.ComputeHash(value);
            sha256.Clear();
            return string.Join("", bytes.Select(x => $"{x:x2}"));
        }

        public static string UrlEncode(this string value)
        {
            return System.Web.HttpUtility.UrlEncode(value);
        }

        public static string UrlDeocde(this string value)
        {
            return System.Web.HttpUtility.UrlDecode(value);
        }

        public static string Base64Encode(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
        }

        public static byte[] Base64Decode(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new byte[] { };
            }
            return Convert.FromBase64String(value);
        }

        public static string ToSnakeCase(this string text, bool toUpper)
        {
            var regex = new System.Text.RegularExpressions.Regex("[a-z][A-Z]");
            var result = regex.Replace(text, s => $"{s.Groups[0].Value[0]}_{s.Groups[0].Value[1]}");
            return toUpper ? result.ToUpper() : result.ToLower();
        }

        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            var sb = new System.Text.StringBuilder();
            var chars = value.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 0)
                {
                    sb.Append(chars[i].ToString().ToUpper());
                }
            }
            return sb.ToString();
        }
    }
}
