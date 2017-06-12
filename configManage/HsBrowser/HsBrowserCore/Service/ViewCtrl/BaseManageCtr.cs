using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HsServiceCore.ServiceData;

namespace HsServiceCore.Service.ViewCtrl
{
    public partial class BaseManageCtr : BaseController
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
            set { 
                _srvItem = value;

                initCtrl();
            }
        }

        private void initCtrl()
        {
            int labWidth = 100;
            int ctrlHeight = 20;
            int xSep = 40;
            int rowSep = 25;
            for (int index = 0 ; index < SrvItem.ServiceItemAttrs.Count; index ++)
            {
                ServiceItemAttribute attr = SrvItem.ServiceItemAttrs[index];

                Label lab = new Label();
                lab.Text = attr.KAlias;
                lab.Font = new System.Drawing.Font("宋体", 12);
                lab.Location = new Point(xSep, index * (rowSep + 20));
                lab.Size = new Size(labWidth, ctrlHeight);
                this.Controls.Add(lab);


                if (attr.SelObjects == null || attr.SelObjects == "")
                {
                    TextBox box = new TextBox();
                    box.Name = attr.KName;
                    box.Font = new Font("宋体", 10);
                    box.Location = new Point(xSep + labWidth + xSep , index * (rowSep + 20));
                    box.Size = new Size(150, ctrlHeight);

                    if (OperType == OpertorType.Modi)
                    {
                        box.Text = ToolsUtils.GetObjectPropertyValue<ServiceItem>(SrvItem, attr.KName);
                    }
                    this.Controls.Add(box);

                    box.Tag = attr;
                }
                else
                {
                    string selString = attr.SelObjects.ToString();

                    if (selString.Contains("{") && selString.Contains("}"))
                    {
                        selString = selString.Replace("{","");
                        selString = selString.Replace("}","");
                        RadioButton rbtn1 = new RadioButton();
                        rbtn1.Text = selString.Split(':')[0].ToString();
                        rbtn1.Font = new Font("宋体", 10);
                        rbtn1.Location = new Point(lab.Location.X + lab.Width + xSep, index * (rowSep + 20));
                        rbtn1.Size = new Size(50, ctrlHeight);
                        rbtn1.Tag = attr;
                        this.Controls.Add(rbtn1);



                        RadioButton rbtn2 = new RadioButton();
                        rbtn2.Text = selString.Split(':')[1].ToString();
                        rbtn2.Font = new Font("宋体", 10);
                        rbtn2.Location = new Point(rbtn1.Location.X + rbtn1.Width + 5 , index * (rowSep + 20));
                        rbtn2.Size = new Size(50, ctrlHeight);
                        this.Controls.Add(rbtn2);
                    }
                    else if (selString.Contains("$"))
                    {
                        ComboBox cbBox = new ComboBox();
                        cbBox.Name = attr.KName;
                        cbBox.Font = new Font("宋体", 10);
                        cbBox.Location = new Point(xSep + labWidth + xSep, index * (rowSep + 20));
                        cbBox.Size = new Size(150, ctrlHeight);
                        cbBox.Tag = attr;
                        string AttrName = selString.Replace("$", "");
                        cbBox.DataSource = ToolsUtils.GetStaticAttribute(AttrName);
                        this.Controls.Add(cbBox);

                        if (OperType == OpertorType.Modi)
                        {
                            string code = ToolsUtils.GetObjectPropertyValue<ServiceItem>(SrvItem, attr.KName);

                            for (int inx = 0; inx < ((List<object>)cbBox.DataSource).Count; inx++)
                            {
                                object itemObj = ((List<object>)cbBox.DataSource)[inx];

                                string itemCode = ToolsUtils.getObjectId(itemObj);

                                if (code == itemCode)
                                {
                                    cbBox.SelectedIndex = inx;
                                }
                            }
                        }
                    }
                }
                
            }
        }

        public BaseManageCtr()
        {
            InitializeComponent();
        }

        int index = 0;
        public object getInputValue(string kname)
        {
            for (int index = 0 ; index < this.Controls.Count ; index ++)
            {
                Control ctrl = this.Controls[index];

                if (ctrl.Tag == null)
                {
                    continue;
                }

                ServiceItemAttribute attr = ctrl.Tag as ServiceItemAttribute;

                if(attr.KName == kname)
                {
                    if (ctrl is TextBox)
                    {
                        return ((TextBox)ctrl).Text;
                    }
                    else if (ctrl is RadioButton)
                    {
                        RadioButton rbt = (RadioButton)ctrl;
                        if (rbt.Checked)
                        {
                            return (rbt.Text == "是" ? 1 : 0);
                        }
                        continue;
                    }
                    else if (ctrl is ComboBox)
                    {
                        ComboBox rbt = (ComboBox)ctrl;

                        object selObj = rbt.SelectedItem;

                        if (selObj == null)
                        {
                            return null;
                        }

                        return ToolsUtils.getObjectId(selObj);

                    }
                }
            }

            return null;
        }


        internal void loadSrvData()
        {
            
        }
    }
}
