#!/usr/bin/env python
# -*- coding: utf-8 -*-

# 公共处理函数
from publicFunction import *
import shutil

# 公共配置缓存
from HsShareData import *


class PublicService(object):
    #添加用户业务
    @staticmethod
    def getPostData(request):
        postDataList = {}
        if request.method == 'POST':
            for key in request.POST:
                postDataList[key] = request.POST.getlist(key)[0]

        import json
        if not postDataList or len(postDataList) == 0:
            try:
                bodyTxt = request.body
                postDataList = json.loads(bodyTxt)
            except Exception, ex:
                pass

        return postDataList