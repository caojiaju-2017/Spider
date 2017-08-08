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
            CountList = dbSheets.find()

            for item in CountList:
                if len(totalResults) >= 20:
                    break

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
        return totalResults,CountList

    @staticmethod
    def getService2Data(qStruct):
        # 先查出所有数据
        from pymongo import MongoClient
        client = MongoClient('www.h-sen.com', 27017)

        db = client['TenderDb']

        dbList = ['ZhaoBiao']
        CountList = []
        OneObjects = []
        totalResults = []

        for one in dbList:
            dbSheets = db[one]

            CountList = dbSheets.find()
            # tempIndex = sheetCount

            for item in CountList:
                if len(totalResults) >= 20:
                    break

                oneData = {}
                oneData["Classfic"] = item["Classfic"]
                oneData["ProjectNo"] = item["ProjectNo"]
                oneData["Url"] = item["Url"]
                oneData["ProjectName"] = item["ProjectName"]
                oneData["Title"] = item["Title"]
                oneData["Way"] = item["Way"]
                oneData["Time"] = item["Time"]
                oneData["Owner"] = item["Owner"]
                oneData["Unique"] = item["Time"]
                totalResults.append(oneData)
                pass
        return totalResults,CountList

    @staticmethod
    def getService3Data(qStruct):
        pass
