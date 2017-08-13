package tender.tc.hs.tenderclient.Util;

import java.io.ByteArrayInputStream;
import java.io.UnsupportedEncodingException;
import java.math.BigInteger;

public class BytesOperator {
	/**
	 * 从一个byte[]数组中截取一部分
	 * @param src
	 * @param begin
	 * @param count
	 * @return
	 */
	public static byte[] subBytes(byte[] src, int begin, int count)
	{
		byte[] bs = new byte[count];
		for (int i=begin; i<begin+count; i++)
			bs[i-begin] = src[i];

		return bs;
	}

	public static char byteToChar(byte[] b) {
//        char c = (char) (((b[0] & 0xFF) << 8) | (b[1] & 0xFF));
		char c = (char) ((b[0] & 0xFF) << 8);
		return c;
	}
	public static byte[] charToByte(char c) {
		byte[] b = new byte[1];
//        b[0] = (byte) ((c & 0xFF00) >> 8);
		b[0] = (byte) (c & 0xFF);
		return b;
	}
	public static short bytesToShort(byte[] b) {
		return (short) (b[0] & 0xff | (b[1] & 0xff) << 8);
	}

	/**
	 * short整数转换为2字节的byte数组
	 *
	 * @param s
	 *            short整数
	 * @return byte数组
	 */
	public static byte[] unsignedShortToByte2(int s) {
		byte[] targets = new byte[2];
		targets[0] = (byte) (s & 0xFF);
		targets[1] = (byte) (s >> 8 & 0xFF);

		return targets;
	}

	public static int getIntValue(byte [] b,int s,int e)
	{
		int nR=0;

		int offset = e - s;

//	    if(s+3<e)
//	    {
		if (offset == 0)
		{
			return 0;
		}

		if (offset >= 1)
		{
			nR = b[s] & 0xff;
		}

		if (offset >= 2)
		{
			nR = nR + ( b[s+1]<<8  & 0x0000ff00 );
		}

		if (offset >= 3)
		{
			nR = nR + ( b[s+2]<<16 & 0x00ff0000 );
		}

		if(offset >= 4)
		{
			nR = nR + ( b[s+3]<<24 & 0xff000000 );
		}
//	    }

		return nR;
	}

	public static String Int2ByteArray(int iSource, int iArrayLen) {
		byte[] bLocalArr = new byte[iArrayLen];
		for (int i = 0; (i < 4) && (i < iArrayLen); i++) {
			bLocalArr[i] = (byte) (iSource >> 8 * i & 0xFF);
		}


		String byteString = null;
		try {
			byteString = new String(bLocalArr, "utf-8");
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return byteString;
	}

	public static byte[] IntTwoByte(int iSource, int iArrayLen) {
		byte[] bLocalArr = new byte[iArrayLen];
		for (int i = 0; (i < 4) && (i < iArrayLen); i++) {
			bLocalArr[i] = (byte) (iSource >> 8 * i & 0xFF);
		}

		return bLocalArr;

	}

	public static String getStrValue(byte[] b,int s,int e)
	{
		byte[] tb=new byte[e-s+1];
		int i=0;
		for(byte ee:tb)
		{
			tb[i++]=0;
		}

		i=0;
		for(byte ee:b)
		{
			if(i>=s && i<e)
			{
				tb[i-s]= b[i];
			}
			else if(i>=e)
			{
				break;
			}
			i++;
		}
		String r=new String(tb);
		return r;
	}


	public static String binary(byte[] bytes, int radix){
		return new BigInteger(1, bytes).toString(radix);// 这里的1代表正数
	}
	public static String bytesToHexString(byte[] src){
		StringBuilder stringBuilder = new StringBuilder("");
		if (src == null || src.length <= 0) {
			return null;
		}
		for (int i = 0; i < src.length; i++) {
			int v = src[i] & 0xFF;
			String hv = Integer.toHexString(v);
			if (hv.length() < 2) {
				stringBuilder.append(0);
			}
			stringBuilder.append(hv);
		}
		return stringBuilder.toString();
	}


	// 整形 int---> byte[]
	public static byte[] toByteArray(int integer) {
		int byteNum = (40 -Integer.numberOfLeadingZeros (integer < 0 ? ~integer : integer))/ 8;
		byte[] byteArray = new byte[4];

		for (int n = 0; n < byteNum; n++)
			byteArray[3 - n] = (byte) (integer>>> (n * 8));

		return (byteArray);
	}

	//java 合并两个byte数组
	public static byte[] byteMerger(byte[] byte_1, byte[] byte_2)
	{

		byte[] byte_3 = new byte[byte_1.length+byte_2.length];
		System.arraycopy(byte_1, 0, byte_3, 0, byte_1.length);
		System.arraycopy(byte_2, 0, byte_3, byte_1.length, byte_2.length);
		return byte_3;
	}

}
