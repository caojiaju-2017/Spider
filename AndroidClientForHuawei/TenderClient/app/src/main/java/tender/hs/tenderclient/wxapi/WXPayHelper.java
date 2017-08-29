package tender.hs.tenderclient.wxapi;

import android.content.Context;

import com.tencent.mm.sdk.modelpay.PayReq;
import com.tencent.mm.sdk.openapi.IWXAPI;
import com.tencent.mm.sdk.openapi.WXAPIFactory;

import tender.hs.tenderclient.HsApplication;

public class WXPayHelper {

	PayReq req;
	IWXAPI msgApi;
	StringBuffer sb;

	public WXPayHelper(Context context) {
		super();
		msgApi = WXAPIFactory.createWXAPI(context, HsApplication.APP_ID_WX,false);
		req = new PayReq();
		sb = new StringBuffer();

		msgApi.registerApp(HsApplication.APP_ID_WX);
	}

	/**
	 * 使用后台返回的支付参数支付
	 * 
	 * @param payConfig
	 */
	public void doPay(PayConfig payConfig) {

		req.appId = payConfig.getAppid();
		req.partnerId = payConfig.getPartnerid();
		req.prepayId = payConfig.getPrepayid();
		req.packageValue = payConfig.getPack();
		req.nonceStr = payConfig.getNoncestr();
		req.timeStamp = payConfig.getTimestamp();
		req.sign = payConfig.getPaySign();

		PayReq request = new PayReq();
		request.appId = req.appId;
		request.partnerId = req.partnerId;
		request.prepayId= req.prepayId;
		request.packageValue = req.packageValue;
		request.nonceStr= req.nonceStr;
		request.timeStamp= req.timeStamp;
		request.sign= req.sign;

		// 发起支付
		msgApi.sendReq(req);
	}

}
