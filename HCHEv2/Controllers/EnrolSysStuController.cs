using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using HC.Core.Common.FileUpload;
using HCHEv2.pdfHelp;
using HC.Service.EnrolSys;
using HCHEv2.Models.Zkz;

namespace HCHEv2.Controllers
{
    public class EnrolSysStuController : Controller
    {



        #region fild
        private IEnrolSysService _iEnrolsysService;
        #endregion


        #region cto
        public EnrolSysStuController(IEnrolSysService ienrosysSercie)
        {
            this._iEnrolsysService = ienrosysSercie;
        }
        #endregion


        /// <summary>
        /// 先生成图片 再组装网页在生成PDF
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintDemoInPic(string id)
        {
            #region 判断是否存在
            if (_iEnrolsysService.getStudentInfoByCardID(id) == null)
            {
                return new EmptyResult();
            }
            #endregion


            string pngPath = getSavePicPathH(id);//生成图片
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            //從網址下載Html字串
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string htmlText = wc.DownloadString(url + "/EnrolSysStu/PrintZkzInPic/" + pngPath);
            byte[] pdfFile = this.ConvertHtmlTextToPDF(htmlText);

            return File(pdfFile, "application/pdf", "1111.pdf");
        }




        /// <summary>
        /// 將Html文字 輸出到PDF檔裡
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免當htmlText無任何html tag標籤的純文字時，轉PDF時會掛掉，所以一律加上<p>標籤
            //htmlText = "<p>" + htmlText + "</p>";

            MemoryStream outputStream = new MemoryStream();//要把PDF寫到哪個串流
            byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串轉成byte[]
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();//要寫PDF的文件，建構子沒填的話預設直式A4
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件預設開檔時的縮放為100%
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //開啟Document文件 
            doc.Open();
            //使用XMLWorkerHelper把Html parse到PDF檔裡
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //將pdfDest設定的資料寫到PDF檔
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();
            //回傳PDF檔案 
            return outputStream.ToArray();
        }



        /// <summary>
        /// 高质量存图 
        /// </summary>
        /// <returns>生成准考证图片地址</returns>
        public string getSavePicPathH(string id)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail(url + "/EnrolSysStu/GenerateZkzPng/"+id, 690, 955, 690, 955);

            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/png");//jpeg
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            // Save the bitmap as a JPEG file with quality level 75.
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;


            // Response.BinaryWrite(buff);
            //var ZkzPath = HttpRuntime.AppDomainAppPath.ToString() + "ZhunKaoZheng";
            var ZkzPath = "ZhunKaoZheng";
            CreateDir(ZkzPath);
            var picName = Guid.NewGuid()+".png";
            //m_Bitmap.Save(HttpRuntime.AppDomainAppPath.ToString() + ZkzPath + "\\aa.png", System.Drawing.Imaging.ImageFormat.Png);
            m_Bitmap.Save(HttpRuntime.AppDomainAppPath.ToString() + ZkzPath + "\\" + picName, myImageCodecInfo, myEncoderParameters);
            m_Bitmap.Dispose();
            return picName;
        }


        #region 生成图片辅助方法

        public static void CreateDir(string dir)
        {
            if (dir.Length == 0) return;
            if (!Directory.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
                Directory.CreateDirectory(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
        }

        /// <summary>
        /// 获取bitmap图片编码
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        #endregion



        /// <summary>
        /// 准考证图片原型页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateZkzPng(string id)
        {
            var StuModel= _iEnrolsysService.getStudentInfoByCardID(id);
            var model = new ZkzInfo();
            model.stuInfo = StuModel;
            model.ZkzNo = "10697WS273";
            model.ExamNo = "25";
            model.RoomNo = "3308";
            model.SiteNo = "3";
            model.ExamPlace = "西北大学桃园校区（高新四路15号）";
            model.ExamTime = "2016年 3月20日 9：00-12：00";
            model.SubjectAndTime = "其中：9：00-10：00 语文； 10：00-11：00 数学； 11：00-12：00 英语；";
            return View(model);                        
        }



        /// <summary>
        /// 准考查询入口页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InquirieZkz()
        {
            return View();
        }

        /// <summary>
        /// 准考证查询POST页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InquirieZkz(FormCollection form)
        {
            var queryStr = form["CardIdOrRegistNo"];
            //var queryStr = Request["CardIdOrRegistNo"];

            #region 判断是否存在
            var stuModel = _iEnrolsysService.getStudentInfoByCardID(queryStr);
            #endregion

            if (stuModel == null)
            {
                ViewBag.mes = "查无此信息";
                return View();
            }
            else
            {
                var model = new ZkzInfo();
                model.stuInfo = stuModel;
                model.ZkzNo = "10697WS273";
                model.ExamNo = "25";
                model.RoomNo = "3308";
                model.SiteNo = "3";
                model.ExamPlace = "西北大学桃园校区（高新四路15号）";
                model.ExamTime = "2016年 3月20日 9：00-12：00";
                model.SubjectAndTime = "其中：9：00-10：00 语文； 10：00-11：00 数学； 11：00-12：00 英语；";
                return View("PreviewZkz", model);
            }
          

        }



        /// <summary>
        /// 准考证预览页面 包含下载准考证
        /// </summary>
        /// <returns></returns>
        public ActionResult PreviewZkz()
        {
            return View();
        }






        /// <summary>
        /// 准考证打印html输出页面 嵌入一张生成的准考证图片的html
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PrintZkzInPic(string id)
        {
            ViewBag.id = id;
            return View();
        }





        #region  报名入口
        [HttpGet]
        public ActionResult StuRegister()
        {
            ViewBag.AddResult = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StuRegister(HCHEv2.Models.stuRegist.StuRegistInfo model)
        {
            ViewBag.AddResult = false;
            string upPhotePath = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Files["Sphoto"];

                    #region 图片上传和验证
                    if (file != null)
                    {
                        imgUpload imgUpLoad = new imgUpload();//上传图片的类   
                        upPhotePath = imgUpLoad.picUpLoad(file, "BmStuPic");
                    }
                    else
                    {
                        ModelState.AddModelError("noPhoto", "请上传1寸免冠照片");
                        return View(model);
                    }
                    if (upPhotePath == "0" || upPhotePath == "1")
                    {
                        string errorMsg = "";
                        switch (upPhotePath)
                        {
                            case "0":
                                errorMsg = "图片格式错误，请重新上传";
                                break;
                            case "1":
                                errorMsg = "图片大小超出1M，请重新上传";
                                break;
                        }
                        ModelState.AddModelError("noPhoto", errorMsg);
                        return View(model);
                    }
                   
                    #endregion


                    #region 保存报名学生
                    var student = new HC.Data.Models.StudentInfo()
                    {
                        FatherPhone = model.FatherPhone,
                        major = model.major,
                        MotherPhone = model.MotherPhone,
                        ReceiveName = model.ReceiveName,
                        ReceivePhone = model.ReceivePhone,
                        registerNo = model.registerNo,
                        RegisterTime = DateTime.Now,
                        Scategory = model.Scategory,
                        SchoolName = model.SchoolName,
                        SeflPhone = model.SeflPhone,
                        SelfCardNo = model.SelfCardNo,
                        SendAddress = model.SendAddress,
                        Sex = model.Sex,
                        Sfrom = model.Sfrom,
                        Sphoto = upPhotePath,
                        StudentName = model.StudentName,
                        Stype = model.Stype,
                        TelNum = model.TelNum,
                        ZipCode = model.ZipCode
                    };


                    #region 重复校验
                    if (_iEnrolsysService.isExsit(student))
                    {
                        ModelState.AddModelError("noPhoto", "您的信息已存在，请勿重复提交！");
                        return View(model);
                    }
                    #endregion


                    _iEnrolsysService.AddStudentInfo(student);
                    #endregion

                    ViewBag.AddResult = true;
                }
                catch
                {

                }
                return View();
            }
            else
            {
                return View(model);
            }

        }
        #endregion
    }
}
