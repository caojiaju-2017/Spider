#!/usr/bin/env python
# -*- coding: utf-8 -*-

import os,commands,time,random,threading,bs4,lxml,shutil
# from multiprocessing import Pool
from Config.EmailConfig import *
from Config.TimeConfig import *
from Config.UrlTreeConfig import *
from BaseDoJob import *
from Map.Line import *


serviceDirs={}
Check_Update = 60

def listCustomService():
    # 获取当前目录r
    currentDir = os.getcwd()

    # 获取动态目录
    ServiceDir= os.path.join(currentDir,"Service")

    # 扫描业务自定义
    return scan_files(ServiceDir)

def scan_files(directory):
    '''
    扫描目录下的子目录
    :param directory:父亲目录
    :return:
    '''
    dirs_list = []

    for root, sub_dirs, files in os.walk(directory):
        for special_dir in sub_dirs:
            if isStopService(special_dir):
                continue

            dirs_list.append(os.path.join(root, special_dir))

    # print dirs_list
    return dirs_list

def isStopService(srvDir):
    if srvDir[-2:] == "No":
        return True
    return False

def startMain():
    global  serviceDirs
    # 提取当前 目录下的 Service
    serviceDirs = listCustomService()

    # 循环Service下的目录
    # pool = Pool()
    # pool.map(startWorkThread, serviceDirs)

    for oneDir in serviceDirs:
        t = threading.Thread(target=startWorkThread, args=(oneDir,))
        t.daemon = True
        t.start()
    return


def startWorkThread(srvDir):
    dirCurrentService =  os.path.split(srvDir)[-1]

    print srvDir

    doJobHandle = None
    # 检查当前是否定义了处理类
    import_string = "from Service.%s.%s import %s" % (dirCurrentService,'ServiceDoJob','ServiceDoJob')
    try:
        exec import_string
        doJobHandle = ServiceDoJob()
    except Exception,ex:
        doJobHandle = BaseDoJob()

    # 配置文件途径
    cfgFile = os.path.join(srvDir,'config.ini')

    doJobHandle.tmConfig = TimeConfig.loadConfig(cfgFile)

    # 构建URL配置---反射
    doJobHandle.urlConfigs = UrlTreeConfig.loadConfig(cfgFile)

    # 构建Notice配置----反射
    doJobHandle.emailConfigs = EmailConfig.loadConfig(cfgFile)

    # 如果没有，则用通用处理函数
    doJobHandle.doWork()

    return

def hasNewService():
    global serviceDirs

    while True:
        dirs = listCustomService()

        for oneDir in dirs:
            if oneDir in serviceDirs:
                continue

            t = threading.Thread(target=startWorkThread, args=(oneDir,))
            t.daemon = True
            t.start()

        time.sleep(Check_Update)
    pass

def startTimeCheck():
    t = threading.Thread(target=hasNewService)
    t.daemon = True
    t.start()

def enableService(flag):
    # 获取当前目录
    currentDir = os.getcwd()

    # 获取动态目录
    ServiceDir= os.path.join(currentDir,"Service")

    dirs = os.listdir(ServiceDir)

    for one in dirs:
        if os.path.isfile(one):
            continue

        if flag:
            oneTemp = one.rstrip("No")
            try:
                shutil.move(os.path.join(ServiceDir,one),os.path.join(ServiceDir,oneTemp))
            except Exception,ex:
                print ex
        else:
            if one[-1:2] == "No":
                continue

            try:
                shutil.move(os.path.join(ServiceDir,one),os.path.join(ServiceDir,one+"No"))
            except Exception,ex:
                print ex

def dynimicData():
    print "dynimicData"
    for key, value in dataCollection.items():
        print key, ' 帖子动态====>>', value
    pass

def generalMap(input):
    try:
        inputType = int(input[1:])
        DrawBar(dataCollection)
    except Exception,ex:
        print ex.message
        print "请输入制图类型，如：g1表示柱状图"
        return
    pass

if __name__ == "__main__":
    while True:
        str = raw_input(u"请选择操作内容：\n    输入r, 运行爬虫 \n    输入s,设置当前业务为暂停业务\n    输入e,启用所有暂停业务\n    输入c,统计板块动态\n    输入g,制图\n    输入q,退出程序\n你的输入：")
        if str.lower() == "r":
            break
        elif str.lower() == "s":
            enableService(False)
            continue
        elif str.lower() == "e":
            enableService(True)
        elif str.lower() == "c":
            dynimicData()
        elif str[:1].lower() == "g":
            generalMap(str)
        elif str.lower() == "q":
            exit()


    startMain()

    # 启动线程检查是否有新业务部署
    startTimeCheck()
    while True:
        str = raw_input(u"请选择操作内容：\n    输入r, 运行爬虫 \n    输入s,设置当前业务为暂停业务\n    输入e,启用所有暂停业务\n    输入c,统计板块动态\n    输入g,制图\n    输入q,退出程序\n你的输入：")
        if str.lower() == "r":
            break
        elif str.lower() == "s":
            enableService(False)
            continue
        elif str.lower() == "e":
            enableService(True)
        elif str.lower() == "c":
            dynimicData()
        elif str[:1].lower() == "g":
            generalMap(str)
        elif str.lower() == "q":
            exit()