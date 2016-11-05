using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HC.Data.ViewModels
{
    public class RegisterModel
    {
        
        [Required(ErrorMessage="用户名必填")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "注意长度为6~10位")]
        public string Uname { get; set; }



       [StringLength(10, MinimumLength = 6, ErrorMessage = "注意长度为6~10位")]
        [Required(ErrorMessage="密码必填")]
        public string Pwd { get; set; }


        [StringLength(10, MinimumLength = 6, ErrorMessage = "注意长度为6~10位")]
        [Required(ErrorMessage = "密码必填")]
        public string PwdConfirm { get; set; }
    }
}
