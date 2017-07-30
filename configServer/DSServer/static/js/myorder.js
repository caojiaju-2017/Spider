/**
 * Created by caojiaju on 17-7-22.
 */
var leftDivWidth = 400;
var divSep = 5;
window.onload=function()
{
    $.setPosition();
};

$(window).resize(function(){
    $.setPosition();

});


$(document).ready(function() {
    // set cookie
    $.setPosition();
$.blockUI();
    $.loadData();
});



// custom function
$.extend({
    loadData:function () {
        var loginUser = $.cookie('username');
        $.post("getMyOrderData", {username: loginUser, pageindex: 1,pagesize:20},
            function (data)
            {
                var receiveObj = eval("("+data+")");

                //Count
                var count = receiveObj.Count;
                var pageMax = receiveObj.PageMax;
                var pageIndex = receiveObj.PageIndex;

                var dataArray = receiveObj.Datas

                for (i=0;i<dataArray.length ;i++ )
                {
                    var oneCode = dataArray[i];

                    var title = oneCode.Title;
                    var state = oneCode.State;
                    var time = oneCode.Time;
                    var receiveCode = oneCode.ReceiveCode;

                    $.addOneItem(oneCode);
                    //alert(title);
                }

                $.unblockUI();
            },
            "text");//这里返回的类型有：json,html,xml,text
    },

    addOneItem: function (oneCode) {
      var oneTemp = "\
        <div style=\"width: 100%;height: 80px;border-bottom:1px solid #d4d4e4;\">\
            <div class=\"itemTitle\">\
            <LABEL class='titleLabel'>{Title}</LABEL>\
            </div>\
            <div class=\"itemInfo1\">\
            <div class='itemInfo1Attr1'>\
            <label style='text-align: left;display: inline-block;width: 40%'>通知状态</label>\
            <label style='text-align: left;display: inline-block;width: 50%;color: chocolate'>{Notice}</label>\
            </div>\
            <div class='itemInfo1Attr2'>\
            <label style='text-align: left;display: inline-block;width: 40%'>接受号码</label>\
            <label style='text-align: left;display: inline-block;width: 50%;color: chocolate'>{receiveCode}</label>\
            </div>\
            </div>\
            <div class=\"itemInfo2\">\
            <div class='itemInfo2Attr1'>\
            <label style='text-align: left;width: 30%'>项目预算</label>\
            <label style='text-align: left;width: 70%;color: chocolate'>{Price}</label>\
            </div>\
            </div>\
            <div class=\"detailViewButton\">\
            <button class=\"viewButton\" onmouseover=\"this.style.backgroundColor='#98AFC7';this.style.color='white'\" onmouseout=\"this.style.backgroundColor='';this.style.color='#666666'\">搜索</button>\
            </div>\
        </div>";

        var oneT = $("#_detailInfoView").html();

        if("undefined" == typeof oneT)
        {
            oneT = "";
        }

        var sState = "未通知";
        if (oneCode.State == 1)
        {
            sState = "已通知";
        }
        var abcTemp = {Title:oneCode.Title,Notice:sState,receiveCode:oneCode.ReceiveCode,Price:oneCode.Price}
        $("#_detailInfoView").html(oneT + $.format(oneTemp,abcTemp) );
    },
    sleep: function (numberMillis) {
        var now = new Date();
        var exitTime = now.getTime() + numberMillis;
        while (true) {
            now = new Date();
            if (now.getTime() > exitTime)
                return;
        }
    },
    setPosition: function () {

        // 设置最外层div
        $("#_myorder").height($(window).height() - 20);
        $("#_myorder").css("left",($(window).width() - $("#_myorder").width()) / 2);

        var mainLeft = $("#_myorder").offset().left;
        var mainTop = $("#_myorder").offset().top
        var mainWidth = $("#_myorder").width()
        var mainHeight = $("#_myorder").height()


        // 设置左边div
        $("#_leftdiv").css("left",mainLeft);
        $("#_leftdiv").css("top",$("#_myorder").top);
        $("#_leftdiv").width(leftDivWidth);
        $("#_leftdiv").height(mainHeight);

        // alert(mainLeft);
        // 设置右边Div
        $("#_rightdiv").css("left",mainLeft + divSep + leftDivWidth);
        $("#_rightdiv").css("top",mainTop);
        $("#_rightdiv").width(mainWidth - divSep - leftDivWidth);
        $("#_rightdiv").height(mainHeight);

        // 设置右边 detailInfoView   multiPageControl
        var rightDivLeft = $("#_rightdiv").offset().left;
        var rightDivTop = $("#_rightdiv").offset().top;
        var rightDivWidth = $("#_rightdiv").width();
        var rightDivHeight = $("#_rightdiv").height();

        var sep = 2;
        var pctrl = 50;

        var bodyHd = rightDivHeight - rightDivTop - 105 - pctrl - sep;

        $("#_detailInfoView").css("left",rightDivLeft + sep);
        $("#_detailInfoView").css("top",rightDivTop + 100);
        $("#_detailInfoView").width(rightDivWidth - sep*2 );
        $("#_detailInfoView").height(bodyHd -sep*3);

        $("#_multiPageControl").css("left",rightDivLeft + sep);
        $("#_multiPageControl").css("top",rightDivTop + rightDivHeight - pctrl );
        $("#_multiPageControl").width(rightDivWidth - sep*2);
        $("#_multiPageControl").height(pctrl);
    },

    format : function(source,args){
					var result = source;
					if(typeof(args) == "object"){
						if(args.length==undefined){
							for (var key in args) {
								if(args[key]!=undefined){
									var reg = new RegExp("({" + key + "})", "g");
									result = result.replace(reg, args[key]);
								}
							}
						}else{
							for (var i = 0; i < args.length; i++) {
								if (args[i] != undefined) {
									var reg = new RegExp("({[" + i + "]})", "g");
									result = result.replace(reg, args[i]);
								}
							}
						}
					}
					return result;
				},
});