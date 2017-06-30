using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 数据库管理.DAL;
using 数据库管理.Model;

namespace 数据库管理
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadConfigs();
            loadUsers();
            ShowServices();
        }

        #region Config
        private void loadConfigs()
        {
            ShowDGV();
            txtCode.Text = "SC_" + GetTimeStamp() + new Random().Next(10000, 99999);
            cBoxTimeType.Text = "定时";
            cBoxEnable.Text = "是";
        }

        private void cBoxTimeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxTimeType.Text == "定时")
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cBoxTimeType.Text == "定时")
            {
                if (txtTimeOne.Text.Trim() == "" || txtTimeTwo.Text.Trim() == "")
                {
                    MessageBox.Show("时间段为必填项！");
                    return;
                }
            }
            else
            {
                if (!cBoxOne.Checked && !cBoxTwo.Checked && !cBoxThree.Checked && !cBoxFour.Checked && !cBoxFive.Checked && !cBoxSix.Checked && !cBoxSeven.Checked)
                {
                    MessageBox.Show("请至少选择一天！");
                    return;
                }
                if (txtTimeThree.Text.Trim() == "" || txtTimeFour.Text.Trim() == "")
                {
                    MessageBox.Show("时间段为必填项！");
                    return;
                }
            }
            if (txtEMail.Text.Trim() == "" || txtMobile.Text.Trim() == "" || txtName.Text.Trim() == "" || txtJobClassName.Text.Trim() == "")
            {
                MessageBox.Show("邮箱电话等信息为必填项！");
                return;
            }
            Sp_config sp = new Sp_config();
            //Sp_config[] spAll = new Sp_configDAL().GetAllData();
            //sp.Id = spAll.Length + 1;
            if (cBoxTimeType.Text == "定时")
            {
                sp.TimeType = "0000000";
                sp.TimeSep = txtTimeOne.Text.Trim() + "~" + txtTimeTwo.Text.Trim();
            }
            else
            {
                string[] txt = new string[7] { "0", "0", "0", "0", "0", "0", "0" };
                #region MyRegion
                if (cBoxOne.Checked)
                {
                    txt[0] = "1";
                }
                if (cBoxTwo.Checked)
                {
                    txt[1] = "1";
                }
                if (cBoxThree.Checked)
                {
                    txt[2] = "1";
                }
                if (cBoxFour.Checked)
                {
                    txt[3] = "1";
                }
                if (cBoxFive.Checked)
                {
                    txt[4] = "1";
                }
                if (cBoxSix.Checked)
                {
                    txt[5] = "1";
                }
                if (cBoxSeven.Checked)
                {
                    txt[6] = "1";
                }
                #endregion
                sp.TimeType = txt[0] + txt[1] + txt[2] + txt[3] + txt[4] + txt[5] + txt[6];
                sp.TimeSep = txtTimeThree.Text.Trim() + "~" + txtTimeFour.Text.Trim();
            }
            sp.EMail = txtEMail.Text.Trim();
            sp.Mobile = txtMobile.Text.Trim();
            sp.Code = txtCode.Text.Trim();
            sp.Name = txtName.Text.Trim();
            if (cBoxEnable.Text == "是")
            {
                sp.Enable = 1;
            }
            else
            {
                sp.Enable = 0;
            }
            sp.JobClassName = txtJobClassName.Text.Trim();
            try
            {
                if (btnAdd.Text == "新  增")
                {
                    new Sp_configDAL().Insert(sp);
                    MessageBox.Show("添加成功");
                    txtCode.Text = "SC_" + GetTimeStamp() + new Random().Next(10000, 99999);
                }
                else
                {
                    sp.Id = Convert.ToInt32(dgvShow.SelectedRows[0].Cells["Id"].Value);
                    new Sp_configDAL().Update(sp);
                    MessageBox.Show("修改成功");
                    txtCode.Text = "SC_" + GetTimeStamp() + new Random().Next(10000, 99999);
                    btnAdd.Text = "新  增";
                }
                txtEMail.Text= ""; txtMobile.Text= ""; txtName.Text= ""; txtJobClassName.Text= "";
                txtCode.Text = "SC_" + GetTimeStamp() + new Random().Next(10000, 99999);
                ShowDGV();
            }
            catch (Exception ex)
            {
                if (btnAdd.Text == "新  增")
                {
                    MessageBox.Show("添加失败!错误代码为：" + ex.ToString());
                }
                else
                {
                    MessageBox.Show("修改失败!错误代码为：" + ex.ToString());
                }
            }
        }

        private void ShowDGV()
        {
            dgvShow.Rows.Clear();
            Sp_config[] sp = new Sp_configDAL().GetAllData();
            for (int i = 0; i < sp.Length; i++)
            {
                int d = dgvShow.Rows.Add();
                dgvShow.Rows[d].Cells["编号"].Value = d;
                dgvShow.Rows[d].Cells["时间方案"].Value = sp[i].TimeType;
                dgvShow.Rows[d].Cells["时间段"].Value = sp[i].TimeSep;
                dgvShow.Rows[d].Cells["邮箱"].Value = sp[i].EMail;
                dgvShow.Rows[d].Cells["手机号"].Value = sp[i].Mobile;
                dgvShow.Rows[d].Cells["唯一编码"].Value = sp[i].Code;
                dgvShow.Rows[d].Cells["配置名称"].Value = sp[i].Name;
                if (sp[i].Enable == 1)
                {
                    dgvShow.Rows[d].Cells["是否生效"].Value = "是";
                }
                else
                {
                    dgvShow.Rows[d].Cells["是否生效"].Value = "否";
                }
                dgvShow.Rows[d].Cells["工作类名字"].Value = sp[i].JobClassName;
                dgvShow.Rows[d].Cells["Id"].Value = sp[i].Id;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvShow.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("确定要删除此条数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    new Sp_configDAL().Delete(dgvShow.SelectedRows[0].Cells["唯一编码"].Value.ToString());
                    Sp_url[] spp = new Sp_urlDAL().GetAllData(dgvShow.SelectedRows[0].Cells["唯一编码"].Value.ToString()) ;
                    for (int i = 0; i < spp.Length; i++)
                    {
                        new Sp_url_attrDAL().DeleteByUrlCode(spp[i].Code);
                    }
                    new Sp_urlDAL().DeleteByCode(dgvShow.SelectedRows[0].Cells["唯一编码"].Value.ToString());
                    //MessageBox.Show("您选中的数据已删除！", "提示",
                                        //MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowDGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败!错误代码为：" + ex.ToString());
                }
            }
        }

        private void cloneRecord()
        {
            txtEMail.Text = dgvShow.SelectedRows[0].Cells["邮箱"].Value.ToString();
            txtMobile.Text = dgvShow.SelectedRows[0].Cells["手机号"].Value.ToString();
            txtName.Text = dgvShow.SelectedRows[0].Cells["配置名称"].Value.ToString();
            //txtCode.Text = dgvShow.SelectedRows[0].Cells["唯一编码"].Value.ToString();
            txtCode.Text = "SC_" + GetTimeStamp() + new Random().Next(10000, 99999);
            txtJobClassName.Text = dgvShow.SelectedRows[0].Cells["工作类名字"].Value.ToString();
            if (dgvShow.SelectedRows[0].Cells["时间方案"].Value.ToString() == "0000000")
            {
                cBoxTimeType.Text = "定时";
                txtTimeOne.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[0];
                txtTimeTwo.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[1];
            }
            else
            {
                cBoxTimeType.Text = "循环";
                txtTimeThree.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[0];
                txtTimeFour.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[1];
                string txt = dgvShow.SelectedRows[0].Cells["时间方案"].Value.ToString();
                if (txt[0].ToString() == "1")
                {
                    cBoxOne.Checked = true;
                }
                if (txt[1].ToString() == "1")
                {
                    cBoxTwo.Checked = true;
                }
                if (txt[2].ToString() == "1")
                {
                    cBoxThree.Checked = true;
                }
                if (txt[3].ToString() == "1")
                {
                    cBoxFour.Checked = true;
                }
                if (txt[4].ToString() == "1")
                {
                    cBoxFive.Checked = true;
                }
                if (txt[5].ToString() == "1")
                {
                    cBoxSix.Checked = true;
                }
                if (txt[6].ToString() == "1")
                {
                    cBoxSeven.Checked = true;
                }
                cBoxEnable.Text = dgvShow.SelectedRows[0].Cells["是否生效"].Value.ToString();

                btnAdd.Text = "新  增";
            }
        }
        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvShow.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmInfo f = new frmInfo();
            f.Code = dgvShow.SelectedRows[0].Cells["唯一编码"].Value.ToString();
            f.ShowDialog();
        }

        private void 克隆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvShow.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cloneRecord();
        }

        private void dgvShow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEMail.Text = dgvShow.SelectedRows[0].Cells["邮箱"].Value.ToString();
            txtMobile.Text = dgvShow.SelectedRows[0].Cells["手机号"].Value.ToString();
            txtName.Text = dgvShow.SelectedRows[0].Cells["配置名称"].Value.ToString();
            txtCode.Text = dgvShow.SelectedRows[0].Cells["唯一编码"].Value.ToString();
            txtJobClassName.Text = dgvShow.SelectedRows[0].Cells["工作类名字"].Value.ToString();
            if (dgvShow.SelectedRows[0].Cells["时间方案"].Value.ToString() == "0000000")
            {
                cBoxTimeType.Text = "定时";
                txtTimeOne.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[0];
                txtTimeTwo.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[1];
            }
            else
            {
                cBoxTimeType.Text = "循环";
                txtTimeThree.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[0];
                txtTimeFour.Text = dgvShow.SelectedRows[0].Cells["时间段"].Value.ToString().Split('~')[1];
                string txt = dgvShow.SelectedRows[0].Cells["时间方案"].Value.ToString();
                if (txt[0].ToString() == "1")
                {
                    cBoxOne.Checked = true;
                }
                if (txt[1].ToString() == "1")
                {
                    cBoxTwo.Checked = true;
                }
                if (txt[2].ToString() == "1")
                {
                    cBoxThree.Checked = true;
                }
                if (txt[3].ToString() == "1")
                {
                    cBoxFour.Checked = true;
                }
                if (txt[4].ToString() == "1")
                {
                    cBoxFive.Checked = true;
                }
                if (txt[5].ToString() == "1")
                {
                    cBoxSix.Checked = true;
                }
                if (txt[6].ToString() == "1")
                {
                    cBoxSeven.Checked = true;
                }
                cBoxEnable.Text = dgvShow.SelectedRows[0].Cells["是否生效"].Value.ToString();
                
            }
            btnAdd.Text = "修  改";
        }
        #endregion

        #region User
        private void loadUsers()
        {
            ShowUsers();
            //txtCode.Text = "SC_" + GetTimeStamp() + new Random().Next(10000, 99999);
            //cBoxTimeType.Text = "定时";
            //cBoxEnable.Text = "是";
        }

        private void ShowUsers()
        {
            dtUserTable.Rows.Clear();
            Sps_user[] sp = new Sps_userDAL().GetAllData();
            for (int i = 0; i < sp.Length; i++)
            {
                int d = dtUserTable.Rows.Add();
                dtUserTable.Rows[d].Cells["spsUserNum"].Value = d;
                dtUserTable.Rows[d].Cells["spsUserAccount"].Value = sp[i].Account;
                dtUserTable.Rows[d].Cells["spsUserEMail"].Value = sp[i].EMail;
                dtUserTable.Rows[d].Cells["spsUserAlias"].Value = sp[i].Alias;
                dtUserTable.Rows[d].Cells["spsUserAddress"].Value = sp[i].Address;
                dtUserTable.Rows[d].Cells["spsUserLant"].Value = sp[i].Lantudite;
                dtUserTable.Rows[d].Cells["spsUserLong"].Value = sp[i].Longdite;
                dtUserTable.Rows[d].Cells["spsUserOrgName"].Value = sp[i].OrgName;

                dtUserTable.Rows[d].Cells["spsUserId"].Value = sp[i].Id;
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (tbAccount.Text.Trim() == "" || tbAlias.Text.Trim() == "")
            {
                MessageBox.Show("账户和账户名不能为空！");
                return;
            }
            Sps_user spUrl  = new Sps_user();
            spUrl.Account = tbAccount.Text.Trim();
            spUrl.EMail = tbEMail.Text.Trim();
            spUrl.Alias = tbAlias.Text.Trim();
            spUrl.Address = tbAddress.Text.Trim();
            spUrl.Lantudite = float.Parse(tbLant.Text.Trim());
            spUrl.Longdite = float.Parse(tbLong.Text.Trim());
            spUrl.OrgName = tbOrgName.Text.Trim();

            if (btnAddUser.Text == "新 增")
            {
                try
                {
                    new Sps_userDAL().Insert(spUrl);
                    MessageBox.Show("添加成功");
                    ShowUsers();
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
                    spUrl.Id = Convert.ToInt32(dtUserTable.SelectedRows[0].Cells["spsUserId"].Value);
                    new Sps_userDAL().Update(spUrl);
                    MessageBox.Show("修改成功");
                    btnAddUser.Text = "新 增";
                    ShowUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败,错误代码为：" + ex.ToString());
                }
            }
        }

        private void dtUserTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbAccount.Text = dtUserTable.SelectedRows[0].Cells["spsUserAccount"].Value.ToString();
            tbEMail.Text = dtUserTable.SelectedRows[0].Cells["spsUserEMail"].Value.ToString();
            tbAlias.Text = dtUserTable.SelectedRows[0].Cells["spsUserAlias"].Value.ToString();
            tbAddress.Text = dtUserTable.SelectedRows[0].Cells["spsUserAddress"].Value.ToString();
            tbLant.Text = dtUserTable.SelectedRows[0].Cells["spsUserLant"].Value.ToString();
            tbLong.Text = dtUserTable.SelectedRows[0].Cells["spsUserLong"].Value.ToString();
            tbOrgName.Text = dtUserTable.SelectedRows[0].Cells["spsUserOrgName"].Value.ToString();


            btnAddUser.Text = "修 改";
        }

        private void tsmBindService_Click(object sender, EventArgs e)
        {

        }

        private void tsmSetOrder_Click(object sender, EventArgs e)
        {

        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (dtUserTable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("确定要删除此条数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    new Sps_userDAL().DeleteByCode(dtUserTable.SelectedRows[0].Cells["spsUserAccount"].Value.ToString());
                    //MessageBox.Show("您选中的数据已删除！", "提示",
                    //MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败!错误代码为：" + ex.ToString());
                }
            }
        }
        #endregion
           
        #region Service

        private void pbServiceImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;//禁止同时选择多个文件

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //pbServiceImage.BackgroundImage.Dispose();
                    pbServiceImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("文件类型有问题");
                }
                //pictureBox1.Image=Image.FromFile(picArr[0]);
            }
        }

        private void tsmDeleteMenu_Click(object sender, EventArgs e)
        {
            if (dataTabService.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还没有选中任何数据！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("确定要删除此条数据吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    new Sps_serviceDAL().DeleteByCode(dataTabService.SelectedRows[0].Cells["serviceId"].Value.ToString());
                    //MessageBox.Show("您选中的数据已删除！", "提示",
                    //MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowServices();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败!错误代码为：" + ex.ToString());
                }
            }
        }

        private void dataTabService_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           tbServiceName.Text = dataTabService.SelectedRows[0].Cells["srvName"].Value.ToString();
            tbServiceInfo.Text = dataTabService.SelectedRows[0].Cells["srvInfo"].Value.ToString();

            byte[] image = (byte[])dataTabService.SelectedRows[0].Tag;
            
            MemoryStream ms = new MemoryStream(); //新建内存流
            ms.Write(image, 0, image.Length); //附值
            pbServiceImage.BackgroundImage = Image.FromStream(ms); //读取流中内容

            btnAddService.Text = "修 改";
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            if (tbServiceName.Text.Trim() == "")
            {
                MessageBox.Show("业务名称不能为空！");
                return;
            }
            Sps_service spUrl = new Sps_service();
            spUrl.Name = tbServiceName.Text.Trim();
            spUrl.Info = tbServiceInfo.Text.Trim();


            spUrl.Image = ImageHelper.ImageToBytes(pbServiceImage.BackgroundImage);
            //spUrl.Id = Convert.ToInt32(dtUserTable.SelectedRows[0].Cells["spsUserId"].Value);
            if (btnAddService.Text == "新 增")
            {
                spUrl.Code = "SS_" + GetTimeStamp() + new Random().Next(10000, 99999);
                try
                {
                    new Sps_serviceDAL().Insert(spUrl);
                    MessageBox.Show("添加成功");
                    ShowServices();
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
                    spUrl.Id = Convert.ToInt32(dataTabService.SelectedRows[0].Cells["serviceId"].Value);
                    new Sps_serviceDAL().Update(spUrl);
                    MessageBox.Show("修改成功");
                    btnAddService.Text = "新 增";
                    ShowServices();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败,错误代码为：" + ex.ToString());
                }
            }
        }

        private void ShowServices()
        {
            dataTabService.Rows.Clear();
            Sps_service[] sp = new Sps_serviceDAL().GetAllData();
            for (int i = 0; i < sp.Length; i++)
            {
                int d = dataTabService.Rows.Add();
                dataTabService.Rows[d].Cells["serviceNum"].Value = d;
                dataTabService.Rows[d].Cells["serviceId"].Value = sp[i].Id;
                dataTabService.Rows[d].Cells["srvName"].Value = sp[i].Name;
                dataTabService.Rows[d].Cells["srvInfo"].Value = sp[i].Info;
                dataTabService.Rows[d].Cells["srvImage"].Value = "查看";

                dataTabService.Rows[d].Tag = sp[i].Image;
                dataTabService.Rows[d].Cells["serviceId"].Value = sp[i].Id;
            }
        }
        private void dataTabService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;//这行语句也可以不要，如果已经创建了dgv，详见航道系统的代码。
            //如果是"Button"列，按钮被点击
            if (dgv.Columns[e.ColumnIndex].Name == "srvImage")//此处索引列可以使name、也可以使headertext，看具体的设置。
            {
                MessageBox.Show(e.RowIndex.ToString() +
                    "行的按钮被点击了。");
            }
        }
        #endregion


    }
}
