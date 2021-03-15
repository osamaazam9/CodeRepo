using System.Text;
using System;
using System.Collections;

namespace SO.Utility
{
    public static class StringExtensions
    {
        

        public static string[] Split(this string s, string splitChar)
        {
            char[] ar = splitChar.ToCharArray();

            return s.Split(ar);
        }

      

        public static string[] RemoveAt(this string[] parts, int i)
        {
            ArrayList a = new ArrayList(parts);
            a.RemoveAt(i);
            return a.ToStringArray();
        }

        public static string SubstringAfter(this string value, string keyword)
        {
            string result = null;

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(keyword))
            {
                if (value.Contains(keyword))
                {
                    result = value.Substring(value.IndexOf(keyword) + keyword.Length);
                }
            }
            return result;
        }

     

    }
}