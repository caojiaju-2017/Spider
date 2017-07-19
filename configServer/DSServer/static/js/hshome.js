/**
 * Created by jiaju_cao on 2017/6/7.
 */

var userName=null;
var loginToken="fsdefasfs#f!@fsae3";
var selectIndex = -1;

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
        // // 清空其他选项
        // $.clearSelection();
        //
        // // $.sleep(100);
        //
        // // 设置当前为选定项
        // $("#findTrantionImage").attr("src","/static/Srv/icon1-0.png");


        if($("#findTrantionRightImg").is(":hidden"))
        {
             $("#findTrantionRightImg").show();
        }
        else
        {
            $("#findTrantionRightImg").hide();
        }

        selectIndex = 1001;
    })

    $("#queryProductPriceImage").click(function() {
        // // 清空其他选项
        // $.clearSelection();
        //
        // // $.sleep(100);
        //
        // // 设置当前为选定项
        // $("#queryProductPriceImage").attr("src","/static/Srv/icon2-0.png");

        if($("#queryProductPriceRightImg").is(":hidden"))
        {
             $("#queryProductPriceRightImg").show();
        }
        else
        {
            $("#queryProductPriceRightImg").hide();
        }

        selectIndex = 1002;
        // alert("cccc");
    })

    $("#serviceThreeImage").click(function() {
        // 清空其他选项
        // $.clearSelection();
        // $.sleep(3000);
        // 设置当前为选定项
        // $("#rightImg").attr("hidden","show");


        if($("#serviceThreeRightImg").is(":hidden"))
        {
             $("#serviceThreeRightImg").show();
        }
        else
        {
            $("#serviceThreeRightImg").hide();
        }


        // if( =='visible')
        //     $("rightImg").css().visibility='hidden';
        // else
        //     $("rightImg").css().visibility='visible';

        // alert("b");
        selectIndex = 1003;
    })

    $("#serviceFourImage").click(function() {
        // 清空其他选项
        // $.clearSelection();
        // // $.sleep(3000);
        // // 设置当前为选定项
        // $("#serviceFourImage").attr("src","/static/Srv/icon4-0.png");
        //
        // selectIndex = 1004;
        // // alert("cccc2");
    })

    $("#serviceFiveImage").click(function() {
        // 清空其他选项
        // $.clearSelection();
        // // $.sleep(3000);
        // // 设置当前为选定项
        // $("#serviceFiveImage").attr("src","/static/Srv/icon5-0.png");
        //
        // selectIndex = 1005;
        // alert("cccc2");
    })

    $("#serviceSixImage").click(function() {
        // // 清空其他选项
        // $.clearSelection();
        // // $.sleep(3000);
        // // 设置当前为选定项
        // $("#serviceSixImage").attr("src","/static/Srv/icon6-0.png");
        //
        // selectIndex = 1006;
        // // alert("cccc2");
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

               // alert("ckValue");
                $.checkCookie();
           }
        },

        checkCookie:function ()
        {
            var ckValue = $.cookie('username');

            // alert(ckValue);
            if (ckValue != "undefined" && ckValue != ""&& ckValue != null)
            {
                $("#userNameLab").text(ckValue);
                $("#userNameLab").show();
                $("#exitLab").show();
                $("#loginLab").hide();

                userName = ckValue;

            }
        },

        deleteCookie:function ()
        {
            var ckValue = $.cookie('username');

            // alert(ckValue);
            if (ckValue != "undefined" && ckValue != ""&& ckValue != null)
            {
                $("#userNameLab").hide()
                $("#exitLab").hide();
                $("#loginLab").show();
            }

            $.cookie("username",null);
        },

        encryptInfo:function(orginSrc){
            alert("encryptInfo");
        },
        
        isNull:function (datas) {
            alert("check");
            return (data == "" || data == undefined || data == null) ? 0 : 1;
        }
    });