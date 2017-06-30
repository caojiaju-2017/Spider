using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using 数据库管理.Model;

namespace 数据库管理.DAL
{
    public class Sps_userDAL
    {
        public static readonly string url =
          ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public Sps_user[] GetAllData()
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sps_user ";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            mycon.Close();
            DataTable dt = ds.Tables[0];
            mycon.Close();
            List<Sps_user> operators = new List<Sps_user>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators.Add(ToSp_url(dt.Rows[i]));
            }
            return operators.ToArray();
        }

        private Sps_user ToSp_url(DataRow row)
        {
            Sps_user op = new Sps_user();
            op.Id = Convert.ToInt32(row["Id"]);
            op.Account = (string)row["Account"];
            op.EMail = (string)row["EMail"];
            op.Alias = (string)row["Alias"];
            op.Address = (string)row["Address"];
            op.OrgName = (string)row["OrgName"];
            op.Lantudite = (float)row["Lantudite"];
            op.Longdite = (float)row["Longdite"];
           
            return op;
        }

        public void Insert(Sps_user ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("INSERT into sps_user(Account,EMail,Alias,Address,OrgName,Lantudite,Longdite)values('{1}','{2}','{3}','{4}','{5}',{6},{7})", new object[]
                        {
                            ud.Id,ud.Account,ud.EMail,ud.Alias,ud.Address,ud.OrgName,ud.Lantudite,ud.Longdite
                        });
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void Update(Sps_user ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("update sps_user set EMail='" + ud.EMail + "',Alias='" + ud.Alias + "',Address='" + ud.Address + "',OrgName='"
                + ud.OrgName + "',Lantudite=" + ud.Lantudite + ",Longdite=" + ud.Longdite + " where Account ='" + ud.Account + "'");
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void DeleteByCode(string code)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("delete from sps_user where Account='" + code + "'");
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }
    }
}
