using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HC.Core.Configuration
{

    public partial class HCConfig:IConfigurationSectionHandler
    {
        /// <summary>
        /// 此方法会自动执行
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        //public object Create(object parent, object configContext, System.Xml.XmlNode section)
        //{
        //    var config = new HCConfig();
        //    var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
        //    if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
        //    {
        //        var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
        //        if (attribute != null)
        //            config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
        //    }


        //    var engineNode = section.SelectSingleNode("Engine");
        //    if (engineNode != null && engineNode.Attributes != null)
        //    {
        //        var attribute = engineNode.Attributes["Type"];
        //        if (attribute != null)
        //            config.EngineType = attribute.Value;
        //    }

        //    var themeNode = section.SelectSingleNode("Themes");
        //    if (themeNode != null && themeNode.Attributes != null)
        //    {
        //        var attribute = themeNode.Attributes["basePath"];
        //        if (attribute != null)
        //            config.ThemeBasePath = attribute.Value;
        //    }
        //    return config;
        //}

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var config = new HCConfig();
            var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
            if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
            {
                var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
                if (attribute != null)
                    config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
            }

            var engineNode = section.SelectSingleNode("Engine");
            if (engineNode != null && engineNode.Attributes != null)
            {
                var attribute = engineNode.Attributes["Type"];
                if (attribute != null)
                    config.EngineType = attribute.Value;
            }

            var startupNode = section.SelectSingleNode("Startup");
            if (startupNode != null && startupNode.Attributes != null)
            {
                var attribute = startupNode.Attributes["IgnoreStartupTasks"];
                if (attribute != null)
                    config.IgnoreStartupTasks = Convert.ToBoolean(attribute.Value);
            }

            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var attribute = themeNode.Attributes["basePath"];
                if (attribute != null)
                    config.ThemeBasePath = attribute.Value;
            }

            return config;
        }
        public bool DynamicDiscovery { get; set; }

        public string EngineType { get; set; }

        public string ThemeBasePath { get; set; }
        public bool IgnoreStartupTasks { get; private set; }
    }
}
