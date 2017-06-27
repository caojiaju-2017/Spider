using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess.DataStruct
{
    public class SConfig
    {

        #region 构造函数
        public SConfig()
        {
            initMap();
        }
        #endregion

        #region Variant and Property
        int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        string _timeType;

        public string TimeType
        {
            get { return _timeType; }
            set { _timeType = value; }
        }
        string _timeSep;

        public string TimeSep
        {
            get { return _timeSep; }
            set { _timeSep = value; }
        }
        string _email;

        public string EMail
        {
            get { return _email; }
            set { _email = value; }
        }
        string _mobile;

        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
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
            MapList.Add(new Attr2FieldMap("TimeType", "TimeType"));
            MapList.Add(new Attr2FieldMap("TimeSep", "TimeSep"));
            MapList.Add(new Attr2FieldMap("EMail", "EMail"));
            MapList.Add(new Attr2FieldMap("Mobile", "Mobile"));
            MapList.Add(new Attr2FieldMap("Code", "Code"));
            MapList.Add(new Attr2FieldMap("Name", "Name"));

            _tableName = "sp_config";
        }
        #endregion

        #region DB Persiteanl
        private static string  buildQueryCmd()
        {
            string rtnString = null;
            for (int index = 0 ; index < MapList.Count; index++)
            {
                Attr2FieldMap map = MapList[index];
                if (rtnString == null)
                {
                    rtnString = map.Field;
                    continue;
                }

                rtnString = rtnString + "," + map.Field;
            }

            return String.Format("select {0} from {1} where 1=1;",rtnString,_tableName) ;
        }
        public static List<SConfig>  FetchObject(MysqlDBHelper helper)
        {
            initMap();

            List<SConfig> rtnResult = new List<SConfig>();

            String cmdString = buildQueryCmd();
            MySqlCommand cmd = new MySqlCommand(cmdString, helper.DbConnector);
            //查询结果读取器
            MySqlDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SConfig cfg = new SConfig();

                    for (int index = 0; index < MapList.Count; index++)
                    {
                        Attr2FieldMap map = MapList[index];

                        Attr2FieldMap.setValue(cfg, reader[index],map.Field);
                    }

                    rtnResult.Add(cfg);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (reader!=null)
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
