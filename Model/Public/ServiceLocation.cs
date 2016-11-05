using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// ServiceLocation:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ServiceLocation
    {
        public ServiceLocation()
        { }
        #region Model
        private int _id;
        private int? _servicetype;
        private string _servicename;
        private string _province;
        private int? _provincecode;
        private string _city;
        private int? _citycode;
        private string _area;
        private int? _areacode;
        private string _town;
        private int? _towncode;
        private string _address;
        private double? _longitude;
        private double? _latitude;
        private int? _updateby;
        private DateTime _updatedt = Convert.ToDateTime(System.DateTime.Now);
        private int _dataStatus;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ServiceType
        {
            set { _servicetype = value; }
            get { return _servicetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName
        {
            set { _servicename = value; }
            get { return _servicename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProvinceCode
        {
            set { _provincecode = value; }
            get { return _provincecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CityCode
        {
            set { _citycode = value; }
            get { return _citycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Town
        {
            set { _town = value; }
            get { return _town; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TownCode
        {
            set { _towncode = value; }
            get { return _towncode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? longitude
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? latitude
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UpdateBy
        {
            set { _updateby = value; }
            get { return _updateby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateDT
        {
            set { _updatedt = value; }
            get { return _updatedt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataStatus
        {
            set { _dataStatus = value; }
            get { return _dataStatus; }
        }
        #endregion Model

    }
}

