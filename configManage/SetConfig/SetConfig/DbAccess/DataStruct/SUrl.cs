using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess.DataStruct
{
    public class SUrl
    {
        public SUrl()
        {
            initMap();
        }

        #region Attr Property
        int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        int _startIndex;

        public int StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; }
        }
        int _stopIndex;

        public int StopIndex
        {
            get { return _stopIndex; }
            set { _stopIndex = value; }
        }
        int _step;

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }
        string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }
        string _shortUrl;

        public string ShortUrl
        {
            get { return _shortUrl; }
            set { _shortUrl = value; }
        }
        int _loopType;

        public int LoopType
        {
            get { return _loopType; }
            set { _loopType = value; }
        }
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        string _alias;

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        string _sheetName;

        public string SheetName
        {
            get { return _sheetName; }
            set { _sheetName = value; }
        }
        string _configId;

        public string ConfigId
        {
            get { return _configId; }
            set { _configId = value; }
        }
        string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        List<SUrlAttribute> _attrs = new List<SUrlAttribute>();

        public List<SUrlAttribute> Attrs
        {
            get { return _attrs; }
            set { _attrs = value; }
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
            MapList.Add(new Attr2FieldMap("StartIndex", "StartIndex"));
            MapList.Add(new Attr2FieldMap("StopIndex", "StopIndex"));
            MapList.Add(new Attr2FieldMap("Step", "Step"));
            MapList.Add(new Attr2FieldMap("BaseUrl", "BaseUrl"));
            MapList.Add(new Attr2FieldMap("ShortUrl", "ShortUrl"));
            MapList.Add(new Attr2FieldMap("LoopType", "LoopType"));
            MapList.Add(new Attr2FieldMap("Name", "Name"));
            MapList.Add(new Attr2FieldMap("Alias", "Alias"));
            MapList.Add(new Attr2FieldMap("SheetName", "Sheet"));
            MapList.Add(new Attr2FieldMap("ConfigId", "ConfigId"));
            MapList.Add(new Attr2FieldMap("Code", "Code"));

            _tableName = "sp_url";
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
        public static List<SUrl> FetchObject(MysqlDBHelper helper)
        {
            initMap();

            List<SUrl> rtnResult = new List<SUrl>();

            String cmdString = buildQueryCmd();
            MySqlCommand cmd = new MySqlCommand(cmdString, helper.DbConnector);
            //查询结果读取器
            MySqlDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SUrl cfg = new SUrl();

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

        internal static void buildAttribute(List<SUrl> _suList, List<SUrlAttribute> _attrList)
        {
            for (int index = 0 ; index < _suList.Count ; index++)
            {
                SUrl url = _suList[index];

                for (int inx = 0; inx < _attrList.Count ; inx ++)
                {
                    SUrlAttribute attr = _attrList[inx];

                    if (attr.UrlCode == url.Code)
                    {
                        url.Attrs.Add(attr);
                        _attrList.RemoveAt(inx--);
                    }
                }
            }
        }
    }
}
