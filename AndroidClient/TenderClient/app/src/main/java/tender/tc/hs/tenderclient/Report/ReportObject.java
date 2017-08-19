package tender.tc.hs.tenderclient.Report;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Dictionary;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

/**
 * Created by caojiaju on 2016/8/20.
 */
public class ReportObject
{
    // 0 饼图  1线图
    public int type = 0;
    public String reportName = null;
    public Map<String,Object> collectionDatas ; // 饼图用

    public ReportObject parseFromJosn(JSONObject reportData)
    {
        ReportObject rtnObject = new ReportObject();

        try {
            this.reportName = reportData.getString("Title");
            this.type = reportData.getInt("Type");

            JSONObject values = reportData.getJSONObject("Values");

            this.collectionDatas = (Map<String, Object>)fromJson(values);

        } catch (JSONException e) {
            e.printStackTrace();
        }

        return  rtnObject;
    }
    public static Map<String, Object> fromJson(JSONObject jsonObject) {
        Map<String, Object> map = new HashMap<String, Object>();

        Iterator<String> keyIterator = jsonObject.keys();
        while (keyIterator.hasNext()) {
            String key = keyIterator.next();
            try {
                Object obj = jsonObject.get(key);

                if (obj instanceof JSONObject) {
                    map.put(key, fromJson((JSONObject)obj));
                }
                else if (obj instanceof JSONArray) {
                    map.put(key, fromJson((JSONObject)obj));
                }
                else {
                    map.put(key, obj);
                }
            }
            catch (JSONException jsone) {
//                Log.wtf("RequestManager", "Failed to get value for " + key + " from JSONObject.", jsone);
            }
        }

        return map;
    }
}
