using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC.HSControl.Logger
{
    public partial class LoggerPannel : UserControl
    {
        int fliterHeight = 80;
        
        CXListView listV;
        QueryPanel queryPanel;
        PageSwitchPanel switchPanel;

        public LoggerPannel()
        {
            InitializeComponent();

            this.BackColor = Color.Transparent;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        public void buildControl()
        {
            // 创建条件控件
            queryPanel = new QueryPanel();
            this.Controls.Add(queryPanel);


            // 创建显示结果listview
            listV = new CXListView();
            this.Controls.Add(listV);
            initListVHeader(listV);

            // 创建翻页面板
            switchPanel = new PageSwitchPanel();
            this.Controls.Add(switchPanel);

            // 设置控件位置
            setControlPosition();
        }

        private void initListVHeader(CXListView lv)
        {
            lv.Columns.Clear();
            lv.Columns.Add("操作員");
            lv.Columns.Add("操作類型");
            lv.Columns.Add("操作描述");
            lv.Columns.Add("IP");
            lv.Columns.Add("其他");

            for (int index = 0 ; index < lv.Columns.Count ; index ++)
            {
                lv.Columns[index].Width = 150;
            }
        }

        private void LoggerPannel_Resize(object sender, EventArgs e)
        {
            setControlPosition();
        }

        private void setControlPosition()
        {
            if (listV != null)
            {
                listV.Location = new Point(5, fliterHeight + 5 );
                listV.Size = new Size(this.Width - 2 * 5, this.Height - fliterHeight - fliterHeight / 2);
            }

            if (queryPanel != null)
            {
                queryPanel.Location = new Point(5, 5);
                queryPanel.Size = new Size(this.Width - 2 * 5, fliterHeight);
            }

            if (switchPanel != null)
            {
                //switchPanel.BackColor = Color.Red;

                //switchPanel.Location = new Point(5, listV.Top + listV.Height + 1);
                switchPanel.Location = new Point(5, listV.Top + listV.Height + 1);
                switchPanel.Size = new Size(this.Width - 2 * 5, this.Height - listV.Bottom - 2 * 1);
            }
        }
    }


}
