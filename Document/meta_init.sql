insert into sp_custom(account,password,alias,profile,mobile,serviceflag,acode) values ('root',md5('123456'),'管理员','root.hs','15680585185','000000000000000000000000000000000000',null);

-----------------------------------------------------管理菜单
--- 权限管理菜单
delete from sp_menu where 1=1;

insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'主页',null ,'292ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',0,1,null);

insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'产品与服务',null ,'492ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',1,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','产品管理','492ca7a9-27af-11e7-a71e-bcee7b2bea55','5382dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=ADD_MENU','添加产品','5382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afaf522-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=MODI_MENU','修改产品','5382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afcc459-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=DELE_MENU','停用产品','5382dab6-2d4f-11e7-bd08-bcee7b2bea55','5aff5f78-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_MENU');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','服务管理','492ca7a9-27af-11e7-a71e-bcee7b2bea55','6384e102-2d4f-11e7-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=ADD_PROTOCOL','添加服务','6384e102-2d4f-11e7-bd08-bcee7b2bea55','5b070924-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=MODI_PROTOCOL','修改服务','6384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0a0b80-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=DELE_PROTOCOL','停用服务','6384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0d6880-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_PROTOCOL');



insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'核心管理',null ,'392ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',2,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','爬虫管理','392ca7a9-27af-11e7-a71e-bcee7b2bea55','3382dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=ADD_MENU','添加爬虫','3382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afaf522-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=MODI_MENU','修改爬虫','3382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afcc459-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=DELE_MENU','删除爬虫','3382dab6-2d4f-11e7-bd08-bcee7b2bea55','5aff5f78-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_MENU');
	  insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=DELE_MENU','爬虫关联','3382dab6-2d4f-11e7-bd08-bcee7b2bea55','5aff5f78-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_MENU');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','清洗组件管理','392ca7a9-27af-11e7-a71e-bcee7b2bea55','4384e102-2d4f-11e7-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=ADD_PROTOCOL','添加清洗组件','4384e102-2d4f-11e7-bd08-bcee7b2bea55','5b070924-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=MODI_PROTOCOL','修改清洗组件','4384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0a0b80-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=DELE_PROTOCOL','删除清洗组件','4384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0d6880-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_PROTOCOL');
		


insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'客户管理',null ,'592ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',3,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','终端客户','592ca7a9-27af-11e7-a71e-bcee7b2bea55','7382dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=ADD_MENU','添加终端客户','7382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afaf522-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=MODI_MENU','修改终端客户信息','7382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afcc459-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=DELE_MENU','删除终端客户','7382dab6-2d4f-11e7-bd08-bcee7b2bea55','5aff5f78-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_MENU');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','代理商','592ca7a9-27af-11e7-a71e-bcee7b2bea55','8384e102-2d4f-11e7-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=ADD_PROTOCOL','添加代理商','8384e102-2d4f-11e7-bd08-bcee7b2bea55','5b070924-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=MODI_PROTOCOL','修改代理商','8384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0a0b80-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=DELE_PROTOCOL','删除代理商','8384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0d6880-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_PROTOCOL');
		


insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'订单管理',null ,'192ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',4,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','订单查询','192ca7a9-27af-11e7-a71e-bcee7b2bea55','c082dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','订单报表','192ca7a9-27af-11e7-a71e-bcee7b2bea55','c084e102-2d4f-11e7-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
	
		

insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'权限管理',null ,'a92ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',5,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','菜单管理','a92ca7a9-27af-11e7-a71e-bcee7b2bea55','c382dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=ADD_MENU','添加菜单','c382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afaf522-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=MODI_MENU','修改菜单','c382dab6-2d4f-11e7-bd08-bcee7b2bea55','5afcc459-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=DELE_MENU','删除菜单','c382dab6-2d4f-11e7-bd08-bcee7b2bea55','5aff5f78-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_MENU');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','协议管理','a92ca7a9-27af-11e7-a71e-bcee7b2bea55','c384e102-2d4f-11e7-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=ADD_PROTOCOL','添加协议','c384e102-2d4f-11e7-bd08-bcee7b2bea55','5b070924-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=MODI_PROTOCOL','修改协议','c384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0a0b80-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_PROTOCOL');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=DELE_PROTOCOL','删除协议','c384e102-2d4f-11e7-bd08-bcee7b2bea55','5b0d6880-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_PROTOCOL');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PRIVS','授权管理','a92ca7a9-27af-11e7-a71e-bcee7b2bea55','c3867fd4-2d4f-11e7-bd08-bcee7b2bea55','menu4.png',3,0,'LIST_PRIVS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=GRANT_PRIV','授权菜单','c3867fd4-2d4f-11e7-bd08-bcee7b2bea55','5b13d137-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'GRANT_PRIV');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=RECLAIM_PRIV','解除授权','c3867fd4-2d4f-11e7-bd08-bcee7b2bea55','5b171632-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'RECLAIM_PRIV');


insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'日志管理',null ,'092ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',6,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','日志查询','092ca7a9-27af-11e7-a71e-bcee7b2bea55','9382dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','日志归档','092ca7a9-27af-11e7-a71e-bcee7b2bea55','9384e102-2d4f-11e7-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PRIVS','留言管理','092ca7a9-27af-11e7-a71e-bcee7b2bea55','93867fd4-2d4f-11e7-bd08-bcee7b2bea55','menu4.png',3,0,'LIST_PRIVS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=GRANT_PRIV','我要留言','c3867fd4-2d4f-11e7-bd08-bcee7b2bea55','1b13d137-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'GRANT_PRIV');
	
		
insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv',null,'系统设置',null ,'692ca7a9-27af-11e7-a71e-bcee7b2bea55','menu1.png',7,1,null);
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=FETCH_MENUS','存储设置','692ca7a9-27af-11e7-a71e-bcee7b2bea55','9302dab6-2d4f-11e7-bd08-bcee7b2bea55','menu2.png',1,0,'FETCH_MENUS');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=ADD_MENU','添加存储','9302dab6-2d4f-11e7-bd08-bcee7b2bea55','0afaf522-2d51-11e7-bd08-bcee7b2bea55','menu2.png',1,2,'ADD_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=MODI_MENU','修改存储','9302dab6-2d4f-11e7-bd08-bcee7b2bea55','0afcc459-2d51-11e7-bd08-bcee7b2bea55','menu2.png',2,2,'MODI_MENU');
		insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/priv/?Command=DELE_MENU','删除存储','9302dab6-2d4f-11e7-bd08-bcee7b2bea55','0aff5f78-2d51-11e7-bd08-bcee7b2bea55','menu2.png',3,2,'DELE_MENU');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','告警设置','692ca7a9-27af-11e7-a71e-bcee7b2bea55','a384e102-2d4f-11e7-bd08-b0ee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
		
	insert into sp_menu(preapi, url,name,purlid,urlid,imagename,inx,IsVirtual,cmdstring) values('priv','/api/user/?Command=LIST_PROTOCOLS','心跳设置','692ca7a9-27af-11e7-a71e-bcee7b2bea55','a384e102-2d4f-1117-bd08-bcee7b2bea55','menu3.png',2,0,'LIST_PROTOCOLS');
				
				


delete from sp_menu_attribute where cmdstring = 'ADD_MENU';
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (1,'ADD_MENU',1,'Url'      ,'Url地址',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (2,'ADD_MENU',1,'Name'     ,'菜单名',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (3,'ADD_MENU',1,'PUrlId'   ,'父亲UrlId',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (4,'ADD_MENU',1,'UrlId'    ,'Url代码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (5,'ADD_MENU',1,'ImageName','菜单图片',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (6,'ADD_MENU',1,'Inx'      ,'显示索引',0,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (7,'ADD_MENU',1,'IsVirtual ','否为虚拟节点',0,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (8,'ADD_MENU',1,'CmdString','命令码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (9,'ADD_MENU',1,'PreApi',  '协议前缀',1,null,0);

delete from sp_menu_attribute where cmdstring = 'FETCH_MENUS';
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (1,'FETCH_MENUS',1,'Url','Url地址',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (2,'FETCH_MENUS',1,'Name','菜单名',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (3,'FETCH_MENUS',1,'PUrlId','父亲UrlId',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (4,'FETCH_MENUS',1,'UrlId','Url代码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (5,'FETCH_MENUS',1,'ImageName','菜单图片',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (6,'FETCH_MENUS',1,'Inx','显示索引',0,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (7,'FETCH_MENUS',1,'IsVirtual','否为虚拟节点',0,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (8,'FETCH_MENUS',1,'CmdString','命令码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (9,'FETCH_MENUS',1,'PreApi',  '协议前缀',1,null,0);

delete from sp_menu_attribute where cmdstring = 'MODI_MENU';
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (1,'MODI_MENU',1,'Url'      ,'Url地址',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (2,'MODI_MENU',1,'Name'     ,'菜单名',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (3,'MODI_MENU',1,'PUrlId'   ,'父亲UrlId',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (4,'MODI_MENU',1,'UrlId'    ,'Url代码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (5,'MODI_MENU',1,'ImageName','菜单图片',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (6,'MODI_MENU',1,'Inx'      ,'显示索引',0,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (7,'MODI_MENU',1,'IsVirtual ','否为虚拟节点',0,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (8,'MODI_MENU',1,'CmdString','命令码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (9,'MODI_MENU',1,'PreApi',  '协议前缀',1,null,0);

delete from sp_menu_attribute where cmdstring = 'DELE_MENU';
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (1,'DELE_MENU',1,'UrlId'    ,'Url代码',1,null,1);
insert into sp_menu_attribute(Inx,cmdstring,type,kname,kalias,kdatatype,selObjects,isshow)values (2,'DELE_MENU',1,'PreApi'   ,'协议前缀',1,null,0);
