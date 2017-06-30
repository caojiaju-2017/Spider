using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 数据库管理.DAL;
using 数据库管理.Model;

namespace 数据库管理
{
    public partial class CopyUrlWnd : Form
    {
        public CopyUrlWnd()
        {
            InitializeComponent();
        }

        private string _urlCode;

        public string UrlCode
        {
            get { return _urlCode; }
            set { _urlCode = value; }
        }

        private void CopyUrlWnd_Load(object sender, EventArgs e)
        {
            Sp_config[] sp = new Sp_configDAL().GetAllData();

            cbConfig.DataSource = sp;
        }

        private void cbConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sp_config cfg =(Sp_config) cbConfig.SelectedItem;

            if (cfg == null)
            {
                return;
            }

            Sp_url[] spUrl = new Sp_urlDAL().GetAllData(cfg.Code);
            double[] arr = new double[50];

            List<Sp_url> list = spUrl.ToList();//把数组转换成泛型类
            for (int index = 0; index < list.Count; index++ )
            {
                Sp_url oneUrl = list[index];

                if (oneUrl.Code == UrlCode)
                {
                    list.RemoveAt(index);
                    break;
                }
            }
                //double[] newarr = list.ToArray();//再由泛型类转换成数组

            cbUrl.DataSource = list;

        }

        private void cbUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sp_url url = (Sp_url)cbUrl.SelectedItem;
            if (url == null)
            {
                return;
            }


            Sp_url_attr[] spUrl = new Sp_url_attrDAL().GetAllData(url.Code);
            tvAttr.Nodes.Clear();

            for (int i = 0; i < spUrl.Length; i++)
            {
                TreeNode node = new TreeNode(spUrl[i].ToString());
                node.Tag = spUrl[i];
                tvAttr.Nodes.Add(node);
            }
        }

        private Dictionary<string, List<string>> _result;

        public Dictionary<string, List<string>> Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            for (int index = 0 ; index < tvAttr.Nodes.Count ; index ++)
            {
                TreeNode node = tvAttr.Nodes[index];

                if (!node.Checked)
                {
                    continue;
                }

                Sp_url_attr attr = (Sp_url_attr)node.Tag;
                if (attr == null)
                {
                    continue;
                }

                if (Result == null)
                {
                    _result = new Dictionary<string, List<string>>();
                }

                List<string> urlCodes;
                if (Result.ContainsKey(attr.UrlCode))
                {
                    urlCodes = Result[attr.UrlCode];
                }
                else
                {
                    urlCodes = new List<string>();
                }

                urlCodes.Add(attr.Code);

                Result[attr.UrlCode] = urlCodes;

            }

            if (Result == null)
            {
                MessageBox.Show("未选中任何需要拷贝的属性");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            setSelectAll(true);
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            setSelectAll(false);
        }

        private void setSelectAll(bool flag)
        {
            for (int index = 0; index < tvAttr.Nodes.Count; index++)
            {
                TreeNode node = tvAttr.Nodes[index];
                node.Checked = flag;
            }
        }
    }
}
