using System;
using System.Linq;

namespace sga.utils
{
    public static class Generator
    {
        public static string GenNumberRandom(int size)
        {
            Random randomNumber = new Random();
            int intValue;
            string str = "9";
            if (size == 0)
            {
                intValue = randomNumber.Next();
            }
            else
            {
                intValue = randomNumber.Next(0, int.Parse(str.PadLeft(size, '9')));
            }
            return intValue.ToString();
        }

        public static string GenStringRandom(int size)
        {
            const string chars = "!@#$%¨&*()ABCDEFGHIJLKMNOPQRSTUWXYZ1234567890abcdefghijlkmnopqrstuwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, size).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}