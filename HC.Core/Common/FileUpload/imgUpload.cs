using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using HC.Core.Common.logHelper;
using HC.Core.CommonMethod;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HC.Core.Common.FileUpload
{

    /// <summary>
    /// 图片上传
    /// </summary>
    /// <returns></returns>
    public class imgUpload
    {
        public string picUpLoad(HttpPostedFileBase file, string folderName, int width, int height)
        {
            string fileName = System.Guid.NewGuid().ToString();
            string url = picUpLoad(file, folderName, fileName, width, height);
            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folderName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string picUpLoad(HttpPostedFileBase file, string folderName, string fileName, int width, int height)
        {
            var PicUrl = UntilMethod.appsetingValue("PicUrl");

            #region 设置本地临时路径和允许允许上传的文件格式与大小
            string fileTypes = "jpg,jpeg,png,bmp";
            int maxSize = 4096000;//大小限制4M 和web.config中一致
            #endregion

            #region 临时路径是否存在
            var TempfolderPath = HttpRuntime.AppDomainAppPath.ToString() + "uploadTemp";
            if (!Directory.Exists(TempfolderPath))
            {
                fileRWhelper.CreateDirectory(TempfolderPath);//创建路径
            }
            #endregion

            #region 裁剪并保存图片
            string fileExt = Path.GetExtension(file.FileName).ToLower();
            fileName = fileName + fileExt;
            string TempfilePath = TempfolderPath + "//" + fileName;
            if (width == 0 && height == 0)
            {
                Image image = Image.FromStream(file.InputStream);
                image.Save(TempfilePath);
            }
            else
            {
                GenerateThumbnailWarr(TempfilePath, width, height, file.InputStream);
            }
            #endregion

            #region 是否符合格式与大小
            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));
            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                //大小限制

            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                //格式限制

            }
            #endregion

            #region 先将图片保存至程序所在目录
            //string newFileName = Guid.NewGuid().ToString() + fileExt;
            //string TempfilePath = TempfolderPath + newFileName;
            //file.SaveAs(TempfilePath);
            #endregion

            #region 上传至图片服务器
            string filesdir = "//" + folderName + "//";

            String uploadUrl = PicUrl + "/Services/PicServices.aspx?PathDir=" + filesdir;
            String fileFormName = "file";
            String contenttype = "image/jpeg";
            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uploadUrl);
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(Path.GetFileName(fileName));
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");
            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            FileStream fileStream = new FileStream(TempfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            webrequest.ContentLength = length;
            Stream requestStream = webrequest.GetRequestStream();
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            byte[] buffer = new Byte[(int)fileStream.Length];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            requestStream.Close();
            fileStream.Close();
            fileStream.Dispose();

            Thread.Sleep(1000);
            string fileUrl = PicUrl + filesdir + fileName;
            #endregion

            #region 删除临时文件
            FileInfo info = new FileInfo(TempfilePath);
            info.Delete();
            #endregion

            fileUrl = fileUrl.Replace(PicUrl, "");
            return fileUrl;
        }

        #region 生成指定大小的图片
        /// <summary>  
        /// 生成指定大小的图片  
        /// </summary>  
        /// <param name="maxWidth">生成图片的最大宽度</param>  
        /// <param name="maxHeight">生成图片的最大高度</param>  
        /// <param name="imgFileStream">图片文件流对象</param>  
        public void GenerateThumbnailWarr(string fileName, int maxWidth, int maxHeight, Stream imgFileStream)
        {
            try
            {
                using (var img = Image.FromStream(imgFileStream))
                {
                    var bmp = new Bitmap(maxWidth, maxHeight);
                    //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。  
                    using (var gr = Graphics.FromImage(bmp))
                    {
                        //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality  
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        //下面这个也设成高质量  
                        gr.CompositingQuality = CompositingQuality.HighQuality;
                        //下面这个设成High         
                        gr.InterpolationMode = InterpolationMode.High;

                        //把原始图像绘制成上面所设置宽高的缩小图  
                        var rectDestination = new Rectangle(0, 0, maxWidth, maxHeight);
                        gr.DrawImage(img, rectDestination, 0, 0, img.Width, img.Height,
                                      GraphicsUnit.Pixel);
                        bmp.Save(fileName, ImageFormat.Jpeg);
                        bmp.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
        #endregion

        public string ExcelUpLoad(HttpPostedFileBase file)
        {


            #region 临时路径是否存在
            var TempfolderPath = HttpRuntime.AppDomainAppPath.ToString() + "HC.Manage" + "\\" + "Content" + "\\" + "uploadTemp" + "\\";
            if (!Directory.Exists(TempfolderPath))
            {
                fileRWhelper.CreateDirectory(TempfolderPath);//创建路径
            }
            #endregion

            #region 保存文件至临时文件目录
            string fileExt = Path.GetExtension(file.FileName).ToLower();
            string newFileName = Guid.NewGuid().ToString() + fileExt;
            string TempfilePath = TempfolderPath + newFileName;
            file.SaveAs(TempfilePath);
            #endregion
            return TempfilePath;
        }





        /// <summary>
        /// 保存图片到服务器
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folderName"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string picUpLoad(HttpPostedFileBase file, string folderName)
        {
            
            #region 设置本地临时路径和允许允许上传的文件格式与大小
            string fileTypes = "jpg,jpeg,png,bmp";
            int maxSize = 1050000;//大小限制1M 
            #endregion

            #region 临时路径是否存在
            var TempfolderPath = HttpRuntime.AppDomainAppPath.ToString() + folderName;//"\\Upfiles\\"+folderName;
            if (!Directory.Exists(TempfolderPath))
            {
                fileRWhelper.CreateDirectory(TempfolderPath);//创建路径
            }
            #endregion

            #region 图片格式验证
            string fileExt = Path.GetExtension(file.FileName).ToLower();            
            #endregion

            #region 是否符合格式与大小
            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));
            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                //大小限制
                return "1";

            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                //格式限制
                return "0";

            }
            #endregion

            #region 将图片保存至程序所在目录
            string newFileName = Guid.NewGuid().ToString() + fileExt;
            string TempfilePaths = TempfolderPath +"\\"+ newFileName;
            file.SaveAs(TempfilePaths);
            #endregion

            //var sbuStr = TempfilePaths.Replace('\\', '/');
            var subSta = TempfilePaths.IndexOf(folderName);
            var sbuStr = TempfilePaths.Substring(subSta).Replace('\\', '/');

            //var subSta=TempfilePaths.IndexOf("HCHEv2")+8;
            //var sbuStr=TempfilePaths.Substring(subSta).Replace('\\','/');
            return "/"+ sbuStr;
        }





        #region ueditor 文件上传辅助


        public static string GetUploadDirectory(string fileType,out string dir)
        {
            string directory = string.Empty;
            //if (System.IO.File.Exists(ConfigHelper.UploadFileRootDirectory))//判断是否绝对路径
            //{
            //    //如果是绝对路径
            //    // directory = ConfigHelper.UploadFileRootDirectory + "\\" + businessFlag.ToString() + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("yyyyMM");//月份文件夹物理路径
            //    directory = ConfigHelper.UploadFileRootDirectory + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("yyyyMM");//月份文件夹物理路径
            //}
            //else
            //{
            //    //如果是相对路径,先转换为绝对路径
            //    // directory = System.Web.HttpContext.Current.Server.MapPath(ConfigHelper.UploadFileRootDirectory) + "\\" + businessFlag.ToString() + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("yyyyMM") + "";//月份文件夹物理路径
            //    directory = System.Web.HttpContext.Current.Server.MapPath(ConfigHelper.UploadFileRootDirectory) + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("yyyyMM") + "";//月份文件夹物理路径
            //}
            dir = "/Upfiles/" + fileType + "/" + DateTime.Now.ToString("yyyyMM") + "";
            directory = System.Web.HttpContext.Current.Server.MapPath("/Upfiles") + "\\"+fileType+"\\" + DateTime.Now.ToString("yyyyMM") + "";

            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            return directory;
        }
        #endregion



    }
}