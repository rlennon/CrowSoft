using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crowsoftmvc.Helpers
{
    public class SecurityHelper
    {
        public static string ToByteArrayToString(string input)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(input);
            var string_array = Convert.ToBase64String(byt);
            return string_array;
        }

        public static string ToString(byte[] input)
        {
            return Convert.ToBase64String(input);
        }
    }
}
