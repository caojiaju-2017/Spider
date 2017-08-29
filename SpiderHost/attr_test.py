#!/usr/bin/env python
# -*- coding: utf-8 -*-
import urllib2,urllib
from bs4 import  BeautifulSoup
import time,random
from multiprocessing import Pool

ip = []

def f(x):
    time.sleep(10)
    return x*x

def testAttribute(absoluteUrl,htmlTag):
    user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
    send_headers = {
        'Host': 'www.jb51.net',
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.2; rv:16.0) Gecko/20100101 Firefox/16.0',
        'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
        'Connection': 'keep-alive',
        'X-Forwarded-For': ip[random.randint(0, 9998)]
    }

    req = urllib2.Request(absoluteUrl, data=None, headers=send_headers, origin_req_host=None, unverifiable=True)

    try:
        html_doc = urllib2.urlopen(req, timeout=60).read()
        # html_doc = content.read()

        # if isinstance(html_doc, unicode):
        #     pass
        # else:
        #     html_doc = html_doc.decode('utf-8')

        # print html_doc
    except Exception, ex:
        # response = urllib2.urlopen(req)
        return  None


    soup = BeautifulSoup(html_doc, 'lxml')

    datas = soup.select(htmlTag)

    # print datas[0]

    print datas
    # get_text
    return str(datas)

def snapPng(url):

    pass
if __name__ == "__main__":

    for index in range(10000):
        ip.append("%d.%d.%d.%d" % (
        random.randint(2, 254), random.randint(2, 254), random.randint(2, 254), random.randint(2, 254)))

    # attr = 'div.yahoo > div > span:nth-of-type(2)'
    attr = '#info > ul > li:nth-of-type(2)'
    url = 'http://www.ccgp-guizhou.gov.cn/view-1153418052184995-21939068717121920.html'
    testAttribute(url, attr)

    # testAttribute("","")  HTTP Error 403: Forbidden