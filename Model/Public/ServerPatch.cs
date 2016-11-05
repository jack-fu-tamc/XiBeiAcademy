using System;
namespace Maticsoft.Models
{
    public class ServerPatch
    {
        public ServerPatch()
        { }
        private string _imageURL; //浏览图片的相对地址
        private string _ImageHomePath; //上传时物理路径   

        private string _ssoAddress; //  SSO地址
        private string _runCarAdress; //帮我养车地址   
        private string _mastSiteAddress; //主站地址

        public string imageURL
        {
            set { _imageURL = value; }
            get { return _imageURL; }
        }
        public string ImageHomePath
        {
            set { _ImageHomePath = value; }
            get { return _ImageHomePath; }
        }
        public string SsoAddress
        {
            set { _ssoAddress = value; }
            get { return _ssoAddress; }
        }
        public string RunCarAdress
        {
            set { _runCarAdress = value; }
            get { return _runCarAdress; }
        }
        public string MastSiteAddress
        {
            set { _mastSiteAddress = value; }
            get { return _mastSiteAddress; }
        }



    }
}