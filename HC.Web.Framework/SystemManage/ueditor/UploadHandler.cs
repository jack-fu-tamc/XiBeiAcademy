using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HC.Core.Infrastructure;
using HC.Service.FileRecord;


//using CommunityGrid.Common.Enum;
namespace HC.Web.Framework.ueditor
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : Handler
    {
        public UploadConfig UploadConfig { get; private set; }
        public UploadResult Result { get; private set; }

        public UploadHandler(HttpContext context, UploadConfig config):base(context)
        {
            string BusinessGuid = string.Empty;
            if (context.Request.Params["BusinessGuid"] != null)
            {
                BusinessGuid = context.Request.Params["BusinessGuid"].ToString();
            }
            config.BusinessGuid = BusinessGuid;

            if (context.Request.Params["BusinessID"] != null)
            {
                config.BusinessID = context.Request.Params["BusinessID"].ToString();
            }
            else
            {
                config.BusinessID = string.Empty;
            }

            if (context.Request.Params["BusinessFlag"] != null)
            {
                //config.BusinessFlag = CommunityGrid.Common.Utility.ToShort(context.Request.Params["BusinessFlag"].ToString(), 0);
            }
            else
            {
                config.BusinessFlag = 0;
            }

            if (context.Request.Params["BTBusinessID"] != null)
            {
                config.BTBusinessID = context.Request.Params["BTBusinessID"].ToString();
            }
            else
            {
                config.BTBusinessID =string.Empty;
            }

            if (context.Request.Params["BTBusinessFlag"] != null)
            {
               // config.BTBusinessFlag =CommunityGrid.Common.Utility.ToShort(context.Request.Params["BTBusinessFlag"].ToString(),0);
            }
            else
            {
                config.BTBusinessFlag =0;
            }
            this.UploadConfig = config;
            this.Result = new UploadResult() { State = UploadState.Unknown };
        }

        public override void Process()
        {
            byte[] uploadFileBytes = null;
            string uploadFileName = null;


            decimal fileSize = 0;
            if (UploadConfig.Base64)
            {
                uploadFileName = UploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request[UploadConfig.UploadFieldName]);
            }
            else
            {
                var file = Request.Files[UploadConfig.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    Result.State = UploadState.TypeNotAllow;
                    WriteResult();
                    return;
                }
                if (!CheckFileSize(file.ContentLength))
                {
                    Result.State = UploadState.SizeLimitExceed;
                    WriteResult();
                    return;
                }
                fileSize = Math.Round((decimal)file.ContentLength / 1024, 3);

                uploadFileBytes = new byte[file.ContentLength];
                try
                {
                    file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    Result.State = UploadState.NetworkError;
                    WriteResult();
                }
            }

            Result.OriginFileName = uploadFileName;

            //var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
           // var localPath = Server.MapPath(savePath);



            string ext = uploadFileName.Split('.')[uploadFileName.Split('.').Length - 1];//文件扩展名
           // string localPath = BLL.SystemManage.FileUploadHelper.GetUploadDirectory((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + "\\" + uploadFileName + "";
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + ext;
            //string localPath = BLL.SystemManage.FileUploadHelper.GetUploadDirectory((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + "\\" + newFileName + "";



            string returnPath="";
             string localPath = HC.Core.Common.FileUpload.imgUpload.GetUploadDirectory(this.UploadConfig.Action,out returnPath)+ "\\" + newFileName + "";
            
            









            //保存的文件名            
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }

               



                File.WriteAllBytes(localPath, uploadFileBytes);
                //Result.Url = BLL.SystemManage.FileUploadHelper.GetAccessPath((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag)+uploadFileName + "";
                //Result.Url = BLL.SystemManage.FileUploadHelper.GetAccessPath((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + newFileName + "";
                Result.Url = returnPath+"/"+newFileName;

                #region 文件表记录已上传的文件信息
                //BLL.SystemManage.RelationFile rfBLL = new BLL.SystemManage.RelationFile();
                //Model.SystemManage.RelationFile relationFile = new Model.SystemManage.RelationFile();
                //relationFile.FileID = Guid.NewGuid();
                //relationFile.FileName = uploadFileName;
                //relationFile.FilePath = Result.Url;
                //relationFile.FileType = ext;
                //relationFile.FileSize = fileSize;
                //relationFile.BusinessFlag =this.UploadConfig.BusinessFlag;
                //relationFile.BusinessID =this.UploadConfig.BusinessID;
                //relationFile.ParentId = Guid.Empty;
                ////relationFile.UploadUser=
                //if (this.UploadConfig.Action == "uploadimage")
                //{
                //    relationFile.FileFlag = "Imgs";
                //}
                //else if (this.UploadConfig.Action == "uploadscrawl")
                //{
                //    relationFile.FileFlag = (short)enumFileFlag.EditorGraffiti;
                //}
                //else if (this.UploadConfig.Action == "uploadvideo")
                //{
                //    relationFile.FileFlag = "video";
                //}
                //else if (this.UploadConfig.Action == "uploadfile")
                //{
                //    relationFile.FileFlag = (short)enumFileFlag.EditorFile;
                //}
                //else
                //{
                //    relationFile.FileFlag = (short)enumFileFlag.Doc;
                //}
                //relationFile.ThumbnailSize =string.Empty;
                //rfBLL.Save(relationFile);

                ////如果是编辑器图片,且业务模块是CmsContent
                //if (relationFile.FileFlag == (short)enumFileFlag.EditorPicture && relationFile.BusinessFlag == (short)enumBusinessFlag.CMSContent)
                //{
                //    生成缩略图
                //    1读取缩略图设置
                //    Model.SystemManage.Search.BusinessThumbnail btModel = new Model.SystemManage.Search.BusinessThumbnail();
                //    btModel.BusinessFlag = this.UploadConfig.BTBusinessFlag;
                //    btModel.BusinessID = this.UploadConfig.BTBusinessID;
                //    Common.Paging pageObject = new Common.Paging("ThumbnailSize ASC", Common.Enum.ListGridType.DataGrid) { PageIndex = 1, PageSize = Common.ConfigHelper.NoPageingSize };
                //    DataTable btTable = new BLL.SystemManage.BusinessThumbnail().SearchPageData(btModel, pageObject);
                //    BLL.SystemManage.Thumbnail thumbnailBLL = new Thumbnail();
                //    foreach (DataRow dr in btTable.Rows)
                //    {
                //        string reFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //        生成缩略图
                //        string tblocalPath = BLL.SystemManage.FileUploadHelper.GetUploadDirectory((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + "\\" + reFileName + "." + ext + "";
                //        int width = Common.Utility.ToInt(dr["Width"].ToString(), 100);
                //        int height = Common.Utility.ToInt(dr["Height"].ToString(), 100);
                //        short model = Common.Utility.ToShort(dr["Mode"].ToString(), 1);
                //        string thumbnailSize = dr["ThumbnailSize"].ToString();
                //        Common.ImageTools.MakeThumbnail(localPath, tblocalPath, width, height, (Common.Enum.enumThumbnailMode)model);
                //        记录缩略图信息
                //        Model.SystemManage.RelationFile tbRelationFile = new Model.SystemManage.RelationFile();
                //        tbRelationFile.FileID = Guid.NewGuid();
                //        tbRelationFile.BusinessFlag = relationFile.BusinessFlag;
                //        tbRelationFile.BusinessID = relationFile.BusinessID;
                //        tbRelationFile.FileName = relationFile.FileName + "缩略图(" + thumbnailSize + ")";
                //        tbRelationFile.FileType = relationFile.FileType;
                //        System.IO.FileInfo file = new FileInfo(tblocalPath);//获取缩略图的大小
                //        relationFile.FileSize = Math.Round((decimal)file.Length / 1024, 3);//
                //        tbRelationFile.FilePath = BLL.SystemManage.FileUploadHelper.GetAccessPath((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + "/" + reFileName + "." + ext + "";
                //        tbRelationFile.BusinessGuid = relationFile.BusinessGuid;
                //        tbRelationFile.ParentId = relationFile.FileID;
                //        tbRelationFile.FileFlag = (short)enumFileFlag.Thumbnail;
                //        tbRelationFile.ThumbnailSize = dr["ThumbnailSize"].ToString();
                //        rfBLL.Save(tbRelationFile);
                //    }
                //    IList<Model.SystemManage.Thumbnail> tbList=DAL.DataTableHelper.ConvertTo<Model.SystemManage.Thumbnail>(btTable);
                //    foreach (Model.SystemManage.Thumbnail tb in tbList)
                //    {
                //        //string reFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //        ////生成缩略图
                //        //string tblocalPath=BLL.SystemManage.FileUploadHelper.GetAccessPath((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + "\\" + reFileName + "." + ext + "";
                //        //Common.ImageTools.MakeThumbnail(localPath, tblocalPath, tb.Width, tb.Height, (Common.Enum.enumThumbnailMode)tb.Mode);

                //        ////记录缩略图信息
                //        //Model.SystemManage.RelationFile tbRelationFile = new Model.SystemManage.RelationFile();
                //        //tbRelationFile.FileID = Guid.NewGuid();
                //        //tbRelationFile.BusinessFlag = relationFile.BusinessFlag;
                //        //tbRelationFile.BusinessID = relationFile.BusinessID;
                //        //tbRelationFile.FileName = relationFile.FileName + "缩略图(" + tb.ThumbnailSize + ")";
                //        //tbRelationFile.FileType = relationFile.FileType;
                //        //System.IO.FileInfo file = new FileInfo(tblocalPath);//获取缩略图的大小
                //        //relationFile.FileSize =Math.Round((decimal)file.Length/1024,3);//
                //        //tbRelationFile.FilePath = BLL.SystemManage.FileUploadHelper.GetAccessPath((Common.Enum.enumBusinessFlag)this.UploadConfig.BusinessFlag) + "/" + reFileName + "." + ext + "";
                //        //tbRelationFile.BusinessGuid = relationFile.BusinessGuid;
                //        //tbRelationFile.ParentId = relationFile.FileID;
                //        //tbRelationFile.FileFlag = (short)enumFileFlag.Thumbnail;
                //        //tbRelationFile.ThumbnailSize = tb.ThumbnailSize;
                //        //rfBLL.Save(tbRelationFile);
                //    }
                //}


                var fileRecorderService = EngineContext.Current.Resolve<IFileRecordService>();
                var entity=new HC.Data.Models.FileRecord();

                entity.CreateTime = DateTime.Now;
                entity.FileName = uploadFileName;
                entity.FileType = this.UploadConfig.Action == "uploadimage" ? 1 : 2;
                entity.ID = Guid.NewGuid();
                entity.Path = Result.Url;
                

                
                fileRecorderService.AddRecord(entity);



                #endregion


                Result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }
            finally
            {
                WriteResult();
            }










            WriteResult();
        }

        private void WriteResult()
        {
            this.WriteJson(new
            {
                state = GetStateMessage(Result.State),
                url = Result.Url,
                title = Result.OriginFileName,
                original = Result.OriginFileName,
                error = Result.ErrorMessage
            });
        }

        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private bool CheckFileSize(int size)
        {
            return size < UploadConfig.SizeLimit;
        }
    }

    public class UploadConfig
    {
        /// <summary>
        /// 文件命名规则
        /// </summary>
        public string PathFormat { get; set; }

        /// <summary>
        /// 上传表单域名称
        /// </summary>
        public string UploadFieldName { get; set; }

        /// <summary>
        /// 上传大小限制
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// 上传允许的文件格式
        /// </summary>
        public string[] AllowExtensions { get; set; }

        /// <summary>
        /// 文件是否以 Base64 的形式上传
        /// </summary>
        public bool Base64 { get; set; }

        /// <summary>
        /// Base64 字符串所表示的文件名
        /// </summary>
        public string Base64Filename { get; set; }

        /// <summary>
        /// BusinessGuid 临时业务Id
        /// </summary>
        public string BusinessGuid { get; set; }

        /// <summary>
        /// 上传的类别
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 文件业务ID
        /// </summary>
        public string BusinessID { get; set; }

        /// <summary>
        /// 文件业务类型
        /// </summary>
        public short BusinessFlag { get; set; }
        /// <summary>
        /// 对应缩略图业务ID
        /// </summary>
        public string BTBusinessID { get; set; }

        /// <summary>
        /// 对应缩略图业务类型
        /// </summary>
        public short BTBusinessFlag { get; set; }
    }

    public class UploadResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public string OriginFileName { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }
}

