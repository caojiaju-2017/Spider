#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
from django.views.decorators.csrf import csrf_exempt

from PIL import *
from DSServer.Api.PublicService import *
from SmsDataBuffer import *
from DSServer.models import *

from DSServer.Api.ReportServiceTender import *

# python标准库导入
import json,re,pymongo,json
from pymongo import MongoClient

from CLSms import *

from DSServer.WX.wzhifuSDK import *

# from DSServer.WX.wx_pay import  WxPay, WxPayError

class ChangeApi(object):
    @staticmethod
    def CreateOrder(request):
        print "User login"
        # url中的参数提取
        command = request.GET.get('Command')
        timesnap = request.GET.get('TimeSnap')
        sig = request.GET.get('Sig')
        servicecode = request.GET.get('ServiceCode')

        # 检查请求合法性
        dict = {}
        dict["COMMAND"] = command
        dict["TIMESNAP"] = timesnap
        dict["SIG"] = sig
        dict["ServiceCode"] = servicecode

        # 检查url合法性
        result = PublicService.validUrl(timesnap,sig)

        if not result:
            loginResut = json.dumps({"ErrorInfo": "非法URL", "ErrorId": 10001, "Result": None})
            return HttpResponse(loginResut)

        # 提取post数据
        postDataList = {}
        postDataList = getPostData(request)

        account = postDataList["Account"]
        buyNums = postDataList["Months"]
        serviceCode = servicecode

        # 1、查找业务定义数据
        serviceHandle= SpsService.objects.get(code=serviceCode)
        if not serviceHandle:
            loginResut = json.dumps({"ErrorInfo": "该业务已下线或停用", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        feeRate  = serviceHandle.feerate
        price = serviceHandle.price

        totalPrice = 0

        try:
            # 计算金额
            if feeRate[0] == "0":
                totalPrice = price * buyNums*100
            else:
                monthSwitch = int(feeRate[0:len(feeRate) - 2])
                if monthSwitch > buyNums:
                    totalPrice = price * buyNums * 100
                else:
                    rateFee = int(feeRate[-2:])
                    totalPrice = price * buyNums * 100*rateFee / 100
        except:
            totalPrice = price * buyNums * 100

        # 调用微信接口生成预订单
        currentTime = time.strftime('%Y%m%d%H%M%S',time.localtime(time.time()))
        prpareOrder = ChangeApi.InvokeWXInterface(totalPrice,currentTime)
        prpareOrder.getPrepayId()

        if prpareOrder:
            newOrder = SpsOrders()
            newOrder.account = postDataList["Account"]
            newOrder.scode = serviceCode
            newOrder.createtime = currentTime
            newOrder.state = 0
            newOrder.ordernum = prpareOrder.result["prepay_id"]
            newOrder.totalprice = totalPrice
            newOrder.months = buyNums

            commitDataList = []
            commitDataList.append(CommitData(newOrder, 0))

            # 事务提交
            try:
                result = commitCustomDataByTranslate(commitDataList)

                if not result:
                    loginResut = json.dumps({"ErrorInfo": "预付订单生成失败", "ErrorId": 99999, "Result": None})
                    return HttpResponse(loginResut)
            except Exception, ex:
                loginResut = json.dumps({"ErrorInfo": "预付订单生成失败", "ErrorId": 99999, "Result": None})
                return HttpResponse(loginResut)

        rtnDict={}
        rtnDict["OrderNo"] = prpareOrder.result["prepay_id"]
        rtnDict["MchId"] = prpareOrder.result["mch_id"]
        rtnDict["NonceStr"]= prpareOrder.result["nonce_str"]

        rtnDict["Sign"] = prpareOrder.result["sign"]
        print rtnDict["Sign"]
        rtnDict["AppId"] = prpareOrder.result["appid"]
        rtnDict["ApiKey"] = WxPayConf_pub.KEY
        rtnDict["TimeStamp"] = prpareOrder.result["timestamp"]
        rtnDict["OrderPrice"] = 1#totalPrice/100

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": rtnDict})
        return HttpResponse(loginResut)

    @staticmethod
    def InvokeWXInterface(totalPrice,currentTime):
        prpareOrder = UnifiedOrder_pub()
        prpareOrder.parameters["out_trade_no"] = currentTime
        prpareOrder.parameters["body"] = "汉森镖局-预付款单接口测试"
        prpareOrder.parameters["total_fee"] = "1"#"%d"%totalPrice
        prpareOrder.parameters["notify_url"] = "http://www.weixin.qq.com/wxpay/pay.php"
        prpareOrder.parameters["trade_type"] = "APP"

        # prpareOrder.parameters["fee_type"] = "CNY"

        return prpareOrder
