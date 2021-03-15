using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SO.Utility.Extensions
{
    public static class DictionaryExtensions
    {


        public static object getValue(this Dictionary<string, object> list, string key)
        {
            try
            {
                object value;
                bool hasValue = list.TryGetValue(key, out value);
                return value;
            }
            catch { return null;  }
        }

      
     

    }
}