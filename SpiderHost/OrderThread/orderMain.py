#!/usr/bin/env python
# -*- coding: utf-8 -*-

import threading,os,sys,time

from SpMysqlDb.Datas.SpsUserOrder import *
from Notice.BaseEmail import *
from OrderThread.ZbOrderDataStruct import *
from OrderThread.BuildExcel import *

def startWorkThread():
    # 查询配置数据
    configDatas = SpsUserOrder.fetchDatas()

    # 检查用户订阅是否过期
    currentDate = time.strftime('%Y-%m-%d',time.localtime(time.time()))

    dateValidDatas = []
    for oneConf in configDatas:
        if oneConf.OverTime < currentDate:
            continue
        if oneConf.Enable == 0:
            continue

        dateValidDatas.append(oneConf)

    # 每个用户一个线程
    for oneConfig in dateValidDatas:
        t = threading.Thread(target=threadWork, args=(oneConfig,))
        t.daemon = True
        t.start()

    return

def threadWork(config):
    # 提取用户设置的订阅配置
    fliter = config.Fliters
    email = config.EMail
    serviceCode = config.SCode


    results = []
    # 查询数据
    if serviceCode == "HS_ZB_RP_0001":  # 招标数据订阅
        results = ZbOrderDataStruct.getDataByFliter(fliter)
        pass
    elif serviceCode == "HS_WB_RP_0001":  #外包数据订阅
        pass

    for oneResult in results:
        print oneResult["Title"]
    # 生成excel导出数据
    fileName = BuildExcel.buildExcel(results)

    # 邮件推送
    BaseEmail.sendMailHasAttach(email,[],"Test", fileName)
    pass


def startRestartSchema():
    python = sys.executable
    os.execl(python, python, * sys.argv)
    pass

if __name__ == "__main__":
    startWorkThread()

    # 启动定时重启线程
    # startRestartSchema()

    result = raw_input("waiting:")