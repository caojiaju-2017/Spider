#!/usr/local/bin/python
#-*- coding:utf-8 -*-

from kivy.clock import Clock

class KivyClock(object):
    def __init__(self , timeSep = 0):
        self.funcLink = None
        self.timeSep = timeSep
        pass

    def defaultTimeFunction(self,dt):
        print 'defaultTimeFunction'
        pass
    def registerCallBack(self,funcLink):
        self.funcLink = funcLink

    def startClock(self):
        if self.funcLink:
            Clock.schedule_interval(self.funcLink,self.timeSep)
        else:
            Clock.schedule_interval(self.defaultTimeFunction, self.timeSep)