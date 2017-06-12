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
    public partial class RoleControl : UserControl
    {
        BaseTreeView roleListTree;
        ShapeButton addRole,removeRole;
        Panel basePanel, savePanel, dbPanel;

        public RoleControl()
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

            // 添加
            addRole = new ShapeButton();
            this.Controls.Add(addRole);
            addRole.Text = "添加";
            addRole.ForeColor = Color.White;
            addRole.Font = new Font("宋體", 13, FontStyle.Bold);

            removeRole = new ShapeButton();
            this.Controls.Add(removeRole);
            removeRole.Text = "刪除";
            removeRole.ForeColor = Color.White;
            removeRole.Font = new Font("宋體", 13, FontStyle.Bold);

            /////////功能列表

            if (basePanel == null)
            {
                // 基础配置
                basePanel = new Panel();
                this.Controls.Add(basePanel);

                addSubControl(basePanel, "功能列表1");
            }

            if (savePanel == null)
            {
                // 存储设置
                savePanel = new Panel();
                this.Controls.Add(savePanel);

                addSubControl(savePanel, "功能列表2");
            }

            if (dbPanel == null)
            {
                // 数据配置
                dbPanel = new Panel();
                this.Controls.Add(dbPanel);

                addSubControl(dbPanel, "功能列表3");

            }

        }

        private void RoleControl_Resize(object sender, EventArgs e)
        {
            setPosition();
        }

        private void setPosition()
        {
            if (roleListTree != null)
            {
                roleListTree.Location = new Point(2, 2);
                roleListTree.Width = 180;
                roleListTree.Height = this.Height - 45;
            }

            if (addRole != null)
            {
                addRole.Location = new Point(2, roleListTree.Bottom + 2);
                addRole.Width = 180;
                addRole.Height = 36;
                addRole.Radius = 8;
            }

            if (removeRole != null)
            {
                removeRole.Width = 180;
                removeRole.Height = 36;
                removeRole.Radius = 8;
                removeRole.Location = new Point(this.Width - removeRole.Width - 4, roleListTree.Bottom + 2);
            }

            int xSep = 2;
            int ySep = 2;

            if (basePanel != null && savePanel != null && dbPanel != null)
            {
                basePanel.Location = new Point(roleListTree.Right + xSep, ySep);
                basePanel.Size = new Size(this.Width - 3 * xSep - roleListTree.Width, (this.Height - 2 * ySep - 45) / 3);
                basePanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");

                savePanel.Location = new Point(roleListTree.Right + xSep, basePanel.Bottom + 3);
                savePanel.Size = new Size(this.Width - 3 * xSep - roleListTree.Width, (this.Height - 2 * ySep - 45) / 3);
                savePanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");

                dbPanel.Location = new Point(roleListTree.Right + xSep, savePanel.Bottom + 3);
                dbPanel.Size = new Size(this.Width - 3 * xSep - roleListTree.Width, (this.Height - 2 * ySep - 45) / 3);
                dbPanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");

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
            ColumnHeader head1 = listV.Columns.Add("参数标识");
            head1.Width = 200;

            ColumnHeader head2 = listV.Columns.Add("参数说明");
            head2.Width = 200;

            ColumnHeader head3 = listV.Columns.Add("参数值");
            head3.Width = 200;

            ColumnHeader head4 = listV.Columns.Add("更改");
            head4.Width = 200;
        }

    }
}
