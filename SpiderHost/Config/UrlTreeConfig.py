#!/usr/bin/env python
# -*- coding: utf-8 -*-

# 描述url 基地址
# 描述URL循环方式
import ConfigParser
from Config.UrlItemConfig import *

class UrlTreeConfig(object):
    def __init__(self):
        self.Name = None
        self.SheetName = None
        self.Alias = None
        self.UrlId = None
        self.BaseURL = None
        self.ShortURL = None
        self.LoopType = SysLoopType.Index
        self.LoopIndex = None
        self.Attributes = []
        pass

    def buildUrlList(self):
        if not self.LoopIndex:
            return  []

        rtnUrlList = []
        for index in range(self.LoopIndex.StartIndex,self.LoopIndex.StopIndex,self.LoopIndex.Step):
            rtnUrlList.append(self.BaseURL + self.ShortURL % index)

        return  rtnUrlList

    @staticmethod
    def loadConfig(cfgFile):
        cf = ConfigParser.ConfigParser()
        cf.read(cfgFile)

        urlCount = 0
        rtnUrlCfg = []
        try:
            urlCount = int(cf.get("url", "count"))
        except Exception, ex:
            pass

        if urlCount <= 0:
            return []

        for index in range(urlCount):
            try:
                loopindex = cf.get("url_%d"%(index + 1), "loopindex")
                attrCount = int(cf.get("url_%d" % (index + 1), "attrcount"))
                baseurl = cf.get("url_%d" % (index + 1), "baseurl")
                shorturl = cf.get("url_%d" % (index + 1), "shorturl")
                looptype = cf.get("url_%d" % (index + 1), "looptype")
                name = cf.get("url_%d" % (index + 1), "name")
                alias = cf.get("url_%d" % (index + 1), "alias")
                sheetname = cf.get("url_%d" % (index + 1), "sheet")

                urlCfg = UrlTreeConfig()

                urlCfg.UrlId = "Spider"
                urlCfg.ShortURL = shorturl.lstrip()
                urlCfg.Alias = alias
                urlCfg.BaseURL = baseurl
                urlCfg.LoopType = int(looptype)
                urlCfg.Name = name
                urlCfg.SheetName = sheetname


                # 处理属性
                attrList = []
                for attrInx in range(attrCount):
                    try:
                        htmltag = cf.get("url_%d_%d" % (index + 1,attrInx + 1), "htmltag")
                        name = cf.get("url_%d_%d" % (index + 1, attrInx + 1), "name")
                        alias = cf.get("url_%d_%d" % (index + 1, attrInx + 1), "alias")
                        calcway = cf.get("url_%d_%d" % (index + 1, attrInx + 1), "calcway")
                        externstr = cf.get("url_%d_%d" % (index + 1, attrInx + 1), "externstr")
                        subattr = cf.get("url_%d_%d" % (index + 1, attrInx + 1), "subattr")
                        item = UrlItemConfig()
                        item.name = name
                        item.alias = alias
                        item.calcway = calcway
                        item.externstr = externstr.decode("utf8")
                        item.htmlTag = htmltag.replace('\'','')
                        item.subattr = subattr

                        attrList.append(item)
                    except:
                        continue
                urlCfg.Attributes = attrList
                # 处理属性----------end



                # 处理循环索引
                lpIndexs = loopindex.split('.')
                if len(lpIndexs) != 3:
                    raise
                lpTypes = LoopType()
                lpTypes.StartIndex = int(lpIndexs[0])
                lpTypes.StopIndex = int(lpIndexs[1])
                lpTypes.Step = int(lpIndexs[2])
                urlCfg.LoopIndex = lpTypes
                # 处理循环索引--------end



                rtnUrlCfg.append(urlCfg)
            except Exception,ex:
                continue

        return rtnUrlCfg

class LoopType(object):
    def __init__(self):
        self.StartIndex = 0
        self.StopIndex = 0
        self.Step = 1

class SysLoopType(object):
    Index = 0  # 按索引循环
    Static = 1 # 不循环，直接拼装
