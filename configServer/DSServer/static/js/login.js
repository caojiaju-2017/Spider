$(document).ready(function()
{
    $.setGuestAccount();

    $("#btnLogin").click(function()
    {

        // 发送请求
        $.ajax_post();
        //登陆成功

    })
});


$.extend({
    ajax_post: function ()
    {
        $.post("excuteLogin", {username: $('#userName').val(), password: $('#userPassword').val()},
            function (data)
            {
                // alert(data)
                window.returnValue = data;
                //
                // if(window.opener!=undefined)
                //   {
                //     window.opener.returnValue = "return from sub";
                //   }else{
                //     window.returnValue = "return from sub";
                //   }

                $.cookie('username', $('#userName').val());
                window.close();
            },
            "text");//这里返回的类型有：json,html,xml,text
    },
    setGuestAccount:function () {
        $("#userName").attr("value","guest04");
        $("#userPassword").attr("value","000000");
    },
});

