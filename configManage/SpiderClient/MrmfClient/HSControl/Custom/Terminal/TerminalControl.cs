using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpiderC.HSControl.Search;

namespace SpiderC.HSControl.Custom.Terminal
{
    public partial class TerminalControl : UserControl
    {
        ShapeButton exportBtn;
        SearchBox searchBox;
        ShapeButton addBtn;
        ShapeButton deleBtn;
        ShapeButton resetPassword;

        CXListView customLv;
        PageSwitchPanel pagePanel;

        public TerminalControl()
        {
            InitializeComponent();
        }
        public void buildControl()
        {
            // !
            exportBtn = new ShapeButton();
            exportBtn.Text = "导出";
            this.Controls.Add(exportBtn);

            // 
            searchBox = new SearchBox();
            this.Controls.Add(searchBox);

            //
            addBtn = new ShapeButton();
            addBtn.Text = "增加";
            this.Controls.Add(addBtn);

            //
            deleBtn = new ShapeButton();
            deleBtn.Text = "删除";
            this.Controls.Add(deleBtn);


            //
            resetPassword = new ShapeButton();
            resetPassword.Text = "重置密码";
            this.Controls.Add(resetPassword);

            //
            customLv = new CXListView();
            initListVHeader(customLv);
            this.Controls.Add(customLv);
            customLv.FullRowSelect = true;
            customLv.GridLines = true;


            //
            pagePanel = new PageSwitchPanel();
            this.Controls.Add(pagePanel);
        }
        private void TerminalControl_Resize(object sender, EventArgs e)
        {
            SetControlPosition();
        }

        private void SetControlPosition()
        {
            int btnHd = 30;
            int btnWd = 80;
            int sep = 10;
            if (exportBtn != null)
            {
                exportBtn.Location = new Point(sep, sep);
                exportBtn.Size = new Size(btnWd, btnHd);
            }

            if (searchBox != null)
            {
                searchBox.Location = new Point(sep + (sep + btnWd) * 1, sep);
                searchBox.Size = new Size(btnWd*2, btnHd);
            }

            if (resetPassword != null)
            {
                resetPassword.Location = new Point(this.Width - (sep + (sep + btnWd) * 1), sep);
                resetPassword.Size = new Size(btnWd, btnHd);
            }

            if (deleBtn != null)
            {
                deleBtn.Location = new Point(this.Width - (sep + (sep + btnWd) * 2), sep);
                deleBtn.Size = new Size(btnWd, btnHd);
            }

            if (addBtn != null)
            {
                addBtn.Location = new Point(this.Width - (sep + (sep + btnWd) * 3), sep);
                addBtn.Size = new Size(btnWd, btnHd);
            }

            if (customLv != null)
            {
                customLv.Location = new Point(sep, btnHd + 2* sep);
                customLv.Size = new Size(this.Width - 2*sep, this.Height - 50 - 50);
            }

            if (pagePanel != null)
            {
                pagePanel.Location = new Point(sep,this.Height - 50);
                pagePanel.Size = new Size(this.Width - 2 * sep, 50);
            }
        }

        private void initListVHeader(CXListView lv)
        {
            lv.Columns.Clear();
            lv.Columns.Add("登陆名");
            lv.Columns.Add("姓名");
            lv.Columns.Add("注册时间");
            lv.Columns.Add("服务到期");
            lv.Columns.Add("操作");

            for (int index = 0; index < lv.Columns.Count; index++)
            {
                lv.Columns[index].Width = 150;
            }
        }
    }
}
