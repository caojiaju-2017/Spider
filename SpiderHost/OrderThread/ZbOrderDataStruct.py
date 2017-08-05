#!/usr/bin/env python
# -*- coding: utf-8 -*-
import re

class ZbOrderDataStruct(object):
    def __init__(self):
        self.ReleaseDate = None
        self.Title = None
        self.PrjNum = None
        self.PrjName = None
        self.Way = None
        self.Province = None
        self.City = None
        pass

    @staticmethod
    def getDataByFliter(fliter):
        # TenderDb   ZhaoBiao
        # 先查出所有数据
        from pymongo import MongoClient
        client = MongoClient('www.h-sen.com', 27017)

        db = client['TenderDb']

        dbList = ['ZhaoBiao']
        CountList = []
        totalResults = []

        for one in dbList:
            dbSheets = db[one]

            splitList = fliter.split('|')

            for oneFliter in splitList:
                if not oneFliter:
                    continue

                abc = {"Title": re.compile(oneFliter)}
                CountList = dbSheets.find(abc)

                for item in CountList:
                    oneData = {}
                    oneData["Classfic"] = item["Classfic"]
                    oneData["ProjectNo"] = item["ProjectNo"]
                    oneData["Url"] = item["Url"]
                    oneData["ProjectName"] = item["ProjectName"]
                    oneData["Title"] = item["Title"]
                    oneData["Way"] = item["Way"]
                    oneData["Time"] = item["Time"]
                    oneData["Owner"] = item["Owner"]
                    oneData["Unique"] = item["Unique"]
                    totalResults.append(oneData)
                    pass
        return  totalResults