using Hs.Comminucation;
using HsServiceCore.Service.ViewCtrl;
using HsServiceCore.ServiceData;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HsServiceCore.Service.Priv
{
    public partial class MenuMangeForm : Form
    {
        OpertorType _operType;

        public OpertorType OperType
        {
            get { return _operType; }
            set { _operType = value; }
        }
        ServiceItem _srvItem;

        public ServiceItem SrvItem
        {
            get { return _srvItem; }
            set { _srvItem = value; }
        }

        BaseManageCtr _manageCtrl;

        public BaseManageCtr ManageCtrl
        {
            get { return _manageCtrl; }
            set { _manageCtrl = value; }
        }


        public MenuMangeForm()
        {
            InitializeComponent();
        }

        private void pbImage_MouseEnter(object sender, EventArgs e)
        {
            loadSaveImage(true);
        }

        private void pbImage_MouseLeave(object sender, EventArgs e)
        {
            loadSaveImage(false);
        }

        private void MenuMangeForm_Load(object sender, EventArgs e)
        {
            loadSaveImage(false);

            loadMenuList();

            initManageCtrl();
        }

        private void initManageCtrl()
        {
            if (ManageCtrl == null)
            {
                ManageCtrl = new BaseManageCtr();
                ManageCtrl.OperType = OperType;
                ManageCtrl.SrvItem = _srvItem;

                this.Controls.Add(ManageCtrl);
            }
            ManageCtrl.Location = new Point(5, 40);
            ManageCtrl.Size = new Size(this.Width, this.Height - 40);
        }

        private void loadMenuList()
        {
            //cbParentUrl.DataSource = GlobalConfig.SystemMenus;
        }

        private void loadSaveImage(bool pFlag)
        {
            string SrcDir = Path.Combine(Application.StartupPath, "Res");

            if (!pFlag)
            {
                pbImage.Image = Image.FromFile(Path.Combine(SrcDir, "save-0.png"));
            }
            else
            {
                pbImage.Image = Image.FromFile(Path.Combine(SrcDir, "save.png"));
            }
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            HsApply applyLogin = new HsApply(GlobalConfig.getUrlHead(), SrvItem.CmdString);
            applyLogin.InvokeType = SrvItem.getHttpType(_operType);

            //HsParam param1 = new HsParam("Account", userName.Text.ToString(), DataType.String, InvType.POST);
            //HsParam param2 = new HsParam("Password", ToolsUtils.Md5(userPassword.Text), DataType.String, InvType.POST);
            //applyLogin.addParam(param1);
            //applyLogin.addParam(param2);
            //for (int index = 0 ; index < SrvItem)
            for (int index = 0 ; index < SrvItem.ServiceItemAttrs.Count; index ++)
            {
                ServiceItemAttribute attr = SrvItem.ServiceItemAttrs[index];
                
                HsParam param1 = attr.buildParam(applyLogin.InvokeType);


                param1.ParamValue = ManageCtrl.getInputValue(attr.KName);

                applyLogin.addParam(param1);
              
            }

            HsResultData resultDat = applyLogin.excuteCommand();

            //GlobalConfig.SystemProtocols.Clear();
            if (resultDat.ErrorId == 200)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                return ;
            }
            MessageBox.Show(resultDat.ErrorInfo);
            return ;
        }

        private void btnGuid_Click(object sender, EventArgs e)
        {
            //tbUrlId.Text = Guid.NewGuid().ToString();
        }
    }
}
