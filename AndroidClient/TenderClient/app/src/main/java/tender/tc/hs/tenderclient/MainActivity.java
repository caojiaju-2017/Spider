package tender.tc.hs.tenderclient;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;


import android.app.Activity;
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
import android.widget.BaseAdapter;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.GridView;
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
import lecho.lib.hellocharts.model.PointValue;
import lecho.lib.hellocharts.model.SliceValue;
import lecho.lib.hellocharts.view.ColumnChartView;
import lecho.lib.hellocharts.view.LineChartView;
import lecho.lib.hellocharts.view.PieChartView;
import tender.tc.hs.tenderclient.Data.BookData;
import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.HsListView.BookItemAdapter;
import tender.tc.hs.tenderclient.HsListView.DisplayUtils;
import tender.tc.hs.tenderclient.HsListView.QueryInfo;
import tender.tc.hs.tenderclient.HsListView.XListView;
import tender.tc.hs.tenderclient.Report.ColumnReportService;
import tender.tc.hs.tenderclient.Report.PieReportService;
import tender.tc.hs.tenderclient.Report.ReportObject;
import tender.tc.hs.tenderclient.Report.LineReportService;
import tender.tc.hs.tenderclient.Util.LogUtil;
import tender.tc.hs.tenderclient.wxapi.WXEntryActivity;

public class MainActivity extends Activity implements OnClickListener ,
        AdapterView.OnItemClickListener,
        XListView.IXListViewListener
{
    LinearLayout layout_home;
    LinearLayout layout_report;
    LinearLayout layout_user;

    RelativeLayout layout_book_view;
    RelativeLayout layout_userinfo_view;

    ViewPager pager;
    PagerAdapter mAdapter;
    private List<View> mViews = new ArrayList<View>();

    private ImageView img_set;
    private ImageView img_home;
    private ImageView img_report;

    private ImageView img_help;

//    private  Button img_book_save;


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
//    ToggleButton mToggleButton01;

    TextView except_tips;

    QueryInfo _queryInfo ;

    RelativeLayout help_infoview ;
//    Button saveUserInfo ;

    ImageView img_add_cfg;
    private GridView gview;
    private FrameLayout framView;
    private String[] iconName = { "通讯录", "照相机","浏览器", "视频频",
            "通讯录", "照相机","浏览器", "视频频" };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.main_activity);

        // 注册主线程通信句柄
        registerComminucation();


        //
        mHandler = new Handler();

//        geneItems();

        initView();
        initEvent();

        layout_home.performClick();

        _queryInfo = new QueryInfo(mainHandlers);

        if (setHomeTips())
        {
            HsProgressDialog.newProgressDlg().show(MainActivity.this);
            _queryInfo.queryNextBathc();
        }

        framView = (FrameLayout)findViewById(R.id.fragmentView);
//        data_list = new ArrayList<Map<String, Object>>();
//        // 测试
        gview = (GridView) findViewById(R.id.gview);

        List<String> abc = new ArrayList<>();
        for (int index = 0 ; index < iconName.length; index++)
        {
            abc.add(iconName[index]);
        }
        GridViewAdapter gridViewAdapter = new GridViewAdapter();
        gview.setAdapter(gridViewAdapter);
        // 为GridView设定监听器
        gview.setOnItemClickListener(new gridViewListener());



//        //获取数据
//        getData();
//        //新建适配器
//        String [] from ={"image","text"};
//        int [] to = {R.id.image,R.id.text};
//        SimpleAdapter sim_adapter = new SimpleAdapter(this, data_list, R.layout.item, from, to);
//        //配置适配器
//        gview.setAdapter(sim_adapter);
    }
    class gridViewListener implements AdapterView.OnItemClickListener {

        @Override
        public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
                                long arg3) {
            // TODO Auto-generated method stub
            System.out.println("arg2 = " + arg2); // 打印出点击的位置
        }
    }

    class GridViewAdapter extends BaseAdapter {

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {

            LayoutInflater mInflater = LayoutInflater.from(MainActivity.this);
            ItemViewTag viewTag;

            if (convertView == null)
            {
                convertView = mInflater.inflate(R.layout.province_item, null);

                // construct an item tag
                viewTag = new ItemViewTag((TextView) convertView.findViewById(R.id.prov_name));
                convertView.setTag(viewTag);
            } else
            {
                viewTag = (ItemViewTag) convertView.getTag();
            }

            // set name
            viewTag.mName.setText(iconName[position]);

            // set icon
//            viewTag.mIcon.setBackgroundDrawable(mDrawableList.get(position));
//            viewTag.mIcon.setLayoutParams(params);
            return convertView;




//            View tab01 = inflater.inflate(R.layout.province_item, null);
//
//            TextView txtview ;
//            if (convertView == null) {
//                txtview = (TextView)tab01.findViewById(R.id.prov_name);
//                //imageview.setScaleType(ImageView.ScaleType.CENTER_INSIDE); // 设置缩放方式
////                txtview.setPadding(5, 0, 5, 0); // 设置ImageView的内边距
//            } else {
//                txtview = (TextView) convertView;
//            }
//
//            txtview.setText(iconName[position]); // 为ImageView设置要显示的图片
//            return tab01; // 返回ImageView
        }

        /*
         * 功能：获得当前选项的ID
         *
         * @see android.widget.Adapter#getItemId(int)
         */
        @Override
        public long getItemId(int position) {
            //System.out.println("getItemId = " + position);
            return position;
        }

        /*
         * 功能：获得当前选项
         *
         * @see android.widget.Adapter#getItem(int)
         */
        @Override
        public Object getItem(int position) {
            return position;
        }

        /*
         * 获得数量
         *
         * @see android.widget.Adapter#getCount()
         */
        @Override
        public int getCount() {
            return iconName.length;
        }

        class ItemViewTag
        {

            protected TextView mName;


            public ItemViewTag(TextView name)
            {
                this.mName = name;

            }
        }
    }


//    private void loadBookConfig() {
//        if (HsApplication.Global_App._myUserInfo._myBookInfo == null)
//        {
//            return;
//        }
//
//        EditText emailTxt = (EditText)findViewById(R.id.book_email);
//        EditText phoneTxt = (EditText)findViewById(R.id.book_phone);
//        EditText startDateTxt = (EditText)findViewById(R.id.book_startdate);
//        EditText stopDateTxt = (EditText)findViewById(R.id.book_stopdate);
//        EditText bookKey1Txt = (EditText)findViewById(R.id.book_key1);
//        EditText bookKey2Txt = (EditText)findViewById(R.id.book_key2);
//        EditText bookKey3Txt = (EditText)findViewById(R.id.book_key3);
//
//        emailTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._email);
//        phoneTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._phone);
//        startDateTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._startDate);
//        stopDateTxt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._stopDate);
//        bookKey1Txt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._key1);
//        bookKey2Txt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._key2);
//        bookKey3Txt.setText(HsApplication.Global_App._myUserInfo._myBookInfo._key3);
//
////        if (HsApplication.Global_App._myUserInfo._myBookInfo._enable == 0)
////        {
////            mToggleButton01.setToggleOff(true);
////        }
////        else
////        {
////            mToggleButton01.setToggleOn(true);
////        }
//    }

    // 事件初始化
    private void initEvent() {
//        layout_book.setOnClickListener(this);
        layout_home.setOnClickListener(this);
        layout_report.setOnClickListener(this);
        layout_user.setOnClickListener(this);

        img_help.setOnClickListener(this);
//        img_person.setOnClickListener(this);

//        img_book_save.setOnClickListener(this);

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
                        img_report.setImageResource(R.drawable.report_pressed);
                        img_help.setVisibility(View.INVISIBLE);
                        break;
                    case 1:
                        setHomeTips();
                        img_home.setImageResource(R.drawable.home_pressed);
                        img_help.setVisibility(View.VISIBLE);
                        break;
                    case 2:
                        img_set.setImageResource(R.drawable.book_pressed);
                        img_help.setVisibility(View.INVISIBLE);
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
//        layout_book = (LinearLayout) findViewById(R.id.layout_book);
        layout_home = (LinearLayout) findViewById(R.id.layout_home);
        layout_report = (LinearLayout) findViewById(R.id.layout_report);
        layout_user = (LinearLayout) findViewById(R.id.layout_my);

        img_set = (ImageView) findViewById(R.id.img_my);
        img_home = (ImageView) findViewById(R.id.img_home);
        img_report = (ImageView) findViewById(R.id.img_report);

//        img_person = (ImageView)findViewById(R.id.person_btn);
        img_help = (ImageView)findViewById(R.id.help_btn);



        pager = (ViewPager) findViewById(R.id.pager);
        LayoutInflater inflater = LayoutInflater.from(this);
//        View tab01 = inflater.inflate(R.layout.book_lay, null);
        View tab02 = inflater.inflate(R.layout.home_lay, null);
        View tab03 = inflater.inflate(R.layout.report_lay, null);
        View tab04 = inflater.inflate(R.layout.user_info_lay, null);
//        mViews.add(tab01);
        mViews.add(tab03);
        mViews.add(tab02);
        mViews.add(tab04);

        TextView service_t_date = (TextView)tab04.findViewById(R.id.service_t_date);
        service_t_date.setText(HsApplication.Global_App._myUserInfo._serviceOverDate);
        layout_book_view = (RelativeLayout)tab04.findViewById(R.id.user_infoview);
        layout_userinfo_view = (RelativeLayout)tab04.findViewById(R.id.book_infoview);
        help_infoview = (RelativeLayout)tab04.findViewById(R.id.help_infoview);

//        saveUserInfo = tab04.findViewById(R.id.save_change);
        report_container = (LinearLayout)tab03.findViewById(R.id.report_container);

        no_config_lay = (LinearLayout)tab02.findViewById(R.id.no_config_lay);
        have_config_lay = (LinearLayout)tab02.findViewById(R.id.have_config_lay);
        except_tips = (TextView) tab02.findViewById(R.id.except_tips);

//        img_book_save = (Button)tab01.findViewById(R.id.save_book_btn);
//        book_startdate = (EditText)tab01.findViewById(R.id.book_startdate);
//        book_stopdate = (EditText)tab01.findViewById(R.id.book_stopdate);
//
//        Button pickDate = (Button) tab01.findViewById(R.id.but_showDate);
//        Button pickDate2 = (Button) tab01.findViewById(R.id.but_showDate2);
//        pickDate.setOnClickListener(this);
//        pickDate2.setOnClickListener(this);



//        mToggleButton01 = (ToggleButton)tab01.findViewById(R.id.mToggleButton01);
//        mToggleButton01.setOnToggleChanged(new ToggleButton.OnToggleChanged() {
//            @Override
//            public void onToggle(boolean on) {
//                if (on) {
//                    Toast.makeText(MainActivity.this, "打开", Toast.LENGTH_SHORT).show();
//                }else {
//                    Toast.makeText(MainActivity.this, "关闭", Toast.LENGTH_SHORT).show();
//                }
//            }
//        });

        // 注册添加配置 点击
        img_add_cfg = (ImageView)tab02.findViewById(R.id.img_add_cfg);
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
//        saveUserInfo.setOnClickListener(this);

        layout_book_view.setOnClickListener(this);
        layout_userinfo_view.setOnClickListener(this);
        help_infoview.setOnClickListener(this);


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

        RelativeLayout layView = (RelativeLayout)findViewById(R.id.top_lay);
        switch (v.getId()) {
//            case R.id.layout_book:
//                pager.setCurrentItem(0);
//
//                layView.setVisibility(View.VISIBLE);
//                img_set.setImageResource(R.drawable.book_pressed);
//
//                // 加载订阅配置
//                loadBookConfig();
//
//                break;
            case R.id.layout_home:
                resetImg();
                pager.setCurrentItem(1);
                // 设置主页文字显示
                setHomeTips();
                layView.setVisibility(View.VISIBLE);
                img_help.setVisibility(View.VISIBLE);
                img_home.setImageResource(R.drawable.home_pressed);
                break;
            case R.id.layout_report:
                resetImg();
                pager.setCurrentItem(0);

                layView.setVisibility(View.VISIBLE);
                img_help.setVisibility(View.INVISIBLE);

                img_report.setImageResource(R.drawable.report_pressed);

                if (setHomeTips())
                {
                    HsProgressDialog.newProgressDlg().show(MainActivity.this);
                    loadReportData();
                }
                else
                {
                    Toast.makeText(MainActivity.this, "您的服务已过期或未设置任何可用订阅项，你可通过个人资料查看服务到期日", Toast.LENGTH_SHORT).show();
                }

                break;
            case R.id.layout_my:
                resetImg();
                pager.setCurrentItem(2);
                layView.setVisibility(View.VISIBLE);
                img_help.setVisibility(View.INVISIBLE);
//                loadUserInfo();

                img_set.setImageResource(R.drawable.book_pressed);
                break;

            case R.id.img_add_cfg:
                if (exceptionType == 2)
                {
                    Intent iUserBookInfo = new Intent(MainActivity.this,UserBookSettingActivity.class);
                    startActivity(iUserBookInfo);
                }
                else if(exceptionType == 1)
                {
                    pager.setCurrentItem(2);
                    img_set.setImageResource(R.drawable.book_pressed);
                    showHomeWay(true);
                }

                break;

            case  R.id.help_btn:
//                Intent iHelp = new Intent(MainActivity.this,HelpActivity.class);
//                startActivity(iHelp);
                if (framView.getVisibility() == View.VISIBLE)
                {
                    framView.setVisibility(View.GONE);
                }
                else
                {
                    framView.setVisibility(View.VISIBLE);
                }
                break;

            case R.id.user_infoview:
                Intent iUserInfo = new Intent(MainActivity.this,UserInfoActivity.class);
                startActivity(iUserInfo);
                break;

            case R.id.book_infoview:
                Intent iUserBookInfo = new Intent(MainActivity.this,UserBookSettingActivity.class);
                startActivity(iUserBookInfo);
                break;

            case  R.id.help_infoview:
                Intent iUserHelpInfo = new Intent(MainActivity.this,HelpActivity.class);
                startActivity(iUserHelpInfo);
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

    int exceptionType =  0;
    private boolean setHomeTips()
    {
        exceptionType = 0;
        LinearLayout no_config_lay = mViews.get(1).findViewById(R.id.no_config_lay);
        LinearLayout have_config_lay = mViews.get(1).findViewById(R.id.have_config_lay);
        TextView except_tips = (TextView)no_config_lay.findViewById(R.id.except_tips);

        if (HsApplication.Global_App._myUserInfo._serviceOverTime)
        {
            except_tips.setText("您的服务已过期，请续费使用");
            img_add_cfg.setImageResource(R.drawable.repay);
            no_config_lay.setVisibility(View.VISIBLE);
            have_config_lay.setVisibility(View.GONE);
            exceptionType = 1;
            return false;
        }

        if (HsApplication.Global_App._myUserInfo._myBookInfo == null ||
                (HsApplication.Global_App._myUserInfo._myBookInfo._key1 == null &&
                        HsApplication.Global_App._myUserInfo._myBookInfo._key3 == null &&
                        HsApplication.Global_App._myUserInfo._myBookInfo._key3 == null)
                || HsApplication.Global_App._myUserInfo._myBookInfo._enable == 0)
        {
            except_tips.setText("您未配置任何订阅信息,或您的配置已停用");
            img_add_cfg.setImageResource(R.drawable.add_config);
            no_config_lay.setVisibility(View.VISIBLE);
            have_config_lay.setVisibility(View.GONE);
            exceptionType = 2;
            return false;
        }

        have_config_lay.setVisibility(View.VISIBLE);
        no_config_lay.setVisibility(View.GONE);

        return true;
    }



//
//    private void saveBookSetting()
//    {
//
//    }

    // 将所有图片切换为暗色
    private void resetImg() {
        img_set.setImageResource(R.drawable.book_normal);
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
        Intent iInfo = new Intent(MainActivity.this,WXEntryActivity.class);
        startActivity(iInfo);
    }

    private void registerComminucation() {
        // 主线程通信事件句柄
        mainHandlers = new Handler() {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case CommandDefine.PAY_FOR_SERVICE: {
                        break;
                    }
                    case CommandDefine.QUERY_DATA: {
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
                        try {
                            HsProgressDialog.getProgressDlg().Close();
                        } catch (Exception e) {
                            // TODO: handle exception
                        }
                        showReportData(msg);
                        break;
                    }

                    case CommandDefine.GET_USERINFO: {
                        break;
                    }
//
//                    case CommandDefine.SET_USERINFO:
//                    {
//                        try {
//                            HsProgressDialog.getProgressDlg().Close();
//                        } catch (Exception e) {
//                            // TODO: handle exception
//                        }
//
//                        String receiveData = msg.obj.toString();
//
//                        JSONTokener jsonParser = new JSONTokener(receiveData);
//                        try {
//                            JSONObject person = (JSONObject) jsonParser.nextValue();
//
//                            String errorinfo = person.getString("ErrorInfo");
//                            int errorId = Integer.parseInt(person.getString("ErrorId"));
//                            if (errorId == 200) {
//
//                                Toast.makeText(MainActivity.this, "操作成功", Toast.LENGTH_SHORT).show();
//                                updateUserInfo();
//
////                                finish();
//                                break;
//                            }
//                            else
//                            {
////                                closeLoginingDlg();// 关闭对话框
//                                Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
//                            }
//
//                        } catch (JSONException e) {
//                            e.printStackTrace();
//                        }
//                        break;
//                    }
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

        report_container.removeAllViews();
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
                report_name.setText(oneReport.reportTitle);

                // 按星期查看客户销售习惯分布
                LineChartView line_chart = (LineChartView) dynmacReportView.findViewById(R.id.line_chart);
                line_chart.setOnValueTouchListener(new LineValueTouchListener());

                LineReportService rs = new LineReportService(line_chart);
                rs.getAxisXLables(oneReport);//获取x轴的标注
                rs.getAxisPoints(oneReport);//获取坐标点
                rs.initLineChart(oneReport);//初始化

            }
            else if(oneReport.type == 1) // 饼图
            {
                LayoutInflater inflater = (LayoutInflater) MainActivity.this.getSystemService(
                        Context.LAYOUT_INFLATER_SERVICE);
                LinearLayout dynmacReportView = (LinearLayout) inflater.inflate(
                        R.layout.report_pie, null);
                report_container.addView(dynmacReportView);
                LinearLayout.LayoutParams mLayoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, DisplayUtils.dip2px(this,270));
                dynmacReportView.setLayoutParams(mLayoutParams);
                //设置报表名称
                TextView report_name = (TextView)dynmacReportView.findViewById(R.id.report_name);
                report_name.setText(oneReport.reportTitle);

                PieChartView pie_chart = (PieChartView) this.findViewById(R.id.pie_chart);
                pie_chart.setOnValueTouchListener(new ValueTouchListener());

                PieReportService pieReportService = new PieReportService(pie_chart);
                pieReportService.setPieChartData(oneReport);
                pieReportService.initPieChart(oneReport);
            }
            else if (oneReport.type == 2)
            {
                LayoutInflater inflater = (LayoutInflater) MainActivity.this.getSystemService(
                        Context.LAYOUT_INFLATER_SERVICE);
                LinearLayout dynmacReportView = (LinearLayout) inflater.inflate(
                        R.layout.report_column, null);
                report_container.addView(dynmacReportView);
                LinearLayout.LayoutParams mLayoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, DisplayUtils.dip2px(this,270));
                dynmacReportView.setLayoutParams(mLayoutParams);
                //设置报表名称
                TextView report_name = (TextView)dynmacReportView.findViewById(R.id.report_name);
                report_name.setText(oneReport.reportTitle);

                ColumnChartView column_chart = (ColumnChartView) this.findViewById(R.id.column_chart);
//                column_chart.setOnValueTouchListener(new ValueTouchListener());
                column_chart.setZoomEnabled(false);//禁止手势缩放

                ColumnReportService columnReportService = new ColumnReportService(column_chart);

                columnReportService.setData(oneReport);
                columnReportService.initChart(oneReport);
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
//
//    private void loadUserInfo() {
//        EditText et_account = (EditText)this.findViewById(R.id.et_account);
//        EditText et_email = (EditText)this.findViewById(R.id.et_email);
//        EditText china_name = (EditText)this.findViewById(R.id.china_name);
//        EditText custom_address = (EditText)this.findViewById(R.id.custom_address);
//        EditText org_name = (EditText)this.findViewById(R.id.org_name);
//        EditText service_t_date = (EditText)this.findViewById(R.id.service_t_date);
//
//        et_account.setText(HsApplication.Global_App._myUserInfo._account);
//        et_email.setText(HsApplication.Global_App._myUserInfo._email);
//        china_name.setText(HsApplication.Global_App._myUserInfo._Alias);
//        custom_address.setText(HsApplication.Global_App._myUserInfo._Address);
//        org_name.setText(HsApplication.Global_App._myUserInfo._orgName);
//        service_t_date.setText(HsApplication.Global_App._myUserInfo._serviceOverDate);
//    }
//    UserInfo _updateUserInfo = new UserInfo();
//    private void saveChange()
//    {
//        LogUtil.info("Invoke saveBookSetting");
//        final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.SET_USERINFO);
//        final Map<String,String> dataMap = new HashMap<>();
//
//        try {
//            JSONObject jsonObject = new JSONObject();
//
//            EditText et_account = (EditText)this.findViewById(R.id.et_account);
//            EditText et_email = (EditText)this.findViewById(R.id.et_email);
//            EditText china_name = (EditText)this.findViewById(R.id.china_name);
//            EditText custom_address = (EditText)this.findViewById(R.id.custom_address);
//            EditText org_name = (EditText)this.findViewById(R.id.org_name);
//            EditText service_t_date = (EditText)this.findViewById(R.id.service_t_date);
//
//            String email = et_email.getText().toString();
//            String phone = et_account.getText().toString();
//            String alias = china_name.getText().toString();
//            String address = custom_address.getText().toString();
//            String orgname = org_name.getText().toString();
//
//
//            jsonObject.put("Account",HsApplication.Global_App._myUserInfo._account);
//            jsonObject.put("EMail",email);
//            jsonObject.put("Alias",alias);
//            jsonObject.put("Address",address);
//            jsonObject.put("OrgName",orgname);
//
//
//            _updateUserInfo = new UserInfo();
//            _updateUserInfo._email = email;
//            _updateUserInfo._Address = address;
//            _updateUserInfo._Alias = alias;
//            _updateUserInfo._orgName = orgname;
//
//
//            access.setJsonObject(jsonObject);
//        } catch (JSONException e) {
//            LogUtil.info("Invoke httpaccess prepare failed");
//            e.printStackTrace();
//        }
//        new Thread(new Runnable(){
//            public void run()
//            {
//                access.HttpPost();
//            }
//        }).start();
//    }
//
//    private void updateUserInfo() {
//        if (_updateUserInfo == null)
//        {
//
//        }
//        HsApplication.Global_App._myUserInfo._email = _updateUserInfo._email;
//        HsApplication.Global_App._myUserInfo._Address = _updateUserInfo._Address;
//        HsApplication.Global_App._myUserInfo._orgName = _updateUserInfo._orgName;
//        HsApplication.Global_App._myUserInfo._Alias = _updateUserInfo._Alias;
//
//    }
}
