#!/usr/bin/env python
# -*- coding: utf-8 -*-
import MySQLdb,sys
from SpMysqlDb.Datas.Attr2FieldMap import *

from SpMysqlDb.DbConfig import *
reload(sys)
sys.setdefaultencoding('utf-8')

class SConfig(object):
    TABLE_NAME= "sp_config"
    ATTR_map_FIELD=[]
    def __init__(self):
        self.Id = None
        self.TimeType = None
        self.TimeSep = None
        self.EMail = None
        self.Mobile = None
        self.Code = None
        self.Name = None
        self.Enable = 1
        self.Urls = []
        pass

    @staticmethod
    def initMap():
        SConfig.ATTR_map_FIELD = []
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("Id","Id"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("TimeType", "TimeType"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("TimeSep", "TimeSep"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("EMail", "EMail"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("Mobile", "Mobile"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("Code", "Code"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("Name", "Name"))
        SConfig.ATTR_map_FIELD.append(Attr2FieldMap("Enable", "Enable"))

    @staticmethod
    def buildQueryCmd():
        fieldString = None
        for index in range(len(SConfig.ATTR_map_FIELD)):
            oneMap = SConfig.ATTR_map_FIELD[index]
            if not fieldString:
                fieldString = oneMap.Field
            else:
                fieldString = fieldString + "," + oneMap.Field

        return "select %s from %s where 1=1"%(fieldString,SConfig.TABLE_NAME)

    @staticmethod
    def fetchDatas():
        rtnConfigs = []
        SConfig.initMap()

        # 打开数据库连接
        db = MySQLdb.connect(DbConfig.ip,DbConfig.userName, DbConfig.password, DbConfig.dbName,charset='utf8')

        # 使用cursor()方法获取操作游标
        cursor = db.cursor()

        # 使用execute方法执行SQL语句
        cursor.execute(SConfig.buildQueryCmd())

        results = cursor.fetchall()

        for row in results:
            newCfg = SConfig()
            for index in range(len(SConfig.ATTR_map_FIELD)):
                oneMap = SConfig.ATTR_map_FIELD[index]
                Attr2FieldMap.setValue(newCfg,row[index],oneMap.Attribute)

            rtnConfigs.append(newCfg)
        # 关闭数据库连接
        db.close()

        return  rtnConfigs