using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess.DataStruct
{
    public class SUrlAttribute
    {

        #region Attr
        int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        string _urlCode;

        public string UrlCode
        {
            get { return _urlCode; }
            set { _urlCode = value; }
        }
        string _htmlTag;

        public string HtmlTag
        {
            get { return _htmlTag; }
            set { _htmlTag = value; }
        }
        string _attrName;

        public string AttrName
        {
            get { return _attrName; }
            set { _attrName = value; }
        }
        string _alias;

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        string _calcWay;

        public string CalcWay
        {
            get { return _calcWay; }
            set { _calcWay = value; }
        }
        string _externStr;

        public string ExternStr
        {
            get { return _externStr; }
            set { _externStr = value; }
        }
        string _subAttr;

        public string SubAttr
        {
            get { return _subAttr; }
            set { _subAttr = value; }
        }
        #endregion

        #region 字段属性映射
        static string _tableName;

        public static string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        static List<Attr2FieldMap> _mapList = new List<Attr2FieldMap>();

        public static List<Attr2FieldMap> MapList
        {
            get { return _mapList; }
            set { _mapList = value; }
        }

        public static void initMap()
        {
            MapList.Clear();
            MapList.Add(new Attr2FieldMap("Id", "Id"));
            MapList.Add(new Attr2FieldMap("UrlCode", "UrlCode"));
            MapList.Add(new Attr2FieldMap("HtmlTag", "HtmlTag"));
            MapList.Add(new Attr2FieldMap("AttrName", "AttrName"));
            MapList.Add(new Attr2FieldMap("Alias", "Alias"));
            MapList.Add(new Attr2FieldMap("CalcWay", "CalcWay"));
            MapList.Add(new Attr2FieldMap("ExternStr", "ExternStr"));
            MapList.Add(new Attr2FieldMap("SubAttr", "SubAttr"));

            _tableName = "sp_url_attr";
        }
        #endregion

        #region DB Persiteanl
        private static string buildQueryCmd()
        {
            string rtnString = null;
            for (int index = 0; index < MapList.Count; index++)
            {
                Attr2FieldMap map = MapList[index];
                if (rtnString == null)
                {
                    rtnString = map.Field;
                    continue;
                }

                rtnString = rtnString + "," + map.Field;
            }

            return String.Format("select {0} from {1} where 1=1;", rtnString, _tableName);
        }
        public static List<SUrlAttribute> FetchObject(MysqlDBHelper helper)
        {
            initMap();

            List<SUrlAttribute> rtnResult = new List<SUrlAttribute>();

            String cmdString = buildQueryCmd();
            MySqlCommand cmd = new MySqlCommand(cmdString, helper.DbConnector);
            //查询结果读取器
            MySqlDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SUrlAttribute cfg = new SUrlAttribute();

                    for (int index = 0; index < MapList.Count; index++)
                    {
                        Attr2FieldMap map = MapList[index];

                        Attr2FieldMap.setValue(cfg, reader[index], map.Attrbute);
                    }

                    rtnResult.Add(cfg);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

            }


            return rtnResult;
        }

        public bool delete()
        {
            return true;
        }

        public bool save()
        {
            return true;
        }
        #endregion
    }
}
