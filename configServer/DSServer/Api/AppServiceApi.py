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
import json,re,pymongo
from pymongo import MongoClient

from CLSms import *

class AppServiceApi(object):
    @staticmethod
    @csrf_exempt
    def CommandDispatch(req):
        command = req.GET.get('Command').upper()
        sig = req.GET.get('Sig')
        time = req.GET.get('Time')

        if command == 'LOG_SYSTEM':
            return AppServiceApi.LogSystem(req)
        elif command  == "REG_ACCOUNT":
            return AppServiceApi.RegAccount(req)
        elif command  == "GET_BOOK":
            return AppServiceApi.GetBook(req)
        elif command  == "SET_BOOK":
            return AppServiceApi.SetBook(req)
        elif command  == "QUERY_DATA":
            return AppServiceApi.QueryData(req)
        elif command  == "GET_NORMAL_REPORT":
            return AppServiceApi.QueryReport(req)
        elif command  == "PAY_FOR_SERVICE":
            return AppServiceApi.PayForService(req)
        elif command  == "GET_USERINFO":
            return AppServiceApi.GetUserInfo(req)
        elif command  == "SET_USERINFO":
            return AppServiceApi.SetUserInfo(req)
        elif command  == "APPLY_SMSCODE":
            return AppServiceApi.ApplySmscode(req)
        return

    @staticmethod
    def LogSystem(request):
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

        users = SpsUser.objects.filter(account=postDataList["Account"])

        if len(users) != 1:
            loginResut = json.dumps({"ErrorInfo": "账户密码错误", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        user = users[0]
        if not user or user.password != postDataList["Password"]:
            loginResut = json.dumps({"ErrorInfo": "账户密码错误", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        # 获取用户业务绑定信息
        serviceBind = SpsUserService.objects.get(account=postDataList["Account"],scode=servicecode)
        overDate = True
        if not serviceBind:
            overDate = False

        # 获取业务代码
        serviceHandle= SpsService.objects.get(code=servicecode)


        if serviceBind.overdate >= time.strftime("%Y-%m-%d", time.localtime()):
            overDate = False

        rtnDictionary = {}
        rtnDictionary["OverTime"] = overDate
        rtnDictionary["OverDate"] = serviceBind.overdate

        if not serviceHandle:
            rtnDictionary["FeeInfo"] = None
        else:
            rtnDictionary["FeeInfo"] = serviceHandle.feerate

        # 返回订阅信息
        orderInfo = SpsUserOrder.objects.filter(account=postDataList["Account"],scode=servicecode)
        if len(orderInfo) == 1:
            rtnDictionary["OrderInfo"] = AppServiceApi.buildOrderInfo(orderInfo[0])
        else:
            rtnDictionary["OrderInfo"] = None

        rtnDictionary["UserInfo"] = AppServiceApi.buildUserInfo(user)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": rtnDictionary})
        return HttpResponse(loginResut)

    @staticmethod
    def RegAccount(request):
        print "User Register"
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

        # 检测短信验证
        result = SmsDataBuffer.validSms(postDataList["Account"],postDataList["SmsCode"],HsShareData.SmsListData)
        if not result:
            loginResut = json.dumps({"ErrorInfo": "短信验证信息不正确，请重新输入", "ErrorId": 10004, "Result": ""})
            return HttpResponse(loginResut)

        users = SpsUser.objects.filter(account=postDataList["Account"])

        if len(users) != 0:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        serviceHandle = SpsService.objects.get(code=servicecode)
        if not serviceHandle:
            loginResut = json.dumps({"ErrorInfo": "您选择的业务已下线或在升级维护，请稍后重试", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)

        newUser = SpsUser()
        newUser.account = postDataList["Account"]
        newUser.password = postDataList["Password"]
        newUser.longdite = 36.20394
        newUser.lantudite = 108.20394

        newUserSrv = SpsUserService()
        newUserSrv.account = newUser.account
        newUserSrv.scode = servicecode

        # 首次注册， 免费30天
        now = datetime.datetime.now()
        delta = datetime.timedelta(days=30)
        n_days = now + delta

        newUserSrv.overdate = n_days.strftime('%Y-%m-%d')


        commitDataList=[]
        commitDataList.append(CommitData(newUser, 0))
        commitDataList.append(CommitData(newUserSrv, 0))
        # 事务提交
        try:
            result = commitCustomDataByTranslate(commitDataList)

            if not result:
                loginResut = json.dumps({"ErrorInfo": "数据库操作失败", "ErrorId": 99999, "Result": None})
                return HttpResponse(loginResut)
        except Exception,ex:
            loginResut = json.dumps({"ErrorInfo":"数据库操作失败","ErrorId":99999,"Result":None})
            return HttpResponse(loginResut)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
        return HttpResponse(loginResut)

    @staticmethod
    def GetBook(request):
        print "User Register"
        # url中的参数提取
        # url中的参数提取
        command = request.GET.get('Command')
        timesnap = request.GET.get('TimeSnap')
        sig = request.GET.get('Sig')
        servicecode = request.GET.get('ServiceCode')
        account = request.GET.get('Account')

        # 检查请求合法性
        dict = {}
        dict["COMMAND"] = command
        dict["TIMESNAP"] = timesnap
        dict["SIG"] = sig
        dict["ServiceCode"] = servicecode
        dict["Account"] = account

        # 检查url合法性
        result = PublicService.validUrl(timesnap,sig)
        if not result:
            loginResut = json.dumps({"ErrorInfo": "非法URL", "ErrorId": 10001, "Result": None})
            return HttpResponse(loginResut)



        # 提取post数据
        postDataList = {}
        postDataList = getPostData(request)

        orderConfig = SpsUserOrder.objects.get(account=postDataList["Account"],scode=servicecode)

        if not orderConfig:
            loginResut = json.dumps({"ErrorInfo": "未发现订阅配置，请先配置后继续。", "ErrorId": 10002, "Result": None})
            return HttpResponse(loginResut)

        # 如果未配置任何 过滤项
        if (not orderConfig.fliter1 or len(orderConfig.fliter1) == 0) and \
                (not orderConfig.fliter2 or len(orderConfig.fliter2) == 0) and \
                (not orderConfig.fliter3 or len(orderConfig.fliter3) == 0) :
            loginResut = json.dumps({"ErrorInfo": "未发现订阅配置，请先配置后继续。", "ErrorId": 10002, "Result": None})
            return HttpResponse(loginResut)
        rtnDict = AppServiceApi.buildOrderInfo(orderConfig)
        # rtnDict={}
        # rtnDict["StartDate"] =orderConfig.startdate
        # rtnDict["StopDate"] =orderConfig.stopdate
        # rtnDict["Fliter1"] =orderConfig.fliter1
        # rtnDict["Fliter2"] =orderConfig.fliter2
        # rtnDict["Fliter3"] =orderConfig.fliter3
        # rtnDict["SCode"] =orderConfig.scode
        # rtnDict["NotifyType"] =orderConfig.notifytype
        # rtnDict["Enable"] =orderConfig.enable
        # rtnDict["Phone"] =orderConfig.phone
        # rtnDict["Emal"] =orderConfig.email


        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": rtnDict})
        return HttpResponse(loginResut)

    @staticmethod
    def SetBook(request):
        print "User Register"
        # url中的参数提取
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

        book = SpsUserOrder.objects.filter(account=postDataList["Account"],scode=servicecode)

        if len(book) != 1:
            book = None
        else:
            book = book[0]

        if not book:
            book = SpsUserOrder()

        book.account = postDataList["Account"]
        book.fliter1 = postDataList["Fliter1"]
        book.fliter2 = postDataList["Fliter2"]
        book.fliter3 = postDataList["Fliter3"]
        book.email = postDataList["EMail"]
        book.phone = postDataList["Phone"]
        book.startdate = postDataList["StartDate"]
        book.stopdate = postDataList["StopDate"]
        book.enable = int(postDataList["Enable"])
        book.scode = servicecode
        book.notifytype = 1
        try:
            book.save()
        except:
            loginResut = json.dumps({"ErrorInfo": "修改失败", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
        return HttpResponse(loginResut)

    @staticmethod
    def QueryData(request):
        print "User Register"
        # url中的参数提取
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

        # 确保唯一性
        user = SpsUser.objects.filter(account=postDataList["Account"])
        if len(user) != 1:
            user = None
        else:
            user = user[0]

        if not user:
            loginResut = json.dumps({"ErrorInfo": "账户信息异常", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        # 检查账户服务是否到期
        userServiceHandle = SpsUserService.objects.filter(account= postDataList["Account"],scode=servicecode)
        if len(userServiceHandle) != 1:
            userServiceHandle = None
        else:
            userServiceHandle = userServiceHandle[0]

        if not userServiceHandle:
            loginResut = json.dumps({"ErrorInfo": "您选择的业务已下线或在升级维护，请稍后重试", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)

        if userServiceHandle.overdate < time.strftime("%Y-%m-%d", time.localtime()):
            loginResut = json.dumps({"ErrorInfo": "您的服务期限已到，请购买后继续", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)

        Fliters = []
        # 处理用户指定过滤字符串
        try:
            fliter = postDataList["Fliter"]
            fliter = fliter.strip()
            if not fliter and len(fliter) > 0:
                Fliters.append(fliter)
        except:
            pass

        # 如果用户未指定过滤，则使用订阅配置数据
        if len(Fliters) == 0:
            # 开始查询数据
            bookHandles = SpsUserOrder.objects.filter(account=postDataList["Account"], scode=servicecode)
            bookHandle = None
            if len(bookHandles) != 1:
                loginResut = json.dumps({"ErrorInfo": "订阅配置数据异常或未配置，请检查配置", "ErrorId": 10003, "Result": ""})
                return HttpResponse(loginResut)
            else:
                bookHandle = bookHandles[0]

            # 没配置订阅项---缺少时间过滤
            if (bookHandle.fliter1 and len(bookHandle.fliter1) <= 0) or \
                    (bookHandle.fliter2 and len(bookHandle.fliter2) <= 0) or\
                    (bookHandle.fliter3 and len(bookHandle.fliter3) <= 0) or \
                            bookHandle.enable == 0:
                loginResut = json.dumps({"ErrorInfo": "订阅配置数据异常或未配置，请检查配置", "ErrorId": 10003, "Result": ""})
                return HttpResponse(loginResut)

            if bookHandle.fliter1 and len(bookHandle.fliter1) > 0:
                Fliters.append(bookHandle.fliter1)
            if bookHandle.fliter2 and len(bookHandle.fliter2) > 0:
                Fliters.append(bookHandle.fliter2)
            if bookHandle.fliter3 and len(bookHandle.fliter3) > 0:
                Fliters.append(bookHandle.fliter3)

        # 开始查询
        PageIndex = int(postDataList["PageIndex"])
        PageSize = int(postDataList["PageSize"])
        RCode = postDataList["RCode"]
        UFlag = int(postDataList["UFlag"])


        client = MongoClient('www.h-sen.com', 27017)

        db = client['TenderDb']
        tenderDatas = db["ZhaoBiao"]

        rtnList = []
        if UFlag == 0:
            queryResult = None
            # for oneFliter in Fliters:
            #     if not queryResult:
            #         queryResult = tenderDatas.find({'ProjectName': re.compile(oneFliter)})
            #     else:
            #         queryResult.queryResult.
            for item in tenderDatas.find({'ProjectName': re.compile(Fliters[0])}).sort('RecordTime', pymongo.DESCENDING). \
                    limit((PageIndex + 1) * PageSize). \
                    skip(PageIndex * PageSize):
                itemDict = {}
                itemDict["Classfic"] = item["Classfic"]
                itemDict["Unique"] = item["Unique"]
                itemDict["RecordTime"] = item["RecordTime"]
                itemDict["Title"] = item["Title"]
                itemDict["Url"] = item["Url"]
                itemDict["ProjectName"] = item["ProjectName"]
                itemDict["ProjectNo"] = item["ProjectNo"]
                itemDict["Time"] = item["Time"]
                itemDict["Way"] = item["Way"]
                rtnList.append(itemDict)

        client.close()
        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": rtnList})
        return HttpResponse(loginResut)

    @staticmethod
    def QueryReport(request):
        print "User Register"
        # url中的参数提取
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

        bookHandles = SpsUserOrder.objects.filter(account=postDataList["Account"], scode=servicecode)
        bookHandle = None
        if len(bookHandles) != 1:
            bookHandle = None
        else:
            bookHandle = bookHandles[0]

        rtnDict = []

        if bookHandle:
            resultDict = ReportServiceTender.buildKeyPieReport(bookHandle)
            rtnDict.append(resultDict)

        rtnDict.append(ReportServiceTender.buildHistoryLineReport(30))
        rtnDict.append(ReportServiceTender.buildWeekColumnReport(7))

        # rtnDict = AppServiceApi.buildTestReportData()
        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": rtnDict})
        return HttpResponse(loginResut)

    @staticmethod
    def buildTestReportData():
        rtnResult = []
        oneConfig = {}
        oneConfig["Title"] = "线图"
        oneConfig["YLabel"] = "发标量(个)"
        oneConfig["XLabel"] = "按日期"
        oneConfig["InnerLabel"] = "按关键字分布"
        oneConfig["Type"] = 0

        innerData = {}
        innerData["A"] = random.randint(10, 100)
        innerData["B"] = random.randint(10, 100)
        innerData["C"] = random.randint(10, 100)
        innerData["D"] = random.randint(10, 100)
        innerData["E"] = random.randint(10, 100)
        innerData["F"] = random.randint(10, 100)
        innerData["G"] = random.randint(10, 100)
        innerData["H"] = random.randint(10, 100)
        innerData["I"] = random.randint(10, 100)
        innerData["J"] = random.randint(10, 100)
        innerData["K"] = random.randint(10, 100)
        innerData["L"] = random.randint(10, 100)
        oneConfig["Values"] = innerData

        oneConfig2 = {}
        oneConfig2["Title"] = "饼图"
        oneConfig2["YLabel"] = "发标量(个)"
        oneConfig2["XLabel"] = "按日期"
        oneConfig2["InnerLabel"] = "按关键字分布"
        oneConfig2["Type"] = 1
        innerData2 = {}
        innerData2["A"] = random.randint(10, 100)
        innerData2["B"] = random.randint(10, 100)
        innerData2["C"] = random.randint(10, 100)
        innerData2["D"] = random.randint(10, 100)
        innerData2["E"] = random.randint(10, 100)
        innerData2["F"] = random.randint(10, 100)
        oneConfig2["Values"] = innerData2

        oneConfig3 = {}
        oneConfig3["Title"] = "柱状图"
        oneConfig3["YLabel"] = "发标量(个)"
        oneConfig3["XLabel"] = "按日期"
        oneConfig3["InnerLabel"] = "按关键字分布"
        oneConfig3["Type"] = 2
        innerData3 = {}
        innerData3["A"] = random.randint(10, 100)
        innerData3["B"] = random.randint(10, 100)
        innerData3["C"] = random.randint(10, 100)
        innerData3["D"] = random.randint(10, 100)
        innerData3["E"] = random.randint(10, 100)
        innerData3["F"] = random.randint(10, 100)
        oneConfig3["Values"] = innerData2

        rtnResult.append(oneConfig)
        rtnResult.append(oneConfig2)
        rtnResult.append(oneConfig3)

        return rtnResult
    # 支付后触发
    @staticmethod
    def PayForService(request):
        print "PayForService"
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

        # 获取业务代码
        serviceHandle= SpsService.objects.get(code=servicecode)

        if not serviceHandle:
            loginResut = json.dumps({"ErrorInfo": "您选择的业务已下线或在升级维护，请稍后重试", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)


        book = SpsUserOrder.objects.get(account=postDataList["Account"],scode=servicecode)

        if not book:
            book = SpsUserOrder()

        #  计算费用
        months = postDataList["Months"]
        book.startdate = datetime.datetime.now().strftime('%Y-%m-%d')
        book.stopdate = get_day_of_day(months*30).strftime('%Y-%m-%d')

        try:
            book.save()
        except:
            loginResut = json.dumps({"ErrorInfo": "续费失败，请联系管理员", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
        return HttpResponse(loginResut)

    # 支付后触发
    @staticmethod
    def GetUserInfo(request):
        print "GetUserInfo"
        # url中的参数提取
        command = request.GET.get('Command')
        timesnap = request.GET.get('TimeSnap')
        sig = request.GET.get('Sig')
        servicecode = request.GET.get('ServiceCode')
        account = request.GET.get('Account')

        # 检查请求合法性
        dict = {}
        dict["COMMAND"] = command
        dict["TIMESNAP"] = timesnap
        dict["SIG"] = sig
        dict["ServiceCode"] = servicecode
        dict["Account"] = account

        # 检查url合法性
        result = PublicService.validUrl(timesnap,sig)
        if not result:
            loginResut = json.dumps({"ErrorInfo": "非法URL", "ErrorId": 10001, "Result": None})
            return HttpResponse(loginResut)

        # 提取post数据
        postDataList = {}
        postDataList = getPostData(request)

        # 获取业务代码
        userHandle= SpsUser.objects.get(account=account)

        if not userHandle:
            loginResut = json.dumps({"ErrorInfo": "用户数据异常", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)

        rtnDict = AppServiceApi.buildUserInfo(userHandle)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": rtnDict})
        return HttpResponse(loginResut)

    # 支付后触发
    @staticmethod
    def SetUserInfo(request):
        print "PayForService"
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

        # 获取业务代码
        userHandle= SpsUser.objects.filter(account=postDataList["Account"])

        if len(userHandle) != 1:
            loginResut = json.dumps({"ErrorInfo": "用户数据异常", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)
        else:
            userHandle = userHandle[0]

        userHandle.email = postDataList["EMail"]
        userHandle.alias = postDataList["Alias"]
        userHandle.address = postDataList["Address"]
        userHandle.orgname = postDataList["OrgName"]
        try:
            userHandle.lantudite = postDataList["Lantudite"]
            userHandle.longdite = postDataList["Longdite"]
        except:
            pass

        try:
            userHandle.save()
        except Exception ,ex:
            pass

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": None})
        return HttpResponse(loginResut)

    @staticmethod
    def buildOrderInfo(info):
        if not info:
            return None

        rtnDict = {}
        rtnDict["Id"] = info.id
        rtnDict["Account"] = info.account
        rtnDict["StartDate"] = info.startdate
        rtnDict["StopDate"] = info.stopdate
        rtnDict["SCode"] = info.scode
        rtnDict["NotifyType"] = info.notifytype
        rtnDict["EMail"] = info.email
        rtnDict["Phone"] = info.phone
        rtnDict["Enable"] = info.enable
        rtnDict["Fliter1"] = info.fliter1
        rtnDict["Fliter2"] = info.fliter2
        rtnDict["Fliter3"] = info.fliter3

        return  rtnDict

    @staticmethod
    def buildUserInfo(info):
        if not info:
            return None

        rtnDict = {}
        rtnDict["Id"] = info.id
        rtnDict["Account"] = info.account
        rtnDict["EMail"] = info.email
        rtnDict["Alias"] = info.alias
        rtnDict["Address"] = info.address
        rtnDict["OrgName"] = info.orgname
        rtnDict["Lantudite"] = info.lantudite
        rtnDict["Longdite"] = info.longdite
        return  rtnDict

    @staticmethod
    def ApplySmscode(request):
        print "PayForService"
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

        users = SpsUser.objects.filter(account=postDataList["Phone"])
        if len(users) != 0:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码请联系客服", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)


        smsHandle = SmsDataBuffer.createSmsObj(postDataList["Phone"])

        for one in HsShareData.SmsListData:
            if one.phone == smsHandle.phone:
                HsShareData.SmsListData.remove(one)
                break
        HsShareData.SmsListData.append(smsHandle)

        smsHandle.sendMessage()

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": None})
        return HttpResponse(loginResut)