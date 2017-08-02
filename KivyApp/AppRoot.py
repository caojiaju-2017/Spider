#!/usr/local/bin/python
#-*- coding:utf-8 -*-

from kivy.uix.boxlayout import BoxLayout

from UiDefine.HelpPage import *
from UiDefine.GuidePage import *
from UiDefine.LoginPage import *
from UiDefine.MainHome import *

from kivy.uix.label import Label

class AppRoot(BoxLayout): #

    def show_other_wnd(self):
        print 'hello new window'

        self.clear_widgets()
        self.add_widget(MainHome())
    pass