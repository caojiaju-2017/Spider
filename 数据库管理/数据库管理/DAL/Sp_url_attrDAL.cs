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
    public class Sp_url_attrDAL
    {
        public static readonly string url =
          ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public Sp_url_attr[] GetAllData()
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sp_url_attr ";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            mycon.Close();
            DataTable dt = ds.Tables[0];
            mycon.Close();
            List<Sp_url_attr> operators = new List<Sp_url_attr>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators.Add(ToSp_url_attr(dt.Rows[i]));
            }
            return operators.ToArray();
        }

        public Sp_url_attr[] GetAllData(string UrlCode)
        {
            MySql.Data.MySqlClient.MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "select * from sp_url_attr where UrlCode = '" + UrlCode + "'";
            MySqlDataAdapter mda = new MySqlDataAdapter(sql2, mycon);
            DataSet ds = new DataSet();
            mda.Fill(ds, "table1");
            DataTable dt = ds.Tables[0];
            mycon.Close();
            Sp_url_attr[] operators = new Sp_url_attr[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                operators[i] = ToSp_url_attr(dt.Rows[i]);
            }
            return operators;
        }

        private Sp_url_attr ToSp_url_attr(DataRow row)
        {
            Sp_url_attr op = new Sp_url_attr();
            op.Id = Convert.ToInt32(row["Id"]);
            op.UrlCode = (string)row["UrlCode"];
            op.HtmlTag = (string)row["HtmlTag"];
            op.AttrName = (string)row["AttrName"];
            op.Alias = (string)row["Alias"];
            op.CalcWay = (string)row["CalcWay"];
            op.ExternStr = (string)row["ExternStr"];
            op.SubAttr = (string)row["SubAttr"];
            op.AttachAttr = (string)row["AttachAttr"];
            op.Code = (string)row["Code"];
            op.IsUrl = Convert.ToInt32(row["IsUrl"]);
            return op;
        }

        public void Insert(Sp_url_attr ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("INSERT into sp_url_attr(UrlCode,HtmlTag,AttrName,Alias,CalcWay,ExternStr,SubAttr,AttachAttr,Code,IsUrl)values('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", new object[]
                        {
                            ud.Id,ud.UrlCode,ud.HtmlTag,ud.AttrName,ud.Alias,ud.CalcWay,ud.ExternStr,ud.SubAttr,ud.AttachAttr,ud.Code,ud.IsUrl
                        });
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void Update(Sp_url_attr ud)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = "update sp_url_attr set UrlCode='"+ ud.UrlCode + "',HtmlTag='" + ud.HtmlTag + "',AttrName='" + ud.AttrName + "',Alias='" + ud.Alias + "',CalcWay='" + ud.CalcWay + "',ExternStr='" + ud.ExternStr + "',SubAttr='" + ud.SubAttr + "',AttachAttr='" + ud.AttachAttr + "',Code='" + ud.Code + "',IsUrl=" + ud.IsUrl + " where Id =" + ud.Id + "";
            MySqlCommand comm = new MySqlCommand(sql2, mycon);
            mycon.Open();
            int result = comm.ExecuteNonQuery();
            mycon.Close();
            if (result > 0)
            {
            }
        }

        public void DeleteByUrlCode(string code)
        {
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = url;
            string sql2 = string.Format("delete from sp_url_attr where UrlCode='" + code + "'");
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
            string sql2 = string.Format("delete from sp_url_attr where Code='" + code + "'");
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
