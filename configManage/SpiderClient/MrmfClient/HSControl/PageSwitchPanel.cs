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
    public partial class PageSwitchPanel : UserControl
    {
        PictureBox pbFirst;
        PictureBox pbUpper;
        PictureBox pbNext;
        PictureBox pbLast;
        Label labInfo;

        int imageWd = 24;
        int labWd = 60;
        int sep = 5;
        public PageSwitchPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲


            buildControl();
        }

        public void buildControl()
        {
            // 第一页
            pbFirst = new PictureBox();
            this.Controls.Add(pbFirst);
            pbFirst.SizeMode = PictureBoxSizeMode.StretchImage;

            // 上一页
            pbUpper = new PictureBox();
            this.Controls.Add(pbUpper);
            pbUpper.SizeMode = PictureBoxSizeMode.StretchImage;

            // 页数概要
            labInfo = new Label();
            this.Controls.Add(labInfo);
            labInfo.AutoSize = false;
            labInfo.BackColor = Color.Transparent;
            labInfo.TextAlign = ContentAlignment.MiddleCenter;

            //  下一页
            pbNext = new PictureBox();
            this.Controls.Add(pbNext);
            pbNext.SizeMode = PictureBoxSizeMode.StretchImage;

            // 尾页
            pbLast = new PictureBox();
            this.Controls.Add(pbLast);
            pbLast.SizeMode = PictureBoxSizeMode.StretchImage;

            // 设置 位置
            setControlPosition();
        }

        private void PageSwitchPanel_Resize(object sender, EventArgs e)
        {
            setControlPosition();
        }

        private void setControlPosition()
        {
            if (pbFirst != null)
            {
                pbFirst.Location = new Point(this.Width / 2 - 2 * imageWd - 2 * sep - labWd / 2, (this.Height - imageWd) / 2);
                pbFirst.Size = new Size(imageWd, imageWd);
            }
            if (pbUpper != null)
            {
                pbUpper.Location = new Point(this.Width / 2 - imageWd - sep - labWd / 2, (this.Height - imageWd) / 2);
                pbUpper.Size = new Size(imageWd, imageWd);
            }
            if (labInfo != null)
            {
                labInfo.Location = new Point(this.Width / 2 - labWd / 2, (this.Height - imageWd) / 2);
                labInfo.Size = new Size(labWd, imageWd);
            }

            if (pbLast != null)
            {
                pbLast.Location = new Point(this.Width / 2 + imageWd + 2 * sep + labWd / 2, (this.Height - imageWd) / 2);
                pbLast.Size = new Size(imageWd, imageWd);
            }
            if (pbNext != null)
            {
                pbNext.Location = new Point(this.Width / 2 + sep + labWd / 2, (this.Height - imageWd) / 2);
                pbNext.Size = new Size(imageWd, imageWd);
            }
        }

        private void PageSwitchPanel_Load(object sender, EventArgs e)
        {
            setButtonImage();
        }

        private void setButtonImage()
        {
            if (pbFirst != null)
            {
                pbFirst.Image = PublicFunction.getImageByFile("first.png");
            }
            if (pbUpper != null)
            {
                pbUpper.Image = PublicFunction.getImageByFile("upper.png");
            }
            if (labInfo != null)
            {
                labInfo.Text = "0/0 頁";
            }

            if (pbLast != null)
            {
                pbLast.Image = PublicFunction.getImageByFile("last.png");
            }
            if (pbNext != null)
            {
                pbNext.Image = PublicFunction.getImageByFile("next.png");
            }
        }
    }
}
