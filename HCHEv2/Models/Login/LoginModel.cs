using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HC.Core.Extension;

namespace HCHEv2.Models.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码必填")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        //[Display(Name = "记住我?")]
        //public bool RememberMe { get; set; }

        [Required(ErrorMessage = "验证码必填")]
        //[captEqual(messageZD = "验证码错误")]
        public string CaptchaCode { get; set; }
    }
}