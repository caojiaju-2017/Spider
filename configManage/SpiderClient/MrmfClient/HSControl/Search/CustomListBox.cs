using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiderC.HSControl.Search
{
    public class CustomListBox : ListBox
    {
        public CustomListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        /// <summary>
        /// 选中项的颜色
        /// </summary>
        private Color selectItemColor = Color.FromArgb(255, 110, 0);

        /// <summary>
        /// 选中项的颜色
        /// </summary>
        public Color SelectItemColor
        {
            set { selectItemColor = value; Invalidate(); }
            get { return selectItemColor; }
        }

        /// <summary>
        /// 选中鼠标焦点所在行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            SelectedIndex = IndexFromPoint(e.Location);
            base.OnMouseMove(e);
        }

        /// <summary>
        /// 重绘item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (Items.Count > 0)
            {
                int index = e.Index;//获取当前要进行绘制的行的序号，从0开始。
                if (index > -1)
                {
                    Graphics g = e.Graphics;//获取Graphics对象。
                    Rectangle bound = new Rectangle(e.Bounds.Left+10, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);//获取当前要绘制的行的一个矩形范围。
                    
                   // string text = ((DataRow)Items[index])[DisplayMember].ToString();//获取当前要绘制的行的显示文本。
                    string text =((DataTable)DataSource).Rows[index][DisplayMember].ToString();

                    //如果当前行为选中行。
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        TextRenderer.DrawText(g, text, this.Font, bound, selectItemColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    }
                    else
                    {
                        TextRenderer.DrawText(g, text, this.Font, bound, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    }
                }
            }

            base.OnDrawItem(e);
        }
    }
}
