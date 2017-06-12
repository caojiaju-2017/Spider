using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HsServiceCore.Service.ViewCtrl
{
    public partial class BaseController : UserControl
    {
        //private ServiceItem _serverItem;

        //public ServiceItem ServerItem
        //{
        //    get { return _serverItem; }
        //    set { _serverItem = value; }
        //}

        public BaseController()
        {
            InitializeComponent();
            
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            //graph.DrawLine(new Pen(Color.FromArgb(180, 180, 180)), new Point(0, 39), new Point(this.Width - 5, 39));
            
            //graph.DrawRectangle(new Pen(Color.FromArgb(180, 180, 180)),this.Location.X,this.Location.Y,this.Width,this.Height);
            base.OnPaint(e);
        }
    
    }
}
