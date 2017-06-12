using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC.HSControl.Priv
{
    public partial class GrantControl : UserControl
    {
        BaseTreeView roleListTree;
        Panel basePanel;
        PictureBox addPic;

        public GrantControl()
        {
            InitializeComponent();

            this.BackColor = Color.Transparent;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        internal void buildControl()
        {
            // 角色列表
            roleListTree = new BaseTreeView();
            roleListTree.BackColor = Color.Gray;
            this.Controls.Add(roleListTree);
            roleListTree.BorderStyle = BorderStyle.None;
            roleListTree.ItemHeight = 60;
            roleListTree.Font = new Font("宋体", 10);
            roleListTree.NodeMouseClick += roleListTree_NodeMouseClick;
            roleListTree.Invalidate();

            if (basePanel == null)
            {
                // 基础配置
                basePanel = new Panel();
                this.Controls.Add(basePanel);

                addSubControl(basePanel, "用戶列表");
            }

            addPic = new PictureBox();
            addPic.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(addPic);
        }

        private void GrantControl_Load(object sender, EventArgs e)
        {

        }

        private void GrantControl_Resize(object sender, EventArgs e)
        {
            setPosition();
        }

        private void setPosition()
        {
            if (roleListTree != null)
            {
                roleListTree.Location = new Point(2, 2);
                roleListTree.Width = 180;
                roleListTree.Height = this.Height - 2*2;
            }

            int xSep = 2;
            int ySep = 2;

            if (basePanel != null)
            {
                basePanel.Location = new Point(roleListTree.Right + xSep, ySep);
                basePanel.Size = new Size(this.Width - 3 * xSep - roleListTree.Width, (this.Height - 4));
                basePanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            }

            if (addPic != null)
            {
                addPic.Image = PublicFunction.getImageByFile("plus-icon.png");
                addPic.Location = new Point(this.Width - 36,5);
                addPic.Size = new Size(20, 20);
            }
        }

        private void roleListTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {


        }

        private void addSubControl(Panel panel, string textLab)
        {
            int sep = 3;
            PictureBox box = new PictureBox();
            box.Image = PublicFunction.getImageByFile("icon1.png");
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            panel.Controls.Add(box);
            box.Location = new Point(sep, sep);
            box.Size = new Size(20, 20);
            box.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            Label lab = new Label();
            panel.Controls.Add(lab);
            lab.Text = textLab;
            lab.AutoSize = false;
            lab.Location = new Point(box.Right + sep, sep);
            lab.Size = new Size(100, 20);
            lab.TextAlign = ContentAlignment.MiddleLeft;
            lab.Font = new Font("宋体", 10);
            lab.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            CXListView listV = new CXListView();
            panel.Controls.Add(listV);
            listV.Location = new Point(sep, box.Bottom + sep);
            listV.Size = new Size(panel.Width - 2 * sep, panel.Height - box.Bottom - sep * 2);
            listV.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            listV.View = View.Details;
            ColumnHeader head1 = listV.Columns.Add("登錄名");
            head1.Width = 200;

            ColumnHeader head2 = listV.Columns.Add("姓名");
            head2.Width = 200;

            ColumnHeader head3 = listV.Columns.Add("代理商");
            head3.Width = 200;

            ColumnHeader head4 = listV.Columns.Add("更改");
            head4.Width = 200;
        }
    }
}
