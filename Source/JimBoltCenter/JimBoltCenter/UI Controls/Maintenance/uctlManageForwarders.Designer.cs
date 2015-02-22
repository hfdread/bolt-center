namespace JimBoltCenter.UI_Controls.Maintenance
{
    partial class uctlManageForwarders
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
            this.grdForwarders = new DevExpress.XtraGrid.GridControl();
            this.grdvForwarders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDeleteForwarder = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditForwarder = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddForwarder = new DevExpress.XtraEditors.SimpleButton();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdForwarders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvForwarders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panelControl1, 0, 0);
            this.tlpMain.Controls.Add(this.panelControl2, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.Size = new System.Drawing.Size(732, 478);
            this.tlpMain.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdForwarders);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(726, 392);
            this.panelControl1.TabIndex = 0;
            // 
            // grdForwarders
            // 
            this.grdForwarders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdForwarders.Location = new System.Drawing.Point(2, 2);
            this.grdForwarders.MainView = this.grdvForwarders;
            this.grdForwarders.Name = "grdForwarders";
            this.grdForwarders.Size = new System.Drawing.Size(722, 388);
            this.grdForwarders.TabIndex = 1;
            this.grdForwarders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvForwarders});
            // 
            // grdvForwarders
            // 
            this.grdvForwarders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.grdvForwarders.GridControl = this.grdForwarders;
            this.grdvForwarders.Name = "grdvForwarders";
            this.grdvForwarders.OptionsView.ShowGroupPanel = false;
            this.grdvForwarders.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grdvForwarders_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Company";
            this.gridColumn2.FieldName = "CompanyName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Details";
            this.gridColumn3.FieldName = "Details";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Dirty";
            this.gridColumn4.FieldName = "isDirty";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnDeleteForwarder);
            this.panelControl2.Controls.Add(this.btnEditForwarder);
            this.panelControl2.Controls.Add(this.btnAddForwarder);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(3, 401);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(726, 74);
            this.panelControl2.TabIndex = 1;
            // 
            // btnDeleteForwarder
            // 
            this.btnDeleteForwarder.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnDeleteForwarder.Appearance.Options.UseFont = true;
            this.btnDeleteForwarder.Location = new System.Drawing.Point(443, 16);
            this.btnDeleteForwarder.Name = "btnDeleteForwarder";
            this.btnDeleteForwarder.Size = new System.Drawing.Size(84, 35);
            this.btnDeleteForwarder.TabIndex = 6;
            this.btnDeleteForwarder.Text = "&Delete";
            this.btnDeleteForwarder.Click += new System.EventHandler(this.btnDeleteForwarder_Click);
            // 
            // btnEditForwarder
            // 
            this.btnEditForwarder.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnEditForwarder.Appearance.Options.UseFont = true;
            this.btnEditForwarder.Location = new System.Drawing.Point(320, 16);
            this.btnEditForwarder.Name = "btnEditForwarder";
            this.btnEditForwarder.Size = new System.Drawing.Size(84, 35);
            this.btnEditForwarder.TabIndex = 5;
            this.btnEditForwarder.Text = "&Edit";
            this.btnEditForwarder.Click += new System.EventHandler(this.btnEditForwarder_Click);
            // 
            // btnAddForwarder
            // 
            this.btnAddForwarder.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnAddForwarder.Appearance.Options.UseFont = true;
            this.btnAddForwarder.Location = new System.Drawing.Point(197, 16);
            this.btnAddForwarder.Name = "btnAddForwarder";
            this.btnAddForwarder.Size = new System.Drawing.Size(84, 35);
            this.btnAddForwarder.TabIndex = 4;
            this.btnAddForwarder.Text = "&Add";
            this.btnAddForwarder.Click += new System.EventHandler(this.btnAddForwarder_Click);
            // 
            // uctlManageForwarders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "uctlManageForwarders";
            this.Size = new System.Drawing.Size(732, 478);
            this.Load += new System.EventHandler(this.uctlManageForwarders_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdForwarders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvForwarders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grdForwarders;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvForwarders;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SimpleButton btnDeleteForwarder;
        private DevExpress.XtraEditors.SimpleButton btnEditForwarder;
        private DevExpress.XtraEditors.SimpleButton btnAddForwarder;
    }
}
