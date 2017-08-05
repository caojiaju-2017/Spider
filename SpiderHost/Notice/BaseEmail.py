#!/usr/bin/env python
# -*- coding: utf-8 -*-

import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import *
from email.header import Header
# from email.MIMEMultipart import MIMEMultipart
# from email.MIMEText import MIMEText
import mimetypes ,os.path

import smtplib
import email.MIMEMultipart# import MIMEMultipart
import email.MIMEText# import MIMEText
import email.MIMEBase# import MIMEBase
import os.path
import mimetypes


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

    @staticmethod
    def sendMailHasAttach(maddr,bodyDatas,title,file_name):
        to_addr = maddr

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



        ## 读入文件内容并格式化 [方式1]－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
        data = open(file_name, 'rb')
        ctype, encoding = mimetypes.guess_type(file_name)
        if ctype is None or encoding is not None:
            ctype = 'application/octet-stream'
        maintype, subtype = ctype.split('/', 1)
        file_msg = email.MIMEBase.MIMEBase(maintype, subtype)
        file_msg.set_payload(data.read())
        data.close()
        email.Encoders.encode_base64(file_msg)  # 把附件编码
        ''''' 
         测试识别文件类型：mimetypes.guess_type(file_name) 
         rar 文件             ctype,encoding值：None None（ini文件、csv文件、apk文件） 
         txt text/plain None 
         py  text/x-python None 
         gif image/gif None 
         png image/x-png None 
         jpg image/pjpeg None 
         pdf application/pdf None 
         doc application/msword None 
         zip application/x-zip-compressed None 

        encoding值在什么情况下不是None呢？以后有结果补充。 
        '''
        # －－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－

        ## 设置附件头
        basename = os.path.basename(file_name)
        file_msg.add_header('Content-Disposition', 'attachment', filename=basename)  # 修改邮件头
        msg.attach(file_msg)


        smtp = smtplib.SMTP()
        smtp.connect(SMTP_SERVER)
        smtp.ehlo()
        smtp.starttls()
        smtp.ehlo()
        smtp.set_debuglevel(0)
        smtp.login(SEND_ADDRESS, MAIL_PASSWORD)
        smtp.sendmail(SEND_ADDRESS, to_addr, msg.as_string())
        smtp.quit()

