package tender.tc.hs.tenderclient;

import java.util.Map;

import android.app.AlertDialog;
import android.content.Context;

import org.apache.commons.collections.map.HashedMap;

public class HsProgressDialog extends Object
{
	private static HsProgressDialog shareWaitHandle;
	private AlertDialog shareWaitHandleDialog;
	Context mxContext;
	@SuppressWarnings("unchecked")
	private static Map<String,HsProgressDialog> m_singleDialogList = new HashedMap();
	private HsProgressDialog() {
		// TODO Auto-generated constructor stub  操作成功

        HsProgressDialog.shareWaitHandle = this;
	}

	public static HsProgressDialog getShareInstance()
	{
		if (shareWaitHandle == null) {
			shareWaitHandle = new HsProgressDialog();
		}
		
		return shareWaitHandle;
	}
	public static HsProgressDialog getProgressDlg()
	{
		return HsProgressDialog.getShareInstance();
	}
	public static HsProgressDialog newProgressDlg()
	{
		return HsProgressDialog.getShareInstance();
	}
	
	public void show(Context context)
	{
		AlertDialog myDialog = new AlertDialog.Builder(context).create();

		myDialog.setCancelable(false);
		this.shareWaitHandleDialog = myDialog;
		this.shareWaitHandleDialog.show();
		this.shareWaitHandleDialog.getWindow().setContentView(R.layout.wait);
        
	}
	public void Close() 
	{
		if (this.shareWaitHandleDialog != null)
		{
		this.shareWaitHandleDialog.hide();
		this.shareWaitHandleDialog.dismiss();
		}
	}

}
