using System;
using System.Collections.Generic;
namespace HC.Data.Models
{
    public partial class StudentInfo : BaseEntity
    {
        public int StudentID { get; set; }
        public string Sex { get; set; }
        public string Stype { get; set; }
        public string Scategory { get; set; }
        public string Sfrom { get; set; }
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
        public System.DateTime RegisterTime { get; set; }
        public string StudentName { get; set; }
    }
} 
