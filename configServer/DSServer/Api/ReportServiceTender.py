#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
from django.views.decorators.csrf import csrf_exempt

from PIL import *
from DSServer.Api.PublicService import *
from SmsDataBuffer import *
from DSServer.models import *

# python标准库导入
import json,re,pymongo
from pymongo import MongoClient

class ReportServiceTender(object):
    @staticmethod
    def buildKeyPieReport(bookHandle):
        oneConfig2 = {}
        oneConfig2["Title"] = "按客户订阅关键字分布"
        oneConfig2["YLabel"] = "发标量(个)"
        oneConfig2["XLabel"] = "关键字"
        oneConfig2["InnerLabel"] = "关键字分布"
        oneConfig2["Type"] = 1

        client = MongoClient('www.h-sen.com', 27017)
        db = client['TenderDb']
        tenderDatas = db["ZhaoBiao"]

        innerData2 = {}

        fliter1 = bookHandle.fliter1
        if not fliter1 or len(fliter1) == 0:
            fliter1 = None
            innerData2["未设置"] = 0
        else:
            count = tenderDatas.find({'ProjectName': re.compile(fliter1)}).count()
            innerData2[fliter1] = count

        fliter2 = bookHandle.fliter2
        if not fliter2 or len(fliter2) == 0:
            fliter2 = None
            innerData2["未设置"] = 0
        else:
            count = tenderDatas.find({'ProjectName': re.compile(fliter2)}).count()
            innerData2[fliter2] = count

        fliter3 = bookHandle.fliter3
        if not fliter3 or len(fliter3) == 0:
            fliter3 = None
            innerData2["未设置"] = 0
        else:
            count = tenderDatas.find({'ProjectName': re.compile(fliter3)}).count()
            innerData2[fliter3] = count

        oneConfig2["Values"] = innerData2
        client.close()
        return oneConfig2

    @staticmethod
    def buildWeekColumnReport(days):
        oneConfig2 = {}
        oneConfig2["Title"] = "近一周四川省发布量统计"
        oneConfig2["YLabel"] = "发标量(个)"
        oneConfig2["XLabel"] = "日期"
        oneConfig2["InnerLabel"] = "按日期统计一个月"
        oneConfig2["Type"] = 2

        client = MongoClient('www.h-sen.com', 27017)
        db = client['TenderDb']
        tenderDatas = db["ZhaoBiao"]

        innerData2 = {}

        dateList =  ReportServiceTender.generalBeforeDataList(days)

        for one in dateList:
            count = tenderDatas.find({'Time': re.compile(one)}).count()
            innerData2[one] = count

        oneConfig2["Values"] = innerData2
        client.close()
        return oneConfig2

    @staticmethod
    def buildHistoryLineReport(days):
        oneConfig2 = {}
        oneConfig2["Title"] = "近%d天四川省发布量统计"%days
        oneConfig2["YLabel"] = "发标量(个)"
        oneConfig2["XLabel"] = "日期"
        oneConfig2["InnerLabel"] = "按日期统计一个月"
        oneConfig2["Type"] = 0

        client = MongoClient('www.h-sen.com', 27017)
        db = client['TenderDb']
        tenderDatas = db["ZhaoBiao"]

        innerData2 = {}

        dateList =  ReportServiceTender.generalBeforeDataList(days)

        for one in dateList:
            count = tenderDatas.find({'Time': re.compile(one)}).count()
            innerData2[one] = count

        oneConfig2["Values"] = innerData2
        client.close()
        return oneConfig2

    @staticmethod
    def generalBeforeDataList(days):
        rtnDateString = []

        now = datetime.datetime.now()
        # rtnDateString.append(now.strftime('%Y-%m-%d')  )
        for index in range(abs(days)):
            delta = datetime.timedelta(days=-index)
            n_days = now + delta
            rtnDateString.append(n_days.strftime('%Y-%m-%d'))

        return rtnDateString