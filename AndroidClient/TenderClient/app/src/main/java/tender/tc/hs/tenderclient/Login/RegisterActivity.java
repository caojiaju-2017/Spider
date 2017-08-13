package tender.tc.hs.tenderclient.Login;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;
import java.util.Random;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import tender.tc.hs.tenderclient.R;
import tender.tc.hs.tenderclient.Util.Utils;

//注册
public class RegisterActivity extends Activity implements OnClickListener {
	private TextView txt_title;
	private ImageView img_back;
	private Button btn_register, btn_send;
	private EditText et_usertel, et_password, et_code;
	private MyCount mc;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		setContentView(R.layout.activity_register);
		super.onCreate(savedInstanceState);
		btn_register = (Button)findViewById(R.id.btn_register);
		btn_send = (Button)findViewById(R.id.btn_send);
		txt_title = (EditText)findViewById(R.id.et_usertel);
		et_usertel = (EditText)findViewById(R.id.et_usertel);
		et_password = (EditText)findViewById(R.id.et_password);
		et_code = (EditText) findViewById(R.id.et_code);
		setListener();
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
					mc = new MyCount(10000, 1000); // 第一参数是总的时间，第二个是间隔时间
				}
				mc.start();
//				getCode();
				break;
			case R.id.btn_register:
//				getRegister();
				Toast.makeText(RegisterActivity.this, "sendRegister command", Toast.LENGTH_LONG).show();
				break;
			default:
				break;
		}
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
			et_code.setText(gernalCode(et_usertel.getText().toString()));
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
}
