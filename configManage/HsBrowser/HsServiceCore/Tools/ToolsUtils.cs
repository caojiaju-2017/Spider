using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace HsServiceCore
{
    public class ToolsUtils
    {
        public static string Md5(string strSrc)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(strSrc, "MD5").ToLower();
        }

        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }

        public static object GetStaticAttribute(string attrName)
        {
            return typeof(GlobalConfig).GetProperty(attrName).GetValue(null, null);
        }

        public static string getObjectId(object obj)
        {
            if (obj is ServiceItem)
            {
                ServiceItem item = obj as ServiceItem;

                return item.getCode();
            }

            return null;
        }
    }
}
