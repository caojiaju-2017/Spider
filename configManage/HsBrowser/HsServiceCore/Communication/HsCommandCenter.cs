using HsServiceCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hs.Comminucation
{
    public class HsCommandCenter
    {
        internal HsResultData dispatcherCommand(HsApply applyData)
        {
            string urlHeader = applyData.UrlHeader;


            string urlTemplate = null;

            if (applyData.CommandString == "LOGIN_SYSTEM" || applyData.CommandString == "FETCH_MENUS")
            {
                urlTemplate =
                String.Format("/api/{0}/?Command={1}&TimeSnap=mmm&Sig=20151023","user", applyData.CommandString);
            }
            else
            {
                urlTemplate =
                String.Format("/api/{0}/?Command={1}&TimeSnap=mmm&Sig=20151023", GlobalConfig.getMenu(applyData.CommandString).PreApi, applyData.CommandString);
            }

            string ecuteUrl = applyData.getAbusoluteUrl(urlHeader, urlTemplate);

            string srResult;
            if (applyData.InvokeType == InvType.GET)
            {
                srResult = HsExcute.HttpGet(ecuteUrl);

                if (applyData.ReturnType != 0)
                {
                    return HsResultData.parseBinData(srResult);
                }
                
            }
            else
            {
                string postData = applyData.getPostData();

                srResult = HsExcute.PostMoths(ecuteUrl, postData);
            }


           return HsResultData.parseData(srResult);
          
            
        }


        public static string getUrl(HsApply applyData)
        {
            string urlHeader = applyData.UrlHeader;
            string urlTemplate = null;
            //if (AdverstCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = AdverstCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (DocumentCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = DocumentCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (KnowlegeCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = KnowlegeCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (MeetingCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = MeetingCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (OrgCommandBase.isContainer(applyData.CommandString))
            //{
            //    //
            //    urlTemplate = OrgCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (PrivilegeCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = PrivilegeCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (ProductCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = ProductCommandBase.getTemplate(applyData.CommandString);
            //}
            //else if (AttachCommandBase.isContainer(applyData.CommandString))
            //{
            //    urlTemplate = AttachCommandBase.getTemplate(applyData.CommandString);
            //}

            if (urlTemplate == null)
            {
                return null;
            }
            return applyData.getAbusoluteUrl(urlHeader, urlTemplate);
        }
    }
}
