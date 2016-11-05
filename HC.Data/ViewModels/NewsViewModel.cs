using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HC.Data.ViewModels
{
    public class NewsViewModel
    {
       
        public HC.Data.Models.News model { get; set; }

        public attchments attchments { get; set; }

        public viodeInfo viodeInfo { get; set; }
    }


    public class attchments
    {
        public attchments()
        {
            this.albumList = new List<attchment>();
        }
        public IList<attchment> albumList { get; set; }
    }


    public class attchment
    {
        public string AttachmentUrl { get; set; }
        public string AttchmentIllustrate { get; set; }
    }

    public class viodeInfo
    {
        public string voidURL { get; set; }
        public string viodeOriginalName { get; set; }
        public string voidpicurl { get; set; }
        public string voidpicOriginalName { get; set; }
    }
}
