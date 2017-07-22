/**
 * Created by caojiaju on 17-7-22.
 */
var leftDivWidth = 400;
var divSep = 5;
window.onload=function()
{
    $.setPosition();
};

$(window).resize(function(){
    $.setPosition();

});


$(document).ready(function() {
    // set cookie
    $.setPosition();
});



// custom function
$.extend({
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

        // 设置最外层div
        $("#_myorder").height($(window).height() - 20);
        $("#_myorder").css("left",($(window).width() - $("#_myorder").width()) / 2);

        var mainLeft = $("#_myorder").offset().left;
        var mainTop = $("#_myorder").offset().top
        var mainWidth = $("#_myorder").width()
        var mainHeight = $("#_myorder").height()


        // 设置左边div
        $("#_leftdiv").css("left",mainLeft);
        $("#_leftdiv").css("top",$("#_myorder").top);
        $("#_leftdiv").width(leftDivWidth);
        $("#_leftdiv").height(mainHeight);

        // alert(mainLeft);
        // 设置右边Div
        $("#_rightdiv").css("left",mainLeft + divSep + leftDivWidth);
        $("#_rightdiv").css("top",mainTop);
        $("#_rightdiv").width(mainWidth - divSep - leftDivWidth);
        $("#_rightdiv").height(mainHeight);
    },
});