package tender.tc.hs.tenderclient;

import android.Manifest;
import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.tencent.mm.sdk.openapi.BaseReq;
import com.tencent.mm.sdk.openapi.BaseResp;
import com.tencent.mm.sdk.openapi.IWXAPI;
import com.tencent.mm.sdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.sdk.openapi.SendMessageToWX;
import com.tencent.mm.sdk.openapi.WXAPIFactory;
import com.tencent.mm.sdk.openapi.WXMediaMessage;
import com.tencent.mm.sdk.openapi.WXWebpageObject;
import com.tencent.mm.sdk.platformtools.Util;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.HashMap;
import java.util.Map;

import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class HelpActivity extends Activity implements OnClickListener, IWXAPIEventHandler {
    private IWXAPI api;
    Handler mainHandlers;
    ImageView img_go_back;
    ImageView wx_account1;
    ImageView wx_account2;

    ImageView phone_img;

    UserInfo _updateUserInfo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.help_info);
        api = WXAPIFactory.createWXAPI(this, "");
        api.handleIntent(getIntent(), this);

        initView();

    }

    private void initView() {
        img_go_back = (ImageView) this.findViewById(R.id.go_back);
        img_go_back.setOnClickListener(this);

        wx_account1 = (ImageView) this.findViewById(R.id.wx_account1);
        wx_account2 = (ImageView) this.findViewById(R.id.wx_account2);
        phone_img = (ImageView) this.findViewById(R.id.phone_img);

        wx_account1.setOnClickListener(this);
        wx_account2.setOnClickListener(this);
        phone_img.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.go_back:
                this.finish();
                break;
            case R.id.wx_account1:
                send(true);
                break;
            case R.id.wx_account2:
                send(true);
                break;
            case R.id.phone_img:
                TextView org_phone = (TextView)findViewById(R.id.org_phone);

                Intent mIntent = new Intent(Intent.ACTION_CALL);
                mIntent.setData(Uri.parse("tel:" + org_phone.getText().toString()));
                startActivity(mIntent);
                break;
        }
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////
    //值为true，表示发送到朋友圈,反之发送给群或者好友
    private void send(boolean sendType) {
        Log.i("test","执行");

        WXWebpageObject webpage = new WXWebpageObject();
        webpage.webpageUrl = "www.h-sen.com";
        WXMediaMessage msg = new WXMediaMessage(webpage);
        msg.title = "客服二维码";
        msg.description = "";
        Bitmap thumb = BitmapFactory.decodeResource(getResources(), R.drawable.kf_erweima);
        msg.thumbData = Util.bmpToByteArray(thumb, true);

        SendMessageToWX.Req req = new SendMessageToWX.Req();
        req.transaction = buildTransaction("webpage");
        req.message = msg;
        req.scene = sendType ? SendMessageToWX.Req.WXSceneTimeline : SendMessageToWX.Req.WXSceneSession;

        Toast.makeText(getApplicationContext(), "等待分配APPID,请稍后", Toast.LENGTH_LONG).show();
        api.sendReq(req);
    }

    private String buildTransaction(final String type) {
        return (type == null) ? String.valueOf(System.currentTimeMillis()) : type + System.currentTimeMillis();
    }

    @Override
    public void onReq(BaseReq baseReq) {
    }

    @Override
    public void onResp(BaseResp baseResp) {
        String result = null;
        switch (baseResp.errCode) {
            case BaseResp.ErrCode.ERR_OK:
                result = "分享成功";
                Log.i("test","分享成功");
                break;
            case BaseResp.ErrCode.ERR_USER_CANCEL:
                result = "分享取消";
                Log.i("test",""+result);
                break;
            case BaseResp.ErrCode.ERR_AUTH_DENIED:
                result = "分享被拒绝";
                Log.i("test",""+result);
                break;
            default:
                result = "分享返回";
                Log.i("test",""+result);
                break;
        }
    }
}
