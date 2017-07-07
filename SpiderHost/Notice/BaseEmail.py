#!/usr/bin/env python
# -*- coding: utf-8 -*-

import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import *
from email.header import Header
# from email.MIMEMultipart import MIMEMultipart
# from email.MIMEText import MIMEText

SEND_ADDRESS = "gj00001@qq.com"
MAIL_PASSWORD="caojj_123"
SMTP_SERVER="smtp.qq.com"
SMTP_PORT = 465

class BaseEmail(object):
    def __init__(self):
        pass

    @staticmethod
    def sendMail(cfg,bodyDatas,title):
        to_addr = cfg.EMail

        subject = '竞标提示：人脸识别'
        # msg = MIMEText('你好', 'text', 'utf-8')  # 中文需参数‘utf-8’，单字节字符不需要
        # msg['Subject'] = Header(subject, 'utf-8')

        msg = MIMEMultipart()
        msg['From'] = SEND_ADDRESS
        msg['To'] = to_addr
        # msg['Cc'] = ccaddr
        msg['Subject'] = "竞标提示===>>" + title

        body = "竞标提示：请关注以下新增招标"
        for one in bodyDatas:
            body = "%s \n    %s"%(body, one)

        body = body.encode("utf-8")
        msg.attach(MIMEText(body, 'plain'))

        smtp = smtplib.SMTP()
        smtp.connect(SMTP_SERVER)
        smtp.ehlo()
        smtp.starttls()
        smtp.ehlo()
        smtp.set_debuglevel(0)
        smtp.login(SEND_ADDRESS, MAIL_PASSWORD)
        smtp.sendmail(SEND_ADDRESS, to_addr, msg.as_string())
        smtp.quit()

        # @staticmethod
    # def _format_addr(s):
    #     name, addr = parseaddr(s)
    #     return formataddr(( \
    #         Header(name, 'utf-8').encode(), \
    #         addr.encode('utf-8') if isinstance(addr, unicode) else addr))