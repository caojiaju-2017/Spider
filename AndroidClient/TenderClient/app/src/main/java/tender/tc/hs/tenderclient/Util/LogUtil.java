package tender.tc.hs.tenderclient.Util;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;


import android.os.Environment;
import android.util.Log;

/**
 * 日志记录
 *
 */
public class LogUtil {
    /**
     * 开发阶段
     */
    private static final int DEVELOP = 0;
    /**
     * 内部测试阶段
     */
    private static final int DEBUG = 1;
    /**
     * 公开测试
     */
    private static final int BATE = 2;
    /**
     * 正式版
     */
    private static final int RELEASE = 3;

    /**
     * 当前阶段标示
     */
    private static int currentStage = DEVELOP;

    /**
     *
     */
    private static String path;
    private static File file;
    private static FileOutputStream outputStream;
    private static String pattern = "yyyy-MM-dd HH:mm:ss";


    public static void info(String msg) {
        info("Door", msg);
    }

    public static void info(String tag, String msg) {
        Date date = new Date();
        SimpleDateFormat sFormat = new SimpleDateFormat(pattern);
        String dateString = sFormat.format(date);

//        HsApplication.Global_App.runLogsList.add(String.format("%s %s", dateString,msg));
        switch (currentStage) {
            case DEVELOP:
                // output to the console
                Log.i(tag, msg);
                break;
            case DEBUG:
                // 在应用下面创建目录存放日志
                break;
            case BATE:

                break;
            case RELEASE:
                // 一般不做日志记录
                break;
        }
    }
}
