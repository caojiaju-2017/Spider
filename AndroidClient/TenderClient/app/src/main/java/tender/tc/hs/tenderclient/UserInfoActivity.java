package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v4.view.ViewPager.OnPageChangeListener;
import android.util.DisplayMetrics;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import tender.tc.hs.tenderclient.Data.MyBook;
import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.HsListView.XListView;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class UserInfoActivity extends Activity implements OnClickListener
{
    Handler mainHandlers;
    ImageView img_go_back;
    ImageView img_save_change;
    Button pay_btn;
    UserInfo _updateUserInfo;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.user_info);

        initView();

        // 注册主线程通信句柄
        registerComminucation();

        // 加载当前用户数据
        loadUserInfo();
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
        img_save_change = (ImageView)this.findViewById(R.id.save_change);
        pay_btn = (Button)this.findViewById(R.id.pay_btn);

        img_go_back.setOnClickListener(this);
        img_save_change.setOnClickListener(this);
        pay_btn.setOnClickListener(this);
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

                                Toast.makeText(UserInfoActivity.this, "操作成功", Toast.LENGTH_SHORT).show();
                                updateUserInfo();

                                finish();
                                break;
                            }
                            else
                            {
//                                closeLoginingDlg();// 关闭对话框
                                Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
                            }

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                        break;
                    }

                }
            }
        };
    }

    private void updateUserInfo() {
        if (_updateUserInfo == null)
        {

        }
        HsApplication.Global_App._myUserInfo._email = _updateUserInfo._email;
        HsApplication.Global_App._myUserInfo._Address = _updateUserInfo._Address;
        HsApplication.Global_App._myUserInfo._orgName = _updateUserInfo._orgName;
        HsApplication.Global_App._myUserInfo._Alias = _updateUserInfo._Alias;

    }


    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.go_back:
                this.finish();
                break;

            case R.id.save_change:
                HsProgressDialog.newProgressDlg().show(UserInfoActivity.this);
                saveChange();
                break;

            case R.id.pay_btn:
                Toast.makeText(getApplicationContext(), "暂未开发在线续费(后续升级会提供)，请联系管理员续费，管理员微信号：han_sen2017", Toast.LENGTH_LONG).show();
                break;
        }
    }
}
