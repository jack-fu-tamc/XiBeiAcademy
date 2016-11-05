using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResposityOfEf;
using HC.Data.Models;
using HC.Data.ViewModels;

namespace HC.Service.Section
{
    public class SectionService:ISectionService
    {
        #region field
        private IRepository<NewsClass> _SectionRepository;
        #endregion


        #region cto
        public SectionService(IRepository<NewsClass> SectionRepository)
        {
            this._SectionRepository = SectionRepository;
        }
        #endregion
        public SectionList getAllSection()
        {
            var query = this._SectionRepository.Table;
            var result = new SectionList();
            result.SectionLists = query.ToList();
            return result;
        }

        public NewsClass getNewsClassByID(int newsClassID)
        {
            var query = this._SectionRepository.Table;
            return query.Where(x => x.ClassID == newsClassID).FirstOrDefault();
        }

        public void UpdataNewsClass(NewsClass model)
        {
            _SectionRepository.Update(model);
        }

        public void AddNewClass(NewsClass model)
        {
            _SectionRepository.Insert(model);
        }

        public void DeleteNewClass(NewsClass entity)
        {
            _SectionRepository.Delete(entity);
        }

        public IList<NewsClass> GetSubNewClassByParentNewClass(int parentNewClassID)
        {
            var query = _SectionRepository.Table;
            return query.Where(x => x.ParentID == parentNewClassID).ToList();
        }

        public IList<NewsClass> GetSiblingNewsClass(int newsClassParentID)
        {
            var query = _SectionRepository.Table;
            return query.Where(x => x.ParentID == newsClassParentID).ToList();
        }
    }
}
