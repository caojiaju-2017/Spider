/**
 * Created by jiaju_cao on 2017/6/7.
 */

var userName=null;
var loginToken="fsdefasfs#f!@fsae3";
var selectIndex = null;

var serviceCodes ;

window.onload=function()
{
    $.setPosition();
};

$(window).resize(function(){
    $.setPosition();
        // alert("aaa");

});


$(document).ready(function(){
    // 设置cookie
    $.checkCookie();

    $("#ewmImageLarge").css("left",$("#ewmImage").offset().left + 15);

    // 窗体生成时注册事件＝＝＝＝＝＝＝＝同图片切换相关
    $("#findTrantionImage").click(function() {

        if("undefined" == typeof serviceCodes)
        {
            alert("请登录后操作");
            return ;
        }

        // change
        var isHasCode = $.checkService("SS_149886674647457");
        if (!isHasCode)
        {
            alert("当前账户未开通此业务");
            return;
        }
        var currentState = $("#findTrantionRightImg").is(":hidden");

        if (!currentState)
        {
            $.unCheckAllService();
        }
        else
        {
            $("#findTrantionRightImg").left = $("#findTrantionImage").left + $("#findTrantionImage").width;
            $("#findTrantionRightImg").top = $("#findTrantionImage").top;

             $("#findTrantionRightImg").show();
        }


        selectIndex = "SS_149886674647457";
    })

    $("#queryProductPriceImage").click(function() {
        if("undefined" == typeof serviceCodes)
        {
            alert("请登录后操作");
            return ;
        }


        var isHasCode = $.checkService("SS_149886674647454");
        if (!isHasCode)
        {
            alert("当前账户未开通此业务");
            return;
        }
        var currentState = $("#queryProductPriceRightImg").is(":hidden");

        if (!currentState)
        {
            $.unCheckAllService();
        }
        else
        {
            $("#queryProductPriceRightImg").left = $("#queryProductPriceImage").left + $("#queryProductPriceImage").width;
            $("#queryProductPriceRightImg").top = $("#queryProductPriceImage").top;

             $("#queryProductPriceRightImg").show();
        }


        selectIndex = "SS_149886674647454";
    })

    $("#serviceThreeImage").click(function() {
        if("undefined" == typeof serviceCodes)
        {
            alert("请登录后操作");
            return ;
        }

        var isHasCode = $.checkService("SS_149886674647455");
        if (!isHasCode)
        {
            alert("当前账户未开通此业务");
            return;
        }
        var currentState = $("#serviceThreeRightImg").is(":hidden");

        if (!currentState)
        {
            $.unCheckAllService();
        }
        else
        {
            $("#serviceThreeRightImg").left = $("#serviceThreeImage").left + $("#serviceThreeImage").width;
            $("#serviceThreeRightImg").top = $("#serviceThreeImage").top;

             $("#serviceThreeRightImg").show();
        }


        selectIndex = "SS_149886674647455";
    })

    $("#serviceFourImage").click(function() {
        if("undefined" == typeof serviceCodes)
        {
            alert("请登录后操作");
            return ;
        }

        // change
        var isHasCode = $.checkService("SS_149886674647457");
        if (!isHasCode)
        {
            alert("当前账户未开通此业务");
            return;
        }

        $.unCheckAllService();

        // 跳转页面
        self.location='my_order.html';

    })

    $("#serviceFiveImage").click(function() {
        // 跳转页面
        self.location='intro.html';
    })

    $("#serviceSixImage").click(function() {
        self.location='reportIntro.html';
    })

    // 搜索按钮
    $("#searchButton").click(function() {

        // 检查用户是否登陆
        var username = $.cookie("username");
        if (username == "" || username == undefined || username == null)
        {
            alert("请先登录，体验账号：guest04   密码：000000");
            return ;
        }
        // alert( username);


        var searchContextValue = $("#searchContext").val();
        // 未输入，则不执行查询
        if(searchContextValue == null || searchContextValue == "" || searchContextValue=="请输入搜索内容" || userName == "undefined") 
        {
            alert("请输入搜索内容");
            return ;
        } 

        if (selectIndex == -1 )
        {
            alert("必须指定搜索类型，在搜索框下面，通过鼠标点击选取");
            return ;
        }

        // 设置当前搜索类型和过滤字符串
        $.cookie("servicecode",selectIndex);
        $.cookie("pageindex",0);

        var urlAddress = "searchService.html";

        // alert(urlAddress);
        location.href =urlAddress;

        // location.href = "http://www.h-sen.com";
        //向服务器推送指令
        // $.get("./searchData", {'userName':userName, 'loginToken':loginToken,'ServiceIndex':selectIndex},function(data)
        // {
        //     alert(data);
        // });

    })

    // 登陆按钮
    $("#loginLab").click(function() {
        $.showDialog();
        // alert(result);
    })
    // 登陆按钮
    $("#exitLab").click(function() {
        // 退出登陆
        $.deleteCookie();
    })

    $("#ewmImage").mouseover(function (){
            // alert("aaaaaa");
            $("#ewmImageLarge").show();
        }).mouseout(function (){
            // $(".content").hide();

        // $("#ewmImageLarge").offset().left = $("#ewmImage").offset().left;

        $("#ewmImageLarge").css("left",$("#ewmImage").offset().left + 15);


        // $("#ewmImageLarge").left = $("#ewmImage").left;
        $("#ewmImageLarge").hide();
        });
});

    // 自定义函数
    $.extend({
        clearSelection: function () {
            $("#findTrantionImage").attr("src", "/static/Srv/icon1.png");
            $("#queryProductPriceImage").attr("src", "/static/Srv/icon2.png");
            $("#serviceThreeImage").attr("src", "/static/Srv/icon3.png");

            $("#serviceFourImage").attr("src", "/static/Srv/icon4.png");
            $("#serviceFiveImage").attr("src", "/static/Srv/icon5.png");
            $("#serviceSixImage").attr("src", "/static/Srv/icon6.png");
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
            var coulumnCount = 3;
            var rowCount = 2;

            //一个服务的尺寸
            var actualSize = 260;

            // 获取div宽度 高度
            var parentWidth = $("#service_div_id").width();
            var parentHeight = $("#service_div_id").height();


            //计算margin－left  margin－top
            var margin_left = (parentWidth - coulumnCount * actualSize) / (coulumnCount + 1);
            var margin_top = (parentHeight / rowCount - actualSize) / 2;
            // alert(margin_left);
            //处理第一行
            var rowIndex = 1;

            var columnIndex = 1;
            $("#findTration").width(actualSize);
            $("#findTration").height(actualSize);
            $("#findTration").css("marginLeft", margin_left);
            $("#findTration").css("marginTop", margin_top);

            columnIndex = 2;
            $("#queryProductPrice").width(actualSize);
            $("#queryProductPrice").height(actualSize);
            $("#queryProductPrice").css("marginLeft", margin_left);
            $("#queryProductPrice").css("marginTop", margin_top);

            columnIndex = 3;
            $("#serviceThree").width(actualSize);
            $("#serviceThree").height(actualSize);
            $("#serviceThree").css("marginLeft", margin_left);
            $("#serviceThree").css("marginTop", margin_top);


//    第二排
            rowIndex = 2;
            columnIndex = 1;
            $("#serviceFour").width(actualSize);
            $("#serviceFour").height(actualSize);
            $("#serviceFour").css("marginLeft", margin_left);
            $("#serviceFour").css("marginTop", margin_top);

            columnIndex = 2;
            $("#serviceFive").width(actualSize);
            $("#serviceFive").height(actualSize);
            $("#serviceFive").css("marginLeft", margin_left);
            $("#serviceFive").css("marginTop", margin_top);

            columnIndex = 3;
            $("#serviceSix").width(actualSize);
            $("#serviceSix").height(actualSize);
            $("#serviceSix").css("marginLeft", margin_left);
            $("#serviceSix").css("marginTop", margin_top);
        },

        showDialog: function () {

           if(   document.all   ) //IE
           {
               feature="dialogWidth:300px;dialogHeight:200px;status:no;help:no";
               window.showModalDialog("login.html",null,feature);
           }
           else
           {
                //modelessDialog可以将modal换成dialog=yes
               feature ="width=400,height=300,menubar=no,toolbar=no,location=no,";
               feature+="scrollbars=no,status=no,modal=yes";
               feature+="scrollbars=no,status=no,modal=yes";
               var temp = window.showModalDialog("login.html",null,feature);

                $.checkCookie();
           }
        },

        checkCookie:function ()
        {
            var ckValue = $.cookie('username');
            var ckCodedefine = $.cookie('codedefine');

            // alert(ckValue);
            if (ckValue != "undefined" && ckValue != ""&& ckValue != null)
            {
                $("#userNameLab").text(ckValue);
                $("#userNameLab").show();
                $("#exitLab").show();
                $("#loginLab").hide();

                userName = ckValue;

                var srvCodes = ckCodedefine.split('|')

                // 将业务代码放到页面全局变量
                // change
                serviceCodes = new Array();
                serviceCodes = ckCodedefine.split("|")

            }
        },

        checkService:function (code)
        {
            for (i=0;i<serviceCodes.length ;i++ )
            {
                var oneCode = serviceCodes[i];

                if (oneCode == null || oneCode == "")
                {
                    continue;
                }

                if (oneCode == code)
                {
                    return true;
                }

            }

            return false;
        },

        unCheckAllService:function ()
        {
            $("#findTrantionRightImg").hide();
            $("#queryProductPriceRightImg").hide();
            $("#serviceThreeRightImg").hide();

            selectIndex = null;
            return false;
        },

        deleteCookie:function ()
        {
            var ckValue = $.cookie('username');
            var ckCodedefine = $.cookie('codedefine');

            // alert(ckValue);
            if (ckValue != "undefined" && ckValue != ""&& ckValue != null)
            {
                $("#userNameLab").hide()
                $("#exitLab").hide();
                $("#loginLab").show();
            }

            $.cookie("username",null);
            $.cookie("codedefine",null);
        },

        encryptInfo:function(orginSrc){
            alert("encryptInfo");
        },
        
        isNull:function (datas) {
            alert("check");
            return (data == "" || data == undefined || data == null) ? 0 : 1;
        }
    });