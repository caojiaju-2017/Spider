#!/usr/bin/env python
# -*- coding: utf-8 -*-


class ServiceData(object):
    @staticmethod
    def getService1Data(qStruct):
        # 先查出所有数据
        from pymongo import MongoClient
        client = MongoClient('www.h-sen.com', 27017)

        db = client['ShowGuide']

        dbList = ['ShowGuide']
        CountList =[]
        OneObjects = []
        totalResults = []

        sheetCount = qStruct.pageSize / len(dbList)
        for one in dbList:
            dbSheets = db[one]
            CountList.append(dbSheets.find().count())
            tempIndex = sheetCount

            for item in dbSheets.find():
                if tempIndex <= 0:
                    break

                if tempIndex == sheetCount:
                    OneObjects.append(item)

                tempIndex = tempIndex - 1
                oneData = {}
                oneData["Info"] = item["Info"].strip("\u3000")
                oneData["Classfic"] = item["Classfic"]
                oneData["Name"] = item["Name"]
                oneData["Url"] = item["Url"]
                oneData["Nation"] = item["Nation"]
                oneData["Start"] = item["Start"]
                oneData["Time"] = item["Time"]
                totalResults.append(oneData)
                pass

        # 组合报表
        return CountList,OneObjects,totalResults

    @staticmethod
    def getService2Data(qStruct):
        pass

    @staticmethod
    def getService3Data(qStruct):
        pass
