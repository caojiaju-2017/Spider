#!/usr/bin/env python
# -*- coding: utf-8 -*-

 # 描述服务启动时间
import ConfigParser
class TimeConfig(object):
    def __init__(self):
        self.Type = '0000000'  # 0000000 表示特定时间段， 等于0时，日期字段有效  1表示按星期循环
        self.TimeSeps = []
        self.DataInfo = None  #
        pass

    def CurrentTimeIsValid(self):
        return True

    @staticmethod
    def loadConfig(cfgFile):
        cf = ConfigParser.ConfigParser()
        cf.read(cfgFile)

        rtnTimes = []
        try:
            type = cf.get("time", "Type")
            timesepstring = cf.get("time", "TimeSep")
            timeseps =timesepstring.split('|')

            for oneTime in timeseps:
                oneTime = oneTime.lstrip()
                if len(oneTime) == 0:
                    continue
                times = oneTime.split('~')
                if len(times) != 2:
                    continue

                oneTime = TimeConfig()
                oneTime.Type = type
                oneTime.TimeSeps.append(TimeSep(times[0],times[1]))
                rtnTimes.append(oneTime)
        except:
            pass

        return  rtnTimes

class TimeSep(object):
    def __init__(self,startTime,stopTime):
        self.StartTime =  startTime
        self.StopTime = stopTime