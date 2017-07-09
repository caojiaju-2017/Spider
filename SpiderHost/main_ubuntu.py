#!/usr/bin/env python
# -*- coding: utf-8 -*-

import threading,os

# from multiprocessing import Pool
from Service.UrlJobExhibition import *
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
            print "Config was Disable %s" % oneConfig.Code
            continue

        t = threading.Thread(target=startWorkThread, args=(oneConfig,))
        t.daemon = True
        t.start()
    return


def startWorkThread(config):
    doJobHandle = None

    # 检查当前是否定义了处理类
    import_string = "from Service.%s import %s" % (config.JobClassName,config.JobClassName, )
    try:
        exec import_string

        build_object = "doJobHandle = %s()"%config.JobClassName

        exec  build_object

    except Exception, ex:
        print "Config Load Work Failed:%s \n"%config.Code
        return

    # 如果没有，则用通用处理函数
    doJobHandle.doWork(config)

    return

def restart_program():
    python = sys.executable
    os.execl(python, python, * sys.argv)

TIME_MAX = 60 * 60
TIME_CURRENT = 0
SLEEP_SEP = 100
if __name__ == "__main__":
    TIME_CURRENT = 0
    startMain()
    print 'Program was start'
    while TIME_MAX > TIME_CURRENT:
        print 'Wait restart %d/%d' % (TIME_CURRENT,TIME_MAX)
        time.sleep(SLEEP_SEP)
        TIME_CURRENT = TIME_CURRENT + SLEEP_SEP

    restart_program()