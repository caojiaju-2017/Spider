#!/usr/local/bin/python
#-*- coding:utf-8 -*-

from kivy.uix.boxlayout import BoxLayout
from kivy.uix.floatlayout import FloatLayout
from kivy.properties import ObjectProperty
from kivy.network.urlrequest import *
import json

class GuidePage(BoxLayout): #
    def _initialize(self):
        self.next = self.ids.btn_next.__self__  # same result if I don't use .__self__
    pass