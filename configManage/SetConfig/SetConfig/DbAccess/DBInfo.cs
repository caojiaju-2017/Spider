using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess
{
    public class DBInfo
    {
        string _serverIp;

        public string ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }
        int _serverPort;

        public int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }
        string _dbName;

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }
        string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        public string getConnectString()
        {
            return String.Format("server={0};port={1};user id={2}; password={3}; database={4}; pooling=false",
                this.ServerIp,this.ServerPort,this.UserName,this.Password,this.DbName);
        }
    }
}
