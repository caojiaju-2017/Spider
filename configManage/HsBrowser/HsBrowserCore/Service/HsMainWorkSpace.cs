using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HsServiceCore.Service.ViewCtrl;
using System.Drawing.Drawing2D;

namespace HsServiceCore.Service
{
    public partial class HsMainWorkSpace : UserControl
    {
        HsServiceTree _srvTree;
        BaseListCtrl _baseControl;

        public HsServiceTree SrvTree
        {
            get { return _srvTree; }
            set { _srvTree = value; }
        }
        public HsMainWorkSpace()
        {
            InitializeComponent();
        }

        private void HsMainWorkSpace_Load(object sender, EventArgs e)
        {
            setControl();

            ThreadStart entry = new ThreadStart(loadMenuData);//求和方法被定义为工作线程入口  
            Thread workThread = new Thread(entry);
            workThread.Start();  
            
        }

        private void loadMenuData()
        {
            // 改变鼠标状态
            this.Invoke(new EventHandler(delegate
            {
                this.Cursor = Cursors.WaitCursor;
            }));

            //List<ServiceItem> srvItems = ServiceInit.loadSystemFuncTree();

            // 还原鼠标状态
            this.Invoke(new EventHandler(delegate
            {
                this.Cursor = Cursors.Arrow;
            }));
        }

        private void HsMainWorkSpace_Resize(object sender, EventArgs e)
        {
            setControl();
        }

        private void setControl()
        {
            // 导航树
            if (_srvTree == null)
            {
                _srvTree = new HsServiceTree();
                _srvTree.BackColor = Color.Beige;
                this.Controls.Add(_srvTree);
            }

            _srvTree.Location = new Point(0, 0);
            _srvTree.Width = 240;
            _srvTree.Height = this.Height;

            //createWorkCtrl();
        }

        private void createWorkCtrl()
        {
            // 工作控件
            if (_baseControl == null)
            {
                _baseControl = new BaseListCtrl();
                this.Controls.Add(_baseControl);
            }
            _baseControl.Location = new Point(245, 0);
            _baseControl.Width = this.Width - _srvTree.Width - 5;
            _baseControl.Height = this.Height;
        }

        internal void clearCurrentData()
        {
            //_baseControl.Dispose();
            //_baseControl = null;

        }

        internal void loadData(ServiceItem item)
        {
            if (_baseControl == null)
            {
                createWorkCtrl();
                //return;
            }
            //_baseControl = new BaseListCtrl();
            _baseControl.LoadData(item);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            this.BackColor = Color.FromArgb(250, 250, 250);

            //graph.DrawLine(new Pen(Color.Red), new Point(0, 39), new Point(this.Width, 39));
            base.OnPaint(e);
        }
    }
}
