#!/usr/bin/env python
# -*- coding: utf-8 -*-
import  time,random
import urllib2
from bs4 import  BeautifulSoup

from SpMysqlDb.Datas.SUrlAttribute import  *
from MongoDb.DbOperator import *
from Notice.BaseEmail import *

from SpMysqlDb.Datas.NationDefine import *
ip = []
dataCollection={}
IDLE_WAIT = 60*15
class UrlJobTenderForGuizhou(object):
    StopFlag = False
    def __init__(self):
        for index  in range(10000):
            ip.append("%d.%d.%d.%d"%(random.randint(2, 254),random.randint(2, 254),random.randint(2, 254),random.randint(2, 254)))
        return

    def doWork(self,config):
        while not UrlJobTenderForGuizhou.StopFlag :
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
                user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
                send_headers = {
                    'Host': 'www.jb51.net',
                    'User-Agent': 'Mozilla/5.0 (Windows NT 6.2; rv:16.0) Gecko/20100101 Firefox/16.0',
                    'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
                    'Connection': 'keep-alive',
                    'X-Forwarded-For': ip[random.randint(0, 9998)]
                }

                req = urllib2.Request(absoluteUrl, data=None, headers=send_headers, origin_req_host=None,
                                      unverifiable=True)

                # page = urllib2.urlopen(absoluteUrl)
                html_doc = html_doc = urllib2.urlopen(req, timeout=60).read()
                # print html_doc
            except Exception,ex:
                print ex.message
                time.sleep(1)
                continue

            soup = BeautifulSoup(html_doc, 'lxml')

            for oneAttr in oneUrl.Attrs:
                if oneAttr.AttachAttr and len(oneAttr.AttachAttr) > 0:
                    continue
                htmlTag = oneAttr.HtmlTag
                name = oneAttr.AttrName
                datas = soup.select(htmlTag)

                # 如果是url，看看能否剔除空项目
                newDatas = []
                if oneAttr.AttrName.lower() == "url":
                    for index ,one in enumerate(datas):
                        if index % 2 == 1:
                            continue
                        newDatas.append(one)

                    oneAttr.datas = newDatas
                else:
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
            user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
            send_headers = {
                'Host': 'www.jb51.net',
                'User-Agent': 'Mozilla/5.0 (Windows NT 6.2; rv:16.0) Gecko/20100101 Firefox/16.0',
                'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
                'Connection': 'keep-alive',
                'X-Forwarded-For': ip[random.randint(0, 9998)]
            }

            req = urllib2.Request(oneUrl, data=None, headers=send_headers, origin_req_host=None,
                                  unverifiable=True)

            # page = urllib2.urlopen(absoluteUrl)
            html_doc = html_doc = urllib2.urlopen(req, timeout=60).read()

            # page = urllib2.urlopen(oneUrl, timeout=30)
            # html_doc = page.read()
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


                    if oneAttr.AttrName.lower() == "url":
                        valueTemp = oneUrl.BaseUrl + valueTemp


                    data[oneAttr.AttrName] = valueTemp

            # 如果属性遍历完毕
            if nullColumn == len(oneUrl.Attrs):
                break

            # 提取记录唯一性编码
            data["Unique"] = SUrlAttribute.getUniqueKey(data)
            data["RecordTime"] = time.strftime( '%Y-%m-%d %X', time.localtime() )
            data["Way"] = "公开招标"
            data["Owner"] = "贵州省***"
            # data["ProjectNo"] = "************"
            data["Classfic"] = oneUrl.Classfic
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

                    if oneAttr.AttrName.lower() == "projectno":
                        pos = rvData.find("GZ")
                        if pos > 0:
                            rvData = rvData[pos:]

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