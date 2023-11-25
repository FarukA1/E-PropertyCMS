using System;
using System.Dynamic;
using System.Reflection;
using E_PropertyCMS.Core.CustomException;

namespace E_PropertyCMS.Core.Shaper
{
	public class DataShaper<T>
	{
        public static object GetShapedObject(T data, string fields)
        {
            var fieldList = new List<string>();

            if(fields != null)
            {
                fieldList = fields.ToLower().Split(',').ToList();
            }

            if (!fieldList.Any())
            {
                return data;
            }

            ExpandoObject result = new ExpandoObject();

            foreach(var field in fieldList)
            {
                try
                {
                    var fieldValue = data.GetType()
                        .GetProperty(field,
                        BindingFlags.IgnoreCase |
                        BindingFlags.Public |
                        BindingFlags.Instance)
                        .GetValue(data, null);

                    ((IDictionary<String, Object>)result).Add(field, fieldValue);
                }
                catch(NullReferenceException e)
                {
                    throw new EPropertyCMSException($"{field} is an Invalid value");
                }

            }

            return result;
        }
    }
}

