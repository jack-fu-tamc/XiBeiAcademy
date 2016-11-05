using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.ViewModels;
using HC.Data.Models;

namespace HC.Service.Section
{
    public interface  ISectionService
    {
        SectionList getAllSection();
        NewsClass getNewsClassByID(int newsClassID);
        void UpdataNewsClass(NewsClass model);
        void AddNewClass(NewsClass model);
        void DeleteNewClass(NewsClass entity);
        IList<NewsClass> GetSubNewClassByParentNewClass(int parentNewClassID);

        IList<NewsClass> GetSiblingNewsClass(int newsClassParentID);

    }
}
