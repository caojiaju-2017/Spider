package tender.tc.hs.tenderclient;

import java.util.ArrayList;
import java.util.List;


import android.app.Activity;
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
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsListView.XListView;
import tender.tc.hs.tenderclient.Login.RegisterActivity;

public class MainActivity extends Activity implements OnClickListener ,
        AdapterView.OnItemClickListener,
        XListView.IXListViewListener
{
    LinearLayout layout_book;
    LinearLayout layout_home;
    LinearLayout layout_report;
    LinearLayout layout_my;
    ViewPager pager;
    PagerAdapter mAdapter;
    private List<View> mViews = new ArrayList<View>();

    private ImageView img_book;
    private ImageView img_home;
    private ImageView img_report;


    LinearLayout no_config_lay;
    LinearLayout have_config_lay;


    XListView customListView;
    private ArrayAdapter<String> mAdapterTest;
    private ArrayList<String> items = new ArrayList<String>();
    private Handler mainHandlers;

    private Handler mHandler;
    private Dialog mRegisterHandle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);// 去除title

        setContentView(R.layout.main_activity);

        registerComminucation();
        mHandler = new Handler();

        geneItems();

        initView();
        initEvent();

        layout_home.performClick();

        showHomeWay(false);


    }

    // 事件初始化
    private void initEvent() {
        layout_book.setOnClickListener(this);
        layout_home.setOnClickListener(this);
        layout_report.setOnClickListener(this);
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
        pager = (ViewPager) findViewById(R.id.pager);
        LayoutInflater inflater = LayoutInflater.from(this);
        View tab01 = inflater.inflate(R.layout.book_lay, null);
        View tab02 = inflater.inflate(R.layout.home_lay, null);
        View tab03 = inflater.inflate(R.layout.report_lay, null);
        mViews.add(tab01);
        mViews.add(tab02);
        mViews.add(tab03);

        no_config_lay = (LinearLayout)tab02.findViewById(R.id.no_config_lay);
        have_config_lay = (LinearLayout)tab02.findViewById(R.id.have_config_lay);

        // 注册添加配置 点击
        ImageView img_add_cfg = (ImageView)tab02.findViewById(R.id.img_add_cfg);
        if (img_add_cfg != null)
        {
            img_add_cfg.setOnClickListener(this);
        }

        /////////////////订阅数据列表/////////////////////
        customListView = (XListView) tab02.findViewById(R.id.xListView);
        customListView.setPullLoadEnable(true);
        mAdapterTest = new ArrayAdapter<String>(this, R.layout.list_item, items);
        customListView.setAdapter(mAdapterTest);
        customListView.setXListViewListener(this);


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
        switch (v.getId()) {
            case R.id.layout_book:
                pager.setCurrentItem(0);
                img_book.setImageResource(R.drawable.book_pressed);
                break;
            case R.id.layout_home:
                pager.setCurrentItem(1);
                img_home.setImageResource(R.drawable.home_pressed);
                break;
            case R.id.layout_report:
                pager.setCurrentItem(2);
                img_report.setImageResource(R.drawable.report_pressed);
                break;
            case R.id.img_add_cfg:
//                pager.setCurrentItem(0);
//                img_book.setImageResource(R.drawable.book_pressed);
                showHomeWay(true);
                break;

        }
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
//    private  void RegisterListEvent()
//    {
//        customListView.setOnRefreshEventListener(this);
//        customListView.setOnItemClickListener(this);
//        customListView.setAutoLoadOnBottom(false);
//        customListView.setPullLoadEnable(false);
//    }


    private int start = 0;
private void geneItems() {

    for (int i = 0; i < 20; ++i) {
        items.add("refresh cnt " + (++start));
    }
}

    @Override
    public void onRefresh() {
        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {

                geneItems();
                mAdapterTest.notifyDataSetInvalidated();

                onLoad();
            }
        }, 2000);
    }

    @Override
    public void onLoadMore() {
        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                geneItems();

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
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

    }

    private void registerComminucation() {
        //        iDCardDevice=new publicSecurityIDCardLib(this);
        // 主线程通信事件句柄
        mainHandlers = new Handler() {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case CommandDefine.SET_BOOK: {
                        break;
                    }
                    case CommandDefine.GET_BOOK: {
                        break;
                    }
                    case CommandDefine.PAY_FOR_SERVICE: {
                        break;
                    }
                    case CommandDefine.QUERY_DATA: {
                        break;
                    }
                    case CommandDefine.GET_NORMAL_REPORT: {
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
