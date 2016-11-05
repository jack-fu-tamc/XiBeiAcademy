using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HC.Core.Common.logHelper;
using HC.Core.Infrastructure;
using HC.Service.FileRecord;

namespace HC.Web.Framework.ueditor
{
    public class DeletePicHandler : Handler
    {

        public DeletePicHandler(HttpContext context)
            : base(context)
        {
        }


        enum ResultState
        {
            Success,
            Error           
        }

        private ResultState State;

        public override void Process()
        {
            try
            {
                var filePath = this.Request["path"].ToString();
                var fileRecordService = EngineContext.Current.Resolve<IFileRecordService>();
                fileRWhelper.DeleteFile(filePath);
                fileRecordService.DeleteFileRecordByPath(filePath);
            }
            catch (Exception ex)
            {
                State = ResultState.Error;
                WriteResult();
                return;
            }
            finally
            {
                WriteResult();
            }
        }

        private void WriteResult()
        {
            WriteJson(new
            {
                message = GetStateString(),                
            });
        }

        private string GetStateString()
        {
            switch (State)
            {
                case ResultState.Success:
                    return "SUCCESS";                    
                case ResultState.Error:
                    return "删除出错";                   
            }
            return "未知错误";
        }
    }
}
