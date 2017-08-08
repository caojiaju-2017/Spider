#!/usr/local/bin/python
#-*- coding:utf-8 -*-

from kivy.uix.boxlayout import BoxLayout
from kivy.uix.floatlayout import FloatLayout
from kivy.properties import ObjectProperty
from kivy.network.urlrequest import *
import json

from TimeClock.KivyClock import *

class GuidePage(BoxLayout): #
    def __init__(self):
        super(GuidePage, self).__init__()
        self.timeClock = KivyClock(1)
        self.timeClock.registerCallBack(self.funcLink)
        self.timeClock.startClock()
        pass
    def _initialize(self):
        self.next = self.ids.btn_next.__self__  # same result if I don't use .__self__

    def funcLink(self,dt):
        Logger.info("Start app in build")