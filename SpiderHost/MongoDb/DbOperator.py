#!/usr/bin/env python
# -*- coding: utf-8 -*-

import pymongo

from MongoDb.DbConfig import *
class DbOperator(object):
    def __init__(self):
        self.linkConfig = DbConfig()
        self.connectDb()
        pass

    def connectDb(self):
        self.client = pymongo.MongoClient(self.linkConfig.ip,self.linkConfig.port)
        return

    def insertData(self,dbName,shName,datas):
        db = self.client[dbName]
        sheet_page= db[shName.decode('utf8')]

        try:
            unique = datas["Unique"]

            if self.checkExistData(unique,dbName,shName):
                # print "exist skip"
                return False
            else:
                print 'new tie ,write',datas
        except:
            return False

        sheet_page.insert_one(datas)
        return True

    def checkExistData(self,key,dbname,shname):
        db = self.client[dbname]
        sheet_page= db[shname]
        for post in sheet_page.find({"Unique":key}):
            return True
        return False

if __name__ == "__main__":
    DbOperator().checkExistData("","zhtelecom","TradeShow")