/**
 * Created by jiaju_cao on 2017/6/7.
 */

var userName='caojiaju';
var loginToken="fsdefasfs#f!@fsae3";
var selectIndex = -1;

window.onload=function()
{
    $.setPosition();

    $.init();
};

$(window).resize(function(){
    $.setPosition();
        // alert("aaa");

});


$(document).ready(function(){
    // var skey = getCookie("p_skey");
    // alert(skey);

    // 窗体生成时注册事件＝＝＝＝＝＝＝＝同图片切换相关
    $("#findTrantionImage").click(function() {
        // 清空其他选项
        $.clearSelection();

        // $.sleep(100);

        // 设置当前为选定项
        $("#findTrantionImage").attr("src","/static/Srv/icon1-0.png");

        selectIndex = 1;
    })

    $("#queryProductPriceImage").click(function() {
        // 清空其他选项
        $.clearSelection();

        // $.sleep(100);

        // 设置当前为选定项
        $("#queryProductPriceImage").attr("src","/static/Srv/icon2-0.png");

        selectIndex = 2;
        // alert("cccc");
    })

    $("#serviceThreeImage").click(function() {
        // 清空其他选项
        $.clearSelection();
        // $.sleep(3000);
        // 设置当前为选定项
        $("#serviceThreeImage").attr("src","/static/Srv/icon3-0.png");
        // alert("cccc2");

        selectIndex = 3;
    })

    $("#serviceFourImage").click(function() {
        // 清空其他选项
        $.clearSelection();
        // $.sleep(3000);
        // 设置当前为选定项
        $("#serviceFourImage").attr("src","/static/Srv/icon4-0.png");

        selectIndex = 4;
        // alert("cccc2");
    })

    $("#serviceFiveImage").click(function() {
        // 清空其他选项
        $.clearSelection();
        // $.sleep(3000);
        // 设置当前为选定项
        $("#serviceFiveImage").attr("src","/static/Srv/icon5-0.png");

        selectIndex = 5;
        // alert("cccc2");
    })

    $("#serviceSixImage").click(function() {
        // 清空其他选项
        $.clearSelection();
        // $.sleep(3000);
        // 设置当前为选定项
        $("#serviceSixImage").attr("src","/static/Srv/icon6-0.png");

        selectIndex = 6;
        // alert("cccc2");
    })

    // 搜索按钮
    $("#searchButton").click(function() {
        // alert("start1");
        // 取出过滤字符串
        var searchContextValue = $("#searchContext").val();

        // 未输入，则不执行查询
        if(searchContextValue == null || searchContextValue == "" || selectIndex == -1) 
        {
            alert("请输入搜索内容，并指定业务服务类型（通过下方图片选择即可）");
            return ;
        } 

        // alert("start");
        // alert(selectIndex);
        var urlAddress = "searchData?" + 'userName=' + userName + '&loginToken='+ loginToken + '&ServiceIndex=' + selectIndex;

        alert(urlAddress);
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

               $("#loginLab").text("caojiaju");
               // alert("result===>" + temp);
           }
        },

        encryptInfo:function(orginSrc){
            alert("encryptInfo");
        },

    });