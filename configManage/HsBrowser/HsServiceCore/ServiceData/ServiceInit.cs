using Hs.Comminucation;
using HsServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace HsServiceCore
{
    public class ServiceInit
    {
        public static List<ServiceItem> loadSystemFuncTree()
        {
            List<ServiceItem> rtnMenus = new List<ServiceItem>();
            try
            {
                HsApply applyLogin = new HsApply(GlobalConfig.getUrlHead(), "FETCH_MENUS");
                applyLogin.InvokeType = InvType.GET;

                HsParam param1 = new HsParam("Account", "root", DataType.String, InvType.GET);

                applyLogin.addParam(param1);
                //applyLogin.addParam(param2);


                HsResultData resultDat = applyLogin.excuteCommand();

                if (resultDat.ErrorId == 200)
                {
                    JArray rtnRolesArray = (JArray)resultDat.ResultString;

                    for (int index = 0; index < rtnRolesArray.Count; index++)
                    {
                        ServiceItem oneRole = ServiceItem.parseFromString(rtnRolesArray[index].ToString());

                        rtnMenus.Add(oneRole);
                    }
                }

                rtnMenus.Sort();
                GlobalConfig.SystemMenus = rtnMenus;
                return rtnMenus;
            }
            catch (Exception ex)
            {
            }

            GlobalConfig.SystemMenus = rtnMenus;
            return rtnMenus;
        }

        private static System.Drawing.Image loadImage(string pImageName)
        {
            string fileN = Path.Combine(System.Environment.CurrentDirectory, pImageName);

            if (File.Exists(fileN))
            {
                return Image.FromFile(fileN);
            }

            return null;
        }
    }
}
