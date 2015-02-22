namespace JimBoltCenter.UI_Controls.Maintenance
{
    partial class uctlManageInvoiceType
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grdInvoice = new DevExpress.XtraGrid.GridControl();
            this.grdvInvoice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoTxt2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoChk2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnDeleteInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxt2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoChk2)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Controls.Add(this.panelControl1, 0, 0);
            this.tlpMain.Controls.Add(this.panelControl2, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpMain.Size = new System.Drawing.Size(685, 410);
            this.tlpMain.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdInvoice);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(679, 314);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnDeleteInvoice);
            this.panelControl2.Controls.Add(this.btnEditInvoice);
            this.panelControl2.Controls.Add(this.btnAddInvoice);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 323);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(679, 84);
            this.panelControl2.TabIndex = 1;
            // 
            // grdInvoice
            // 
            this.grdInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInvoice.Location = new System.Drawing.Point(2, 2);
            this.grdInvoice.MainView = this.grdvInvoice;
            this.grdInvoice.Name = "grdInvoice";
            this.grdInvoice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoTxt2,
            this.repoChk2});
            this.grdInvoice.Size = new System.Drawing.Size(675, 310);
            this.grdInvoice.TabIndex = 2;
            this.grdInvoice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvInvoice});
            // 
            // grdvInvoice
            // 
            this.grdvInvoice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.grdvInvoice.GridControl = this.grdInvoice;
            this.grdvInvoice.Name = "grdvInvoice";
            this.grdvInvoice.OptionsView.ShowGroupPanel = false;
            this.grdvInvoice.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grdvInvoice_CellValueChanged);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "ID";
            this.gridColumn8.FieldName = "ID";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Type";
            this.gridColumn9.ColumnEdit = this.repoTxt2;
            this.gridColumn9.FieldName = "Type";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            // 
            // repoTxt2
            // 
            this.repoTxt2.AutoHeight = false;
            this.repoTxt2.Name = "repoTxt2";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Code";
            this.gridColumn10.ColumnEdit = this.repoTxt2;
            this.gridColumn10.FieldName = "Code";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Dirty";
            this.gridColumn11.ColumnEdit = this.repoChk2;
            this.gridColumn11.FieldName = "isDirty";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // repoChk2
            // 
            this.repoChk2.AutoHeight = false;
            this.repoChk2.Name = "repoChk2";
            // 
            // btnDeleteInvoice
            // 
            this.btnDeleteInvoice.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnDeleteInvoice.Appearance.Options.UseFont = true;
            this.btnDeleteInvoice.Location = new System.Drawing.Point(420, 18);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(84, 35);
            this.btnDeleteInvoice.TabIndex = 6;
            this.btnDeleteInvoice.Text = "&Delete";
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            // 
            // btnEditInvoice
            // 
            this.btnEditInvoice.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnEditInvoice.Appearance.Options.UseFont = true;
            this.btnEditInvoice.Location = new System.Drawing.Point(297, 18);
            this.btnEditInvoice.Name = "btnEditInvoice";
            this.btnEditInvoice.Size = new System.Drawing.Size(84, 35);
            this.btnEditInvoice.TabIndex = 5;
            this.btnEditInvoice.Text = "&Edit";
            this.btnEditInvoice.Click += new System.EventHandler(this.btnEditInvoice_Click);
            // 
            // btnAddInvoice
            // 
            this.btnAddInvoice.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnAddInvoice.Appearance.Options.UseFont = true;
            this.btnAddInvoice.Location = new System.Drawing.Point(174, 18);
            this.btnAddInvoice.Name = "btnAddInvoice";
            this.btnAddInvoice.Size = new System.Drawing.Size(84, 35);
            this.btnAddInvoice.TabIndex = 4;
            this.btnAddInvoice.Text = "&Add";
            this.btnAddInvoice.Click += new System.EventHandler(this.btnAddInvoice_Click);
            // 
            // uctlManageInvoiceType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "uctlManageInvoiceType";
            this.Size = new System.Drawing.Size(685, 410);
            this.Load += new System.EventHandler(this.uctlManageInvoiceType_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxt2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoChk2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdInvoice;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoTxt2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repoChk2;
        private DevExpress.XtraEditors.SimpleButton btnDeleteInvoice;
        private DevExpress.XtraEditors.SimpleButton btnEditInvoice;
        private DevExpress.XtraEditors.SimpleButton btnAddInvoice;
    }
}
