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
    public class NewsService : INewsService
    {

        #region fileds
        private IRepository<Data.Models.News> _NewsRepository;
        private IRepository<Data.Models.NewsClass> _NewsClassRepository;
        #endregion


        #region cto
        public NewsService(IRepository<HC.Data.Models.News> NewsRepository, IRepository<Data.Models.NewsClass> NewsClassRepository)
        {
            this._NewsRepository = NewsRepository;
            this._NewsClassRepository = NewsClassRepository;
        }
        #endregion


        public IPagedList<Data.Models.News> GetAllNewsByClassID(int classID,List<int> sectionIDs, int pagesize, int pageIndex, string searchKey, DateTime startTime, DateTime endTime, int seartchType, int sortType)
        {
            var query = _NewsRepository.Table;
            if (classID > 0)
            {
                var newclass = _NewsClassRepository.GetById(classID);
                if (newclass.ParentID == 0)//二级栏目下所有新闻 包含三级新闻
                {
                    var SubNewsclasID = _NewsClassRepository.Table.Where(x => x.ParentID == newclass.ClassID).Select(y => y.ClassID);
                    if (SubNewsclasID.Count() > 0)//有子栏目
                    {
                        query = query.Where(x => SubNewsclasID.Contains(x.ClassID));
                    }
                    else//无子栏目
                    {
                        query = query.Where(x => x.ClassID == classID);
                    }
                }
                else//一级栏目或者三级栏目
                {
                    query = query.Where(x => x.ClassID == classID);
                }
            }
            else
            {
                query = query.Where(x => sectionIDs.Contains(x.ClassID));
            }
            query = query.Where(x => x.CreatTime >= startTime && x.CreatTime <= endTime && x.isDelete != 1);
            if (seartchType > 0)
            {
                switch (seartchType)
                {
                    case 1://标题或关键字
                        query = query.Where(x => x.NewsTitle.Contains(searchKey));
                        break;
                    case 2://作者
                        query = query.Where(x => x.Author == searchKey);
                        break;
                }
            }

            if (sortType > 0)
            {
                switch (sortType)
                {
                    case 1://日期降序
                        query = query.OrderByDescending(x => x.CreatTime);
                        break;
                    case 2://点击量降序
                        query = query.OrderByDescending(x => x.ClickNum);
                        break;
                    case 3://权重
                        query = query.OrderByDescending(x => x.NewsOrder);
                        break;
                }
            }



            return new PagedList<HC.Data.Models.News>(query, pageIndex, pagesize);
        }


        public HC.Data.Models.News GetNewsByID(int newsID)
        {
            var query = _NewsRepository.Table;
            return query.Where(x => x.NewsID == newsID).FirstOrDefault();
        }

        public void AddNews(HC.Data.Models.News entity)
        {
            _NewsRepository.Insert(entity);
        }

        public void UpdateNews(HC.Data.Models.News entity)
        {
            this._NewsRepository.Update(entity);
        }

        public IQueryable<HC.Data.Models.News> GetAllNews()
        {
            return _NewsRepository.Table.Where(x=> x.isDelete == 0);
        }

        public IList<HC.Data.Models.News> GetLeftDeliveryNews(int count,List<int> exceptClassIDs) 
        {
            var query = _NewsRepository.Table;
            return query.Where(x => x.isDelete == 0&&!exceptClassIDs.Contains(x.ClassID)).OrderByDescending(x => x.CreatTime).Take(count).ToList();
        }

        public IPagedList<HC.Data.Models.News> SearchNews(string sKey, int pagesize, int pageIndex)
        {
            var query = _NewsRepository.Table;
            query = query.Where(x => x.NewsTitle.Contains(sKey.Trim()) && x.isDelete == 0).OrderByDescending(x => x.NewsID);
            return new PagedList<HC.Data.Models.News>(query, pageIndex - 1, pagesize);
        }

        public void DeleteNews(HC.Data.Models.News entity)
        {
            _NewsRepository.Delete(entity);
        }


        /// <summary>
        /// 获取某个栏目下count条新闻 按时间降序排列
        /// </summary>
        /// <param name="newsClassID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<HC.Data.Models.News> getNewsByClassID(int newsClassID, int count, bool exceptRec, int exceptCount)
        {
            //var query = _NewsRepository.Table;

            //var subClassIDs = _NewsClassRepository.Table.Where(x => x.ParentID == newsClassID).Select(x => x.ClassID).ToList();
            //if (subClassIDs.Count == 0)
            //{
            //    query = query.Where(x => x.ClassID == newsClassID).OrderByDescending(x => x.CreatTime).Take(count);
            //}
            //else
            //{
            //    query = query.Where(x => subClassIDs.Contains(x.ClassID)).OrderByDescending(x => x.CreatTime).Take(count);
            //}
            //return query.ToList();
            if (exceptRec)
            {
                var reResult = getDiGui(newsClassID);
                var exceptNewsIds = reResult.Where(x => x.IsRec == 1&&x.PicURL!="").OrderByDescending(x => x.CreatTime).Take(exceptCount).Select(x=>x.NewsID);
                return reResult.Where(x => !exceptNewsIds.Contains(x.NewsID)).Take(count).ToList();
            }
            else
            {
                return getDiGui(newsClassID).OrderByDescending(x => x.CreatTime).Take(count).ToList();
            }
           
        }


        public IList<HC.Data.Models.News> getDiGui(int newsClassID)
        {
            var query = _NewsRepository.Table;
          
            var subClassIDs = _NewsClassRepository.Table.Where(x => x.ParentID == newsClassID).Select(x => x.ClassID).ToList();
            if (subClassIDs.Count > 0)
            {
                var tempResult = new List<HC.Data.Models.News>();
                for (var j = 0; j < subClassIDs.Count; j++)
                {
                    tempResult.AddRange(getDiGui(subClassIDs[j]));
                }
                return tempResult;
            }
            else
            {
                return query.Where(x => x.ClassID == newsClassID && x.isDelete == 0).ToList();
            }
        }


        /// <summary>
        /// 获取某栏目下推荐新闻 带图或全部
        /// </summary>
        /// <param name="newsClassID"></param>
        /// <param name="count"></param>
        /// <param name="hasPic"></param>
        /// <returns></returns>
        public IList<HC.Data.Models.News> getRecNewsByClassID(int newsClassID, int count,bool hasPic)
        {
            var query = _NewsRepository.Table;
            var picFlag = hasPic ? 0 : -1;


            var subClassIDs = _NewsClassRepository.Table.Where(x => x.ParentID == newsClassID).Select(x => x.ClassID).ToList();
            if (subClassIDs.Count == 0)
            {
                query = query.Where(x => x.ClassID == newsClassID && x.IsRec == 1 && x.PicURL.Length > picFlag && x.isDelete == 0).OrderByDescending(x => x.CreatTime).Take(count);
            }
            else
            {
                query = query.Where(x => subClassIDs.Contains(x.ClassID) && x.IsRec == 1 && x.PicURL.Length > picFlag && x.isDelete == 0).OrderByDescending(x => x.CreatTime).Take(count);
            }
            return query.ToList();
        }


        public IPagedList<HC.Data.Models.News> GetRecycleNews(List<int> sectionIDs, int pageSize, int pageIndex, DateTime beTime, DateTime endTime)
        {
            var query = _NewsRepository.Table;
            query = query.Where(x => sectionIDs.Contains(x.ClassID) && x.isDelete == 1 && x.CreatTime >= beTime && x.CreatTime <= endTime).OrderByDescending(x => x.CreatTime);
            return new PagedList<HC.Data.Models.News>(query, pageIndex - 1, pageSize);
        }

        /// <summary>
        /// 获取待审核新闻
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<HC.Data.Models.News> getNoCheckedNews(int pageIndex,int pageSize)
        {
            var query = _NewsRepository.Table.Where(x=>x.isDelete==2).OrderByDescending(x=>x.CreatTime);
            return new PagedList<HC.Data.Models.News>(query, pageIndex, pageSize);
        }
    }
}
