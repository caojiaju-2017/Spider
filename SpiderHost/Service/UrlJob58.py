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
class UrlJob58(object):
    StopFlag = False
    def __init__(self):
        return

    def doWork(self,config):
        while not UrlJob58.StopFlag :
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

                successCount,urls = self.startSpiderData(oneUrl)

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
            successCount,emailUrls = self.writeToDb(oneUrl)
        return  successCount,emailUrls

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

        if len(datas) > 0:
            return  datas[0]
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
                    data[oneAttr.AttrName] = valueTemp

            # 如果属性遍历完毕
            if nullColumn == len(oneUrl.Attrs):
                break

            # 提取记录唯一性编码
            data["Unique"] = SUrlAttribute.getUniqueKey(data)
            # data["Nation"] = NationDefine.getNation(data["Name"])
            # data["Classfic"] = "人脸识别"
            rowIndex = rowIndex+ 1

            # 处理关联属性============================================
            for oneAttr in oneUrl.Attrs:
                if oneAttr.AttachAttr and len(oneAttr.AttachAttr) > 0:
                    parentAttr = self.findParent(oneAttr.AttachAttr,oneUrl.Attrs)
                    if not parentAttr:
                        continue

                    try:
                        rvData = self.spiderCombineAttr(oneAttr.HtmlTag,data[parentAttr.AttrName])
                    except Exception,ex:
                        pass

                    data[oneAttr.AttrName] = rvData
                # time.sleep(1)
            # 处理关联属性============================================

            if DbOperator().insertData(oneUrl.Name,oneUrl.Sheet,data):
                result = result + 1
                rtnUrls.append("%10s %s" % (data['Price'],data['Url']))

            # if result:
            #     self.plusCount(oneUrl.Alias)
        return result,rtnUrls

    def plusCount(self,keyName):
        global dataCollection
        currentCount = 0
        if dataCollection.has_key(keyName):
            currentCount = dataCollection[keyName] + 1

        dataCollection[keyName] = currentCount
        # print dataCollection