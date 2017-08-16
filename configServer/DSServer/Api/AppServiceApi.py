#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
from django.views.decorators.csrf import csrf_exempt

from PIL import *
from DSServer.Api.PublicService import *

from DSServer.models import *

# python标准库导入
import json
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
        elif command  == "QUERY_REPORT":
            return AppServiceApi.QueryReport(req)
        elif command  == "PAY_FOR_SERVICE":
            return AppServiceApi.PayForService(req)
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

        user = SpsUser.objects.get(account=postDataList["Account"])

        if not user or user.password != postDataList["Password"]:
            loginResut = json.dumps({"ErrorInfo": "账户密码错误", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        # 获取用户业务绑定信息
        serviceBind = SpsUserService.objects.get(account=postDataList["Account"],scode=servicecode)
        overDate = False
        if not serviceBind:
            overDate = False

        # 获取业务代码
        serviceHandle= SpsService.objects.get(code=servicecode)


        if serviceBind.overdate >= time.strftime("%Y-%m-%d", time.localtime()):
            overDate = False

        rtnDictionary = {}
        rtnDictionary["OverTime"] = overDate

        if not serviceHandle:
            rtnDictionary["FeeInfo"] = None
        else:
            rtnDictionary["FeeInfo"] = serviceHandle.feerate

        # 返回订阅信息
        orderInfo = SpsUserOrder.objects.get(account=postDataList["Account"],scode=servicecode)
        rtnDictionary["OrderInfo"] = AppServiceApi.buildOrderInfo(orderInfo)
        
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

        user = SpsUser.objects.get(account=postDataList["Account"])

        if user:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        serviceHandle = SpsService.objects.get(code=servicecode)
        if not serviceHandle:
            loginResut = json.dumps({"ErrorInfo": "您选择的业务已下线或在升级维护，请稍后重试", "ErrorId": 10003, "Result": ""})
            return HttpResponse(loginResut)

        newUser = SpsUser()
        newUser.account = postDataList["Account"]
        newUser.password = postDataList["Password"]

        newUserSrv = SpsUserService()
        newUserSrv.account = newUser.account
        newUserSrv.scode = servicecode
        newUserSrv.overdate = time.strftime("%Y-%m-%d", get_day_of_day(30))


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

        rtnDict={}
        rtnDict["StartDate"] =orderConfig.startdate
        rtnDict["StopDate"] =orderConfig.stopdate
        rtnDict["Fliter1"] =orderConfig.fliter1
        rtnDict["Fliter2"] =orderConfig.fliter2
        rtnDict["Fliter3"] =orderConfig.fliter3
        rtnDict["SCode"] =orderConfig.scode
        rtnDict["NotifyType"] =orderConfig.notifytype
        rtnDict["Enable"] =orderConfig.enable
        rtnDict["Phone"] =orderConfig.phone
        rtnDict["Emal"] =orderConfig.email


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

        user = SpsUser.objects.get(account=postDataList["Account"])

        if user:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        newUser = SpsUser()
        newUser.account = postDataList["Account"]
        newUser.password = postDataList["Password"]

        try:
            newUser.save()
        except:
            loginResut = json.dumps({"ErrorInfo": "注册失败", "ErrorId": 10002, "Result": ""})
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

        user = SpsUser.objects.get(account=postDataList["Account"])

        if user:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        newUser = SpsUser()
        newUser.account = postDataList["Account"]
        newUser.password = postDataList["Password"]

        try:
            newUser.save()
        except:
            loginResut = json.dumps({"ErrorInfo": "注册失败", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
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

        user = SpsUser.objects.get(account=postDataList["Account"])

        if user:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        newUser = SpsUser()
        newUser.account = postDataList["Account"]
        newUser.password = postDataList["Password"]

        try:
            newUser.save()
        except:
            loginResut = json.dumps({"ErrorInfo": "注册失败", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
        return HttpResponse(loginResut)

    @staticmethod
    def PayForService(request):
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

        user = SpsUser.objects.get(account=postDataList["Account"])

        if user:
            loginResut = json.dumps({"ErrorInfo": "账户已注册，如果您忘记密码", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        newUser = SpsUser()
        newUser.account = postDataList["Account"]
        newUser.password = postDataList["Password"]

        try:
            newUser.save()
        except:
            loginResut = json.dumps({"ErrorInfo": "注册失败", "ErrorId": 10002, "Result": ""})
            return HttpResponse(loginResut)

        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
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
        rtnDict["Emal"] = info.email
        rtnDict["Phone"] = info.phone
        rtnDict["Enable"] = info.enable
        rtnDict["Fliter1"] = info.fliter1
        rtnDict["Fliter2"] = info.fliter2
        rtnDict["Fliter3"] = info.fliter3

        return  rtnDict
