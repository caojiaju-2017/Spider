#!/usr/bin/env python
# -*- coding: utf-8 -*-


import_string = "from BaseDoJob import BaseDoJob"
exec import_string

class ServiceDoJob(BaseDoJob):
    def __init__(self):
        # BaseDoJob.__init__(self)
        return

    def doWork(self):
        print "service dowork"
        super(ServiceDoJob, self).doWork()
        return