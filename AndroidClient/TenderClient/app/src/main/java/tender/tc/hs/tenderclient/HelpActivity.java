package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.widget.EditText;
import android.widget.ImageView;
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

public class HelpActivity extends Activity implements OnClickListener,IWXAPIEventHandler
{
    private IWXAPI api;
    Handler mainHandlers;
    ImageView img_go_back;
    ImageView wx_account1;
    ImageView wx_account2;

    UserInfo _updateUserInfo;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.help_info);
        api = WXAPIFactory.createWXAPI(this,"");
        api.handleIntent(getIntent(),this);

        initView();

    }

    private void initView() {
        img_go_back = (ImageView)this.findViewById(R.id.go_back);
        img_go_back.setOnClickListener(this);

        wx_account1 = (ImageView)this.findViewById(R.id.wx_account1);
        wx_account2 = (ImageView)this.findViewById(R.id.wx_account2);

        wx_account1.setOnClickListener(this);
        wx_account2.setOnClickListener(this);
    }

    private void saveChange()
    {
        LogUtil.info("Invoke saveBookSetting");
        final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.SET_USERINFO);
        final Map<String,String> dataMap = new HashMap<>();

        try {
            JSONObject jsonObject = new JSONObject();

            EditText et_account = (EditText)this.findViewById(R.id.et_account);
            EditText et_email = (EditText)this.findViewById(R.id.et_email);
            EditText china_name = (EditText)this.findViewById(R.id.china_name);
            EditText custom_address = (EditText)this.findViewById(R.id.custom_address);
            EditText org_name = (EditText)this.findViewById(R.id.org_name);
            EditText service_t_date = (EditText)this.findViewById(R.id.service_t_date);

            String email = et_email.getText().toString();
            String phone = et_account.getText().toString();
            String alias = china_name.getText().toString();
            String address = custom_address.getText().toString();
            String orgname = org_name.getText().toString();


            jsonObject.put("Account",HsApplication.Global_App._myUserInfo._account);
            jsonObject.put("EMail",email);
            jsonObject.put("Alias",alias);
            jsonObject.put("Address",address);
            jsonObject.put("OrgName",orgname);


            _updateUserInfo = new UserInfo();
            _updateUserInfo._email = email;
            _updateUserInfo._Address = address;
            _updateUserInfo._Alias = alias;
            _updateUserInfo._orgName = orgname;


            access.setJsonObject(jsonObject);
        } catch (JSONException e) {
            LogUtil.info("Invoke httpaccess prepare failed");
            e.printStackTrace();
        }
        new Thread(new Runnable(){
            public void run()
            {
                access.HttpPost();
            }
        }).start();
    }

    private void registerComminucation() {
        // 主线程通信事件句柄
        mainHandlers = new Handler() {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case CommandDefine.SET_USERINFO: {

                        break;
                    }

                }
            }
        };
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
