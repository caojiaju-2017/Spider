using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections;
using SpiderC.Data;

namespace SpiderC
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲


            #region 注册事件
            registerMouseEvent(loginImg);
            registerMouseEvent(imageSplide);
            registerMouseEvent(pbLoginImage);
            registerMouseEvent(spiderIcon);
            registerMouseEvent(imageLogin);
            registerMouseEvent(tbUserName);
            registerMouseEvent(tbPassword);
            registerMouseEvent(noticeLab);
            #endregion

            #region 初始化本地配置
            if (GlobalShare.CurrentConfig.RememberUserInfo)
            {
                tbUserName.Text = GlobalShare.CurrentConfig.UserName;
                tbPassword.Text = GlobalShare.CurrentConfig.UserPasswd;
                isRemeberInfo.Checked = GlobalShare.CurrentConfig.RememberUserInfo;
            }
            else
            {
                tbUserName.Text = "";
                tbPassword.Text = "";
                isRemeberInfo.Checked = false;
            }
            #endregion
        }

        public void startUpdate(string upateParams)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "ClientUpdate.exe";//需要启动的程序名     
            //p.StartInfo.Arguments = @"a  -t7z    " + cmdstr + " -mx=9   ";//启动参数 
            p.StartInfo.Arguments = upateParams;
            p.Start();//启动   
        }


        private void LoginFrm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = PublicFunction.getImageByFile("login_back.jpg");
            loginImg.BackgroundImage = PublicFunction.getImageByFile("login.png");
            loginImg.BackgroundImageLayout = ImageLayout.Stretch;

            imageSplide.BackgroundImage = PublicFunction.getImageByFile("loginsepator.png");
            imageSplide.BackgroundImageLayout = ImageLayout.Stretch;

            pbLoginImage.BackgroundImage = PublicFunction.getImageByFile("login_image.png");

            spiderIcon.BackgroundImage = PublicFunction.getImageByFile("spider.png");
            spiderIcon.BackgroundImageLayout = ImageLayout.Stretch;

            imageLogin.BackgroundImage = PublicFunction.getImageByFile("loginImage.png");
            imageLogin.BackgroundImageLayout = ImageLayout.Stretch;



            //Color textColor = Color.FromArgb(208, 37, 111);

            tbUserName.AutoSize = false;
            tbUserName.Height = 18;
            //labName.ForeColor = textColor;

            tbPassword.AutoSize = false;
            tbPassword.Height = 18;
            //labPasswd.ForeColor = textColor;

            noticeLab.Text = "1、本系统需要运行在IE8.0及以上版本\n\n2、如果您忘记您的密码，请联系客服\n\n3、如果您未曾使用过本系统，请联系客服";

            
        }


        /// <summary>
        /// 注册窗体移动事件
        /// </summary>
        /// <param name="ctrl"></param>
        private void registerMouseEvent(Control ctrl)
        {
            ctrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFrm_MouseDown);
            ctrl.MouseLeave += new System.EventHandler(this.LoginFrm_MouseLeave);
            ctrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LoginFrm_MouseMove);
            ctrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LoginFrm_MouseUp);
        }

        #region 鼠标窗口移动
        Point mMouseDown;
        Boolean mouseDownFlag = false;
        private void LoginFrm_MouseDown(object sender, MouseEventArgs e)
        {
            mMouseDown = e.Location;
            mouseDownFlag = true;
        }

        private void LoginFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDownFlag)
            {
                return;
            }
            Point currentPt = e.Location;
            this.Location = new Point(this.Location.X + (currentPt.X - mMouseDown.X), this.Location.Y + (currentPt.Y - mMouseDown.Y));
        }

        private void LoginFrm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownFlag = false;
        }

        private void LoginFrm_MouseLeave(object sender, EventArgs e)
        {
            mouseDownFlag = false;
        }
        #endregion

        #region 按钮点击事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            this.Cursor = Cursors.Arrow;

            // 存储配置
            GlobalShare.CurrentConfig.UserName = tbUserName.Text;
            GlobalShare.CurrentConfig.UserPasswd = tbPassword.Text;
            GlobalShare.CurrentConfig.RememberUserInfo = isRemeberInfo.Checked;

            LocalConfig.reBuildConfigData(GlobalShare.CurrentConfig);

            MainFrm frm = new MainFrm();

            frm.Show();
            this.Hide();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            tbUserName.Text = "";
            tbPassword.Text = "";
            isRemeberInfo.Checked = false;
        }

        private void forgetPasswd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }
        #endregion




    }
}
