
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections;

namespace HC.Core.CommonMethod
{
    /// <summary>
    /// 构建通用函数
    /// </summary>
    public class UntilMethod
    {
        //建立加密对象的密钥和偏移量   
        static byte[] key = { 55, 103, 246, 79, 36, 99, 167, 3 };//定义密钥    

        #region 获取图片地址
        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <returns></returns>
        //public static string GetPicUrl()
        //{
        //    return ConfigurationManager.AppSettings["PicUrl"];
        //}
        #endregion

        #region MD5加密
        /// <summary>   
        /// MD5加密
        /// 操作人：余博 时间：2015-05-06
        /// 修改备注：新增函数
        /// </summary>   
        /// <param name="strSource">需要加密的字符串</param>   
        /// <returns>MD5加密后的字符串</returns>   
        public static string Md5Encrypt(string strSource)
        {

            var enPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5");
           var result = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(enPwd+"dzxx", "MD5");
          
           return result;
            ////把字符串放到byte数组中   
            //byte[] bytIn = System.Text.Encoding.Default.GetBytes(strSource);
            ////实例DES加密类   
            //DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();
            //mobjCryptoService.Key = key;
            //mobjCryptoService.IV = key;
            //ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            ////实例MemoryStream流加密密文件   
            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            //cs.Write(bytIn, 0, bytIn.Length);
            //cs.FlushFinalBlock();
            //string strEncrypt = System.Convert.ToBase64String(ms.ToArray());
            //strEncrypt = strEncrypt.Replace("+", "%2B");
            ////strEncrypt = strEncrypt.Replace("==", "." + DateTime.Now.Date.ToString("yyyy.MM"));
            //strEncrypt = strEncrypt.Replace("==", ".");
            //return strEncrypt;
        }
        # endregion

        #region MD5解密
        /// <summary>   
        /// MD5解密
        /// 操作人：余博 时间：2015-05-06
        /// 修改备注：新增函数
        /// </summary>   
        /// <param name="Source">需要解密的字符串</param>   
        /// <returns>MD5解密后的字符串</returns>   
        public static string Md5Decrypt(string Source)
        {
            //将解密字符串转换成字节数组
            Source = HttpUtility.UrlDecode(Source);
            //Source = Source.Replace("." + DateTime.Now.Date.ToString("yyyy.MM"), "==");
            Source = Source.Replace(".", "==");
            byte[] bytIn = null;
            try
            {
                bytIn = System.Convert.FromBase64String(Source);
            }
            catch
            {
                string strEncrypt = Source.Replace("%2B", "+");
                strEncrypt = strEncrypt.Replace(" ", "+");
                bytIn = System.Convert.FromBase64String(strEncrypt);
            }
            DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();
            mobjCryptoService.Key = key;
            mobjCryptoService.IV = key;
            //实例流进行解密
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader strd = new StreamReader(cs, Encoding.Default);
            return strd.ReadToEnd();
        }
        #endregion

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// 操作人：余博 时间：2015-05-06
        /// 修改备注：新增函数
        /// </summary>
        /// <param name="logo">日志信息</param>
        /// <returns></returns>
        public void WirteLogo(string logo)
        {
            try
            {
                FileInfo fileinfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/") + "Log/logResult.txt");
                using (FileStream fs = fileinfo.OpenWrite())
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(DateTime.Now.ToString() + logo);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion

        #region 日期格式化（yyyy年MM月dd日 mm:ss）
        /// <summary>
        /// 日期格式化（yyyy年MM月dd日 mm:ss）
        /// 操作人：余博 时间：2015-05-06
        /// 修改备注：新增函数
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static string DateFormart(string date)
        {
            string datetime = string.Empty;
            if (!string.IsNullOrEmpty(date))
            {
                if (date.Length == 19)
                    datetime = date.Substring(0, 4) + "年" + date.Substring(5, 2) + "月" + date.Substring(8, 2) + "日 " + date.Substring(11, date.Length - 11);
                if (date.Length == 16)
                    datetime = date.Substring(0, 4) + "年" + date.Substring(5, 2) + "月" + date.Substring(8, 2) + "日 " + date.Substring(11, date.Length - 11);
                else if (date.Length == 10)
                    datetime = date.Substring(0, 4) + "年" + date.Substring(5, 2) + "月" + date.Substring(8, 2) + "日";
            }
            return datetime;
        }
        #endregion

        #region 前台页面显示的订单状态
        /// <summary>
        /// 前台页面显示的订单状态
        /// 操作人：余博 时间：2015-05-06
        /// 修改备注：新增函数
        /// </summary>
        /// <param name="billconf">订单状态</param>
        /// <param name="usertype">用户类型</param>
        /// <returns></returns>
        public string GetBuyerBillConf(string billconf, string usertype)
        {
            string status = string.Empty;
            switch (billconf)
            {
                case "X":
                    status = "等待提交订单";
                    break;
                case "P":
                    status = "等待买家付款";
                    break;
                case "Y":
                    status = "等待卖家发货";
                    break;
                case "C":
                    status = "等待买家确认收货";
                    break;
                case "S":
                    status = "交易成功";
                    break;
                case "J":
                    status = "已评论";
                    break;
                case "L":
                    status = "交易关闭";
                    break;
                default:
                    status = "订单生成失败";
                    break;
            }
            return status;
        }
        #endregion

        #region 前台页面显示的订单操作状态
        /// <summary>
        /// 前台页面显示的订单操作状态
        /// 操作人：余博 时间：2015-05-06
        /// 修改备注：新增函数
        /// </summary>
        /// <param name="billconf">订单状态</param>
        /// <param name="billid">订单号</param>
        /// <param name="partid">商品id</param>
        /// <returns></returns>
        public string GetOperateConf(string billconf, string billid, string partid)
        {
            string status = string.Empty;
            switch (billconf)
            {
                case "X":
                    status = "<input type=\"submit\"  onclick=\"javascript:window.open('../Pages/Shopping/OrderInfo.aspx?custom_order_no=" + billid + "')\"  value=\"提交订单\" runat=\"server\" />";
                    break;
                case "P":
                    status = "<input type=\"submit\" onclick=\"javascript:window.open('../Pages/Shopping/OrderSuccess.aspx?custom_order_no=" + billid + "')\"  value=\"去支付\" runat=\"server\" /><span  class=\"CtroOrder\"><a href=\"#\" onclick=\"CancelOrder('" + billid + "')\">取消订单</a></span>";
                    break;
                case "Y":
                    status = "<a href=\"javascript:void(0);\">待发货</a><br/><a href=\"/UserManageCenter/advice.aspx?custom_order_no=" + billid + "\">投诉卖家</a>";
                    break;
                case "C":
                    status = "<input type=\"submit\"  onclick=\"javascript:window.open('https://auth.alipay.com')\" value=\"确认收货\" runat=\"server\" /><a href=\"/UserManageCenter/advice.aspx?custom_order_no=" + billid + "\">投诉卖家</a>";
                    break;
                case "S":
                    status = "<input type=\"button\" onclick=\"window.location.href='/Comment/AddComment.aspx?custom_order_no=" + billid + "&part_no=" + partid + "'\"  value=\"评论\" />";
                    break;
                case "J":
                    status = "<a href=\"/Comment/AddComment.aspx?part_no=" + partid + "\">追加评论</a>";
                    break;
                case "L":
                    break;
                default:
                    status = "<a href=\"#\" target=\"_blank\">订单生成失败</a>";
                    break;
            }
            return status;
        }
        #endregion

        #region 获取默认城市
        /// <summary>
        /// 获取默认城市
        /// </summary>
        /// <param name="nodetype">层级</param>
        /// <returns></returns>
        public string GetDefaultAddressByIP(string nodetype)
        {
            string address = string.Empty;

            ////获取本机IP
            //string hostAddress = HttpContext.Current.Request.UserHostAddress;

            //// 字符串转换为数字
            //if (hostAddress == "::1") hostAddress = "1.83.146.197";
            //string strhostAddress = "SELECT iptonum_function('" + hostAddress + "') hostaddress FROM dual";
            //DataSet dshostAddress = xm.getDataSet(strhostAddress);
            //string hostaddress = dshostAddress.Tables[0].Rows[0]["hostaddress"].ToString();

            ////根据IP数字获取地址
            //StringBuilder strddress = new StringBuilder();
            //strddress.Append(" SELECT");
            //if (nodetype == "1") strddress.Append(" nvl(province,'') address");
            //else strddress.Append(" nvl(city,'') address");
            //strddress.Append(" FROM IP_TABLES");
            //strddress.Append(" WHERE " + hostaddress + " BETWEEN isbeginip_num AND isendip_num");
            //DataSet dsAddress = xm.getDataSet(strddress.ToString());

            //if (dsAddress.Tables[0].Rows.Count > 0)
            //    address = dsAddress.Tables[0].Rows[0]["address"].ToString();

            return address;
        }
        #endregion

        public static string appsetingValue(string paraName)
        {
            //#region 服务及路径相关
            ////图片服务器路径
            //if (paraName == "PicUrl")
            //{
            //    ServerPatch sp = Maticsoft.Model.Common.GetServerPatch();
            //    return sp.imageURL;
            //}
            ////SSO地址
            //if (paraName == "ssoAddress")
            //{
            //    ServerPatch sp = Maticsoft.Model.Common.GetServerPatch();
            //    return sp.SsoAddress;
            //}
            ////帮我养车地址
            //if (paraName == "runCarAdress")
            //{
            //    ServerPatch sp = Maticsoft.Model.Common.GetServerPatch();
            //    return sp.RunCarAdress;
            //}
            //// 主站地址
            //if (paraName == "mastSiteAddress")
            //{
            //    ServerPatch sp = Maticsoft.Model.Common.GetServerPatch();
            //    return sp.MastSiteAddress;
            //}
            //#endregion

            //#region 图片所在文件夹


            //#endregion

            return "";
            //return ConfigurationManager.AppSettings[paraName];
        }


        #region 给卖家发送短信提醒
        /// <summary>
        /// 给卖家发送短信提醒
        /// </summary>
        /// <param name="corpcomp">公司名称</param>
        /// <param name="accName">商品名称</param>
        /// <param name="memo">购买状态</param>
        /// <param name="order_no">订单号</param>
        /// <param name="userPhone">商家手机号</param>
        public static void SendMsgToMer(string corpcomp, string accName, string memo, string order_no, string userPhone)
        {
            //string account = Maticsoft.Model.Common.SmsAccount; //用户名
            //string password = Maticsoft.Model.Common.SmsPassword; //密码
            //string status = accName + memo;
            //string content = "亲爱的" + corpcomp + "，您店铺的" + status + "。订单号:" + order_no;
            //sms sms1 = new sms();
            //SubmitResult SubmitResult1 = sms1.Submit(account, password, userPhone, content);
        }
        #endregion


        #region 汉字拼音首字母
        /// <summary> 
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /// </summary> 
        /// <param name="CnChar">单个汉字</param> 
        /// <returns>单个大写字母</returns> 
        public static string GetCharSpellCode(string CnChar)
        {
            var expChast = new Hashtable();
            expChast.Add("昊", "H");
            expChast.Add("岱", "D");
            expChast.Add("禅", "C");
            expChast.Add("讴", "O");
            expChast.Add("雪", "X");
            expChast.Add("睿", "R");
            expChast.Add("缤", "B");
            expChast.Add("伽", "J");
            expChast.Add("骐", "Q");
            expChast.Add("选", "X");
            expChast.Add("氙", "X");
            expChast.Add("耦", "O");
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                // get the array of byte from the single char 
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            //expresstion 
            //table of the constant list 
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 

            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 

            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //没有U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            // iCnChar match the constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else
            {
                if (expChast.ContainsKey(CnChar))
                {
                    return expChast[CnChar].ToString();
                }
                else
                {
                    return ("?");
                }


            }
        }

        #endregion




        public static string getAppSettingValue(string para)
        {

            var xmlHelper = new HC.Core.Common.XMLHealper();
            return xmlHelper.GetXmlNodeValue("//root/" + para).Trim();

            //return ConfigurationManager.AppSettings[para];
        }


        public static void writeAppSettingValue(string para, string value)
        {

            //System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/");
            //AppSettingsSection app = config.AppSettings;
            ////app.Settings["indexPicAddress"].Value = @"g:\";
            //app.Settings[para].Value = value;
            //config.Save(ConfigurationSaveMode.Modified);

            var xmlHelper = new HC.Core.Common.XMLHealper();
            xmlHelper.SetXmlNodeValue("//root/" + para, value);
            xmlHelper.SavexmlDocument();


        }





        #region  C#版DES加解密方法及相关函数

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="beinetstr">待加密的字符串</param>
        /// <param name="beinetkey">密钥</param>
        /// <returns></returns>
        public string EncryptDES(string beinetstr, string beinetkey)
        {
            if (string.IsNullOrEmpty(beinetkey))
                return string.Empty;

            return stringToHex(des(beinetkey, beinetstr, true, false, string.Empty));
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="beinetstr">待解密的字符串</param>
        /// <param name="beinetkey">密钥</param>
        /// <returns></returns>
        public string DecryptDES(string beinetstr, string beinetkey)
        {
            if (string.IsNullOrEmpty(beinetkey))
                return null;
            string ret = des(beinetkey, HexTostring(beinetstr), false, false, string.Empty);
            return ret;
        }

        /// <summary>
        /// 把字符串转换为16进制字符串
        /// 如：a变成61（即10进制的97）；abc变成616263
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string stringToHex(string s)
        {
            string r = "";
            string[] hexes = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            for (int i = 0; i < (s.Length); i++)
            {
                r += hexes[RM(s[i], 4)] + hexes[s[i] & 0xf];
            }
            return r;
        }

        /// <summary>
        /// 16进制字符串转换为字符串
        /// 如：61（即10进制的97）变成a；616263变成abc
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string HexTostring(string s)
        {
            string ret = string.Empty;

            for (int i = 0; i < s.Length; i += 2)
            {
                int sxx = Convert.ToInt32(s.Substring(i, 2), 16);
                ret += (char)sxx;
            }
            return ret;
        }

        /// <summary>
        /// 带符号位右移（类似于js的>>>）
        /// </summary>
        /// <param name="a">用于右移的操作数</param>
        /// <param name="bit">右移位数</param>
        /// <returns></returns>
        public int RM(int a, int bit)
        {
            unchecked
            {
                uint b = (uint)a;
                b = b >> bit;
                return (int)b;
            }
        }

        /// <summary>
        /// 加解密主调方法
        /// </summary>
        /// <param name="beinetkey">密钥</param>
        /// <param name="message">加密时为string，解密时为byte[]</param>
        /// <param name="encrypt">true：加密；false：解密</param>
        /// <param name="mode">true：CBC mode；false：非CBC mode</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        private string des(string beinetkey, string message, bool encrypt, bool mode, string iv)
        {
            //declaring this locally speeds things up a bit
            long[] spfunction1 = { 0x1010400, 0, 0x10000, 0x1010404, 0x1010004, 0x10404, 0x4, 0x10000, 0x400, 0x1010400, 0x1010404, 0x400, 0x1000404, 0x1010004, 0x1000000, 0x4, 0x404, 0x1000400, 0x1000400, 0x10400, 0x10400, 0x1010000, 0x1010000, 0x1000404, 0x10004, 0x1000004, 0x1000004, 0x10004, 0, 0x404, 0x10404, 0x1000000, 0x10000, 0x1010404, 0x4, 0x1010000, 0x1010400, 0x1000000, 0x1000000, 0x400, 0x1010004, 0x10000, 0x10400, 0x1000004, 0x400, 0x4, 0x1000404, 0x10404, 0x1010404, 0x10004, 0x1010000, 0x1000404, 0x1000004, 0x404, 0x10404, 0x1010400, 0x404, 0x1000400, 0x1000400, 0, 0x10004, 0x10400, 0, 0x1010004 };
            long[] spfunction2 = { -0x7fef7fe0, -0x7fff8000, 0x8000, 0x108020, 0x100000, 0x20, -0x7fefffe0, -0x7fff7fe0, -0x7fffffe0, -0x7fef7fe0, -0x7fef8000, -0x80000000, -0x7fff8000, 0x100000, 0x20, -0x7fefffe0, 0x108000, 0x100020, -0x7fff7fe0, 0, -0x80000000, 0x8000, 0x108020, -0x7ff00000, 0x100020, -0x7fffffe0, 0, 0x108000, 0x8020, -0x7fef8000, -0x7ff00000, 0x8020, 0, 0x108020, -0x7fefffe0, 0x100000, -0x7fff7fe0, -0x7ff00000, -0x7fef8000, 0x8000, -0x7ff00000, -0x7fff8000, 0x20, -0x7fef7fe0, 0x108020, 0x20, 0x8000, -0x80000000, 0x8020, -0x7fef8000, 0x100000, -0x7fffffe0, 0x100020, -0x7fff7fe0, -0x7fffffe0, 0x100020, 0x108000, 0, -0x7fff8000, 0x8020, -0x80000000, -0x7fefffe0, -0x7fef7fe0, 0x108000 };
            long[] spfunction3 = { 0x208, 0x8020200, 0, 0x8020008, 0x8000200, 0, 0x20208, 0x8000200, 0x20008, 0x8000008, 0x8000008, 0x20000, 0x8020208, 0x20008, 0x8020000, 0x208, 0x8000000, 0x8, 0x8020200, 0x200, 0x20200, 0x8020000, 0x8020008, 0x20208, 0x8000208, 0x20200, 0x20000, 0x8000208, 0x8, 0x8020208, 0x200, 0x8000000, 0x8020200, 0x8000000, 0x20008, 0x208, 0x20000, 0x8020200, 0x8000200, 0, 0x200, 0x20008, 0x8020208, 0x8000200, 0x8000008, 0x200, 0, 0x8020008, 0x8000208, 0x20000, 0x8000000, 0x8020208, 0x8, 0x20208, 0x20200, 0x8000008, 0x8020000, 0x8000208, 0x208, 0x8020000, 0x20208, 0x8, 0x8020008, 0x20200 };
            long[] spfunction4 = { 0x802001, 0x2081, 0x2081, 0x80, 0x802080, 0x800081, 0x800001, 0x2001, 0, 0x802000, 0x802000, 0x802081, 0x81, 0, 0x800080, 0x800001, 0x1, 0x2000, 0x800000, 0x802001, 0x80, 0x800000, 0x2001, 0x2080, 0x800081, 0x1, 0x2080, 0x800080, 0x2000, 0x802080, 0x802081, 0x81, 0x800080, 0x800001, 0x802000, 0x802081, 0x81, 0, 0, 0x802000, 0x2080, 0x800080, 0x800081, 0x1, 0x802001, 0x2081, 0x2081, 0x80, 0x802081, 0x81, 0x1, 0x2000, 0x800001, 0x2001, 0x802080, 0x800081, 0x2001, 0x2080, 0x800000, 0x802001, 0x80, 0x800000, 0x2000, 0x802080 };
            long[] spfunction5 = { 0x100, 0x2080100, 0x2080000, 0x42000100, 0x80000, 0x100, 0x40000000, 0x2080000, 0x40080100, 0x80000, 0x2000100, 0x40080100, 0x42000100, 0x42080000, 0x80100, 0x40000000, 0x2000000, 0x40080000, 0x40080000, 0, 0x40000100, 0x42080100, 0x42080100, 0x2000100, 0x42080000, 0x40000100, 0, 0x42000000, 0x2080100, 0x2000000, 0x42000000, 0x80100, 0x80000, 0x42000100, 0x100, 0x2000000, 0x40000000, 0x2080000, 0x42000100, 0x40080100, 0x2000100, 0x40000000, 0x42080000, 0x2080100, 0x40080100, 0x100, 0x2000000, 0x42080000, 0x42080100, 0x80100, 0x42000000, 0x42080100, 0x2080000, 0, 0x40080000, 0x42000000, 0x80100, 0x2000100, 0x40000100, 0x80000, 0, 0x40080000, 0x2080100, 0x40000100 };
            long[] spfunction6 = { 0x20000010, 0x20400000, 0x4000, 0x20404010, 0x20400000, 0x10, 0x20404010, 0x400000, 0x20004000, 0x404010, 0x400000, 0x20000010, 0x400010, 0x20004000, 0x20000000, 0x4010, 0, 0x400010, 0x20004010, 0x4000, 0x404000, 0x20004010, 0x10, 0x20400010, 0x20400010, 0, 0x404010, 0x20404000, 0x4010, 0x404000, 0x20404000, 0x20000000, 0x20004000, 0x10, 0x20400010, 0x404000, 0x20404010, 0x400000, 0x4010, 0x20000010, 0x400000, 0x20004000, 0x20000000, 0x4010, 0x20000010, 0x20404010, 0x404000, 0x20400000, 0x404010, 0x20404000, 0, 0x20400010, 0x10, 0x4000, 0x20400000, 0x404010, 0x4000, 0x400010, 0x20004010, 0, 0x20404000, 0x20000000, 0x400010, 0x20004010 };
            long[] spfunction7 = { 0x200000, 0x4200002, 0x4000802, 0, 0x800, 0x4000802, 0x200802, 0x4200800, 0x4200802, 0x200000, 0, 0x4000002, 0x2, 0x4000000, 0x4200002, 0x802, 0x4000800, 0x200802, 0x200002, 0x4000800, 0x4000002, 0x4200000, 0x4200800, 0x200002, 0x4200000, 0x800, 0x802, 0x4200802, 0x200800, 0x2, 0x4000000, 0x200800, 0x4000000, 0x200800, 0x200000, 0x4000802, 0x4000802, 0x4200002, 0x4200002, 0x2, 0x200002, 0x4000000, 0x4000800, 0x200000, 0x4200800, 0x802, 0x200802, 0x4200800, 0x802, 0x4000002, 0x4200802, 0x4200000, 0x200800, 0, 0x2, 0x4200802, 0, 0x200802, 0x4200000, 0x800, 0x4000002, 0x4000800, 0x800, 0x200002 };
            long[] spfunction8 = { 0x10001040, 0x1000, 0x40000, 0x10041040, 0x10000000, 0x10001040, 0x40, 0x10000000, 0x40040, 0x10040000, 0x10041040, 0x41000, 0x10041000, 0x41040, 0x1000, 0x40, 0x10040000, 0x10000040, 0x10001000, 0x1040, 0x41000, 0x40040, 0x10040040, 0x10041000, 0x1040, 0, 0, 0x10040040, 0x10000040, 0x10001000, 0x41040, 0x40000, 0x41040, 0x40000, 0x10041000, 0x1000, 0x40, 0x10040040, 0x1000, 0x41040, 0x10001000, 0x40, 0x10000040, 0x10040000, 0x10040040, 0x10000000, 0x40000, 0x10001040, 0, 0x10041040, 0x40040, 0x10000040, 0x10040000, 0x10001000, 0x10001040, 0, 0x10041040, 0x41000, 0x41000, 0x1040, 0x1040, 0x40040, 0x10000000, 0x10041000 };


            //create the 16 or 48 subkeys we will need
            int[] keys = des_createKeys(beinetkey);
            int m = 0;
            int i, j;
            int temp, right1, right2, left, right;
            int[] looping;
            int cbcleft = 0, cbcleft2 = 0, cbcright = 0, cbcright2 = 0;
            int endloop;
            int loopinc;
            int len = message.Length;
            int chunk = 0;
            //set up the loops for single and triple des
            var iterations = keys.Length == 32 ? 3 : 9;//single or triple des
            if (iterations == 3)
            {
                looping = encrypt ? new int[] { 0, 32, 2 } : new int[] { 30, -2, -2 };
            }
            else { looping = encrypt ? new int[] { 0, 32, 2, 62, 30, -2, 64, 96, 2 } : new int[] { 94, 62, -2, 32, 64, 2, 30, -2, -2 }; }

            if (encrypt)
            {
                message += "\0\0\0\0\0\0\0\0";//pad the message out with null bytes
            }
            //store the result here
            //List<byte> result = new List<byte>();
            //List<byte> tempresult = new List<byte>();
            string result = string.Empty;
            string tempresult = string.Empty;

            if (mode)
            {//CBC mode
                int[] tmp = { 0, 0, 0, 0, 0, 0, 0, 0 };
                int pos = 24;
                int iTmp = 0;
                while (m < iv.Length && iTmp < tmp.Length)
                {
                    if (pos < 0)
                        pos = 24;
                    tmp[iTmp++] = iv[m++] << pos;
                    pos -= 8;
                }
                cbcleft = tmp[0] | tmp[1] | tmp[2] | tmp[3];
                cbcright = tmp[4] | tmp[5] | tmp[6] | tmp[7];

                //cbcleft = (iv[m++] << 24) | (iv[m++] << 16) | (iv[m++] << 8) | iv[m++];
                //cbcright = (iv[m++] << 24) | (iv[m++] << 16) | (iv[m++] << 8) | iv[m++];
                m = 0;
            }

            //loop through each 64 bit chunk of the message
            while (m < len)
            {
                if (encrypt)
                {/*加密时按双字节操作*/
                    left = (message[m++] << 16) | message[m++];
                    right = (message[m++] << 16) | message[m++];
                }
                else
                {
                    left = (message[m++] << 24) | (message[m++] << 16) | (message[m++] << 8) | message[m++];
                    right = (message[m++] << 24) | (message[m++] << 16) | (message[m++] << 8) | message[m++];
                }
                //for Cipher Block Chaining mode,xor the message with the previous result
                if (mode)
                {
                    if (encrypt)
                    {
                        left ^= cbcleft; right ^= cbcright;
                    }
                    else
                    {
                        cbcleft2 = cbcleft; cbcright2 = cbcright; cbcleft = left; cbcright = right;
                    }
                }

                //first each 64 but chunk of the message must be permuted according to IP
                temp = (RM(left, 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
                temp = (RM(left, 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
                temp = (RM(right, 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
                temp = (RM(right, 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

                left = ((left << 1) | RM(left, 31));
                right = ((right << 1) | RM(right, 31));

                //do this either 1 or 3 times for each chunk of the message
                for (j = 0; j < iterations; j += 3)
                {
                    endloop = looping[j + 1];
                    loopinc = looping[j + 2];
                    //now go through and perform the encryption or decryption 
                    for (i = looping[j]; i != endloop; i += loopinc)
                    {//for efficiency
                        right1 = right ^ keys[i];
                        right2 = (RM(right, 4) | (right << 28)) ^ keys[i + 1];
                        //the result is attained by passing these bytes through the S selection functions
                        temp = left;
                        left = right;
                        right = (int)(temp ^ (spfunction2[RM(right1, 24) & 0x3f] | spfunction4[RM(right1, 16) & 0x3f] | spfunction6[RM(right1, 8) & 0x3f] | spfunction8[right1 & 0x3f] | spfunction1[RM(right2, 24) & 0x3f] | spfunction3[RM(right2, 16) & 0x3f] | spfunction5[RM(right2, 8) & 0x3f] | spfunction7[right2 & 0x3f]));
                    }
                    temp = left; left = right; right = temp;//unreverse left and right
                }//for either 1 or 3 iterations

                //move then each one bit to the right
                left = (RM(left, 1) | (left << 31));
                right = (RM(right, 1) | (right << 31));

                //now perform IP-1,which is IP in the opposite direction
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
                temp = (RM(right, 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
                temp = (RM(right, 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
                temp = (RM(left, 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
                temp = (RM(left, 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);

                //for Cipher Block Chaining mode,xor the message with the previous result
                if (mode)
                {
                    if (encrypt)
                    {
                        cbcleft = left; cbcright = right;
                    }
                    else
                    {
                        left ^= cbcleft2; right ^= cbcright2;
                    }
                }
                //int[] arrInt;
                if (encrypt)
                {
                    //arrInt = new int[]{ RM(left, 24), (RM(left, 16) & 0xff), (RM(left, 8) & 0xff), (left & 0xff), RM(right, 24), (RM(right, 16) & 0xff), (RM(right, 8) & 0xff), (right & 0xff) };
                    tempresult += String.Concat((char)RM(left, 24),
                        (char)(RM(left, 16) & 0xff),
                        (char)(RM(left, 8) & 0xff),
                        (char)(left & 0xff),
                        (char)RM(right, 24),
                        (char)(RM(right, 16) & 0xff),
                        (char)(RM(right, 8) & 0xff),
                        (char)(right & 0xff));
                }
                else
                {
                    // 解密时，最后一个字符如果是\0，去除
                    //arrInt = new int[] { (RM(left, 16) & 0xffff), (left & 0xffff), (RM(right, 16) & 0xffff), (right & 0xffff) };
                    int tmpch = (RM(left, 16) & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    tmpch = (left & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    tmpch = (RM(right, 16) & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    tmpch = (right & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    //tempresult += String.Concat((char)(RM(left, 16) & 0xffff),
                    //    (char)(left & 0xffff),
                    //    (char)(RM(right, 16) & 0xffff),
                    //    (char)(right & 0xffff));
                }/*解密时输出双字节*/
                //byte[] arrByte = new byte[arrInt.Length];
                //for (int loop = 0; loop < arrInt.Length; loop++)
                //{
                //    tempresult.Add(byte.Parse(arrInt[loop].ToString()));
                //    //arrByte[loop] = byte.Parse(arrInt[loop].ToString());
                //}
                //tempresult.Add(arrByte;// System.Text.Encoding.Unicode.GetString(arrByte);

                chunk += encrypt ? 16 : 8;
                if (chunk == 512)
                {
                    //result.AddRange(tempresult);tempresult.Clear(); 
                    result += tempresult; tempresult = string.Empty;
                    chunk = 0;
                }
            }//for every 8 characters,or 64 bits in the message

            //return the result as an array

            //result.AddRange(tempresult);
            //return result.ToArray();
            return result + tempresult;
        }//end of des

        /// <summary>
        /// this takes as input a 64 bit beinetkey(even though only 56 bits are used)
        /// as an array of 2 integers,and returns 16 48 bit keys
        /// </summary>
        /// <param name="beinetkey"></param>
        /// <returns></returns>
        private int[] des_createKeys(string beinetkey)
        {
            //declaring this locally speeds things up a bit
            int[] pc2bytes0 = { 0, 0x4, 0x20000000, 0x20000004, 0x10000, 0x10004, 0x20010000, 0x20010004, 0x200, 0x204, 0x20000200, 0x20000204, 0x10200, 0x10204, 0x20010200, 0x20010204 };
            int[] pc2bytes1 = { 0, 0x1, 0x100000, 0x100001, 0x4000000, 0x4000001, 0x4100000, 0x4100001, 0x100, 0x101, 0x100100, 0x100101, 0x4000100, 0x4000101, 0x4100100, 0x4100101 };
            int[] pc2bytes2 = { 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808, 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808 };
            int[] pc2bytes3 = { 0, 0x200000, 0x8000000, 0x8200000, 0x2000, 0x202000, 0x8002000, 0x8202000, 0x20000, 0x220000, 0x8020000, 0x8220000, 0x22000, 0x222000, 0x8022000, 0x8222000 };
            int[] pc2bytes4 = { 0, 0x40000, 0x10, 0x40010, 0, 0x40000, 0x10, 0x40010, 0x1000, 0x41000, 0x1010, 0x41010, 0x1000, 0x41000, 0x1010, 0x41010 };
            int[] pc2bytes5 = { 0, 0x400, 0x20, 0x420, 0, 0x400, 0x20, 0x420, 0x2000000, 0x2000400, 0x2000020, 0x2000420, 0x2000000, 0x2000400, 0x2000020, 0x2000420 };
            int[] pc2bytes6 = { 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002, 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002 };
            int[] pc2bytes7 = { 0, 0x10000, 0x800, 0x10800, 0x20000000, 0x20010000, 0x20000800, 0x20010800, 0x20000, 0x30000, 0x20800, 0x30800, 0x20020000, 0x20030000, 0x20020800, 0x20030800 };
            int[] pc2bytes8 = { 0, 0x40000, 0, 0x40000, 0x2, 0x40002, 0x2, 0x40002, 0x2000000, 0x2040000, 0x2000000, 0x2040000, 0x2000002, 0x2040002, 0x2000002, 0x2040002 };
            int[] pc2bytes9 = { 0, 0x10000000, 0x8, 0x10000008, 0, 0x10000000, 0x8, 0x10000008, 0x400, 0x10000400, 0x408, 0x10000408, 0x400, 0x10000400, 0x408, 0x10000408 };
            int[] pc2bytes10 = { 0, 0x20, 0, 0x20, 0x100000, 0x100020, 0x100000, 0x100020, 0x2000, 0x2020, 0x2000, 0x2020, 0x102000, 0x102020, 0x102000, 0x102020 };
            int[] pc2bytes11 = { 0, 0x1000000, 0x200, 0x1000200, 0x200000, 0x1200000, 0x200200, 0x1200200, 0x4000000, 0x5000000, 0x4000200, 0x5000200, 0x4200000, 0x5200000, 0x4200200, 0x5200200 };
            int[] pc2bytes12 = { 0, 0x1000, 0x8000000, 0x8001000, 0x80000, 0x81000, 0x8080000, 0x8081000, 0x10, 0x1010, 0x8000010, 0x8001010, 0x80010, 0x81010, 0x8080010, 0x8081010 };
            int[] pc2bytes13 = { 0, 0x4, 0x100, 0x104, 0, 0x4, 0x100, 0x104, 0x1, 0x5, 0x101, 0x105, 0x1, 0x5, 0x101, 0x105 };

            //how many iterations(1 for des,3 for triple des)
            int iterations = beinetkey.Length >= 24 ? 3 : 1;
            //stores the return keys
            int[] keys = new int[32 * iterations];
            //now define the left shifts which need to be done
            int[] shifts = { 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0 };
            //other variables
            int left, right;
            int lefttemp;
            int righttemp;
            int m = 0, n = 0;
            int temp;

            for (int j = 0; j < iterations; j++)
            {//either 1 or 3 iterations
                int[] tmp = { 0, 0, 0, 0, 0, 0, 0, 0 };
                int pos = 24;
                int iTmp = 0;
                while (m < beinetkey.Length && iTmp < tmp.Length)
                {
                    if (pos < 0)
                        pos = 24;
                    tmp[iTmp++] = beinetkey[m++] << pos;
                    pos -= 8;
                }
                left = tmp[0] | tmp[1] | tmp[2] | tmp[3];
                right = tmp[4] | tmp[5] | tmp[6] | tmp[7];

                //left = (beinetkey[m++] << 24) | (beinetkey[m++] << 16) | (beinetkey[m++] << 8) | beinetkey[m++];
                //right = (beinetkey[m++] << 24) | (beinetkey[m++] << 16) | (beinetkey[m++] << 8) | beinetkey[m++];

                temp = (RM(left, 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
                temp = (RM(right, -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
                temp = (RM(left, 2) ^ right) & 0x33333333; right ^= temp; left ^= (temp << 2);
                temp = (RM(right, -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
                temp = (RM(right, 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

                //the right side needs to be shifted and to get the last four bits of the left side
                temp = (left << 8) | (RM(right, 20) & 0x000000f0);
                //left needs to be put upside down
                left = (right << 24) | ((right << 8) & 0xff0000) | (RM(right, 8) & 0xff00) | (RM(right, 24) & 0xf0);
                right = temp;

                //now go through and perform these shifts on the left and right keys
                for (int i = 0; i < shifts.Length; i++)
                {
                    //shift the keys either one or two bits to the left
                    if (shifts[i] == 1)
                    {
                        left = (left << 2) | RM(left, 26); right = (right << 2) | RM(right, 26);
                    }
                    else
                    {
                        left = (left << 1) | RM(left, 27); right = (right << 1) | RM(right, 27);
                    }
                    left &= -0xf; right &= -0xf;

                    //now apply PC-2,in such a way that E is easier when encrypting or decrypting
                    //this conversion will look like PC-2 except only the last 6 bits of each byte are used
                    //rather than 48 consecutive bits and the order of lines will be according to 
                    //how the S selection functions will be applied:S2,S4,S6,S8,S1,S3,S5,S7
                    lefttemp = pc2bytes0[RM(left, 28)] | pc2bytes1[RM(left, 24) & 0xf]
                   | pc2bytes2[RM(left, 20) & 0xf] | pc2bytes3[RM(left, 16) & 0xf]
                   | pc2bytes4[RM(left, 12) & 0xf] | pc2bytes5[RM(left, 8) & 0xf]
                   | pc2bytes6[RM(left, 4) & 0xf];
                    righttemp = pc2bytes7[RM(right, 28)] | pc2bytes8[RM(right, 24) & 0xf]
                   | pc2bytes9[RM(right, 20) & 0xf] | pc2bytes10[RM(right, 16) & 0xf]
                   | pc2bytes11[RM(right, 12) & 0xf] | pc2bytes12[RM(right, 8) & 0xf]
                   | pc2bytes13[RM(right, 4) & 0xf];
                    temp = (RM(righttemp, 16) ^ lefttemp) & 0x0000ffff;
                    keys[n++] = lefttemp ^ temp; keys[n++] = righttemp ^ (temp << 16);
                }
            }//for each iterations
            //return the keys we"ve created
            return keys;
        }//end of des_createKeys

        #endregion

    }

}
