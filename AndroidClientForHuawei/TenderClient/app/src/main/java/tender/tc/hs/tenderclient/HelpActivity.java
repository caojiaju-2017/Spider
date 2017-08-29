package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.widget.ImageView;
import android.widget.TextView;


import tender.tc.hs.tenderclient.Data.UserInfo;


public class HelpActivity extends Activity implements OnClickListener {
//    private IWXAPI api;
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

//        api = WXAPIFactory.createWXAPI(this, HsApplication.Global_App.wxAppid, true);
//        api.registerApp(HsApplication.Global_App.wxAppid);
      //  api.handleIntent(getIntent(),this);

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
//                send(true);
                break;
            case R.id.wx_account2:
//                send(false);
                break;
            case R.id.phone_img:
                TextView org_phone = (TextView)findViewById(R.id.org_phone);
                call(org_phone.getText().toString());
                break;
        }
    }

    /**
     * 调用拨号界面
     * @param phone 电话号码
     */
    private void call(String phone) {
        Intent intent = new Intent(Intent.ACTION_DIAL,Uri.parse("tel:"+phone));
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //值为true，表示发送到朋友圈,反之发送给群或者好友
//    private void send(boolean sendType) {
//        String title ;
//        String wcAccount ;
//        if (sendType)
//        {
//            title = "汉森客服二维码";
//            wcAccount = "微信号： han_sen2017";
//        }
//        else
//        {
//            title = "汉森公众号二维码";
//            wcAccount = "微信号： hansenjiaoyucd";
//        }
//
//        WXWebpageObject webpage = new WXWebpageObject();
//        webpage.webpageUrl = "http://blog.csdn.net/u013626215/article/details/51679713";
//
//        WXMediaMessage msg = new WXMediaMessage(webpage);
//        msg.mediaObject = webpage;
//        msg.title = title;
//        msg.description = wcAccount;
//        Bitmap thumb = BitmapFactory.decodeResource(getResources(), R.drawable.kf_erweima);
//        msg.thumbData = Util.bmpToByteArray(thumb, true);
//
//        SendMessageToWX.Req req = new SendMessageToWX.Req();
//        req.transaction = buildTransaction("webpage");
//        req.message = msg;
////        req.scene = sendType ? SendMessageToWX.Req.WXSceneTimeline : SendMessageToWX.Req.WXSceneSession;
//        Log.i("test","send!");
//        api.sendReq(req);
//    }

    private String buildTransaction(final String type) {
        return (type == null) ? String.valueOf(System.currentTimeMillis()) : type + System.currentTimeMillis();
    }

}
