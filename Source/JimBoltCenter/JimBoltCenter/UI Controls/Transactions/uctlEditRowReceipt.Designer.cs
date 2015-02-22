namespace JimBoltCenter.UI_Controls.Transactions
{
    partial class uctlEditRowReceipt
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdReceiptItems = new DevExpress.XtraGrid.GridControl();
            this.grdvReceiptItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoCboItemName = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoCboSize = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoCboUnit = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoTxtPrice = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoTxtQTY = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoTxtSubTotal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.tlpMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceiptItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvReceiptItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCboItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCboSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCboUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxtPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxtQTY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxtSubTotal)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panel1, 0, 0);
            this.tlpMain.Controls.Add(this.panel2, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 245);
            this.tlpMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdReceiptItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 174);
            this.panel1.TabIndex = 0;
            // 
            // grdReceiptItems
            // 
            this.grdReceiptItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReceiptItems.Location = new System.Drawing.Point(0, 0);
            this.grdReceiptItems.MainView = this.grdvReceiptItems;
            this.grdReceiptItems.Name = "grdReceiptItems";
            this.grdReceiptItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoTextEdit,
            this.repoCboSize,
            this.repoCboItemName,
            this.repoTxtPrice,
            this.repoTxtQTY,
            this.repoTxtSubTotal,
            this.repoCboUnit});
            this.grdReceiptItems.Size = new System.Drawing.Size(994, 174);
            this.grdReceiptItems.TabIndex = 1;
            this.grdReceiptItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvReceiptItems});
            // 
            // grdvReceiptItems
            // 
            this.grdvReceiptItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.grdvReceiptItems.GridControl = this.grdReceiptItems;
            this.grdvReceiptItems.Name = "grdvReceiptItems";
            this.grdvReceiptItems.OptionsView.ColumnAutoWidth = false;
            this.grdvReceiptItems.OptionsView.ShowGroupPanel = false;
            this.grdvReceiptItems.OptionsView.ShowIndicator = false;
            this.grdvReceiptItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grdvReceiptItems_CellValueChanged);
            this.grdvReceiptItems.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grdvReceiptItems_InvalidRowException);
            this.grdvReceiptItems.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdvReceiptItems_ValidateRow);
            this.grdvReceiptItems.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grdvReceiptItems_CustomColumnDisplayText);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ItemIndex";
            this.gridColumn1.FieldName = "ItemIndex";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Stock Name";
            this.gridColumn2.ColumnEdit = this.repoCboItemName;
            this.gridColumn2.FieldName = "itemName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 190;
            // 
            // repoCboItemName
            // 
            this.repoCboItemName.AutoHeight = false;
            this.repoCboItemName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoCboItemName.ImmediatePopup = true;
            this.repoCboItemName.Name = "repoCboItemName";
            this.repoCboItemName.EditValueChanged += new System.EventHandler(this.repoCboItemName_EditValueChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Size";
            this.gridColumn4.ColumnEdit = this.repoCboSize;
            this.gridColumn4.FieldName = "itemSize";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 190;
            // 
            // repoCboSize
            // 
            this.repoCboSize.AutoHeight = false;
            this.repoCboSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoCboSize.ImmediatePopup = true;
            this.repoCboSize.Name = "repoCboSize";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Unit";
            this.gridColumn3.ColumnEdit = this.repoCboUnit;
            this.gridColumn3.FieldName = "_Unit";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // repoCboUnit
            // 
            this.repoCboUnit.AutoHeight = false;
            this.repoCboUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoCboUnit.ImmediatePopup = true;
            this.repoCboUnit.Name = "repoCboUnit";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Unit Price";
            this.gridColumn5.ColumnEdit = this.repoTxtPrice;
            this.gridColumn5.DisplayFormat.FormatString = "#,##0.000";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "UnitPrice";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 110;
            // 
            // repoTxtPrice
            // 
            this.repoTxtPrice.AutoHeight = false;
            this.repoTxtPrice.Name = "repoTxtPrice";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Discount";
            this.gridColumn6.ColumnEdit = this.repoTextEdit;
            this.gridColumn6.FieldName = "Discount";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 100;
            // 
            // repoTextEdit
            // 
            this.repoTextEdit.AutoHeight = false;
            this.repoTextEdit.Name = "repoTextEdit";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Discounted Price";
            this.gridColumn7.FieldName = "DiscountedPrice";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 130;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "QTY";
            this.gridColumn8.ColumnEdit = this.repoTxtQTY;
            this.gridColumn8.DisplayFormat.FormatString = "#,##0";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn8.FieldName = "QTY";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            // 
            // repoTxtQTY
            // 
            this.repoTxtQTY.AutoHeight = false;
            this.repoTxtQTY.Name = "repoTxtQTY";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Sub Total";
            this.gridColumn9.ColumnEdit = this.repoTxtSubTotal;
            this.gridColumn9.DisplayFormat.FormatString = "#,##0.000";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn9.FieldName = "SubTotal";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.OptionsColumn.FixedWidth = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 110;
            // 
            // repoTxtSubTotal
            // 
            this.repoTxtSubTotal.AutoHeight = false;
            this.repoTxtSubTotal.Name = "repoTxtSubTotal";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Orig. Unit Price";
            this.gridColumn10.DisplayFormat.FormatString = "#,##0.000";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "OrigPrice";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 8;
            this.gridColumn10.Width = 110;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 183);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(994, 59);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(541, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(405, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // uctlEditRowReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "uctlEditRowReceipt";
            this.Size = new System.Drawing.Size(1000, 245);
            this.Load += new System.EventHandler(this.uctlEditRowReceipt_Load);
            this.tlpMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReceiptItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvReceiptItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCboItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCboSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoCboUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxtPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxtQTY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoTxtSubTotal)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl grdReceiptItems;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvReceiptItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoCboItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoCboSize;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoTxtPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoTextEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoTxtQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repoTxtSubTotal;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoCboUnit;
    }
}
