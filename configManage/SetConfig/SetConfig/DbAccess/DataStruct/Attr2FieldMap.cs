using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess.DataStruct
{
    public class Attr2FieldMap
    {
        string _attrbute;

        public string Attrbute
        {
            get { return _attrbute; }
            set { _attrbute = value; }
        }
        string _field;

        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }


        public Attr2FieldMap(string Attr, string Fld)
        {
            Attrbute = Attr;
            Field = Fld;
        }

        public static void setValue(object objHandle,object value,string attrName)
        {
            // 获取属性类型
            Type type = objHandle.GetType(); //获取类型
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(attrName); //获取指定名称的属性

            if (propertyInfo == null)
            {
                Console.WriteLine(String.Format("Attribute Error: {0}",attrName));
                return;
            }
            if (propertyInfo.PropertyType.Name == "Int32")
            {
                try
                {
                    propertyInfo.SetValue(objHandle, Int32.Parse(value.ToString()), null); 
                }
                catch
                {

                }
            }
            else if (propertyInfo.PropertyType.Name.ToLower() == "string")
            {
                propertyInfo.SetValue(objHandle, value.ToString(), null); 
            }
            
        }
    }
}
