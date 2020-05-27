using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SimpleSharpTool.Core
{
    /// <summary>
    /// 利用反射获取Attribute信息
    /// </summary>
    /// <remarks>Get Attribute Information via Reflection</remarks>
    public class AttributeService
    {
        /// <summary>
        /// 获取类中属性的Attribute
        /// </summary>
        /// <param name="classWithAttr">实例对象</param>
        /// <param name="attribute">特性</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public Attribute GetAttr(object classWithAttr,Attribute attribute, string propertyName)
        {
            var classType = classWithAttr.GetType();

            var propertyInfo = classType.GetProperty(propertyName);

            var propertyAttribute = propertyInfo?.GetCustomAttribute(attribute.GetType());

            return propertyAttribute;
        }
    }
}
