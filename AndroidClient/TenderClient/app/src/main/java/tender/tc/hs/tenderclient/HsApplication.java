package tender.tc.hs.tenderclient;

import android.app.Application;

public class HsApplication extends Application {
	public static HsApplication Global_App;

	public String url_HeadString = "http://192.168.1.105:7001";
//	public String url_HeadString = "http://115.159.224.102:7001";

	public String SrvCode = "CHINA_ZF_ZB_0000"; // 政府招标信息

	@Override
	public void onCreate() {
		super.onCreate();
	}

}
