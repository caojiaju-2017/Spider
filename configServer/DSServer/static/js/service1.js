/**
 * Created by caojiaju on 17/6/14.
 */
var pageIndex = 0;
var pageSize = 20;
var serviceCode = 1001;

$(document).ready(function()
{
    $.search_data();
});


$.extend({
    search_data: function ()
    {
        var ckValue = $.cookie('username');
        if (ckValue != "undefined" && ckValue != ""&& ckValue != null)
        {
            return;
        }
        var cookieCode = $.cookie('cookie_uuid');

        $.post("search_data", {username: ckValue, cookiecode: cookieCode,pageindex:pageIndex,pagesize:pageSize,servicecode:serviceCode},
            function (data)
            {
                // alert(data);
                $("#resultbody").text( data );
            },
            "text");//这里返回的类型有：json,html,xml,text
    },
});
