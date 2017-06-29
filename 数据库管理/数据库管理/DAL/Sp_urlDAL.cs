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
    public class Sp_urlDAL
    {
        public static readonly string url =
          ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public Sp_url[] GetAllData()
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sp_url ";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            mycon.Close();
            DataTable dt = ds.Tables[0];
            mycon.Close();
            List<Sp_url> operators = new List<Sp_url>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators.Add(ToSp_url(dt.Rows[i]));
            }
            return operators.ToArray();
        }

        public Sp_url[] GetAllData(string Code)
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sp_url where ConfigId = '" + Code + "'";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            DataTable dt = ds.Tables[0];
            mycon.Close();
            Sp_url[] operators = new Sp_url[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators[i] = ToSp_url(dt.Rows[i]);
            }
            return operators;
        }

        private Sp_url ToSp_url(DataRow row)
        {
            Sp_url op = new Sp_url();
            op.Id = Convert.ToInt32(row["Id"]);
            op.StartIndex = (int)row["StartIndex"];
            op.StopIndex = (int)row["StopIndex"];
            op.Step = (int)row["Step"];
            op.BaseUrl = (string)row["BaseUrl"];
            op.ShortUrl = (string)row["ShortUrl"];
            op.LoopType = (int)row["LoopType"];
            op.Name = (string)row["Name"];
            op.Alias = (string)row["Alias"];
            op.Sheet = (string)row["Sheet"];
            op.ConfigId = (string)row["ConfigId"];
            op.Code = (string)row["Code"];
            op.Enable = (int)row["Enable"];
            op.Classfic = (string)row["Classfic"];
            return op;
        }

        public void Insert(Sp_url ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("INSERT into sp_url(Id,StartIndex,StopIndex,Step,BaseUrl,ShortUrl,LoopType,Name,Alias,Sheet,ConfigId,Code,Enable,Classfic)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", new object[]
                        {
                            ud.Id,ud.StartIndex,ud.StopIndex,ud.Step,ud.BaseUrl,ud.ShortUrl,ud.LoopType,ud.Name,ud.Alias,ud.Sheet,ud.ConfigId,ud.Code,ud.Enable,ud.Classfic
                        });
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void Update(Sp_url ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("update sp_url set StartIndex='" + ud.StartIndex + "',StopIndex='" + ud.StopIndex + "',Step='" + ud.Step + "',BaseUrl='" + ud.BaseUrl + "',ShortUrl='" + ud.ShortUrl + "',LoopType='" + ud.LoopType + "',Name='" + ud.Name + "',Alias='" + ud.Alias + "',Sheet='" + ud.Sheet + "',ConfigId='" + ud.ConfigId + "',Enable=" + ud.Enable + ",Classfic='" + ud.Classfic + "' where Id =" + ud.Id + "");
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void DeleteByConfigId(string code)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("delete from sp_url where ConfigId='" + code + "'");
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
            string sql2 = string.Format("delete from sp_url where Code='" + code + "'");
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
