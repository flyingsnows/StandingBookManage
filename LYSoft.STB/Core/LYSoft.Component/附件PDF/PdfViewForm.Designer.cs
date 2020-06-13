namespace LYSoft.Component
{
    partial class PdfViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdfViewForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.file_grid_control = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.location = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_file_delete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_file_daoru = new DevExpress.XtraEditors.SimpleButton();
            this.btn_file_open = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.text_search = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.file_grid_control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.text_search.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.file_grid_control);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(786, 365);
            this.panelControl1.TabIndex = 0;
            // 
            // file_grid_control
            // 
            this.file_grid_control.Location = new System.Drawing.Point(0, 94);
            this.file_grid_control.MainView = this.gridView1;
            this.file_grid_control.Name = "file_grid_control";
            this.file_grid_control.Size = new System.Drawing.Size(785, 269);
            this.file_grid_control.TabIndex = 1;
            this.file_grid_control.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.name,
            this.location,
            this.type});
            this.gridView1.GridControl = this.file_grid_control;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // name
            // 
            this.name.AppearanceCell.Options.UseTextOptions = true;
            this.name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.name.AppearanceHeader.Options.UseTextOptions = true;
            this.name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.name.Caption = "文件名";
            this.name.FieldName = "name";
            this.name.Name = "name";
            this.name.OptionsColumn.AllowEdit = false;
            this.name.OptionsColumn.AllowFocus = false;
            this.name.Visible = true;
            this.name.VisibleIndex = 0;
            // 
            // location
            // 
            this.location.AppearanceCell.Options.UseTextOptions = true;
            this.location.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.location.AppearanceHeader.Options.UseTextOptions = true;
            this.location.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.location.Caption = "储存路径";
            this.location.FieldName = "location";
            this.location.Name = "location";
            this.location.OptionsColumn.AllowEdit = false;
            this.location.OptionsColumn.AllowFocus = false;
            this.location.Visible = true;
            this.location.VisibleIndex = 1;
            // 
            // type
            // 
            this.type.AppearanceCell.Options.UseTextOptions = true;
            this.type.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.type.AppearanceHeader.Options.UseTextOptions = true;
            this.type.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.type.Caption = "文件类型";
            this.type.FieldName = "type";
            this.type.Name = "type";
            this.type.OptionsColumn.AllowEdit = false;
            this.type.OptionsColumn.AllowFocus = false;
            this.type.Visible = true;
            this.type.VisibleIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_file_delete);
            this.panelControl2.Controls.Add(this.btn_file_daoru);
            this.panelControl2.Controls.Add(this.btn_file_open);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.text_search);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(785, 95);
            this.panelControl2.TabIndex = 0;
            // 
            // btn_file_delete
            // 
            this.btn_file_delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_file_delete.Image")));
            this.btn_file_delete.Location = new System.Drawing.Point(684, 29);
            this.btn_file_delete.Name = "btn_file_delete";
            this.btn_file_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_file_delete.TabIndex = 5;
            this.btn_file_delete.Text = "删除";
            this.btn_file_delete.Click += new System.EventHandler(this.btn_file_delete_Click);
            // 
            // btn_file_daoru
            // 
            this.btn_file_daoru.Image = ((System.Drawing.Image)(resources.GetObject("btn_file_daoru.Image")));
            this.btn_file_daoru.Location = new System.Drawing.Point(573, 29);
            this.btn_file_daoru.Name = "btn_file_daoru";
            this.btn_file_daoru.Size = new System.Drawing.Size(75, 23);
            this.btn_file_daoru.TabIndex = 4;
            this.btn_file_daoru.Text = "导入";
            this.btn_file_daoru.Click += new System.EventHandler(this.btn_file_daoru_Click);
            // 
            // btn_file_open
            // 
            this.btn_file_open.Image = ((System.Drawing.Image)(resources.GetObject("btn_file_open.Image")));
            this.btn_file_open.Location = new System.Drawing.Point(467, 29);
            this.btn_file_open.Name = "btn_file_open";
            this.btn_file_open.Size = new System.Drawing.Size(75, 23);
            this.btn_file_open.TabIndex = 3;
            this.btn_file_open.Text = "查看文件";
            this.btn_file_open.Click += new System.EventHandler(this.btn_file_open_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(315, 29);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "查询（Q）";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // text_search
            // 
            this.text_search.Location = new System.Drawing.Point(81, 32);
            this.text_search.Name = "text_search";
            this.text_search.Size = new System.Drawing.Size(136, 20);
            this.text_search.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "文件名";
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // PdfViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 365);
            this.Controls.Add(this.panelControl1);
            this.Name = "PdfViewForm";
            this.Text = "附件管理";
            this.Load += new System.EventHandler(this.PdfViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.file_grid_control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.text_search.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit text_search;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_file_daoru;
        private DevExpress.XtraEditors.SimpleButton btn_file_open;
        private DevExpress.XtraEditors.SimpleButton btn_file_delete;
        private DevExpress.XtraGrid.GridControl file_grid_control;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn location;
        private DevExpress.XtraGrid.Columns.GridColumn type;
        private DevExpress.XtraGrid.Columns.GridColumn name;
    }
}