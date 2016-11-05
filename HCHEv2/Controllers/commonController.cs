using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Web.Framework.UI.Captcha;

namespace HCHEv2.Controllers
{
    public class commonController : Controller
    {
        public ActionResult commonHeader()
        {

            return View();
        }


        public ActionResult commonFooter()
        {
            return View();
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult generateCaptcha()
        {
            System.Drawing.FontFamily family = new System.Drawing.FontFamily("Arial");
            CaptchaImage img = new CaptchaImage(88, 36, family);
            string text = img.CreateRandomText(5);
            img.SetText(text);
            img.GenerateImage();
            var imgName = Guid.NewGuid().ToString();
            img.Image.Save(Server.MapPath("/Content/captcha/") + imgName + ".png", System.Drawing.Imaging.ImageFormat.Png); 
            Session["captchaText"] = text;
            var catModel = new
            {
                src = "/Content/captcha/" + imgName + ".png?t=" + DateTime.Now.Ticks
                //capt = text
            };
            return new JsonResult() { Data = catModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
