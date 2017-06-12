using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HsServiceCore.Service.CommCtrl
{
    public partial class CircleButton : UserControl
    {
        public CircleButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            //System.Drawing.Drawing2D.GraphicsPath myg = new System.Drawing.Drawing2D.GraphicsPath();

            //myg.AddEllipse(new Rectangle(0, 0, 30, 30)); //加椭圆
            //this.BackColor = Color.Purple;
            //this.Size = new System.Drawing.Size(this.Width, this.Height);
            //this.Region = new Region(myg); 

            //FontFamily ff = new FontFamily("Arial");
            //string str = "添加";
            //int fs = (int)FontStyle.Bold;
            //int emsize = 15;
            //PointF origin = new PointF(0, 0);
            //StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            //myg.AddString(str, ff, fs, emsize, origin, sf);
            //this.Region = new Region(myg); 

            graph.FillEllipse(new SolidBrush(Color.Red), 0, 0, 30, 30);
            base.OnPaint(e);
        }
    }
}
