#!/usr/bin/env python
# -*- coding: utf-8 -*-

# import xlwt
import xlrd
import xlwt,os

class BuildExcel(object):
    def __init__(self):
        pass

    @staticmethod
    def buildExcel(datas):
        file = xlwt.Workbook()  # 注意这里的Workbook首字母是大写，无语吧
        table = file.add_sheet('Test')
        index = 2
        for one in datas:
            index = index + 1
            table.write(index, 1, one["Title"])
            table.write(index, 2, one["Classfic"])
            table.write(index, 3, one["ProjectNo"])
            table.write(index, 4, one["ProjectName"])
            table.write(index, 5, one["Way"])
            table.write(index, 6, one["Owner"])
            table.write(index, 7, one["Time"])
            table.write(index, 8, one["Url"])


        # 表头写入
        index = 2
        table.write(index, 1, u"标题")
        table.write(index, 2, u"地区")
        table.write(index, 3, u"项目编号")
        table.write(index, 4, u"项目名称")
        table.write(index, 5, u"招标方式")
        table.write(index, 6, u"项目业主")
        table.write(index, 7,u"发布时间")
        table.write(index, 8, u"详情")

        file.save('demo.xls')
        return os.path.join(os.getcwd(), 'demo.xls')