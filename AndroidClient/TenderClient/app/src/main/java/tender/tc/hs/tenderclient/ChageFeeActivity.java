package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.app.Dialog;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.modelpay.PayReq;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Random;

//import io.github.mayubao.pay_library.PayAPI;
//import io.github.mayubao.pay_library.WechatPayReq;
//import io.github.mayubao.pay_library.PayAPI;
//import io.github.mayubao.pay_library.WechatPayReq;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.Util.LogUtil;
import tender.tc.hs.tenderclient.wxapi.MD5;


public class ChageFeeActivity extends Activity implements OnClickListener
{
    private IWXAPI msgApi = WXAPIFactory.createWXAPI(this, HsApplication.Global_App.wxAppid);

    Handler mainHandlers;

    ImageView img_go_back;
    EditText months_input;
    Button fetch_order;

    RelativeLayout order_info;
    TextView order_number;
    TextView order_price;

    RelativeLayout chage_chanel;
    Button start_pay;

    String prepareId ;
    String signString;
    String nonceStr;
    String mchId;
    String timeSanp ;
    String API_KEY;

    private Dialog mRegisterHandle;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.pay_activity);
        registerComminucation();

        initView();

        msgApi.registerApp(HsApplication.Global_App.wxAppid);
    }

    private void initView() {
        img_go_back = (ImageView)this.findViewById(R.id.go_back);
        months_input = (EditText)this.findViewById(R.id.months_input);
        fetch_order = (Button)this.findViewById(R.id.fetch_order);
        order_info= (RelativeLayout)this.findViewById(R.id.order_info);
        order_number= (TextView)this.findViewById(R.id.order_number);
        order_price= (TextView)this.findViewById(R.id.order_price);
        chage_chanel= (RelativeLayout)this.findViewById(R.id.chage_chanel);
        start_pay = (Button)this.findViewById(R.id.start_pay);

        img_go_back.setOnClickListener(this);
        fetch_order.setOnClickListener(this);
        start_pay.setOnClickListener(this);

        months_input.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });
    }
    private void registerComminucation() {
        // 主线程通信事件句柄
        mainHandlers = new Handler() {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case CommandDefine.CREATE_ORDER:
                    {
                        try {
                            HsProgressDialog.getProgressDlg().Close();
                        } catch (Exception e) {
                            // TODO: handle exception
                        }

                        String receiveData = msg.obj.toString();

                        JSONTokener jsonParser = new JSONTokener(receiveData);
                        try {
                            JSONObject person = (JSONObject) jsonParser.nextValue();

                            String errorinfo = person.getString("ErrorInfo");
                            int errorId = Integer.parseInt(person.getString("ErrorId"));
                            if (errorId == 200) {
                                JSONObject result = person.getJSONObject("Result");

                                prepareId = result.getString("OrderNo");
                                signString = result.getString("Sign");
                                nonceStr = result.getString("NonceStr");
                                mchId = result.getString("MchId");
                                timeSanp = result.getString("TimeStamp");
                                API_KEY = result.getString("ApiKey");
                                openChageInfo(prepareId, result.getString("OrderPrice"));


                                break;
                            }
                            else
                            {
                                months_input.setEnabled(true);
                                fetch_order.setEnabled(true);
                                Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
                            }

                        } catch (JSONException e) {
                            months_input.setEnabled(true);
                            fetch_order.setEnabled(true);
                            e.printStackTrace();
                        }
                        break;
                    }
                }
            }
        };
    }

    private void openChageInfo(String orderNo, String price)
    {
        order_info.setVisibility(View.VISIBLE);
        start_pay.setVisibility(View.VISIBLE);
        chage_chanel.setVisibility(View.VISIBLE);

        order_number.setText(orderNo);
        order_price.setText(price);
    }


    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.go_back:
                this.finish();
                break;
            case R.id.fetch_order:
                creatOrderNumber();
                break;
            case R.id.start_pay:
                startPay();
        }
    }
    private long genTimeStamp() {
        return System.currentTimeMillis() / 1000;
    }
    //生成随机号，防重发
    private String getNonceStr() {
        // TODO Auto-generated method stub
        Random random=new Random();

        return MD5.getMessageDigest(String.valueOf(random.nextInt(10000)).getBytes());
    }
    private  void startPay2()
    {
//        WechatPayReq wechatPayReq = new WechatPayReq.Builder()
//                .with(this) //activity instance
//                .setAppId(HsApplication.Global_App.wxAppid) //wechat pay AppID
//                .setPartnerId(mchId)//wechat pay partner id
//                .setPrepayId(prepareId)//pre pay id
//                .setPackageValue("Sign=WXPay")//"Sign=WXPay"
//                .setNonceStr(nonceStr)
//                .setTimeStamp(timeSanp)//time stamp
//                .setSign(signString)//sign
//                .create();
//        //2. send the request with wechat pay
//        PayAPI.getInstance().sendPayRequest(wechatPayReq);

    }
    private void startPay() {
       PayReq req = new PayReq();
        req.appId = HsApplication.Global_App.wxAppid;
        req.partnerId = mchId;
        req.prepayId = prepareId;
        req.nonceStr = nonceStr; //getNonceStr(); //
        req.timeStamp =  timeSanp;//String.valueOf(genTimeStamp());
        req.packageValue = "Sign=WXPay"; //"prepay_id="+prepareId; //"


//        req.sign = signString;
        List<NameValuePair> signParams = new LinkedList<NameValuePair>();
        signParams.add(new BasicNameValuePair("appid", req.appId));
        signParams.add(new BasicNameValuePair("noncestr", req.nonceStr));
        signParams.add(new BasicNameValuePair("package", req.packageValue));
        signParams.add(new BasicNameValuePair("partnerid", req.partnerId));
        signParams.add(new BasicNameValuePair("prepayid", req.prepayId));
        signParams.add(new BasicNameValuePair("timestamp", req.timeStamp));

        req.sign = genAppSign(signParams);

        Toast.makeText(this, "正常调起支付", Toast.LENGTH_SHORT).show();

        LogUtil.info(req.appId);

        boolean result = msgApi.sendReq(req);
        if (result)
        {

        }
//        //1.create request for wechat pay
//        WechatPayReq wechatPayReq = new WechatPayReq.Builder()
//                .with(this) //activity instance
//                .setAppId(appid) //wechat pay AppID
//                .setPartnerId(partnerid)//wechat pay partner id
//                .setPrepayId(prepayid)//pre pay id
////								.setPackageValue(wechatPayReq.get)//"Sign=WXPay"
//                .setNonceStr(noncestr)
//                .setTimeStamp(timestamp)//time stamp
//                .setSign(sign)//sign
//                .create();
//        //2. send the request with wechat pay
//        PayAPI.getInstance().sendPayRequest(wechatPayReq);




    }

    private String genAppSign(List<NameValuePair> params) {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < params.size(); i++) {
            sb.append(params.get(i).getName());
            sb.append('=');
            sb.append(params.get(i).getValue());
            sb.append('&');
        }
        sb.append("key=");
        sb.append("D306B2D5AB3793063150ACF448D36C28");

        sb.append("sign str\n"+sb.toString()+"\n\n");
        String appSign = MD5.getMessageDigest(sb.toString().getBytes());
        Log.e("Simon","----"+appSign);
        return appSign;
    }
    private void creatOrderNumber()
    {
//        order_info.setVisibility(View.VISIBLE);
//        start_pay.setVisibility(View.VISIBLE);
//        chage_chanel.setVisibility(View.VISIBLE);

        LogUtil.info("Invoke httpaccess");
        final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.CREATE_ORDER);

        final Map<String,String> dataMap = new HashMap<>();
        try {
            JSONObject jsonObject = new JSONObject();
            int numbs;

            try
            {
                numbs = Integer.parseInt(months_input.getText().toString());

                if (numbs > 36)
                {
                    Toast.makeText(getApplicationContext(), "输入的购买月份不正确，请输入整数，最长购买36个月", Toast.LENGTH_LONG).show();
                    return;
                }
            }
            catch (Exception ex)
            {
                Toast.makeText(getApplicationContext(), "输入的购买月份不正确，请输入整数，最长购买36个月", Toast.LENGTH_LONG).show();
                return;
            }
            jsonObject.put("Account",HsApplication.Global_App._myUserInfo._account);
            jsonObject.put("Months",numbs);

            access.setJsonObject(jsonObject);
        } catch (JSONException e) {
            LogUtil.info("Invoke httpaccess prepare failed");
            e.printStackTrace();
        }
        HsProgressDialog.newProgressDlg().show(ChageFeeActivity.this);
        months_input.setEnabled(false);
        fetch_order.setEnabled(false);
        new Thread(new Runnable(){
            public void run()
            {
                access.HttpPost();
            }
        }).start();


    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //值为true，表示发送到朋友圈,反之发送给群或者好友
    private void send(boolean sendType) {

    }



    //////////////////////////////////////////////////////////////////////////////////////
    /* 显示正在登录对话框 */
    private void showLoginingDlg() {
        if (mRegisterHandle != null)
            mRegisterHandle.show();
    }

    /* 关闭正在登录对话框 */
    private void closeLoginingDlg() {
        if (mRegisterHandle != null && mRegisterHandle.isShowing())
            mRegisterHandle.dismiss();
    }
    /* 初始化正在登录对话框 */
    private void initLoginingDlg() {

        mRegisterHandle = new Dialog(this, R.style.loginingDlg);
        mRegisterHandle.setContentView(R.layout.logining_dlg);

        Window window = mRegisterHandle.getWindow();
        WindowManager.LayoutParams params = window.getAttributes();
        // 获取和mLoginingDlg关联的当前窗口的属性，从而设置它在屏幕中显示的位置

        // 获取屏幕的高宽
        DisplayMetrics dm = new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(dm);
        int cxScreen = dm.widthPixels;
        int cyScreen = dm.heightPixels;

        int height = (int) getResources().getDimension(
                R.dimen.loginingdlg_height);// 高42dp
        int lrMargin = (int) getResources().getDimension(
                R.dimen.loginingdlg_lr_margin); // 左右边沿10dp
        int topMargin = (int) getResources().getDimension(
                R.dimen.loginingdlg_top_margin); // 上沿20dp

        params.y = (-(cyScreen - height) / 2) + topMargin; // -199
		/* 对话框默认位置在屏幕中心,所以x,y表示此控件到"屏幕中心"的偏移量 */

        params.width = cxScreen;
        params.height = height;
        // width,height表示mLoginingDlg的实际大小

        mRegisterHandle.setCanceledOnTouchOutside(true); // 设置点击Dialog外部任意区域关闭Dialog
    }


}
