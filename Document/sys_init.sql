CREATE TABLE IF NOT EXISTS sp_spider  (
   Id  int NOT NULL COMMENT 'ID',
   Code  varchar(20) NOT NULL COMMENT '代码',
   Name  varchar(200) COMMENT '名称',
   Version  varchar(20) COMMENT '版本',
   State  int COMMENT '状态',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_spider_binder  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) NOT NULL COMMENT '爬虫',
   CCode  varchar(20) NOT NULL COMMENT '客户',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_spider_host_info  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) COMMENT '爬虫',
   Host  varchar(15) COMMENT 'IP地址',
   Cpu  varchar(200) COMMENT 'CPU信息',
   Disk  Float COMMENT '磁盘大小',
   Memory  Float COMMENT '内存大小',
   OperSys  varchar(200) NOT NULL COMMENT '操作系统',
   WorkPath  varchar(2000) COMMENT '工作目录',
   
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_host_monitor  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) NOT NULL COMMENT '爬虫',
   Cpu  Float  COMMENT 'CPU占用率',
   Disk  Float COMMENT '磁盘',
   Memory  Float COMMENT '内存使用百分比',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_agency  (
   Id  int NOT NULL COMMENT 'ID',
   Account  varchar(20) NOT NULL COMMENT '代理账户',
   Password  varchar(20) NOT NULL COMMENT '密码',
   Alias  varchar(200)  COMMENT '姓名',
   ProFile  varchar(2000) COMMENT '协议文件位置',
   Mobile  varchar(20) COMMENT '移动号码',
  PRIMARY KEY ( Id )
) ;
CREATE TABLE IF NOT EXISTS sp_custom  (
   Id  int NOT NULL COMMENT 'ID',
   Account  varchar(20) NOT NULL COMMENT '代理账户',
   Password  varchar(20) NOT NULL COMMENT '密码',
   Alias  varchar(200)  COMMENT '姓名',
   ProFile  varchar(2000) COMMENT '协议文件位置',
   Mobile  varchar(20) COMMENT '移动号码',
   ServiceFlag  varchar(32) NOT NULL COMMENT '业务开通标识',
   ACode  varchar(20) COMMENT '代理商代码',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_agency_custom  (
   Id  int NOT NULL COMMENT 'ID',
   ACode  varchar(20) NOT NULL COMMENT '代理',
   CCode  varchar(20) NOT NULL COMMENT '客户',
  PRIMARY KEY ( Id )
) ;
CREATE TABLE IF NOT EXISTS sp_control  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) NOT NULL COMMENT '爬虫',
   State  int NOT NULL COMMENT '状态',
   ReleaseTime  varchar(20) NOT NULL COMMENT '指令发布时间',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_service_prd  (
   Id  int NOT NULL COMMENT 'ID',
   Code  varchar(20) NOT NULL COMMENT '爬虫',
   Type  int NOT NULL COMMENT '状态',
   Name  varchar(200) NOT NULL COMMENT '名称',
   Price  Float NOT NULL COMMENT '价格',
   TimePeriod  int NOT NULL COMMENT '使用时长',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_service_fee  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) NOT NULL COMMENT '爬虫',
   FeeRate  Float NOT NULL COMMENT '折扣',
   StartDate  varchar(200) NOT NULL COMMENT '开始日期',
   StopDate  Float NOT NULL COMMENT '结束日期',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_custom_service  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) NOT NULL COMMENT '产品或业务代码',
   CCode  varchar(20) NOT NULL COMMENT '爬虫',
   TerminalDate  varchar(20) NOT NULL COMMENT '服务结束日期',
  PRIMARY KEY ( Id )
) ;
CREATE TABLE IF NOT EXISTS sp_order  (
   Id  int NOT NULL COMMENT 'ID',
   SCode  varchar(20) NOT NULL COMMENT '产品或业务代码',
   CCode  varchar(20) NOT NULL COMMENT '爬虫',
   PayTime  varchar(20) NOT NULL COMMENT '支付时间',
   Price  Float NOT NULL COMMENT '价格',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_runlog  (
   Id  int NOT NULL COMMENT 'ID',
   Type  varchar(32) NOT NULL COMMENT '操作类别',
   Account  varchar(20) NOT NULL COMMENT '客户代码',
   OperTime  varchar(20) NOT NULL COMMENT '操作时间',
   Info  varchar(2000) COMMENT '日志详情',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_suggest  (
   Id  int NOT NULL COMMENT 'ID',
   CCode  varchar(20) NOT NULL COMMENT '客户代码',
   State  int NOT NULL COMMENT '客户代码',
   Reply  varchar(2000)  COMMENT '回复内容',
   Info  varchar(2000) COMMENT '日志详情',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_menu  (
   Id  int NOT NULL COMMENT 'ID',
   CCode  varchar(20) NOT NULL COMMENT '客户代码',
   Url  varchar(2000) NOT NULL COMMENT 'Url地址',
   Name  varchar(200) NOT NULL COMMENT '菜单名',
   PUrlId  varchar(64)  COMMENT '父亲UrlId',
   UrlId  varchar(64) NOT NULL COMMENT 'Url代码',
   ImageName  varchar(200)  COMMENT '菜单图片名',
   Inx  int  COMMENT '索引',
   IsVirtual  int  COMMENT '是否为虚拟节点',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_menu_attribute  (
   Id  int NOT NULL COMMENT 'ID',
   UrlId  varchar(64) NOT NULL COMMENT '对应的RUL',
   KName  varchar(40) NOT NULL COMMENT '属性名',
   KAlias  varchar(40)  COMMENT '属性别名',
   KDataType  int  COMMENT '数据类型',
   SelObjects  varchar(20)  COMMENT '对象选择范围',
   IsShow  int  COMMENT '是否在列表显示',
  PRIMARY KEY ( Id )
) ;
CREATE TABLE IF NOT EXISTS sp_menu_custom  (
   Id  int NOT NULL COMMENT 'ID',
   MCode  varchar(20) NOT NULL COMMENT '菜单代码',
   CCode  varchar(20) NOT NULL COMMENT '客户代码',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_version  (
   Id  int NOT NULL COMMENT 'ID',
   VType  Int NOT NULL COMMENT '版本类别',
   OperType  varchar(200) COMMENT '适应操作系统',
   OldVer  varchar(40)  COMMENT '老版本',
   NewVer  varchar(40)  NOT NULL COMMENT '新版本',
   Way  Int  NOT NULL COMMENT '升级方法',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_config  (
   Id  int NOT NULL COMMENT 'ID',
   CCode  varchar(20) NOT NULL COMMENT '客户CODE',
   Url  varchar(400) NOT NULL COMMENT 'URL连接',
   SpiderFile  varchar(200) NOT NULL  COMMENT '爬虫资源文件',
   FileVer  varchar(20) NOT NULL COMMENT '爬虫文件版本',
   StartTime  varchar(20) NOT NULL COMMENT '工作开始时间',
   StopTime  varchar(20) NOT NULL COMMENT '工作结束时间',
   MongoCode  varchar(20) COMMENT '存储代码',
  PRIMARY KEY ( Id )
) ;

CREATE TABLE IF NOT EXISTS sp_nosql_db  (
   Id  int NOT NULL COMMENT 'ID',
   Code  varchar(20) NOT NULL COMMENT '客户CODE',
   IpAddress  varchar(20) NOT NULL COMMENT 'Ip地址',
   DbUser  varchar(20) NOT NULL  COMMENT '用户名',
   DbPassword  varchar(20) COMMENT '用户密码',
  PRIMARY KEY ( Id )
) ;


CREATE TABLE IF NOT EXISTS sp_protocol  (
   Id  int NOT NULL COMMENT 'ID',
   Command  varchar(20) NOT NULL COMMENT '命令码',
   ApiLab  varchar(20) NOT NULL COMMENT 'Api标签',
  PRIMARY KEY ( Id )
) ;
