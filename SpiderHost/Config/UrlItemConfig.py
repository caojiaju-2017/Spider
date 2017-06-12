#!/usr/bin/env python
# -*- coding: utf-8 -*-
import hashlib

class UrlItemConfig(object):
    def __init__(self):
        self.htmlTag = None
        self.name = None
        self.alias = None
        self.calcway = None
        self.externstr = None
        self.subattr = None
        self.datas = None
        pass

    @staticmethod
    def getValue(url,item,rawData):
        rtnValue = None
        if not item.subattr or len(item.subattr.lstrip()) == 0:
            rtnValue = rawData.get_text()
        else:
            rtnValue = rawData.get(item.subattr.strip())

        if item.calcway == "00":
            rtnValue = rtnValue.replace(item.externstr,"")
        elif item.calcway == "10":
            rtnValue = UrlItemConfig.getExternedValue(url,item,item.externstr) + rtnValue
        elif item.calcway == "11":
            rtnValue = rtnValue + UrlItemConfig.getExternedValue(url,item,item.externstr)
        elif item.calcway == "99":
            pass
        return  rtnValue

    @staticmethod
    def getUniqueKey(dataPaires):
        keys = dataPaires.keys()
        keys.sort()
        valueString = ""
        for oneKey in keys:
            valueString = valueString + "%s%s"%(oneKey,dataPaires[oneKey])

        utf8String = valueString.encode('utf8')
        hash_md5 = hashlib.md5(utf8String)

        return hash_md5.hexdigest()

    @staticmethod
    def getExternedValue(url,item,extStr):
        newStr = extStr.replace("{URL}", "url")
        exec_string = "rtnValue = '" + "%s'" % newStr
        #print exec_string
        exec(exec_string)

        return rtnValue

# 描述 节点在网页位置
# 描述 节点属性