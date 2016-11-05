using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// ServiceLocationType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ServiceLocationType
	{
		public ServiceLocationType()
		{}
		#region Model
		private int _id;
		private string _typename;
		private int? _datastatus;
		private int? _updateby;
        private DateTime _updatedt = Convert.ToDateTime(System.DateTime.Now);
		/// <summary>
		/// auto_increment
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DataStatus
		{
			set{ _datastatus=value;}
			get{return _datastatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UpdateBy
		{
			set{ _updateby=value;}
			get{return _updateby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateDT
		{
			set{ _updatedt=value;}
			get{return _updatedt;}
		}
		#endregion Model

	}
}

