using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HsServiceCore
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ServiceItem : IComparable<ServiceItem>  
    {
        public int CompareTo(ServiceItem p)
        {
            if (this.Inx > p.Inx)
            {
                return 1;
            }
            return 0;
        }
         public ServiceItem()
         {

         }

         string _preApi;

         public string PreApi
         {
             get { return _preApi; }
             set { _preApi = value; }
         }

         string _Url;

         public string Url
         {
             get { return _Url; }
             set { _Url = value; }
         }
         string _Name;

         public string Name
         {
             get { return _Name; }
             set { _Name = value; }
         }
         string _PUrlId;

         public string PUrlId
         {
             get { return _PUrlId; }
             set { _PUrlId = value; }
         }
         string _UrlId;

         public string UrlId
         {
             get { return _UrlId; }
             set { _UrlId = value; }
         }
         string _ImageName;

         public string ImageName
         {
             get { return _ImageName; }
             set { _ImageName = value; }
         }
         int _Inx;

         public int Inx
         {
             get { return _Inx; }
             set { _Inx = value; }
         }
         int _IsVirtual;

         public int IsVirtual
         {
             get { return _IsVirtual; }
             set { _IsVirtual = value; }
         }


         internal static ServiceItem parseFromString(string jsonData)
         {
             JObject jo = JObject.Parse(jsonData);
             Dictionary<string, object> rtnResults = jo.ToObject<Dictionary<string, object>>();

             ServiceItem rtnMenuItem = new ServiceItem();
             rtnMenuItem.Url = (rtnResults["Url"] == null ? "" : rtnResults["Url"].ToString());
             rtnMenuItem.Name = (rtnResults["Name"] == null ? "" : rtnResults["Name"].ToString());
             rtnMenuItem.PUrlId = (rtnResults["PUrlId"] == null ? "" : rtnResults["PUrlId"].ToString());
             rtnMenuItem.UrlId = (rtnResults["UrlId"] == null ? "" : rtnResults["UrlId"].ToString()); ;
             rtnMenuItem.ImageName = (rtnResults["ImageName"] == null ? "" : rtnResults["ImageName"].ToString()); 
             rtnMenuItem.Inx = (rtnResults["Inx"] == null ? 0 : Int32.Parse(rtnResults["Inx"].ToString()));
             rtnMenuItem.IsVirtual = (rtnResults["IsVirtual"] == null ? 0 : Int32.Parse(rtnResults["IsVirtual"].ToString()));
             rtnMenuItem.CmdString = (rtnResults["CmdString"] == null ? "" : rtnResults["CmdString"].ToString());
             rtnMenuItem.PreApi = (rtnResults["PreApi"] == null ? "" : rtnResults["PreApi"].ToString());

             JArray attrsArray = rtnResults["Attrs"] as JArray;
             for (int index = 0; index < attrsArray.Count; index++)
             {
                 ServiceItemAttribute oneAttr = ServiceItemAttribute.parseFromString(attrsArray[index].ToString());

                 rtnMenuItem.ServiceItemAttrs.Add(oneAttr);
             }
             rtnMenuItem.ServiceItemAttrs.Sort();
             return rtnMenuItem;
         }

         List<ServiceItemAttribute> _ServiceItemAttrs;

         public List<ServiceItemAttribute> ServiceItemAttrs
         {
             get {
                 if (_ServiceItemAttrs == null)
                 {
                     _ServiceItemAttrs = new List<ServiceItemAttribute>();
                 }
                 return _ServiceItemAttrs; 
             }
             set { _ServiceItemAttrs = value; }
         }

         string _cmdString;

         public string CmdString
         {
             get { return _cmdString; }
             set { _cmdString = value; }
         }

         public override string ToString()
         {
             return _Name;
         }


         public string getOperCommand(ServiceData.OpertorType _operType)
         {
             for (int index = 0 ; index < GlobalConfig.SystemMenus.Count; index ++)
             {
                 ServiceItem item = GlobalConfig.SystemMenus[index];

                 if (item.PUrlId == this.UrlId && _operType == ServiceData.OpertorType.Add && item.CmdString.Contains("ADD_"))
                 {
                     return item.CmdString;
                 }
                 else if (item.PUrlId == this.UrlId && _operType == ServiceData.OpertorType.Modi && item.CmdString.Contains("MODI_"))
                 {
                     return item.CmdString;
                 }
                 else if (item.PUrlId == this.UrlId && _operType == ServiceData.OpertorType.Delete && item.CmdString.Contains("DELE_"))
                 {
                     return item.CmdString;
                 }
             }

             return null;
         }

         public Hs.Comminucation.InvType getHttpType(ServiceData.OpertorType _operType)
         {
             if (this.ServiceItemAttrs[0].Type == 0)
             {
                 return Hs.Comminucation.InvType.GET;
             }
             else if(this.ServiceItemAttrs[0].Type == 1)
             {
                 return Hs.Comminucation.InvType.POST;
             }

             return Hs.Comminucation.InvType.UNKNOWN;
         }

         internal string getCode()
         {
             return UrlId;
         }
    }

}
