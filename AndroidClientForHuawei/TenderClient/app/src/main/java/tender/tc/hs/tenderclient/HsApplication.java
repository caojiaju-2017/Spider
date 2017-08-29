package tender.tc.hs.tenderclient;

import android.app.Application;
import android.content.Context;
import android.support.multidex.MultiDex;

import java.util.List;

import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.Report.ReportObject;

public class HsApplication extends Application
{

	@Override
	protected void attachBaseContext(Context base) {
		super.attachBaseContext(base);
		MultiDex.install(this);
	}


	public static HsApplication Global_App;

//	public String url_HeadString = "http://192.168.1.104:7001";
	public String url_HeadString = "http://115.159.224.102:7001";

	public String SrvCode = "CHINA_ZF_ZB_0000"; // 政府招标信息
	public String wxAppid = "wx5cf4976ee8ae65c5";


	public UserInfo _myUserInfo;

	public BookData _currentBookInfo;

	public List<ReportObject> _currentReportData;
	@Override
	public void onCreate() {
		super.onCreate();
	}

}
