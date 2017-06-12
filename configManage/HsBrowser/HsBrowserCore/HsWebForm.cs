using HsServiceCore.Service;
using HsServiceCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace HsServiceCore
{
    public partial class HsWebForm : Form
    {
        public static HsWebForm g_WebForm;
        HsWebTitle mainTitle = null;

        // bar
        HsWebBar mainBar = new HsWebBar();
        int mainBarHeight = 30;
        int titleHeigh = 20;

        int formBorderWd = 2;

        // 地址栏
        HsFastLink mainFastLink = null;

        //会频繁切换的窗体
        HsPubLogin login_Form;
        WebBrowser web_Broswer;
        public HsMainWorkSpace main_workSpace;
        public HsWebForm()
        {
            InitializeComponent();
            HsWebForm.g_WebForm = this;
        }

        private void HsWebForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(240, 240, 240);
            // 加载title
            mainTitle = new HsWebTitle(this);
            mainTitle.Location = new Point(this.Width - mainTitle.Width - formBorderWd, formBorderWd);
            this.Controls.Add(mainTitle);

            // 加载Bar
            mainBar.Location = new Point(formBorderWd, titleHeigh);
            mainBar.Height = mainBarHeight;
            mainBar.Width = this.Width - 2 * formBorderWd;

            TabItem item = new TabItem();
            item.Name = "登录";
            item.Url = String.Format("{0}://{1}", GlobalConfig.ProtocolPre, GlobalConfig.LoginAddress);
            mainBar.addItem(item);
            this.Controls.Add(mainBar);

            //
            mainFastLink = new HsFastLink(mainBar.getCurrentItem());
            mainFastLink.Location = new Point(formBorderWd, mainBar.Location.Y + mainBar.Height + 1);
            mainFastLink.Width = this.Width - 3 * formBorderWd;
            this.Controls.Add(mainFastLink);            

            this.Text = GlobalConfig.ServerName;

            loadWorkSpace(mainBar.getCurrentItem());

            refreshUi();
        }

        private void loadWorkSpace(TabItem tabItem)
        {

            // 如果未登录, 不是普通网页访问，也不是空白页
            if (!checkLoginState() && !tabItem.isNormalWeb() && !tabItem.isBlank() || tabItem.isLoginFrm())
            {
                if (web_Broswer != null)
                {
                    web_Broswer.Hide();
                }

                if (main_workSpace != null)
                {
                    main_workSpace.Hide();
                }

                showLoginFrm();
                return;
            }

            if (tabItem.isLoginFrm())
            {
                if (web_Broswer != null)
                {
                    web_Broswer.Hide();
                }
                if (main_workSpace != null)
                {
                    main_workSpace.Hide();
                }
                showLoginFrm();
                return;
            }
            else if (tabItem.isNormalWeb())
            {
                if (login_Form != null)
                {
                    login_Form.Hide();
                }
                if (main_workSpace != null)
                {
                    main_workSpace.Hide();
                }
                showWebBrowser(tabItem.Url);
            }
            else if (tabItem.isBlank())
            {
                if (web_Broswer != null)
                {
                    web_Broswer.Hide();
                }
                
                if (login_Form != null)
                {
                    login_Form.Hide();
                }
                if (main_workSpace!= null)
                {
                    main_workSpace.Hide();
                }
                
            }
            else
            {
                if (web_Broswer != null)
                {
                    web_Broswer.Hide();
                }

                if (login_Form != null)
                {
                    login_Form.Hide();
                }
               
                showMainWorkSpace();
            }
            
        }

        private bool checkLoginState()
        {
            return GlobalConfig.LoginState;
        }

        #region 窗体刷新
        private void showLoginFrm()
        {
            // 
            if (login_Form == null)
            {
                login_Form = new HsPubLogin();
                login_Form.Dock = DockStyle.Fill;
                this.Controls.Add(login_Form);
            }
            login_Form.Location = new Point(formBorderWd, mainFastLink.Location.Y + mainFastLink.Height + 1);
            login_Form.Width = this.Width - 3;
            login_Form.Height = this.Height - mainFastLink.Location.Y - mainFastLink.Height;
            login_Form.Visible = true;
        }

        private void HsWebForm_Resize(object sender, EventArgs e)
        {
            if (mainTitle != null)
            {
                mainTitle.Location = new Point(this.Width - mainTitle.Width - formBorderWd, formBorderWd);
            }


            if (mainBar != null)
            {
                mainBar.Location = new Point(2, titleHeigh);
                mainBar.Height = mainBarHeight;
                mainBar.Width = this.Width - 2 * formBorderWd;
            }

            if (mainFastLink != null)
            {
                mainFastLink.Location = new Point(formBorderWd, mainBar.Location.Y + mainBar.Height + 1);
                mainFastLink.Width = this.Width - 3 * formBorderWd;
            }


            refreshUi();
            
        }
        private void refreshUi()
        {
            this.Invalidate();

            if (mainTitle != null)
            {
                mainTitle.Invalidate();
            }
            
            mainBar.Invalidate();

            if (login_Form != null && login_Form.Visible == true)
            {
                showLoginFrm();
            }

            if (web_Broswer != null && web_Broswer.Visible == true)
            {
                showWebBrowser(null);
            }
        }


        #endregion

        #region 鼠标窗口移动
        Point mMouseDown;
        Boolean mouseDownFlag = false;

        private void CustomMouseDown(object sender, MouseEventArgs e)
        {
            mMouseDown = e.Location;
            mouseDownFlag = true;
        }

        private void CustomMouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDownFlag)
            {
                return;
            }
            Point currentPt = e.Location;
            this.Location = new Point(this.Location.X + (currentPt.X - mMouseDown.X), this.Location.Y + (currentPt.Y - mMouseDown.Y));
        }

        private void CustomMouseUp(object sender, MouseEventArgs e)
        {
            mouseDownFlag = false;
        }

        private void CustomMouseLeave(object sender, EventArgs e)
        {
            mouseDownFlag = false;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            Point startPt = new Point(1, 1);
            Size formSize = new Size(this.Width - 4, this.Height - 3);
            Rectangle rect = new Rectangle(startPt,formSize);
            graph.DrawRectangle(new Pen(Color.Gray,1),rect);

            base.OnPaint(e);
        }

        internal void nagivationUrl(string pUrl)
        {
            // 判断链接是不是正常web
            showWebBrowser(pUrl);

            // 判断是不是登录界面

            // 判断是不是
            //showLoginFrm();
            Console.WriteLine(pUrl);

        }

        #region 访问正常的web
        private void showWebBrowser(string url)
        {
            if (web_Broswer == null)
            {
                web_Broswer = new WebBrowser();
                this.Controls.Add(web_Broswer);

                web_Broswer.NewWindow += new System.ComponentModel.CancelEventHandler(this.wbView_NewWindow);
                web_Broswer.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbView_DocumentView);

            }
            web_Broswer.Location = new Point(formBorderWd, mainFastLink.Location.Y + mainFastLink.Height + 1);
            web_Broswer.Width = this.Width - 3;
            web_Broswer.Height = this.Height - mainFastLink.Location.Y - mainFastLink.Height;
            web_Broswer.Visible = true;
            if (url != null)
            {
                web_Broswer.Navigate(url);
            }
            
        }

        // 屏蔽错误事件
        private void wbView_DocumentView(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((System.Windows.Forms.WebBrowser)sender).ScriptErrorsSuppressed = true;
            mainFastLink.CurrentShowItem.Name = web_Broswer.DocumentTitle;

            mainBar.updateTab(mainFastLink.CurrentShowItem);
            //Console.WriteLine("i receive");
        }

        // 在本软件开启窗口
        private void wbView_NewWindow(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.WebBrowser wbView = (System.Windows.Forms.WebBrowser)sender;
            e.Cancel = true;

            string nativeUrl = wbView.StatusText;

            Console.WriteLine(nativeUrl);

            wbView.Navigate(nativeUrl);

        }
        #endregion

        #region 页签变化
        internal void TabChange(TabItem tabItem)
        {
            mainFastLink.CurrentShowItem = tabItem;
            loadWorkSpace(tabItem);
        }

        internal void updateTableItem(TabItem item)
        {
            mainBar.updateTab(item);
            loadWorkSpace(item);
        }
        #endregion

        private void HsWebForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

            //this.Invalidate();
        }

        internal void openInitWorkSpace()
        {
            // 登录后
            mainFastLink.CurrentShowItem.Name = "后台管理";
            mainFastLink.CurrentShowItem.Url = GlobalConfig.ManageAddress;
            mainFastLink.CurrentShowItem = mainFastLink.CurrentShowItem;

            mainBar.updateTab(mainFastLink.CurrentShowItem);
            loadWorkSpace(mainFastLink.CurrentShowItem);
        }


        private void showMainWorkSpace()
        {
            if (main_workSpace == null)
            {
                main_workSpace = new HsMainWorkSpace();
                this.Controls.Add(main_workSpace);

            }
            main_workSpace.Location = new Point(formBorderWd, mainFastLink.Location.Y + mainFastLink.Height + 1);
            main_workSpace.Width = this.Width - 3;
            main_workSpace.Height = this.Height - mainFastLink.Location.Y - mainFastLink.Height;
            main_workSpace.Visible = true;
            
        }

        #region 加载点击后的数据
        internal void loadNewCtrl(ServiceItem item)
        {
            // 是否当前加载了控件 ？
            closeCurrentCtrl();

            // 加载新内容
            loadNewItem(item);

            // 更新URL地址
            updateUrl(item);
        }

        private void updateUrl(ServiceItem item)
        {
            
        }

        private void loadNewItem(ServiceItem item)
        {
            main_workSpace.loadData(item);
        }

        private void closeCurrentCtrl()
        {
            main_workSpace.clearCurrentData();
        }
        #endregion
    }
}
