using HsServiceCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HsServiceCore
{
    [Serializable]
    public class TabItem
    {
        string _urlAddress;
        string _urlName;

        //public TabItem(string name,string url)
        //{
        //    _urlName = name;
        //    _urlAddress = String.Format("{0}://{1}", GlobalConfig.ProtocolPre, GlobalConfig.LoginAddress);
        //}

        public TabItem()
        {
            _urlName = "新页签";
            _urlAddress = String.Format("{0}://blank", GlobalConfig.ProtocolPre);
        }
        public string Url
        {
            get { return _urlAddress; }
            set { _urlAddress = value; }
        }

        public string Name
        {
            get { return _urlName; }
            set { _urlName = value; }
        }

        Rectangle _mainRect;

        public Rectangle MainRect
        {
            get { return _mainRect; }
            set { _mainRect = value; }
        }
        Rectangle _waitImageRect;

        public Rectangle WaitImageRect
        {
            get { return _waitImageRect; }
            set { _waitImageRect = value; }
        }
        Rectangle _closeRect;

        public Rectangle CloseRect
        {
            get { return _closeRect; }
            set { _closeRect = value; }
        }

        public PtPosition getPointPosition(Point pt)
        {
            if (CloseRect.Contains(pt))
            {
                return PtPosition.InClose;
            }
            else if (MainRect.Contains(pt))
            {
                return PtPosition.InMain;
            }

            return PtPosition.OutSide;
        }

        #region 站点状态检查
        internal bool isLoginFrm()
        {
            string url = String.Format("{0}://{1}", GlobalConfig.ProtocolPre, GlobalConfig.LoginAddress);
            if (url.Equals(this.Url))
            {
                return true;
            }

            return false;
        }

        internal bool isNormalWeb()
        {
            string urlStrTemp = Url.ToLower();

            if (urlStrTemp.Contains("http:") || !urlStrTemp.Contains(GlobalConfig.ProtocolPre + ":"))
            {
                return true;
            }

            return false;
        }

        internal bool isBlank()
        {
            string url = String.Format("{0}://blank", GlobalConfig.ProtocolPre);
            if (url.Equals(this.Url))
            {
                return true;
            }

            return false;
        }
        #endregion
    }

    public enum PtPosition
    {
        InMain,InClose,OutSide
    }
}
