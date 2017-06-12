using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace HsServiceCore
{
    public partial class HsWebTitle : UserControl
    {
        HsWebForm mainFrm;

        Rectangle minRect;
        Rectangle maxRect;
        Rectangle closeRect;

        int btnWd = 44;
        int btnHd = 0;

        public HsWebTitle(HsWebForm parent)
        {
            InitializeComponent();
            mainFrm = parent;
            btnHd = (int)(((float)35 / 56) * btnWd);
            this.Height = btnHd + 2;
            this.Width = (btnWd + 2) * 3 + 14;
        }

        private void HsWebTitle_Load(object sender, EventArgs e)
        {

            //this.BackColor = Color.White;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            // 绘制最小化按钮
            minRect = DrawMinBtn(graph);

            // 绘制最大化按钮
            maxRect = DrawMaxBtn(graph);

            // 绘制关闭按钮
            closeRect = DrawCloseBtn(graph);

            base.OnPaint(e);
        }

        private Rectangle DrawCloseBtn(Graphics graph)
        {
            int startX = (maxRect.X + maxRect.Width);
            int startY = 1;

            Rectangle btnRect = new Rectangle(startX, startY, btnWd, btnHd);

            string fileName;
            if (closeRect.Contains(this.PointToClient(Control.MousePosition)))
            {
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "close_1.png");
            }
            else
            {
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "close_0.png");
            }

            Image image = Image.FromFile(fileName);

            graph.DrawImage(image, btnRect);

            return btnRect;
        }

        private Rectangle DrawMaxBtn(Graphics graph)
        {
            int startX = minRect.X + minRect.Width;
            int startY = 1;

            Rectangle btnRect = new Rectangle(startX, startY, btnWd, btnHd);

            string fileName;
            if (maxRect.Contains(this.PointToClient(Control.MousePosition)))
            {
                if (mainFrm.WindowState == FormWindowState.Normal)
                {
                    fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "max_1.png");
                }
                else
                {
                    fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "normal_1.png");
                }
            }
            else
            {
                if (mainFrm.WindowState == FormWindowState.Normal)
                {
                    fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "max_0.png");
                }
                else
                {
                    fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "normal_0.png");
                }
            }
           

            Image image = Image.FromFile(fileName);

            graph.DrawImage(image, btnRect);

            return btnRect;
        }

        private Rectangle DrawMinBtn(Graphics graph)
        {
            int startX = 4;
            int startY = 1;

            Rectangle btnRect = new Rectangle(startX, startY, btnWd, btnHd);

            string fileName ;
            if (minRect.Contains(this.PointToClient(Control.MousePosition)))
            {
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "min_1.png");
            }
            else
            {
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "min_0.png");
            }
            
            Image image = Image.FromFile(fileName);

            graph.DrawImage(image, btnRect);

            return btnRect;
        }

        private void HsWebTitle_MouseEnter(object sender, EventArgs e)
        {
            if (minRect.Contains(this.PointToClient(Control.MousePosition))
                || maxRect.Contains(this.PointToClient(Control.MousePosition))
                || closeRect.Contains(this.PointToClient(Control.MousePosition)))
            {
                this.Invalidate();
            }
        }

        private void HsWebTitle_MouseLeave(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void HsWebTitle_MouseMove(object sender, MouseEventArgs e)
        {
            this.Invalidate();
        }

        private void HsWebTitle_MouseClick(object sender, MouseEventArgs e)
        {
            if (minRect.Contains(e.Location))
            {
                mainFrm.WindowState = FormWindowState.Minimized;
            }
            else if (maxRect.Contains(e.Location))
            {
                if (mainFrm.WindowState == FormWindowState.Maximized)
                {
                    mainFrm.WindowState = FormWindowState.Normal;
                }
                else
                {
                    mainFrm.WindowState = FormWindowState.Maximized;
                }
            }
            else if (closeRect.Contains(e.Location))
            {
                Application.Exit();
            }
        }
    }
}
