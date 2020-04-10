using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FODfinder.Utility
{
    public class ServingSizeCleaner
    {
        public static string Clean(string ServingSize)
        {
            if (ServingSize == "")
            {
                return ServingSize;
            }
            if (ServingSize.Contains(" ONZ"))
            {
                return ServingSize.Replace(" ONZ", " oz");
            }
            if (ServingSize.Contains(" OZA"))
            {
                return ServingSize.Replace(" OZA", " fl oz");
            }
            if (ServingSize.Contains("("))
            {
                return ServingSize.Substring(0, ServingSize.IndexOf('(')).ToLower();
            }
            return ServingSize.ToLower();
        }
    }
}