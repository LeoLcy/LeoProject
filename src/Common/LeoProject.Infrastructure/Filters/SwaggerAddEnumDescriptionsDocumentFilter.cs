using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeoProject.Infrastructure.Filters
{
    /// <summary>
    /// Swagger添加枚举描述对应的文档 DisplayAttribute
    /// </summary>
    public class SwaggerAddEnumDescriptionsDocumentFilter : IDocumentFilter
    {

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            // add enum descriptions to result models
            // 将枚举加到返回对象的描述中，json.definitions对象里的枚举
            foreach (KeyValuePair<string, Schema> schemaDictionaryItem in swaggerDoc.Definitions)
            {
                Schema schema = schemaDictionaryItem.Value;
                foreach (KeyValuePair<string, Schema> propertyDictionaryItem in schema.Properties)
                {
                    Schema property = propertyDictionaryItem.Value;
                    IList<object> propertyEnums = property.Enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        property.Description += DescribeEnum(propertyEnums);
                    }
                }
            }

            // add enum descriptions to input parameters
            if (swaggerDoc.Paths.Count > 0)
            {
                foreach (PathItem pathItem in swaggerDoc.Paths.Values)
                {
                    DescribeEnumParameters(pathItem.Parameters);
                    // head, patch, options, delete left out
                    List<Operation> possibleParameterisedOperations = new List<Operation> { pathItem.Get, pathItem.Post, pathItem.Put };
                    possibleParameterisedOperations.FindAll(x => x != null).ForEach(x => DescribeEnumParameters(x.Parameters));
                }
            }
        }

        private void DescribeEnumParameters(IList<IParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    if (param.In == "path")
                    {
                        var nonParam = (NonBodyParameter)param;
                        IList<object> paramEnums = nonParam.Enum;
                        if (paramEnums != null && paramEnums.Count > 0)
                        {
                            param.Description += "：" + DescribeEnum(paramEnums);
                        }
                    }
                    if (param.In == "body")
                    {
                        var bodyParam = (BodyParameter)param;
                        Schema property = bodyParam.Schema;
                        IList<object> propertyEnums = property.Enum;
                        if (propertyEnums != null && propertyEnums.Count > 0)
                        {
                            property.Description += "：" + DescribeEnum(propertyEnums);
                        }
                    }
                    if (param.In == "query")
                    {
                        var nonParam = (NonBodyParameter)param;
                        IList<object> paramEnums = nonParam.Enum;
                        if (paramEnums != null && paramEnums.Count > 0)
                        {
                            param.Description += "：" + DescribeEnum(paramEnums);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 枚举转换成值和描述
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        private string DescribeEnum(IList<object> enums)
        {
            List<string> enumDescriptions = new List<string>();
            foreach (object item in enums)
            {
                var type = item.GetType();
                var objArr = type.GetField(item.ToString()).GetCustomAttributes(typeof(DisplayAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DisplayAttribute da = objArr[0] as DisplayAttribute;
                    enumDescriptions.Add($"{(int)item} {da.Name}");
                }
                else
                {
                    enumDescriptions.Add(string.Format("{0} = {1}", (int)item, Enum.GetName(item.GetType(), item)));
                }
            }
            return string.Join(", ", enumDescriptions.ToArray());
        }

    }
}
