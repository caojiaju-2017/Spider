package tender.tc.hs.tenderclient.HsListView;

import java.lang.reflect.Method;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.Enumeration;
import java.util.List;
import java.util.UUID;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.Service;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ServiceInfo;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.Rect;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.TrafficStats;
import android.net.Uri;
import android.os.Build;
import android.provider.MediaStore;
import android.telephony.TelephonyManager;
import android.text.ClipboardManager;
import android.text.TextUtils;
import android.view.Display;
import android.view.View;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.webkit.CookieManager;
import android.webkit.CookieSyncManager;
import android.widget.EditText;

//import com.sale.app.Constants;
//import com.sale.app.activity.BaseActivity;
//import com.sale.app.global.Global;
//import com.sale.app.utils.log.LogManager;

/**
 *
 * @功能:系统帮助类
 * @author caomin
 * @2015-8-31
 * @上午11:50:41
 */
public class SystemUtils
{
	/**
	 * 建议默认线程池的大小根据系统可用的处理器
	 */
	public static final int DEFAULT_THREAD_POOL_SIZE = getDefaultThreadPoolSize();

	private static final int THREAD_POOL_SIZE = 8;

	/**
	 * mac地址
	 */
	private static String mac = "";

	/**
	 * 获得推荐默认线程池的大小
	 * @return if 2 * availableProcessors + 1 less than 8, return it, else return 8;
	 */
	public static int getDefaultThreadPoolSize()
	{
		return getDefaultThreadPoolSize(THREAD_POOL_SIZE);
	}

	/**
	 * 获得推荐默认线程池的大小
	 * @param max
	 * @return if 2 * availableProcessors + 1 less than max, return it, else return max;
	 */
	public static int getDefaultThreadPoolSize(int max)
	{
		int availableProcessors = 2 * Runtime.getRuntime().availableProcessors() + 1;
		return availableProcessors > max ? max : availableProcessors;
	}

	/**
	 * 跳转到HOME
	 * @param context
	 */
	public static void toSysHome(Context context)
	{
		Intent intent = new Intent();
		intent.setAction(Intent.ACTION_MAIN);
		intent.addCategory(Intent.CATEGORY_HOME);
		context.startActivity(intent);
	}

	/**
	 * 获取手机串号imei
	 * @param context
	 */
	public static String getDeviceId(Context context)
	{
		if (context == null)
		{
			return "";
		}
		TelephonyManager manger = (TelephonyManager) context.getSystemService(Context.TELEPHONY_SERVICE);
		if (manger != null)
		{
			return manger.getDeviceId();
		}
		return "";
	}

	/**
	 * @return 手机型号
	 */
	public static String getModels()
	{
		return Build.MODEL;
	}

	/**
	 * @return 手机品牌
	 */
	public static String getManuFactor()
	{
		return Build.MANUFACTURER;
	}

	/**
	 * 获取当前屏幕宽度
	 */
	public static int getScreenWidth(Context context)
	{
		if (null != context)
		{
			WindowManager windowManager = (WindowManager) context.getSystemService(Context.WINDOW_SERVICE);
			Display display = windowManager.getDefaultDisplay();
			return display.getWidth();
		}
		return 0;
	}

	/**
	 * 获取当前屏幕高度
	 */
	public static int getScreenHeight(Context context)
	{
		if (null != context)
		{
			WindowManager windowManager = (WindowManager) context.getSystemService(Context.WINDOW_SERVICE);
			Display display = windowManager.getDefaultDisplay();
			return display.getHeight();
		}
		return 0;
	}

//	public static void exitSystem(Context context)
//	{
//		exitSystem(context, false);
//	}
//
//	/**
//	 * 退出系统
//	 */
//	public static void exitSystem(Context context, boolean isExitService)
//	{
//		// 退出所有Activity
//		BaseActivity.exitAllActivity();
//
//		if (isExitService)
//		{
//			if (context != null)
//			{
////				context.stopService(new Intent(context, SocialService.class));
//			}
//		}
//		//清理Global里的数据
//		Global.getInstance().clear();
//		System.exit(0);
//	}

//	/**
//	 * 获取设备Mac地址
//	 */
//	public static String getDeviceMac(Context context)
//	{
//		return getDeviceMac("-", context);
//	}

//	/**
//	 * 获取设备mac地址
//	 * @param spaceMark mac地址间隔符（如：-）
//	 */
//	public static String getDeviceMac(String spaceMark, Context context)
//	{
//		if (!TextUtils.isEmpty(mac))
//		{
//			return mac;
//		}
//
//		if (null != context)
//		{
//			// 取得WifiManager对象
//			WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
//
//			// 取得WifiInfo对象
//			WifiInfo wifiInfo = wifiManager.getConnectionInfo();
//			if (null != wifiInfo)
//			{
//				mac = wifiInfo.getMacAddress();
//
//				// 替换间隔符号
//				if (!TextUtils.isEmpty(mac))
//				{
//					mac = mac.replaceAll(":", spaceMark);
//				}
//			}
//		}
//		return mac;
//	}

	/**
	 * 获取手机序列号
	 */
	public static String getSerialNumber()
	{
		String serial = null;
		try
		{
			Class<?> c = Class.forName("android.os.SystemProperties");
			Method get = c.getMethod("get", String.class);
			serial = (String) get.invoke(c, "ro.serialno");
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		return serial;
	}

	/**
	 * 获取设备id
	 */
	public static String getDeviceSerNum(Context context)
	{

		String deviceSerNum = "";
		if (null != context)
		{
			TelephonyManager mTelephonyManager = (TelephonyManager) context.getSystemService(Context.TELEPHONY_SERVICE);

			if (null != mTelephonyManager)
			{
				deviceSerNum = mTelephonyManager.getDeviceId();
			}
		}

		if (TextUtils.isEmpty(deviceSerNum))
		{
			deviceSerNum = Build.ID;
		}

		if (TextUtils.isEmpty(deviceSerNum))
		{
			deviceSerNum = UUID.randomUUID().toString().substring(0, 30);
		}

		return deviceSerNum;
	}

	/**
	 * 获取系统版本名字
	 */
	public static String getSystemVersionName()
	{
		String sysVersion = Build.VERSION.RELEASE;
		if (TextUtils.isEmpty(sysVersion))
		{
			sysVersion = "";
		}
		return sysVersion;
	}

	/**
	 * 获取系统版本值
	 */
	public static int getSystemVersionCode()
	{
		return Build.VERSION.SDK_INT;
	}

//	/**
//	 * 获取本机googleplay账号
//	 */
//	public static String getGooglePlayAccount(Context context)
//	{
//		AccountManager accountManager = AccountManager.get(context);
//		Account[] accounts = accountManager.getAccountsByType("com.google");
//		Account account = null;
//		if (accounts.length > 0)
//		{
//			account = accounts[0];
//			return account.name;
//		}
//		return null;
//	}

	/**
	 * 判断一个程序是否正在运行
	 */
	public static boolean isLauncherRunnig(Context context)
	{
		ActivityManager mActivityManager = (ActivityManager) context.getSystemService(Context.ACTIVITY_SERVICE);
		String packageName = context.getPackageName();
		if (null == packageName)
		{
			return false;
		}
		boolean result = false;
		List<ActivityManager.RunningAppProcessInfo> appList = mActivityManager.getRunningAppProcesses();
		for (ActivityManager.RunningAppProcessInfo running : appList)
		{
			if (running.importance == ActivityManager.RunningAppProcessInfo.IMPORTANCE_FOREGROUND)
			{
				if (packageName.equals(running.processName))
				{
					result = true;
					break;
				}
			}
		}
//		LogManager.getLogger().d(Constants.Config.TAG, "程序是否正在运行：" + result);
		return result;
	}

	/**
	 * 判断当前应用是否在前台运行
	 */
	public static boolean isTopActivity(Context context)
	{
		if (context == null)
		{
			return false;
		}

		String packageName = context.getPackageName();

		ActivityManager activityManager = (ActivityManager) context.getSystemService(Context.ACTIVITY_SERVICE);

		List<ActivityManager.RunningTaskInfo> tasksInfo = activityManager.getRunningTasks(1);

		if (tasksInfo != null && tasksInfo.size() > 0)
		{
			// 应用程序位于堆栈的顶层
			if (packageName.equals(tasksInfo.get(0).topActivity.getPackageName()))
			{
				return true;
			}
		}
		return false;
	}

	/**
	 * 通过URI获取文件路径
	 */
	public static String getRealPathFromURI(Uri paramUri, Context context)
	{

		if (null == paramUri)
		{
			return null;
		}

		String localPath;
		String uriString = paramUri.toString();

		// 如果是文件前缀头，那么直接截取路径
		if (uriString.startsWith("file://"))
		{
			localPath = paramUri.getPath();

			if (TextUtils.isEmpty(localPath))
			{

				localPath = uriString.replaceFirst("file://", "");

				// 替换空格
				localPath = localPath.replaceAll("%20", " ");
			}

			return localPath;
		}

		Cursor localCursor = null;
		try
		{
			String[] proj = { MediaStore.Images.Media.DATA };
			localCursor = context.getContentResolver().query(paramUri, proj, null, null, null);
			if (null != localCursor)
			{
				int i = localCursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
				localCursor.moveToFirst();
				return localCursor.getString(i);
			}
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		finally
		{
			if (localCursor != null && !localCursor.isClosed())
			{
				localCursor.close();
			}
		}
		return null;
	}

	/**
	 * 获取本机IP地址
	 */
	public static String getLocalIpAddress()
	{
		try
		{
			for (Enumeration<NetworkInterface> en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements();)
			{
				NetworkInterface intf = en.nextElement();
				for (Enumeration<InetAddress> enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements();)
				{
					InetAddress inetAddress = enumIpAddr.nextElement();
					if (!inetAddress.isLoopbackAddress())
					{
						return inetAddress.getHostAddress();
					}
				}
			}
		}
		catch (SocketException ex)
		{
//			LogManager.getLogger().e(Constants.Config.TAG, "WifiPreference IpAddress", ex);
		}
		return "";
	}

//	/**
//	 * 获取本机MAC地址
//	 */
//	public static String getLocalMacAddress(Context context)
//	{
//		try
//		{
//			WifiManager wifi = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
//			WifiInfo info = wifi.getConnectionInfo();
//			return info.getMacAddress();
//		}
//		catch (Exception e)
//		{
//			e.printStackTrace();
//		}
//		return "";
//	}

//	/**
//	 * 获取本机名称
//	 */
//	public static String getPhoneName()
//	{
//		String phoneName = "";
//		phoneName = Build.BRAND + " " + Build.MODEL;
//		if (TextUtils.isEmpty(phoneName))
//		{
//			phoneName = Constants.Config.UNKNOW;
//		}
//		return phoneName;
//	}

	/**
	 * 判断SD卡是否可用
	 */
	public static boolean isAvaiableSDCard()
	{
		return android.os.Environment.getExternalStorageState().equals(android.os.Environment.MEDIA_MOUNTED);
	}

	/**
	 * 获取总的接受字流量，包含Mobile和WiFi等
	 * @return 流量数kb
	 */
	public static long getTotalRxBytes(Context context)
	{
		return TrafficStats.getTotalRxBytes() == TrafficStats.UNSUPPORTED ? 0 : (TrafficStats.getUidRxBytes(context.getApplicationInfo().uid) / 1024);
	}

	/**
	 * 获取总的发送流量，包含Mobile和WiFi等
	 * @return 流量数kb
	 */
	public static long getTotalTxBytes(Context context)
	{
		return TrafficStats.getTotalTxBytes() == TrafficStats.UNSUPPORTED ? 0 : (TrafficStats.getUidTxBytes(context.getApplicationInfo().uid) / 1024);
	}

	/**
	 * 获取通过Mobile连接收到的流量，不包含WiFi
	 * @return 流量数kb
	 */
	public static long getMobileRxBytes()
	{
		return TrafficStats.getMobileRxBytes() == TrafficStats.UNSUPPORTED ? 0 : (TrafficStats.getMobileRxBytes() / 1024);
	}

	/**
	 * 获取通过Mobile发送的流量，不包含WiFi
	 * @return 流量数kb
	 */
	public static long getMobileTxBytes()
	{
		return TrafficStats.getMobileRxBytes() == TrafficStats.UNSUPPORTED ? 0 : (TrafficStats.getMobileRxBytes() / 1024);
	}

	/**
	 * 关闭软键盘，输入法
	 * @param context
	 */
	public static void hideSoftInputMethod(Activity context)
	{
		try
		{
			InputMethodManager inputMethodManager = (InputMethodManager) context.getSystemService(Context.INPUT_METHOD_SERVICE);
			View view = context.getCurrentFocus();
			if (view != null)
			{
				inputMethodManager.hideSoftInputFromWindow(view.getWindowToken(), 0);
			}
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
	}

	/**
	 * 清除本应用中的Cookie
	 */
	public static void clearCookie(Context context)
	{
		CookieSyncManager.createInstance(context);
		CookieManager cookieManager = CookieManager.getInstance();
		cookieManager.removeAllCookie();
	}

	/**
	 * 打开软键盘，输入法
	 */
	public static void showSoftInputMethod(Activity context, EditText view)
	{
		InputMethodManager inputMethodManager = (InputMethodManager) context.getSystemService(Context.INPUT_METHOD_SERVICE);

		// 接受软键盘输入的编辑文本或其它视图
		inputMethodManager.showSoftInput(view, 0);
	}

	/**
	 * 复制文字到系统
	 * @param context 上下文
	 * @param text 要复制的文字
	 */
	public static void copyTextToSystem(Context context, String text)
	{
		ClipboardManager clipboardManager = (ClipboardManager) context.getSystemService(Context.CLIPBOARD_SERVICE);
		clipboardManager.setText(text);
	}

//	/**
//	 * 根据指定的metaData key 获取
//	 * @return 返回对应的metaData的value值
//	 */
//	public static synchronized String getMetaData(Context ctx, String metaKey)
//	{
//		String result = Constants.Config.UNKNOW;
//		try
//		{
//			ApplicationInfo appInfo;
//			appInfo = ctx.getPackageManager().getApplicationInfo(ctx.getPackageName(), PackageManager.GET_META_DATA);
//			if (appInfo != null && appInfo.metaData != null)
//			{
//				result = appInfo.metaData.get(metaKey) + "";
//			}
//		}
//		catch (Exception e)
//		{
//			LogManager.getLogger().d(Constants.Config.TAG, "read " + metaKey + " error!", e);
//			e.printStackTrace();
//		}
//		return result;
//	}

	/**
	 * 根据指定的metaData key 获取
	 * @return 返回对应的metaData的value值
	 */
	public static synchronized String getServiceMetaData(Service ctx, String metaKey)
	{
		String result = null;
		try
		{
			ComponentName cn = new ComponentName(ctx, ctx.getClass());
			ServiceInfo serviceInfo = ctx.getPackageManager().getServiceInfo(cn, PackageManager.GET_META_DATA);
			if (serviceInfo != null && serviceInfo.metaData != null)
			{
				result = serviceInfo.metaData.get(metaKey) + "";
			}
		}
		catch (Exception e)
		{
//			LogManager.getLogger().d(Constants.Config.TAG, "read " + metaKey + " error!", e);
			e.printStackTrace();
		}
		return result;
	}

	/**
	 * 启动系统的下载
	 * @param url 要下载的url路径
	 */
	public static void systemDownload(Context context, String url) throws Exception
	{
		Intent i = new Intent(Intent.ACTION_VIEW);
		if (!url.startsWith("http://"))
		{
			url = "http://" + url;
		}
		i.setData(Uri.parse(url));
		context.startActivity(i);
	}

	/**
	 * 截屏代码
	 */
	public static Bitmap takeScreenShot(Activity activity)
	{
		// View是你需要截图的View
		View view = activity.getWindow().getDecorView();
		view.setDrawingCacheEnabled(true);
		view.buildDrawingCache();
		Bitmap b1 = view.getDrawingCache();

		// 获取状态栏高度
		Rect frame = new Rect();
		activity.getWindow().getDecorView().getWindowVisibleDisplayFrame(frame);
		int statusBarHeight = frame.top;

		// 获取屏幕长和高
		int width = activity.getWindowManager().getDefaultDisplay().getWidth();
		int height = activity.getWindowManager().getDefaultDisplay().getHeight();
		// 去掉标题栏
		Bitmap b = Bitmap.createBitmap(b1, 0, statusBarHeight, width, height - statusBarHeight);
		view.destroyDrawingCache();
		return b;
	}

	/**
	 * 生成min-max之间的随机数
	 * @param min 最小的值(包含)
	 * @param max 最大的值(包含)
	 * @return min-max之间的随机数
	 */
	public static int random(int min, int max)
	{
		return (int) Math.round(Math.random() * (max - min) + min);
	}

	/**
	 * 获取网络连接是否可用
	 */
	public static boolean isNetworkConnected(Context context)
	{
		if (context != null)
		{
			ConnectivityManager mConnectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
			NetworkInfo mNetworkInfo = mConnectivityManager.getActiveNetworkInfo();
			if (mNetworkInfo != null)
			{
				return mNetworkInfo.isAvailable();
			}
		}
		return false;
	}

	/**
	 * 获取当前屏幕宽度
	 *
	 * @return
	 */
	public static int getScreenWidth(Activity activity)
	{
		if (null != activity)
		{
			Display display = activity.getWindowManager().getDefaultDisplay();
			return display.getWidth();
		}
		return 0;
	}
}
