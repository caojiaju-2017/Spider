package tender.hs.tenderclient.Data;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by jiaju on 2017-08-19.
 */

public class BookData
{
    public String _classfic;
    public String _projectNo;
    public String _url;
    public String _projectName;
    public String _title;
    public String _way;
    public String _time;
    public String _owner;
    public String _unique;
    public String _recordTime;

    public static List<BookData> buildTestData(int count)
    {
        List<BookData> rtnDatas = new ArrayList<>();

        for (int index = 0 ; index < count; index ++)
        {
            BookData data = new BookData();

            rtnDatas.add(data);
        }

        return rtnDatas;
    }

    public static BookData pareFromJson(JSONObject oneResult) {
        BookData oneData = new BookData();

        try
        {
            oneData._classfic = oneResult.getString("Classfic").replace("\n","").replace("\t","");
            oneData._unique = oneResult.getString("Unique").replace("\n","").replace("\t","");
            oneData._recordTime = oneResult.getString("RecordTime").replace("\n","").replace("\t","");
            oneData._title = oneResult.getString("Title").replace("\n","").replace("\t","");
            oneData._url = oneResult.getString("Url");
            oneData._projectName = oneResult.getString("ProjectName").replace("\n","").replace("\t","");
            oneData._projectNo = oneResult.getString("ProjectNo").replace("\n","").replace("\t","");
            oneData._time = oneResult.getString("Time").replace("\n","").replace("\t","");
            oneData._way = oneResult.getString("Way").replace("\n","").replace("\t","");
        }
        catch (JSONException ex)
        {
            return  null;
        }

        return  oneData;
    }
}
