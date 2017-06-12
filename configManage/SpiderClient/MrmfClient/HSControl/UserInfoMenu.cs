using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC.HSControl
{
    public partial class UserInfoMenu : UserControl
    {
        public UserInfoMenu()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        public void buildUserInfo(object userInfo)
        {

            // 头像
            PictureBox box = new PictureBox();
            box.Location = new Point(5, 3);
            box.BackgroundImage = PublicFunction.getImageByFile("login.png");
            box.BackgroundImageLayout = ImageLayout.Stretch;
            box.Size = new Size(40, 40);
            this.Controls.Add(box);

            // 欢迎词
            Label hello = new Label();
            hello.Text = "Hello\n曹家驹";
            hello.AutoSize = false;
            this.Controls.Add(hello);
            hello.ForeColor = Color.White;
            hello.Location = new Point(55, 3);
            hello.Size = new Size(60, 40);
            hello.TextAlign = ContentAlignment.MiddleLeft;


            // 菜单下拉图标
            PictureBox boxMenuDrop = new PictureBox();
            boxMenuDrop.Location = new Point(this.Width - 30 - 5, 13);
            boxMenuDrop.BackgroundImage = PublicFunction.getImageByFile("down.png");
            boxMenuDrop.BackgroundImageLayout = ImageLayout.Stretch;
            boxMenuDrop.Size = new Size(20, 15);
            this.Controls.Add(boxMenuDrop);

            boxMenuDrop.Click += new System.EventHandler(this.show_UserMenu);

            //exitLogin
            exitLogin.Image = PublicFunction.getImageByFile("exit.png");
            modiPassword.Image = PublicFunction.getImageByFile("chagepassword.png");
            setting.Image = PublicFunction.getImageByFile("setting.png");
        }

        private void UserInfoMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void show_UserMenu(object sender, EventArgs e)
        {
            Control menuC = sender as Control;
            Point pt = new Point(menuC.Location.X, menuC.Location.Y + menuC.Height);
            pt = this.PointToScreen(pt);

            pt.X = pt.X - (this.Width - 30 - 5);
            pt.Y = pt.Y + 20;

            contextMenuStrip1.Width = menuC.Width;
            contextMenuStrip1.Show(pt);

        }

        #region 菜单事件
        private void exitLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void modiPassword_Click(object sender, EventArgs e)
        {

        }

        private void setting_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
