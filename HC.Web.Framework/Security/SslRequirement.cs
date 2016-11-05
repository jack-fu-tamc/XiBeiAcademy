
namespace HC.Web.Framework.Security
{
    public enum SslRequirement
    {
        /// <summary>
        /// 页面需要安全过滤
        /// </summary>
        Yes,
        /// <summary>
        /// 不需要安全过滤
        /// </summary>
        NO,
        /// <summary>
        /// 忽略
        /// </summary>
        NoMatter,
    }
}
