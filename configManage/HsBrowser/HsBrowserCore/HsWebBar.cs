using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using HsServiceCore;

namespace HsServiceCore
{
    public partial class HsWebBar : UserControl
    {
       
        List<TabItem> _itemLists = new List<TabItem>();
        int _selectIndex = 0;

        int _maxWidth = 150;

        Rectangle addRect;
        int addPageWd = 20;


        public int MaxWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; }
        }

        Color _CtrlBackColor = Color.White;

        public Color CtrlBackColor
        {
            get { return _CtrlBackColor; }
            set { _CtrlBackColor = value; }
        }

        Color _focusBackColor = ColorTranslator.FromHtml("#060606");

        public Color FocusBackColor
        {
            get { return _focusBackColor; }
            set { _focusBackColor = value; }
        }
        Color _unFocusBackColor = ColorTranslator.FromHtml("#D4E1E9");

        public Color UnFocusBackColor
        {
            get { return _unFocusBackColor; }
            set { _unFocusBackColor = value; }
        }

        Color _lineColor = ColorTranslator.FromHtml("#99A9B8");

        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }
        


        public List<TabItem> ItemLists
        {
            get { return _itemLists; }
        }

        public int SelectIndex
        {
            get { return _selectIndex; }
            set { _selectIndex = value; }
        }

        /// <summary>
        /// 左侧偏移量
        /// </summary>
        int _leftOffset = 10;

        public int LeftOffset
        {
            get { return _leftOffset; }
            set { _leftOffset = value; }
        }

        public HsWebBar()
        {
            InitializeComponent();
        }

        private void HsWebBar_Load(object sender, EventArgs e)
        {
            if (_itemLists == null)
            {
                _itemLists = new List<TabItem>();
            }

            // 如果没有可选项
            if (ItemLists.Count <= 0)
            {
                TabItem item = new TabItem();

                item.Url = String.Format("{0}://{1}", GlobalConfig.ProtocolPre, GlobalConfig.LoginAddress);
                ItemLists.Add(item);
                _selectIndex = 0;
            }


        }

        public void addItem(TabItem item)
        {
            ItemLists.Add(item);

            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.CompositingQuality = CompositingQuality.HighQuality;

            this.BackColor = CtrlBackColor;

            for (int index = ItemLists.Count - 1; index >= 0; index--)
            {
                if (_selectIndex == index)
                {
                    continue;
                }
                Rectangle rect = drawItemRect(graph, index);
                ItemLists[index].MainRect = rect;
                ItemLists[index].CloseRect = drawCloseButton(graph, rect);
                drawText(graph, ItemLists[index]);
                
            }
            Rectangle rectSel = drawItemRect(graph, _selectIndex);
            ItemLists[_selectIndex].MainRect = rectSel;
            ItemLists[_selectIndex].CloseRect = drawCloseButton(graph, rectSel);
            drawText(graph, ItemLists[_selectIndex]);
            
            
            
            // 绘制添加按钮
            addRect = drawAddButton(graph);

            // 绘制底部横线
            Point startPt = new Point(0, this.Height - 1);
            Point stopPt = new Point(this.Width, this.Height - 1);

            graph.DrawLine(new Pen(Color.Gray,1),startPt,stopPt);
            base.OnPaint(e);
        }

        private void drawText(Graphics graph, TabItem tabItem)
        {
            int closeBtnSize = 16;

            int startX = tabItem.MainRect.X + closeBtnSize;
            int startY = tabItem.MainRect.Y + 5;
            int width = (int)(tabItem.MainRect.Width * 0.8) - 2 * closeBtnSize;
            int height = tabItem.MainRect.Height - 2 * 5;

            Font stringFont = new Font("宋体", 10, FontStyle.Regular);

            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            format.LineAlignment = StringAlignment.Center;  // 更正： 垂直居中
            format.Alignment = StringAlignment.Center;      // 水平居中

            RectangleF rectString = new Rectangle(startX, startY, width, height);
            graph.DrawString(tabItem.Name, stringFont, Brushes.Black, rectString, format);
        }

        private Rectangle drawCloseButton(Graphics graph, Rectangle rect)
        {
            int closeBtnSize = 16;
            //throw new NotImplementedException();
            int startX = rect.X + (int)(rect.Width * 0.9) - closeBtnSize;
            int startY = rect.Y + 5;

            Rectangle closeRect = new Rectangle(startX, startY, closeBtnSize, closeBtnSize);
            string fileName = Path.Combine(Path.Combine(Application.StartupPath, "Res"), "delete.png");
            Image image = Image.FromFile(fileName);

            graph.DrawImage(image, closeRect);

            return closeRect;
        }

        private Rectangle drawAddButton(Graphics graph)
        {
            int startX = ItemLists[ItemLists.Count - 1].MainRect.X + ItemLists[ItemLists.Count - 1].MainRect.Width + 2*LeftOffset;
            int startY = (this.Height - addPageWd) / 2;

            Rectangle rect = new Rectangle(startX, startY, addPageWd, addPageWd);

            string fileName = Path.Combine(Path.Combine(Application.StartupPath,"Res"),"add.png");
            Image image = Image.FromFile(fileName);

            graph.DrawImage(image, rect);
            return rect;
        }


        private Rectangle drawItemRect(Graphics graph, int rectIndex)
        {
            if (_selectIndex < 0 || _selectIndex >= ItemLists.Count)
            {
                return new Rectangle();
            }

            int wd = (this.Width-addPageWd)/ ItemLists.Count;

            if (wd > MaxWidth)
            {
                wd = MaxWidth;
            }
            int itemOffset = 10;
            int topWd = LeftOffset + rectIndex * wd - rectIndex * itemOffset;
            int ofsset =(int) (wd * 0.1);

            

            Queue<Point> PointArray = new Queue<Point>();
            int penWd = 2;
            Rectangle rect = new Rectangle(topWd + ofsset, penWd, wd - 2 * ofsset, this.Height - penWd*2);
            PointArray.Enqueue(new Point(topWd + ofsset, penWd));
            PointArray.Enqueue(new Point(topWd + wd - 2 * ofsset, penWd));

            PointArray.Enqueue(new Point(topWd + wd, this.Height - penWd));
            PointArray.Enqueue(new Point(topWd, this.Height - penWd));

            graph.DrawPolygon(new Pen(LineColor, penWd), PointArray.ToArray());

            if (rectIndex == _selectIndex)
            {
                graph.FillPolygon(new SolidBrush(Color.WhiteSmoke), PointArray.ToArray());
            }
            else
            {
                graph.FillPolygon(new SolidBrush(UnFocusBackColor), PointArray.ToArray());
            }
            
            
            return rect;
        }
  
        /// <summary>
        /// 获取每个页签的宽度
        /// </summary>
        /// <returns></returns>
        private int getItemWidth()
        {
            return 50;
        }

        /// <summary>
        /// 获取item的高度
        /// </summary>
        /// <returns></returns>
        private int getItemHeight()
        {
            return 20;
        }

        private void HsWebBar_MouseClick(object sender, MouseEventArgs e)
        {
            for (int index = 0  ; index < ItemLists.Count ; index ++)
            {
                TabItem item = ItemLists[index];
                PtPosition posType = item.getPointPosition(e.Location);
                
                if (posType == PtPosition.InMain)
                {
                    if (_selectIndex != index)
                    {
                        _selectIndex = index;

                        HsWebForm.g_WebForm.TabChange(ItemLists[_selectIndex]);

                        this.Invalidate();
                        return;
                    }
                }
                else if (posType == PtPosition.InClose)
                {
                    ItemLists.RemoveAt(index);

                    if (ItemLists.Count <= 0)
                    {
                        Application.Exit();
                    }

                    if (this.SelectIndex == index)
                    {
                        this.SelectIndex = 0;
                    }
                    else if (this.SelectIndex > index)
                    {
                        this.SelectIndex = this.SelectIndex - 1;
                    }

                    this.Invalidate();
                }
                
            }

            if (addRect.Contains(e.Location))
            {
                if (ItemLists.Count >= 5)
                {
                    MessageBox.Show("不能再开启更多窗口了");
                    return;
                }
                ItemLists.Add(new TabItem());
                _selectIndex = ItemLists.Count - 1;

                // 通知主窗体
                HsWebForm.g_WebForm.TabChange(ItemLists[_selectIndex]);
                this.Invalidate();
                return;
            }
        }

        private void HsWebBar_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        internal TabItem getCurrentItem()
        {
            return ItemLists[_selectIndex];
        }

        internal void updateTab(TabItem item)
        {
            ItemLists.RemoveAt(_selectIndex);
            ItemLists.Insert(_selectIndex, item);

            this.Invalidate();
        }
    }
}
