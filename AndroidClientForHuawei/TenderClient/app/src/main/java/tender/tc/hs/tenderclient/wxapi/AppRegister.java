package tender.tc.hs.tenderclient.wxapi;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

import com.tencent.mm.sdk.openapi.IWXAPI;
import com.tencent.mm.sdk.openapi.WXAPIFactory;

import tender.tc.hs.tenderclient.HsApplication;

public class AppRegister extends BroadcastReceiver {

	@Override
	public void onReceive(Context context, Intent intent) {
		final IWXAPI api = WXAPIFactory.createWXAPI(context, null);

		//填写微信开放平台的应用APPID
		api.registerApp(HsApplication.Global_App.wxAppid);
	}
}
