using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HsServiceCore.Service.ViewCtrl
{
    public partial class PageSwitchView : UserControl
    {
        public PageSwitchView()
        {
            InitializeComponent();
        }

        private void PageSwitchView_Load(object sender, EventArgs e)
        {

            setButtonImage(btnStart, "starticon-0.png");
            setButtonImage(btnPrev, "previcon-0.png");
            setButtonImage(btnNext, "nexticon-0.png");
            setButtonImage(btnEnd, "endicon-0.png");

        }

        private void setButtonImage(Button btn, string imageName)
        {
            string ResourceDir = Path.Combine(Application.StartupPath, "Res");
            btn.Image = Image.FromFile(Path.Combine(ResourceDir, imageName));
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            // 临时代码
            if(imageName.Contains("-0.png"))
            {
                btn.Enabled = false;
            }
            else
            {
                btn.Enabled = true;
            }
        }
    }
}
