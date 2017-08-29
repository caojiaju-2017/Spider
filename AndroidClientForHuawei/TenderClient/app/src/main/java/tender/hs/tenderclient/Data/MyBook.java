package tender.hs.tenderclient.Data;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by jiaju on 2017-08-18.
 */

public class MyBook
{
    public String _email;
    public String _phone;
    public String _startDate;
    public String _stopDate;
    public String _key1;
    public String _key2;
    public String _key3;
    public int _enable;
    public int _notifyType;

    public  MyBook()
    {

    }
    public  MyBook(JSONObject data)
    {
        try {
            this._email = data.getString("EMail");
            this._phone = data.getString("Phone");
            this._startDate = data.getString("StartDate");
            this._stopDate = data.getString("StopDate");
            this._key1 = data.getString("Fliter1");
            this._key2 = data.getString("Fliter2");
            this._key3 = data.getString("Fliter3");
            this._enable = data.getInt("Enable");
            this._notifyType = data.getInt("NotifyType");

        } catch (JSONException e) {
            e.printStackTrace();
        }


    }

}
