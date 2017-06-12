using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC.HSControl.Order
{
    public partial class OrderControl : UserControl
    {
        FliterPanel filterPanel;
        PageSwitchPanel switchPanel;
        Panel orderShowPanel;
        public OrderControl()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
        }

        public void buildControl()
        {
            filterPanel = new FliterPanel();
            this.Controls.Add(filterPanel);

            orderShowPanel = new Panel();
            this.Controls.Add(orderShowPanel);
            orderShowPanel.BackColor = Color.LightGray;
            orderShowPanel.AutoScroll = true;
            

            switchPanel = new PageSwitchPanel();
            this.Controls.Add(switchPanel);

        }

        private void OrderControl_Resize(object sender, EventArgs e)
        {
            SetControlPosition();
        }

        
        private void SetControlPosition()
        {
            if (filterPanel !=null)
            {
                filterPanel.Location = new Point(5, 5);
                filterPanel.Size = new Size(this.Width - 2 * 5, 80);
                filterPanel.BackColor = Color.Transparent;
            }

            if (orderShowPanel != null)
            {
                orderShowPanel.Location = new Point(5, filterPanel.Bottom + 2);
                orderShowPanel.Size = new Size(this.Width - 2 * 5, this.Height - filterPanel.Bottom - 2*2 - 50);

                addTestItem();
            }

            if (switchPanel != null)
            {
                switchPanel.Location = new Point(5, this.Height - 50);
                switchPanel.Size = new Size(this.Width - 2 * 5, 40);
                switchPanel.BackColor = Color.Transparent;
            }
        }

        private void addTestItem()
        {
            int xSep = 20;
            int itemHeight = 100;
            for (int index = 0; index < 3; index ++ )
            {
                Panel p = new Panel();
                orderShowPanel.Controls.Add(p);
                p.Location = new Point(xSep, 2 + (itemHeight + 2) * index);
                p.Size = new Size(orderShowPanel.Width - xSep * 2, itemHeight);

                if (index % 2 == 0)
                {
                    p.BackColor = Color.Red;
                }
                else
                {
                    p.BackColor = Color.Blue;
                }
            }
        }
    }
}
