namespace JimBoltCenter.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMenu = new DevExpress.XtraEditors.PanelControl();
            this.navBarMainMenu = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarTransactions = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarTransactions_IssueReceipt = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarTransactions_Invoice_Stockin = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarTransactions_Retail = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarViewers = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarViewers_Invoice_Stockin = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarViewers_Receipt = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarViewers_Inventory = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarViewers_Series = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarViewers_Contacts = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarMaintenance = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarMaintenance_Forwarders = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarMaintenance_Sizes = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarMaintenance_InvoiceType = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarMaintenance_ItemWeight = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarUsers = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarUsers_Manage = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarUsers_ChangePass = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarUsers_Login = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarViewers_Tools = new DevExpress.XtraNavBar.NavBarItem();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCloseAll = new DevExpress.XtraEditors.SimpleButton();
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.lblSubTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.tabWindow = new DevExpress.XtraTab.XtraTabControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMenu)).BeginInit();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMainMenu)).BeginInit();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlMenu, 0, 0);
            this.tlpMain.Controls.Add(this.tlpData, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(918, 584);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.navBarMainMenu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenu.Location = new System.Drawing.Point(2, 2);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(206, 580);
            this.pnlMenu.TabIndex = 0;
            // 
            // navBarMainMenu
            // 
            this.navBarMainMenu.ActiveGroup = this.navBarTransactions;
            this.navBarMainMenu.Appearance.GroupHeader.Options.UseTextOptions = true;
            this.navBarMainMenu.Appearance.GroupHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.navBarMainMenu.Appearance.GroupHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.navBarMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarMainMenu.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarTransactions,
            this.navBarViewers,
            this.navBarMaintenance,
            this.navBarUsers});
            this.navBarMainMenu.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarTransactions_Invoice_Stockin,
            this.navBarViewers_Contacts,
            this.navBarViewers_Invoice_Stockin,
            this.navBarViewers_Tools,
            this.navBarViewers_Inventory,
            this.navBarTransactions_IssueReceipt,
            this.navBarViewers_Receipt,
            this.navBarMaintenance_InvoiceType,
            this.navBarMaintenance_Sizes,
            this.navBarMaintenance_Forwarders,
            this.navBarMaintenance_ItemWeight,
            this.navBarUsers_Manage,
            this.navBarUsers_Login,
            this.navBarUsers_ChangePass,
            this.navBarTransactions_Retail,
            this.navBarViewers_Series});
            this.navBarMainMenu.Location = new System.Drawing.Point(2, 2);
            this.navBarMainMenu.Margin = new System.Windows.Forms.Padding(2);
            this.navBarMainMenu.Name = "navBarMainMenu";
            this.navBarMainMenu.OptionsNavPane.ExpandedWidth = 264;
            this.navBarMainMenu.Size = new System.Drawing.Size(202, 576);
            this.navBarMainMenu.TabIndex = 2;
            this.navBarMainMenu.Text = "navBarControl2";
            this.navBarMainMenu.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinExplorerBarViewInfoRegistrator("Metropolis");
            // 
            // navBarTransactions
            // 
            this.navBarTransactions.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarTransactions.Appearance.Options.UseFont = true;
            this.navBarTransactions.Caption = "Transactions";
            this.navBarTransactions.Expanded = true;
            this.navBarTransactions.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarTransactions_IssueReceipt),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarTransactions_Invoice_Stockin),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarTransactions_Retail)});
            this.navBarTransactions.Name = "navBarTransactions";
            this.navBarTransactions.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarTransactions.SmallImage")));
            // 
            // navBarTransactions_IssueReceipt
            // 
            this.navBarTransactions_IssueReceipt.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarTransactions_IssueReceipt.Appearance.Options.UseFont = true;
            this.navBarTransactions_IssueReceipt.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarTransactions_IssueReceipt.AppearancePressed.Options.UseFont = true;
            this.navBarTransactions_IssueReceipt.Caption = "Issue Receipt";
            this.navBarTransactions_IssueReceipt.Name = "navBarTransactions_IssueReceipt";
            this.navBarTransactions_IssueReceipt.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarTransactions_IssueReceipt_LinkClicked);
            // 
            // navBarTransactions_Invoice_Stockin
            // 
            this.navBarTransactions_Invoice_Stockin.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarTransactions_Invoice_Stockin.Appearance.Options.UseFont = true;
            this.navBarTransactions_Invoice_Stockin.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarTransactions_Invoice_Stockin.AppearancePressed.Options.UseFont = true;
            this.navBarTransactions_Invoice_Stockin.Caption = "StockIn Invoice";
            this.navBarTransactions_Invoice_Stockin.Name = "navBarTransactions_Invoice_Stockin";
            this.navBarTransactions_Invoice_Stockin.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarTransactions_Invoice_Stockin_LinkClicked);
            // 
            // navBarTransactions_Retail
            // 
            this.navBarTransactions_Retail.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarTransactions_Retail.Appearance.Options.UseFont = true;
            this.navBarTransactions_Retail.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarTransactions_Retail.AppearancePressed.Options.UseFont = true;
            this.navBarTransactions_Retail.Caption = "Retail Invoice";
            this.navBarTransactions_Retail.Name = "navBarTransactions_Retail";
            this.navBarTransactions_Retail.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarTransactions_Retail_LinkClicked);
            // 
            // navBarViewers
            // 
            this.navBarViewers.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers.Appearance.Options.UseFont = true;
            this.navBarViewers.Caption = "Viewers";
            this.navBarViewers.Expanded = true;
            this.navBarViewers.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarViewers_Invoice_Stockin),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarViewers_Receipt),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarViewers_Inventory),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarViewers_Series),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarViewers_Contacts)});
            this.navBarViewers.Name = "navBarViewers";
            this.navBarViewers.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarViewers.SmallImage")));
            // 
            // navBarViewers_Invoice_Stockin
            // 
            this.navBarViewers_Invoice_Stockin.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Invoice_Stockin.Appearance.Options.UseFont = true;
            this.navBarViewers_Invoice_Stockin.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Invoice_Stockin.AppearancePressed.Options.UseFont = true;
            this.navBarViewers_Invoice_Stockin.Caption = "StockIn Invoices";
            this.navBarViewers_Invoice_Stockin.Name = "navBarViewers_Invoice_Stockin";
            this.navBarViewers_Invoice_Stockin.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarViewers_Invoice_Stockin_LinkClicked);
            // 
            // navBarViewers_Receipt
            // 
            this.navBarViewers_Receipt.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarViewers_Receipt.Appearance.Options.UseFont = true;
            this.navBarViewers_Receipt.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarViewers_Receipt.AppearancePressed.Options.UseFont = true;
            this.navBarViewers_Receipt.Caption = "Receipts";
            this.navBarViewers_Receipt.Name = "navBarViewers_Receipt";
            this.navBarViewers_Receipt.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarViewers_Receipt_LinkClicked);
            // 
            // navBarViewers_Inventory
            // 
            this.navBarViewers_Inventory.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Inventory.Appearance.Options.UseFont = true;
            this.navBarViewers_Inventory.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Inventory.AppearancePressed.Options.UseFont = true;
            this.navBarViewers_Inventory.Caption = "Inventory";
            this.navBarViewers_Inventory.Name = "navBarViewers_Inventory";
            this.navBarViewers_Inventory.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarViewers_Inventory_LinkClicked);
            // 
            // navBarViewers_Series
            // 
            this.navBarViewers_Series.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarViewers_Series.Appearance.Options.UseFont = true;
            this.navBarViewers_Series.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarViewers_Series.AppearancePressed.Options.UseFont = true;
            this.navBarViewers_Series.Caption = "Retail Series";
            this.navBarViewers_Series.Name = "navBarViewers_Series";
            this.navBarViewers_Series.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarViewers_Series_LinkClicked);
            // 
            // navBarViewers_Contacts
            // 
            this.navBarViewers_Contacts.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Contacts.Appearance.Options.UseFont = true;
            this.navBarViewers_Contacts.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Contacts.AppearancePressed.Options.UseFont = true;
            this.navBarViewers_Contacts.Caption = "Contacts";
            this.navBarViewers_Contacts.Name = "navBarViewers_Contacts";
            this.navBarViewers_Contacts.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarViewers_Contacts_LinkClicked);
            // 
            // navBarMaintenance
            // 
            this.navBarMaintenance.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarMaintenance.Appearance.Options.UseFont = true;
            this.navBarMaintenance.Caption = "Maintenance";
            this.navBarMaintenance.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarMaintenance_Forwarders),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarMaintenance_Sizes),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarMaintenance_InvoiceType),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarMaintenance_ItemWeight)});
            this.navBarMaintenance.Name = "navBarMaintenance";
            this.navBarMaintenance.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarMaintenance.SmallImage")));
            // 
            // navBarMaintenance_Forwarders
            // 
            this.navBarMaintenance_Forwarders.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_Forwarders.Appearance.Options.UseFont = true;
            this.navBarMaintenance_Forwarders.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_Forwarders.AppearancePressed.Options.UseFont = true;
            this.navBarMaintenance_Forwarders.Caption = "Forwarders";
            this.navBarMaintenance_Forwarders.Name = "navBarMaintenance_Forwarders";
            this.navBarMaintenance_Forwarders.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarMaintenance_Forwarders_LinkClicked);
            // 
            // navBarMaintenance_Sizes
            // 
            this.navBarMaintenance_Sizes.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_Sizes.Appearance.Options.UseFont = true;
            this.navBarMaintenance_Sizes.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_Sizes.AppearancePressed.Options.UseFont = true;
            this.navBarMaintenance_Sizes.Caption = "Item Sizes";
            this.navBarMaintenance_Sizes.Name = "navBarMaintenance_Sizes";
            this.navBarMaintenance_Sizes.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarMaintenance_Sizes_LinkClicked);
            // 
            // navBarMaintenance_InvoiceType
            // 
            this.navBarMaintenance_InvoiceType.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_InvoiceType.Appearance.Options.UseFont = true;
            this.navBarMaintenance_InvoiceType.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_InvoiceType.AppearancePressed.Options.UseFont = true;
            this.navBarMaintenance_InvoiceType.Caption = "Invoice Types";
            this.navBarMaintenance_InvoiceType.Name = "navBarMaintenance_InvoiceType";
            this.navBarMaintenance_InvoiceType.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarMaintenance_InvoiceType_LinkClicked);
            // 
            // navBarMaintenance_ItemWeight
            // 
            this.navBarMaintenance_ItemWeight.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_ItemWeight.Appearance.Options.UseFont = true;
            this.navBarMaintenance_ItemWeight.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarMaintenance_ItemWeight.AppearancePressed.Options.UseFont = true;
            this.navBarMaintenance_ItemWeight.Caption = "Item Weights";
            this.navBarMaintenance_ItemWeight.Name = "navBarMaintenance_ItemWeight";
            this.navBarMaintenance_ItemWeight.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarMaintenance_ItemWeight_LinkClicked);
            // 
            // navBarUsers
            // 
            this.navBarUsers.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.navBarUsers.Appearance.Options.UseFont = true;
            this.navBarUsers.Caption = "Users";
            this.navBarUsers.Expanded = true;
            this.navBarUsers.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarUsers_Manage),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarUsers_ChangePass),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarUsers_Login)});
            this.navBarUsers.Name = "navBarUsers";
            this.navBarUsers.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarUsers.SmallImage")));
            // 
            // navBarUsers_Manage
            // 
            this.navBarUsers_Manage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarUsers_Manage.Appearance.Options.UseFont = true;
            this.navBarUsers_Manage.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarUsers_Manage.AppearancePressed.Options.UseFont = true;
            this.navBarUsers_Manage.Caption = "Manage Users";
            this.navBarUsers_Manage.Name = "navBarUsers_Manage";
            this.navBarUsers_Manage.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarUsers_Manage_LinkClicked);
            // 
            // navBarUsers_ChangePass
            // 
            this.navBarUsers_ChangePass.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarUsers_ChangePass.Appearance.Options.UseFont = true;
            this.navBarUsers_ChangePass.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarUsers_ChangePass.AppearancePressed.Options.UseFont = true;
            this.navBarUsers_ChangePass.Caption = "Change Password";
            this.navBarUsers_ChangePass.Name = "navBarUsers_ChangePass";
            // 
            // navBarUsers_Login
            // 
            this.navBarUsers_Login.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarUsers_Login.Appearance.Options.UseFont = true;
            this.navBarUsers_Login.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F);
            this.navBarUsers_Login.AppearancePressed.Options.UseFont = true;
            this.navBarUsers_Login.Caption = "Log In";
            this.navBarUsers_Login.Name = "navBarUsers_Login";
            this.navBarUsers_Login.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarUsers_Login_LinkClicked);
            // 
            // navBarViewers_Tools
            // 
            this.navBarViewers_Tools.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Tools.Appearance.Options.UseFont = true;
            this.navBarViewers_Tools.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarViewers_Tools.AppearancePressed.Options.UseFont = true;
            this.navBarViewers_Tools.Caption = "Tools";
            this.navBarViewers_Tools.Name = "navBarViewers_Tools";
            this.navBarViewers_Tools.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarViewers_Tools_LinkClicked);
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 1;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Controls.Add(this.panelControl1, 0, 0);
            this.tlpData.Controls.Add(this.tlpBody, 0, 1);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(212, 2);
            this.tlpData.Margin = new System.Windows.Forms.Padding(2);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 2;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(704, 580);
            this.tlpData.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Controls.Add(this.lblSubTitle);
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(700, 61);
            this.panelControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnCloseAll, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTime, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(500, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(198, 57);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnCloseAll.Appearance.Options.UseFont = true;
            this.btnCloseAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseAll.Image")));
            this.btnCloseAll.Location = new System.Drawing.Point(3, 29);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(92, 20);
            this.btnCloseAll.TabIndex = 3;
            this.btnCloseAll.Text = "Close All";
            this.btnCloseAll.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // lblTime
            // 
            this.lblTime.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTime.Location = new System.Drawing.Point(131, 3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Padding = new System.Windows.Forms.Padding(0, 2, 12, 0);
            this.lblTime.Size = new System.Drawing.Size(64, 18);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "00:00:00";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.Location = new System.Drawing.Point(22, 37);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(73, 16);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "[Bolt Center]";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(22, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(114, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BOLT CENTER";
            // 
            // tlpBody
            // 
            this.tlpBody.ColumnCount = 1;
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpBody.Controls.Add(this.tabWindow, 0, 0);
            this.tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBody.Location = new System.Drawing.Point(2, 67);
            this.tlpBody.Margin = new System.Windows.Forms.Padding(2);
            this.tlpBody.Name = "tlpBody";
            this.tlpBody.RowCount = 1;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 529F));
            this.tlpBody.Size = new System.Drawing.Size(700, 511);
            this.tlpBody.TabIndex = 1;
            // 
            // tabWindow
            // 
            this.tabWindow.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.tabWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWindow.Location = new System.Drawing.Point(2, 2);
            this.tabWindow.Margin = new System.Windows.Forms.Padding(2);
            this.tabWindow.Name = "tabWindow";
            this.tabWindow.Size = new System.Drawing.Size(696, 507);
            this.tabWindow.TabIndex = 0;
            this.tabWindow.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabWindow_SelectedPageChanged);
            this.tabWindow.CloseButtonClick += new System.EventHandler(this.tabWindow_CloseButtonClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // MainForm
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 584);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bolt Center";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMenu)).EndInit();
            this.pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarMainMenu)).EndInit();
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private DevExpress.XtraEditors.PanelControl pnlMenu;
        private DevExpress.XtraNavBar.NavBarControl navBarMainMenu;
        private DevExpress.XtraNavBar.NavBarGroup navBarTransactions;
        private DevExpress.XtraNavBar.NavBarGroup navBarViewers;
        private DevExpress.XtraNavBar.NavBarItem navBarTransactions_Invoice_Stockin;
        private DevExpress.XtraNavBar.NavBarItem navBarViewers_Contacts;
        private DevExpress.XtraNavBar.NavBarItem navBarViewers_Invoice_Stockin;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private DevExpress.XtraEditors.LabelControl lblTime;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraNavBar.NavBarGroup navBarMaintenance;
        private DevExpress.XtraNavBar.NavBarItem navBarViewers_Tools;
        public DevExpress.XtraTab.XtraTabControl tabWindow;
        public DevExpress.XtraEditors.LabelControl lblSubTitle;
        public DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraNavBar.NavBarItem navBarViewers_Inventory;
        private System.Windows.Forms.Timer timer2;
        private DevExpress.XtraNavBar.NavBarItem navBarTransactions_IssueReceipt;
        private DevExpress.XtraNavBar.NavBarItem navBarViewers_Receipt;
        private DevExpress.XtraNavBar.NavBarItem navBarMaintenance_Forwarders;
        private DevExpress.XtraNavBar.NavBarItem navBarMaintenance_Sizes;
        private DevExpress.XtraNavBar.NavBarItem navBarMaintenance_InvoiceType;
        private DevExpress.XtraNavBar.NavBarItem navBarMaintenance_ItemWeight;
        private DevExpress.XtraEditors.SimpleButton btnCloseAll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraNavBar.NavBarGroup navBarUsers;
        private DevExpress.XtraNavBar.NavBarItem navBarUsers_Manage;
        private DevExpress.XtraNavBar.NavBarItem navBarUsers_ChangePass;
        private DevExpress.XtraNavBar.NavBarItem navBarUsers_Login;
        private DevExpress.XtraNavBar.NavBarItem navBarTransactions_Retail;
        private DevExpress.XtraNavBar.NavBarItem navBarViewers_Series;


    }
}