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
        $.post("excuteLogin", {username: $('#userName').val(), password: $.md5($('#userPassword').val())},
            function (data)
            {
                 var receiveObj = eval("("+data+")");

                // 如果发生错误，则报错
                if (receiveObj.ErrorCode != 0)
                {
                    alert(receiveObj.Result);
                    return;
                }
                // alert(data)
                window.returnValue = data;


                $.cookie('username', $('#userName').val());

                // 设置cookie

                window.close();
            },
            "text");//这里返回的类型有：json,html,xml,text
    },
    setGuestAccount:function () {
        $("#userName").attr("value","guest04");
        $("#userPassword").attr("value","000000");
    },
});

