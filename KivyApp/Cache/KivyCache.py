#!/usr/local/bin/python
#-*- coding:utf-8 -*-
from kivy.cache import Cache

class KivyCache(object):
    def registerCache(self,name,lim,timeout):
        Cache.register(name ,limit=lim, timeout=timeout)

    def removeCache(self,name):
        Cache.remove(name)
        
    def getInstance(self,name,key):
        return Cache.get(name,key=key)

    def addToBuffer(self,name,key, obj):
        Cache.append(name,key,obj)

    def getLastAccess(self,name,key):
        return  Cache.get_lastaccess(name,key)