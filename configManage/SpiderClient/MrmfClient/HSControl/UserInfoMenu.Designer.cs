namespace SpiderC.HSControl
{
    partial class UserInfoMenu
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setting = new System.Windows.Forms.ToolStripMenuItem();
            this.modiPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setting,
            this.modiPassword,
            this.toolStripSeparator1,
            this.exitLogin});
            this.contextMenuStrip1.Name = "userInfoMenu";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 88);
            // 
            // setting
            // 
            this.setting.Name = "setting";
            this.setting.Size = new System.Drawing.Size(181, 26);
            this.setting.Text = "设置";
            this.setting.Click += new System.EventHandler(this.setting_Click);
            // 
            // modiPassword
            // 
            this.modiPassword.Name = "modiPassword";
            this.modiPassword.Size = new System.Drawing.Size(144, 26);
            this.modiPassword.Text = "修改密码";
            this.modiPassword.Click += new System.EventHandler(this.modiPassword_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitLogin
            // 
            this.exitLogin.Name = "exitLogin";
            this.exitLogin.Size = new System.Drawing.Size(181, 26);
            this.exitLogin.Text = "退出";
            this.exitLogin.Click += new System.EventHandler(this.exitLogin_Click);
            // 
            // UserInfoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UserInfoMenu";
            this.Size = new System.Drawing.Size(259, 68);
            this.Load += new System.EventHandler(this.UserInfoMenu_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setting;
        private System.Windows.Forms.ToolStripMenuItem modiPassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitLogin;
    }
}
