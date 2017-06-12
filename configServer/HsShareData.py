#!/usr/bin/env python
# -*- coding: utf-8 -*-

import threading,datetime,md5,os

class HsShareData:
    dataLock = threading.Lock()
    Version = 'Act_V1.0'
    KeyCode = {}

    # 活动发布最新记录
    Activity_Release = {}

    # 保活记录
    HeartBeats = {}

    # 短信验证码
    SMS_CODE = {}

    SigCode = 'af?Fzio2u'

    AttchRoot = "D:\NewStart\Display\Code\Server\Attach"

    @staticmethod
    def getDocumentDir():
        return  os.getcwd()

class SmsInfo:
    SMS_SEP = 60
    def __init__(self):
        self.Code = None
        self.Time = None

    def checkTimeOut(self):
        starttime = datetime.datetime.strptime(self.Time, "%Y-%m-%d %H:%M:%S")
        endtime = datetime.datetime.now()
        sep = (endtime - starttime).seconds

        if sep >= SmsInfo.SMS_SEP:
            return  True
        return False