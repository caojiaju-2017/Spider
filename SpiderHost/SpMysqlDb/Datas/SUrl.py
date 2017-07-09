#!/usr/bin/env python
# -*- coding: utf-8 -*-
import MySQLdb,sys
from SpMysqlDb.Datas.Attr2FieldMap import *

from SpMysqlDb.DbConfig import *

class SUrl(object):
    TABLE_NAME= "sp_url"
    ATTR_map_FIELD=[]
    def __init__(self):
        self.Id = None
        self.StartIndex = 0
        self.StopIndex = 0
        self.Step = 0
        self.BaseUrl = None
        self.ShortUrl = None
        self.LoopType = 0
        self.Name = None
        self.Alias = None
        self.Sheet = None
        self.ConfigId = None
        self.Code = None
        self.Enable = 1
        self.Classfic = None

        self.Attrs = []
        pass

    @staticmethod
    def initMap():
        SUrl.ATTR_map_FIELD = []
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Id","Id"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("StartIndex", "StartIndex"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("StopIndex", "StopIndex"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Step", "Step"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("BaseUrl", "BaseUrl"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("ShortUrl", "ShortUrl"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("LoopType", "LoopType"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Name", "Name"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Alias", "Alias"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Sheet", "Sheet"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("ConfigId", "ConfigId"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Code", "Code"))

        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Enable", "Enable"))
        SUrl.ATTR_map_FIELD.append(Attr2FieldMap("Classfic", "Classfic"))


    @staticmethod
    def buildQueryCmd():
        fieldString = None
        for index in range(len(SUrl.ATTR_map_FIELD)):
            oneMap = SUrl.ATTR_map_FIELD[index]
            if not fieldString:
                fieldString = oneMap.Field
            else:
                fieldString = fieldString + "," + oneMap.Field

        return "select %s from %s where 1=1"%(fieldString,SUrl.TABLE_NAME)

    @staticmethod
    def fetchDatas():
        rtnObjs = []
        SUrl.initMap()

        # 打开数据库连接
        db = MySQLdb.connect(DbConfig.ip,DbConfig.userName, DbConfig.password, DbConfig.dbName,charset='utf8')

        # 使用cursor()方法获取操作游标
        cursor = db.cursor()

        # 使用execute方法执行SQL语句
        cursor.execute(SUrl.buildQueryCmd())

        results = cursor.fetchall()

        for row in results:
            newCfg = SUrl()
            for index in range(len(SUrl.ATTR_map_FIELD)):
                oneMap = SUrl.ATTR_map_FIELD[index]
                Attr2FieldMap.setValue(newCfg,row[index],oneMap.Attribute)

            rtnObjs.append(newCfg)
        # 关闭数据库连接
        db.close()

        return rtnObjs


    def buildUrlList(self):
        rtnUrl = []
        try:
            for i in range(self.StartIndex, self.StopIndex,self.Step):
                absUrl = self.BaseUrl+self.ShortUrl
                rtnUrl.append(absUrl%i)
        except Exception,ex:
            pass
        return rtnUrl