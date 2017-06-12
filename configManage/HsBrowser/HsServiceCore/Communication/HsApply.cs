using System;
using System.Collections.Generic;
using System.Text;

namespace Hs.Comminucation
{
    public class HsApply
    {
        private List<HsParam> invokeParams = new List<HsParam>();
        private InvType invokeType = InvType.GET;

        private int _returnType = 0;

        /// <summary>
        /// 0 表示标准格式
        /// 1 表示二进制
        /// </summary>
        public int ReturnType
        {
            get { return _returnType; }
            set { _returnType = value; }
        }

        private string urlHeader;
        private string commandString;

        private HsCommandCenter commandCenter = new HsCommandCenter();
        
        public HsApply(string urlString,string cmdString)
        {
            commandString = cmdString;
            urlHeader = urlString;
        }

        public string CommandString
        {
            get { return commandString; }
            set { commandString = value; }
        }
        public List<HsParam> InvokeParams
        {
            get { return invokeParams; }
            set { invokeParams = value; }
        }
        public string UrlHeader
        {
            get { return urlHeader; }
            set { urlHeader = value; }
        }
        public InvType InvokeType
        {
            get { return invokeType; }
            set { invokeType = value; }
        }

        public void addParam(HsParam param)
        {
            invokeParams.Add(param);
        }

        public HsResultData excuteCommand()
        {
            return commandCenter.dispatcherCommand(this);


        }

        internal string getAbusoluteUrl(string urlHeader, string urlTemplate)
        {
            string rtnUrl = urlHeader + urlTemplate;
            for (int index = 0; index < invokeParams.Count; index++)
            {
                HsParam param = invokeParams[index];

                if (param.InvokeType == InvType.POST)
                {
                    continue;
                }

                rtnUrl = rtnUrl + "&" + param.ParamName + "=" + param.ParamValue;
            }

            return rtnUrl;
        }

        internal string getPostData()
        {
            // 判断是不是POST
            String rtnString = "";
            for (int index = 0; index < invokeParams.Count; index++)
            {

                HsParam data = invokeParams[index];

                if (index != 0)
                {
                    rtnString = rtnString + ",";
                }

                if (data.ParamType == DataType.Float || data.ParamType == DataType.Int)
                {
                    rtnString = rtnString + "\"" + data.ParamName + "\"" + ":" + data.ParamValue;
                }
                else if (data.ParamType == DataType.ByteArray)
                {
                    string str = Convert.ToBase64String((byte[])data.ParamValue);
                    rtnString = rtnString + "\"" + data.ParamName + "\"" + ":" + "\"" + str + "\"";
                }
                else
                {
                    rtnString = rtnString + "\"" + data.ParamName + "\"" + ":" + "\"" + data.ParamValue + "\"";
                }

            }

            return "{" + rtnString + "}";
        }
    }
}
