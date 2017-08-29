package tender.tc.hs.tenderclient.wxapi;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast;

import com.tencent.mm.opensdk.constants.ConstantsAPI;
import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
//import com.wang.umbrella.app.CommonConstant;
//import com.wang.umbrella.bean.event.WXPaySuccessEvent;

//import de.greenrobot.event.EventBus;
import tender.tc.hs.tenderclient.HsApplication;


/**
 * Created by Dumpligs on 2017/8/9.
 */
public class WXPayEntryActivity extends Activity implements IWXAPIEventHandler {

    private IWXAPI api;


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        setContentView(R.layout.three);
        api = WXAPIFactory.createWXAPI(this, HsApplication.Global_App.wxAppid);
        api.handleIntent(getIntent(), this);
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        setIntent(intent);
        api.handleIntent(intent, this);
    }

    @Override
    public void onReq(BaseReq req) {

        Toast.makeText(this.getBaseContext(),"abc+",Toast.LENGTH_LONG);

//        Button start_btn = this.findViewById(R.id.start_btn);
//        String txt = start_btn.getText().toString();
//        start_btn.setText(txt + " ," + "DEF" + req.getType());
    }

    @Override
    public void onResp(BaseResp resp) {
//        Toast.makeText(this.getBaseContext(),"abc+",Toast.LENGTH_LONG);
//        Toast.makeText(this.getBaseContext(),"abc+" + resp.getType(),Toast.LENGTH_LONG);
//
//        Button start_btn = this.findViewById(R.id.start_btn);
//        String txt = start_btn.getText().toString();
//        start_btn.setText(txt + " ," + "ABC" + resp.errStr + "|" + resp.errCode);

        if (resp.getType() == ConstantsAPI.COMMAND_PAY_BY_WX) {
            //以下是自定义微信支付广播的发送，微信支付广播请自己定义
//            EventBus.getDefault().post(new WXPaySuccessEvent());
//            finish();

        }
    }
}