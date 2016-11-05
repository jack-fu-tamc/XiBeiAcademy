using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using HC.Data.ViewModels;
using ResposityOfEf;

namespace HC.Service.News
{
    public interface INewsService
    {
        IPagedList<HC.Data.Models.News> GetAllNewsByClassID(int classID, List<int> sectionIDs, int pagesize, int pageIndex, string searchKey, DateTime startTime, DateTime endTime, int seartchType, int sortType);

        HC.Data.Models.News GetNewsByID(int newsID);
        void AddNews(HC.Data.Models.News entity);
        void UpdateNews(HC.Data.Models.News entity);
        void DeleteNews(HC.Data.Models.News entity);
        IQueryable<HC.Data.Models.News> GetAllNews();

        IList<HC.Data.Models.News> GetLeftDeliveryNews(int count, List<int> exceptClassIDs); 

        IPagedList<HC.Data.Models.News> SearchNews(string sKey, int pagesize, int pageIndex);

        /// <summary>
        /// 获取某个栏目下count条新闻 除过count条 推荐新闻
        /// </summary>
        /// <param name="newsClassID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<HC.Data.Models.News> getNewsByClassID(int newsClassID, int count, bool exceptRec, int exceptCount);

        IPagedList<HC.Data.Models.News> GetRecycleNews(List<int> sectionIDs, int pageSize, int pageIndex,DateTime beTime,DateTime endTime);

        /// <summary>
        /// 获取某栏目下推荐新闻 带图或不带图
        /// </summary>
        /// <param name="newsClassID"></param>
        /// <param name="count"></param>
        /// <param name="hasPic"></param>
        /// <returns></returns>
        IList<HC.Data.Models.News> getRecNewsByClassID(int newsClassID, int count, bool hasPic);

        IPagedList<HC.Data.Models.News> getNoCheckedNews(int pageIndex, int pageSize);
    }
}
