package tender.tc.hs.tenderclient;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;


import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
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
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import lecho.lib.hellocharts.listener.LineChartOnValueSelectListener;
import lecho.lib.hellocharts.listener.PieChartOnValueSelectListener;
import lecho.lib.hellocharts.model.PieChartData;
import lecho.lib.hellocharts.model.PointValue;
import lecho.lib.hellocharts.model.SliceValue;
import lecho.lib.hellocharts.view.LineChartView;
import lecho.lib.hellocharts.view.PieChartView;
import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.Data.MyBook;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.HsListView.BookItemAdapter;
import tender.tc.hs.tenderclient.HsListView.DisplayUtils;
import tender.tc.hs.tenderclient.HsListView.QueryInfo;
import tender.tc.hs.tenderclient.HsListView.XListView;
import tender.tc.hs.tenderclient.Report.ReportObject;
import tender.tc.hs.tenderclient.Report.LineReportService;
import tender.tc.hs.tenderclient.Util.LogUtil;

public class MainActivity extends Activity implements OnClickListener ,
        AdapterView.OnItemClickListener,
        XListView.IXListViewListener
{
    private static final int DATE_DIALOG_ID = 1;
    private int SELECTID = 0;
//    private static final int SHOW_DATAPICK = 0;
    private int mYear;
    private int mMonth;
    private int mDay;

    LinearLayout layout_book;
    LinearLayout layout_home;
    LinearLayout layout_report;
    ViewPager pager;
    PagerAdapter mAdapter;
    private List<View> mViews = new ArrayList<View>();

    private ImageView img_book;
    private ImageView img_home;
    private ImageView img_report;

    private ImageView img_person;
    private ImageView img_help;

    private  ImageView img_book_save;


    LinearLayout no_config_lay;
    LinearLayout have_config_lay;

    LinearLayout report_container;


    XListView customListView;
    private BookItemAdapter mAdapterTest;
    private ArrayList<BookData> items = new ArrayList<BookData>();
    private Handler mainHandlers;

    private Handler mHandler;
    private Dialog mRegisterHandle;

    private EditText book_stopdate;
    private EditText book_startdate;
    ToggleButton mToggleButton01;

    TextView except_tips;

    QueryInfo _queryInfo ;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.main_activity);

        // 注册主线程通信句柄
        registerComminucation();
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);



        //
        mHandler = new Handler();

//        geneItems();

        initView();
        initEvent();

        layout_home.performClick();

        _queryInfo = new QueryInfo(mainHandlers);

        _queryInfo.queryNextBathc();
//        showHomeWay(false);
    }

    private void loadBookConfig() {
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

    // 事件初始化
    private void initEvent() {
        layout_book.setOnClickListener(this);
        layout_home.setOnClickListener(this);
        layout_report.setOnClickListener(this);

        img_help.setOnClickListener(this);
        img_person.setOnClickListener(this);

        img_book_save.setOnClickListener(this);

//        layout_my.setOnClickListener(this);
        // 最新的api setOnPageChangeListener 已更换为addOnPageChangeListener
        pager.addOnPageChangeListener(new OnPageChangeListener() {
            @Override
            public void onPageSelected(int arg0) {
                int currentItem = pager.getCurrentItem();
                //重置图片颜色
                resetImg();
                switch (currentItem) {
                    case 0:
                        img_book.setImageResource(R.drawable.book_pressed);
                        break;
                    case 1:
                        setHomeTips();
                        img_home.setImageResource(R.drawable.home_pressed);
                        break;
                    case 2:
                        img_report.setImageResource(R.drawable.report_pressed);
                        break;
                }

            }

            @Override
            public void onPageScrolled(int arg0, float arg1, int arg2) {
                // TODO Auto-generated method stub

            }

            @Override
            public void onPageScrollStateChanged(int arg0) {
                // TODO Auto-generated method stub

            }
        });
    }

    private void initView() {
        layout_book = (LinearLayout) findViewById(R.id.layout_book);
        layout_home = (LinearLayout) findViewById(R.id.layout_home);
        layout_report = (LinearLayout) findViewById(R.id.layout_report);
        img_book = (ImageView) findViewById(R.id.img_book);
        img_home = (ImageView) findViewById(R.id.img_home);
        img_report = (ImageView) findViewById(R.id.img_report);

        img_person = (ImageView)findViewById(R.id.person_btn);
        img_help = (ImageView)findViewById(R.id.help_btn);



        pager = (ViewPager) findViewById(R.id.pager);
        LayoutInflater inflater = LayoutInflater.from(this);
        View tab01 = inflater.inflate(R.layout.book_lay, null);
        View tab02 = inflater.inflate(R.layout.home_lay, null);
        View tab03 = inflater.inflate(R.layout.report_lay, null);
        mViews.add(tab01);
        mViews.add(tab02);
        mViews.add(tab03);

        report_container = (LinearLayout)tab03.findViewById(R.id.report_container);

        no_config_lay = (LinearLayout)tab02.findViewById(R.id.no_config_lay);
        have_config_lay = (LinearLayout)tab02.findViewById(R.id.have_config_lay);
        except_tips = (TextView) tab02.findViewById(R.id.except_tips);

        img_book_save = (ImageView)tab01.findViewById(R.id.save_book_btn);
        book_startdate = (EditText)tab01.findViewById(R.id.book_startdate);
        book_stopdate = (EditText)tab01.findViewById(R.id.book_stopdate);

        Button pickDate = (Button) tab01.findViewById(R.id.but_showDate);
        Button pickDate2 = (Button) tab01.findViewById(R.id.but_showDate2);
        pickDate.setOnClickListener(this);
        pickDate2.setOnClickListener(this);



        mToggleButton01 = (ToggleButton)tab01.findViewById(R.id.mToggleButton01);
        mToggleButton01.setOnToggleChanged(new ToggleButton.OnToggleChanged() {
            @Override
            public void onToggle(boolean on) {
                if (on) {
                    Toast.makeText(MainActivity.this, "打开", Toast.LENGTH_SHORT).show();
                }else {
                    Toast.makeText(MainActivity.this, "默认关闭", Toast.LENGTH_SHORT).show();
                }
            }
        });

        // 注册添加配置 点击
        ImageView img_add_cfg = (ImageView)tab02.findViewById(R.id.img_add_cfg);
        if (img_add_cfg != null)
        {
            img_add_cfg.setOnClickListener(this);
        }

        /////////////////订阅数据列表/////////////////////
        customListView = (XListView) tab02.findViewById(R.id.xListView);
        customListView.setPullLoadEnable(true);
        mAdapterTest = new BookItemAdapter(this, items);
        customListView.setAdapter(mAdapterTest);
        customListView.setXListViewListener(this);
        customListView.setOnItemClickListener(this);


        mAdapter = new PagerAdapter() {
            @Override
            public boolean isViewFromObject(View arg0, Object arg1) {
                // TODO Auto-generated method stub
                return arg0 == arg1;
            }

            @Override
            public int getCount() {
                // TODO Auto-generated method stub
                return mViews.size();
            }

            // 需要复写的方法
            @Override
            public void destroyItem(ViewGroup container, int position,
                                    Object object) {
                container.removeView(mViews.get(position));
            }

            // 需要复写的方法
            @Override
            public Object instantiateItem(ViewGroup container, int position) {
                View view = mViews.get(position);

                container.addView(view);
                return view;
            }
        };
        pager.setCurrentItem(0);
        pager.setAdapter(mAdapter);
    }

    @Override
    public void onClick(View v) {
        resetImg();
        RelativeLayout layView = (RelativeLayout)findViewById(R.id.top_lay);
        switch (v.getId()) {
            case R.id.layout_book:
                pager.setCurrentItem(0);

                layView.setVisibility(View.GONE);
                img_book.setImageResource(R.drawable.book_pressed);

                // 加载订阅配置
                loadBookConfig();

                break;
            case R.id.layout_home:
                pager.setCurrentItem(1);
                // 设置主页文字显示
                setHomeTips();
                layView.setVisibility(View.VISIBLE);
                img_home.setImageResource(R.drawable.home_pressed);
                break;
            case R.id.layout_report:
                pager.setCurrentItem(2);

                layView.setVisibility(View.GONE);
                img_report.setImageResource(R.drawable.report_pressed);

                loadReportData();

                break;
            case R.id.img_add_cfg:
                pager.setCurrentItem(0);
                img_book.setImageResource(R.drawable.book_pressed);
//                showHomeWay(true);

                break;
            case R.id.person_btn:
                Intent iUserInfo = new Intent(MainActivity.this,UserInfoActivity.class);
                startActivity(iUserInfo);
                break;

            case  R.id.help_btn:
                Intent iHelp = new Intent(MainActivity.this,HelpActivity.class);
                startActivity(iHelp);
                break;

            case R.id.save_book_btn:
                showLoginingDlg();
                saveBookSetting();
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

    private void loadReportData()
    {
        LogUtil.info("Invoke loadReportData");
        final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.GET_NORMAL_REPORT);
        final Map<String,String> dataMap = new HashMap<>();

        try {
            JSONObject jsonObject = new JSONObject();

            jsonObject.put("Account",HsApplication.Global_App._myUserInfo._account);

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

    private void setHomeTips()
    {

        LinearLayout no_config_lay = mViews.get(1).findViewById(R.id.no_config_lay);
        LinearLayout have_config_lay = mViews.get(1).findViewById(R.id.have_config_lay);
        TextView except_tips = (TextView)no_config_lay.findViewById(R.id.except_tips);

        if (HsApplication.Global_App._myUserInfo._serviceOverTime)
        {
            except_tips.setText("您的服务已过期，请续费使用");
            no_config_lay.setVisibility(View.VISIBLE);
            have_config_lay.setVisibility(View.GONE);

            return;
        }

        if (HsApplication.Global_App._myUserInfo._myBookInfo == null ||
                (HsApplication.Global_App._myUserInfo._myBookInfo._key1 == null &&
                        HsApplication.Global_App._myUserInfo._myBookInfo._key3 == null &&
                        HsApplication.Global_App._myUserInfo._myBookInfo._key3 == null)
                || HsApplication.Global_App._myUserInfo._myBookInfo._enable == 0)
        {
            except_tips.setText("您未配置任何订阅信息,或您的配置已停用");
            no_config_lay.setVisibility(View.VISIBLE);
            have_config_lay.setVisibility(View.GONE);

            return;
        }

        have_config_lay.setVisibility(View.VISIBLE);
        no_config_lay.setVisibility(View.GONE);
    }

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

    MyBook newBookInfo;
    private void saveBookSetting()
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

    // 将所有图片切换为暗色
    private void resetImg() {
        img_book.setImageResource(R.drawable.book_normal);
        img_home.setImageResource(R.drawable.home_normal);
        img_report.setImageResource(R.drawable.report_normal);
    }

    private void showHomeWay(boolean haveCfg)
    {
        if (haveCfg)
        {
            no_config_lay.setVisibility(View.GONE);
            have_config_lay.setVisibility(View.VISIBLE);
        }
        else
        {
            no_config_lay.setVisibility(View.VISIBLE);
            have_config_lay.setVisibility(View.GONE);
        }
    }

    @Override
    public void onRefresh() {
        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {

//                _queryInfo.queryNextBathc();
                mAdapterTest.notifyDataSetInvalidated();

                onLoad();
            }
        }, 2000);
    }

    private void geneItems() {
        items.addAll(BookData.buildTestData(10));
    }

    @Override
    public void onLoadMore() {
        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                _queryInfo.queryNextBathc();

                mAdapter.notifyDataSetChanged();
                onLoad();
            }
        }, 2000);
    }

    private void onLoad() {
        customListView.stopRefresh();
        customListView.stopLoadMore();
        customListView.setRefreshTime("刚刚");
    }

    @Override
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l)
    {
        HsApplication.Global_App._currentBookInfo = (BookData)view.getTag();
        Intent iInfo = new Intent(MainActivity.this,BookDetailViewActivity.class);
        startActivity(iInfo);
    }

    private void registerComminucation() {
        // 主线程通信事件句柄
        mainHandlers = new Handler() {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case CommandDefine.SET_BOOK: {
                        String receiveData = msg.obj.toString();

                        JSONTokener jsonParser = new JSONTokener(receiveData);
                        try {
                            JSONObject person = (JSONObject) jsonParser.nextValue();

                            String errorinfo = person.getString("ErrorInfo");
                            int errorId = Integer.parseInt(person.getString("ErrorId"));
                            if (errorId == 200) {
                                closeLoginingDlg();// 关闭对话框
                                Toast.makeText(MainActivity.this, "操作成功", Toast.LENGTH_SHORT).show();
                                HsApplication.Global_App._myUserInfo._myBookInfo = newBookInfo;
                                break;
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
                    case CommandDefine.GET_BOOK: {
                        break;
                    }
                    case CommandDefine.PAY_FOR_SERVICE: {
                        break;
                    }
                    case CommandDefine.QUERY_DATA: {
                        String receiveData = msg.obj.toString();

                        JSONTokener jsonParser = new JSONTokener(receiveData);
                        try {
                            JSONObject person = (JSONObject) jsonParser.nextValue();

                            String errorinfo = person.getString("ErrorInfo");
                            int errorId = Integer.parseInt(person.getString("ErrorId"));
                            int currentCount = 0;
                            if (errorId == 200) {
                                JSONArray result = person.getJSONArray("Result");

                                for (int index = 0 ;index < result.length(); index ++)
                                {
                                    currentCount ++;

                                    JSONObject oneResult = result.getJSONObject(index);

                                    BookData oneData = BookData.pareFromJson(oneResult);

                                    if (oneData != null)
                                    {
                                        items.add(oneData);
                                    }
                                }

                                if (currentCount > 0)
                                {
                                    // 通知
                                    mAdapterTest.notifyDataSetInvalidated();
                                    onLoad();
                                }
                                else
                                {
                                    Toast.makeText(getApplicationContext(), "没有更多数据", Toast.LENGTH_LONG).show();
                                    _queryInfo.reduceQueryInfo();
                                }

                                break;
                            }
                            else
                            {
                                Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
                            }

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                        break;
                    }
                    case CommandDefine.GET_NORMAL_REPORT: {
                        showReportData(msg);
                        break;
                    }

                    case CommandDefine.SET_USERINFO: {
                        break;
                    }
                    case CommandDefine.GET_USERINFO: {
                        break;
                    }
                }
            }
        };
    }

    private void showReportData(Message msg) {
        String receiveData = msg.obj.toString();

        HsApplication.Global_App._currentReportData = new ArrayList<>();

        JSONTokener jsonParser = new JSONTokener(receiveData);
        try {
            JSONObject person = (JSONObject) jsonParser.nextValue();

            String errorinfo = person.getString("ErrorInfo");
            int errorId = Integer.parseInt(person.getString("ErrorId"));
            int currentCount = 0;
            if (errorId == 200) {
                JSONArray result = person.getJSONArray("Result");

                for (int index = 0 ;index < result.length(); index ++)
                {
                    ReportObject oneObj = new ReportObject();
                    oneObj.parseFromJosn(result.getJSONObject(index));

                    HsApplication.Global_App._currentReportData.add(oneObj);
                }
            }
            else
            {
                Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
            }



        } catch (JSONException e) {
            e.printStackTrace();
        }

        //
        if (HsApplication.Global_App._currentReportData == null || HsApplication.Global_App._currentReportData.size() == 0)
        {
            return;
        }

        for (int index  = 0 ; index < HsApplication.Global_App._currentReportData.size(); index ++)
        {
            ReportObject oneReport = HsApplication.Global_App._currentReportData.get(index);
            if (oneReport.type == 0) // 线图
            {
                LayoutInflater inflater = (LayoutInflater) MainActivity.this.getSystemService(
                        Context.LAYOUT_INFLATER_SERVICE);
                LinearLayout dynmacReportView = (LinearLayout) inflater.inflate(
                        R.layout.report_line, null);
                report_container.addView(dynmacReportView);
                LinearLayout.LayoutParams mLayoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, DisplayUtils.dip2px(this,270));
                dynmacReportView.setLayoutParams(mLayoutParams);

                //设置报表名称
                TextView report_name = (TextView)dynmacReportView.findViewById(R.id.report_name);
                report_name.setText(oneReport.reportName);

                // 按星期查看客户销售习惯分布
                int dotCount = oneReport.collectionDatas.size();
                LineChartView line_chart = (LineChartView) dynmacReportView.findViewById(R.id.line_chart);
                line_chart.setOnValueTouchListener(new LineValueTouchListener());

                LineReportService rs = new LineReportService(line_chart);
                rs.getAxisXLables(oneReport);//获取x轴的标注
                rs.getAxisPoints(oneReport);//获取坐标点
                rs.initLineChart();//初始化

            }
            else if(oneReport.type == 1) // 饼图
            {
//                LayoutInflater inflater = (LayoutInflater) MainActivity.this.getSystemService(
//                        Context.LAYOUT_INFLATER_SERVICE);
//                LinearLayout dynmacReportView = (LinearLayout) inflater.inflate(
//                        R.layout.report_pie, null);
//                report_container.addView(dynmacReportView);
//                LinearLayout.LayoutParams mLayoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, DisplayUtils.dip2px(this,270));
//                dynmacReportView.setLayoutParams(mLayoutParams);
//                //设置报表名称
//                TextView report_name = (TextView)dynmacReportView.findViewById(R.id.report_name);
//                report_name.setText(oneReport.reportName);
//
//                PieChartView pie_chart = (PieChartView) this.findViewById(R.id.pie_chart);
//                pie_chart.setOnValueTouchListener(new ValueTouchListener());
//                PieChartData pie_chart_data = LineReportService.generatePieData(oneReport);
//                pie_chart.setPieChartData(pie_chart_data);
            }
        }


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

    //////////////////////////////////报表相关//////////////////////////////////////////////////
    private class ValueTouchListener implements PieChartOnValueSelectListener {
        @Override
        public void onValueSelected(int arcIndex, SliceValue value) {
            Toast.makeText(MainActivity.this, "Selected: " + value, Toast.LENGTH_SHORT).show();
        }

        @Override
        public void onValueDeselected() {
            // TODO Auto-generated method stub

        }
    }

    private class LineValueTouchListener implements LineChartOnValueSelectListener {

        @Override
        public void onValueSelected(int lineIndex, int pointIndex, PointValue value)
        {
//            ReportData clickData = null;
            Toast.makeText(MainActivity.this, "Selected: " + value, Toast.LENGTH_SHORT).show();
        }
        @Override
        public void onValueDeselected() {
        }

    }
    private class LineValueCustomTouchListener implements LineChartOnValueSelectListener {

        @Override
        public void onValueSelected(int lineIndex, int pointIndex, PointValue value)
        {
            Toast.makeText(MainActivity.this, "", Toast.LENGTH_LONG).show();
        }
        @Override
        public void onValueDeselected() {
        }

    }
}
