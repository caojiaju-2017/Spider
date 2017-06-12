using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Drawing;
using System.Security.Cryptography;
using SpiderC.Data;

namespace SpiderC
{
    class GlobalShare
    {
        private static string currentVersion = "V1.0.0";
        /// <summary>
        /// 软件版本号
        /// </summary>
        public static string Version
        {
            get { return GlobalShare.currentVersion; }
            set { GlobalShare.currentVersion = value; }
        }


        private static LocalConfig g_loginUser;
        /// <summary>
        /// 当前登录配置信息
        /// </summary>
        public static LocalConfig CurrentConfig
        {
            get { return GlobalShare.g_loginUser; }
            set { GlobalShare.g_loginUser = value; }
        }
    }
  
}
