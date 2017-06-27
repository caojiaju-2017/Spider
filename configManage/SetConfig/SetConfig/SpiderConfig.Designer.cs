namespace SetConfig
{
    partial class SpiderConfig
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.configTree = new System.Windows.Forms.TreeView();
            this.btnNewConfig = new System.Windows.Forms.Button();
            this.mainTabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.detailSubTab = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnUpdateCfg = new System.Windows.Forms.Button();
            this.mainTabCtrl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // configTree
            // 
            this.configTree.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.configTree.Location = new System.Drawing.Point(12, 59);
            this.configTree.Name = "configTree";
            this.configTree.Size = new System.Drawing.Size(283, 515);
            this.configTree.TabIndex = 2;
            // 
            // btnNewConfig
            // 
            this.btnNewConfig.Location = new System.Drawing.Point(12, 12);
            this.btnNewConfig.Name = "btnNewConfig";
            this.btnNewConfig.Size = new System.Drawing.Size(120, 41);
            this.btnNewConfig.TabIndex = 3;
            this.btnNewConfig.Text = "添加";
            this.btnNewConfig.UseVisualStyleBackColor = true;
            this.btnNewConfig.Click += new System.EventHandler(this.btnNewConfig_Click);
            // 
            // mainTabCtrl
            // 
            this.mainTabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabCtrl.Controls.Add(this.tabPage1);
            this.mainTabCtrl.Controls.Add(this.tabPage2);
            this.mainTabCtrl.Controls.Add(this.tabPage3);
            this.mainTabCtrl.Location = new System.Drawing.Point(317, 59);
            this.mainTabCtrl.Name = "mainTabCtrl";
            this.mainTabCtrl.SelectedIndex = 0;
            this.mainTabCtrl.Size = new System.Drawing.Size(516, 515);
            this.mainTabCtrl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnUpdateCfg);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBox4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(508, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "时间";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(508, 489);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "通知";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(508, 489);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "细节";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.detailSubTab);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(13, 17);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(492, 516);
            this.tabControl2.TabIndex = 0;
            // 
            // detailSubTab
            // 
            this.detailSubTab.Location = new System.Drawing.Point(4, 22);
            this.detailSubTab.Name = "detailSubTab";
            this.detailSubTab.Padding = new System.Windows.Forms.Padding(3);
            this.detailSubTab.Size = new System.Drawing.Size(484, 490);
            this.detailSubTab.TabIndex = 0;
            this.detailSubTab.Text = "基础信息";
            this.detailSubTab.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(484, 490);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "属性";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(175, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 41);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "载入";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 34);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 41);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(73, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(128, 21);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间方案";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "时间段";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(73, 76);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(128, 21);
            this.textBox2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "手机号";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(306, 249);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(128, 21);
            this.textBox3.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "邮箱";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(306, 205);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(128, 21);
            this.textBox4.TabIndex = 4;
            // 
            // btnUpdateCfg
            // 
            this.btnUpdateCfg.Location = new System.Drawing.Point(306, 443);
            this.btnUpdateCfg.Name = "btnUpdateCfg";
            this.btnUpdateCfg.Size = new System.Drawing.Size(188, 40);
            this.btnUpdateCfg.TabIndex = 8;
            this.btnUpdateCfg.Text = "生成配置";
            this.btnUpdateCfg.UseVisualStyleBackColor = true;
            this.btnUpdateCfg.Click += new System.EventHandler(this.btnUpdateCfg_Click);
            // 
            // SpiderConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 585);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.mainTabCtrl);
            this.Controls.Add(this.btnNewConfig);
            this.Controls.Add(this.configTree);
            this.Name = "SpiderConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "爬虫配置管理";
            this.Load += new System.EventHandler(this.SpiderConfig_Load);
            this.mainTabCtrl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView configTree;
        private System.Windows.Forms.Button btnNewConfig;
        private System.Windows.Forms.TabControl mainTabCtrl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage detailSubTab;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnUpdateCfg;

    }
}

