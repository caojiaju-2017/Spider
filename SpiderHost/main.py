#!/usr/bin/env python
# -*- coding: utf-8 -*-

import os,commands,time,random,threading,bs4,lxml,shutil
# from multiprocessing import Pool
from BaseDoJob import *
from Map.Line import *


from SpMysqlDb.Datas.SConfig import *
from SpMysqlDb.Datas.SUrl import *
from SpMysqlDb.Datas.SUrlAttribute import *

serviceDirs={}
Check_Update = 60

def updateUrlAttr(urls,attrs):
    for index in range(len(urls)):
        oneUrl = urls[index]

        for inx in range(len(attrs)):
            oneAttr = attrs[inx]

            if oneUrl.Code == oneAttr.UrlCode:
                oneUrl.Attrs.append(oneAttr)
    pass

def updateConfig(configs, urls):
    for index in range(len(configs)):
        oneConfig = configs[index]

        for inx in range(len(urls)):
            oneUrl = urls[inx]

            if oneConfig.Code == oneUrl.ConfigId:
                oneConfig.Urls.append(oneUrl)
    pass

def startMain():
    configDatas = SConfig.fetchDatas()
    urlDatas = SUrl.fetchDatas()
    urlAttrDatas = SUrlAttribute.fetchDatas()

    updateUrlAttr(urlDatas,urlAttrDatas)
    updateConfig(configDatas, urlDatas)

    for oneConfig in configDatas:
        if oneConfig.Enable == 0:
            continue

        t = threading.Thread(target=startWorkThread, args=(oneConfig,))
        t.daemon = True
        t.start()
    return


def startWorkThread(config):
    doJobHandle = None

    # 检查当前是否定义了处理类
    doJobHandle = BaseDoJob()

    # 如果没有，则用通用处理函数
    doJobHandle.doWork(config)

    return

if __name__ == "__main__":
    while True:
        str = raw_input(u"请选择操作内容：\n    输入r, 运行爬虫 \n    输入q,退出程序\n你的输入：")
        if str.lower() == "r":
            break
        elif str.lower() == "q":
            exit()


    startMain()
    while True:
        str = raw_input(u"请选择操作内容：\n    输入q,退出程序\n你的输入：")
        if str.lower() == "r":
            break
        elif str.lower() == "q":
            exit()