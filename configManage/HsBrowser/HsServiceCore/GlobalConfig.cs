using System;
using System.Collections.Generic;
using System.Text;

namespace HsServiceCore
{
    public class GlobalConfig
    {
        static bool _isLogin = false;

        static string _protocolPre = "chs";
        static string _ServerName = "XX专用浏览器";

        static string _loginAddress = "loginSys";

        static string _manageAddress = "manage";

        static bool _loginState = false;

        public static bool LoginState
        {
            get { return GlobalConfig._loginState; }
            set { GlobalConfig._loginState = value; }
        }

        public static string ManageAddress
        {
            get { return String.Format("{0}://{1}",_protocolPre,_manageAddress); }
            set { GlobalConfig._manageAddress = value; }
        }

        public static string LoginAddress
        {
            get { return GlobalConfig._loginAddress; }
            set { GlobalConfig._loginAddress = value; }
        }

        static  public string ServerName
        {
            get { return _ServerName; }
            set { _ServerName = value; }
        }

        static string _privateKey = "$ac0_3f%";

        static string _serverIp = "127.0.0.1";
        static int _serverPort = 27009;

        static public bool IsLogin
        {
            get{ return _isLogin;}
            set { _isLogin = value; }
        }

        public static string ProtocolPre
        {
            get { return _protocolPre; }
            set { _protocolPre = value; }
        }

        static public string ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }

        static public int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }

        static public string PrivateKey
        {
            get { return _privateKey; }
            set { _privateKey = value; }
        }

        public static string getUrlHead()
        {
            return String.Format("http://{0}:{1}", ServerIp,ServerPort);
        }

        //static List<HsProtocol> _systemProtocols = new List<HsProtocol>();

        //public static List<HsProtocol> SystemProtocols
        //{
        //    get { return _systemProtocols; }
        //    set { _systemProtocols = value; }
        //}

        static List<ServiceItem> _systemMenus = new List<ServiceItem>();

        public static List<ServiceItem> SystemMenus
        {
            get { return GlobalConfig._systemMenus; }
            set { GlobalConfig._systemMenus = value; }
        }


        public static ServiceItem getMenu(string pCmd)
        {
            for (int index = 0 ; index < SystemMenus.Count; index ++)
            {
                ServiceItem item = SystemMenus[index];

                if (item.CmdString == pCmd)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
