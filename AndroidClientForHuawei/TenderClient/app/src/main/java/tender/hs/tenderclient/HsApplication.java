package tender.hs.tenderclient;

import android.app.Application;
import android.content.Context;
import android.support.multidex.MultiDex;

import java.util.List;

import tender.hs.tenderclient.Data.BookData;
import tender.hs.tenderclient.Data.UserInfo;
import tender.hs.tenderclient.Report.ReportObject;

public class HsApplication extends Application
{

	@Override
	protected void attachBaseContext(Context base) {
		super.attachBaseContext(base);
		MultiDex.install(this);
	}


	public static HsApplication Global_App;

	public String url_HeadString = "http://192.168.1.104:7001";
//	public String url_HeadString = "http://115.159.224.102:7001";

	public String SrvCode = "CHINA_ZF_ZB_0000"; // 政府招标信息

//	public String wxAppid = "wx5cf4976ee8ae65c5";
	public static final String APP_ID_WX = "wx5cf4976ee8ae65c5";//1487941952
//	// 商户号
//	public static final String MCH_ID_WX = "1487941952";
//	// API密钥，在商户平台设置
//	public static final String API_KEY_WX = "57DC610BC6CC48B28330260649A665FD";  //778960704b1d68df879e506a49d81cc7


	public ClientWay globalWay = ClientWay.Tecent;



	public UserInfo _myUserInfo;

	public BookData _currentBookInfo;

	public List<ReportObject> _currentReportData;
	@Override
	public void onCreate() {
		super.onCreate();
	}

}
enum ClientWay
{
	HuaWei,Tecent
}
