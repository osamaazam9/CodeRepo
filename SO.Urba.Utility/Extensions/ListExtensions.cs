using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SO.Utility.Extensions
{
    public static class ListExtensions
    {

      
        public static List<T> convertTo<T>(this List<object> list)
        {
            if (list == null)
                return null;
            var result = list.ConvertAll<T>(delegate(object obj) { return (T)obj; });
            return result;
        }

     
     

    }
}