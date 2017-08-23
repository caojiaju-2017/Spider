package tender.tc.hs.tenderclient;

import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.DisplayMetrics;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;

import tender.tc.hs.tenderclient.Data.MyBook;
import tender.tc.hs.tenderclient.Data.UserInfo;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class UserBookSettingActivity extends Activity implements OnClickListener
{
    private static final int DATE_DIALOG_ID = 1;
    private int SELECTID = 0;
    private int mYear;
    private int mMonth;
    private int mDay;

    Handler mainHandlers;
    MyBook newBookInfo;
    private Dialog mRegisterHandle;
    ToggleButton mToggleButton01;

    Button save_book_btn;
    ImageView go_back ;

    private EditText book_stopdate;
    private EditText book_startdate;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.user_book_info);

        initView();

        // 注册主线程通信句柄
        registerComminucation();

        // 加载当前用户数据
        loadUserSettingInfo();
    }

    private void initView() {
        book_startdate = (EditText)findViewById(R.id.book_startdate);
        book_stopdate = (EditText)findViewById(R.id.book_stopdate);
        save_book_btn = (Button)findViewById(R.id.save_book_btn);
        go_back = (ImageView)findViewById(R.id.go_back);
        mToggleButton01 = (ToggleButton)findViewById(R.id.mToggleButton01);


        Button pickDate = (Button) findViewById(R.id.but_showDate);
        Button pickDate2 = (Button) findViewById(R.id.but_showDate2);
        pickDate.setOnClickListener(this);
        pickDate2.setOnClickListener(this);
        save_book_btn.setOnClickListener(this);
        go_back.setOnClickListener(this);


        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);
    }

    private void loadUserSettingInfo() {
        if (HsApplication.Global_App._myUserInfo._myBookInfo == null)
        {
            return;
        }

        EditText emailTxt = (EditText)findViewById(R.id.book_email);
        EditText phoneTxt = (EditText)findViewById(R.id.book_phone);
        EditText startDateTxt = (EditText)findViewById(R.id.book_startdate);
        EditText stopDateTxt = (EditText)findViewById(R.id.book_stopdate);
        EditText bookKey1Txt = (EditText)findViewById(R.id.book_key1);
        EditText bookKey2Txt = (EditText)findViewById(R.id.book_key2);
        EditText bookKey3Txt = (EditText)findViewById(R.id.book_key3);

        mToggleButton01.setOnToggleChanged(new ToggleButton.OnToggleChanged() {
            @Override
            public void onToggle(boolean on) {
                if (on) {
                    Toast.makeText(UserBookSettingActivity.this, "打开", Toast.LENGTH_SHORT).show();
                }else {
                    Toast.makeText(UserBookSettingActivity.this, "关闭", Toast.LENGTH_SHORT).show();
                }
            }
        });

        emailTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._email);
        phoneTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._phone);
        startDateTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._startDate);
        stopDateTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._stopDate);
        bookKey1Txt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._key1);
        bookKey2Txt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._key2);
        bookKey3Txt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._key3);

        if (HsApplication.Global_App._myUserInfo._myBookInfo._enable == 0)
        {
            mToggleButton01.setToggleOff(true);
        }
        else
        {
            mToggleButton01.setToggleOn(true);
        }
    }

    private void saveChange()
    {
        LogUtil.info("Invoke saveBookSetting");
        final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.SET_BOOK);
        final Map<String,String> dataMap = new HashMap<>();

        try {
            JSONObject jsonObject = new JSONObject();

            EditText emailTxt = (EditText)findViewById(R.id.book_email);
            EditText phoneTxt = (EditText)findViewById(R.id.book_phone);
            EditText startDateTxt = (EditText)findViewById(R.id.book_startdate);
            EditText stopDateTxt = (EditText)findViewById(R.id.book_stopdate);
            EditText bookKey1Txt = (EditText)findViewById(R.id.book_key1);
            EditText bookKey2Txt = (EditText)findViewById(R.id.book_key2);
            EditText bookKey3Txt = (EditText)findViewById(R.id.book_key3);

            String email = emailTxt.getText().toString();
            String phone = phoneTxt.getText().toString();
            String startDate = startDateTxt.getText().toString();
            String stopDate = stopDateTxt.getText().toString();
            String key1 = bookKey1Txt.getText().toString();
            String key2 = bookKey2Txt.getText().toString();
            String key3 = bookKey3Txt.getText().toString();

            jsonObject.put("Account",HsApplication.Global_App._myUserInfo._account);
            jsonObject.put("Fliter1",key1);
            jsonObject.put("Fliter2",key2);
            jsonObject.put("Fliter3",key3);
            jsonObject.put("EMail",email);
            jsonObject.put("Phone",phone);
            jsonObject.put("StartDate",startDate);
            jsonObject.put("StopDate",stopDate);
            jsonObject.put("Enable",(mToggleButton01.getToggleState()? 1:0));

            newBookInfo = new MyBook();
            newBookInfo._email = email;
            newBookInfo._startDate = startDate;
            newBookInfo._stopDate = stopDate;
            newBookInfo._enable = (mToggleButton01.getToggleState()? 1:0);
            newBookInfo._notifyType = 1;
            newBookInfo._phone = phone;
            newBookInfo._key1 = key1;
            newBookInfo._key2 = key2;
            newBookInfo._key3 = key3;


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
                    case CommandDefine.SET_BOOK: {
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
                                closeLoginingDlg();// 关闭对话框
                                Toast.makeText(UserBookSettingActivity.this, "操作成功", Toast.LENGTH_LONG).show();
                                HsApplication.Global_App._myUserInfo._myBookInfo = newBookInfo;

                                hideSoftInputMethod(UserBookSettingActivity.this);
                                finish();
                            }
                            else
                            {
                                closeLoginingDlg();// 关闭对话框
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

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.go_back:
                hideSoftInputMethod(UserBookSettingActivity.this);
                this.finish();
                break;

            case R.id.save_book_btn:
                HsProgressDialog.newProgressDlg().show(UserBookSettingActivity.this);
                saveChange();
                break;

            case R.id.pay_btn:
                Toast.makeText(getApplicationContext(), "暂未开发在线续费(后续升级会提供)，请联系管理员续费，管理员微信号：han_sen2017", Toast.LENGTH_LONG).show();
                break;

            // 日期相关
            case R.id.but_showDate:
                SELECTID = 0;
                showDialog(DATE_DIALOG_ID);
                break;

            case R.id.but_showDate2:
                SELECTID = 1;
                showDialog(DATE_DIALOG_ID);
                break;

        }
    }

    ////////////////////////////日期顯示對話框---------------------------------
    /**

     * 更新日期

     */

    private void updateDisplay() {
        if (SELECTID == 0)
        {
            book_startdate.setText(new StringBuilder().append(mYear).append("-").append(
                    (mMonth + 1) < 10 ? "0" + (mMonth + 1) : (mMonth + 1)).append("-").append(
                    (mDay < 10) ? "0" + mDay : mDay));
        }
        else
        {
            book_stopdate.setText(new StringBuilder().append(mYear).append("-").append(
                    (mMonth + 1) < 10 ? "0" + (mMonth + 1) : (mMonth + 1)).append("-").append(
                    (mDay < 10) ? "0" + mDay : mDay));
        }
    }
    /**
     * 日期控件的事件
     */
    private DatePickerDialog.OnDateSetListener mDateSetListener = new DatePickerDialog.OnDateSetListener() {

        public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
            mYear = year;
            mMonth = monthOfYear;
            mDay = dayOfMonth;
            updateDisplay();
        }
    };
    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case DATE_DIALOG_ID:
                return new DatePickerDialog(this, mDateSetListener, mYear, mMonth,
                        mDay);
        }
        return null;
    }

    @Override
    protected void onPrepareDialog(int id, Dialog dialog) {
        switch (id) {
            case DATE_DIALOG_ID:
                ((DatePickerDialog) dialog).updateDate(mYear, mMonth, mDay);
                break;

        }
    }

    /**
     * 关闭软键盘，输入法
     * @param context
     */
    public static void hideSoftInputMethod(Activity context)
    {
        try
        {
            InputMethodManager inputMethodManager = (InputMethodManager) context.getSystemService(Context.INPUT_METHOD_SERVICE);
            View view = context.getCurrentFocus();
            if (view != null)
            {
                inputMethodManager.hideSoftInputFromWindow(view.getWindowToken(), 0);
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

}
