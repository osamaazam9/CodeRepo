using System.Text;
using System;
using System.Collections;

namespace SO.Utility
{
    public static class ArrayListExtensions
    {


        public static string[] ToStringArray(this ArrayList list)
        {
            return list.ToArray(typeof(string)) as string[];
        }

       

    }
}