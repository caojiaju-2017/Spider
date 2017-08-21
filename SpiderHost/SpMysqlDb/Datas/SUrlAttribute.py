#!/usr/bin/env python
# -*- coding: utf-8 -*-

import MySQLdb,sys,hashlib
from SpMysqlDb.Datas.Attr2FieldMap import *

from SpMysqlDb.DbConfig import *
reload(sys)
sys.setdefaultencoding('utf-8')

class SUrlAttribute(object):
    TABLE_NAME= "sp_url_attr"
    ATTR_map_FIELD=[]

    def __init__(self):
        self.Id = 0
        self.UrlCode = None
        self.HtmlTag = None
        self.AttrName = None
        self.Alias = None
        self.CalcWay = None
        self.ExternStr = None
        self.SubAttr = None
        self.AttachAttr=None
        self.Code = None
        self.IsUrl = 0
        self.datas = []
        pass


    @staticmethod
    def initMap():
        SUrlAttribute.ATTR_map_FIELD = []
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("Id","Id"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("UrlCode", "UrlCode"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("HtmlTag", "HtmlTag"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("AttrName", "AttrName"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("Alias", "Alias"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("CalcWay", "CalcWay"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("ExternStr", "ExternStr"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("SubAttr", "SubAttr"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("AttachAttr", "AttachAttr"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("Code", "Code"))
        SUrlAttribute.ATTR_map_FIELD.append(Attr2FieldMap("IsUrl", "IsUrl"))

    @staticmethod
    def buildQueryCmd():
        fieldString = None
        for index in range(len(SUrlAttribute.ATTR_map_FIELD)):
            oneMap = SUrlAttribute.ATTR_map_FIELD[index]
            if not fieldString:
                fieldString = oneMap.Field
            else:
                fieldString = fieldString + "," + oneMap.Field

        return "select %s from %s where 1=1"%(fieldString,SUrlAttribute.TABLE_NAME)

    @staticmethod
    def fetchDatas():
        rtnConfigs = []
        SUrlAttribute.initMap()

        # 打开数据库连接
        db = MySQLdb.connect(DbConfig.ip,DbConfig.userName, DbConfig.password, DbConfig.dbName,charset='utf8')

        # 使用cursor()方法获取操作游标
        cursor = db.cursor()

        # 使用execute方法执行SQL语句
        cursor.execute(SUrlAttribute.buildQueryCmd())

        results = cursor.fetchall()

        for row in results:
            newCfg = SUrlAttribute()
            for index in range(len(SUrlAttribute.ATTR_map_FIELD)):
                oneMap = SUrlAttribute.ATTR_map_FIELD[index]
                Attr2FieldMap.setValue(newCfg,row[index],oneMap.Attribute)

            rtnConfigs.append(newCfg)
        # 关闭数据库连接
        db.close()

        return  rtnConfigs


    @staticmethod
    def getValue(url, item, rawData):
        rtnValue = None
        try:
            if not item.SubAttr or len(item.SubAttr.lstrip()) == 0:
                rtnValue = rawData.get_text()
            else:
                rtnValue = rawData.get(item.SubAttr.strip())
        except:
            pass

        if item.CalcWay == "00":
            rtnValue = rtnValue.replace(item.ExternStr, "")
        elif item.CalcWay == "10":
            rtnValue = SUrlAttribute.getExternedValue(url, item, item.ExternStr) + rtnValue
        elif item.CalcWay == "11":
            rtnValue = rtnValue + SUrlAttribute.getExternedValue(url, item, item.ExternStr)
        elif item.CalcWay == "12":
            rtnValue = item.SubAttr
        elif item.CalcWay == "99":
            pass
        return rtnValue

    @staticmethod
    def getUniqueKey(dataPaires):
        keys = dataPaires.keys()
        keys.sort()
        valueString = ""
        for oneKey in keys:
            valueString = valueString + "%s%s" % (oneKey, dataPaires[oneKey])

        utf8String = valueString.encode('utf8')
        hash_md5 = hashlib.md5(utf8String)

        return hash_md5.hexdigest()

    @staticmethod
    def getExternedValue(url, item, extStr):
        newStr = extStr.replace("{URL}", "url")
        exec_string = "rtnValue = " + "%s" % newStr
        # print exec_string
        exec (exec_string)

        return rtnValue