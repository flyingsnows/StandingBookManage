namespace LYSoft.Main
{
    partial class RibbonFormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonFormMain));
            this.TabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.btn_User = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.btn_Book = new DevExpress.XtraNavBar.NavBarItem();
            this.bar_Item_Borr = new DevExpress.XtraNavBar.NavBarItem();
            this.btn_item_jt = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup4 = new DevExpress.XtraNavBar.NavBarGroup();
            this.btn_file_mg = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.bar_UpdatePaw = new DevExpress.XtraNavBar.NavBarItem();
            this.bar_logout = new DevExpress.XtraNavBar.NavBarItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.bar_Ver = new DevExpress.XtraBars.BarStaticItem();
            this.bar_Time = new DevExpress.XtraBars.BarStaticItem();
            this.bar_User = new DevExpress.XtraBars.BarStaticItem();
            this.bar_Number = new DevExpress.XtraBars.BarStaticItem();
            this.bar_RoleName = new DevExpress.XtraBars.BarStaticItem();
            this.skinBarSubItem1 = new DevExpress.XtraBars.SkinBarSubItem();
            this.barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.TabbedMdiManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabbedMdiManager
            // 
            this.TabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.TabbedMdiManager.FloatOnDrag = DevExpress.Utils.DefaultBoolean.True;
            this.TabbedMdiManager.FloatPageDragMode = DevExpress.XtraTabbedMdi.FloatPageDragMode.Preview;
            this.TabbedMdiManager.MdiParent = this;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup4,
            this.navBarGroup3});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.btn_User,
            this.btn_Book,
            this.bar_UpdatePaw,
            this.bar_logout,
            this.bar_Item_Borr,
            this.btn_item_jt,
            this.btn_file_mg});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 222;
            this.navBarControl1.Size = new System.Drawing.Size(222, 602);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarGroup1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.Appearance.Image")));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Appearance.Options.UseImage = true;
            this.navBarGroup1.Caption = "系统管理";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.btn_User)});
            this.navBarGroup1.Name = "navBarGroup1";
            this.navBarGroup1.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.SmallImage")));
            // 
            // btn_User
            // 
            this.btn_User.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btn_User.Appearance.Options.UseFont = true;
            this.btn_User.Caption = "用户管理";
            this.btn_User.Name = "btn_User";
            this.btn_User.SmallImage = ((System.Drawing.Image)(resources.GetObject("btn_User.SmallImage")));
            this.btn_User.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem1_LinkClicked);
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarGroup2.Appearance.Options.UseFont = true;
            this.navBarGroup2.Caption = "数据管理";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.btn_Book),
            new DevExpress.XtraNavBar.NavBarItemLink(this.bar_Item_Borr),
            new DevExpress.XtraNavBar.NavBarItemLink(this.btn_item_jt)});
            this.navBarGroup2.Name = "navBarGroup2";
            this.navBarGroup2.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup2.SmallImage")));
            // 
            // btn_Book
            // 
            this.btn_Book.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Book.Appearance.Options.UseFont = true;
            this.btn_Book.Caption = "供应（服务）商管理台账";
            this.btn_Book.Name = "btn_Book";
            this.btn_Book.SmallImage = ((System.Drawing.Image)(resources.GetObject("btn_Book.SmallImage")));
            this.btn_Book.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btn_Book_LinkClicked);
            // 
            // bar_Item_Borr
            // 
            this.bar_Item_Borr.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.bar_Item_Borr.Appearance.Options.UseFont = true;
            this.bar_Item_Borr.Appearance.Options.UseTextOptions = true;
            this.bar_Item_Borr.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.bar_Item_Borr.Caption = "评标专家管理台账";
            this.bar_Item_Borr.Name = "bar_Item_Borr";
            this.bar_Item_Borr.SmallImage = ((System.Drawing.Image)(resources.GetObject("bar_Item_Borr.SmallImage")));
            this.bar_Item_Borr.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.bar_Item_Borr_LinkClicked);
            // 
            // btn_item_jt
            // 
            this.btn_item_jt.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btn_item_jt.Appearance.Options.UseFont = true;
            this.btn_item_jt.Appearance.Options.UseTextOptions = true;
            this.btn_item_jt.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btn_item_jt.Caption = "招标议价谈判登记台账";
            this.btn_item_jt.Name = "btn_item_jt";
            this.btn_item_jt.SmallImage = ((System.Drawing.Image)(resources.GetObject("btn_item_jt.SmallImage")));
            this.btn_item_jt.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btn_item_jt_LinkClicked);
            // 
            // navBarGroup4
            // 
            this.navBarGroup4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarGroup4.Appearance.Options.UseFont = true;
            this.navBarGroup4.Appearance.Options.UseTextOptions = true;
            this.navBarGroup4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.navBarGroup4.Caption = "文件管理";
            this.navBarGroup4.Expanded = true;
            this.navBarGroup4.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.btn_file_mg)});
            this.navBarGroup4.Name = "navBarGroup4";
            this.navBarGroup4.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup4.SmallImage")));
            // 
            // btn_file_mg
            // 
            this.btn_file_mg.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btn_file_mg.Appearance.Options.UseFont = true;
            this.btn_file_mg.Appearance.Options.UseTextOptions = true;
            this.btn_file_mg.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btn_file_mg.Caption = "公文管理台账";
            this.btn_file_mg.ImageUri.Uri = "Customization";
            this.btn_file_mg.Name = "btn_file_mg";
            this.btn_file_mg.SmallImage = ((System.Drawing.Image)(resources.GetObject("btn_file_mg.SmallImage")));
            this.btn_file_mg.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.btn_file_pdf_LinkClicked);
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarGroup3.Appearance.Options.UseFont = true;
            this.navBarGroup3.Appearance.Options.UseTextOptions = true;
            this.navBarGroup3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.navBarGroup3.Caption = "帮助";
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.bar_UpdatePaw),
            new DevExpress.XtraNavBar.NavBarItemLink(this.bar_logout)});
            this.navBarGroup3.Name = "navBarGroup3";
            this.navBarGroup3.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup3.SmallImage")));
            // 
            // bar_UpdatePaw
            // 
            this.bar_UpdatePaw.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar_UpdatePaw.Appearance.Options.UseFont = true;
            this.bar_UpdatePaw.Appearance.Options.UseTextOptions = true;
            this.bar_UpdatePaw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.bar_UpdatePaw.Caption = "修改密码";
            this.bar_UpdatePaw.Name = "bar_UpdatePaw";
            this.bar_UpdatePaw.SmallImage = ((System.Drawing.Image)(resources.GetObject("bar_UpdatePaw.SmallImage")));
            this.bar_UpdatePaw.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.bar_UpdatePaw_LinkClicked);
            // 
            // bar_logout
            // 
            this.bar_logout.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar_logout.Appearance.Options.UseFont = true;
            this.bar_logout.Appearance.Options.UseTextOptions = true;
            this.bar_logout.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.bar_logout.Caption = "注销登录";
            this.bar_logout.Name = "bar_logout";
            this.bar_logout.SmallImage = ((System.Drawing.Image)(resources.GetObject("bar_logout.SmallImage")));
            this.bar_logout.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.bar_logout_LinkClicked);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCheckItem1,
            this.bar_Ver,
            this.bar_Time,
            this.bar_User,
            this.bar_Number,
            this.bar_RoleName,
            this.skinBarSubItem1,
            this.barMdiChildrenListItem1,
            this.barEditItem1});
            this.barManager1.MaxItemId = 9;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1133, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 602);
            this.barDockControlBottom.Size = new System.Drawing.Size(1133, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 602);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1133, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 602);
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "User";
            this.barCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barCheckItem1.Glyph")));
            this.barCheckItem1.Id = 0;
            this.barCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barCheckItem1.LargeGlyph")));
            this.barCheckItem1.Name = "barCheckItem1";
            this.barCheckItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bar_Ver
            // 
            this.bar_Ver.Caption = "Ver";
            this.bar_Ver.Glyph = ((System.Drawing.Image)(resources.GetObject("bar_Ver.Glyph")));
            this.bar_Ver.Id = 1;
            this.bar_Ver.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bar_Ver.LargeGlyph")));
            this.bar_Ver.Name = "bar_Ver";
            this.bar_Ver.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_Ver.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bar_Time
            // 
            this.bar_Time.Caption = "Time";
            this.bar_Time.Glyph = ((System.Drawing.Image)(resources.GetObject("bar_Time.Glyph")));
            this.bar_Time.Id = 2;
            this.bar_Time.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bar_Time.LargeGlyph")));
            this.bar_Time.Name = "bar_Time";
            this.bar_Time.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_Time.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bar_User
            // 
            this.bar_User.Caption = "User";
            this.bar_User.Glyph = ((System.Drawing.Image)(resources.GetObject("bar_User.Glyph")));
            this.bar_User.Id = 3;
            this.bar_User.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bar_User.LargeGlyph")));
            this.bar_User.Name = "bar_User";
            this.bar_User.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_User.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bar_Number
            // 
            this.bar_Number.Caption = "实时数量";
            this.bar_Number.Glyph = ((System.Drawing.Image)(resources.GetObject("bar_Number.Glyph")));
            this.bar_Number.Id = 4;
            this.bar_Number.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bar_Number.LargeGlyph")));
            this.bar_Number.Name = "bar_Number";
            this.bar_Number.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_Number.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bar_RoleName
            // 
            this.bar_RoleName.Caption = "角色权限";
            this.bar_RoleName.Glyph = ((System.Drawing.Image)(resources.GetObject("bar_RoleName.Glyph")));
            this.bar_RoleName.Id = 5;
            this.bar_RoleName.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bar_RoleName.LargeGlyph")));
            this.bar_RoleName.Name = "bar_RoleName";
            this.bar_RoleName.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bar_RoleName.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // skinBarSubItem1
            // 
            this.skinBarSubItem1.Caption = "skinBarSubItem1";
            this.skinBarSubItem1.Id = 6;
            this.skinBarSubItem1.Name = "skinBarSubItem1";
            // 
            // barMdiChildrenListItem1
            // 
            this.barMdiChildrenListItem1.Caption = "barMdiChildrenListItem1";
            this.barMdiChildrenListItem1.Id = 7;
            this.barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 8;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // RibbonFormMain
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 625);
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "RibbonFormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "唐口煤业内部招标议价管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RibbonFormMain_FormClosing);
            this.Load += new System.EventHandler(this.RibbonFormMain_Load);
            this.Resize += new System.EventHandler(this.RibbonFormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.TabbedMdiManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem btn_User;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem btn_Book;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem bar_UpdatePaw;
        private DevExpress.XtraNavBar.NavBarItem bar_logout;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem bar_Ver;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarStaticItem bar_Time;
        private DevExpress.XtraBars.BarStaticItem bar_User;
        private DevExpress.XtraBars.BarStaticItem bar_Number;
        private DevExpress.XtraNavBar.NavBarItem bar_Item_Borr;
        private DevExpress.XtraBars.BarStaticItem bar_RoleName;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItem1;
        private DevExpress.XtraBars.BarMdiChildrenListItem barMdiChildrenListItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraNavBar.NavBarItem btn_item_jt;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup4;
        private DevExpress.XtraNavBar.NavBarItem btn_file_mg;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager TabbedMdiManager;
    }
}