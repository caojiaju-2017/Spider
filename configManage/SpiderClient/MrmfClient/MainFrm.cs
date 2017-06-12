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
using SpiderC.HSControl;
using SpiderC.HSControl.Config;
using SpiderC.HSControl.Logger;
using SpiderC.HSControl.Priv;
using SpiderC.HSControl.Order;
using SpiderC.HSControl.Custom.Terminal;

namespace SpiderC
{
    public partial class MainFrm : Form
    {
        #region 变量
        Panel titlePanel;
        int titleHeight = 50;

        int treeWidth = 220;
        Label corpName;
        BaseTreeView systemTree;

        NativeBar bar;

        // 
        SystemSetting setCtrl;

        LoggerPannel logPanel;

        GrantControl grantCtrl;
        RoleControl roleCtrl;
        OrderControl orderControl;
        TerminalControl terminalCtrl;
        #endregion

        #region 界面初始化
        public MainFrm()
        {
            InitializeComponent();

            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            this.Width = 1080;
            this.Height = 668;
            buildControl();
        }



        private void LoginFrm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = PublicFunction.getImageByFile("login_back.jpg");

            loadTreeNode();
            
            // 初始化bar标题
            bar.buildControl(null);

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //BackColor = Color.Black;
            //TransparencyKey = Color.Black;
        }

        private void loadTreeNode()
        {
            TreeNode home = new TreeNode();
            home.Text = "主页";
            home.ImageIndex = 0;
            systemTree.Nodes.Add(home);

            TreeNode product = new TreeNode();
            product.Text = "产品与服务";
            product.ImageIndex = 1;
            systemTree.Nodes.Add(product);

            TreeNode spider = new TreeNode();
            spider.Text = "核心管理";
            spider.ImageIndex = 2;
            systemTree.Nodes.Add(spider);

            TreeNode custom = new TreeNode();
            custom.Text = "客户管理";
            custom.ImageIndex = 3;
            systemTree.Nodes.Add(custom);
#region sub
            TreeNode sub1 = new TreeNode();
            sub1.Text = "终端客户管理";
            sub1.ImageIndex = 6;
            custom.Nodes.Add(sub1);

            TreeNode sub2 = new TreeNode();
            sub2.Text = "代理商管理";
            sub2.ImageIndex = 7;
            custom.Nodes.Add(sub2);
#endregion


            TreeNode order = new TreeNode();
            order.Text = "订单管理";
            order.ImageIndex = 4;
            systemTree.Nodes.Add(order);

            TreeNode priv = new TreeNode();
            priv.Text = "权限管理";
            priv.ImageIndex = 5;
            systemTree.Nodes.Add(priv);
#region sub
            TreeNode sub3 = new TreeNode();
            sub3.Text = "角色管理";
            sub3.ImageIndex = 6;
            priv.Nodes.Add(sub3);

            TreeNode sub4 = new TreeNode();
            sub4.Text = "用戶授權";
            sub4.ImageIndex = 7;
            priv.Nodes.Add(sub4);
#endregion


            TreeNode logger = new TreeNode();
            logger.Text = "日志管理";
            logger.ImageIndex = 6;
            systemTree.Nodes.Add(logger);

            TreeNode system = new TreeNode();
            system.Text = "系统设置";
            system.ImageIndex = 7;
            systemTree.Nodes.Add(system);

        }

        private void MainFrm_Resize(object sender, EventArgs e)
        {
            changeControlPos();
        }

        /// <summary>
        /// 动态调整空间位置
        /// </summary>
        private void changeControlPos()
        {
            if (titlePanel != null)
            {
                titlePanel.Location = new Point(0, 0);
                titlePanel.Width = this.Width;
                titlePanel.Height = titleHeight;

                // build Sub title
                buildTitleSubControl();
            }

            if (corpName != null)
            {
                corpName.Location = new Point(0, titleHeight);
                corpName.Width = treeWidth;
                corpName.Height = 40;
            }

            if(systemTree != null)
            {
                systemTree.Location = new Point(0, titleHeight + 40 + 2);
                systemTree.Width = treeWidth;
                systemTree.Height = this.Height - systemTree.Location.Y  ;
            }

            if (bar != null)
            {
                bar.Location = new Point(systemTree.Width + 1, corpName.Top);
                bar.Size = new Size(this.Width - corpName.Width - 1, corpName.Height);
            }

            #region 工作区 位置尺寸一致
            if (setCtrl != null)
            {
                setCtrl.Location = new Point(systemTree.Right, bar.Bottom);
                setCtrl.Size = new Size(this.Width - systemTree.Width, this.Height - bar.Bottom);
            }

            if (logPanel != null)
            {
                logPanel.Location = new Point(systemTree.Right, bar.Bottom);
                logPanel.Size = new Size(this.Width - systemTree.Width, this.Height - bar.Bottom);
            }

            if (grantCtrl != null)
            {
                grantCtrl.Location = new Point(systemTree.Right, bar.Bottom);
                grantCtrl.Size = new Size(this.Width - systemTree.Width, this.Height - bar.Bottom);
            }

            if (roleCtrl != null)
            {
                roleCtrl.Location = new Point(systemTree.Right, bar.Bottom);
                roleCtrl.Size = new Size(this.Width - systemTree.Width, this.Height - bar.Bottom);
            }

            if (orderControl != null)
            {
                orderControl.Location = new Point(systemTree.Right, bar.Bottom);
                orderControl.Size = new Size(this.Width - systemTree.Width, this.Height - bar.Bottom);
            }

            if (terminalCtrl != null)
            {
                terminalCtrl.Location = new Point(systemTree.Right, bar.Bottom);
                terminalCtrl.Size = new Size(this.Width - systemTree.Width, this.Height - bar.Bottom);
            }

            #endregion
        }

        /// <summary>
        /// 创建标题栏的子空间
        /// </summary>
        private void buildTitleSubControl()
        {
            // 标题栏图标
            PictureBox box = new PictureBox();
            box.Size = new Size(40, 40);
            box.BackgroundImage = PublicFunction.getImageByFile("spider.png");
            box.BackgroundImageLayout = ImageLayout.Stretch;
            box.Location = new Point(10, 5);
            titlePanel.Controls.Add(box);
            registerMouseEvent(box);

            // 标题栏，系统名字
            Label systemName = new Label();
            systemName.AutoSize = false;
            systemName.Text = "数据中心";
            systemName.ForeColor = Color.White;
            titlePanel.Controls.Add(systemName);
            systemName.Location = new Point(55, 5);
            systemName.TextAlign = ContentAlignment.MiddleLeft;
            systemName.Width = 200;
            systemName.Height = 40;
            systemName.Font = new System.Drawing.Font("仿宋", 14, FontStyle.Bold);
            systemName.BackColor = Color.Transparent;
            registerMouseEvent(systemName);


            // 注册用户显示控件
            UserInfoMenu userMenu = new UserInfoMenu();
            userMenu.BackColor = ColorTranslator.FromHtml("#62A8D1");
            titlePanel.Controls.Add(userMenu);
            userMenu.Location = new Point(this.Width - 152, 2);
            userMenu.Size = new Size(150, 46);
            userMenu.buildUserInfo(null);

        }
       
        /// <summary>
        /// 动态创建控件
        /// </summary>
        private void buildControl()
        {
            titlePanel = new Panel();
            titlePanel.BackColor = ColorTranslator.FromHtml("#438EB9");
            this.Controls.Add(titlePanel);
            // 注册鼠标事件到窗体

            registerMouseEvent(titlePanel);

            corpName = new Label();
            corpName.AutoSize = false;
            corpName.Text = "上海数码科技";
            corpName.TextAlign = ContentAlignment.MiddleCenter;
            corpName.ForeColor = Color.Black;
            corpName.BackColor = Color.White ;
            this.Controls.Add(corpName);

            systemTree = new BaseTreeView();
            this.Controls.Add(systemTree);
            systemTree.BorderStyle = BorderStyle.None;
            systemTree.ItemHeight = 60;
            systemTree.Font = new Font("宋体", 10);
            systemTree.ImageList = initImages();
            systemTree.NodeMouseClick += systemTree_NodeMouseClick;
            systemTree.Invalidate();

            bar = new NativeBar();
            this.Controls.Add(bar);

            // 
            setCtrl = new SystemSetting();
            this.Controls.Add(setCtrl);
            setCtrl.Visible = false;
            setCtrl.buildControl();

            logPanel = new LoggerPannel();
            this.Controls.Add(logPanel);
            logPanel.Visible = false;
            logPanel.buildControl();


            roleCtrl = new RoleControl();
            grantCtrl = new GrantControl();
            this.Controls.Add(roleCtrl);
            this.Controls.Add(grantCtrl);
            roleCtrl.Visible = false;
            grantCtrl.Visible = false;
            roleCtrl.buildControl();
            grantCtrl.buildControl();


            orderControl = new OrderControl();
            this.Controls.Add(orderControl);
            orderControl.buildControl();
            orderControl.Visible = false;

            terminalCtrl = new TerminalControl();
            this.Controls.Add(terminalCtrl);
            terminalCtrl.buildControl();
            terminalCtrl.Visible = false;
        }

        private ImageList initImages()
        {
            ImageList imgList = new ImageList();
            imgList.Images.Clear();
            imgList.Images.Add(PublicFunction.getImageByFile("1.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("2.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("3.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("4.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("5.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("6.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("7.png"));
            imgList.Images.Add(PublicFunction.getImageByFile("8.png"));


            return imgList;
        }

        /// <summary>
        /// 注册窗体移动事件
        /// </summary>
        /// <param name="ctrl"></param>
        private void registerMouseEvent(Control ctrl)
        {
            ctrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseDown);
            ctrl.MouseLeave += new System.EventHandler(this.Frm_MouseLeave);
            ctrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseMove);
            ctrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseUp);
        }

        #endregion

        #region 鼠标窗口移动
        Point mMouseDown;
        Boolean mouseDownFlag = false;
        private void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            mMouseDown = e.Location;
            mouseDownFlag = true;
        }

        private void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDownFlag)
            {
                return;
            }
            Point currentPt = e.Location;
            this.Location = new Point(this.Location.X + (currentPt.X - mMouseDown.X), this.Location.Y + (currentPt.Y - mMouseDown.Y));
        }

        private void Frm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownFlag = false;
        }

        private void Frm_MouseLeave(object sender, EventArgs e)
        {
            mouseDownFlag = false;
        }
        #endregion


        private void systemTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (!e.Node.IsExpanded)
                {
                    e.Node.ExpandAll();
                }

                return;
            }
            //
            List<TreeNode> nodes = new List<TreeNode>();

            TreeNode oneNode = e.Node;
            nodes.Add(oneNode);
            while (oneNode.Parent != null)
            {
                nodes.Add(oneNode.Parent);

                oneNode = oneNode.Parent;
            }

            // 设置bar
            bar.setBar(nodes);

            ResetControl(false);
            if (e.Node.Text == "系统设置")
            {
                // 装载页面
                setCtrl.Visible = true;
            }
            else if (e.Node.Text == "日志管理")
            {
                logPanel.Visible = true;
            }
            else if (e.Node.Text == "角色管理")
            {
                roleCtrl.Visible = true;
            }
            else if (e.Node.Text == "用戶授權")
            {
                grantCtrl.Visible = true;
            }
            else if (e.Node.Text == "订单管理")
            {
                orderControl.Visible = true;
            }
            else if (e.Node.Text == "终端客户管理")
            {
                terminalCtrl.Visible = true;
            }
        }

        private void ResetControl(bool pFlag)
        {
            if (setCtrl != null)
            {
                setCtrl.Visible = pFlag;
            }

            if (logPanel != null)
            {
                logPanel.Visible = pFlag;
            }

            if (roleCtrl != null)
            {
                roleCtrl.Visible = false;
            }

            if (grantCtrl != null)
            {
                grantCtrl.Visible = false;
            }

            if (orderControl != null)
            {
                orderControl.Visible = false;
            }

            if (terminalCtrl != null)
            {
                terminalCtrl.Visible = false;
            }
        }

    }
}
