using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess
{
    public class MysqlDBHelper
    {
        MySqlConnection _dbConnector;

        public MySqlConnection DbConnector
        {
            get { return _dbConnector; }
            set { _dbConnector = value; }
        }

        public bool OpenDb()
        {
            string connStr = Program.DbConfig.getConnectString();

            _dbConnector = new MySqlConnection(connStr);
            try
            {
                _dbConnector.Open();
            }
            catch (MySqlException exc)
            {
                //MessageBox.Show(exc.Message);
                return false;
            }
            finally
            {
                //myconn.Close();
            }

            return true;
        }
    }
}
