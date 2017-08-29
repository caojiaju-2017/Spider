package tender.hs.tenderclient.HsHttp;

import android.content.Context;
import android.os.Handler;
import android.os.Message;
import org.json.JSONObject;
import java.io.IOException;
import java.net.ConnectException;
import java.net.SocketTimeoutException;
import java.util.Map;
import java.util.concurrent.TimeUnit;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import tender.hs.tenderclient.HsApplication;
import tender.hs.tenderclient.Util.LogUtil;

/**
 * Created by caojiaju on 16/7/31.
 */
public class HttpAccess
{
    MediaType JSON=MediaType.parse("application/json; charset=utf-8");
    Context mCtx;
    Handler mainHandlers = null;
    String errorNet = "{\"ErrorId\":9999,\"ErrorInfo\":\"网络错误\",\"Result\":\"\"}";
    int cmdString = 0;
    JSONObject dictObject = new JSONObject();

    //创建一个OkHttpClient对象
    OkHttpClient okHttpClient = null ;//new OkHttpClient();

    //private LogWriter mLogWriter;

    public  HttpAccess(Handler parentHandle,int commandString)
    {
        mainHandlers = parentHandle;
        cmdString = commandString;

        okHttpClient =new OkHttpClient.Builder().
                readTimeout(10, TimeUnit.SECONDS).
                writeTimeout(10, TimeUnit.SECONDS).
                connectTimeout(10, TimeUnit.SECONDS).
                build();
    }


    public void HttpPost()
    {
        writeLocalLog("enter HttpPost");

        String url_head = HsApplication.Global_App.url_HeadString;
        PostProtocolDefine proDef = new PostProtocolDefine(cmdString);
        String urlString = proDef.getShortUrl();

        //申明给服务端传递一个json串
        String json = dictObject.toString();
        //创建一个RequestBody(参数1：数据类型 参数2传递的json串)
        RequestBody requestBody = RequestBody.create(JSON, json);

        //创建一个请求对象
        Request request = new Request.Builder()
                .url(url_head + urlString)
                .post(requestBody)
                .build();

        LogUtil.info(url_head + urlString);

        writeLocalLog("prepare Post finish");

        //发送请求获取响应
        try {
                Response response=okHttpClient.newCall(request).execute();

            writeLocalLog("exccute finish 1");

               String reposeData = response.body().string();
            writeLocalLog("exccute finish 2");

                if (mainHandlers != null) {
                    Message msg = new Message();
                    msg.what = cmdString;
                    msg.obj = reposeData;

                    writeLocalLog("success ,notice main thread");
                    mainHandlers.sendMessage(msg);
                }
        }
        catch (SocketTimeoutException ex)
        {
            LogUtil.info("Socket Timeout!");
            if (mainHandlers != null) {
                Message msg = new Message();
                msg.what = CommandDefine.INNER_ERROR;
                msg.obj = errorNet;
                writeLocalLog("failed ,notice main thread");
                mainHandlers.sendMessage(msg);
            }
            ex.printStackTrace();
        }
        catch(ConnectException ec)
        {
            LogUtil.info("Connect Timeout!");
            if (mainHandlers != null) {
                Message msg = new Message();
                msg.what = CommandDefine.INNER_ERROR;
                msg.obj = errorNet;
                writeLocalLog("failed ,notice main thread");
                mainHandlers.sendMessage(msg);
            }
            ec.printStackTrace();
        }
        catch (Exception e) {
            LogUtil.info("Other Failed!");
            if (mainHandlers != null) {
                Message msg = new Message();
                msg.what = CommandDefine.INNER_ERROR;
                msg.obj = errorNet;
                writeLocalLog("failed ,notice main thread");
                mainHandlers.sendMessage(msg);
            }
            e.printStackTrace();
        }
    }

    private void writeLocalLog(String  msg)
    {

//        File logf = new File("/sdcard/" + "runlog.txt");
//
//        try {
//            mLogWriter = LogWriter.open(logf.getAbsolutePath());
//        } catch (IOException e) {
//            // TODO Auto-generated catch block
//            return;
//        }

//        try {
//            LogWriter.print(HttpAccess.class, msg);
//        } catch (IOException e) {
//            // TODO Auto-generated catch block
//        }
//
//        try {
//            mLogWriter.close();
//        } catch (IOException e) {
//            e.printStackTrace();
//        }
    }
    public void HttpGet(Map<String,String> mapData)
    {
        PostProtocolDefine proDef = new PostProtocolDefine(cmdString);
        String urlString = proDef.getGetUrl(mapData);
//        String url_head = HsApplication.Global_App.url_HeadString;
//        String urls = url_head + urlString;
        String urls = "";

        Request request = new Request.Builder()
                .url(urls)
                .build();
        try {
            Response response = okHttpClient.newCall(request).execute();
            if(response.isSuccessful()){
                String reposeData = response.body().string();

                if (mainHandlers != null) {
                    Message msg = new Message();
                    msg.what = cmdString;
                    msg.obj = reposeData;

                    mainHandlers.sendMessage(msg);
                }
            }
        } catch (IOException e) {
            if (mainHandlers != null) {
                Message msg = new Message();
                msg.what = cmdString;
                msg.obj = errorNet;

                mainHandlers.sendMessage(msg);
            }
            e.printStackTrace();
        }
    }


    public void setJsonObject(JSONObject jsonObject) {
        dictObject = jsonObject;
    }

    public void setContext(Context context) {
        mCtx = context;
    }

//    public static String invoke() {
//        String result = null;
//        try {
//            final String url = "http://192.168.1.104:180/";
//
//            HttpPost httpPost = new HttpPost(url);
//            DefaultHttpClient httpClient = new DefaultHttpClient();
//
//            //基本身份验证
//            BasicCredentialsProvider bcp = new BasicCredentialsProvider();
//            String userName = "liudong";
//            String password = "123";
//            bcp.setCredentials(AuthScope.ANY, new UsernamePasswordCredentials(
//                    userName, password));
//            httpClient.setCredentialsProvider(bcp);
//
//            HttpResponse httpResponse = httpClient.execute(httpPost);
//
//            StringBuilder builder = new StringBuilder();
//            BufferedReader reader = new BufferedReader(new InputStreamReader(
//                    httpResponse.getEntity().getContent()));
//            for (String s = reader.readLine(); s != null; s = reader.readLine()) {
//                builder.append(s);
//            }
//            result = builder.toString();
//            Log.d(TAG, "result is ( " + result + " )");
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        Log.d(TAG, "over");
//        return result;
//    }
}
