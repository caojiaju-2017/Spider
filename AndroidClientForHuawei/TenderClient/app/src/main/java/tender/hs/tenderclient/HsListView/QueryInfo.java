package tender.hs.tenderclient.HsListView;

import android.os.Handler;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import tender.hs.tenderclient.HsApplication;
import tender.hs.tenderclient.HsHttp.CommandDefine;
import tender.hs.tenderclient.HsHttp.HttpAccess;
import tender.hs.tenderclient.Util.LogUtil;

/**
 * Created by jiaju on 2017-08-19.
 */

public class QueryInfo
{
    public int _pageIndex = -1;
    public int _pageSize = 10;
    public String _fliter = null;

    Handler _parentHandle;

    public QueryInfo(Handler handle)
    {
        _parentHandle = handle;
    }
    public void resetSearch(String fliter)
    {
        _pageIndex = -1;
        _pageSize = 10;
        _fliter = fliter;
    }

    public void reduceQueryInfo()
    {
        if (_pageIndex >= 0)
        {
            _pageIndex = _pageIndex - 1;
        }
    }

    private void queryNextBathc()
    {
        _pageIndex = _pageIndex + 1;
        LogUtil.info("Invoke saveBookSetting");
        final HttpAccess access = new HttpAccess(_parentHandle, CommandDefine.QUERY_DATA);
        final Map<String,String> dataMap = new HashMap<>();

        try {
            JSONObject jsonObject = new JSONObject();

            jsonObject.put("Account", HsApplication.Global_App._myUserInfo._account);
            jsonObject.put("PageIndex",this._pageIndex);
            jsonObject.put("PageSize",this._pageSize);
            jsonObject.put("Fliter",this._fliter);
            jsonObject.put("UFlag",0);
            jsonObject.put("RCode","");
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

    public void queryUpdateData(String recodeId)
    {
        LogUtil.info("Invoke saveBookSetting");
        final HttpAccess access = new HttpAccess(_parentHandle, CommandDefine.QUERY_DATA);
        final Map<String,String> dataMap = new HashMap<>();

        try {
            JSONObject jsonObject = new JSONObject();

            jsonObject.put("Account", HsApplication.Global_App._myUserInfo._account);
            jsonObject.put("PageIndex",0);
            jsonObject.put("PageSize",this._pageSize);
            jsonObject.put("Fliter",this._fliter);
            jsonObject.put("RCode",recodeId);
            jsonObject.put("UFlag",1);

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

    public void queryNextBatchByProvince(String currentProvince) {
        if (currentProvince == null)
        {
            this.queryNextBathc();
            return;
        }

        _pageIndex = _pageIndex + 1;
        LogUtil.info("Invoke saveBookSetting");
        final HttpAccess access = new HttpAccess(_parentHandle, CommandDefine.QUERY_DATA);
        final Map<String,String> dataMap = new HashMap<>();

        try {
            JSONObject jsonObject = new JSONObject();

            jsonObject.put("Account", HsApplication.Global_App._myUserInfo._account);
            jsonObject.put("PageIndex",this._pageIndex);
            jsonObject.put("PageSize",this._pageSize);
            jsonObject.put("Fliter",this._fliter);
            jsonObject.put("UFlag",0);
            jsonObject.put("RCode","");
            jsonObject.put("Province", currentProvince);
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
}
