using System;
using System.Collections.Generic;
namespace Maticsoft.Model
{
    /// <summary>
    /// Search:实体类(
    /// </summary>
    [Serializable]
    public partial class Search_1
    {
        public Search_1()
        { }
        //#region Model


        ////获取商品列表和数量
        //private IList<ServiceAcc_Info> _serviceSearch;
        //private int _iall = 0;

        ////public
        //private int _iPageCount = Common.iPageCount;
        //private int _iPageSize = Common.iPageSize;
        ////Acc_Evaluation  
        //private string _Acc_Evaluation_Type;//Accessories or  BillBody
        //private int _Acc_Evaluation_AccessoriesID;//_Acc_Evaluation_Type=Accessories需要此参数
        //private int _Acc_Evaluation_BillBodyID;//_Acc_Evaluation_Type=BillBody需要此参数
        ////Acc_Info
        //private int _Acc_Info_trantype;//操作类型
        //private int _Acc_Info_MerchantID;//根据店铺ID获取商品
        //private int _Acc_Info_ClassID;//根据类别获取商品信息（专区内）
        //private int _Acc_Info_ID;//根据ID获取实体
        //private int _Acc_Info_CarBrandID;//根据车型获取配件
        //private double _Acc_Info_P_PriceL = 0.00;//价格区间下限
        //private double _Acc_Info_P_PriceH = 0.00;//价格区间上限
        //private int _Acc_Info_ProvinceCode;//省Code
        //private int _Acc_Info_CityCode;//城市Code
        //private int _Acc_Info_order;//排序   0 综合 1 销量 2价格升序 3价格降序       
        //private string _Acc_Info_str;//模糊查询用关键字
        ////AccSearch
        ////private string _AccSearch_Name;//搜索关键字
        ////private int _AccSearch_MerchantID;//店铺ID
        ////User_ColAcc
        //private int _User_ColAcc_UserID;
        ////User_Info
        //private string _User_Info_trantype;//ByName,ByID 
        //private string _User_Info_para;//根据_User_Info_trantype确定是Name还是ID
        //private string _User_Info_listtype;//User,Exp
        //private string _User_Info_keycode;//用户查询使用的关键字
        ////ServiceAcc_Info
        //private int _ServiceAcc_Info_trantype;//操作类型
        //private int _ServiceAcc_Info_MerchantID;//店铺ID
        //private int _ServiceAcc_Info_TypeID;//专区ID
        //private string _ServiceAcc_Info_Id;//服务ID
        //private double _ServiceAcc_Info_P_PriceL = 0.00;//价格区间下限
        //private double _ServiceAcc_Info_P_PriceH = 0.00;//价格区间上限
        //private int _ServiceAcc_Info_ProvinceCode;//省Code
        //private int _ServiceAcc_Info_CityCode;//城市Code
        //private int _ServiceAcc_Info_order;//排序   0 综合 1 销量 2价格升序 3价格降序       
        //private string _ServiceAcc_Info_str;//模糊查询用关键字
        //private int _ServiceAcc_Info_CarBrand_ID;//车品牌ID
        //private string _ServiceAcc_Info_Style;//控制服务展示样式
        ////ServiceCart
        //private int _ServiceCart_UserID;//user id
        ////ServiceEvaluation
        //private string _ServiceEvaluation_trantype;//ServiceAcc or OrderDtl
        //private int _ServiceEvaluation_ServiceAcc_ID;//_ServiceEvaluation_trantype=ServiceAcc需要此参数
        //private int _ServiceEvaluation_ServiceDtlID;//_ServiceEvaluation_trantype=OrderDtl需要此参数
        ////ServiceOrderMst
        //private int _ServiceOrderMst_ExpID;
        //private int _ServiceOrderMst_UserID;
        ////ServiceOrderDtl
        //private int _ServiceOrderDtl_MstID;
        //private int _ServiceOrderDtl_ID;
        ////User_ColMer
        //private int _User_ColMer_UserID;
        ////USER_MSG
        //private int _USER_MSG_UserID;
        ////Sold_Bill
        //private string _Sold_Bill_trantype;//SelectExp or SelectList
        //private int _Sold_Bill_MerchantID;//_Sold_Bill_trantype=SelectExp需要此参数
        //private int _Sold_Bill_UserID;//_Sold_Bill_trantype=SelectList需要此参数
        ////Sold_Bill_Body
        //private int _Sold_Bill_Body_ID;
        //private int _Sold_Bill_Body_MstID;
        //private int _Sold_Bill_Body_MerchantID;
        ////Sold_Cart
        //private int _Sold_Cart_UserID;
        //private string _Sold_Cart_ID;
        ////MyModels
        //private int _MyModels_UserID;
        ////MyBrand
        //private int _MyBrand_UserID;
        ////User_Phone
        //private int _User_Phone_UserID;
        ////Exp_Main
        //private int _Exp_Main_UserID;
        ////Acc_Image
        //private int _Acc_Image_Acc_ID;
        ////User_Complaints
        //private int _User_Complaints_ControlCode;
        //private int _User_Complaints_UserID;
        ////User_DeliverAddress
        //private int _User_DeliverAddress_UserID;
        ////Freeze_Apply
        //private int _Freeze_Apply_ControlCode;
        //private int _Freeze_Apply_ManageID;
        //private int _Freeze_Apply_DealStatus;
        ////NewData_Apply
        //private int _NewData_Apply_ControlCode;
        //private int _NewData_Apply_UserID;
        //private int _NewData_Apply_DealStatus;
        ////User_Hche
        //private int _User_Hche_UserID;
        ////ServiceLocation
        //private int _ServiceLocation_ServiceType;
        ////User_Type_Relation
        //private int _User_Type_Relation_ExpID;
        ////Repair_Image
        //private int _Repair_Image_MstID;
        ////Repair_Return
        //private int _Repair_Return_MstID;
        ////Repair_Info
        //private int _Repair_Return_DataStatus;



        ////获取商品列表和数量
        //#region 获取商品列表和数量
        //public IList<ServiceAcc_Info> ServiceSearch
        //{
        //    get { return _serviceSearch; }
        //    set { _serviceSearch = value; }
        //}
        //public int Iall
        //{
        //    get { return _iall; }
        //    set { _iall = value; }
        //}
        //#endregion
        ////public
        //#region public
        //public int iPageCount
        //{
        //    set { _iPageCount = value; }
        //    get { return _iPageCount; }
        //}
        //public int iPageSize
        //{
        //    set { _iPageSize = value; }
        //    get { return _iPageSize; }
        //}
        //#endregion
        ////Acc_Evaluation
        //#region Acc_Evaluation
        //public string Acc_Evaluation_Type
        //{
        //    set { _Acc_Evaluation_Type = value; }
        //    get { return _Acc_Evaluation_Type; }
        //}
        //public int Acc_Evaluation_AccessoriesID
        //{
        //    set { _Acc_Evaluation_AccessoriesID = value; }
        //    get { return _Acc_Evaluation_AccessoriesID; }
        //}
        //public int Acc_Evaluation_BillBodyID
        //{
        //    set { _Acc_Evaluation_BillBodyID = value; }
        //    get { return _Acc_Evaluation_BillBodyID; }
        //}
        //#endregion
        ////Acc_Info       
        //#region Acc_Info
        //public int Acc_Info_MerchantID
        //{
        //    set { _Acc_Info_MerchantID = value; }
        //    get { return _Acc_Info_MerchantID; }
        //}
        //public int Acc_Info_ClassID
        //{
        //    set { _Acc_Info_ClassID = value; }
        //    get { return _Acc_Info_ClassID; }
        //}
        //public int Acc_Info_ID
        //{
        //    set { _Acc_Info_ID = value; }
        //    get { return _Acc_Info_ID; }
        //}

        //public int Acc_Info_CarBrandID
        //{
        //    set { _Acc_Info_CarBrandID = value; }
        //    get { return _Acc_Info_CarBrandID; }
        //}

        //public int Acc_Info_trantype
        //{
        //    set { _Acc_Info_trantype = value; }
        //    get { return _Acc_Info_trantype; }
        //}
        //public double Acc_Info_P_PriceL
        //{
        //    set { _Acc_Info_P_PriceL = value; }
        //    get { return _Acc_Info_P_PriceL; }
        //}
        //public double Acc_Info_P_PriceH
        //{
        //    set { _Acc_Info_P_PriceH = value; }
        //    get { return _Acc_Info_P_PriceH; }
        //}
        //public int Acc_Info_ProvinceCode
        //{
        //    set { _Acc_Info_ProvinceCode = value; }
        //    get { return _Acc_Info_ProvinceCode; }
        //}
        //public int Acc_Info_CityCode
        //{
        //    set { _Acc_Info_CityCode = value; }
        //    get { return _Acc_Info_CityCode; }
        //}
        //public int Acc_Info_order
        //{
        //    set { _Acc_Info_order = value; }
        //    get { return _Acc_Info_order; }
        //}
        //public string Acc_Info_str
        //{
        //    set { _Acc_Info_str = value; }
        //    get { return _Acc_Info_str; }
        //}
        //#endregion
        ////AccSearch
        //#region AccSearch
        ////public string AccSearch_Name
        ////{
        ////    set { _AccSearch_Name = value; }
        ////    get { return _AccSearch_Name; }
        ////}
        ////public int AccSearch_MerchantID
        ////{
        ////    set { _AccSearch_MerchantID = value; }
        ////    get { return _AccSearch_MerchantID; }
        ////}
        //#endregion
        ////User_ColAcc
        //#region User_ColAcc
        //public int User_ColAcc_UserID
        //{
        //    set { _User_ColAcc_UserID = value; }
        //    get { return _User_ColAcc_UserID; }
        //}
        //#endregion
        ////User_Info
        //#region User_ColAcc
        //public string User_Info_trantype
        //{
        //    set { _User_Info_trantype = value; }
        //    get { return _User_Info_trantype; }
        //}
        //public string User_Info_para
        //{
        //    set { _User_Info_para = value; }
        //    get { return _User_Info_para; }
        //}
        //public string User_Info_listtype
        //{
        //    set { _User_Info_listtype = value; }
        //    get { return _User_Info_listtype; }
        //}
        //public string User_Info_keycode
        //{
        //    set { _User_Info_keycode = value; }
        //    get { return _User_Info_keycode; }
        //}
        //#endregion
        ////ServiceAcc_Info
        //#region ServiceAcc_Info
        //public int ServiceAcc_Info_MerchantID
        //{
        //    set { _ServiceAcc_Info_MerchantID = value; }
        //    get { return _ServiceAcc_Info_MerchantID; }
        //}
        //public int ServiceAcc_Info_TypeID
        //{
        //    set { _ServiceAcc_Info_TypeID = value; }
        //    get { return _ServiceAcc_Info_TypeID; }
        //}
        //public string ServiceAcc_Info_Id
        //{
        //    set { _ServiceAcc_Info_Id = value; }
        //    get { return _ServiceAcc_Info_Id; }
        //}
        //public double ServiceAcc_Info_P_PriceL
        //{
        //    set { _ServiceAcc_Info_P_PriceL = value; }
        //    get { return _ServiceAcc_Info_P_PriceL; }
        //}
        //public double ServiceAcc_Info_P_PriceH
        //{
        //    set { _ServiceAcc_Info_P_PriceH = value; }
        //    get { return _ServiceAcc_Info_P_PriceH; }
        //}
        //public int ServiceAcc_Info_ProvinceCode
        //{
        //    set { _ServiceAcc_Info_ProvinceCode = value; }
        //    get { return _ServiceAcc_Info_ProvinceCode; }
        //}
        //public int ServiceAcc_Info_CityCode
        //{
        //    set { _ServiceAcc_Info_CityCode = value; }
        //    get { return _ServiceAcc_Info_CityCode; }
        //}
        //public int ServiceAcc_Info_order
        //{
        //    set { _ServiceAcc_Info_order = value; }
        //    get { return _ServiceAcc_Info_order; }
        //}
        //public int ServiceAcc_Info_trantype
        //{
        //    set { _ServiceAcc_Info_trantype = value; }
        //    get { return _ServiceAcc_Info_trantype; }
        //}
        //public string ServiceAcc_Info_str
        //{
        //    set { _ServiceAcc_Info_str = value; }
        //    get { return _ServiceAcc_Info_str; }
        //}
        //public int ServiceAcc_Info_CarBrand_ID
        //{
        //    set { _ServiceAcc_Info_CarBrand_ID = value; }
        //    get { return _ServiceAcc_Info_CarBrand_ID; }
        //}
        //public string ServiceAcc_Info_Style
        //{
        //    get { return _ServiceAcc_Info_Style; }
        //    set { _ServiceAcc_Info_Style = value; }
        //}

        //#endregion
        ////ServiceCart
        //#region ServiceCart
        //public int ServiceCart_UserID
        //{
        //    set { _ServiceCart_UserID = value; }
        //    get { return _ServiceCart_UserID; }
        //}
       
        //#endregion
        ////ServiceEvaluation
        //#region ServiceEvaluation
        //public string ServiceEvaluation_trantype
        //{
        //    set { _ServiceEvaluation_trantype = value; }
        //    get { return _ServiceEvaluation_trantype; }
        //}
        //public int ServiceEvaluation_ServiceAcc_ID
        //{
        //    set { _ServiceEvaluation_ServiceAcc_ID = value; }
        //    get { return _ServiceEvaluation_ServiceAcc_ID; }
        //}
        //public int ServiceEvaluation_ServiceDtlID
        //{
        //    set { _ServiceEvaluation_ServiceDtlID = value; }
        //    get { return _ServiceEvaluation_ServiceDtlID; }
        //}

        //#endregion
        ////ServiceOrderMst
        //#region ServiceOrderMst
        //public int ServiceOrderMst_ExpID
        //{
        //    set { _ServiceOrderMst_ExpID = value; }
        //    get { return _ServiceOrderMst_ExpID; }
        //}
        //public int ServiceOrderMst_UserID
        //{
        //    set { _ServiceOrderMst_UserID = value; }
        //    get { return _ServiceOrderMst_UserID; }
        //}
        //#endregion
        ////ServiceOrderDtl
        //#region ServiceOrderDtl
        //public int ServiceOrderDtl_MstID
        //{
        //    set { _ServiceOrderDtl_MstID = value; }
        //    get { return _ServiceOrderDtl_MstID; }
        //}
        //public int ServiceOrderDtl_ID
        //{
        //    set { _ServiceOrderDtl_ID = value; }
        //    get { return _ServiceOrderDtl_ID; }
        //}
        //#endregion
        ////User_ColAcc
        //#region User_ColAcc
        //public int User_ColMer_UserID
        //{
        //    set { _User_ColMer_UserID = value; }
        //    get { return _User_ColMer_UserID; }
        //}
        //#endregion
        ////USER_MSG
        //#region USER_MSG
        //public int USER_MSG_UserID
        //{
        //    set { _USER_MSG_UserID = value; }
        //    get { return _USER_MSG_UserID; }
        //}
        //#endregion
        ////Sold_Bill
        //#region Sold_Bill
        //public string Sold_Bill_trantype
        //{
        //    set { _Sold_Bill_trantype = value; }
        //    get { return _Sold_Bill_trantype; }
        //}
        //public int Sold_Bill_MerchantID
        //{
        //    set { _Sold_Bill_MerchantID = value; }
        //    get { return _Sold_Bill_MerchantID; }
        //}
        //public int Sold_Bill_UserID
        //{
        //    set { _Sold_Bill_UserID = value; }
        //    get { return _Sold_Bill_UserID; }
        //}
        //#endregion
        ////Sold_Bill_Body
        //#region  Sold_Bill_Body
        //public int Sold_Bill_Body_ID
        //{
        //    set { _Sold_Bill_Body_ID = value; }
        //    get { return _Sold_Bill_Body_ID; }
        //}
        //public int Sold_Bill_Body_MstID
        //{
        //    set { _Sold_Bill_Body_MstID = value; }
        //    get { return _Sold_Bill_Body_MstID; }
        //}
        //public int Sold_Bill_Body_MerchantID
        //{
        //    set { _Sold_Bill_Body_MerchantID = value; }
        //    get { return _Sold_Bill_Body_MerchantID; }
        //}
        //#endregion
        ////Sold_Cart
        //#region Sold_Cart
        //public int Sold_Cart_UserID
        //{
        //    set { _Sold_Cart_UserID = value; }
        //    get { return _Sold_Cart_UserID; }
        //}
        //public string Sold_Cart_ID
        //{
        //    get { return _Sold_Cart_ID; }
        //    set { _Sold_Cart_ID = value; }
        //}
        //#endregion
        ////MyModels
        //#region MyModels
        //public int MyModels_UserID
        //{
        //    set { _MyModels_UserID = value; }
        //    get { return _MyModels_UserID; }
        //}
        //#endregion
        ////MyBrand
        //#region MyBrand
        //public int MyBrand_UserID
        //{
        //    set { _MyBrand_UserID = value; }
        //    get { return _MyBrand_UserID; }
        //}
        //#endregion
        ////MyModels
        //#region User_Phone
        //public int User_Phone_UserID
        //{
        //    set { _User_Phone_UserID = value; }
        //    get { return _User_Phone_UserID; }
        //}
        //#endregion
        ////Exp_Main 
        //#region Exp_Main
        //public int Exp_Main_UserID
        //{
        //    set { _Exp_Main_UserID = value; }
        //    get { return _Exp_Main_UserID; }
        //}
        //#endregion
        ////Acc_Image
        //#region Acc_Image
        //public int Acc_Image_Acc_ID
        //{
        //    set { _Acc_Image_Acc_ID = value; }
        //    get { return _Acc_Image_Acc_ID; }
        //}
        //#endregion
        ////User_Complaints
        //#region User_Complaints
        //public int User_Complaints_ControlCode
        //{
        //    set { _User_Complaints_ControlCode = value; }
        //    get { return _User_Complaints_ControlCode; }
        //}
        //public int User_Complaints_UserID
        //{
        //    set { _User_Complaints_UserID = value; }
        //    get { return _User_Complaints_UserID; }
        //}
        //#endregion
        ////User_DeliverAddress
        //#region User_DeliverAddress
        //public int User_DeliverAddress_UserID
        //{
        //    set { _User_DeliverAddress_UserID = value; }
        //    get { return _User_DeliverAddress_UserID; }
        //}
        //#endregion
        ////Freeze_Apply
        //#region Freeze_Apply
        //public int Freeze_Apply_ControlCode
        //{
        //    set { _Freeze_Apply_ControlCode = value; }
        //    get { return _Freeze_Apply_ControlCode; }
        //}
        //public int Freeze_Apply_ManageID
        //{
        //    set { _Freeze_Apply_ManageID = value; }
        //    get { return _Freeze_Apply_ManageID; }
        //}
        //public int Freeze_Apply_DealStatus
        //{
        //    set { _Freeze_Apply_DealStatus = value; }
        //    get { return _Freeze_Apply_DealStatus; }
        //}
        //#endregion

        ////NewData_Apply
        //#region NewData_Apply
        //public int NewData_Apply_ControlCode
        //{
        //    set { _NewData_Apply_ControlCode = value; }
        //    get { return _NewData_Apply_ControlCode; }
        //}
        //public int NewData_Apply_UserID
        //{
        //    set { _NewData_Apply_UserID = value; }
        //    get { return _NewData_Apply_UserID; }
        //}
        //public int NewData_Apply_DealStatus
        //{
        //    set { _NewData_Apply_DealStatus = value; }
        //    get { return _NewData_Apply_DealStatus; }
        //}
        //#endregion
        ////User_Hche
        //#region User_Hche
        //public int User_Hche_UserID
        //{
        //    set { _User_Hche_UserID = value; }
        //    get { return _User_Hche_UserID; }
        //}
        //#endregion
        ////ServiceLocation
        //#region ServiceLocation
        //public int ServiceLocation_ServiceType
        //{
        //    set { _ServiceLocation_ServiceType = value; }
        //    get { return _ServiceLocation_ServiceType; }
        //}
        //#endregion
        ////User_Type_Relation
        //#region User_Type_Relation
        //public int User_Type_Relation_ExpID
        //{
        //    set { _User_Type_Relation_ExpID = value; }
        //    get { return _User_Type_Relation_ExpID; }
        //}
        //#endregion
        ////Repair_Image
        //#region Repair_Image
        //public int Repair_Image_MstID
        //{
        //    set { _Repair_Image_MstID = value; }
        //    get { return _Repair_Image_MstID; }
        //}
        //#endregion

        ////Repair_Return
        //#region Repair_Return
        //public int Repair_Return_MstID
        //{
        //    set { _Repair_Return_MstID = value; }
        //    get { return _Repair_Return_MstID; }
        //}
        //#endregion

        ////Repair_Info
        //#region Repair_Info
        //public int Repair_Return_DataStatus
        //{
        //    set { _Repair_Return_DataStatus = value; }
        //    get { return _Repair_Return_DataStatus; }
        //}
        //#endregion
        //#endregion Model

    }
}

