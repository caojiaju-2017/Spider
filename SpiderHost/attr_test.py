#!/usr/bin/env python
# -*- coding: utf-8 -*-
import urllib2,urllib
from bs4 import  BeautifulSoup


def testAttribute(absoluteUrl,htmlTag):
    try:
        user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
        headers = {'User-Agent':'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.137 Safari/537.36 LBBROWSER'  }
        req = urllib2.Request(absoluteUrl,data=None,headers=headers,origin_req_host=None,unverifiable=False)
        response = urllib2.urlopen(req)
        html_doc = response.read()
    except Exception, ex:
        return  None


    soup = BeautifulSoup(html_doc, 'lxml')

    datas = soup.select(htmlTag)

    print datas[0].get_text()
    # get_text
    return str(datas)

def snapPng(url):

    pass
if __name__ == "__main__":
    # snapPng(
    #     'http://www.sczfcg.com/CmsNewsController.do?method=recommendBulletinList&rp=25&page=1&moreType=provincebuyBulletinMore&channelCode=cggg',
    #     'div.colsList > ul > li > a')
    # print testAttribute('http://www.qianlima.com/zbgg/', 'div.sevenday_list > dl > dt > a')
    print testAttribute('http://202.61.88.152:9002/view/staticpags/xjcggg/2017-08-03/731bca7b6169457d870226b1393e4454.html', 'table > tr:nth-of-type(2) > td.bordertt.confont')

    # testAttribute("","")  HTTP Error 403: Forbidden