using SpiderC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpiderC
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 装在配置
            GlobalShare.CurrentConfig = LocalConfig.loadLocalConfig();

            Application.Run(new LoginFrm());
        }
    }
}
