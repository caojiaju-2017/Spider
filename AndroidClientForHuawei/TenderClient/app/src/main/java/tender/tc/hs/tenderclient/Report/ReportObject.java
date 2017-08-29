package tender.tc.hs.tenderclient.Report;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
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
    public String reportTitle = null;
    public String xLabelName;
    public String yLabelName;
    public String innerLabelName;
    private Map<String,Object> collectionDatas ; // 饼图用
    public List<String> _keyLists = new ArrayList<>();
    public List<Integer> _valueLists = new ArrayList<>();

    public ReportObject parseFromJosn(JSONObject reportData)
    {
        ReportObject rtnObject = new ReportObject();

        try {
            this.reportTitle = reportData.getString("Title");
            this.type = reportData.getInt("Type");
            this.xLabelName = reportData.getString("XLabel");
            this.yLabelName = reportData.getString("YLabel");
            this.innerLabelName = reportData.getString("InnerLabel");

            JSONObject values = reportData.getJSONObject("Values");

            this.collectionDatas = (Map<String, Object>)fromJson(values);
            for (Map.Entry<String, Object> entry : this.collectionDatas.entrySet()) {
                String sKey = entry.getKey();
                int iValue =Integer.parseInt(entry.getValue().toString());

                // 插入排序 -- 降序
                if (_keyLists.size() == 0)
                {
                    _keyLists.add(sKey);
                    _valueLists.add(iValue);
                    continue;
                }

                boolean haveFind = false;
                for (int index = 0 ; index < _keyLists.size(); index++)
                {
                    String cKey = _keyLists.get(index);
                    if (sKey.compareTo(cKey) > 0)
                    {
                        haveFind = true;
                        _keyLists.add(index,sKey);
                        _valueLists.add(index,iValue);
                        break;
                    }
                }

                if (!haveFind)
                {
                    _keyLists.add(sKey);
                    _valueLists.add(iValue);
                }
            }

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
