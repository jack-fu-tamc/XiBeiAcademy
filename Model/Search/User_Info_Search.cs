using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.Model
{
    public class User_Info_Search : Base_Search
    {
        private string _UserName = "";
        private int _ProvinceCode = 0;
        private int _CityCode = 0;
        private int _DataStatus = 1;
        private int _Order = 0;
        //下面2个是对于查询实体时使用，上面是针对列表使用
        private string _trantype;//ByName,ByID 
        private string _para;//根据_trantype确定是Name还是ID


        public string UserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }
        public int ProvinceCode
        {
            set { _ProvinceCode = value; }
            get { return _ProvinceCode; }
        }
        public int CityCode
        {
            set { _CityCode = value; }
            get { return _CityCode; }
        }
        public int DataStatus
        {
            set { _DataStatus = value; }
            get { return _DataStatus; }
        }
        public int Order
        {
            set { _Order = value; }
            get { return _Order; }
        }

        public string trantype
        {
            set { _trantype = value; }
            get { return _trantype; }
        }
        public string para
        {
            set { _para = value; }
            get { return _para; }
        }


    }
}
