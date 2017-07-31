$(document).ready(function()
{
    $.setGuestAccount();

    $("#btnLogin").click(function()
    {
        // alert("aaaaa");
$.blockUI();
        // 发送请求
        $.ajax_post();
        //登陆成功

    })

    $("#btnCancel").click(function()
    {
        $("#_loginPanel").hide();
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

                srvCodes = receiveObj.ServiceCodes;

                var codeString = "";
                for (i=0;i<srvCodes.length ;i++ )
                {
                    var oneCode = srvCodes[i];
                    oneCode = oneCode.replace(/|/g,"");
                    codeString = codeString + "|" + oneCode
                }
                alert(codeString);
                $.cookie('codedefine', codeString);
                $.cookie('username', $('#userName').val());

                // 设置cookie

                //window.close();
                $.checkCookie();
$.unblockUI();
                $("#_loginPanel").hide();


            },
            "text");//这里返回的类型有：json,html,xml,text
    },
    setGuestAccount:function () {
        $("#userName").attr("value","guest04");
        $("#userPassword").attr("value","caojj123");
    },
});

