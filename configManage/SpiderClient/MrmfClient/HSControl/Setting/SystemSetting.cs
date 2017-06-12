using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC.HSControl.Config
{
    public partial class SystemSetting : UserControl
    {
        Panel basePanel, savePanel, dbPanel;
        public SystemSetting()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

        }
        public void setPosition()
        {
            int xSep = 20;
            int ySep = 20;

            if (basePanel !=null && savePanel != null && dbPanel != null)
            {
                basePanel.Location = new Point(xSep, ySep);
                basePanel.Size = new Size(this.Width - 2 * xSep, (this.Height - 2 * ySep) / 3);
                basePanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");

                savePanel.Location = new Point(xSep, basePanel.Bottom + 3);
                savePanel.Size = new Size(this.Width - 2 * xSep, (this.Height - 2 * ySep) / 3);
                savePanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");

                dbPanel.Location = new Point(xSep, savePanel.Bottom + 3);
                dbPanel.Size = new Size(this.Width - 2 * xSep, (this.Height - 2 * ySep) / 3);
                dbPanel.BackColor = ColorTranslator.FromHtml("#f2f2f2");

            }


        }
        public void buildControl()
        {

            if (basePanel == null)
            {
                // 基础配置
                basePanel = new Panel();
                this.Controls.Add(basePanel);

                addSubControl(basePanel,"基础参数");
            }

            if (savePanel == null)
            {
                // 存储设置
                savePanel = new Panel();
                this.Controls.Add(savePanel);

                addSubControl(savePanel, "存储参数");
            }

            if (dbPanel == null)
            {
                // 数据配置
                dbPanel = new Panel();
                this.Controls.Add(dbPanel);

                addSubControl(dbPanel, "数据库参数");

            }

            setPosition();
        }

        private void addSubControl(Panel panel,string textLab)
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
            listV.Size = new Size(panel.Width - 2*sep, panel.Height - box.Bottom - sep*2);
            listV.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            listV.View = View.Details;
            ColumnHeader head1 = listV.Columns.Add("参数标识");
            head1.Width = 200;

            ColumnHeader head2  = listV.Columns.Add("参数说明");
            head2.Width = 200;

            ColumnHeader head3 = listV.Columns.Add("参数值");
            head3.Width = 200;

            ColumnHeader head4 = listV.Columns.Add("更改");
            head4.Width = 200;
        }

        private void SystemSetting_Resize(object sender, EventArgs e)
        {
            setPosition();
        }
    }
}
