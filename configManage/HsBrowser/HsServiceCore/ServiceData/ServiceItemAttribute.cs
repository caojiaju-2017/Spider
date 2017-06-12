using Hs.Comminucation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HsServiceCore
{
    public class ServiceItemAttribute : IComparable<ServiceItemAttribute>  
    {
        static string _splitChar = "|";

        string _CmdString;

        public string CmdString
        {
            get { return _CmdString; }
            set { _CmdString = value; }
        }

        // 
        int _Type;

        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        string _KName;

        public string KName
        {
            get { return _KName; }
            set { _KName = value; }
        }
        string _KAlias;

        public string KAlias
        {
            get { return _KAlias; }
            set { _KAlias = value; }
        }
        int _KDataType;

        public int KDataType
        {
            get { return _KDataType; }
            set { _KDataType = value; }
        }
        string _SelObjects;

        public string SelObjects
        {
            get { return _SelObjects; }
            set { _SelObjects = value; }
        }
        int _IsShow;

        public int IsShow
        {
            get { return _IsShow; }
            set { _IsShow = value; }
        }

        int _Inx;

        public int Inx
        {
            get { return _Inx; }
            set { _Inx = value; }
        }

        internal static ServiceItemAttribute parseFromString(string jsonData)
        {
            JObject jo = JObject.Parse(jsonData);
            Dictionary<string, object> rtnResults = jo.ToObject<Dictionary<string, object>>();

            ServiceItemAttribute rtnMenuItem = new ServiceItemAttribute();
            rtnMenuItem.CmdString = (rtnResults["CmdString"] == null ? "" : rtnResults["CmdString"].ToString());
            rtnMenuItem.KName = (rtnResults["KName"] == null ? "" : rtnResults["KName"].ToString());
            rtnMenuItem.KAlias = (rtnResults["KAlias"] == null ? "" : rtnResults["KAlias"].ToString());
            rtnMenuItem.KDataType = (rtnResults["KDataType"] == null ? 0 : Int32.Parse(rtnResults["KDataType"].ToString())); ;
            rtnMenuItem.SelObjects = (rtnResults["SelObjects"] == null ? "" : rtnResults["SelObjects"].ToString());
            rtnMenuItem.IsShow = (rtnResults["IsShow"] == null ? 0 : Int32.Parse(rtnResults["IsShow"].ToString()));
            rtnMenuItem.Inx = (rtnResults["Inx"] == null ? 0 : Int32.Parse(rtnResults["Inx"].ToString()));
            rtnMenuItem.Type = (rtnResults["Type"] == null ? 0 : Int32.Parse(rtnResults["Type"].ToString()));

            return rtnMenuItem;
        }

        public int CompareTo(ServiceItemAttribute p)
        {

            if (this.Inx < p.Inx)
            {
                return 1;
            }
            return 0;
        
        }

        public Hs.Comminucation.HsParam buildParam(Hs.Comminucation.InvType invType)
        {
            //new HsParam("Account", userName.Text.ToString(), DataType.String, InvType.POST);
            HsParam param = new HsParam(this._KName,null,getDataTyp(),invType);

            return param;
        }

        private DataType getDataTyp()
        {
            if (this.KDataType == 0)
            {
                return DataType.Int;
            }
            else if (this.KDataType == 1)
            {
                return DataType.String;
            }

            return DataType.String ;
        }

    }
}
