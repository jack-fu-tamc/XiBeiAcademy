using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Data.Models;
using HC.Data.ViewModels;
using HC.Service.LeaveMessage;
using Telerik.Web.Mvc;

namespace HC.Manage.Controllers
{
    public class LeaveMessageController : ManageBaseController
    {

        #region filed
        private ILeaveMessageService _ileaveMessageService;
        #endregion

        public LeaveMessageController(ILeaveMessageService ileaveMessageService)
        {
            _ileaveMessageService = ileaveMessageService;
        }


        //public ActionResult ManageMessage()
        //{
        //    var model = new LeaveMesModelList();
        //    return View(model);
        //}


        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult MessageAjax(GridCommand cmd)
        {
            var SearchData = _ileaveMessageService.getAllLeaveMessages(cmd.PageSize, cmd.Page - 1);
            var gridModel = new GridModel<LeaveMessaeModel>()
            {
                Data = SearchData.Select(x => new LeaveMessaeModel() { Answer = x.Answer, answerTime = x.answerTime.ToString("yyyy-MM-dd"), createTime = x.createTime.ToString("yyyy-MM-dd"), IsShow = x.IsShow, LeaveContent = x.LeaveContent, LeaveName = x.LeaveName, MessageID = x.MessageID, Phone = x.Phone, QQ = x.QQ }),
                Total = SearchData.TotalCount
            };
            return Json(gridModel);

        }


        /// <summary>
        /// 咨询回复
        /// </summary>
        /// <param name="evaK">leaveMessage的id</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public JsonResult reMessage(int evaK, string content)
        {
            try
            {
                var entity = _ileaveMessageService.GetLeaveMessageByID(evaK);
                entity.Answer = content;
                entity.answerTime = DateTime.Now;
                _ileaveMessageService.UpdateMessage(entity);
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "fail", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 删除咨询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult delMessage(int id)
        {
            try
            {
                var entity = _ileaveMessageService.GetLeaveMessageByID(id);
                    _ileaveMessageService.deleteLeaveMessage(entity);
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 显示咨询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult showMes(string ids)
        {
            try
            {
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {
                    var entity = _ileaveMessageService.GetLeaveMessageByID(int.Parse(leaveIds[i]));
                    entity.IsShow = true;
                    _ileaveMessageService.UpdateMessage(entity);
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }



        /// <summary>
        /// 取消咨询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult offshowMes(string ids)
        {
            try
            {
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {
                    var entity = _ileaveMessageService.GetLeaveMessageByID(int.Parse(leaveIds[i]));
                    entity.IsShow = false;
                    _ileaveMessageService.UpdateMessage(entity);
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}
