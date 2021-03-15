using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SO.Utility.Extensions
{
    public static class ObjectExtensions
    {

      

        public static IDictionary<string, string> ToDictionaryStringString(this object source, 
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => (propInfo.GetValue(source, null)==null ? null : propInfo.GetValue(source, null).ToString())
            );

        }

        public static IDictionary<string, object> ToDictionary(this object source,
          BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo =>  propInfo.GetValue(source, null)
            );

        }
     

    }
}