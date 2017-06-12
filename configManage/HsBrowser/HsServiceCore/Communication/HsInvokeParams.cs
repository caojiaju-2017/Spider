using System;
using System.Collections.Generic;
using System.Text;

namespace Hs.Comminucation
{
    public class HsParam
    {
        private string paramName;
        private object paramValue;
        private DataType paramType = DataType.String;
        private InvType invokeType = InvType.GET;

        public string ParamName
        {
            get { return paramName; }
            set { paramName = value; }
        }
        public object ParamValue
        {
            get { return paramValue; }
            set { paramValue = value; }
        }

        public DataType ParamType
        {
            get { return paramType; }
            set { paramType = value; }
        }

        public InvType InvokeType
        {
            get { return invokeType; }
            set { invokeType = value; }
        }

        public HsParam(string pName, object pValue, DataType pType,InvType invType)
        {
            paramName = pName;
            paramValue = pValue;
            paramType = pType;
            invokeType = invType;
        }
    }

    public enum DataType
    {
        Int, Float, String, ByteArray
    }

    public enum InvType
    {
        GET,POST,UNKNOWN
    }

}
