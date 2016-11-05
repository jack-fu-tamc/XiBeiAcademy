using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// IP_TABLES:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class IP_TABLES
	{
		public IP_TABLES()
		{}
		#region Model
		private string _isbeginip;
		private string _isendip;
		private string _address;
		private string _comp;
		private int? _isbeginip_num;
		private int? _isendip_num;
		private string _provinceid;
		private string _province;
		private string _cityid;
		private string _city;
		private string _areaid;
		private string _area;
		/// <summary>
		/// 
		/// </summary>
		public string isbeginip
		{
			set{ _isbeginip=value;}
			get{return _isbeginip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string isendip
		{
			set{ _isendip=value;}
			get{return _isendip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string comp
		{
			set{ _comp=value;}
			get{return _comp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isbeginip_num
		{
			set{ _isbeginip_num=value;}
			get{return _isbeginip_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isendip_num
		{
			set{ _isendip_num=value;}
			get{return _isendip_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string provinceid
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cityid
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string city
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string areaid
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string area
		{
			set{ _area=value;}
			get{return _area;}
		}
		#endregion Model

	}
}

