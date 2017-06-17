/**
 * Created by caojiaju on 17/6/14.
 */
var userName ;
var pageIndex = 0;
var pageSize = 20;
var serviceCode = 1001;
var cookieCode;



$(document).ready(function()
{
    $.loadCookieInfo();
    $.setResultPosition();
    $.search_data();
});

$(window).resize(function(){
    $.setResultPosition();
});

$.extend({
    search_data: function ()
    {
        // 提取用户名
        userName = $.cookie('username');
        if (userName == "undefined" || userName == "" || userName == null)
        {
            return;
        }

        // 提取cookie
         cookieCode = $.cookie('cookiecode');
        // if (cookiecode == "undefined" || cookiecode == "" || cookiecode == null)
        // {
        //     return;
        // }

        pageIndex = $.cookie('pageindex');
        if (pageIndex == "undefined" || pageIndex == "" || pageIndex == null)
        {
            return;
        }


        serviceCode = $.cookie('servicecode');
        if (serviceCode == "undefined" || serviceCode == "" || serviceCode == null)
        {
            return;
        }

        $.post("search_data", {username: userName, cookiecode: cookieCode,pageindex:pageIndex,pagesize:pageSize,servicecode:serviceCode},
            function (data)
            {
                // 检查查询状态
                var  statu = data.status;
                var  errorInfo = data.errorinfo;

                if (statu == 1001) // 体验账户今天使用次数已到
                {
                    alert(errorInfo);

                    var urlAddress = "home.html";
                    location.href =urlAddress;
                    return;
                }

                // 左侧图片
                var ImgCount = data.ImageCount;
                for (var i = 0; i < ImgCount; i++){
                    eval("var urlId = data.name" + (i +1 ));
                    // alert(urlId);
                    $.addReportImg(urlId);
                }

                // 右侧item
                var itemCount = data.ItemCount;

                for (var index = 0 ; index < itemCount; index ++)
                {

                    eval("var oneItem = data.item" + (index +1 ));

                    $.addItemInfo(oneItem,"itemName" + index,index);

                }
            },
            "json");//这里返回的类型有：json,html,xml,text
    },

    addReportImg:function (url) {
                var newImg = document.createElement("img");
                newImg.setAttribute("src", url);
                // newImg.setAttribute("id",imageName);
                newImg.setAttribute("title","点击查看大图");

                newImg.setAttribute("onclick","$.imageClick('" + url + "')");

                $("#resultLeft").append(newImg);
    },

    addItemInfo:function (oneItem,itemName,index) {

        // 得到div 总宽度
        var divWd = $("#resultRight").width();
        var divHd = $("resultRight").height();

        // 计算每个卡片宽度
        var itemWd = 360;
        var itemHd = 60;
        // 每行最多item
        var itemCnt = Math.floor(divWd / itemWd);
        var leftWd = divWd % itemWd;

        if (leftWd == 0)
        {
            itemCnt = itemCnt - 1;
        }

        sep = (divWd - itemCnt*itemWd) / (itemCnt + 1);
        // 计算卡片索引
        var divItem = document.createElement("div");
        divItem.setAttribute("id",itemName);

        divItem.setAttribute("onclick","$.itemClick()");

        // alert("2");
        $("#resultRight").append(divItem);

        // alert(itemName);
        // 设置div位置
        // var left = sep ;
        var top = 15;
        $("#"+itemName).css("margin-Left",divWd*2 /20);
        $("#"+itemName).css("margin-Top",top);
        $("#"+itemName).css("margin-bottom",top);
        $("#"+itemName).css("cursor","pointer");

    },
    setResultPosition:function () {

        $("#resultRight").width(($("#result_body_id").width() - $("#resultLeft").width() - 10*3)/2);
        $("#resultRight").height($(document).height() - $("#top_titile_id").height() - 70);

        $("#resultLeft").height($("#resultRight").height());

        $("#MiddleDiv").width($(document).width() -  $("#resultLeft").width() - $("#resultRight").width() - 20);
        $("#MiddleDiv").height($("#resultRight").height());

    },
    
    loadCookieInfo:function () {
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

    imageClick:function (imageName)
    {
        // alert("imageClick");

        var imgInfos = imageName.split('/');

        var imageNameShort = imgInfos[imgInfos.length - 1];

        // alert(imageNameShort);

        $.showDialog("ImageView.html?ImageName=" + imageNameShort);
    },

    showDialog: function (url) {

           if(   document.all   ) //IE
           {
               feature="dialogWidth:900px;dialogHeight:640px;status:no;help:no";
               window.showModalDialog(url,null,feature);
           }
           else
           {
               // alert(url);
                //modelessDialog可以将modal换成dialog=yes
               feature ="width=900,height=640,menubar=no,toolbar=no,location=no,";
               feature+="scrollbars=no,status=no,modal=yes";
               feature+="scrollbars=no,status=no,modal=yes";
               window.showModalDialog(url,null,feature);
           }
        },
    itemClick:function () {
        alert("游客未开放，请联系客服。");
    },
});
