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
        public string Stype { get; set; }
        public string Scategory { get; set; }
        public string Sfrom { get; set; }

        [Required(ErrorMessage="学校名称必填")]
        public string SchoolName { get; set; }
        public string SelfCardNo { get; set; }
        public string SeflPhone { get; set; }
        public string FatherPhone { get; set; }
        public string MotherPhone { get; set; }
        public string TelNum { get; set; }
        public string major { get; set; }
        public string SendAddress { get; set; }
        public string ZipCode { get; set; }
        public string ReceiveName { get; set; }
        public string ReceivePhone { get; set; }
        public string Sphoto { get; set; }
        public string registerNo { get; set; }
        public string StudentName { get; set; }
    }
}