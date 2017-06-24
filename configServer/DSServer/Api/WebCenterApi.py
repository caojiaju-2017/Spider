#!/usr/bin/env python
# -*- coding: utf-8 -*-

from django.shortcuts import render,render_to_response
from django.http import  HttpResponse
import json
from django.views.decorators.csrf import csrf_exempt


from DSServer.Service.Service1BuildData import *

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
        abc = {}
        abc["Result"] = "login success!"
        return HttpResponse(json.dumps(abc))

    @staticmethod
    @csrf_exempt
    def searchData(request):
        print "searchData"
        # a = request.GET['a']
        # b = request.GET['b']
        # a = int(a)
        # b = int(b)
        # return HttpResponse(str(a + b))
        import json
        abc={}
        abc["count"] = 4;
        abc["name1"] = "https://images2015.cnblogs.com/news/24442/201706/24442-20170613154413368-1655528381.jpg";
        abc["name2"] = "https://images2015.cnblogs.com/news/24442/201706/24442-20170613154413368-1655528381.jpg";
        abc["name3"] = "http://unionad.vanclimg.com/union/ad_images/200x200_20130826_164651.jpg";
        abc["name4"] = "https://images2015.cnblogs.com/news/24442/201706/24442-20170613154413368-1655528381.jpg";
        return HttpResponse(json.dumps(abc))
    #
    # @staticmethod
    # def product_list(request):
    #     return render(request, 'product_list.html')
    #
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
