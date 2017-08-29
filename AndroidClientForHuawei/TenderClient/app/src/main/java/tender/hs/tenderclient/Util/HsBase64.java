package tender.hs.tenderclient.Util;

import org.apache.commons.codec.binary.Base64;

/**
 * Created by jiaju on 2017-08-15.
 */

public class HsBase64 {
    // 加密
    public static String getBase64(String base64String) {
        byte[] result = Base64.encodeBase64(base64String.getBytes());

        return new String(result);
    }

    // 解密
    public static String getFromBase64(String base64String) {
        byte[] result = Base64.decodeBase64(base64String.getBytes());
        return new String(result);
    }
}
