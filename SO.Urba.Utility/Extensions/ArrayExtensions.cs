using System.Text;
using System;

namespace SO.Utility
{
    public static class ArrayExtensions
    {


        public static string Join(this string[] ar, string seperator)
        {
            string[] value = ar;
            return String.Join(seperator, value);
           
        }

       

    }
}