#!/usr/bin/env python
# -*- coding: utf-8 -*-

class Attr2FieldMap(object):
    def __init__(self,attr,fid):
        self.Attribute = attr
        self.Field = fid
        pass

    @staticmethod
    def setValue(objectHandle, value, attrName):
        # exec_string = "if isinstance(objectHandle.%s,int):\n" \
        #               "    objectHandle.%s = %s\n" \
        #               "elif isinstance(objectHandle.%s,str):\n" \
        #               "    objectHandle.%s = '%s'" %(attrName,attrName,value,attrName,attrName,value)
        # #print exec_string
        # exec(exec_string)


        result1 = False
        assert_string = "result1 = isinstance(value,str)"
        exec(assert_string)

        result2 = False
        assert_string = "result2 = isinstance(value,unicode)"
        exec(assert_string)

        if result1:
            # print value.decode('gb2312')
            exec_string = "setattr(objectHandle,attrName,value)"
        elif result2:
            exec_string = "setattr(objectHandle,attrName,value.decode('utf-8'))"
        else:
            exec_string = "setattr(objectHandle,attrName,value)"


        exec (exec_string)


        pass
