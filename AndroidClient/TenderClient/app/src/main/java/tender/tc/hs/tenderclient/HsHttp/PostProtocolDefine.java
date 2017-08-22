package tender.tc.hs.tenderclient.HsHttp;
import java.security.KeyPair;
import java.util.ArrayList;
import java.util.Date;
import java.util.Dictionary;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import tender.tc.hs.tenderclient.Util.HsBase64;

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

		long currentTime = new Date().getTime();
		String sigString = String.format("%d",currentTime);
		sigString = HsBase64.getBase64(sigString);
		if (mCommand == CommandDefine.LOG_SYSTEM)
		{
			metaUrl = String.format("/api/account/?Command=LOG_SYSTEM&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.REG_ACCOUNT)
		{
			metaUrl = String.format("/api/account/?Command=REG_ACCOUNT&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if (mCommand == CommandDefine.RESET_PASSWORD)
		{
			metaUrl = String.format("/api/account/?Command=RESET_PASSWORD&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.GET_BOOK)
		{
			metaUrl = String.format("/api/account/?Command=GET_BOOK&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.SET_BOOK)
		{
			metaUrl = String.format("/api/account/?Command=SET_BOOK&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.GET_NORMAL_REPORT)
		{
			metaUrl = String.format("/api/account/?Command=GET_NORMAL_REPORT&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.GET_USERINFO)
		{
			metaUrl = String.format("/api/account/?Command=GET_USERINFO&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.SET_USERINFO)
		{
			metaUrl = String.format("/api/account/?Command=SET_USERINFO&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.QUERY_DATA)
		{
			metaUrl = String.format("/api/account/?Command=QUERY_DATA&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}
		else if(mCommand == CommandDefine.APPLY_SMSCODE)
		{
			metaUrl = String.format("/api/account/?Command=APPLY_SMSCODE&ServiceCode=TS_001&TimeSnap=%d&Sig=%s",currentTime,sigString);
		}

		return metaUrl;
	}

	public static String getGetUrl(String cmd,Map<String,String>datas)
	{
		String metaUrl = null;
		if (cmd == "VIEW_MEDIA")
		{
			metaUrl = "/api/attach/?Command=VIEW_MEDIA&ServiceCode=TS_001&TimeSnap=mmm&Sig=20151023";
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

