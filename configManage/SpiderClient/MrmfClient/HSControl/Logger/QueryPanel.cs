using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC.HSControl.Logger
{
    public partial class QueryPanel : UserControl
    {
        public QueryPanel()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲


            initControl();
        }

        private void initControl()
        {
            pbSearch.Image = PublicFunction.getImageByFile("search.png");
            pbSearch.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pbSearch_MouseEnter(object sender, EventArgs e)
        {
            pbSearch.Image = PublicFunction.getImageByFile("search-1.png");
        }

        private void pbSearch_MouseLeave(object sender, EventArgs e)
        {
            pbSearch.Image = PublicFunction.getImageByFile("search.png");
        }
    }
}
