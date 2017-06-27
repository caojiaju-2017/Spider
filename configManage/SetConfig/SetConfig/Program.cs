using SetConfig.DbAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetConfig
{
    static class Program
    {
        static DBInfo _dbConfig;

        public static DBInfo DbConfig
        {
            get { return _dbConfig; }
            set { _dbConfig = value; }
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.DbConfig = new DBInfo();
            try
            {
                Program.DbConfig.ServerIp = ConfigurationManager.AppSettings["Configerver"].Trim();
                Program.DbConfig.ServerPort = Int32.Parse(ConfigurationManager.AppSettings["ServerPort"].Trim());
                Program.DbConfig.DbName = ConfigurationManager.AppSettings["DbName"].Trim();
                Program.DbConfig.UserName = ConfigurationManager.AppSettings["DBUser"].Trim();
                Program.DbConfig.Password = ConfigurationManager.AppSettings["Password"].Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            Application.Run(new SpiderConfig());
        }
    }
}
