package tender.tc.hs.tenderclient;

import android.app.Application;

import java.util.List;

import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.Data.MyBook;
import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.Report.ReportObject;

public class HsApplication extends Application {
	public static HsApplication Global_App;

	public String url_HeadString = "http://192.168.1.209:7001";
//	public String url_HeadString = "http://115.159.224.102:7001";

	public String SrvCode = "CHINA_ZF_ZB_0000"; // 政府招标信息
	public String wxAppid = "";

	public UserInfo _myUserInfo;

	public BookData _currentBookInfo;

	public List<ReportObject> _currentReportData;
	@Override
	public void onCreate() {
		super.onCreate();
	}

}
