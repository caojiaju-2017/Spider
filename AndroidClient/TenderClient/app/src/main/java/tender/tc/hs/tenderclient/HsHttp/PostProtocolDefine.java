package tender.tc.hs.tenderclient.HsHttp;
import java.security.KeyPair;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class PostProtocolDefine
{
	private List<String> mParams = new ArrayList<String>();
	private int mCommand = 0;
	public PostProtocolDefine(int cmdString)
	{
		mCommand = cmdString;
	}
	public void SetParam(String param)
	{
		mParams.add(param);
	}
	
	public String getShortUrl()
	{
		String rtnString = this.getInnerUrl();

		return rtnString;
	}
	
	public int getCommand()
	{
		return mCommand;
	}
	private String getInnerUrl() 
	{
		// TODO Auto-generated method stub
		String metaUrl = null;

		if (mCommand == CommandDefine.LOG_SYSTEM)
		{
			metaUrl = "/api/account/?Command=LOG_SYSTEM&TimeSnap=mmm&Sig=20151023";
		}

		return metaUrl;
	}

	public static String getGetUrl(String cmd,Map<String,String>datas)
	{
		String metaUrl = null;
		if (cmd == "VIEW_MEDIA")
		{
			metaUrl = "/api/attach/?Command=VIEW_MEDIA&TimeSnap=mmm&Sig=20151023";
		}

		Iterator iter=datas.keySet().iterator();
		while(iter.hasNext())
		{
			Object key = iter.next();
			Object value = datas.get(key);

			String sValue = null;
			if (value == null)
			{
				sValue = "";
			}
			else
			{
				sValue = value.toString();
			}
			metaUrl = String.format("%s&%s=%s", metaUrl, key.toString(), sValue);
		}

//		metaUrl = String.format("%s&%s=%s", metaUrl, "LoginCode", HsApplication.Global_App.mLoginInfoData.UserCode);

		return  null;
		//return HsApplication.Global_App.url_HeadString + metaUrl;
	}
	public String getGetUrl(Map<String,String>datas)
	{
		// TODO Auto-generated method stub
		String metaUrl = null;

//		if(mCommand == CommandDefine.CHECK_CARD)
//		{
//			metaUrl = "/cmbcOC/customChannelListener";
//		}

		if (mCommand == CommandDefine.LOG_SYSTEM)
		{
			metaUrl = "/api/account";
		}

        Iterator iter=datas.keySet().iterator();
        while(iter.hasNext())
        {
            Object key = iter.next();
            Object value = datas.get(key);

            String sValue = null;
            if (value == null)
            {
                sValue = "";
            }
            else
            {
                sValue = value.toString();
            }
            metaUrl = String.format("%s&%s=%s", metaUrl, key.toString(), sValue);
        }

//		metaUrl = String.format("%s&%s=%s", metaUrl, "LoginCode", HsApplication.Global_App.mLoginInfoData.UserCode);

		return metaUrl;
	}
}

