#!/usr/local/bin/python
#-*- coding:utf-8 -*-


from kivy.app import App

from UiDefine.MainHome import *
from UiDefine.GuidePage import *
from UiDefine.HelpPage import *
from UiDefine.LoginPage import *

from UiDefine.CustomWidget.HomeLay import *
from UiDefine.CustomWidget.HomeNoSettingLay import *
from UiDefine.CustomWidget.OrderLay import *
from UiDefine.CustomWidget.ReportLay import *

from AppRoot import *



class MainApp(App):
    # MainApp.setAppBaseInfo()
    # Window.
    pass

    def build(self):
        # 设置应用基础信息
        MainApp.setAppBaseInfo()

        # 检查本地标志，如果从未登录，则执行引导页

        # 曾经登录，则执行登录页

        root = AppRoot()

        root.add_widget(GuidePage())

        return root

    def on_pause(self):
        return True

    @staticmethod
    def setAppBaseInfo():
        title = "aaaa"
        icon = "D:\\Work\\Product\\AIWCPaper\\WCRecgKivy\\Images\\png-0010.png"