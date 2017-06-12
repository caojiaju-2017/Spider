using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;

namespace SpiderC.HSControl.Search
{
    public partial class SearchBox : UserControl
    {
        #region 属性
        /// <summary>
        /// 水印文本框
        /// </summary>
        MarkTextBox txt = null;
        /// <summary>
        /// 候选列表
        /// </summary>
        CustomListBox list = null;
        /// <summary>
        /// 放大镜宽度
        /// </summary>
        int imgWidth = 15;
        /// <summary>
        /// 边框路径
        /// </summary>
        GraphicsPath path = null;
        /// <summary>
        /// 鼠标是否在放大镜上
        /// </summary>
        bool isHover = false;
        string text = "";

        /// <summary>
        /// 搜索框的值
        /// </summary>
        public string Value
        {
            set
            {
                text = value;
                this.Text = value;
            }
            get { return text; }
        }

        /// <summary>
        /// 搜索框显示的内容
        /// </summary>
        public string DispalyValue
        {
            set
            {
                if (txt != null)
                {
                    txt.Text = value;
                }
            }

            get
            {
                if (txt != null)
                {
                    return txt.Text;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 候选列表选中色
        /// </summary>
        private Color baseColor = Color.FromArgb(255, 110, 0);

        /// <summary>
        /// 原始数据源
        /// </summary>
        private DataTable dataSource;

        /// <summary>
        /// List的数据源
        /// </summary>
        public DataTable DataSource
        {
            set
            {
                if (list != null)
                {
                    list.DataSource = value;
                    dataSource = value;
                }
            }
            get { return list == null ? null : (DataTable)list.DataSource; }
        }

        /// <summary>
        /// 显示对象
        /// </summary>
        public string DisplayMember
        {
            set
            {
                if (list != null)
                {
                    list.DisplayMember = value;
                }
            }
            get { return list == null ? "" : list.DisplayMember; }
        }

        /// <summary>
        /// 值对象
        /// </summary>
        public string ValueMember
        {
            set
            {
                if (list != null)
                {
                    list.ValueMember = value;
                }
            }
            get { return list == null ? "" : list.ValueMember; }
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        private int radius = 10;

        /// <summary>
        /// 圆角半径
        /// </summary>
        public int Radius
        {
            set
            {
                radius = value;
                if (txt != null)
                {
                    txt.Location = new Point((int)Math.Round(Height / 3f, 0), (int)Math.Round(Height / 2f - txt.Height / 2f));
                    txt.Size = new Size(Width - Height - imgWidth, txt.Height);
                }
                path = GraphicsPathHelper.CreatePath(new RectangleF(0, 0, Width, Height), radius, GraphicsPathHelper.RoundStyle.All, true);
                Invalidate();
            }
            get { return radius; }
        }

        /// <summary>
        /// 候选列表选中色
        /// </summary>
        public Color BaseColor
        {
            set { baseColor = value; }
            get { return baseColor; }
        }

        #endregion

        #region 事件
        public delegate void SearchBoxClickHandler();
        /// <summary>
        /// 放大镜点击事件
        /// </summary>
        public event SearchBoxClickHandler SearchBoxClick;

        public delegate void ValueSubmitHandler();
        /// <summary>
        /// 提交Value
        /// </summary>
        public event ValueSubmitHandler ValueSubmit;

        #endregion

        public SearchBox()
        {
            BackColor = Color.FromArgb(250, 250, 250);
            this.SetStyle(
              ControlStyles.UserPaint |  //控件自行绘制，而不使用操作系统的绘制
              ControlStyles.AllPaintingInWmPaint | //忽略擦出的消息，减少闪烁。
              ControlStyles.OptimizedDoubleBuffer |//在缓冲区上绘制，不直接绘制到屏幕上，减少闪烁。
              ControlStyles.ResizeRedraw | //控件大小发生变化时，重绘。                  
              ControlStyles.SupportsTransparentBackColor, true);//支持透明背景颜色
        }

        #region 重写事件

        /// <summary>
        /// 创建控件
        /// </summary>
        protected override void OnCreateControl()
        {
            if (txt == null)
            {
                txt = new MarkTextBox();
                txt.Font = this.Font;
                txt.BackColor = BackColor;
                txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
                txt.SetWatermark("搜索");
                txt.TextChanged += Txt_TextChanged;
                txt.KeyDown += Txt_KeyDown;
                txt.DoubleClick += Txt_DoubleClick;
                if (this.TopLevelControl != null)
                {
                    TopLevelControl.MouseClick += Ctrl_MouseClick;
                    SetClickEvent(TopLevelControl);
                }
            }
            if (list == null)
            {
                list = new CustomListBox();
                if (this.Parent != null)
                {
                    list.Click += List_Click;
                    list.MouseEnter += List_MouseEnter;
                    list.MouseLeave += List_MouseLeave;
                    list.ItemHeight = 17;
                    list.Font = Font;
                    list.BackColor = BackColor;
                    list.SelectItemColor = baseColor;
                    bool flag = true;
                    Control ctrl = this;
                    while (ctrl.Parent is FlowLayoutPanel)
                    {
                        ctrl = ctrl.Parent;
                        flag = false;
                    }
                    ctrl.Parent.Controls.Add(list);
                    list.BringToFront();
                    list.Location = flag ? new Point(this.Left, this.Bottom + 1) : new Point(ctrl.Left + this.Left, ctrl.Top + this.Bottom + 1);
                    list.Size = new Size(Width, 100);
                    list.Visible = false;
                }
            }
            txt.Location = new Point((int)Math.Round(Height / 3f, 0), (int)Math.Round(Height / 2f - txt.Height / 2f));
            txt.Size = new Size(Width - Height - imgWidth, txt.Height);
            this.Controls.Add(txt);
            txt.BringToFront();
            base.OnCreateControl();
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            if (path == null)
            {
                path = GraphicsPathHelper.CreatePath(new RectangleF(0, 0, Width, Height), radius, GraphicsPathHelper.RoundStyle.All, true);

            }
            if (isHover)
            {
                DrawMagnifier(e.Graphics, new RectangleF(this.Width - Height / 3 - imgWidth, Height / 2 - imgWidth / 2, imgWidth, imgWidth), baseColor);
            }
            else
            {
                DrawMagnifier(e.Graphics, new RectangleF(this.Width - Height / 3 - imgWidth, Height / 2 - imgWidth / 2, imgWidth, imgWidth), Color.Gray);
            }
            e.Graphics.DrawPath(Pens.Gray, path);
            base.OnPaint(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                this.BackColor = this.Parent.BackColor;
                if (txt != null)
                {
                    txt.BackColor = BackColor;
                }
                if (list != null)
                {
                    list.BackColor = BackColor;
                }
            }

            base.OnParentBackColorChanged(e);
        }

        /// <summary>
        /// 控件size变化后改变txt大小位置
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (txt != null)
            {
                txt.Location = new Point((int)Math.Round(Height / 3f, 0), (int)Math.Round(Height / 2f - txt.Height / 2f));
                txt.Size = new Size(Width - Height - imgWidth, txt.Height);
            }
            path = GraphicsPathHelper.CreatePath(new RectangleF(0, 0, Width, Height), radius, GraphicsPathHelper.RoundStyle.All, true);
            base.OnResize(e);
        }

        /// <summary>
        /// 绘制放大镜
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        private void DrawMagnifier(Graphics g, RectangleF rect, Color color)
        {
            float value = 0.77f;
            g.DrawEllipse(new Pen(color, 2.5f), new RectangleF(rect.Left, rect.Top, rect.Width * value, rect.Height * value));
            g.DrawLine(new Pen(color, 2f), new PointF(rect.Left + rect.Width * 0.67f, rect.Top + rect.Width * 0.67f), new PointF(rect.Left + rect.Width, rect.Top + rect.Height));
        }

        /// <summary>
        /// 鼠标进入放大镜，放大镜变色
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            RectangleF rect = new RectangleF(this.Width - Height / 3 - imgWidth, Height / 2 - imgWidth / 2, imgWidth, imgWidth);
            if (!isHover && rect.Contains(e.Location))
            {
                isHover = true;
                Invalidate();
            }
            if (isHover && !rect.Contains(e.Location))
            {
                isHover = false;
                Invalidate();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// 点击放大镜
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (isHover && e.Button == MouseButtons.Left)
            {
                if (SearchBoxClick != null)
                {
                    SearchBoxClick();
                    isHover = false;
                    Invalidate();
                }
            }
            base.OnMouseClick(e);
        }

        #endregion

        /// <summary>
        /// 文本框内容变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_TextChanged(object sender, EventArgs e)
        {

            string text = txt.Text.Trim();
            Value = text;
            if (list != null && list.DataSource != null && list.DataSource is DataTable)
            {
                string displayMember = list.DisplayMember;
                string valueMember = list.ValueMember;
                string sql = displayMember + " like '%" + text + "%'";
                DataRow[] dr = dataSource.Select(sql);
                DataTable dt = dataSource.Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                int height = dr.Length > 15 ? 15 : dr.Length;
                list.Size = new Size(list.Width, (height + 1) * list.ItemHeight);
                list.DataSource = dt;
                list.DisplayMember = displayMember;
                list.ValueMember = valueMember;
                list.Visible = dr.Length > 0;
            }

        }

        /// <summary>
        /// 双击选中所有文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_DoubleClick(object sender, EventArgs e)
        {
            txt.SelectAll();
        }

        /// <summary>
        /// 候选列表点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_Click(object sender, EventArgs e)
        {
            txt.TextChanged -= Txt_TextChanged;
            Value = list.SelectedValue.ToString();
            txt.Text = list.Text;
            txt.Focus();
            list.Visible = false;
            txt.TextChanged += Txt_TextChanged;
            if (ValueSubmit != null)
            {
                ValueSubmit();
            }
        }

        /// <summary>
        /// 方向键控制选择项，enter键确认选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (list != null && list.Visible)
            {
                if (e.KeyCode == Keys.Up)
                {
                    list.SelectedIndex = list.SelectedIndex > 0 ? --list.SelectedIndex : 0;
                }
                if (e.KeyCode == Keys.Down)
                {
                    list.SelectedIndex = list.SelectedIndex < list.Items.Count - 1 ? ++list.SelectedIndex : list.Items.Count - 1;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    txt.TextChanged -= Txt_TextChanged;
                    Value = list.SelectedValue.ToString();
                    txt.Text = list.Text;
                    txt.Focus();
                    list.Visible = false;
                    txt.TextChanged += Txt_TextChanged;

                }
            }

            if (e.KeyCode == Keys.Enter && ValueSubmit != null)
            {
                ValueSubmit();
            }


        }

        /// <summary>
        /// 设置鼠标单击事件
        /// </summary>
        /// <param name="ctrl"></param>
        private void SetClickEvent(Control ctrl)
        {
            ctrl.MouseClick -= Ctrl_MouseClick;
            ctrl.MouseClick += Ctrl_MouseClick;
            if (ctrl.HasChildren)
            {
                foreach (Control item in ctrl.Controls)
                {
                    SetClickEvent(item);
                }
            }
        }

        /// <summary>
        /// 其他控件鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctrl_MouseClick(object sender, MouseEventArgs e)
        {
            if (list != null && list.Visible)
            {
                list.Focus();
                list.Visible = false;
            }
        }

        /// <summary>
        /// 重新设置水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_MouseLeave(object sender, EventArgs e)
        {
            txt.SetWatermark("搜索");
        }

        /// <summary>
        /// 把水印设置为空，防止出现闪烁效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_MouseEnter(object sender, EventArgs e)
        {
            txt.SetWatermark("");
        }
    }
}
