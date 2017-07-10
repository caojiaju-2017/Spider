#!/usr/bin/env python
# -*- coding: utf-8 -*-


import urllib2
from bs4 import  BeautifulSoup

def testAttribute(absoluteUrl,htmlTag):
    try:
        page = urllib2.urlopen(absoluteUrl, timeout=30)
        html_doc = page.read()
    except Exception, ex:
        return  None

    soup = BeautifulSoup(html_doc, 'lxml')

    datas = soup.select(htmlTag)

    return str(datas)