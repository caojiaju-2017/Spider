#!/usr/bin/env python
# -*- coding: utf-8 -*-

import threading,datetime,md5,os
import  matplotlib.pyplot as plt
import numpy as np
from pylab import mpl

from DisplayServer.settings import *
class HsShareData:
    dataLock = threading.Lock()
    Version = 'Act_V1.0'
    KeyCode = {}


    SigCode = 'af?Fzio2u'


    GuestAccessDict = {}
    GuestMaxAccessCount = 50

    SmsListData = []

    @staticmethod
    def getDocumentDir():
        return  os.getcwd()


    @staticmethod
    def DrawBar(dicts,name):
        # print dicts
        mpl.rcParams['font.sans-serif'] = ['FangSong']  # 指定默认字体
        mpl.rcParams['axes.unicode_minus'] = False  # 解决保存图像是负号'-'显示为方块的问题

        vls = []
        labels = []
        for key, value in dicts.items():
            labels.append(key.decode('utf8'))
            vls.append(value)

        n_groups = len(labels)
        # means_men = (20, 35, 30, 35, 27)
        # means_women = (25, 32, 34, 20, 25)

        fig, ax = plt.subplots()
        index = np.arange(n_groups)
        bar_width = 0.35

        opacity = 0.4
        rects1 = plt.bar(index, tuple(vls), bar_width, alpha=opacity, color='b', label=u'板块动态统计')
        # rects2 = plt.bar(index + bar_width, means_women, bar_width, alpha=opacity, color = 'r', label = 'Women')

        plt.xlabel(u'科目')
        plt.ylabel(u'平均成绩')
        plt.title(u'成绩统计')
        plt.xticks(index + bar_width, labels)
        plt.ylim(0, max(vls) + 20)
        plt.legend()

        # plt.tight_layout()

        savename = os.path.join(STATIC_ROOT,"ReportImage")
        savename = os.path.join(savename, name)
        plt.savefig(savename)
        # plt.show()
        return "/static/ReportImage/%s" % name


# class SmsInfo:
#     SMS_SEP = 60
#     def __init__(self):
#         self.Code = None
#         self.Time = None
#
#     def checkTimeOut(self):
#         starttime = datetime.datetime.strptime(self.Time, "%Y-%m-%d %H:%M:%S")
#         endtime = datetime.datetime.now()
#         sep = (endtime - starttime).seconds
#
#         if sep >= SmsInfo.SMS_SEP:
#             return  True
#         return False