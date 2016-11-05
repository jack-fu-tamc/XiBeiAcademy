using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HC.Core.Enum;
using HC.Core.ComponentModel;


namespace HC.Core
{
    public partial class CommonHelper
    {
        /// <summary>
        /// 验证字符串是否为有效的电子邮件格式
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

     
       



  


   
        private static AspNetHostingPermissionLevel? _trustLevel = null;

        /// <summary>
        /// 查找正在运行的应用程序的信任级别 
        /// </summary>
        /// <returns>当前信任级别.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //设定最低
                _trustLevel = AspNetHostingPermissionLevel.None;

                //确定最大
                foreach (AspNetHostingPermissionLevel trustLevel in
                        new AspNetHostingPermissionLevel[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal 
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //我们设置了最高权限
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }

   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeConverter GetUserTypeConverter(Type type)
        {

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();
            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// 转换值到目标类型
        /// </summary>
        /// <param name="value">要转换的值.</param>
        /// <param name="destinationType">要转换的类型.</param>
        /// <returns></returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 转换值到目标类型.
        /// </summary>
        /// <param name="value">要转换的值.</param>
        /// <param name="destinationType">要转换的类型.</param>
        /// <param name="culture">区域设置</param>
        /// <returns></returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetUserTypeConverter(destinationType);
                TypeConverter sourceConverter = GetUserTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return System.Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// 转换值到目标类型.
        /// </summary>
        /// <param name="value">要转换的值.</param>
        /// <typeparam name="T">要转换的类型.</typeparam>
        /// <returns>.</returns>
        public static T To<T>(object value)
        {

            return (T)To(value, typeof(T));
        }

  
      

       
    }
}
