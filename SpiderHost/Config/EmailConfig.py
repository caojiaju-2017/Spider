#!/usr/bin/env python
# -*- coding: utf-8 -*-

import ConfigParser
# 描述客户邮件地址
# 描述客户短信手机号码
class EmailConfig(object):
    def __init__(self):
        self.Address = None
        self.Phone = None
        self.NoticePolicy = 0  # 0 邮件  1 短信
        pass
    def buildConfig(self,addr,phone):
        self.Address = addr
        self.Phone = phone

    @staticmethod
    def loadConfig(cfgFile):
        cf = ConfigParser.ConfigParser()
        cf.read(cfgFile)

        rtnCfg = EmailConfig()
        try:
            email = cf.get("notice", "email")
            phone = cf.get("notice", "phone")
            rtnCfg.Address = email
            rtnCfg.Phone = phone
        except Exception,ex:
            pass

        return  rtnCfg