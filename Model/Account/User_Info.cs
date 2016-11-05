using System;
using System.Collections.Generic;
namespace Maticsoft.Model
{
    /// <summary>
    /// User_Info:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class User_Info
    {
        public User_Info()
        { }
        #region Model
        private int _id;
        private string _username;
        private string _userpwd;
        private string _Address;
        private bool _isAdmin;
        private string _email;
        private string _Mobile;
        private string _realname;
        private bool _effctive;
        private DateTime _passWordTime;
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPwd
        {
            set { _userpwd = value; }
            get { return _userpwd; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _Address = value; }
            get { return _Address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isAdmin
        {
            set { _isAdmin = value; }
            get { return _isAdmin; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        
        /// <summary>
		/// 
		/// </summary>
        public string Mobile
		{
            set { _Mobile = value; }
            get { return _Mobile; }
		}
       

        /// <summary>
        /// 
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }


        public bool effctive
        {
            set { _effctive = value; }
            get { return _effctive; }
        }

        public DateTime passWordTime
        {
            set { _passWordTime = value; }
            get { return _passWordTime; }
        }
        #endregion Model

    }
}