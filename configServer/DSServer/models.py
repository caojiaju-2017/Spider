# This is an auto-generated Django model module.
# You'll have to do the following manually to clean this up:
#   * Rearrange models' order
#   * Make sure each model has one field with primary_key=True
#   * Make sure each ForeignKey has `on_delete` set to the desidered behavior.
#   * Remove `managed = False` lines if you wish to allow Django to create, modify, and delete the table
# Feel free to rename the models, but don't rename db_table values or field names.
from __future__ import unicode_literals

from django.db import models
#
#
# class AuthGroup(models.Model):
#     name = models.CharField(unique=True, max_length=80)
#
#     class Meta:
#         managed = False
#         db_table = 'auth_group'
#
#
# class AuthGroupPermissions(models.Model):
#     group = models.ForeignKey(AuthGroup, models.DO_NOTHING)
#     permission = models.ForeignKey('AuthPermission', models.DO_NOTHING)
#
#     class Meta:
#         managed = False
#         db_table = 'auth_group_permissions'
#         unique_together = (('group', 'permission'),)
#
#
# class AuthPermission(models.Model):
#     name = models.CharField(max_length=255)
#     content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING)
#     codename = models.CharField(max_length=100)
#
#     class Meta:
#         managed = False
#         db_table = 'auth_permission'
#         unique_together = (('content_type', 'codename'),)
#
#
# class AuthUser(models.Model):
#     password = models.CharField(max_length=128)
#     last_login = models.DateTimeField(blank=True, null=True)
#     is_superuser = models.IntegerField()
#     username = models.CharField(unique=True, max_length=30)
#     first_name = models.CharField(max_length=30)
#     last_name = models.CharField(max_length=30)
#     email = models.CharField(max_length=254)
#     is_staff = models.IntegerField()
#     is_active = models.IntegerField()
#     date_joined = models.DateTimeField()
#
#     class Meta:
#         managed = False
#         db_table = 'auth_user'
#
#
# class AuthUserGroups(models.Model):
#     user = models.ForeignKey(AuthUser, models.DO_NOTHING)
#     group = models.ForeignKey(AuthGroup, models.DO_NOTHING)
#
#     class Meta:
#         managed = False
#         db_table = 'auth_user_groups'
#         unique_together = (('user', 'group'),)
#
#
# class AuthUserUserPermissions(models.Model):
#     user = models.ForeignKey(AuthUser, models.DO_NOTHING)
#     permission = models.ForeignKey(AuthPermission, models.DO_NOTHING)
#
#     class Meta:
#         managed = False
#         db_table = 'auth_user_user_permissions'
#         unique_together = (('user', 'permission'),)
#
#
# class DjangoAdminLog(models.Model):
#     action_time = models.DateTimeField()
#     object_id = models.TextField(blank=True, null=True)
#     object_repr = models.CharField(max_length=200)
#     action_flag = models.SmallIntegerField()
#     change_message = models.TextField()
#     content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING, blank=True, null=True)
#     user = models.ForeignKey(AuthUser, models.DO_NOTHING)
#
#     class Meta:
#         managed = False
#         db_table = 'django_admin_log'
#
#
# class DjangoContentType(models.Model):
#     app_label = models.CharField(max_length=100)
#     model = models.CharField(max_length=100)
#
#     class Meta:
#         managed = False
#         db_table = 'django_content_type'
#         unique_together = (('app_label', 'model'),)
#
#
# class DjangoMigrations(models.Model):
#     app = models.CharField(max_length=255)
#     name = models.CharField(max_length=255)
#     applied = models.DateTimeField()
#
#     class Meta:
#         managed = False
#         db_table = 'django_migrations'
#
#
# class DjangoSession(models.Model):
#     session_key = models.CharField(primary_key=True, max_length=40)
#     session_data = models.TextField()
#     expire_date = models.DateTimeField()
#
#     class Meta:
#         managed = False
#         db_table = 'django_session'
#
#
class SpsUser(models.Model):
    id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
    account = models.CharField(db_column='Account', max_length=20, blank=True, null=True)  # Field name made lowercase.
    password = models.CharField(db_column='Password', max_length=64, blank=True, null=True)  # Field name made lowercase.
    email = models.CharField(db_column='EMail', max_length=64, blank=True, null=True)  # Field name made lowercase.
    alias = models.CharField(db_column='Alias', max_length=12, blank=True, null=True)  # Field name made lowercase.
    address = models.CharField(db_column='Address', max_length=128, blank=True, null=True)  # Field name made lowercase.
    orgname = models.CharField(db_column='OrgName', max_length=128, blank=True, null=True)  # Field name made lowercase.
    lantudite = models.FloatField(db_column='Lantudite', blank=True, null=True)
    longdite = models.FloatField(db_column='Longdite', blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'sps_user'


class SpsUserService(models.Model):
    id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
    account = models.CharField(db_column='Account', max_length=20)  # Field name made lowercase.
    scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
    overdate = models.CharField(db_column='OverDate', max_length=10)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'sps_user_service'


class SpsService(models.Model):
    id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
    code = models.CharField(db_column='Code', max_length=20)  # Field name made lowercase.
    name = models.CharField(db_column='Name', max_length=32)  # Field name made lowercase.
    info = models.CharField(db_column='Info', max_length=2000)  # Field name made lowercase.
    price = models.FloatField(db_column='Price', blank=True, null=True)
    feerate = models.CharField(db_column='FeeRate', max_length=20)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'sps_service'


class SpsUserOrder(models.Model):
    id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
    account = models.CharField(db_column='Account', max_length=20)  # Field name made lowercase.
    startdate = models.CharField(db_column='StartDate', max_length=10)  # Field name made lowercase.
    stopdate = models.CharField(db_column='StopDate', max_length=10)  # Field name made lowercase.
    scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
    notifytype = models.IntegerField(db_column='NotifyType', blank=True, null=True)
    email = models.CharField(db_column='EMail', max_length=64)  # Field name made lowercase.
    phone = models.CharField(db_column='Phone', max_length=20)  # Field name made lowercase.
    enable = models.IntegerField(db_column='Enable', blank=True, null=True)
    fliter1 = models.CharField(db_column='Fliter1', max_length=64)  # Field name made lowercase.
    fliter2 = models.CharField(db_column='Fliter2', max_length=64)  # Field name made lowercase.
    fliter3 = models.CharField(db_column='Fliter3', max_length=64)  # Field name made lowercase.


    class Meta:
        managed = False
        db_table = 'sps_user_order'



# class SpCustom(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     account = models.CharField(db_column='Account', max_length=20)  # Field name made lowercase.
#     password = models.CharField(db_column='Password', max_length=20)  # Field name made lowercase.
#     alias = models.CharField(db_column='Alias', max_length=200, blank=True, null=True)  # Field name made lowercase.
#     profile = models.CharField(db_column='ProFile', max_length=2000, blank=True, null=True)  # Field name made lowercase.
#     mobile = models.CharField(db_column='Mobile', max_length=20, blank=True, null=True)  # Field name made lowercase.
#     serviceflag = models.CharField(db_column='ServiceFlag', max_length=32)  # Field name made lowercase.
#     acode = models.CharField(db_column='ACode', max_length=20, blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_custom'
#
#
# class SpCustomService(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
#     ccode = models.CharField(db_column='CCode', max_length=20)  # Field name made lowercase.
#     terminaldate = models.CharField(db_column='TerminalDate', max_length=20)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_custom_service'
#
#
# class SpHostMonitor(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
#     cpu = models.FloatField(db_column='Cpu', blank=True, null=True)  # Field name made lowercase.
#     disk = models.FloatField(db_column='Disk', blank=True, null=True)  # Field name made lowercase.
#     memory = models.FloatField(db_column='Memory', blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_host_monitor'
#
#
# class SpMenu(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     url = models.CharField(db_column='Url', max_length=2000)  # Field name made lowercase.
#     name = models.CharField(db_column='Name', max_length=200)  # Field name made lowercase.
#     purlid = models.CharField(db_column='PUrlId', max_length=64, blank=True, null=True)  # Field name made lowercase.
#     urlid = models.CharField(db_column='UrlId', max_length=64)  # Field name made lowercase.
#     imagename = models.CharField(db_column='ImageName', max_length=200, blank=True, null=True)  # Field name made lowercase.
#     inx = models.IntegerField(db_column='Inx', blank=True, null=True)  # Field name made lowercase.
#     isvirtual = models.IntegerField(db_column='IsVirtual', blank=True, null=True)  # Field name made lowercase.
#     cmdstring = models.CharField(db_column='CmdString', max_length=20)  # Field name made lowercase.
#     preapi = models.CharField(db_column='PreApi', max_length=20)  # Field name made lowercase.
#     class Meta:
#         managed = False
#         db_table = 'sp_menu'
#
#
# class SpMenuAttribute(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     cmdstring = models.CharField(db_column='CmdString', max_length=20)  # Field name made lowercase.
#     kname = models.CharField(db_column='KName', max_length=40)  # Field name made lowercase.
#     kalias = models.CharField(db_column='KAlias', max_length=40, blank=True, null=True)  # Field name made lowercase.
#     kdatatype = models.IntegerField(db_column='KDataType', blank=True, null=True)  # Field name made lowercase.
#     selobjects = models.CharField(db_column='SelObjects', max_length=20, blank=True, null=True)  # Field name made lowercase.
#     isshow = models.IntegerField(db_column='IsShow', blank=True, null=True)  # Field name made lowercase.
#     inx = models.IntegerField(db_column='Inx', blank=True, null=True)  # Field name made lowercase.
#     type = models.IntegerField(db_column='Type', blank=True, null=True)  # Field name made lowercase.
#     class Meta:
#         managed = False
#         db_table = 'sp_menu_attribute'
#
# class SpMenuCustom(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     urlid = models.CharField(db_column='UrlId', max_length=20)  # Field name made lowercase.
#     ccode = models.CharField(db_column='CCode', max_length=20)  # Field name made lowercase.
#     manageflag = models.CharField(db_column='ManageFlag', max_length=16)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_menu_custom'
#
# class SpNosqlDb(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     code = models.CharField(db_column='Code', max_length=20)  # Field name made lowercase.
#     ipaddress = models.CharField(db_column='IpAddress', max_length=20)  # Field name made lowercase.
#     dbuser = models.CharField(db_column='DbUser', max_length=20)  # Field name made lowercase.
#     dbpassword = models.CharField(db_column='DbPassword', max_length=20, blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_nosql_db'
#
#
# class SpOrder(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
#     ccode = models.CharField(db_column='CCode', max_length=20)  # Field name made lowercase.
#     paytime = models.CharField(db_column='PayTime', max_length=20)  # Field name made lowercase.
#     price = models.FloatField(db_column='Price')  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_order'
#
#
# class SpRunlog(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     type = models.CharField(db_column='Type', max_length=32)  # Field name made lowercase.
#     account = models.CharField(db_column='Account', max_length=20)  # Field name made lowercase.
#     opertime = models.CharField(db_column='OperTime', max_length=20)  # Field name made lowercase.
#     info = models.CharField(db_column='Info', max_length=2000, blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_runlog'
#
#
# class SpServiceFee(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
#     feerate = models.FloatField(db_column='FeeRate')  # Field name made lowercase.
#     startdate = models.CharField(db_column='StartDate', max_length=200)  # Field name made lowercase.
#     stopdate = models.FloatField(db_column='StopDate')  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_service_fee'
#
#
# class SpServicePrd(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     code = models.CharField(db_column='Code', max_length=20)  # Field name made lowercase.
#     type = models.IntegerField(db_column='Type')  # Field name made lowercase.
#     name = models.CharField(db_column='Name', max_length=200)  # Field name made lowercase.
#     price = models.FloatField(db_column='Price')  # Field name made lowercase.
#     timeperiod = models.IntegerField(db_column='TimePeriod')  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_service_prd'
#
#
# class SpSpider(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     code = models.CharField(db_column='Code', max_length=20)  # Field name made lowercase.
#     name = models.CharField(db_column='Name', max_length=200, blank=True, null=True)  # Field name made lowercase.
#     version = models.CharField(db_column='Version', max_length=20, blank=True, null=True)  # Field name made lowercase.
#     state = models.IntegerField(db_column='State', blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_spider'
#
#
# class SpSpiderBinder(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     scode = models.CharField(db_column='SCode', max_length=20)  # Field name made lowercase.
#     ccode = models.CharField(db_column='CCode', max_length=20)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_spider_binder'
#
# # class SpProtocol(models.Model):
# #     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
# #     command = models.CharField(db_column='Command', max_length=20)  # Field name made lowercase.
# #     apilab = models.CharField(db_column='ApiLab', max_length=20)  # Field name made lowercase.
# #
# #     class Meta:
# #         managed = False
# #         db_table = 'sp_protocol'
#
# class SpSpiderHostInfo(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     scode = models.CharField(db_column='SCode', max_length=20, blank=True, null=True)  # Field name made lowercase.
#     host = models.CharField(db_column='Host', max_length=15, blank=True, null=True)  # Field name made lowercase.
#     cpu = models.CharField(db_column='Cpu', max_length=200, blank=True, null=True)  # Field name made lowercase.
#     disk = models.FloatField(db_column='Disk', blank=True, null=True)  # Field name made lowercase.
#     memory = models.FloatField(db_column='Memory', blank=True, null=True)  # Field name made lowercase.
#     opersys = models.CharField(db_column='OperSys', max_length=200)  # Field name made lowercase.
#     workpath = models.CharField(db_column='WorkPath', max_length=2000, blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_spider_host_info'
#
#
# class SpSuggest(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     ccode = models.CharField(db_column='CCode', max_length=20)  # Field name made lowercase.
#     state = models.IntegerField(db_column='State')  # Field name made lowercase.
#     reply = models.CharField(db_column='Reply', max_length=2000, blank=True, null=True)  # Field name made lowercase.
#     info = models.CharField(db_column='Info', max_length=2000, blank=True, null=True)  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_suggest'
#
#
# class SpVersion(models.Model):
#     id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
#     vtype = models.IntegerField(db_column='VType')  # Field name made lowercase.
#     opertype = models.CharField(db_column='OperType', max_length=200, blank=True, null=True)  # Field name made lowercase.
#     oldver = models.CharField(db_column='OldVer', max_length=40, blank=True, null=True)  # Field name made lowercase.
#     newver = models.CharField(db_column='NewVer', max_length=40)  # Field name made lowercase.
#     way = models.IntegerField(db_column='Way')  # Field name made lowercase.
#
#     class Meta:
#         managed = False
#         db_table = 'sp_version'
class SpVersion(models.Model):
    id = models.IntegerField(db_column='Id', primary_key=True)  # Field name made lowercase.
    vtype = models.IntegerField(db_column='VType')  # Field name made lowercase.
    opertype = models.CharField(db_column='OperType', max_length=200, blank=True, null=True)  # Field name made lowercase.
    oldver = models.CharField(db_column='OldVer', max_length=40, blank=True, null=True)  # Field name made lowercase.
    newver = models.CharField(db_column='NewVer', max_length=40)  # Field name made lowercase.
    way = models.IntegerField(db_column='Way')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'sp_version'
