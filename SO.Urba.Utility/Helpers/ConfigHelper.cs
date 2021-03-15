using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace SO.Utility.Helpers
{
    public class ConfigHelper
    {
        public static bool showAds()
        {
            string str = getAppSetting("showAds");
            if (str != null && str == "true") return true;
            return false;
           
        }

        public static string getAppSetting(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch { return null;  }

        }
    }
}
