#!/usr/bin/env python
# -*- coding: utf-8 -*-
import  time
import urllib2
from bs4 import  BeautifulSoup

from SpMysqlDb.Datas.SUrlAttribute import  *
from MongoDb.DbOperator import *
from Notice.BaseEmail import *

from SpMysqlDb.Datas.NationDefine import *

dataCollection={}
IDLE_WAIT = 60*10
class UrlJobTenderForJiangSu(object):
    StopFlag = False
    def __init__(self):
        return

    def doWork(self,config):
        while not UrlJobTenderForJiangSu.StopFlag :
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
                    print "Url was Disable %s" % oneUrl.Code
                    self.waitNextLoop(3)
                    continue

                self.startSpiderData(oneUrl)

                # if successCount > 0:
                #     BaseEmail.sendMail(config,urls,oneUrl.Alias)

                self.waitNextLoop(1)


                continue

            # 等待下一次循环
            print "准备循环刷新"
            self.waitNextLoop(IDLE_WAIT)
        return

    def waitNextLoop(self,timeCount):
        time.sleep(timeCount)

    def startSpiderData(self,oneUrl):
        if not oneUrl:
            return

        successCount = 0
        emailUrls = []
        urlList = []
        if oneUrl.LoopType == 1:
            url = oneUrl.BaseUrl + oneUrl.ShortUrl
            urlList.append(url)
        elif oneUrl.LoopType == 0:
            urlList = oneUrl.buildUrlList()

        for absoluteUrl in urlList:
            print absoluteUrl
            try:
                page = urllib2.urlopen(absoluteUrl,timeout=30)
                html_doc = page.read()
            except Exception,ex:
                time.sleep(1)
                continue

            soup = BeautifulSoup(html_doc, 'lxml')

            for oneAttr in oneUrl.Attrs:
                if oneAttr.AttachAttr and len(oneAttr.AttachAttr) > 0:
                    continue
                htmlTag = oneAttr.HtmlTag
                name = oneAttr.AttrName
                datas = soup.select(htmlTag)
                oneAttr.datas = datas

                time.sleep(1)

            # 当前连接怕取完毕
            self.writeToDb(oneUrl)
        return

    def spiderCombineAttr(self,htmlTag, oneUrl):
        if not oneUrl:
            return  ""

        if not htmlTag:
            return ""

        time.sleep(3)
        try:
            page = urllib2.urlopen(oneUrl, timeout=30)
            html_doc = page.read()
        except Exception, ex:
            return ""

        soup = BeautifulSoup(html_doc, 'lxml')


        datas = soup.select(htmlTag)

        if len(datas) > 0 and datas[0]:
            return  datas[0].get_text()
        else:
            return  ""

    def findParent(self,code, attrs):
        for oneAttr in attrs:
            if oneAttr.Code == code:
                return  oneAttr

        return None

    def writeToDb(self,oneUrl):
        rowIndex = 0
        result = 0
        rtnUrls = []
        while True:
            data = {}
            nullColumn = 0
            for index in range(len(oneUrl.Attrs)):
                oneAttr = oneUrl.Attrs[index]

                if oneAttr.AttachAttr and len(oneAttr.AttachAttr) > 0:
                    continue

                if len(oneAttr.datas) <= rowIndex:
                    data[oneAttr.AttrName] = ""
                    nullColumn = nullColumn + 1
                else:
                    valueTemp = SUrlAttribute.getValue(oneUrl,oneAttr,oneAttr.datas[rowIndex])
                    if valueTemp:
                        valueTemp = valueTemp.replace('<hl>','')
                        valueTemp = valueTemp.replace('</hl>', '')
                        valueTemp = valueTemp.strip()

                    if oneAttr.AttrName.lower() == "time":
                        valueTemp = valueTemp[-10:]
                    if oneAttr.AttrName.lower() == "url":
                        if valueTemp[0:1] == ".":
                            valueTemp = valueTemp[1:]
                        valueTemp = oneUrl.BaseUrl + "/cgxx/cggg"  + valueTemp
                    data[oneAttr.AttrName] = valueTemp

            # 如果属性遍历完毕
            if nullColumn == len(oneUrl.Attrs):
                break

            # 提取记录唯一性编码
            data["Unique"] = SUrlAttribute.getUniqueKey(data)
            data["RecordTime"] = time.strftime( '%Y-%m-%d %X', time.localtime() )
            data["Way"] = "公开招标"
            data["Classfic"] = oneUrl.Classfic
            data["Owner"] = "江苏省***"
            rowIndex = rowIndex+ 1

            # 处理关联属性============================================
            for oneAttr in oneUrl.Attrs:
                if oneAttr.AttachAttr and len(oneAttr.AttachAttr) > 0:
                    parentAttr = self.findParent(oneAttr.AttachAttr,oneUrl.Attrs)
                    if not parentAttr:
                        continue
                    rvData = None
                    try:
                        rvData = self.spiderCombineAttr(oneAttr.HtmlTag,data[parentAttr.AttrName])
                    except Exception,ex:
                        pass

                    data[oneAttr.AttrName] = rvData
                # time.sleep(1)
            # 处理关联属性============================================

            if DbOperator().insertData(oneUrl.Name,oneUrl.Sheet,data):
                pass

            # if result:
            #     self.plusCount(oneUrl.Alias)
        return

    def plusCount(self,keyName):
        global dataCollection
        currentCount = 0
        if dataCollection.has_key(keyName):
            currentCount = dataCollection[keyName] + 1

        dataCollection[keyName] = currentCount
        # print dataCollection