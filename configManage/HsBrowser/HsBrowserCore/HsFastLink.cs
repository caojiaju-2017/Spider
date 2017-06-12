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
    public partial class HsFastLink : UserControl
    {
        TabItem _currentShowItem = null;

        public TabItem CurrentShowItem
        {
            get { return _currentShowItem; }
            set { _currentShowItem = value;
            textBox1.Text = _currentShowItem.Url;
            }
        }
        int _ctrlHeight = 30;

        public HsFastLink()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EEEEF2");
        }
        public HsFastLink(TabItem item)
        {
            InitializeComponent();
            _currentShowItem = item;
            this.BackColor = ColorTranslator.FromHtml("#EEEEF2");

            textBox1.Text = item.Url;
        }
        private void HsFastLink_Load(object sender, EventArgs e)
        {
            this.Height = _ctrlHeight;

            string srcPath = Path.Combine(Application.StartupPath,"Res");

            pbBack.BackgroundImage = Image.FromFile(Path.Combine(srcPath, "back.png"));
            pbBack.BackgroundImageLayout = ImageLayout.Stretch;

            pbPre.BackgroundImage = Image.FromFile(Path.Combine(srcPath, "prev.png"));
            pbPre.BackgroundImageLayout = ImageLayout.Stretch;

            pbRefresh.BackgroundImage = Image.FromFile(Path.Combine(srcPath, "refresh.png"));
            pbRefresh.BackgroundImageLayout = ImageLayout.Stretch;

            pbHome.BackgroundImage = Image.FromFile(Path.Combine(srcPath, "home.png"));
            pbHome.BackgroundImageLayout = ImageLayout.Stretch;

            pbFav.BackgroundImage = Image.FromFile(Path.Combine(srcPath, "fav.png"));
            pbFav.BackgroundImageLayout = ImageLayout.Stretch;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            //graph.DrawLine(new Pen(Color.Gray), new Point(0, 0), new Point(this.Width, 0));

            base.OnPaint(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                if (HsWebForm.g_WebForm!= null)
                {
                    //HsWebForm.g_WebForm.nagivationUrl(textBox1.Text);
                    textBox1.SelectAll();

                    CurrentShowItem.Url = textBox1.Text;

                    HsWebForm.g_WebForm.updateTableItem(CurrentShowItem);
                }
                
            }
        }

    }
}
