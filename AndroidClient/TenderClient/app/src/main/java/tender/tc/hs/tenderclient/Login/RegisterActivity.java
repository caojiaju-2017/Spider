package tender.tc.hs.tenderclient.Login;

import android.app.Activity;
import android.app.Dialog;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.Handler;
import android.os.Message;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.DisplayMetrics;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.util.HashMap;
import java.util.Map;
import java.util.Random;

import tender.tc.hs.tenderclient.HsHttp.CommandDefine;
import tender.tc.hs.tenderclient.HsHttp.HttpAccess;
import tender.tc.hs.tenderclient.R;
import tender.tc.hs.tenderclient.Util.HsUtils;
import tender.tc.hs.tenderclient.Util.LogUtil;
import tender.tc.hs.tenderclient.Util.Utils;

//注册
public class RegisterActivity extends Activity implements OnClickListener {
	private TextView txt_title;
	private ImageView img_back;
	private Button btn_register, btn_send;
	private EditText et_usertel, et_password, et_code;
	private MyCount mc;


	Handler mainHandlers = null;
	private Dialog mRegisterHandle;
	@Override
	public void onCreate(Bundle savedInstanceState) {
		setContentView(R.layout.activity_register);
		super.onCreate(savedInstanceState);
		registerComminucation();
		initLoginingDlg();

		btn_register = (Button)findViewById(R.id.btn_register);
		btn_send = (Button)findViewById(R.id.btn_send);
		txt_title = (EditText)findViewById(R.id.et_usertel);
		et_usertel = (EditText)findViewById(R.id.et_usertel);
		et_password = (EditText)findViewById(R.id.et_password);
		et_code = (EditText) findViewById(R.id.et_code);
		setListener();
	}

	/* 显示正在登录对话框 */
	private void showLoginingDlg() {
		if (mRegisterHandle != null)
			mRegisterHandle.show();
	}

	/* 关闭正在登录对话框 */
	private void closeLoginingDlg() {
		if (mRegisterHandle != null && mRegisterHandle.isShowing())
			mRegisterHandle.dismiss();
	}
	/* 初始化正在登录对话框 */
	private void initLoginingDlg() {

		mRegisterHandle = new Dialog(this, R.style.loginingDlg);
		mRegisterHandle.setContentView(R.layout.logining_dlg);

		Window window = mRegisterHandle.getWindow();
		WindowManager.LayoutParams params = window.getAttributes();
		// 获取和mLoginingDlg关联的当前窗口的属性，从而设置它在屏幕中显示的位置

		// 获取屏幕的高宽
		DisplayMetrics dm = new DisplayMetrics();
		getWindowManager().getDefaultDisplay().getMetrics(dm);
		int cxScreen = dm.widthPixels;
		int cyScreen = dm.heightPixels;

		int height = (int) getResources().getDimension(
				R.dimen.loginingdlg_height);// 高42dp
		int lrMargin = (int) getResources().getDimension(
				R.dimen.loginingdlg_lr_margin); // 左右边沿10dp
		int topMargin = (int) getResources().getDimension(
				R.dimen.loginingdlg_top_margin); // 上沿20dp

		params.y = (-(cyScreen - height) / 2) + topMargin; // -199
		/* 对话框默认位置在屏幕中心,所以x,y表示此控件到"屏幕中心"的偏移量 */

		params.width = cxScreen;
		params.height = height;
		// width,height表示mLoginingDlg的实际大小

		mRegisterHandle.setCanceledOnTouchOutside(true); // 设置点击Dialog外部任意区域关闭Dialog
	}
	protected void setListener() {
		btn_register.setOnClickListener(this);
		btn_send.setOnClickListener(this);
//		et_usertel.addTextChangedListener(new TelTextChange());
//		et_password.addTextChangedListener(new TextChange());
		et_usertel.addTextChangedListener(new TextWatcher() {
			@Override
			public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

			}

			@Override
			public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
				String phone = et_usertel.getText().toString();
				if (phone.length() == 11) {
					if (Utils.isMobileNO(phone))
					{
						btn_send.setEnabled(true);
					}
					else
					{
						if (mc != null)
						{
							mc.cancel();
						}
						et_code.setText("");
						btn_send.setEnabled(false);
						btn_send.setText("发送验证码");
						btn_register.setEnabled(false);
					}
				}
				else
				{
					if (mc != null)
					{
						mc.cancel();
					}
					et_code.setText("");
					btn_send.setEnabled(false);
					btn_send.setText("发送验证码");
					btn_register.setEnabled(false);
				}
			}

			@Override
			public void afterTextChanged(Editable editable) {

			}
		});




		et_password.addTextChangedListener(new TextWatcher() {
			@Override
			public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

			}

			@Override
			public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
				String password = et_password.getText().toString();
				if (password.length() >= 6)
				{
					btn_register.setEnabled(true);
				}
				else
				{
					btn_register.setEnabled(false);
				}

			}

			@Override
			public void afterTextChanged(Editable editable) {

			}
		});
	}

	@Override
	public void onClick(View view) {
		switch (view.getId()) {
			case R.id.btn_send:
				if (mc == null) {
					mc = new MyCount(60000, 1000); // 第一参数是总的时间，第二个是间隔时间
				}
				mc.start();

				showLoginingDlg();
				getCode();
				break;
			case R.id.btn_register:
//				getRegister();
				showLoginingDlg();

				beginRegister();
				break;
			default:
				break;
		}
	}

	private void getCode() {
		LogUtil.info("Invoke httpaccess");
		final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.APPLY_SMSCODE);

		final Map<String,String> dataMap = new HashMap<>();
		try {
			JSONObject jsonObject = new JSONObject();

			String account = et_usertel.getText().toString();
			account = account.trim();

			jsonObject.put("Phone",account);

			access.setJsonObject(jsonObject);
		} catch (JSONException e) {
			LogUtil.info("Invoke httpaccess prepare failed");
			e.printStackTrace();
		}
		new Thread(new Runnable(){
			public void run()
			{
				access.HttpPost();
			}
		}).start();
	}

	private void beginRegister() {
		LogUtil.info("Invoke httpaccess");
		final HttpAccess access = new HttpAccess(mainHandlers, CommandDefine.REG_ACCOUNT);

		final Map<String,String> dataMap = new HashMap<>();
		try {
			JSONObject jsonObject = new JSONObject();

			String password = et_password.getText().toString();
			String account = et_usertel.getText().toString();
			String smscode = et_code.getText().toString();


			account = account.trim();
			password = password.trim();
			password = HsUtils.Md5(String.format("%s%s%s","h",password,"s"));

			jsonObject.put("Account",account);
			jsonObject.put("Password",password);
			jsonObject.put("SmsCode",smscode);

//			jsonObject.put("Longitude","");
//			jsonObject.put("Latitude","");
//			jsonObject.put("Terminal","Android");

			access.setJsonObject(jsonObject);
		} catch (JSONException e) {
			LogUtil.info("Invoke httpaccess prepare failed");
			e.printStackTrace();
		}
		new Thread(new Runnable(){
			public void run()
			{
				access.HttpPost();
			}
		}).start();

	}

	/* 定义一个倒计时的内部类 */
	private class MyCount extends CountDownTimer {
		public MyCount(long millisInFuture, long countDownInterval) {
			super(millisInFuture, countDownInterval);
		}

		@Override
		public void onFinish() {
			btn_send.setEnabled(true);
			btn_send.setText("发送验证码");
//			et_code.setText(gernalCode(et_usertel.getText().toString()));
		}

		@Override
		public void onTick(long millisUntilFinished) {
			btn_send.setEnabled(false);
			btn_send.setText("(" + millisUntilFinished / 1000 + ")秒");
		}
	}

	private String gernalCode(String sPhone)
	{
		int s = new Random().nextInt(9999)%(9999-1000+1) + 1000;
		return String.format("%d",s);
	}

	private void registerComminucation() {
		//        iDCardDevice=new publicSecurityIDCardLib(this);
		// 主线程通信事件句柄
		mainHandlers = new Handler() {
			@Override
			public void handleMessage(Message msg) {
				switch (msg.what) {
					case CommandDefine.REG_ACCOUNT:
					{
						String receiveData = msg.obj.toString();

						JSONTokener jsonParser = new JSONTokener(receiveData);
						try {
							JSONObject person = (JSONObject) jsonParser.nextValue();

							String errorinfo = person.getString("ErrorInfo");
							int errorId = Integer.parseInt(person.getString("ErrorId"));
							if (errorId == 200) {
								closeLoginingDlg();// 关闭对话框
								Toast.makeText(RegisterActivity.this, "注册成功", Toast.LENGTH_SHORT).show();

								// 跳转Activity

								finish();
								break;
							}
							else
							{
								closeLoginingDlg();// 关闭对话框
								Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
							}

						} catch (JSONException e) {
							e.printStackTrace();
						}
						break;
					}
					case CommandDefine.APPLY_SMSCODE:
					{
						String receiveData = msg.obj.toString();

						JSONTokener jsonParser = new JSONTokener(receiveData);
						try {
							JSONObject person = (JSONObject) jsonParser.nextValue();

							String errorinfo = person.getString("ErrorInfo");
							int errorId = Integer.parseInt(person.getString("ErrorId"));
							if (errorId == 200) {
								closeLoginingDlg();// 关闭对话框
								Toast.makeText(RegisterActivity.this, "验证码请求成功", Toast.LENGTH_SHORT).show();
								break;
							}
							else
							{
								closeLoginingDlg();// 关闭对话框
								Toast.makeText(getApplicationContext(), errorinfo, Toast.LENGTH_LONG).show();
							}

						} catch (JSONException e) {
							e.printStackTrace();
						}
						break;
					}
				}
			}
		};
	}
}
