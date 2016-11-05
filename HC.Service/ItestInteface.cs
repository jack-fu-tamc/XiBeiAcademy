using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HC.Service
{
    public interface ItestInteface
    {
        int getInt(int i);

        DataTable GetUserInfoList(string p_trantype, string P_Para);

    }
}
