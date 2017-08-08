#!/usr/bin/env python
# -*- coding: utf-8 -*-
# from HsShareData import *
import uuid,os,gc
import  matplotlib.pyplot as plt
import numpy as np
from pylab import mpl

from DisplayServer.settings import *

class BuildImage(object):
    @staticmethod
    def buildImage(datas,attrName,type):
        oneObjs = datas

        dicts = {}

        for index,one in enumerate(oneObjs):
            className = None
            execstr = "className = one['%s']"%attrName
            exec execstr

            if className:
                className = className.encode('utf-8')

            if dicts.has_key(className):
                dicts[className] = dicts[className] + 1
            else:
                dicts[className] = 1

        # print "buildImage2"

        rtnFileName = None
        if type == ImageType.Bar:
            rtnFileName = BuildImage.DrawBar(dicts, "%s.png"%uuid.uuid1(),u"展会类别",u"展会数量",u"2017年度国际展会统计")
            pass
        # print "buildImage3"
        return rtnFileName

    @staticmethod
    def buildImage2(datas, attrName, type):
        # print "buildImage1"
        oneObjs = datas

        dicts = {}

        for index, one in enumerate(oneObjs):
            className = None
            execstr = "className = one['%s']" % attrName
            exec execstr

            if className:
                className = className.encode('utf-8')

            if dicts.has_key(className):
                dicts[className] = dicts[className] + 1
            else:
                dicts[className] = 1

        # print "buildImage2"

        rtnFileName = None
        if type == ImageType.Bar:
            rtnFileName = BuildImage.DrawBar(dicts, "%s.png" % uuid.uuid1(), u"日期轴", u"标的数量", u"四川省招标数据统计(按日期)")
            pass
        # print "buildImage3"
        return rtnFileName

    @staticmethod
    def DrawBar(dicts,name,xNanme,yName,title):
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

        print 'before plt.subplots()'
        # fig, ax = plt.subplots()


        index = np.arange(n_groups)

        print 'before plt.subplots()-a'

        bar_width = 0.35

        opacity = 0.4
        rects1 = plt.bar(index, tuple(vls), bar_width, alpha=opacity, color='b', label=u'展会数据统计')
        print 'after plt.subplots()1'
        plt.xlabel(xNanme)
        plt.ylabel(yName)
        plt.title(title)
        plt.xticks(index + bar_width, labels)
        plt.ylim(0, max(vls) + 20)
        # plt.legend()
        print 'after plt.subplots()2'
        # plt.tight_layout()

        savename = os.path.join(STATIC_ROOT,"ReportImage")
        savename = os.path.join(savename, name)
        plt.savefig(savename)
        print 'after plt.subplots()3'

        del rects1

        plt.close()

        del index

        gc.collect()
        # plt.show()
        return "/static/ReportImage/%s" % name
class ImageType(object):
    Dot = 0
    Line = 1
    Bar = 2


#http://www.codebelief.com/article/2017/04/python-data-visualization-matplotlib-learning/     绘图例子