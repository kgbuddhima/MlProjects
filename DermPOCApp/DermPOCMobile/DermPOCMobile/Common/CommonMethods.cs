using System;
using System.Collections.Generic;
using System.Text;

namespace DermPOCMobile.Common
{
    public static class CommonMethods
    {
        public static string GetString(float value) 
        {
            return value.ToString("##.##");
        }
    }
}
