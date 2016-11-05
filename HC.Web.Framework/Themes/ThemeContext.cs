using System.Linq;
using HC.Core;


namespace HC.Web.Framework.Themes
{
    /// <summary>
    /// Theme context
    /// </summary>
    public partial class ThemeContext : IThemeContext
    {
        private readonly IWorkContext _workContext;
        private readonly IThemeProvider _themeProvider;

        private bool _desktopThemeIsCached;
        private string _cachedDesktopThemeName;

        //private bool _mobileThemeIsCached;
        //private string _cachedMobileThemeName;

        public ThemeContext(IWorkContext workContext, 

            IThemeProvider themeProvider)
        {
            this._workContext = workContext;
            this._themeProvider = themeProvider;
        }

        /// <summary>
        /// Get or set current theme for desktops (e.g. darkOrange)
        /// </summary>
        public string WorkingDesktopTheme
        {
            get
            {
                if (_desktopThemeIsCached)
                    return _cachedDesktopThemeName;

                string theme = "";
                

   

                //ensure that theme exists
                if (!_themeProvider.ThemeConfigurationExists(theme))
                    theme = _themeProvider.GetThemeConfigurations()
                        .Where(x => !x.MobileTheme)
                        .FirstOrDefault()
                        .ThemeName;
                
                //cache theme
                this._cachedDesktopThemeName = theme;
                this._desktopThemeIsCached = true;
                return theme;
            }
            set
            {
                

                if (_workContext.CurrentCustomer == null)
                    return;

                //_genericAttributeService.SaveAttribute(_workContext.CurrentUser, SystemUserAttributeNames.WorkingDesktopThemeName, value);

                //clear cache
                this._desktopThemeIsCached = false;
            }
        }

    }
}
