#!/usr/bin/env python
# -*- coding: utf-8 -*-


class ZbOrderDataStruct(object):
    def __init__(self):
        self.ReleaseDate = None
        self.Title = None
        self.PrjNum = None
        self.PrjName = None
        self.Way = None
        self.Province = None
        self.City = None
        pass

    @staticmethod
    def getDataByFliter(self,fliter):
        return  []