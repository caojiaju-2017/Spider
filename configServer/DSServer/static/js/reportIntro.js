/**
 * Created by caojiaju on 17-7-25.
 */
/**
 * Created by jiaju_cao on 2017/6/7.
 */

var userName=null;
var loginToken="fsdefasfs#f!@fsae3";
var selectIndex = null;

var serviceCodes ;

window.onload=function()
{
    // $.setPosition();
};

$(window).resize(function(){
    // $.setPosition();
});


$(document).ready(function(){
    // 设置cookie
    $.checkCookie();

    setInterval("$.imgLoop()",3000);
    $("#ewmImageLarge").css("left",$("#ewmImage").offset().left + 15);

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
        imgLoop: function () {
        imageIndex = imageIndex + 1;

        if (imageIndex > 3)
        {
            imageIndex = 0;
        }

        if (imageIndex == 0) // 第一张
        {
            $("#topImg").attr("src","/static/intro/1.jpg");
        }
        else if (imageIndex == 1)
        {
            $("#topImg").attr("src","/static/intro/2.jpg");
        }
        else if(imageIndex == 2)
        {
            $("#topImg").attr("src","/static/intro/3.jpg");
        }
        else if(imageIndex == 3)
        {
            $("#topImg").attr("src","/static/intro/4.jpg");
        }
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