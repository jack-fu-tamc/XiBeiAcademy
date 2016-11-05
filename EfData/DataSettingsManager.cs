using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ResposityOfEf
{
    public class DataSettingsManager
    {
        public static string connectionStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            }
        }
    }
}
