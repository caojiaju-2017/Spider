package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.widget.EditText;
import android.widget.ImageView;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class BookDetailViewActivity extends Activity implements OnClickListener
{
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
                break;
        }
    }
}
