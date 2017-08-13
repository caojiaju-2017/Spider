#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
from django.views.decorators.csrf import csrf_exempt

from PIL import *
from DSServer.Api.PublicService import *

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
        timesnap = request.GET.get('Time')
        sig = request.GET.get('Sig')

        # 检查请求合法性
        dict = {}
        dict["COMMAND"] = command
        dict["TIMESNAP"] = timesnap
        dict["SIG"] = sig

        # 检查url合法性
        result = PublicService.validUrl(request.GET)
        if not result:
            loginResut = json.dumps({"ErrorInfo": "非法URL", "ErrorId": 10001, "Result": None})
            return HttpResponse(loginResut)

        # 提取post数据
        postDataList = {}
        if request.method == 'POST':
            for key in request.POST:
                postDataList[key] = request.POST.getlist(key)[0]

        print "User login Success!"
        loginResut = json.dumps({"ErrorInfo": "操作成功", "ErrorId": 200, "Result": ""})
        return HttpResponse(loginResut)