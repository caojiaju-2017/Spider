package tender.tc.hs.tenderclient.Data;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by jiaju on 2017-08-18.
 */

public class UserInfo
{
    public  String _account;
    public String _email;
    public String _Alias;
    public String _Address;
    public String _orgName;
    public double _lantudite;
    public double _longdite;

    public MyBook _myBookInfo;

    public boolean _serviceOverTime;
    public String  _serviceOverDate;
    public String _serviceFeeRate;

    public UserInfo()
    {

    }
    public UserInfo(JSONObject infoData)
    {
        if (infoData == null)
        {
            return;
        }
        try {
            this._account = infoData.getString("Account");
            this._email = infoData.getString("EMail");
            this._Alias = infoData.getString("Alias");
            this._Address = infoData.getString("Address");
            this._orgName = infoData.getString("OrgName");
            this._lantudite = infoData.getDouble("Lantudite");
            this._longdite = infoData.getDouble("Longdite");
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void setBookData(JSONObject bdata)
    {
        if (bdata == null)
        {
            return;
        }
        this._myBookInfo = new MyBook(bdata);
    }
}
