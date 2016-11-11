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
        public string SelfCardNo { get; set; }


        [Required(ErrorMessage = "手机号必填")]
        public string SeflPhone { get; set; }


        [Required(ErrorMessage = "父亲手机号必填")]
        public string FatherPhone { get; set; }


        [Required(ErrorMessage = "母亲手机号必填")]
        public string MotherPhone { get; set; }


        [Required(ErrorMessage = "固话必填")]
        public string TelNum { get; set; }


        [Required(ErrorMessage = "专业必选")]
        public string major { get; set; }


        [Required(ErrorMessage = "邮寄地址必填")]
        public string SendAddress { get; set; }


        [Required(ErrorMessage = "邮编必填")]
        public string ZipCode { get; set; }


        [Required(ErrorMessage = "收件人必填")]
        public string ReceiveName { get; set; }


        [Required(ErrorMessage = "收件人手机号必填")]
        public string ReceivePhone { get; set; }


        //public string Sphoto { get; set; }

        [Required(ErrorMessage = "高考证号必填")]
        public string registerNo { get; set; }


        [Required(ErrorMessage = "姓名必填")]
        public string StudentName { get; set; }
    }
}