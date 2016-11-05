using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HC.Core.Extension
{
    /// <summary>
    /// 手机号和固话联合验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class PhoneValidateAttribute : ValidationAttribute, IClientValidatable
    {
        public string messageZD { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return true; 
        }

        public override string FormatErrorMessage(string name)
        {
            return messageZD;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "phonevalid"//只能小写
            };
        }
    }

    /// <summary>
    /// 营业执照验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ImgValidateAttribute : ValidationAttribute, IClientValidatable
    {
        public string messageZD { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return messageZD;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "imgvalid"//只能小写
            };
        }
    }

    /// <summary>
    /// 身份证图片验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IdcardValidateAttribute : ValidationAttribute, IClientValidatable
    {
        public string messageZD { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return messageZD;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "idcardvalid"//只能小写
            };
        }
    }



    /// <summary>
    /// 验证码验证
    /// </summary>
    public sealed class captEqualAttribute : ValidationAttribute, IClientValidatable
    {
        public string messageZD { get; set; }
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return true;

        }
        public override string FormatErrorMessage(string name)
        {
            return messageZD;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "enforcetrue"
            };
        }
    }

}
 
