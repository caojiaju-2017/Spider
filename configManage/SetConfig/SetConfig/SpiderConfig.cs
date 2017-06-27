using SetConfig.DbAccess;
using SetConfig.DbAccess.DataStruct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetConfig
{
    public partial class SpiderConfig : Form
    {
        #region Data
        List<SConfig> _scList = new List<SConfig>();

        public List<SConfig> ScList
        {
            get { return _scList; }
            set { _scList = value; }
        }

        List<SUrl> _suList = new List<SUrl>();

        public List<SUrl> SuList
        {
            get { return _suList; }
            set { _suList = value; }
        }

        List<SUrlAttribute> _attrList = new List<SUrlAttribute>();

        public List<SUrlAttribute> AttrList
        {
            get { return _attrList; }
            set { _attrList = value; }
        }
        #endregion

        static MysqlDBHelper _dbHelper;

        public static MysqlDBHelper DbHelper
        {
            get { return _dbHelper; }
            set { _dbHelper = value; }
        }

        public SpiderConfig()
        {
            InitializeComponent();
            
        }

        private void SpiderConfig_Load(object sender, EventArgs e)
        {
            _dbHelper = new MysqlDBHelper();
            if (!_dbHelper.OpenDb())
            {
                MessageBox.Show("");
                return;
            }

            Thread productThread = new Thread(() =>
            {
                _scList = SConfig.FetchObject(DbHelper);
                _attrList = SUrlAttribute.FetchObject(DbHelper);

                _suList = SUrl.FetchObject(DbHelper);

                SUrl.buildAttribute(_suList, _attrList);

                updateUi();
            });
            productThread.Start();

        }

        private void updateUi()
        {
            configTree.Invoke(new EventHandler(delegate
            {
                for (int index = 0; index < _suList.Count; index ++)
                {
                    TreeNode node = new TreeNode(_suList[index].Alias);
                    node.Tag = _suList[index];
                    configTree.Nodes.Add(node);
                }
            }));
        }

        #region 事件
        private void btnNewConfig_Click(object sender, EventArgs e)
        {
            TreeNode newNode = new TreeNode();
            newNode.Text = "新配置";
            configTree.Nodes.Add(newNode);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }


        private void btnUpdateCfg_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
