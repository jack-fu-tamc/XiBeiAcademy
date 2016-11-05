using Maticsoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using Maticsoft.Model;

namespace Maticsoft.Model
{
    public static class Common
    {
        #region 支付宝相关
        //合作身份者ID，以2088开头由16位纯数字组成的字符串
        public const string partner = "2088811454132324";
        //交易安全检验码，由数字和字母组成的32位字符串   //商户的私钥
        public const string key = "hcq5zbvhmzhmj8qbv4x3288uyk8lvbqb";
        //字符编码格式 目前支持 gbk 或 utf-8
        public const string input_charset = "utf-8";
        //签名方式，选择项：RSA、DSA、MD5
        public const string sign_type = "MD5";
        //付款方式，担保交易
        public const string strPartner = "create_partner_trade_by_buyer";
        //付款方式，担即时到账
        public const string strDirect = "create_direct_pay_by_user";
        #endregion
        #region 短信发送相关
        public const string SmsAccount = "cf_autobobo";
        public const string SmsPassword = "AutoboboHche";
        #endregion
        #region 默认初始化数据
        //默认页数
        public const int iPageCount = 0;
        //默认页面数据量
        public const int iPageSize = 100;

        public static ServerPatch GetServerPatch()
        {
            ServerPatch sp = new ServerPatch();
            //图片路径相关
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"D:\Project\HCHE\ImagePatch.xml");
            xmlDoc.Load(ConfigurationSettings.AppSettings["XMLURL"] + @"ServerPatch.xml");
            XmlNode root = xmlDoc.SelectSingleNode("PublichPatch");//指向根节点

            //浏览图片的相对地址
            XmlNode xnImageURL = root.SelectSingleNode("ImageURL");
            sp.imageURL = xnImageURL.InnerText;//读出里面的值 
            //上传时物理路径
            XmlNode xnImageHomePath = root.SelectSingleNode("ImageHomePath");
            sp.ImageHomePath = xnImageHomePath.InnerText;//读出里面的值 
            //SSO地址
            XmlNode xnSsoAddress = root.SelectSingleNode("SsoAddress");
            sp.SsoAddress = xnSsoAddress.InnerText;//读出里面的值 
            //帮我养车地址
            XmlNode xnRunCarAdress = root.SelectSingleNode("RunCarAdress");
            sp.RunCarAdress = xnRunCarAdress.InnerText;//读出里面的值 
            //主站地址
            XmlNode xnMastSiteAddress = root.SelectSingleNode("MastSiteAddress");
            sp.MastSiteAddress = xnMastSiteAddress.InnerText;//读出里面的值 

            return sp;
        }


        public static ImagePatch GetImageURL()
        {
            ImagePatch ip = new ImagePatch();
            //图片路径相关
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"D:\Project\HCHE\ImagePatch.xml");
            xmlDoc.Load(ConfigurationSettings.AppSettings["XMLURL"] + @"ImagePatch.xml");
            XmlNode root = xmlDoc.SelectSingleNode("PublichPatch");//指向根节点

            //头像
            XmlNode xnImageUserPhotoSmall = root.SelectSingleNode("ImageUserPhotoSmall");
            ip.ImageUserPhotoSmall = xnImageUserPhotoSmall.InnerText;//读出里面的值 
            XmlNode xnImageUserPhotoBig = root.SelectSingleNode("ImageUserPhotoBig");
            ip.ImageUserPhotoBig = xnImageUserPhotoBig.InnerText;//读出里面的值 
            //商品主图
            XmlNode xnAccMainImage = root.SelectSingleNode("AccMainImage");
            ip.AccMainImage = xnAccMainImage.InnerText;//读出里面的值 
            //商品图片
            XmlNode xnAccImage = root.SelectSingleNode("AccImage");
            ip.AccImage = xnAccImage.InnerText;//读出里面的值 
            //我的车型图片
            XmlNode xnImageMyCar = root.SelectSingleNode("ImageMyCar");
            ip.ImageMyCar = xnImageMyCar.InnerText;//读出里面的值 
            //配件品牌
            XmlNode xnImageBrandLogo = root.SelectSingleNode("ImageBrandLogo");
            ip.ImageBrandLogo = xnImageBrandLogo.InnerText;//读出里面的值 
            //求购信息图片
            XmlNode xnImageRepair = root.SelectSingleNode("ImageRepair");
            ip.ImageRepair = xnImageRepair.InnerText;//读出里面的值 
            //报修信息图片
            XmlNode xnImageProcurment = root.SelectSingleNode("ImageProcurment");
            ip.ImageProcurment = xnImageProcurment.InnerText;//读出里面的值 
            //建议信息图片
            XmlNode xnImageAdvice = root.SelectSingleNode("ImageAdvice");
            ip.ImageAdvice = xnImageAdvice.InnerText;//读出里面的值 
            //投诉信息图片
            XmlNode xnImageComplaints = root.SelectSingleNode("ImageComplaints");
            ip.ImageComplaints = xnImageComplaints.InnerText;//读出里面的值 
            //举报信息图片   
            XmlNode xnImageProsecution = root.SelectSingleNode("ImageProsecution");
            ip.ImageProsecution = xnImageProsecution.InnerText;//读出里面的值 

            //授权证书 
            XmlNode xnImageCertification = root.SelectSingleNode("ImageCertificate");
            ip.ImageCertificate = xnImageCertification.InnerText;//读出里面的值 

            //店铺形象  
            XmlNode xnImageLince = root.SelectSingleNode("ImageStore");
            ip.ImageStore = xnImageLince.InnerText;//读出里面的值 
            return ip;
        }

        public static string GetDBConn()
        {
            //图片路径相关
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigurationSettings.AppSettings["XMLURL"] + @"Conn.xml");

            // 得到根节点
            XmlNode xn = xmlDoc.SelectSingleNode("Conn");
            // 得到根节点的所有子节点
            XmlNodeList xnl = xn.ChildNodes;
            string conn = xnl.Item(0).InnerText;

            return conn;
        }
        #endregion
    }
}
