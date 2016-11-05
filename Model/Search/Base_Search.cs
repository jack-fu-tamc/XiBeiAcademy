using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.Model
{
    public class Base_Search
    {
        private int _iall = 0;
        private int _iPageCount = Common.iPageCount;
        private int _iPageSize = Common.iPageSize;

         public int Iall
        {
            get { return _iall; }
            set { _iall = value; }
        }
        public int iPageCount
        {
            set { _iPageCount = value; }
            get { return _iPageCount; }
        }
        public int iPageSize
        {
            set { _iPageSize = value; }
            get { return _iPageSize; }
        }
    }

    /// <summary>
    /// 搜索简化类
    /// </summary>
    public class SBase_Search
    {
        #region
        private int _iall = 0;
        private int _iPageCount = Common.iPageCount;
        private int _iPageSize = Common.iPageSize;
        //private int _TypeID;//专区ID
        //private string _Style;//控制服务展示样式
        #endregion

        private int _trantype;//操作类型
        private string _userid = string.Empty;//店铺ID
        private int _TypeID;//专区ID
        private int _Id;//服务ID
        private double _P_PriceL = 0.00;//价格区间下限
        private double _P_PriceH = 0.00;//价格区间上限
        //private int _AddressCode;//城市Code
        private int _ProvinceCode;//省Code
        private int _CityCode;//城市Code
        private int _order;//排序   0 综合 1 销量 2价格升序 3价格降序       
        private string _str;//模糊查询用关键字
        private int _CarBrand_ID;//车品牌ID
        private string _Style;//控制服务展示样式

        #region
        public int Iall
        {
            get { return _iall; }
            set { _iall = value; }
        }
        public int iPageCount
        {
            set { _iPageCount = value; }
            get { return _iPageCount; }
        }
        public int iPageSize
        {
            set { _iPageSize = value; }
            get { return _iPageSize; }
        }
        //public int TypeID
        //{
        //    set { _TypeID = value; }
        //    get { return _TypeID; }
        //}
        //public string Style
        //{
        //    get { return _Style; }
        //    set { _Style = value; }
        //}
        #endregion
        public string Userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        public int TypeID
        {
            set { _TypeID = value; }
            get { return _TypeID; }
        }
        public int Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        public double P_PriceL
        {
            set { _P_PriceL = value; }
            get { return _P_PriceL; }
        }
        public double P_PriceH
        {
            set { _P_PriceH = value; }
            get { return _P_PriceH; }
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
        public int order
        {
            set { _order = value; }
            get { return _order; }
        }
        public int trantype
        {
            set { _trantype = value; }
            get { return _trantype; }
        }
        public string str
        {
            set { _str = value; }
            get { return _str; }
        }
        public int CarBrand_ID
        {
            set { _CarBrand_ID = value; }
            get { return _CarBrand_ID; }
        }
        public string Style
        {
            get { return _Style; }
            set { _Style = value; }
        }
    }
}
