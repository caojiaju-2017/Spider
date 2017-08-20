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
import android.webkit.WebSettings;
import android.webkit.WebView;
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

import java.util.HashMap;
import java.util.Map;

import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class BookDetailViewActivity extends Activity implements OnClickListener,IWXAPIEventHandler
{
    private IWXAPI api;

    Handler mainHandlers;
    ImageView img_go_back;
    ImageView img_share_info;
    ImageView img_save_change;

    UserInfo _updateUserInfo;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.book_detail_view);

        api = WXAPIFactory.createWXAPI(this,"");
        api.handleIntent(getIntent(),this);

        initView();

        BookData bookData = HsApplication.Global_App._currentBookInfo;

        String urlString = bookData._url;
        WebView detail_webview = this.findViewById(R.id.detail_webview);
        //支持javascript
        detail_webview.getSettings().setJavaScriptEnabled(true);
        // 设置可以支持缩放
        detail_webview.getSettings().setSupportZoom(true);
        // 设置出现缩放工具
        detail_webview.getSettings().setBuiltInZoomControls(true);
        //扩大比例的缩放
        detail_webview.getSettings().setUseWideViewPort(true);
        //自适应屏幕
        detail_webview.getSettings().setLayoutAlgorithm(WebSettings.LayoutAlgorithm.SINGLE_COLUMN);
        detail_webview.getSettings().setLoadWithOverviewMode(true);

        detail_webview.loadUrl(urlString);
    }

    private void initView() {
        img_go_back = (ImageView)this.findViewById(R.id.go_back);
        img_go_back.setOnClickListener(this);

        img_share_info = (ImageView)this.findViewById(R.id.share_info);
        img_share_info.setOnClickListener(this);
    }
    private void registerComminucation() {
        // 主线程通信事件句柄
        mainHandlers = new Handler() {
            @Override
            public void handleMessage(Message msg) {
//                switch (msg.what) {
//                    case CommandDefine.SET_USERINFO: {
//                        break;
//                    }

//                }
            }
        };
    }



    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.go_back:
                this.finish();
                break;
            case R.id.share_info:
                send(true);
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //值为true，表示发送到朋友圈,反之发送给群或者好友
    private void send(boolean sendType) {
        Log.i("test","执行");
        Toast.makeText(getApplicationContext(), "等待分配APPID,请稍后", Toast.LENGTH_LONG).show();

        WXWebpageObject webpage = new WXWebpageObject();
        webpage.webpageUrl = HsApplication.Global_App._currentBookInfo._url;
        WXMediaMessage msg = new WXMediaMessage(webpage);
        msg.title = HsApplication.Global_App._currentBookInfo._title;
        msg.description = HsApplication.Global_App._currentBookInfo._projectName;
        Bitmap thumb = BitmapFactory.decodeResource(getResources(), R.drawable.tender);
        msg.thumbData = Util.bmpToByteArray(thumb, true);

        SendMessageToWX.Req req = new SendMessageToWX.Req();
        req.transaction = buildTransaction("webpage");
        req.message = msg;
        req.scene = sendType ? SendMessageToWX.Req.WXSceneTimeline : SendMessageToWX.Req.WXSceneSession;
        Log.i("test","send!");
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
