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
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.huawei.android.sdk.drm.Drm;
import com.huawei.android.sdk.drm.DrmCheckCallback;

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
    // 版权保护id
    private static final String DRM_ID = "890086000102073545";
    // 版权保护公钥
    private static final String DRM_PUBLIC_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtaBaEJyDn+z9iMNRIcPkk+HIjL+did7bXywlZ5A+dyDCOoG+Fu9A9p2KKLmZiGn27/vb495I4YfoklIHY0TbSm81YllAaEXt6rxW9Y918ijyQMdTdDOJ6jkun0aMC06huFDSX1ZSOm4aRpHtLvEEwwzKwcfeD1X62IbsQyDnM26i0SU2Z9HKK2/ygeorkAksA176UntyvjpDBKYYmqTWpwV0jVVuxkb9ogzkeNEuIdhfMBlWaoTsq7XkDn8Ccqj7HhICwp0moOPgcSGxKHyoc6X/GvfC9wXTXCvKNskkzwbWaB9CHO4Y0SZnIzkWCrd4IEDASEp4WQziAvGaElcWowIDAQAB";


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

    TextView except_tips;

    QueryInfo _queryInfo ;

    RelativeLayout help_infoview ;

    Button pay_btn;

    ImageView img_add_cfg;
    private GridView gview;
    private FrameLayout framView;
    private String[] iconName = { "山东省", "四川省","福建省", "山西省",
            "陕西省", "广东省","江苏省" ,"河南省","贵州省","河北省","广西"};


    private String currentProvince = null;
    private ImageView currentSelectImage = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title


        // 调用鉴权方法
        Drm.check(this, this.getPackageName(), DRM_ID, DRM_PUBLIC_KEY,
                new MainActivity.MyDrmCheckCallback());
    }

    /**
     * 鉴权结果处理回调接口
     */
    private class MyDrmCheckCallback implements DrmCheckCallback {

        @Override
        public void onCheckSuccess() {
            // 鉴权成功，用户继续使用程序。
            setContentView(R.layout.main_activity);

            // 注册主线程通信句柄
            registerComminucation();
            //
            mHandler = new Handler();
            initView();
            initEvent();

            layout_home.performClick();
            _queryInfo = new QueryInfo(mainHandlers);

            if (setHomeTips())
            {
                HsProgressDialog.newProgressDlg().show(MainActivity.this);
                _queryInfo.queryNextBatchByProvince(currentProvince);
            }

            /// 配置过滤相关
            framView = (FrameLayout)findViewById(R.id.fragmentView);
            gview = (GridView) findViewById(R.id.gview);
            GridViewAdapter gridViewAdapter = new GridViewAdapter();
            gview.setAdapter(gridViewAdapter);
            // 为GridView设定监听器
            gview.setOnItemClickListener(new gridViewListener());
        }

        @Override
        public void onCheckFailed() {
            // 鉴权失败，用户不能使用程序，程序退出。
            finish();
        }
    }

    class gridViewListener implements AdapterView.OnItemClickListener {
        @Override
        public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
                                long arg3) {
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

                convertView.setOnClickListener(new OnClickListener() {

                    public void onClick(View v)
                    {
                        framView.setVisibility(View.GONE);
                        // 获取当前选中的省
                        TextView textView = v.findViewById(R.id.prov_name);
                        ImageView imgView = v.findViewById(R.id.select_province);

                        if (textView == null)
                        {
                            return;
                        }
                        String prvName = textView.getText().toString();

                        // 比较与当前省是否一致，
                        if (currentProvince != null && currentProvince.equals(prvName))
                        {
                            imgView.setVisibility(View.GONE);
                            currentSelectImage.setVisibility(View.GONE);
                            currentProvince = null;
                            currentSelectImage = null;
                        }
                        else if(currentProvince == null)
                        {
                            currentProvince = prvName;
                            currentSelectImage = imgView;
                            imgView.setVisibility(View.VISIBLE);
                            currentSelectImage.setVisibility(View.VISIBLE);
                        }
                        else
                        {
                            currentProvince = prvName;
                            imgView.setVisibility(View.VISIBLE);
                            currentSelectImage.setVisibility(View.GONE);
                            currentSelectImage = imgView;
                        }

                        Message msg = new Message();
                        msg.what = CommandDefine.REFRESH_QUERY;
                        mainHandlers.sendMessage(msg);
                    }
                });
                viewTag = new ItemViewTag((TextView) convertView.findViewById(R.id.prov_name), (ImageView)convertView.findViewById(R.id.select_province));
                convertView.setTag(viewTag);
            }
            else
            {
                viewTag = (ItemViewTag) convertView.getTag();
            }

            // set name
            viewTag.mName.setText(iconName[position]);

            return convertView;
        }

        /*
         * 功能：获得当前选项的ID
         *
         * @see android.widget.Adapter#getItemId(int)
         */
        @Override
        public long getItemId(int position) {
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
            protected ImageView mImage;


            public ItemViewTag(TextView name, ImageView img)
            {
                this.mName = name;
                this.mImage = img;
            }

            public boolean checkProvinceSame(String currentProvince)
            {
                if (mName == null)
                {
                    return false;
                }

                if (mName.equals(currentProvince))
                {
                    return true;
                }

                return  false;
            }

            public void setImageVisible(boolean flag) {
                if (mImage == null)
                {
                    return;
                }

                mImage.setVisibility(flag ? View.VISIBLE:View.GONE);
            }
        }
    }

    // 事件初始化
    private void initEvent() {
        layout_home.setOnClickListener(this);
        layout_report.setOnClickListener(this);
        layout_user.setOnClickListener(this);

        img_help.setOnClickListener(this);
        // 最新的api setOnPageChangeListener 已更换为addOnPageChangeListener
        pager.addOnPageChangeListener(new OnPageChangeListener() {
            @Override
            public void onPageSelected(int arg0) {
                int currentItem = pager.getCurrentItem();

                if (framView != null)
                {
                    framView.setVisibility(View.GONE);
                }

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
        layout_home = (LinearLayout) findViewById(R.id.layout_home);
        layout_report = (LinearLayout) findViewById(R.id.layout_report);
        layout_user = (LinearLayout) findViewById(R.id.layout_my);

        img_set = (ImageView) findViewById(R.id.img_my);
        img_home = (ImageView) findViewById(R.id.img_home);
        img_report = (ImageView) findViewById(R.id.img_report);

        img_help = (ImageView)findViewById(R.id.help_btn);



        pager = (ViewPager) findViewById(R.id.pager);
        LayoutInflater inflater = LayoutInflater.from(this);
        View tab02 = inflater.inflate(R.layout.home_lay, null);
        View tab03 = inflater.inflate(R.layout.report_lay, null);
        View tab04 = inflater.inflate(R.layout.user_info_lay, null);
        mViews.add(tab03);
        mViews.add(tab02);
        mViews.add(tab04);

        TextView service_t_date = (TextView)tab04.findViewById(R.id.service_t_date);
        service_t_date.setText(HsApplication.Global_App._myUserInfo._serviceOverDate);
        layout_book_view = (RelativeLayout)tab04.findViewById(R.id.user_infoview);
        layout_userinfo_view = (RelativeLayout)tab04.findViewById(R.id.book_infoview);
        help_infoview = (RelativeLayout)tab04.findViewById(R.id.help_infoview);
        pay_btn = (Button)tab04.findViewById(R.id.pay_btn);

        report_container = (LinearLayout)tab03.findViewById(R.id.report_container);

        no_config_lay = (LinearLayout)tab02.findViewById(R.id.no_config_lay);
        have_config_lay = (LinearLayout)tab02.findViewById(R.id.have_config_lay);
        except_tips = (TextView) tab02.findViewById(R.id.except_tips);

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
        pay_btn.setOnClickListener(this);

        // 处理滑动事件， 如果有触摸则，关闭过滤框框
        customListView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN:
                        // 触摸按下时的操作
                        if (framView != null)
                        {
                            framView.setVisibility(View.GONE);
                        }
                        break;
                    case MotionEvent.ACTION_MOVE:
                        // 触摸移动时的操作
                        if (framView != null)
                        {
                            framView.setVisibility(View.GONE);
                        }
                        break;
                    case MotionEvent.ACTION_UP:
                        // 触摸抬起时的操作
                        if (framView != null)
                        {
                            framView.setVisibility(View.GONE);
                        }
                        break;
                }
                return false;
            }
        });

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
            case R.id.layout_home:
                resetImg();
                pager.setCurrentItem(1);
                // 设置主页文字显示
                setHomeTips();
                layView.setVisibility(View.VISIBLE);
                img_help.setVisibility(View.VISIBLE);
                if (framView != null)
                {
                    framView.setVisibility(View.GONE);
                }
                img_home.setImageResource(R.drawable.home_pressed);
                break;
            case R.id.layout_report:
                resetImg();
                pager.setCurrentItem(0);

                layView.setVisibility(View.VISIBLE);
                img_help.setVisibility(View.INVISIBLE);
                if (framView != null)
                {
                    framView.setVisibility(View.GONE);
                }
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
                if (framView != null)
                {
                    framView.setVisibility(View.GONE);
                }
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

            case  R.id.pay_btn:
                Intent chageAct = new Intent(MainActivity.this,ChageFeeActivity.class);
                startActivity(chageAct);
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
                _queryInfo.queryNextBatchByProvince(currentProvince);

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
        if (framView != null)
        {
            framView.setVisibility(View.GONE);

        }

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

                    case CommandDefine.REFRESH_QUERY: {
                        refreshQuery();
                        break;
                    }
                }
            }
        };
    }

    private void refreshQuery()
    {
        // 启动进度条
        HsProgressDialog.newProgressDlg().show(MainActivity.this);

        // 清空列表
        items.clear();
        mAdapterTest.notifyDataSetInvalidated();
        onLoad();

        _queryInfo.resetSearch(null);

        _queryInfo.queryNextBatchByProvince(currentProvince);
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
    ////////////////////////////////////////////////////////////////////////////////////////////////
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {//如果返回键按下
            //此处写退向后台的处理
            Intent intent = new Intent(Intent.ACTION_MAIN);
            intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            intent.addCategory(Intent.CATEGORY_HOME);
            startActivity(intent);
            return true;
        }
        return super.onKeyDown(keyCode, event);
    }


}
