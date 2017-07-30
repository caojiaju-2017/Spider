#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
import json
from django.views.decorators.csrf import csrf_exempt

from DSServer.Service.PublicService import *

from DSServer.Service.Service1BuildData import *
from HsShareData import *

from ServiceData import *
from BuildImage import *

from DSServer.models import *

class WebCenterApi(object):
    @staticmethod
    @csrf_exempt
    def goHome(request):
        return render(request, 'home.html')

    @staticmethod
    @csrf_exempt
    def openLogin(request):
        print "openLogin form"
        return render(request, 'login.html')

    @staticmethod
    @csrf_exempt
    def serviceQuery(request):
        print "servicequery"
        datas=Service1BuildData.buildData()
        return render(request,"searchService.html")

    @staticmethod
    @csrf_exempt
    def excuteLogin(request):
        print 'i have receive login command'

        # 提取post数据
        postDataList = {}
        if request.method == 'POST':
            for key in request.POST:
                postDataList[key] = request.POST.getlist(key)[0]

        # 查询 账户
        users = SpsUser.objects.filter(account=postDataList['username'])
        # 验证账户
        count = len(users)

        if count != 1:
            abc = {}
            abc["Result"] = "账户数据异常!"
            abc["ErrorCode"] = 10001
            return HttpResponse(json.dumps(abc))

        # 查询账户开通的服务
        user = users[0]
        if user.password != postDataList['password']:
            abc = {}
            abc["Result"] = "账户或密码不正确!"
            abc["ErrorCode"] = 10002
            return HttpResponse(json.dumps(abc))

        # 查询用户关联服务
        binds = SpsUserService.objects.filter(account=user.account)


        # 定义返回值
        abc = {}
        abc["Result"] = "login succhess!"
        abc["ErrorCode"] = 0

        srvCodes = []
        for oneBind in binds:
            #  判断时间范围
            srvCodes.append(oneBind.scode)

        abc['ServiceCodes'] = srvCodes

        return HttpResponse(json.dumps(abc))

    @staticmethod
    @csrf_exempt
    def searchData(request):
        # 获取当前访问IP
        if request.META.has_key('HTTP_X_FORWARDED_FOR'):
            ip = request.META['HTTP_X_FORWARDED_FOR']
        else:
            ip = request.META['REMOTE_ADDR']

        print "Access Ip ===>" ,ip

        # 提取post参数
        postDatas = PublicService.getPostData(request)

        print "Recieve Post Datas===>" ,postDatas

        userName = postDatas['username']
        # 如果是体验账户，则当前IP 每天仅能使用3次
        rtnDicts = {}
        if userName == "guest04":
            if not HsShareData.GuestAccessDict.has_key(ip):
                HsShareData.GuestAccessDict[ip] = 1
            else:
                currentCount = HsShareData.GuestAccessDict[ip]
                if currentCount >= HsShareData.GuestMaxAccessCount:
                    rtnDicts["status"] = 1001
                    rtnDicts["errorinfo"] = "已超过游客每天体验次数"
                    return HttpResponse(json.dumps(rtnDicts))
                else:
                    HsShareData.GuestAccessDict[ip] = currentCount + 1

        rtnDicts["status"] = 0
        rtnDicts["errorinfo"] = "成功"
        rtnDicts["ImageCount"] = 4


        dicts = {}
        dicts[""] = 0
        dicts["语文"] = random.randint(30, 100)
        dicts["数学"] = random.randint(30, 100)
        dicts["英语"] = random.randint(30, 100)
        dicts["地理"] = random.randint(30, 100)
        dicts["历史"] = random.randint(30, 100)
        dicts["生物"] = random.randint(30, 100)
        dicts["英语"] = random.randint(30, 100)
        rtnDicts["name1"] = HsShareData.DrawBar(dicts,"report1.png")

        dicts = {}
        dicts["A"] = random.randint(30, 100)
        dicts["B"] = random.randint(30, 100)
        dicts["C"] = random.randint(30, 100)
        rtnDicts["name2"] = HsShareData.DrawBar(dicts,"report2.png")

        dicts = {}
        dicts["A"] = random.randint(30, 100)
        dicts["B"] = random.randint(30, 100)
        dicts["C"] = random.randint(30, 100)
        rtnDicts["name3"] = HsShareData.DrawBar(dicts,"report3.png")

        dicts = {}
        dicts["A"] = random.randint(30, 100)
        dicts["B"] = random.randint(30, 100)
        dicts["C"] = random.randint(30, 100)
        rtnDicts["name4"] = HsShareData.DrawBar(dicts,"report4.png")


        rtnDicts["ItemCount"] = 10
        oneItem = {}
        oneItem["Title"] = ["标题" , "跟婆婆闹矛盾，怎么办？"]
        oneItem["Info"] =["描述","... ... 内容保密 ... ..."]
        oneItem["Url"] = ["地址","http://www.h-sen.com"]
        oneItem["Date"] = ["日期","2017-09-01"]
        rtnDicts["item1"] = oneItem

        oneItem = {}
        oneItem["Title"] = ["标题" , "你想用百度所搜嘛？请跟我来。"]
        oneItem["Info"] =["描述","... ... 内容保密 ... ..."]
        oneItem["Url"] = ["地址","http://www.baidu.com"]
        oneItem["Date"] = ["日期","2016-12-23"]
        rtnDicts["item2"] = oneItem

        oneItem = {}
        oneItem["Title"] = ["标题" , "跟婆婆闹矛盾，怎么办？"]
        oneItem["Info"] =["描述","... ... 内容保密 ... ..."]
        oneItem["Url"] = ["地址","http://www.by-creat.com"]
        oneItem["Date"] = ["日期","2017-02-21"]
        rtnDicts["item3"] = oneItem

        oneItem = {}
        oneItem["Title"] = ["标题" , "快来查字典吧？"]
        oneItem["Info"] =["描述","... ... 内容保密 ... ..."]
        oneItem["Url"] = ["地址","http://www.youdao.com"]
        oneItem["Date"] = ["日期","2016-11-11"]
        rtnDicts["item4"] = oneItem
        return HttpResponse(json.dumps(rtnDicts))

    @staticmethod
    def openImageView(request):
        from django.template import Context, Template
        fp = open(os.path.join(TEMPLATE_DIRS[0],'ImageView.html'))
        t = Template(fp.read())
        fp.close()

        imgName = request.GET.get('ImageName')

        c = Context({"imageName": imgName})
        return HttpResponse(t.render(c))
        # return HttpResponse(template.render(context))

        # return render(request, 'ImageView.html')


    @staticmethod
    @csrf_exempt
    def openMyOrder(request):
        print "openMyOrder"
        datas=Service1BuildData.buildData()
        return render(request,"my_order.html")

    @staticmethod
    @csrf_exempt
    def getMyOrderData(request):
        print "getMyOrderData"
        abc = WebCenterApi.queryData()

        return HttpResponse(json.dumps(abc))

    @staticmethod
    def getTestData():

        abc = {}
        abc["Count"] = 10
        abc["PageMax"] = 9
        abc["PageIndex"] = 1

        datas = []

        for index in range(10):
            oneData = {}
            oneData["Title"] = "XXX人脸识别开发%d" % index
            oneData["State"] = 1
            oneData["Price"] = "45000.0"
            oneData["ReceiveCode"] = "xxxx@qq.com"
            datas.append(oneData)

        abc["Datas"] = datas
        return abc

    @staticmethod
    def queryData():
        from pymongo import MongoClient
        client = MongoClient('www.h-sen.com', 27017)

        db = client['ZhuBaJie']
        AdFliter = db["AdFliter"]
        FaceRecognize = db["FaceRecognize"]
        PyDev = db["PyDev"]

        abc = {}
        datas = []
        for item in db.FaceRecognize.find():
            oneData = {}
            title = item["Title"]

            if len(title) > 10:
                title = title[0:7] + "..."
            oneData["Title"] = title
            oneData["State"] = 1
            oneData["Price"] = item["Price"]
            oneData["ReceiveCode"] = "h-sen@qq.com"
            datas.append(oneData)

            if len(datas) >= 20:
                break

        for item in db.AdFliter.find():
            oneData = {}

            if len(datas) >= 20:
                break
            title = item["Title"]
            if len(title) > 10:
                title = title[0:7] + "..."

            oneData["Title"] = title
            oneData["State"] = 1
            oneData["Price"] = item["Price"]
            oneData["ReceiveCode"] = "h-sen@qq.com"
            datas.append(oneData)

        abc["Datas"] = datas
        abc["Count"] = len(datas)
        abc["PageMax"] = 10
        abc["PageIndex"] = 1

        return  abc

    @staticmethod
    @csrf_exempt
    def openIntro(request):
        print "openIntro"
        return render(request, 'intro.html')

    @staticmethod
    @csrf_exempt
    def openReportIntro(request):
        print "openIntro"
        return render(request, 'reportIntro.html')

    @staticmethod
    @csrf_exempt
    def serviceDataQuery(request):
        print "serviceDataQuery"
        return render(request, 'my_service1.html')

    @staticmethod
    def getServiceDataData(request):
        from QueryStruct import *
        qStruct = QueryStruct()
        qStruct.userName = request.GET.get('username')
        qStruct.pageIndex = int(request.GET.get('pageindex'))
        qStruct.pageSize = int(request.GET.get('pagesize'))
        qStruct.srvCode = request.GET.get('servicecode')
        qStruct.fliterStr = request.GET.get('fliterstr')
        # print "getServiceDataData2"
        abs = {}
        imageFiles = []
        results = {}
        if qStruct.srvCode == "SS_149886674647457":
            results = ServiceData.getService1Data(qStruct)
            abs["Datas"] = results[2]
        elif qStruct.srvCode == "SS_149886674647454":
            abs["Datas"] = ServiceData.getService2Data(qStruct)
        elif qStruct.srvCode == "SS_149886674647455":
            abs["Datas"] = ServiceData.getService3Data(qStruct)
        # print "getServiceDataData3"
        imgShortUrl = BuildImage.buildImage(results,"Classfic",ImageType.Bar)
        imageFiles.append(imgShortUrl)
        abs["Images"] = imageFiles

        # print "getServiceDataData4"
        return HttpResponse(json.dumps(abs))
