namespace JimBoltCenter.UI_Controls.Maintenance
{
    partial class uctlManageSizes
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uctlManageSizes));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grdSize = new DevExpress.XtraGrid.GridControl();
            this.grdvSize = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoTxt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoChk = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDeleteSize = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditSize = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddSize = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoChk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panelControl1, 0, 1);
            this.tlpMain.Controls.Add(this.panelControl2, 0, 2);
            this.tlpMain.Controls.Add(this.panelControl3, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tlpMain.Size = new System.Drawing.Size(672, 442);
            this.tlpMain.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdSize);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 58);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(666, 294);
            this.panelControl1.TabIndex = 0;
            // 
            // grdSize
            // 
            this.grdSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSize.Location = new System.Drawing.Point(2, 2);
            this.grdSize.MainView = this.grdvSize;
            this.grdSize.Name = "grdSize";
            this.grdSize.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoTxt,
            this.repoChk});
            this.grdSize.Size = new System.Drawing.Size(662, 290);
            this.grdSize.TabIndex = 1;
            this.grdSize.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvSize});
            // 
            // grdvSize
            // 
            this.grdvSize.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.grdvSize.GridControl = this.grdSize;
            this.grdvSize.Name = "grdvSize";
            this.grdvSize.OptionsView.ShowGroupPanel = false;
            this.grdvSize.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grdvSize_CellValueChanged);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Size";
            this.gridColumn5.ColumnEdit = this.repoTxt;
            this.gridColumn5.FieldName = "Description";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // repoTxt
            // 
            this.repoTxt.AutoHeight = false;
            this.repoTxt.Name = "repoTxt";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Dirty";
            this.gridColumn6.ColumnEdit = this.repoChk;
            this.gridColumn6.FieldName = "isDirty";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // repoChk
            // 
            this.repoChk.AutoHeight = false;
            this.repoChk.Name = "repoChk";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnDeleteSize);
            this.panelControl2.Controls.Add(this.btnEditSize);
            this.panelControl2.Controls.Add(this.btnAddSize);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 358);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(666, 81);
            this.panelControl2.TabIndex = 1;
            // 
            // btnDeleteSize
            // 
            this.btnDeleteSize.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnDeleteSize.Appearance.Options.UseFont = true;
            this.btnDeleteSize.Location = new System.Drawing.Point(416, 13);
            this.btnDeleteSize.Name = "btnDeleteSize";
            this.btnDeleteSize.Size = new System.Drawing.Size(84, 35);
            this.btnDeleteSize.TabIndex = 6;
            this.btnDeleteSize.Text = "&Delete";
            this.btnDeleteSize.Click += new System.EventHandler(this.btnDeleteSize_Click);
            // 
            // btnEditSize
            // 
            this.btnEditSize.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnEditSize.Appearance.Options.UseFont = true;
            this.btnEditSize.Location = new System.Drawing.Point(293, 13);
            this.btnEditSize.Name = "btnEditSize";
            this.btnEditSize.Size = new System.Drawing.Size(84, 35);
            this.btnEditSize.TabIndex = 5;
            this.btnEditSize.Text = "&Edit";
            this.btnEditSize.Click += new System.EventHandler(this.btnEditSize_Click);
            // 
            // btnAddSize
            // 
            this.btnAddSize.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnAddSize.Appearance.Options.UseFont = true;
            this.btnAddSize.Location = new System.Drawing.Point(170, 13);
            this.btnAddSize.Name = "btnAddSize";
            this.btnAddSize.Size = new System.Drawing.Size(84, 35);
            this.btnAddSize.TabIndex = 4;
            this.btnAddSize.Text = "&Add";
            this.btnAddSize.Click += new System.EventHandler(this.btnAddSize_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Controls.Add(this.txtSearch);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 3);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(666, 49);
            this.panelControl3.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(188, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 35);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(18, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(149, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.EditValueChanged += new System.EventHandler(this.txtSearch_EditValueChanged);
            // 
            // uctlManageSizes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "uctlManageSizes";
            this.Size = new System.Drawing.Size(672, 442);
            this.Load += new System.EventHandler(this.uctlManageSizes_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoChk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdSize;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvSize;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoTxt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repoChk;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SimpleButton btnDeleteSize;
        private DevExpress.XtraEditors.SimpleButton btnEditSize;
        private DevExpress.XtraEditors.SimpleButton btnAddSize;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl3;
    }
}
