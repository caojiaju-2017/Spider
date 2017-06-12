#!/usr/bin/env python
# -*- coding: utf-8 -*-

import pymongo

class DbConfig(object):
    def __init__(self):
        self.ip = "115.159.224.102"
        self.port = 27017