using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// ips_ip:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ips_ip
	{
		public ips_ip()
		{}
		#region Model
		private string _ip_start;
		private string _ip_end;
		private string _region;
		private string _comments;
		/// <summary>
		/// 
		/// </summary>
		public string ip_start
		{
			set{ _ip_start=value;}
			get{return _ip_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ip_end
		{
			set{ _ip_end=value;}
			get{return _ip_end;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string region
		{
			set{ _region=value;}
			get{return _region;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string comments
		{
			set{ _comments=value;}
			get{return _comments;}
		}
		#endregion Model

	}
}

