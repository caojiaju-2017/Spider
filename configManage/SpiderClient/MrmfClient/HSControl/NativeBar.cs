using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpiderC.HSControl.Search;

namespace SpiderC.HSControl
{
    public partial class NativeBar : UserControl
    {
        //PictureBox box;

        public NativeBar()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        public void buildControl(object test)
        {
            // 设置途径

            //设置背景
            this.BackColor = ColorTranslator.FromHtml("#838383");

            // 设置搜索框背景
            SearchBox search = new SearchBox();
            search.Location = new Point(this.Width - 200, 5);
            search.Size = new Size(190, 30);
            
            this.Controls.Add(search);



            //setBar(null);
        }

        public void setBar(List<TreeNode> nodes)
        {
            //清空控件
            for (int index = 0; index < this.Controls.Count; index ++ )
            {
                Control ctrl = this.Controls[index];

                if (ctrl is SearchBox)
                {
                    continue;
                }

                this.Controls.RemoveAt(index);

                index--;
            }

            PictureBox box = new PictureBox();
            TreeNode node = nodes[nodes.Count - 1];

            this.Controls.Add(box);
            box.Location = new Point(20, 5);
            box.Size = new Size(30, 30);
            box.SizeMode = PictureBoxSizeMode.StretchImage;

            if (node.ImageIndex >= 0 && node.ImageIndex < node.TreeView.ImageList.Images.Count)
            {
                box.Image = node.TreeView.ImageList.Images[node.ImageIndex];
            }

            
            for (int index = nodes.Count - 1; index >= 0; index--)
            {
                TreeNode nodeTemp = nodes[index];
                Label lab1 = new Label();
                this.Controls.Add(lab1);
                lab1.AutoSize = false;
                lab1.ForeColor = ColorTranslator.FromHtml("#444444");
                lab1.Location = new Point(box.Right + (10 + 100) * (nodes.Count - 1 - index), box.Top);
                lab1.Size = new Size(100, box.Height);
                lab1.TextAlign = ContentAlignment.MiddleLeft;
                lab1.Font = new Font("宋体", 10);

                lab1.Text = nodeTemp.Text;
            }

        }

    }
}
