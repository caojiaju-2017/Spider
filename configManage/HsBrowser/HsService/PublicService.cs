using Hs.Comminucation;
using HsServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HsService
{
   public  class PublicService
    {
       public static Image getIcon(string fileName)
       {
           try
           {
               HsApply applyLogin = new HsApply(GlobalConfig.getUrlHead(), "GET_BYTES");
               applyLogin.InvokeType = InvType.GET;

               HsParam param1 = new HsParam("Name", "fileName", DataType.String, InvType.GET);
               HsParam param2 = new HsParam("Type", "0", DataType.String, InvType.GET);

               applyLogin.addParam(param1);
               applyLogin.addParam(param2);


               HsResultData resultDat = applyLogin.excuteCommand();

               if (resultDat.ErrorId == 200)
               {
                   JArray rtnArrays = (JArray)resultDat.ResultString;

                   for (int index = 0; index < rtnArrays.Count; index++)
                   {
                       //ServiceItem oneRole = ServiceItem.parseFromString(rtnRolesArray[index].ToString());

                       //rtnMenus.Add(oneRole);
                   }
               }

           }
           catch (Exception ex)
           {
           }

           return null;
       }
    }
}
