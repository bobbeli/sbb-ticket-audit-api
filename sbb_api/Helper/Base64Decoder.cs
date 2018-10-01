using System;
using System.Collections.Generic;
using System.Linq;

namespace sbb_api.Helper
{
    public class Base64Decoder
    {
        public static String Decode(String base64EncodedData)
        {
            return IsBase64String(base64EncodedData);
        }

        // All allowed charactes in for a Base64Encoding String
        static readonly HashSet<char> _base64Characters = new HashSet<char>() 
        {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f',
                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/',
                '='
        };

        /* This Method Checks wheter a String is Base64 valid. 
         * If not, it shortens the string till its valid... :)
         */
        public static string IsBase64String(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NullReferenceException("String is Empty");
            }
            else if (value.Any(c => !_base64Characters.Contains(c)))
            {
                char unallowedChar = value.First(c => !_base64Characters.Contains(c));

                int indexOfUnallowedChar = value.IndexOf(unallowedChar);

                value = value.Substring(0, indexOfUnallowedChar) + "==";

                IsBase64String(value);

            }

            try
            {
                byte[] res = Convert.FromBase64String(value);

                return System.Text.Encoding.UTF8.GetString(res);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);

            }
        }

    }
}
