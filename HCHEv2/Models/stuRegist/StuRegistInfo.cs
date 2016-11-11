using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HC.Core.Extension;

namespace HCHEv2.Models.stuRegist
{
    public class StuRegistInfo
    {
        [Required(ErrorMessage = "性别必填")]       
        public string Sex { get; set; }


        [Required(ErrorMessage = "学生类型必选")]
        public string Stype { get; set; }


        [Required(ErrorMessage = "科别必选")]
        public string Scategory { get; set; }


        [Required(ErrorMessage = "生源地必选")]
        public string Sfrom { get; set; }



        [Required(ErrorMessage="学校名称必填")]
        public string SchoolName { get; set; }


        [Required(ErrorMessage = "身份证号必填")]
        [RegularExpression(@"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessage = "注意输入15或18位身份证号")]
        public string SelfCardNo { get; set; }


        [Required(ErrorMessage = "手机号必填")]
        [RegularExpression(@"^1[3458]\d{9}$", ErrorMessage = "注意手机格式")]
        public string SeflPhone { get; set; }


        [Required(ErrorMessage = "父亲手机号必填")]
        [RegularExpression(@"^1[34578]\d{9}$", ErrorMessage = "注意手机格式")]
        public string FatherPhone { get; set; }


        [Required(ErrorMessage = "母亲手机号必填")]
        [RegularExpression(@"^1[34578]\d{9}$", ErrorMessage = "注意手机格式")]
        public string MotherPhone { get; set; }


        [Required(ErrorMessage = "固话必填")]        
        public string TelNum { get; set; }


        [Required(ErrorMessage = "专业必选")]
        public string major { get; set; }


        [Required(ErrorMessage = "邮寄地址必填")]
        public string SendAddress { get; set; }


        [Required(ErrorMessage = "邮编必填")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能为数字")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "注意长度为6位")]

        public string ZipCode { get; set; }


        [Required(ErrorMessage = "收件人必填")]
        public string ReceiveName { get; set; }


        [Required(ErrorMessage = "收件人手机号必填")]
        [RegularExpression(@"^1[3458]\d{9}$", ErrorMessage = "注意手机格式")]
        public string ReceivePhone { get; set; }


        //public string Sphoto { get; set; }

        [Required(ErrorMessage = "高考证号必填")]
        public string registerNo { get; set; }


        [Required(ErrorMessage = "姓名必填")]
        public string StudentName { get; set; }
    }
}