using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SpiderC.Data
{
     [Serializable]
    public class LocalConfig
    {
        string _userName;

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        string _userPasswd;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPasswd
        {
            get { return _userPasswd; }
            set { _userPasswd = value; }
        }

        bool _rememberUserInfo = false;
        /// <summary>
        /// 是否记住用户
        /// </summary>
        public bool RememberUserInfo
        {
            get { return _rememberUserInfo; }
            set { _rememberUserInfo = value; }
        }

        int _wndWidth = 1280;

        public int WndWidth
        {
            get { return _wndWidth; }
            set { _wndWidth = value; }
        }

        int _wndHeight = 980;

        public int WndHeight
        {
            get { return _wndHeight; }
            set { _wndHeight = value; }
        }


        public static void reBuildConfigData(LocalConfig userInfo)
        {
            string configName = AppDomain.CurrentDomain.BaseDirectory + "local.cfg";
            Stream fStream = new FileStream(configName, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(fStream, userInfo);

            fStream.Close();
        }

        public static LocalConfig loadLocalConfig()
        {
            string configName = AppDomain.CurrentDomain.BaseDirectory + "local.cfg";

            // 
            if (!File.Exists(configName))
            {
                return new LocalConfig();
            }

            FileInfo file = new FileInfo(configName);

            Stream fStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            BinaryFormatter binFormat = new BinaryFormatter();
            LocalConfig localDownLoadAds = new LocalConfig();
            try
            {
                localDownLoadAds = (LocalConfig)binFormat.Deserialize(fStream);
            }
            catch
            {
                localDownLoadAds = new LocalConfig();
            }
            fStream.Close();

            return localDownLoadAds;
        }
    }
}
