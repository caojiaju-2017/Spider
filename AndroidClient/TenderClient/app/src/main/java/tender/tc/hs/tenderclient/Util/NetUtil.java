package tender.tc.hs.tenderclient.Util;

import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.Enumeration;

import android.content.Context;
import android.net.DhcpInfo;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.text.format.Formatter;

import tender.tc.hs.tenderclient.HsApplication;

public class NetUtil {
	public static String getLocalIpAddress() {
		WifiManager wifimanage=(WifiManager) HsApplication.Global_App.getSystemService(Context.WIFI_SERVICE);//获取WifiManager
		  
		//检查wifi是否开启  
		  
		if(!wifimanage.isWifiEnabled())  {  
		  
			wifimanage.setWifiEnabled(true);  
		  
		}  
		  
		WifiInfo wifiinfo= wifimanage.getConnectionInfo();  
		  
		String ip=FormatIP(wifiinfo.getIpAddress());
		
		return ip;
	}

	public static String getMask() {
		WifiManager wifi = (WifiManager) HsApplication.Global_App
				.getSystemService(Context.WIFI_SERVICE);
		DhcpInfo d = wifi.getDhcpInfo();
		return FormatIP(d.netmask);
	}
	
	public static String getGateWay() {
		WifiManager wifi = (WifiManager) HsApplication.Global_App
				.getSystemService(Context.WIFI_SERVICE);
		DhcpInfo d = wifi.getDhcpInfo();
		return FormatIP(d.gateway);
	}

	// 获取mac地址
	public static String getLocalMacAddress() {
		WifiManager wifi = (WifiManager) HsApplication.Global_App
				.getSystemService(Context.WIFI_SERVICE);
		WifiInfo info = wifi.getConnectionInfo();
		return info.getMacAddress();
	}

	public static String FormatIP(int IpAddress)
	{
	    return Formatter.formatIpAddress(IpAddress);
	}
}
