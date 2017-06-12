using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HsServiceCore.Service.CommCtrl;
using System.IO;
using HsServiceCore.Service.Priv;
using HsServiceCore.ServiceData;
using Hs.Comminucation;

namespace HsServiceCore.Service.ViewCtrl
{
    
    public partial class BaseListCtrl : BaseController
    {
        CXListView _dataList;
        PageSwitchView _pageSwitchView;

        int _manageHeight = 40;
        int _pageSwitchHeight = 40;
        int _publicSep = 5;

        Button _addButton;
        Button _modiButton;
        Button _deleButton;
        Button _refreshButton;

        ImageList _imgList = new ImageList();

        ServiceItem _item;

        public BaseListCtrl()
        {
            InitializeComponent();
            _imgList.ImageSize = new System.Drawing.Size(24, 24);
            InitCtrl();
        }

        #region 控件初始化
        private void InitCtrl()
        {
            if (_dataList == null)
            {
                _dataList = new CXListView();
                _dataList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                
                _dataList.BackColor = Color.FromArgb(245, 245, 245);
                _dataList.GridLines = true;
                _dataList.CheckBoxes = true;
                _dataList.FullRowSelect = true;
                _dataList.SmallImageList = _imgList;
                //_dataList.Font = new Font("仿宋", 14);
                this.Controls.Add(_dataList);
            }

            if (_pageSwitchView == null)
            {
                _pageSwitchView = new PageSwitchView();
                //_pageSwitchView.BackColor = Color.Purple;
                this.Controls.Add(_pageSwitchView);
            }

#region 管理按钮初始化
            if (_addButton == null)
            {
                _addButton = new Button();
                _addButton.Text = "添加";

                string fileName = Path.Combine(Application.StartupPath, "Res");
                fileName = Path.Combine(fileName, "add.png");

                _addButton.Image = Image.FromFile(fileName);
                _addButton.ImageAlign = ContentAlignment.MiddleLeft;
                _addButton.TextAlign = ContentAlignment.MiddleRight;
                _addButton.FlatAppearance.BorderSize = 0;
                this.Controls.Add(_addButton);
            }
            if (_modiButton == null)
            {
                _modiButton = new Button();
                _modiButton.Text = "修改";

                string fileName = Path.Combine(Application.StartupPath, "Res");
                fileName = Path.Combine(fileName, "modi.png");

                _modiButton.Image = Image.FromFile(fileName);
                _modiButton.ImageAlign = ContentAlignment.MiddleLeft;
                _modiButton.TextAlign = ContentAlignment.MiddleRight;
                _modiButton.FlatAppearance.BorderSize = 0;
                this.Controls.Add(_modiButton);
            }

            if (_deleButton == null)
            {
                _deleButton = new Button();
                _deleButton.Text = "删除";

                string fileName = Path.Combine(Application.StartupPath, "Res");
                fileName = Path.Combine(fileName, "dele.png");

                _deleButton.Image = Image.FromFile(fileName);
                _deleButton.ImageAlign = ContentAlignment.MiddleLeft;
                _deleButton.TextAlign = ContentAlignment.MiddleRight;
                _deleButton.FlatAppearance.BorderSize = 0;
                this.Controls.Add(_deleButton);
            }

            if (_refreshButton == null)
            {
                _refreshButton = new Button();
                _refreshButton.Text = "刷新";

                string fileName = Path.Combine(Application.StartupPath, "Res");
                fileName = Path.Combine(fileName, "reficon.png");

                _refreshButton.Image = Image.FromFile(fileName);
                _refreshButton.ImageAlign = ContentAlignment.MiddleLeft;
                _refreshButton.TextAlign = ContentAlignment.MiddleRight;
                _refreshButton.FlatAppearance.BorderSize = 0;
                this.Controls.Add(_refreshButton);
            }
#endregion

        }

        private void BaseListCtrl_Load(object sender, EventArgs e)
        {
            registerEvent();
            setControlPosition();
        }

        private void setControlPosition()
        {
            InitCtrl();

            _dataList.Location = new Point(0, _manageHeight);
            _dataList.Size = new Size(this.Width - _publicSep, this.Height - _manageHeight - _pageSwitchHeight - _publicSep);

            _pageSwitchView.Location = new Point(0, _manageHeight + _dataList.Height);
            _pageSwitchView.Size = new Size(this.Width - _publicSep, _pageSwitchHeight - _publicSep);

            _addButton.Location = new Point(_publicSep, _publicSep);
            _addButton.Size = new Size(70, 32);
            _addButton.FlatStyle = FlatStyle.Flat;

            _modiButton.Location = new Point(_publicSep + _addButton.Location.X + _addButton.Width, _publicSep);
            _modiButton.Size = new Size(70, 32);
            _modiButton.FlatStyle = FlatStyle.Flat;

            _deleButton.Location = new Point(_publicSep + _modiButton.Location.X + _modiButton.Width, _publicSep);
            _deleButton.Size = new Size(70, 32);
            _deleButton.FlatStyle = FlatStyle.Flat;

            _refreshButton.Location = new Point(_publicSep + _deleButton.Location.X + _deleButton.Width, _publicSep);
            _refreshButton.Size = new Size(70, 32);
            _refreshButton.FlatStyle = FlatStyle.Flat;
        }
        #endregion

        private void BaseListCtrl_SizeChanged(object sender, EventArgs e)
        {
            setControlPosition();
        }

        #region 注册点击事件
        void registerEvent()
        {
            _addButton.Click +=  new System.EventHandler(add_Click);
            _modiButton.Click += new System.EventHandler(modi_Click);
            _deleButton.Click += new System.EventHandler(dele_Click);
            _refreshButton.Click += new System.EventHandler(refresh_Click);
        }
        private void add_Click(object sender, EventArgs e)
        {
            MenuMangeForm form = new MenuMangeForm();

            form.SrvItem = GlobalConfig.getMenu(_item.getOperCommand(OpertorType.Add));
            form.OperType = ServiceData.OpertorType.Add;

            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("操作成功");
                HsWebForm.g_WebForm.main_workSpace.SrvTree.loadFunctionTree();
            }
        }
        private void modi_Click(object sender, EventArgs e)
        {
            ServiceItem srvItem = null;
            for (int index = 0 ; index < _dataList.Items.Count ; index ++)
            {
                ListViewItem item = _dataList.Items[index];

                if (item.Checked)
                {
                    srvItem = item.Tag as ServiceItem;
                    break;
                }
            }

            if (srvItem == null)
            {
                MessageBox.Show("未选中需要修改的数据");
                return;
            }

            MenuMangeForm form = new MenuMangeForm();

            form.SrvItem = srvItem;
            form.OperType = ServiceData.OpertorType.Modi;

            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("操作成功");
                HsWebForm.g_WebForm.main_workSpace.SrvTree.loadFunctionTree();
            }
        }
        private void dele_Click(object sender, EventArgs e)
        {
            int success = 0;
            int total = 0;
            for (int index = 0; index < _dataList.Items.Count; index++)
            {
                ListViewItem item = _dataList.Items[index];

                if (item.Checked)
                {
                    ServiceItem srvItem = item.Tag as ServiceItem;

                    ServiceItem deleProtocol = GlobalConfig.getMenu(_item.getOperCommand(OpertorType.Delete));

                    total = total + 1;
                    bool result = excuteDelete(deleProtocol, srvItem);

                    if (result)
                    {
                        success = success + 1;
                    }
                    continue;
                }
            }
            MessageBox.Show(String.Format("处理完毕，处理{0},成功{1}",total,success));
        }

        private bool excuteDelete(ServiceItem deleProtocol, ServiceItem srvItem)
        {
            HsApply applyLogin = new HsApply(GlobalConfig.getUrlHead(), deleProtocol.CmdString);
            applyLogin.InvokeType = deleProtocol.getHttpType(OpertorType.Delete);

            for (int index = 0; index < deleProtocol.ServiceItemAttrs.Count; index++)
            {
                ServiceItemAttribute attr = deleProtocol.ServiceItemAttrs[index];

                if (attr.KName == "PreApi")
                {
                    continue;
                }
                HsParam param1 = attr.buildParam(applyLogin.InvokeType);


                param1.ParamValue = ToolsUtils.GetObjectPropertyValue<ServiceItem>(srvItem, attr.KName);

                applyLogin.addParam(param1);

            }

            HsResultData resultDat = applyLogin.excuteCommand();

            //GlobalConfig.SystemProtocols.Clear();
            if (resultDat.ErrorId == 200)
            {
                return true;
            }
            //MessageBox.Show(resultDat.ErrorInfo);
            return false;
        }

        private void refresh_Click(object sender, EventArgs e)
        {

        }
        #endregion

        // 装在数据
        internal void LoadData(ServiceItem item)
        {
            _item = item;
            if (item.CmdString == "FETCH_MENUS")
            {
                initHeader(item.ServiceItemAttrs);

                loadMenuData();
            }
            
        }

        private void loadMenuData()
        {
            _dataList.Items.Clear();
            for (int index = 0 ; index < GlobalConfig.SystemMenus.Count ; index ++)
            {
                ServiceItem menu = GlobalConfig.SystemMenus[index];
                ListViewItem item = new ListViewItem();
                item.Tag = menu;
                for (int inx = 0; inx < _item.ServiceItemAttrs.Count; inx++)
                {
                    ServiceItemAttribute attr = _item.ServiceItemAttrs[inx];

                    if (inx == 0)
                    {
                        item.Text = ToolsUtils.GetObjectPropertyValue<ServiceItem>(menu, attr.KName);
                        continue;
                    }

                    item.SubItems.Add(ToolsUtils.GetObjectPropertyValue<ServiceItem>(menu, attr.KName));
                }
                

                //item.SubItems.Add(menu.Url);
                //item.SubItems.Add(menu.PUrlId);
                //item.SubItems.Add(menu.UrlId);
                //item.SubItems.Add(menu.ImageName);
                //item.SubItems.Add(menu.Inx.ToString());
                //item.SubItems.Add((menu.IsVirtual == 0? "否":"是"));
                //item.SubItems.Add(menu.CmdString);

                _dataList.Items.Add(item);
            }
        }

        //private void initManageHeader()
        //{
        //    _dataList.Columns.Clear();
        //    int widthTotal = 0;

        //    ColumnHeader headName = new ColumnHeader();
        //    headName.Text = "菜单名";
        //    headName.Tag = "Name";
        //    headName.Width = 100;
        //    widthTotal = widthTotal + 100;
        //    _dataList.Columns.Add(headName);

        //    ColumnHeader newHeader1 = new ColumnHeader();
        //    newHeader1.Text = "Url地址";
        //    newHeader1.Tag = "Url";
        //    newHeader1.Width = 200;
        //    widthTotal = widthTotal + 200;
        //    _dataList.Columns.Add(newHeader1);

        //    ColumnHeader newHeader2 = new ColumnHeader();
        //    newHeader2.Text = "父亲URLId";
        //    newHeader2.Tag = "PUrlId";
        //    newHeader2.Width = 150;
        //    widthTotal = widthTotal + 150;
        //    _dataList.Columns.Add(newHeader2);


        //    ColumnHeader newHeader3 = new ColumnHeader();
        //    newHeader3.Text = "Url代码";
        //    newHeader3.Tag = "UrlId";
        //    newHeader3.Width = 150;
        //    widthTotal = widthTotal + 150;
        //    _dataList.Columns.Add(newHeader3);

        //    ColumnHeader newHeader4 = new ColumnHeader();
        //    newHeader4.Text = "菜单图片";
        //    newHeader4.Tag = "ImageName";
        //    newHeader4.Width = 100;
        //    widthTotal = widthTotal + 100;
        //    _dataList.Columns.Add(newHeader4);

        //    ColumnHeader newHeader5 = new ColumnHeader();
        //    newHeader5.Text = "显示索引";
        //    newHeader5.Tag = "Inx";
        //    newHeader5.Width = 60;
        //    widthTotal = widthTotal + 60;
        //    _dataList.Columns.Add(newHeader5);

        //    ColumnHeader newHeader6 = new ColumnHeader();
        //    newHeader6.Text = "虚拟节点";
        //    newHeader6.Tag = "IsVirtual";
        //    newHeader6.Width = 60;
        //    widthTotal = widthTotal + 60;
        //    _dataList.Columns.Add(newHeader6);

        //    ColumnHeader newHeader7 = new ColumnHeader();
        //    newHeader7.Text = "命令码";
        //    newHeader7.Tag = "CmdString";
        //    newHeader7.Width = 120;
        //    widthTotal = widthTotal + 120;
        //    _dataList.Columns.Add(newHeader7);

        //    // 增加空列
        //    ColumnHeader nullHeader = new ColumnHeader();
        //    nullHeader.Text = "";
        //    nullHeader.Width = _dataList.Width - widthTotal - 50;

        //    _dataList.Columns.Add(nullHeader);
        //}

        private void querySystemData()
        {
            _dataList.Items.Clear();

        }

        private void initHeader(List<ServiceItemAttribute> attrs)
        {
            // 初始化列头
            _dataList.Columns.Clear();
            int widthTotal = 0;
            for (int index = 0; index < attrs.Count; index++)
            {
                ServiceItemAttribute attr = attrs[index];

                if (attr == null || attr.IsShow == 0)
                {
                    continue;
                }

                ColumnHeader newHeader = new ColumnHeader();
                newHeader.Text = attr.KAlias;
                newHeader.Tag = attr;
                newHeader.Width = 100;
                widthTotal =  widthTotal + 100;
                _dataList.Columns.Add(newHeader);

            }

            // 增加空列
            ColumnHeader nullHeader = new ColumnHeader();
            nullHeader.Text = "";
            nullHeader.Width = _dataList.Width - widthTotal - 50;

            _dataList.Columns.Add(nullHeader);
        }
    }
}
