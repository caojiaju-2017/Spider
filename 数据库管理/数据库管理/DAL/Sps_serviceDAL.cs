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
    public class Sps_serviceDAL
    {
        public static readonly string url =
          ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public Sps_service[] GetAllData()
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sps_service ";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            mycon.Close();
            DataTable dt = ds.Tables[0];
            mycon.Close();
            List<Sps_service> operators = new List<Sps_service>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators.Add(ToSp_url(dt.Rows[i]));
            }
            return operators.ToArray();
        }

        private Sps_service ToSp_url(DataRow row)
        {
            Sps_service op = new Sps_service();
            op.Id = Convert.ToInt32(row["Id"]);
            op.Code = (string)row["Code"];
            op.Name = (string)row["Name"];

            if (row["Image"] != DBNull.Value)
            {
                op.Image = (byte[])row["Image"];
            }
            
            op.Info = (string)row["Info"];
           
            return op;
        }

        public void Insert(Sps_service ud)
        {
            //MySqlConnection mycon = new MySqlConnection();
            //mycon.ConnectionString = url;
            //string sql2 = string.Format("INSERT into sps_service(Code,Name,Image,Info)values('{1}','{2}',{3},'{4}')", new object[]
            //            {
            //                ud.Id,ud.Code,ud.Name,ud.Image,ud.Info
            //            });
            //MySqlCommand comm = new MySqlCommand(sql2, mycon);
            //mycon.Open();
            //int result = comm.ExecuteNonQuery();
            //mycon.Close();
            //if (result > 0)
            //{


            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            mycon.Open();

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "INSERT into sps_service(Code,Name,Image,Info)values(@Code,@Name,@Image,@Info)";
            comm.Parameters.Add("@Code", MySql.Data.MySqlClient.MySqlDbType.String);
            comm.Parameters.Add("@Name", MySql.Data.MySqlClient.MySqlDbType.String);
            comm.Parameters.Add("@Image", MySql.Data.MySqlClient.MySqlDbType.LongBlob);
            comm.Parameters.Add("@Info", MySql.Data.MySqlClient.MySqlDbType.String);
            comm.Connection = mycon;

            comm.Parameters["@Code"].Value = ud.Code;
            comm.Parameters["@Name"].Value = ud.Name;
            comm.Parameters["@Image"].Value = ud.Image;
            comm.Parameters["@Info"].Value = ud.Info;

            int result = comm.ExecuteNonQuery();
            if (result > 0)
            {
            }
        }

        public void Update(Sps_service ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            mycon.Open();

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "update sps_service set Name= @Name,Image=@Image ,Info=@Info where Id =@Id";

            comm.Parameters.Add("@Id", MySql.Data.MySqlClient.MySqlDbType.Int32);
            comm.Parameters.Add("@Name", MySql.Data.MySqlClient.MySqlDbType.String);
            comm.Parameters.Add("@Image", MySql.Data.MySqlClient.MySqlDbType.LongBlob);
            comm.Parameters.Add("@Info", MySql.Data.MySqlClient.MySqlDbType.String);
            comm.Connection = mycon;

            comm.Parameters["@Name"].Value = ud.Name;
            comm.Parameters["@Image"].Value = ud.Image;
            comm.Parameters["@Info"].Value = ud.Info;
            comm.Parameters["@Id"].Value = ud.Id;

            int result = comm.ExecuteNonQuery();
            if (result > 0)
            {
            }
        }

        public void DeleteByCode(string code)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("delete from sps_service where Id =" + code);
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
