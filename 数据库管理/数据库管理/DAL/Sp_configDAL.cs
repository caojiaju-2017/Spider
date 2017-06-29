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
    public class Sp_configDAL
    {
        public static readonly string url =
          ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public Sp_config[] GetAllData()
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sp_config ";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            mycon.Close();
            DataTable dt = ds.Tables[0];
            mycon.Close();
            List<Sp_config> operators = new List<Sp_config>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators.Add(ToSp_config(dt.Rows[i]));
            }
            return operators.ToArray();
        }

        public Sp_config[] GetAllData(string Code)
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sp_config where Code = '" + Code + "'";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            DataTable dt = ds.Tables[0];
            mycon.Close();
            Sp_config[] operators = new Sp_config[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators[i] = ToSp_config(dt.Rows[i]);
            }
            return operators;
        }

        private Sp_config ToSp_config(DataRow row)
        {
            Sp_config op = new Sp_config();
            op.Id = Convert.ToInt32(row["Id"]);
            op.TimeType = (string)row["TimeType"];
            op.TimeSep = (string)row["TimeSep"];
            op.EMail = (string)row["EMail"];
            op.Mobile = (string)row["Mobile"];
            op.Code = (string)row["Code"];
            op.Name = (string)row["Name"];
            op.Enable = (int)row["Enable"];
            op.JobClassName = (string)row["JobClassName"];
            return op;
        }

        public void Insert(Sp_config ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("INSERT into sp_config(Id,TimeType,TimeSep,EMail,Mobile,Code,Name,Enable,JobClassName)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", new object[]
                        {
                            ud.Id,ud.TimeType,ud.TimeSep,ud.EMail,ud.Mobile,ud.Code,ud.Name,ud.Enable,ud.JobClassName
                        });
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void Update(Sp_config ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("update sp_config set TimeType='" + ud.TimeType + "',TimeSep='" + ud.TimeSep + "',EMail='" + ud.EMail + "',Mobile='" + ud.Mobile + "',Code='" + ud.Code + "',Name='" + ud.Name + "',Enable='" + ud.Enable + "',JobClassName='" + ud.JobClassName +"' where Id =" + ud.Id + "");
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void Delete(string code)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("delete from sp_config where Code='" + code+"'");
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
