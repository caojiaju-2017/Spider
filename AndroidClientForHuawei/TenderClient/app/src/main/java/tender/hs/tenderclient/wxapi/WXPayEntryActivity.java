package tender.hs.tenderclient.wxapi;

import android.content.Intent;
import android.os.Bundle;
import android.os.PersistableBundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.Toast;

import com.tencent.mm.sdk.constants.ConstantsAPI;
import com.tencent.mm.sdk.modelbase.BaseReq;
import com.tencent.mm.sdk.modelbase.BaseResp;
import com.tencent.mm.sdk.openapi.IWXAPI;
import com.tencent.mm.sdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.sdk.openapi.WXAPIFactory;

import tender.hs.tenderclient.HsApplication;
import tender.hs.tenderclient.R;

//import pts.com.wxpayproject.R;

/**
 * Created by Kang on 2017/2/10.
 */

public class WXPayEntryActivity extends AppCompatActivity implements IWXAPIEventHandler {

    private static final String TAG = "WXPayEntryActivity";

    private IWXAPI api;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_pay_status);

        api = WXAPIFactory.createWXAPI(this, HsApplication.APP_ID_WX);
        api.handleIntent(getIntent(), this);
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        setIntent(intent);
        api.handleIntent(intent, this);
    }

    @Override
    public void onReq(BaseReq baseReq) {

    }

    /**
     * 支付结果回调
     * @param resp
     */
    @Override
    public void onResp(BaseResp resp) {

        if (resp.errCode==0){
            Toast.makeText(WXPayEntryActivity.this,resp.errCode+"",Toast.LENGTH_SHORT).show();
//            intent=new Intent(WXPayEntryActivity.this, PayOKActivity.class);
//            startActivity(intent);
////                Intent intent = new Intent();
////                intent.setAction("com.jialimei.weixinpay");
////                //要发送的内容
////                intent.putExtra("errCode", resp.errCode);
////                //发送 一个无序广播
////                sendBroadcast(intent);
//            finish();

        }else {
//            intent=new Intent(WXPayEntryActivity.this, PayOKActivity.class);
//            startActivity(intent);
            Toast.makeText(WXPayEntryActivity.this,"支付失败。。。" + resp.errCode,Toast.LENGTH_SHORT).show();

        }

    }
}
