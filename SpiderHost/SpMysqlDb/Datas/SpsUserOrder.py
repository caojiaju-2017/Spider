#!/usr/bin/env python
# -*- coding: utf-8 -*-
import MySQLdb,sys
from SpMysqlDb.Datas.Attr2FieldMap import *

from SpMysqlDb.DbConfig import *
reload(sys)
sys.setdefaultencoding('utf-8')

class SpsUserOrder(object):
    TABLE_NAME= "sps_user_order"
    ATTR_map_FIELD=[]
    def __init__(self):
        self.Id = None
        self.Account = None
        self.OverTime = None
        self.Fliters = None
        self.SCode = None
        self.NotifyType = None
        self.EMail = None
        self.Phone = None
        pass

    @staticmethod
    def initMap():
        SpsUserOrder.ATTR_map_FIELD = []
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("Id","Id"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("Account", "Account"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("OverTime", "OverTime"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("Fliters", "Fliters"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("SCode", "SCode"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("NotifyType", "NotifyType"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("EMail", "EMail"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("Phone", "Phone"))
        SpsUserOrder.ATTR_map_FIELD.append(Attr2FieldMap("Enable", "Enable"))

    @staticmethod
    def buildQueryCmd():
        fieldString = None
        for index in range(len(SpsUserOrder.ATTR_map_FIELD)):
            oneMap = SpsUserOrder.ATTR_map_FIELD[index]
            if not fieldString:
                fieldString = oneMap.Field
            else:
                fieldString = fieldString + "," + oneMap.Field

        return "select %s from %s where 1=1"%(fieldString,SpsUserOrder.TABLE_NAME)

    @staticmethod
    def fetchDatas():
        rtnConfigs = []
        SpsUserOrder.initMap()

        # 打开数据库连接
        db = MySQLdb.connect(DbConfig.ip,DbConfig.userName, DbConfig.password, DbConfig.dbName,charset='utf8')

        # 使用cursor()方法获取操作游标
        cursor = db.cursor()

        # 使用execute方法执行SQL语句
        cursor.execute(SpsUserOrder.buildQueryCmd())

        results = cursor.fetchall()

        for row in results:
            newCfg = SpsUserOrder()
            for index in range(len(SpsUserOrder.ATTR_map_FIELD)):
                oneMap = SpsUserOrder.ATTR_map_FIELD[index]
                Attr2FieldMap.setValue(newCfg,row[index],oneMap.Attribute)

            rtnConfigs.append(newCfg)
        # 关闭数据库连接
        db.close()

        return  rtnConfigs