namespace 数据库管理
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxTimeType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTimeTwo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeOne = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cBoxSeven = new System.Windows.Forms.CheckBox();
            this.cBoxSix = new System.Windows.Forms.CheckBox();
            this.cBoxFive = new System.Windows.Forms.CheckBox();
            this.cBoxFour = new System.Windows.Forms.CheckBox();
            this.cBoxThree = new System.Windows.Forms.CheckBox();
            this.cBoxTwo = new System.Windows.Forms.CheckBox();
            this.cBoxOne = new System.Windows.Forms.CheckBox();
            this.txtTimeFour = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimeThree = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtJobClassName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cBoxEnable = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.时间方案 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.时间段 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.邮箱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.手机号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.唯一编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.配置名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否生效 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工作类名字 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMstrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.cMstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(37, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "时间方案：";
            // 
            // cBoxTimeType
            // 
            this.cBoxTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxTimeType.FormattingEnabled = true;
            this.cBoxTimeType.Items.AddRange(new object[] {
            "定时",
            "循环"});
            this.cBoxTimeType.Location = new System.Drawing.Point(131, 26);
            this.cBoxTimeType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxTimeType.Name = "cBoxTimeType";
            this.cBoxTimeType.Size = new System.Drawing.Size(92, 20);
            this.cBoxTimeType.TabIndex = 1;
            this.cBoxTimeType.SelectedIndexChanged += new System.EventHandler(this.cBoxTimeType_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTimeTwo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtTimeOne);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(236, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 47);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // txtTimeTwo
            // 
            this.txtTimeTwo.Location = new System.Drawing.Point(225, 16);
            this.txtTimeTwo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTimeTwo.Name = "txtTimeTwo";
            this.txtTimeTwo.Size = new System.Drawing.Size(122, 21);
            this.txtTimeTwo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(206, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "~";
            // 
            // txtTimeOne
            // 
            this.txtTimeOne.Location = new System.Drawing.Point(81, 16);
            this.txtTimeOne.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTimeOne.Name = "txtTimeOne";
            this.txtTimeOne.Size = new System.Drawing.Size(122, 21);
            this.txtTimeOne.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "时间段：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cBoxSeven);
            this.panel2.Controls.Add(this.cBoxSix);
            this.panel2.Controls.Add(this.cBoxFive);
            this.panel2.Controls.Add(this.cBoxFour);
            this.panel2.Controls.Add(this.cBoxThree);
            this.panel2.Controls.Add(this.cBoxTwo);
            this.panel2.Controls.Add(this.cBoxOne);
            this.panel2.Controls.Add(this.txtTimeFour);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTimeThree);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(236, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 78);
            this.panel2.TabIndex = 3;
            this.panel2.Visible = false;
            // 
            // cBoxSeven
            // 
            this.cBoxSeven.AutoSize = true;
            this.cBoxSeven.Location = new System.Drawing.Point(76, 30);
            this.cBoxSeven.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxSeven.Name = "cBoxSeven";
            this.cBoxSeven.Size = new System.Drawing.Size(60, 16);
            this.cBoxSeven.TabIndex = 11;
            this.cBoxSeven.Text = "星期日";
            this.cBoxSeven.UseVisualStyleBackColor = true;
            // 
            // cBoxSix
            // 
            this.cBoxSix.AutoSize = true;
            this.cBoxSix.Location = new System.Drawing.Point(10, 30);
            this.cBoxSix.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxSix.Name = "cBoxSix";
            this.cBoxSix.Size = new System.Drawing.Size(60, 16);
            this.cBoxSix.TabIndex = 10;
            this.cBoxSix.Text = "星期六";
            this.cBoxSix.UseVisualStyleBackColor = true;
            // 
            // cBoxFive
            // 
            this.cBoxFive.AutoSize = true;
            this.cBoxFive.Location = new System.Drawing.Point(273, 10);
            this.cBoxFive.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxFive.Name = "cBoxFive";
            this.cBoxFive.Size = new System.Drawing.Size(60, 16);
            this.cBoxFive.TabIndex = 9;
            this.cBoxFive.Text = "星期五";
            this.cBoxFive.UseVisualStyleBackColor = true;
            // 
            // cBoxFour
            // 
            this.cBoxFour.AutoSize = true;
            this.cBoxFour.Location = new System.Drawing.Point(204, 10);
            this.cBoxFour.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxFour.Name = "cBoxFour";
            this.cBoxFour.Size = new System.Drawing.Size(60, 16);
            this.cBoxFour.TabIndex = 8;
            this.cBoxFour.Text = "星期四";
            this.cBoxFour.UseVisualStyleBackColor = true;
            // 
            // cBoxThree
            // 
            this.cBoxThree.AutoSize = true;
            this.cBoxThree.Location = new System.Drawing.Point(141, 10);
            this.cBoxThree.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxThree.Name = "cBoxThree";
            this.cBoxThree.Size = new System.Drawing.Size(60, 16);
            this.cBoxThree.TabIndex = 7;
            this.cBoxThree.Text = "星期三";
            this.cBoxThree.UseVisualStyleBackColor = true;
            // 
            // cBoxTwo
            // 
            this.cBoxTwo.AutoSize = true;
            this.cBoxTwo.Location = new System.Drawing.Point(76, 10);
            this.cBoxTwo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxTwo.Name = "cBoxTwo";
            this.cBoxTwo.Size = new System.Drawing.Size(60, 16);
            this.cBoxTwo.TabIndex = 6;
            this.cBoxTwo.Text = "星期二";
            this.cBoxTwo.UseVisualStyleBackColor = true;
            // 
            // cBoxOne
            // 
            this.cBoxOne.AutoSize = true;
            this.cBoxOne.Location = new System.Drawing.Point(10, 10);
            this.cBoxOne.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxOne.Name = "cBoxOne";
            this.cBoxOne.Size = new System.Drawing.Size(60, 16);
            this.cBoxOne.TabIndex = 5;
            this.cBoxOne.Text = "星期一";
            this.cBoxOne.UseVisualStyleBackColor = true;
            // 
            // txtTimeFour
            // 
            this.txtTimeFour.Location = new System.Drawing.Point(217, 50);
            this.txtTimeFour.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTimeFour.Name = "txtTimeFour";
            this.txtTimeFour.Size = new System.Drawing.Size(122, 21);
            this.txtTimeFour.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(198, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "~";
            // 
            // txtTimeThree
            // 
            this.txtTimeThree.Location = new System.Drawing.Point(73, 50);
            this.txtTimeThree.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTimeThree.Name = "txtTimeThree";
            this.txtTimeThree.Size = new System.Drawing.Size(122, 21);
            this.txtTimeThree.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(4, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "时间段：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(65, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "邮箱：";
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(131, 86);
            this.txtEMail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(122, 21);
            this.txtEMail.TabIndex = 12;
            this.txtEMail.Text = "test@qq.com";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(453, 86);
            this.txtMobile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(122, 21);
            this.txtMobile.TabIndex = 14;
            this.txtMobile.Text = "15680585185";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(387, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "手机：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(131, 118);
            this.txtCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(122, 21);
            this.txtCode.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(37, 121);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "唯一编码：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(453, 118);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(122, 21);
            this.txtName.TabIndex = 18;
            this.txtName.Text = "配置1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(358, 121);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "配置名称：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(37, 154);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 19;
            this.label10.Text = "是否生效：";
            // 
            // txtJobClassName
            // 
            this.txtJobClassName.Location = new System.Drawing.Point(453, 151);
            this.txtJobClassName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtJobClassName.Name = "txtJobClassName";
            this.txtJobClassName.Size = new System.Drawing.Size(122, 21);
            this.txtJobClassName.TabIndex = 22;
            this.txtJobClassName.Text = "UrlJobExhibition";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(346, 154);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 15);
            this.label11.TabIndex = 21;
            this.label11.Text = "工作类名字：";
            // 
            // cBoxEnable
            // 
            this.cBoxEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxEnable.FormattingEnabled = true;
            this.cBoxEnable.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cBoxEnable.Location = new System.Drawing.Point(131, 153);
            this.cBoxEnable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxEnable.Name = "cBoxEnable";
            this.cBoxEnable.Size = new System.Drawing.Size(92, 20);
            this.cBoxEnable.TabIndex = 23;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(453, 193);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(121, 32);
            this.btnAdd.TabIndex = 24;
            this.btnAdd.Text = "新  增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvShow);
            this.groupBox1.Location = new System.Drawing.Point(9, 226);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(596, 197);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已有配置信息展示";
            // 
            // dgvShow
            // 
            this.dgvShow.AllowUserToAddRows = false;
            this.dgvShow.AllowUserToResizeRows = false;
            this.dgvShow.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编号,
            this.Id,
            this.时间方案,
            this.时间段,
            this.邮箱,
            this.手机号,
            this.唯一编码,
            this.配置名称,
            this.是否生效,
            this.工作类名字});
            this.dgvShow.ContextMenuStrip = this.cMstrip;
            this.dgvShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShow.Location = new System.Drawing.Point(2, 16);
            this.dgvShow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvShow.MultiSelect = false;
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.ReadOnly = true;
            this.dgvShow.RowHeadersVisible = false;
            this.dgvShow.RowTemplate.Height = 27;
            this.dgvShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShow.Size = new System.Drawing.Size(592, 179);
            this.dgvShow.TabIndex = 0;
            this.dgvShow.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShow_CellClick);
            // 
            // 编号
            // 
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // 时间方案
            // 
            this.时间方案.HeaderText = "时间方案";
            this.时间方案.Name = "时间方案";
            this.时间方案.ReadOnly = true;
            // 
            // 时间段
            // 
            this.时间段.HeaderText = "时间段";
            this.时间段.Name = "时间段";
            this.时间段.ReadOnly = true;
            // 
            // 邮箱
            // 
            this.邮箱.HeaderText = "邮箱";
            this.邮箱.Name = "邮箱";
            this.邮箱.ReadOnly = true;
            // 
            // 手机号
            // 
            this.手机号.HeaderText = "手机号";
            this.手机号.Name = "手机号";
            this.手机号.ReadOnly = true;
            // 
            // 唯一编码
            // 
            this.唯一编码.HeaderText = "唯一编码";
            this.唯一编码.Name = "唯一编码";
            this.唯一编码.ReadOnly = true;
            // 
            // 配置名称
            // 
            this.配置名称.HeaderText = "配置名称";
            this.配置名称.Name = "配置名称";
            this.配置名称.ReadOnly = true;
            // 
            // 是否生效
            // 
            this.是否生效.HeaderText = "是否生效";
            this.是否生效.Name = "是否生效";
            this.是否生效.ReadOnly = true;
            // 
            // 工作类名字
            // 
            this.工作类名字.HeaderText = "工作类名字";
            this.工作类名字.Name = "工作类名字";
            this.工作类名字.ReadOnly = true;
            // 
            // cMstrip
            // 
            this.cMstrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cMstrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.cMstrip.Name = "cMstrip";
            this.cMstrip.Size = new System.Drawing.Size(109, 52);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.查看ToolStripMenuItem.Text = "查看";
            this.查看ToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 429);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cBoxEnable);
            this.Controls.Add(this.txtJobClassName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEMail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cBoxTimeType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库管理";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.cMstrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBoxTimeType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTimeTwo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeOne;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cBoxSeven;
        private System.Windows.Forms.CheckBox cBoxSix;
        private System.Windows.Forms.CheckBox cBoxFive;
        private System.Windows.Forms.CheckBox cBoxFour;
        private System.Windows.Forms.CheckBox cBoxThree;
        private System.Windows.Forms.CheckBox cBoxTwo;
        private System.Windows.Forms.CheckBox cBoxOne;
        private System.Windows.Forms.TextBox txtTimeFour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTimeThree;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtJobClassName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cBoxEnable;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.ContextMenuStrip cMstrip;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn 编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间方案;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间段;
        private System.Windows.Forms.DataGridViewTextBoxColumn 邮箱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 手机号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 唯一编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 配置名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否生效;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工作类名字;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
    }
}

