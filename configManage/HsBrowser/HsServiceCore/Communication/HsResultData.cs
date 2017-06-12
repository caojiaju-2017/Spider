using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hs.Comminucation
{
    public class HsResultData
    {
        private int errorId;
        private string errorInfo;
        private object resultString;

        public int ErrorId
        {
            get { return errorId; }
            set { errorId = value; }
        }

        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }

        public object ResultString
        {
            get { return resultString; }
            set { resultString = value; }
        }

        public static HsResultData parseData(string rtnString)
        {
            if (rtnString == null)
            {
                return HsResultData.InnerError();
            }
            HsResultData returnData = new HsResultData();

            JObject jo = JObject.Parse(rtnString);
            Dictionary<string, object> rtnResults = jo.ToObject<Dictionary<string, object>>();

            returnData.ErrorId = Int32.Parse(rtnResults["ErrorId"].ToString());
            returnData.ErrorInfo = rtnResults["ErrorInfo"].ToString();

            if (rtnResults["Result"] == null)
            {
                returnData.ResultString = null;
            }
            else
            {
                returnData.ResultString = rtnResults["Result"];
            }
            

            return returnData;
        }

        private static HsResultData InnerError()
        {
            HsResultData rtnError = new HsResultData();
            rtnError.ErrorId = 9999;
            rtnError.ErrorInfo = "系统内部错误";

            return rtnError;
        }

        internal static HsResultData parseBinData(string srResult)
        {
            HsResultData returnData = new HsResultData();

            returnData.ErrorId = 200;
            returnData.ErrorInfo = "";


            returnData.ResultString = srResult;
          
            return returnData;
        }
    }
}
