#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
import json
from django.views.decorators.csrf import csrf_exempt

from DSServer.Service.PublicService import *

from DSServer.Service.Service1BuildData import *
from HsShareData import *


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

        # 定义返回值
        abc = {}
        abc["Result"] = "login succhess!"

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

    # @staticmethod
    # def ajax_list(request):
    #     a = range(100)
    #     return HttpResponse(json.dumps(a), content_type='application/json')
    #
    # @staticmethod
    # def ajax_dict(request):
    #     name_dict = {'twz': 'Love python and Django', 'zqxt': 'I am teaching Django'}
    #     return HttpResponse(json.dumps(name_dict), content_type='application/json')
    #
    #
    # @staticmethod
    # def loadWish(request):
    #     return render(request, 'wishing.html')
    #
    #
    # @staticmethod
    # def viewProduct(request):
    #     return render(request, 'view_product.html')
