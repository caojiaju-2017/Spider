using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HsServiceCore;
using System.Threading;
using Hs.Comminucation;
using HsServiceCore;
using Newtonsoft.Json.Linq;

namespace HsServiceCore
{
    public partial class HsServiceTree : UserControl
    {
        TreeView _srvTreeView;
        ImageList _imgList = new ImageList();
        object _lockObj = new object();
        public ImageList ImgList
        {
            get {
                _imgList.ImageSize = new Size(16, 16);
                return _imgList;
            }
            set { _imgList = value; }
        }
        

        public TreeView SrvTreeView
        {
            get {
                return _srvTreeView; 
            }
            set { _srvTreeView = value; }
        }



        List<ServiceItem> _srvItems = new List<ServiceItem>();
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ServiceItem> SrvItems
        {
            get { return _srvItems; }
            set { 
                value.Sort();
                _srvItems = value; }
        }


        public HsServiceTree()
        {
            InitializeComponent();
            loadFunctionTree();
        }
        private void CreatTree()
        {
            SrvTreeView = new TreeView();
            SrvTreeView.Location = new Point(0, 0);
            SrvTreeView.Width = this.Width;
            SrvTreeView.Height = this.Height;
            SrvTreeView.ImageList = ImgList;
            SrvTreeView.ItemHeight = 25;
            SrvTreeView.ShowLines = false;
            SrvTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            SrvTreeView.BackColor = Color.FromArgb(236,241,250);

            // 注册事件
            SrvTreeView.AfterSelect += nodeSelect_AfterSelect;
            this.Controls.Add(SrvTreeView);
        }

        public void loadFunctionTree()
        {
            if (SrvTreeView == null)
            {
                CreatTree();
            }

            // 获取系统配置
            SrvItems = ServiceInit.loadSystemFuncTree();
            if (SrvItems != null && SrvItems.Count > 0)
            {
                initTree();
            }
        }

        private void initTree()
        {
            SrvTreeView.Nodes.Clear();
            Dictionary<string, TreeNode> alreadyNodes = new Dictionary<string, TreeNode>();
            ImgList.Images.Clear();
            List<ServiceItem> cloneDatas = new List<ServiceItem>(SrvItems.ToArray());

            int leftCount = cloneDatas.Count;
            while (true)
            {
                for (int index = 0; index < cloneDatas.Count; index++)
                {
                    ServiceItem item = cloneDatas[index];

                    if (item.IsVirtual == 2)
                    {
                        continue;
                    }

                    // 检查是否为根节点
                    if ((item.PUrlId == null || item.PUrlId == "") && item.IsVirtual == 1)
                    {
                        TreeNode rootNode = new TreeNode();
                        rootNode.Text = item.Name;
                        rootNode.Tag = item;
                        SrvTreeView.Nodes.Add(rootNode);

                        alreadyNodes.Add(item.UrlId, rootNode);
                        StartLoadNodeImage(rootNode, item);
                        cloneDatas.RemoveAt(index--);
                        continue;
                    }

                    if (alreadyNodes.ContainsKey(item.PUrlId))
                    {
                        TreeNode parentNode = alreadyNodes[item.PUrlId];

                        TreeNode subNode = new TreeNode();
                        subNode.Text = item.Name;
                        subNode.Tag = item;
                        parentNode.Nodes.Add(subNode);

                        alreadyNodes.Add(item.UrlId, subNode);
                        StartLoadNodeImage(subNode, item);
                        cloneDatas.RemoveAt(index--);
                        continue;
                    }

                }

                // 如果没有处理任何节点，则退出
                if (leftCount == cloneDatas.Count)
                {
                    break;
                }

                leftCount = cloneDatas.Count;
            }
            
        }

        private void HsServiceTree_Load(object sender, EventArgs e)
        {
            if (SrvTreeView == null)
            {
                CreatTree();
            }
            SrvTreeView.Width = this.Width;
            SrvTreeView.Height = this.Height;
        }

        private void HsServiceTree_Resize(object sender, EventArgs e)
        {
            if (SrvTreeView == null)
            {
                CreatTree();
            }
            SrvTreeView.Width = this.Width;
            SrvTreeView.Height = this.Height;
        }

        private void StartLoadNodeImage(TreeNode node,ServiceItem item)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    HsApply remoteInvoke = new HsApply(GlobalConfig.getUrlHead(), "GET_BYTES");
                    remoteInvoke.ReturnType = 1;
                    remoteInvoke.InvokeType = InvType.GET;

                    HsParam param1 = new HsParam("Name", item.ImageName, DataType.String, InvType.GET);
                    HsParam param2 = new HsParam("Type", "0", DataType.String, InvType.GET);

                    remoteInvoke.addParam(param1);
                    remoteInvoke.addParam(param2);


                    HsResultData resultData = remoteInvoke.excuteCommand();

                    SrvTreeView.Invoke(new EventHandler(delegate
                               {
                                   if (resultData.ResultString == null)
                                   {

                                   }
                                   else
                                   {
                                       try
                                       {
                                           byte[] byteDatas = Convert.FromBase64String(resultData.ResultString.ToString());
                                           Image img = ImageHelper.BytesToImage(byteDatas);
                                           if (img != null)
                                           {
                                               lock (_lockObj)
                                               {
                                                   ImgList.Images.Add(img);
                                                   node.ImageIndex = ImgList.Images.Count - 1;
                                                   node.SelectedImageIndex = node.ImageIndex;
                                               }
                                           }
                                       }
                                       catch
                                       {

                                       }

                                   }

                               }));

                }
                catch (Exception ex)
                {
                }
            });
            t.Start();
        }

        private void nodeSelect_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ServiceItem item = e.Node.Tag as ServiceItem;

            if (item == null)
            {
                return;
            }

            // 是否为虚拟节点
            // 如果为虚节点，则展开或关闭树形显示
            if (item.IsVirtual == 1)
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                }
                else
                {
                    e.Node.Expand();
                }

                return;
            }

            
            // 显示新控件
            HsWebForm.g_WebForm.loadNewCtrl(item);
        }
    }
}
