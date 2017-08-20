#!/usr/bin/env python
# -*- coding: utf-8 -*-
import  time,datetime,subprocess,os

IPAddress = "115.159.224.102"
Port = 27017
DBNames=['ShowGuide','TenderDb','ZhuBaJie']
BackDir='/home/pi/mongoback'
THREADSLEEP=30
class backupMongoDb(object):

    @staticmethod
    def BackDb():
        print u'备份线程启动----------------------------'
        while 1:
            nowData = datetime.datetime.now()
            hours = nowData.hour
            minutes = nowData.minute

            if hours != 20 or minutes != 30 :
                print "============not arrived===============%d:%d"%(hours,minutes)
                time.sleep(THREADSLEEP)
                continue

            backName = time.strftime('%Y%m%d%H%M%S', time.localtime(time.time()))

            for oneDb in DBNames:
                finalBackDir = "%s/%s" % (BackDir,oneDb)
                finalBackDir = "%s%s" % (finalBackDir, backName)
                if not os.path.exists(finalBackDir):
                    os.mkdir(finalBackDir)
                cmdString = 'mongodump -h %s:%d -d %s -o %s'%(IPAddress,Port,oneDb,finalBackDir)
                os.system(cmdString)

            time.sleep(THREADSLEEP)

if __name__ == "__main__":
    backupMongoDb.BackDb()