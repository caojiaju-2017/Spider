//----------初始化变量-----------------
var server = null;
var appKey = null;
var userMark = null;
var serviceId = null;
var localUrl = null;
var request ={
	QueryString : function(uri,val){
		var re = new RegExp("" +val+ "=([^&?]*)", "ig");
		return ((uri.match(re))?(uri.match(re)[0].substr(val.length+1)):null);
	}
};
var ss = document.getElementsByTagName("script");
for(var i=0; i<ss.length; i++){
	var src = ss[i].src;
	var index = src.indexOf("visitor_tm_phone_source.js");
	if(index>0){
		serviceId=request.QueryString(src,"serviceId");
		userMark = request.QueryString(src,"userMark");
		appKey = request.QueryString(src,"appKey");
		server = src.substring(0,index-1);
		localUrl = window.location.href;
		break;
	}
	else {
		index = src.indexOf("visitor_tm_phone_source.js");
		if(index>0){
			serviceId=request.QueryString(src,"serviceId");
			userMark = request.QueryString(src,"userMark");
			appKey = request.QueryString(src,"appKey");
			server = src.substring(0,index-1);
			localUrl = window.location.href;
			break;
		}
	}
}
if(!appKey){
	appKey = "";
}
if(!userMark){
	userMark = "";
}
if(!serviceId){
	serviceId = 0;
}

//--------------------加载资源文件--------
(function(win,doc){
	//定义加载资源的函数
	var loadResource = {
		loadJs:function(jsurl,cb){
			var head = doc.getElementsByTagName("head")[0];
			var obj = doc.createElement('script');
			obj.setAttribute('src',jsurl);
			head.appendChild(obj);
			if(obj.onload && obj.onreadystatechange ){
				obj.onload=obj.onreadystatechange=function(){
					if(!this.readyState||this.readyState=='loaded'||this.readyState=='complete'){
						cb();
					}
					obj.onload=obj.onreadystatechange=null;
				};
			}else{
				setTimeout(function(){
					cb();
				}, 1000);
			}
		},
		loadCss:function(cssurl,cb){
			var head = doc.getElementsByTagName("head")[0];
			var obj = doc.createElement('link');
			obj.setAttribute('href',cssurl);
			head.appendChild(obj);
			obj.onload=obj.onreadystatechange=function(){
				if(!this.readyState||this.readyState=='loaded'||this.readyState=='complete'){
					if(cb){
						cb();
					}
				}
				obj.onload=obj.onreadystatechange=null;
			};
		}
	};
	alert(server)
	//加载资源文件
	var jquery = server+'/jquery-1.7.1.min.js';
	loadResource.loadJs(jquery,function(){
		var img = $("<img>",{
			id:"u5_img",
			class:"img",
			src:server+"/u5.png"
		});
		var div1 = $("<div>",{
			id:"u5",
			class:"ax_default image"
		});
		$(div1).append(img).appendTo("body");
		
		var div2 = $("<div>",{
			id:"u7",
			class:"ax_default box_1",
			onmouseover:"$('.hide1').show();",
			onmouseleave:"$('.hide1').hide();"
		});
		$(div2).appendTo("body");
		
		//这里模拟请求后得到的数据
		var data = [
			{'id':'u9','img' :'/img1_u9.png','url':'http://www.baidu.com'},
			{'id':'u11','img':'/img2_u11.png','url':'http://www.baidu.com'},
			{'id':'u13','img':'/img3_u13.png','url':'http://www.baidu.com'}
		]
		$.each(data, function(){
			var imgtemp = $("<img>",{
				id:this.id+"_img",
				class:"img",
				src:server+this.img
			});
			var divtemp = $("<div>",{
				id:this.id,
				class:"ax_default image hide1",
				style:"display: none;",
				onmouseover:"$('.hide1').show();",
				click:function(){
					if( !win.newwein || win.newwein.closed){
						win.newwein = win.open(this.url,"_blank",'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=yes,copyhistory=yes,width=520,height=530,left=600,top=10');
					}
				}
			});
			$(divtemp).append(imgtemp).appendTo("body");
		});
		
	});
	
	var css1 = server+'/styles.css';
	var head = document.getElementsByTagName('HEAD').item(0);
	var style = document.createElement('link');
	style.href = css1;
	style.rel = 'stylesheet';
	style.type = 'text/css';
	head.appendChild(style);
	
	
})(window,window.document);
