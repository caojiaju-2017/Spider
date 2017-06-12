#!/usr/bin/env python
# -*- coding: utf-8 -*-
import  time
import urllib2
from bs4 import  BeautifulSoup

from Config.UrlItemConfig import  *
from MongoDb.DbOperator import *

dataCollection={}
IDLE_WAIT = 30
class BaseDoJob(object):
    StopFlag = False
    def __init__(self):
        self.emailConfigs = None
        self.tmConfig = None
        self.urlConfigs = None
        return

    def doWork(self):
        while not BaseDoJob.StopFlag :
            # 循环时间段， 检查是否具有当前需要处理的任务
            hasValidTime = False
            for oneTimeSep in self.tmConfig:
                if oneTimeSep.CurrentTimeIsValid():
                    hasValidTime = True
                    break
                continue

            # 如果沒有合法的时间段，则不进行下一步处理
            if not hasValidTime:
                # 如果没有，则当前线程空转
                self.waitNextLoop(IDLE_WAIT)
                continue


            # 遍历Url配置
            for oneItem in self.urlConfigs:
                self.startSpiderData(oneItem)
                self.waitNextLoop(3)
                continue

            # 等待下一次循环
            self.waitNextLoop(IDLE_WAIT)
        return

    def waitNextLoop(self,timeCount):
        time.sleep(timeCount)

    def startSpiderData(self,item):
        if not item:
            return

        urlList = []
        if item.LoopType == 1:
            url = item.BaseURL + item.ShortURL
            urlList.append(url)
        elif item.LoopType == 0:
            urlList = item.buildUrlList()

        for oneUrl in urlList:
            # print oneUrl

            try:
                page = urllib2.urlopen(oneUrl,timeout=30)
                html_doc = page.read()
            except Exception,ex:
                time.sleep(1)
                continue

            soup = BeautifulSoup(html_doc, 'lxml')

            for oneItem in item.Attributes:
                htmlTag = oneItem.htmlTag
                name = oneItem.name
                datas = soup.select(htmlTag)
                oneItem.datas = datas

                time.sleep(1)
            # 当前连接怕取完毕
            self.writeToDb(item)
        pass

    def writeToDb(self,item):
        rowIndex = 0
        while True:
            data = {}
            nullColumn = 0
            for index in range(len(item.Attributes)):
                oneAttr = item.Attributes[index]
                if len(oneAttr.datas) <= rowIndex:
                    data[oneAttr.name] = ""
                    nullColumn = nullColumn + 1
                else:
                    data[oneAttr.name] = UrlItemConfig.getValue(item,oneAttr,oneAttr.datas[rowIndex])

            # 如果属性遍历完毕
            if nullColumn == len(item.Attributes):
                break

            # 提取记录唯一性编码
            data["Unique"] = UrlItemConfig.getUniqueKey(data)
            rowIndex = rowIndex+ 1

            result = DbOperator().insertData(item.Name,item.SheetName,data)

            if result:
                self.plusCount(item.Alias)
        return

    def plusCount(self,keyName):
        global dataCollection
        currentCount = 0
        if dataCollection.has_key(keyName):
            currentCount = dataCollection[keyName] + 1

        dataCollection[keyName] = currentCount
        # print dataCollection