/**
 * Created by caojiaju on 17/6/17.
 */
window.onload=function()
{
    var widthClient = 940;
    var heightClient = 730;

    // 获取屏幕尺寸
    var hd = window.screen.height;
    var wd =window.screen.width;

    window.moveTo((wd - widthClient) / 2, (hd - heightClient) / 2);

    window.resizeTo(940, 740);
};