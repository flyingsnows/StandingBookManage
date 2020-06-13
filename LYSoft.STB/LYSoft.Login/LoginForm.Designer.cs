namespace LYSoft.Login
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.g1_ver = new xiaoid.forms.xtraNote();
            this.msg1 = new xiaoid.forms.xtraNote();
            this.ProBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.g1_savePwd = new DevExpress.XtraEditors.CheckEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new xiaoid.forms.xtraImageButtonEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.p1 = new LYSoft.Login.Panel22();
            this.buttonEdit2 = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Tips = new System.Windows.Forms.Label();
            this.Lab_Edition = new System.Windows.Forms.Label();
            this.g1_user2 = new DevExpress.XtraEditors.MRUEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.g1_pwd = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ProBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.g1_savePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.p1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.g1_user2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.g1_pwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // g1_ver
            // 
            this.g1_ver.Appearance.ForeColor = System.Drawing.Color.White;
            this.g1_ver.Appearance.Options.UseForeColor = true;
            this.g1_ver.AutoSize = true;
            this.g1_ver.LineHeight = 1F;
            this.g1_ver.Lines = new string[0];
            this.g1_ver.Location = new System.Drawing.Point(683, 531);
            this.g1_ver.Name = "g1_ver";
            this.g1_ver.Size = new System.Drawing.Size(100, 18);
            this.g1_ver.TabIndex = 2;
            this.g1_ver.UseParentBackground = true;
            // 
            // msg1
            // 
            this.msg1.Appearance.ForeColor = System.Drawing.Color.White;
            this.msg1.Appearance.Options.UseForeColor = true;
            this.msg1.LineHeight = 1F;
            this.msg1.Lines = new string[0];
            this.msg1.Location = new System.Drawing.Point(639, 471);
            this.msg1.Name = "msg1";
            this.msg1.Size = new System.Drawing.Size(368, 117);
            this.msg1.TabIndex = 1;
            this.msg1.UseParentBackground = true;
            // 
            // ProBar
            // 
            this.ProBar.Location = new System.Drawing.Point(42, 165);
            this.ProBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProBar.Name = "ProBar";
            this.ProBar.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.ProBar.Properties.EndColor = System.Drawing.Color.White;
            this.ProBar.Properties.Maximum = 10;
            this.ProBar.Properties.PercentView = false;
            this.ProBar.Size = new System.Drawing.Size(368, 23);
            this.ProBar.TabIndex = 13;
            this.ProBar.Visible = false;
            // 
            // g1_savePwd
            // 
            this.g1_savePwd.Location = new System.Drawing.Point(42, 193);
            this.g1_savePwd.Name = "g1_savePwd";
            this.g1_savePwd.Properties.Caption = "记住密码";
            this.g1_savePwd.Size = new System.Drawing.Size(72, 19);
            this.g1_savePwd.TabIndex = 6;
            this.g1_savePwd.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::LYSoft.Login.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(-90, -9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.AllowEffect = true;
            this.btnExit.BackColor = System.Drawing.Color.Empty;
            this.btnExit.ContextImage = global::LYSoft.Login.Properties.Resources.close_24x24;
            this.btnExit.Location = new System.Drawing.Point(755, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(28, 28);
            this.btnExit.TabIndex = 9;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLogin.Image = global::LYSoft.Login.Properties.Resources.apply_16x16;
            this.btnLogin.Location = new System.Drawing.Point(194, 193);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(118, 40);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "登 录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // p1
            // 
            this.p1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.p1.Appearance.Options.UseBackColor = true;
            this.p1.Controls.Add(this.buttonEdit2);
            this.p1.Controls.Add(this.labelControl1);
            this.p1.Controls.Add(this.buttonEdit1);
            this.p1.Controls.Add(this.linkLabel1);
            this.p1.Controls.Add(this.Tips);
            this.p1.Controls.Add(this.Lab_Edition);
            this.p1.Controls.Add(this.g1_user2);
            this.p1.Controls.Add(this.labelControl3);
            this.p1.Controls.Add(this.btnLogin);
            this.p1.Controls.Add(this.g1_pwd);
            this.p1.Controls.Add(this.labelControl2);
            this.p1.Location = new System.Drawing.Point(393, 87);
            this.p1.Name = "p1";
            this.p1.Show980 = true;
            this.p1.Size = new System.Drawing.Size(368, 281);
            this.p1.TabIndex = 0;
            // 
            // buttonEdit2
            // 
            this.buttonEdit2.EditValue = "";
            this.buttonEdit2.Location = new System.Drawing.Point(228, 136);
            this.buttonEdit2.Name = "buttonEdit2";
            this.buttonEdit2.Properties.AutoHeight = false;
            this.buttonEdit2.Properties.ReadOnly = true;
            this.buttonEdit2.Size = new System.Drawing.Size(80, 22);
            this.buttonEdit2.TabIndex = 19;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(51, 141);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 14);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "验证码:";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.EditValue = "";
            this.buttonEdit1.Location = new System.Drawing.Point(105, 136);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.AutoHeight = false;
            this.buttonEdit1.Properties.ContextImage = ((System.Drawing.Image)(resources.GetObject("buttonEdit1.Properties.ContextImage")));
            this.buttonEdit1.Size = new System.Drawing.Size(117, 22);
            this.buttonEdit1.TabIndex = 17;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(253, 176);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 14);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "立即注册";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Tips
            // 
            this.Tips.AutoSize = true;
            this.Tips.BackColor = System.Drawing.Color.Transparent;
            this.Tips.ForeColor = System.Drawing.Color.Red;
            this.Tips.Location = new System.Drawing.Point(253, 244);
            this.Tips.Name = "Tips";
            this.Tips.Size = new System.Drawing.Size(87, 14);
            this.Tips.TabIndex = 12;
            this.Tips.Text = "数据加载中.....";
            this.Tips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Tips.Visible = false;
            // 
            // Lab_Edition
            // 
            this.Lab_Edition.AutoSize = true;
            this.Lab_Edition.ForeColor = System.Drawing.Color.White;
            this.Lab_Edition.Location = new System.Drawing.Point(5, 193);
            this.Lab_Edition.Name = "Lab_Edition";
            this.Lab_Edition.Size = new System.Drawing.Size(43, 14);
            this.Lab_Edition.TabIndex = 16;
            this.Lab_Edition.Text = "版本号";
            this.Lab_Edition.Visible = false;
            // 
            // g1_user2
            // 
            this.g1_user2.EditValue = "Administrators";
            this.g1_user2.Location = new System.Drawing.Point(105, 44);
            this.g1_user2.Name = "g1_user2";
            this.g1_user2.Properties.AutoHeight = false;
            this.g1_user2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.g1_user2.Properties.ContextImage = global::LYSoft.Login.Properties.Resources.employee_16x16;
            this.g1_user2.Properties.DropDownItemHeight = 20;
            this.g1_user2.Properties.DropDownRows = 8;
            this.g1_user2.Size = new System.Drawing.Size(207, 24);
            this.g1_user2.TabIndex = 3;
            this.g1_user2.SelectedIndexChanged += new System.EventHandler(this.g1_user_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(51, 95);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "密   码:";
            // 
            // g1_pwd
            // 
            this.g1_pwd.EditValue = "123456";
            this.g1_pwd.Location = new System.Drawing.Point(105, 93);
            this.g1_pwd.Name = "g1_pwd";
            this.g1_pwd.Properties.AutoHeight = false;
            this.g1_pwd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::LYSoft.Login.Properties.Resources.show_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.g1_pwd.Properties.ContextImage = global::LYSoft.Login.Properties.Resources.lock_16x16;
            this.g1_pwd.Properties.PasswordChar = '●';
            this.g1_pwd.Size = new System.Drawing.Size(207, 24);
            this.g1_pwd.TabIndex = 5;
            this.g1_pwd.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.g1_pwd_ButtonClick);
            this.g1_pwd.TextChanged += new System.EventHandler(this.g1_pwd_TextChanged);
            this.g1_pwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.g1_pwd_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(51, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "用户名:";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 444);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ProBar);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.g1_ver);
            this.Controls.Add(this.msg1);
            this.Controls.Add(this.g1_savePwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.g1_savePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.g1_user2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.g1_pwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit g1_savePwd;
        private DevExpress.XtraEditors.ButtonEdit g1_pwd;
        private xiaoid.forms.xtraNote msg1;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private xiaoid.forms.xtraNote g1_ver;
        private DevExpress.XtraEditors.MRUEdit g1_user2;
        private Panel22 p1;
        private xiaoid.forms.xtraImageButtonEdit btnExit;
        private System.Windows.Forms.Label Lab_Edition;
        private System.Windows.Forms.Label Tips;
        private DevExpress.XtraEditors.ProgressBarControl ProBar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}