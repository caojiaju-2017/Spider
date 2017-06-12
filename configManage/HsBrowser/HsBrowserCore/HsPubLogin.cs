using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using HsServiceCore;
using Hs.Comminucation;
using Newtonsoft.Json.Linq;

namespace HsServiceCore
{
    public partial class HsPubLogin : UserControl
    {
        // 1680  1104
        public HsPubLogin()
        {
            InitializeComponent();

            //this.Dock = DockStyle.Fill;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            // 背景图
            int backImageHd = (int)(this.Height * 0.6);
            int backImageWd = (int)((float)(backImageHd * 1680) / 1104);
            int startX = (this.Width - backImageWd) / 2;
            int startY = (this.Height - backImageHd) / 2;
            string srcPath = Path.Combine(Application.StartupPath, "Res");
            Image backImage = Image.FromFile(Path.Combine(srcPath, "loginBack.jpg"));
            graph.DrawImage(backImage, new Rectangle(startX, startY, backImageWd, backImageHd));

            // 登陆框
            int lagImageHd = (int)(backImageHd * 0.4);
            int lagImageWd = (int)((float)(lagImageHd * 360) / 302);
            int startLogX = (backImageWd - lagImageWd) / 2 + startX;
            int startLogY = (backImageHd - lagImageHd) / 2 + startY;
            Image logImage = Image.FromFile(Path.Combine(srcPath, "login_frame.png"));
            graph.DrawImage(logImage, new Rectangle(startLogX, startLogY, lagImageWd, lagImageHd));

            base.OnPaint(e);
        }

        private void HsPubLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool result = send_LoginCommand();
            if (!result)
            {
                return;
            }


            GlobalConfig.LoginState = true;
            HsWebForm.g_WebForm.openInitWorkSpace();
        }

        private Boolean send_LoginCommand()
        {
            HsApply applyLogin = new HsApply(GlobalConfig.getUrlHead(), "LOGIN_SYSTEM");
            applyLogin.InvokeType = InvType.POST;
            HsParam param1 = new HsParam("Account", userName.Text.ToString(), DataType.String, InvType.POST);

            HsParam param2 = new HsParam("Password", ToolsUtils.Md5(userPassword.Text), DataType.String, InvType.POST);
            applyLogin.addParam(param1);
            applyLogin.addParam(param2);

            HsResultData resultDat = applyLogin.excuteCommand();

            //GlobalConfig.SystemProtocols.Clear();
            if (resultDat.ErrorId == 200)
            {
                //JArray rtnRolesArray = (JArray)resultDat.ResultString;

                //for (int index = 0; index < rtnRolesArray.Count; index++)
                //{
                //    JObject jo = JObject.Parse(rtnRolesArray[index].ToString());
                //    Dictionary<string, object> rtnResults = jo.ToObject<Dictionary<string, object>>();

                //    HsProtocol pro = new HsProtocol();
                //    pro.Command = rtnResults["Command"].ToString();
                //    pro.Apilab = rtnResults["ApiLab"].ToString();

                //    GlobalConfig.SystemProtocols.Add(pro);
                //}

                return true;
            }
            MessageBox.Show(resultDat.ErrorInfo);
            return false;
        }

        private void userName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("key down1 ");
            if (e.KeyCode == Keys.Return)
            {
                btnLogin.PerformClick();
            }
            
        }

        private void userPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("key down 2");
            if (e.KeyCode == Keys.Return)
            {
                btnLogin.PerformClick();
            }
        }

    }
}
