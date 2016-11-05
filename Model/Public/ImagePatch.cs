using System;
namespace Maticsoft.Models
{
    public class ImagePatch
    {
        public ImagePatch()
        { }
        private string _ImageUserPhotoSmall;//头像小
        private string _ImageUserPhotoBig;//头像大
        private string _AccMainImage;//商品主图
        private string _AccImage;//商品图片
        private string _ImageMyCar;//我的车型图片
        private string _ImageProcurment;//求购信息图片
        private string _ImageRepair;//报修信息图片
        private string _ImageAdvice;//建议信息图片
        private string _ImageComplaints;//投诉信息图片
        private string _ImageProsecution;//举报信息图片     
        private string _ImageCertificate;//授权证书
        private string _ImageStore;//店铺形象


        /// <summary>
        /// 授权证书
        /// </summary>
        public string ImageCertificate
        {
            set { _ImageCertificate = value; }
            get { return _ImageCertificate; }
        }

        /// <summary>
        /// 店铺形象
        /// </summary>
        public string ImageStore
        {
            set { _ImageStore = value; }
            get { return _ImageStore; }
        }


        public string ImageUserPhotoSmall
        {
            set { _ImageUserPhotoSmall = value; }
            get { return _ImageUserPhotoSmall; }
        }
        public string ImageUserPhotoBig
        {
            set { _ImageUserPhotoBig = value; }
            get { return _ImageUserPhotoBig; }
        }
        public string AccMainImage
        {
            set { _AccMainImage = value; }
            get { return _AccMainImage; }
        }
        public string AccImage
        {
            set { _AccImage = value; }
            get { return _AccImage; }
        }
        public string ImageMyCar
        {
            set { _ImageMyCar = value; }
            get { return _ImageMyCar; }
        }
        public string ImageProcurment
        {
            set { _ImageProcurment = value; }
            get { return _ImageProcurment; }
        }
        public string ImageRepair
        {
            set { _ImageRepair = value; }
            get { return _ImageRepair; }
        }
        public string ImageAdvice
        {
            set { _ImageAdvice = value; }
            get { return _ImageAdvice; }
        }
        public string ImageComplaints
        {
            set { _ImageComplaints = value; }
            get { return _ImageComplaints; }
        }
        public string ImageProsecution
        {
            set { _ImageProsecution = value; }
            get { return _ImageProsecution; }
        }

        public string ImageBrandLogo { get; set; }
    }
}