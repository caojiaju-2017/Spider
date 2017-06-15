/**
 * Created by caojiaju on 17/6/14.
 */
var pageIndex = 0;
var pageSize = 20;
var serviceCode = 1001;

$(document).ready(function()
{
    $.setResultPosition();
    $.search_data();
});

$(window).resize(function(){
    $.setResultPosition();
});

$.extend({
    search_data: function ()
    {

        var ckValue = $.cookie('username');

        if (ckValue == "undefined" || ckValue == "" || ckValue == null)
        {
            return;
        }


        var cookieCode = $.cookie('cookie_uuid');
        $.post("search_data", {username: ckValue, cookiecode: cookieCode,pageindex:pageIndex,pagesize:pageSize,servicecode:serviceCode},
            function (data)
            {
                // 左侧图片
                var url = data.name

                var count = data.count;

                for (var i = 0; i < count; i++){
                    eval("var urlId = data.name" + (i +1 ));
                    $.addReportImg(urlId);
                }

            },
            "json");//这里返回的类型有：json,html,xml,text
    },

    addReportImg:function (url) {
                var newImg = document.createElement("img");
                newImg.setAttribute("src", url);
                $("#resultLeft").append(newImg);
    },
    setResultPosition:function () {

        $("#resultRight").width($("#result_body_id").width() - $("#resultLeft").width() - 10*4);
        $("#resultRight").height($(document).height() - $("#top_titile_id").height() - 4*10);

        $("#resultLeft").height($("#resultRight").height());

    }
});
