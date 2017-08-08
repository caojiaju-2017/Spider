#!/usr/local/bin/python
#-*- coding:utf-8 -*-

import os
from kivy.app import App

from UiDefine.MainHome import *
from UiDefine.GuidePage import *
from UiDefine.HelpPage import *
from UiDefine.LoginPage import *

from UiDefine.CustomWidget.HomeLay import *
from UiDefine.CustomWidget.HomeNoSettingLay import *
from UiDefine.CustomWidget.OrderLay import *
from UiDefine.CustomWidget.ReportLay import *

from kivy.logger import Logger
from AppRoot import *


class MainApp(App):
    # MainApp.setAppBaseInfo()
    # Window.
    def build(self):


        self.icon = "Images\Icon\default.png"
        self.kv_directory = os.path.join(os.getcwd(),"kvDirectory")
        self.title = u"汉森镖局"

        # 设置窗体尺寸
        self.use_kivy_settings = False
        # 设置应用基础信息


        # 检查本地标志，如果从未登录，则执行引导页

        # 曾经登录，则执行登录页

        root = AppRoot()

        root.add_widget(GuidePage())

        return root

    def on_pause(self):
        return True
    def on_resume(self):
        return True

    def on_stop(self):
        return True
