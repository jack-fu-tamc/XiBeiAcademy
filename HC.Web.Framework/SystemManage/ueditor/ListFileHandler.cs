using System;
//using CommunityGrid.Common.Enum;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using HC.Core.Infrastructure;
using HC.Service.FileRecord;

namespace HC.Web.Framework.ueditor
{
    /// <summary>
    /// FileManager 的摘要说明
    /// </summary>
    public class ListFileManager : Handler
    {
        enum ResultState
        {
            Success,
            InvalidParam,
            AuthorizError,
            IOError,
            PathNotFound
        }

        private int Start;
        private int Size;
        private int Total;
        private ResultState State;
        private String PathToList;
        private String[] FileList;
        private String[] SearchExtensions;

        //private short BusinessFlag =(short)enumBusinessFlag.CMSContent;

        private short BusinessFlag = 1;

        private string action = "listimage";
        private int fileType = 1;
        public ListFileManager(HttpContext context, string pathToList, string[] searchExtensions,string action):base(context)
        {
            this.SearchExtensions = searchExtensions.Select(x => x.ToLower()).ToArray();
            this.PathToList = pathToList;
            this.action = action;
            if (action == "listfile")
            {
                fileType = 2;
            }

            if (context.Request.Params["BusinessFlag"] != null)
            {
                //this.BusinessFlag = CommunityGrid.Common.Utility.ToShort(context.Request.Params["BusinessFlag"].ToString(), 0);
                this.BusinessFlag = 1;
            }
            else
            {
                this.BusinessFlag = 0;
            }
        }

        public override void Process()
        {
            try
            {
                Start = String.IsNullOrEmpty(Request["start"]) ? 0 : Convert.ToInt32(Request["start"]);
                Size = String.IsNullOrEmpty(Request["size"]) ? Config.GetInt("imageManagerListSize") : Convert.ToInt32(Request["size"]);
                //Size = 20;
            }
            catch (FormatException)
            {
                State = ResultState.InvalidParam;
                WriteResult();
                return;
            }
            var buildingList = new List<String>();
            try
            {

                //enumFileFlag fileFlag = enumFileFlag.EditorPicture;
                //enumBusinessFlag businessFlag=enumBusinessFlag.CMSContent;
                //if (this.action == "listimage")//列出在线图片
                //{
                //    fileFlag = enumFileFlag.EditorPicture;
                //}
                //else if (this.action == "listfile") //列出已经上传的附件
                //{
                //    fileFlag = enumFileFlag.EditorFile;
                //}

                //if (this.BusinessFlag == (short)enumBusinessFlag.CMSContent)//可支持多处地方使用编辑器
                //{
                //    businessFlag = enumBusinessFlag.CMSContent;
                //}
                //else if (this.BusinessFlag == (short)enumBusinessFlag.CMSClass) //可支持多处地方使用编辑器
                //{
                //    businessFlag = enumBusinessFlag.CMSClass;
                //}

                //BLL.SystemManage.RelationFile fileBLL = new RelationFile();
                //FileList = fileBLL.GetFilePath(businessFlag,fileFlag,SearchExtensions, Start, Size, out Total);
                ////var localPath = Server.MapPath(PathToList);
                ////buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                ////    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                ////    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/") + "?fileId=tuwirw-fewfds-rewrewr"));
                ////Total = buildingList.Count;
                ////FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
                var FileRecorderService = EngineContext.Current.Resolve<IFileRecordService>();

                var fileRecorders = FileRecorderService.getFileRecords(Size, Start, fileType);
                Total = fileRecorders.TotalCount;
                FileList = fileRecorders.Select(x => x.Path).ToList().ToArray();

            }
            catch (UnauthorizedAccessException)
            {
                State = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                State = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                State = ResultState.IOError;
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
                state = GetStateString(),
                list = FileList == null ? null : FileList.Select(x => new { url = x }),
                start = Start,
                size = Size,
                total = Total
            });
        }

        private string GetStateString()
        {
            switch (State)
            {
                case ResultState.Success:
                    return "SUCCESS";
                case ResultState.InvalidParam:
                    return "参数不正确";
                case ResultState.PathNotFound:
                    return "路径不存在";
                case ResultState.AuthorizError:
                    return "文件系统权限不足";
                case ResultState.IOError:
                    return "文件系统读取错误";
            }
            return "未知错误";
        }
    }
}