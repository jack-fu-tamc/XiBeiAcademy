using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HC.Service
{
    public class testIntefaceService : ItestInteface
    {

        public int getInt(int i)
        {
            return i;
        }
        public DataTable GetUserInfoList(string p_trantype, string P_Para)
        {
            return null;
        }
    }
}
