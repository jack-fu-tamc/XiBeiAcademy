using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace HC.Data.ViewModels
{
    public class SectionList
    {
        public IList<NewsClass> SectionLists { get; set; }
        public SectionList()
        {
            SectionLists = new List<NewsClass>();
        }
    }
}
