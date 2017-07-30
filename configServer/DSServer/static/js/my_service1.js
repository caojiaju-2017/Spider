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

    $.loadServiceData();

    //

    $("#_thumalImage").mouseover(function (){
            // alert("aaaaaa");
            $("#_imageLarge").show();
        }).mouseout(function ()
    {
            $("#_imageLarge").hide();
    });
});



// custom function
$.extend({
    loadServiceData:function () {
        var loginUser = $.cookie('username');

        $.get("getServiceData", {username: loginUser,
                pageindex: 1,
                pagesize:20,
                servicecode:"SS_149886674647457",
                fliterstr:"efgh"
            },
            function (data)
            {
                var receiveObj = eval("("+data+")");
                var Images = receiveObj.Images;
                var Datas = receiveObj.Datas;

                for (i=0;i<Datas.length ;i++ )
                {
                    var oneCode = Datas[i];
                    $.addOneItem(oneCode);
                }

                // 添加
                for (index =0 ; index < Images.length; index ++)
                {
                    var oneCode = Images[index];
                    $.addOneImage(oneCode);
                }
                $.unblockUI();
            },
            "text");//这里返回的类型有：json,html,xml,text
    },

    addOneItem: function (oneCode) {
      var oneTemp = "\
                <div style=\"height: 90px;width: 100%;border-bottom:1px solid #d4d4e4\">\n" +
          "\n" +
          "                    <div style=\"width: 40%;height: 100%;float: left\">\n" +
          "                        <label id=\"_showTitle\" style='text-align: left;display: inline-block;width: 100%;height:30px;margin-left: 10px;font-size: 14px;color: #3b4246'><b>{Name}</b></label>\n" +
          "                        <label id=\"_showType\" style='text-align: left;display: inline-block;width: 100%;height:30px;margin-left: 40px;color: #686868'>{Classfic}</label>\n" +
          "                        <label id=\"_nation\" style='text-align: left;display: inline-block;width: 100%;height:30px;margin-left: 40px;color: #686868'>{Nation}</label>\n" +
          "                    </div>\n" +
          "\n" +
          "                    <div style=\"width: 40%;height: 100%;float: left\">\n" +
          "                        <label id=\"_showDetail\" style='text-align: left;display: inline-block;width: 100%;height:30px;color: cornflowerblue;font-size: 13px;line-height: 25px'>{Info}</label>\n" +
          "                    </div>\n" +
          "\n" +
          "                    <div style=\"width: 20%;height: 100%;float: left \">\n" +
          "                        <button style='text-align: center;display: inline-block;width: 60px;height:30px;margin-top: 15px;border-radius: 5px'>查看</button>\n" +
          "                        <label id=\"_showTime\" style='text-align: center;display: inline-block;width: 100%;height:40px ;margin-top: 10px;font-size: 12px;color: #808995'>{Time}</label>\n" +
          "                    </div>\n" +
          "                </div>";

        var oneT = $("#_detailInfoView").html();

        if("undefined" == typeof oneT)
        {
            oneT = "";
        }

        var abcTemp = {
            Name:oneCode.Name,
            Classfic:oneCode.Classfic,
            Nation:oneCode.Nation,
            Info:oneCode.Info,
            Time:oneCode.Time
        }
        $("#_detailInfoView").html(oneT + $.format(oneTemp,abcTemp) );
    },
addOneImage: function (name) {
      var oneTemp = "<img id='_thumalImage' src=\"" + name + "\" style=\"width: 90%;height: 300px;margin-top: 20px\">";

        var oneT = $("#_leftdiv").html();

        if("undefined" == typeof oneT)
        {
            oneT = "";
        }

        $("#_leftdiv").html(oneT + oneTemp);
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