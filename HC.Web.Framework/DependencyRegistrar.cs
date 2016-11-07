using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using HC.Core.Infrastructure.DependencyManagement;
using HC.Core.Infrastructure;
using HC.Core.Fakes;
using HC.Core;

using HC.Web.Framework.Themes;
using System.Web.Mvc;

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using HC.Service.Account;
using HC.Service.Authentication;
using HC.Service.Section;

using HC.Service.News;

using HC.Service.User;

using HC.Service.LeaveMessage;
using HC.Service.Attachment;
using HC.Service.ArtistType;
using HC.Service.SqlQuery;
using HC.Service.UserGroup;
using HC.Service.FileRecord;
using HC.Service.OpLog;
using HC.Service.EnrolSys;
using ResposityOfEf;


namespace HC.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
          
            builder.Register(c =>

                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerHttpRequest();


            builder.Register(c => c.Resolve<HttpContextBase>().Request)
               .As<HttpRequestBase>()
               .InstancePerHttpRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest();

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();


            builder.Register<IDbContext>(c => new ErpDataContext(DataSettingsManager.connectionStr)).InstancePerHttpRequest();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();



            #region 注册所有控制器
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            #endregion

            #region 服务注册
           
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerHttpRequest();

            builder.RegisterType<User_InfoService>().As<IUser_InfoService>().InstancePerHttpRequest();

            //builder.RegisterType<testIntefaceService>().As<ItestInteface>().InstancePerHttpRequest();
            //builder.RegisterType<CustomerService>().As<IcustomService>().InstancePerHttpRequest();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerHttpRequest();
            builder.RegisterType<SectionService>().As<ISectionService>().InstancePerHttpRequest();
            builder.RegisterType<NewsService>().As<INewsService>().InstancePerHttpRequest();
            
            builder.RegisterType<UserService>().As<IUserService>().InstancePerHttpRequest();
            
            builder.RegisterType<LeaveMessageService>().As<ILeaveMessageService>().InstancePerHttpRequest();
            builder.RegisterType<AttachmentService>().As<IAttachmentService>().InstancePerHttpRequest();
            builder.RegisterType<ArtistTypeService>().As<IartiseTypeService>().InstancePerHttpRequest();
            builder.RegisterType<SqlQueryService>().As<IsqlQueryService>().InstancePerHttpRequest();
            builder.RegisterType<UserGroupService>().As<IuserGroupService>().InstancePerHttpRequest();
            builder.RegisterType<FileRecorderService>().As<IFileRecordService>().InstancePerHttpRequest();
            builder.RegisterType<OpLogService>().As<IopLogService>().InstancePerHttpRequest();
            builder.RegisterType<EnrolSysService>().As<IEnrolSysService>().InstancePerHttpRequest();
            //builder.RegisterType<User_InfoBll>().As<IUser_InfoService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ExpBll>().As<IUser_ExpService>().InstancePerHttpRequest();
            //builder.RegisterType<User_PhoneBll>().As<IUser_PhoneService>().InstancePerHttpRequest();
            //builder.RegisterType<Exp_MainBll>().As<IExp_MainService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_TypeBll>().As<IAcc_TypeService>().InstancePerHttpRequest();
            //builder.RegisterType<Advert_InfoBll>().As<IAdvert_InfoService>().InstancePerHttpRequest();
            //builder.RegisterType<ServiceAcc_InfoBll>().As<IServiceAcc_InfoService>().InstancePerHttpRequest();
            //builder.RegisterType<Car_BrandBll>().As<ICar_BrandService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_BrandBll>().As<IAcc_BrandService>().InstancePerHttpRequest();
            //builder.RegisterType<User_Type_RelationBll>().As<IUser_Type_RelationService>().InstancePerHttpRequest();
            //builder.RegisterType<Sold_CartBll>().As<ISold_CartService>().InstancePerHttpRequest();
            //builder.RegisterType<ServiceCartBll>().As<IServiceCartService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_ImageBll>().As<IAcc_ImageService>().InstancePerHttpRequest();
            //builder.RegisterType<ServiceOrderDtlBll>().As<IServiceOrderDtlService>().InstancePerHttpRequest();
            //builder.RegisterType<ServiceOrderMstBll>().As<IServiceOrderMstService>().InstancePerHttpRequest();
            //builder.RegisterType<User_DeliverAddressBll>().As<IUser_DeliverAddressService>().InstancePerHttpRequest();
            //builder.RegisterType<MyCarBll>().As<IMyCarService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ColAccBll>().As<IUser_ColAccService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ColMerBll>().As<IUser_ColMerService>().InstancePerHttpRequest();
            //builder.RegisterType<ServiceEvaluationBll>().As<IServiceEvaluationService>().InstancePerHttpRequest();
            //builder.RegisterType<CommonService>().As<IcommonService>().InstancePerHttpRequest();
            //builder.RegisterType<Exp_TypeBll>().As<IExp_TypeService>().InstancePerHttpRequest();
            //builder.RegisterType<User_AdviceBll>().As<IUser_AdviceService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ProsecutionBll>().As<IUser_ProsecutionService>().InstancePerHttpRequest();
            //builder.RegisterType<User_Prosecution_ImageBll>().As<IUser_Prosecution_ImageService>().InstancePerHttpRequest();
            //builder.RegisterType<User_HcheBll>().As<IUser_HcheService>().InstancePerHttpRequest();
            //builder.RegisterType<User_Advice_ImageBll>().As<IUser_Advice_ImageService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ComplaintsAccBll>().As<IUser_ComplaintsAccService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ComplaintsAcc_ImageBll>().As<IUser_ComplaintsAcc_ImageService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ComplaintsServiceBll>().As<IUser_ComplaintsServiceService>().InstancePerHttpRequest();
            //builder.RegisterType<User_ComplaintsService_ImageBll>().As<IUser_ComplaintsService_ImageService>().InstancePerHttpRequest();
            //builder.RegisterType<ServiceSuppliesBll>().As<IServiceSupplieService>().InstancePerHttpRequest();
            //builder.RegisterType<AccCarRelationBLL>().As<IAcc_Car_RelationService>().InstancePerHttpRequest();
            //builder.RegisterType<USER_MSGBll>().As<IUSER_MSGService>().InstancePerHttpRequest();
            //builder.RegisterType<Repair_ImageBll>().As<IRepair_ImageService>().InstancePerHttpRequest();
            //builder.RegisterType<Repair_InfoBll>().As<IRepair_InfoService>().InstancePerHttpRequest();
            //builder.RegisterType<Repair_ReturnBll>().As<IRepair_ReturnService>().InstancePerHttpRequest();
            //builder.RegisterType<AppAddressBll>().As<IAppAddressService>().InstancePerHttpRequest();
            //builder.RegisterType<Repair_EvaluationBll>().As<IRepair_EvaluationService>().InstancePerHttpRequest();
            //builder.RegisterType<User_PhoneBll>().As<IUser_PhoneService>().InstancePerHttpRequest();
            //builder.RegisterType<Advert_Info_ActivityBll>().As<IAdvert_Info_ActivityService>().InstancePerHttpRequest();
            //builder.RegisterType<Sold_BillBll>().As<ISold_BillService>().InstancePerHttpRequest();
            //builder.RegisterType<Sold_Bill_BodyBll>().As<ISold_Bill_BodyService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_InfoBll>().As<IAcc_InfoService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_Info_AddBll>().As<IAcc_Info_AddService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_EvaluationBll>().As<IAcc_EvaluationService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_Brand_TypeBll>().As<IAcc_Brand_TypeService>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_InventoryBll>().As<IAcc_InventoryService>().InstancePerHttpRequest();
            //builder.RegisterType<MyModelsBll>().As<IMyModelsService>().InstancePerHttpRequest();
            //builder.RegisterType<Procurment_ImgBll>().As<IprocurmentImgService>().InstancePerHttpRequest();
            ////builder.RegisterType<Procurment_InfoBll>().As<IprocrumentInfoService>().InstancePerHttpRequest();
            //builder.RegisterType<Procurment_InfoMstBll>().As<Iprocurment_InfoMstService>().InstancePerHttpRequest();
            //builder.RegisterType<Procurment_InfoDtlBll>().As<IProcurment_InfoDtlService>().InstancePerHttpRequest();
            //builder.RegisterType<Procurment_AttachmentBll>().As<IProcurment_AttachmentService>().InstancePerHttpRequest();

            //builder.RegisterType<Procurment_ReturnBll>().As<IProcurment_ReturnSevice>().InstancePerHttpRequest();
            //builder.RegisterType<Acc_PreferentialBill>().As<IAcc_PreferentialService>().InstancePerHttpRequest();
            //builder.RegisterType<MyBrandBll>().As<IMyBrandService>().InstancePerHttpRequest();
            //builder.RegisterType<Freeze_ApplyBll>().As<IFreeze_ApplyService>().InstancePerHttpRequest();
            //builder.RegisterType<RemoveFreeze_ApplyBll>().As<IRemoveFreeze_ApplyService>().InstancePerHttpRequest();
            //builder.RegisterType<NewData_ApplyBll>().As<INewData_ApplyService>().InstancePerHttpRequest();
            //builder.RegisterType<SystemUser_AddInfoBll>().As<ISystemUser_AddInfoService>().InstancePerHttpRequest();
            //builder.RegisterType<SystemUser_AreaBll>().As<ISystemUser_AreaService>().InstancePerHttpRequest();
            
            



            builder.RegisterType<ThemeProvider>().As<IThemeProvider>().InstancePerHttpRequest();
            builder.RegisterType<ThemeContext>().As<IThemeContext>().InstancePerHttpRequest();

            #endregion

        }

        public int Order
        {
            get { return 0; }
        }
    }
}
