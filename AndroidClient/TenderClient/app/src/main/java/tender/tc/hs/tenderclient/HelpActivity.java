package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.HashMap;
import java.util.Map;

import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class HelpActivity extends Activity implements OnClickListener
{
    Handler mainHandlers;
    ImageView img_go_back;
    ImageView img_save_change;

    UserInfo _updateUserInfo;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.help_info);

        initView();

//        // 注册主线程通信句柄
//        registerComminucation();
//
//        // 加载当前用户数据
//        loadUserInfo();
    }

    private void loadUserInfo() {
        EditText et_account = (EditText)this.findViewById(R.id.et_account);
        EditText et_email = (EditText)this.findViewById(R.id.et_email);
        EditText china_name = (EditText)this.findViewById(R.id.china_name);
        EditText custom_address = (EditText)this.findViewById(R.id.custom_address);
        EditText org_name = (EditText)this.findViewById(R.id.org_name);
        EditText service_t_date = (EditText)this.findViewById(R.id.service_t_date);

        et_account.setText(HsApplication.Global_App._myUserInfo._account);
        et_email.setText(HsApplication.Global_App._myUserInfo._email);
        china_name.setText(HsApplication.Global_App._myUserInfo._Alias);
        custom_address.setText(HsApplication.Global_App._myUserInfo._Address);
        org_name.setText(HsApplication.Global_App._myUserInfo._orgName);
        service_t_date.setText(HsApplication.Global_App._myUserInfo._serviceOverDate);
    }

    private void initView() {
        img_go_back = (ImageView)this.findViewById(R.id.go_back);
        img_go_back.setOnClickListener(this);
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

        }
    }
}
