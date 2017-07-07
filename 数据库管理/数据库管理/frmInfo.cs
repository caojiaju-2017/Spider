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
    public partial class frmInfo : Form
    {
        public frmInfo()
        {
            InitializeComponent();
        }
        public string Code { get; set; }
        private void frmInfo_Load(object sender, EventArgs e)
        {
            ShowDGV();
            txtConfigId.Text = Code;
            txtCode.Text = "SU_" + GetTimeStamp() + new Random().Next(10000, 99999);
            txtAttrCode.Text = "SUA_" + GetTimeStamp() + new Random().Next(10000, 99999);
            cBoxLoopType.Text = "静态模式";
            cBoxEnable.Text = "是";
            cBoxCalcWay.Text = "替换";
            cBoxIsUrl.Text = "是";
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private void ShowDGV()
        {
            dgvShowUrl.Rows.Clear();
            Sp_url[] spUrl = new Sp_urlDAL().GetAllData(Code);
            for (int i = 0; i < spUrl.Length; i++)
            {
                int d = dgvShowUrl.Rows.Add();
                dgvShowUrl.Rows[d].Cells["编号Url"].Value = d;
                dgvShowUrl.Rows[d].Cells["索引开始"].Value = spUrl[i].StartIndex;
                dgvShowUrl.Rows[d].Cells["索引结束"].Value = spUrl[i].StopIndex;
                dgvShowUrl.Rows[d].Cells["步长"].Value = spUrl[i].Step;
                dgvShowUrl.Rows[d].Cells["基础URL"].Value = spUrl[i].BaseUrl;
                dgvShowUrl.Rows[d].Cells["动态变化URL"].Value = spUrl[i].ShortUrl;
                dgvShowUrl.Rows[d].Cells["循环模式"].Value = spUrl[i].LoopType;
                dgvShowUrl.Rows[d].Cells["网站名"].Value = spUrl[i].Name;
                dgvShowUrl.Rows[d].Cells["网站别名"].Value = spUrl[i].Alias;
                dgvShowUrl.Rows[d].Cells["Sheet名称"].Value = spUrl[i].Sheet;
                dgvShowUrl.Rows[d].Cells["归属配置Code"].Value = spUrl[i].ConfigId;
                dgvShowUrl.Rows[d].Cells["记录的唯一索引"].Value = spUrl[i].Code;
                dgvShowUrl.Rows[d].Cells["分类"].Value = spUrl[i].Classfic;
                if (spUrl[i].LoopType == 1)
                {
                    dgvShowUrl.Rows[d].Cells["循环模式"].Value = "静态模式";
                }
                else
                {
                    dgvShowUrl.Rows[d].Cells["循环模式"].Value = "循环模式";
                }
                if (spUrl[i].Enable == 1)
                {
                    dgvShowUrl.Rows[d].Cells["是否生效"].Value = "是";
                }
                else
                {
                    dgvShowUrl.Rows[d].Cells["是否生效"].Value = "否";
                }
                dgvShowUrl.Rows[d].Cells["Id"].Value = spUrl[i].Id;
            }
            
            if (dgvShowUrl.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvShowUrl.Rows)
                    dgvr.Selected = false;
                dgvShowUrl.Rows[0].Selected = true;
            }
        }

        private void btnAddUrl_Click(object sender, EventArgs e)
        {
            if (txtStartIndex.Text.Trim() == "" || txtStopIndex.Text.Trim() == "" || txtStep.Text.Trim() == "" || txtBaseUrl.Text.Trim() == "" || txtName.Text.Trim() == "" || txtAlias.Text.Trim() == "" || txtSheet.Text.Trim() == "" || txtClassfic.Text.Trim() == "")
            {
                MessageBox.Show("索引步长等信息为必填项！");
                return;
            }
            Sp_url spUrl = new Sp_url();
            //Sp_url[] spUrlAll = new Sp_urlDAL().GetAllData();
            //spUrl.Id = spUrlAll.Length + 1;
            spUrl.StartIndex = Convert.ToInt32(txtStartIndex.Text.Trim());
            spUrl.StopIndex = Convert.ToInt32(txtStopIndex.Text.Trim());
            spUrl.Step = Convert.ToInt32(txtStep.Text.Trim());
            spUrl.BaseUrl = txtBaseUrl.Text.Trim();
            spUrl.ShortUrl = txtShortUrl.Text.Trim();
            if (cBoxLoopType.Text == "静态模式")
            {
                spUrl.LoopType = 1;
            }
            else
            {
                spUrl.LoopType = 0;
            }
            spUrl.Name = txtName.Text.Trim();
            spUrl.Alias = txtAlias.Text.Trim();
            spUrl.Sheet = txtSheet.Text.Trim();
            spUrl.ConfigId = txtConfigId.Text.Trim();
            spUrl.Code = txtCode.Text.Trim();
            if (cBoxEnable.Text == "是")
            {
                spUrl.Enable = 1;
            }
            else
            {
                spUrl.Enable = 0;
            }
            spUrl.Classfic = txtClassfic.Text.Trim();
            if (btnAddUrl.Text == "新  增")
            {
                try
                {
                    new Sp_urlDAL().Insert(spUrl);
                    MessageBox.Show("添加成功");
                    ShowDGV();
                    txtConfigId.Text = Code;
                    txtCode.Text = "SU_" + GetTimeStamp() + new Random().Next(10000, 99999);
                    txtStartIndex.Text = ""; txtStopIndex.Text = ""; txtStep.Text = ""; txtBaseUrl.Text = ""; txtShortUrl.Text = ""; txtName.Text = ""; txtAlias.Text = ""; txtSheet.Text = ""; txtClassfic.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加失败,错误代码为：" + ex.ToString());
                }
            }
            else
            {
                try
                {
                    spUrl.Id = Convert.ToInt32(dgvShowUrl.SelectedRows[0].Cells["Id"].Value);
                    new Sp_urlDAL().Update(spUrl);
                    MessageBox.Show("修改成功");
                    btnAddUrl.Text = "新  增";
                    ShowDGV();
                    txtConfigId.Text = Code;
                    txtCode.Text = "SU_" + GetTimeStamp() + new Random().Next(10000, 99999);
                    txtStartIndex.Text = ""; txtStopIndex.Text = ""; txtStep.Text = ""; txtBaseUrl.Text = ""; txtShortUrl.Text = ""; txtName.Text = ""; txtAlias.Text = ""; txtSheet.Text = ""; txtClassfic.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败,错误代码为：" + ex.ToString());
                }
            }
        }

        private void dgvShowUrl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ShowDGVUrl_Attr()
        {
            try
            {
                dgvShowUrl_Attr.Rows.Clear();
                Sp_url_attr[] spUrl = new Sp_url_attrDAL().GetAllData(dgvShowUrl.SelectedRows[0].Cells["记录的唯一索引"].Value.ToString());
                for (int i = 0; i < spUrl.Length; i++)
                {
                    int d = dgvShowUrl_Attr.Rows.Add();
                    dgvShowUrl_Attr.Rows[d].Cells["编号Url_Attr"].Value = d;
                    dgvShowUrl_Attr.Rows[d].Cells["归属URL的代码"].Value = spUrl[i].UrlCode;
                    dgvShowUrl_Attr.Rows[d].Cells["HTML标签"].Value = spUrl[i].HtmlTag;
                    dgvShowUrl_Attr.Rows[d].Cells["属性名"].Value = spUrl[i].AttrName;
                    dgvShowUrl_Attr.Rows[d].Cells["属性别名"].Value = spUrl[i].Alias;
                    if (spUrl[i].CalcWay == "00")
                    {
                        dgvShowUrl_Attr.Rows[d].Cells["结果计算方式"].Value = "替换";
                    }
                    if (spUrl[i].CalcWay == "10")
                    {
                        dgvShowUrl_Attr.Rows[d].Cells["结果计算方式"].Value = "前缀";
                    }
                    if (spUrl[i].CalcWay == "11")
                    {
                        dgvShowUrl_Attr.Rows[d].Cells["结果计算方式"].Value = "后缀";
                    }
                    if (spUrl[i].CalcWay == "99")
                    {
                        dgvShowUrl_Attr.Rows[d].Cells["结果计算方式"].Value = "原字符";
                    }
                    dgvShowUrl_Attr.Rows[d].Cells["扩展信息"].Value = spUrl[i].ExternStr;
                    dgvShowUrl_Attr.Rows[d].Cells["子属性"].Value = spUrl[i].SubAttr;
                    dgvShowUrl_Attr.Rows[d].Cells["是否关联其他属性"].Value = spUrl[i].AttachAttr;
                    dgvShowUrl_Attr.Rows[d].Cells["属性代码"].Value = spUrl[i].Code;
                    if (spUrl[i].IsUrl == 1)
                    {
                        dgvShowUrl_Attr.Rows[d].Cells["属性对应值是否是链接"].Value = "是";
                    }
                    else
                    {
                        dgvShowUrl_Attr.Rows[d].Cells["属性对应值是否是链接"].Value = "否";
                    }
                    dgvShowUrl_Attr.Rows[d].Cells["IdUrl_Attr"].Value = spUrl[i].Id;
                }
            }
            catch
            {

            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvShowUrl.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("确定要删除此条数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    new Sp_urlDAL().DeleteByCode(dgvShowUrl.SelectedRows[0].Cells["记录的唯一索引"].Value.ToString());
                    new Sp_url_attrDAL().DeleteByUrlCode(dgvShowUrl.SelectedRows[0].Cells["记录的唯一索引"].Value.ToString());
                    //MessageBox.Show("您选中的数据已删除！", "提示",
                    //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowDGV();
                    ShowDGVUrl_Attr();
                    txtStartIndex.Text = ""; txtStopIndex.Text = ""; txtStep.Text = ""; txtBaseUrl.Text = ""; txtShortUrl.Text = ""; txtName.Text = ""; txtAlias.Text = ""; txtSheet.Text = ""; txtClassfic.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败!错误代码为：" + ex.ToString());
                }
            }
        }

        private void 删除_Click(object sender, EventArgs e)
        {
            if (dgvShowUrl_Attr.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("确定要删除此条数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    new Sp_url_attrDAL().DeleteByCode(dgvShowUrl_Attr.SelectedRows[0].Cells["属性代码"].Value.ToString());
                    //MessageBox.Show("您选中的数据已删除！", "提示",
                    //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowDGVUrl_Attr();
                    txtHtmlTag.Text = ""; txtAttrName.Text = ""; txtAttrAlias.Text = ""; txtExternStr.Text = ""; txtSubAttr.Text = ""; txtAttrCode.Text = "";
                    txtAttachAttr.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败!错误代码为：" + ex.ToString());
                }
            }
        }

        private void btnAddUrl_Attr_Click(object sender, EventArgs e)
        {
            if (txtHtmlTag.Text.Trim() == "" || txtAttrName.Text.Trim() == "" || txtAttrAlias.Text.Trim() == "" || txtAttrCode.Text.Trim() == "")
            {
                MessageBox.Show("HTML标签等信息为必填项！");
                return;
            }
            Sp_url_attr spUrl = new Sp_url_attr();
            //Sp_url_attr[] spUrlAll = new Sp_url_attrDAL().GetAllData();
            //spUrl.Id = spUrlAll.Length + 1;
            spUrl.HtmlTag = txtHtmlTag.Text.Trim();
            spUrl.Alias = txtAttrAlias.Text.Trim();
            spUrl.ExternStr = txtExternStr.Text.Trim();
            spUrl.AttrName = txtAttrName.Text.Trim();
            spUrl.SubAttr = txtSubAttr.Text.Trim();
            spUrl.Code = txtAttrCode.Text.Trim();
            spUrl.UrlCode = txtUrlCode.Text.Trim();
            spUrl.AttachAttr = txtAttachAttr.Text.Trim();
            if (cBoxCalcWay.Text == "替换")
            {
                spUrl.CalcWay = "00";
            }
            if (cBoxCalcWay.Text == "前缀")
            {
                spUrl.CalcWay = "10";
            }
            if (cBoxCalcWay.Text == "后缀")
            {
                spUrl.CalcWay = "11";
            }
            if (cBoxCalcWay.Text == "原字符")
            {
                spUrl.CalcWay = "99";
            }
            if (cBoxIsUrl.Text == "是")
            {
                spUrl.IsUrl = 1;
            }
            else
            {
                spUrl.IsUrl = 0;
            }
            if (btnAddUrl_Attr.Text == "新  增")
            {
                try
                {
                    new Sp_url_attrDAL().Insert(spUrl);
                    MessageBox.Show("添加成功");
                    ShowDGVUrl_Attr();
                    txtAttrCode.Text = "SUA_" + GetTimeStamp() + new Random().Next(10000, 99999);
                    txtHtmlTag.Text = ""; txtAttrName.Text = ""; txtAttrAlias.Text = ""; txtExternStr.Text = ""; txtSubAttr.Text = "";
                    txtAttachAttr.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加失败,错误代码为：" + ex.ToString());
                }
            }
            else
            {
                try
                {
                    spUrl.Id = Convert.ToInt32(dgvShowUrl_Attr.SelectedRows[0].Cells["IdUrl_Attr"].Value);
                    new Sp_url_attrDAL().Update(spUrl);
                    MessageBox.Show("修改成功");
                    btnAddUrl_Attr.Text = "新  增";
                    ShowDGVUrl_Attr();
                    txtAttrCode.Text = "SUA_" + GetTimeStamp() + new Random().Next(10000, 99999);
                    txtHtmlTag.Text = ""; txtAttrName.Text = ""; txtAttrAlias.Text = ""; txtExternStr.Text = ""; txtSubAttr.Text = "";
                    txtAttachAttr.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败,错误代码为：" + ex.ToString());
                }
            }
        }

        private void dgvShowUrl_Attr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvShowUrl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStartIndex.Text = dgvShowUrl.SelectedRows[0].Cells["索引开始"].Value.ToString();
            txtStopIndex.Text = dgvShowUrl.SelectedRows[0].Cells["索引结束"].Value.ToString();
            txtStep.Text = dgvShowUrl.SelectedRows[0].Cells["步长"].Value.ToString();
            txtBaseUrl.Text = dgvShowUrl.SelectedRows[0].Cells["基础URL"].Value.ToString();
            txtShortUrl.Text = dgvShowUrl.SelectedRows[0].Cells["动态变化URL"].Value.ToString();
            txtName.Text = dgvShowUrl.SelectedRows[0].Cells["网站名"].Value.ToString();
            txtAlias.Text = dgvShowUrl.SelectedRows[0].Cells["网站别名"].Value.ToString();
            txtSheet.Text = dgvShowUrl.SelectedRows[0].Cells["Sheet名称"].Value.ToString();
            txtConfigId.Text = dgvShowUrl.SelectedRows[0].Cells["归属配置Code"].Value.ToString();
            txtCode.Text = dgvShowUrl.SelectedRows[0].Cells["记录的唯一索引"].Value.ToString();
            txtClassfic.Text = dgvShowUrl.SelectedRows[0].Cells["分类"].Value.ToString();
            cBoxEnable.Text = dgvShowUrl.SelectedRows[0].Cells["是否生效"].Value.ToString();
            cBoxLoopType.Text = dgvShowUrl.SelectedRows[0].Cells["循环模式"].Value.ToString();
            btnAddUrl.Text = "修  改";
            ShowDGVUrl_Attr();
            txtUrlCode.Text = txtCode.Text;
            txtAttrCode.Text = "SUA_" + GetTimeStamp() + new Random().Next(10000, 99999);
        }

        private void dgvShowUrl_Attr_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHtmlTag.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["HTML标签"].Value.ToString();
            txtAttrName.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["属性名"].Value.ToString();
            txtAttrAlias.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["属性别名"].Value.ToString();
            txtExternStr.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["扩展信息"].Value.ToString();
            txtSubAttr.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["子属性"].Value.ToString();
            txtAttachAttr.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["是否关联其他属性"].Value.ToString();
            txtAttrCode.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["属性代码"].Value.ToString();
            txtUrlCode.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["归属URL的代码"].Value.ToString();
            cBoxCalcWay.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["结果计算方式"].Value.ToString();
            cBoxIsUrl.Text = dgvShowUrl_Attr.SelectedRows[0].Cells["属性对应值是否是链接"].Value.ToString();
            btnAddUrl_Attr.Text = "修  改";
        }

        private void btnCopyUrl_Click(object sender, EventArgs e)
        {
            if (dgvShowUrl.SelectedRows.Count <= 0)
            {
                MessageBox.Show("未选中目标URL,请先添加或选中一个");
                return;
            }
            CopyUrlWnd wnd = new CopyUrlWnd();
            wnd.UrlCode = dgvShowUrl.SelectedRows[0].Cells["记录的唯一索引"].Value.ToString();

            if (wnd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<string, List<string>> result = wnd.Result;
                foreach (var key in result.Keys)
                {
                    // 有且只有一个
                    string urlCode = key.ToString();
                    List<string> attrCopys = result[key];

                    Sp_url_attr[] spUrlAttrs = new Sp_url_attrDAL().GetAllData(urlCode);
                    List<Sp_url_attr> attrs = spUrlAttrs.ToList();

                    for (int index = 0 ; index < attrs.Count ; index ++)
                    {
                        Sp_url_attr attr = attrs[index];

                        if (!attrCopys.Contains(attr.Code))
                        {
                            continue;
                        }
                        Console.WriteLine(attr.ToString());

                        cloneAndAdd(attr, wnd.UrlCode);
                    }

                    ShowDGVUrl_Attr();
                    MessageBox.Show("拷贝成功");
                }
            }
        }

        private void dgvShowUrl_SelectionChanged(object sender, EventArgs e)
        {
            ShowDGVUrl_Attr();
        }

        private void cloneAndAdd(Sp_url_attr srcAttr,string urlCode)
        {
            srcAttr.Code = "SUA_" + GetTimeStamp() + new Random().Next(10000, 99999);
            srcAttr.UrlCode = urlCode;

            try
            {
                new Sp_url_attrDAL().Insert(srcAttr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败,错误代码为：" + ex.ToString());
            }
           
        }
    }
}
