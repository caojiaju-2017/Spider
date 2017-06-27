#!/usr/bin/env python
# -*- coding: utf-8 -*-
import  time
import urllib2
from bs4 import  BeautifulSoup

from SpMysqlDb.Datas.SUrlAttribute import  *
from MongoDb.DbOperator import *

dataCollection={}
IDLE_WAIT = 30
class BaseDoJob(object):
    StopFlag = False
    def __init__(self):
        return

    def doWork(self,config):
        while not BaseDoJob.StopFlag :
            # # 循环时间段， 检查是否具有当前需要处理的任务
            # hasValidTime = False
            # for oneTimeSep in config.TimeSep:
            #     if oneTimeSep.CurrentTimeIsValid():
            #         hasValidTime = True
            #         break
            #     continue
            #
            # # 如果沒有合法的时间段，则不进行下一步处理
            # if not hasValidTime:
            #     # 如果没有，则当前线程空转
            #     self.waitNextLoop(IDLE_WAIT)
            #     continue


            # 遍历Url配置
            for oneUrl in config.Urls:
                if oneUrl.Enable == 0:
                    self.waitNextLoop(3)
                    continue

                self.startSpiderData(oneUrl)
                self.waitNextLoop(3)
                continue

            # 等待下一次循环
            self.waitNextLoop(IDLE_WAIT)
        return

    def waitNextLoop(self,timeCount):
        time.sleep(timeCount)

    def startSpiderData(self,oneUrl):
        if not oneUrl:
            return

        urlList = []
        if oneUrl.LoopType == 1:
            url = oneUrl.BaseUrl + oneUrl.ShortUrl
            urlList.append(url)
        elif oneUrl.LoopType == 0:
            urlList = oneUrl.buildUrlList()

        for absoluteUrl in urlList:
            try:
                page = urllib2.urlopen(absoluteUrl,timeout=30)
                html_doc = page.read()
            except Exception,ex:
                time.sleep(1)
                continue

            soup = BeautifulSoup(html_doc, 'lxml')

            for oneAttr in oneUrl.Attrs:
                htmlTag = oneAttr.HtmlTag
                name = oneAttr.AttrName
                datas = soup.select(htmlTag)
                oneAttr.datas = datas

                time.sleep(1)

            # 当前连接怕取完毕
            self.writeToDb(oneUrl)
        pass

    def writeToDb(self,oneUrl):
        rowIndex = 0
        while True:
            data = {}
            nullColumn = 0
            for index in range(len(oneUrl.Attrs)):
                oneAttr = oneUrl.Attrs[index]
                if len(oneAttr.datas) <= rowIndex:
                    data[oneAttr.AttrName] = ""
                    nullColumn = nullColumn + 1
                else:
                    data[oneAttr.AttrName] = SUrlAttribute.getValue(oneUrl,oneAttr,oneAttr.datas[rowIndex])

            # 如果属性遍历完毕
            if nullColumn == len(oneUrl.Attrs):
                break

            # 提取记录唯一性编码
            data["Unique"] = SUrlAttribute.getUniqueKey(data)
            rowIndex = rowIndex+ 1

            result = DbOperator().insertData(oneUrl.Name,oneUrl.Sheet,data)

            if result:
                self.plusCount(oneUrl.Alias)
        return

    def plusCount(self,keyName):
        global dataCollection
        currentCount = 0
        if dataCollection.has_key(keyName):
            currentCount = dataCollection[keyName] + 1

        dataCollection[keyName] = currentCount
        # print dataCollection