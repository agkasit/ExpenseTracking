using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class StringFormat
    {
        public static string substring(string str, int length) {
            try
            {
                if (str.Length > 30)
                {
                    return str.Substring(0, 30);
                }
                else {
                    return str;
                }
                
            }
            catch (Exception ex) {
                return string.Empty;
            }
        }
    }
}
