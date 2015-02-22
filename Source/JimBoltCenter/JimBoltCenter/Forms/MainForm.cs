using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using JimBoltCenter.Utils;
using JimBoltCenter.UI_Controls.Viewers;
using JimBoltCenter.UI_Controls.Transactions;
using JimBoltCenter.UI_Controls.Maintenance;
using JimBoltCenter.UI_Controls.UserAccount;
using DevExpress.XtraTab.ViewInfo;
using System.Threading;
using System.Configuration;
using DBMapping.DAL;
using DBMapping.BOL;

namespace JimBoltCenter.Forms
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        private const string DEFAULT_TITLE = "BOLT CENTER";
        private BoltCenterSplash splash = null;
        private User_PrivilegesDao privDao = null;
        private bool bConnectORM;
        private UserControl uctl_view { get; set; }
        private GridControl gridControl { get; set; }
        public User logged_user;
        private string _versionInfo = ConfigurationManager.AppSettings.Get("versionInfo");

        public static MainForm m_FormInstance = null;

        public MainForm()
        {
            InitializeComponent();
            bConnectORM = false;
            //splash = new BoltCenterSplash();
            ScaleToBigFont();
            m_FormInstance = this;
            logged_user = null;
            privDao = new User_PrivilegesDao();

            //get the latest version
        }

        private void ScaleToBigFont()
        {
            foreach (dynamic item in navBarMainMenu.Items)
            {    
                item.Appearance.Font = new Font("Tahoma", 12);
                item.AppearanceDisabled.Font = new Font("Tahoma", 12);
                item.AppearanceHotTracked.Font = new Font("Tahoma", 12);
                item.AppearancePressed.Font = new Font("Tahoma", 12);
                
                foreach (dynamic subItem in item.Collection)
                {
                    subItem.Appearance.Font = new Font("Tahoma", 12);
                    subItem.AppearanceDisabled.Font = new Font("Tahoma", 12);
                    subItem.AppearanceHotTracked.Font = new Font("Tahoma", 12);
                    subItem.AppearancePressed.Font = new Font("Tahoma", 12);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabWindow.TabPages.Clear();
            lblTime.Text = string.Format("{0}  v{1}", DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"), _versionInfo);

            lblTitle.Text = DEFAULT_TITLE;
            lblSubTitle.Text = "";

            //hide other menus while not logged in.
            ShowOrHideMenus(false);

            //Login
            UserLogin();
        }

        private void UserLogin()
        {
            //user relevat code
            uctlLogin uctl = new uctlLogin();
            frmGenericPopup form = new frmGenericPopup();

            form.Text = "LogIn";
            form.ShowCtl(uctl);

            if (logged_user != null)
            {
                ApplyingPrivileges();
                /*if (logged_user.authentication == 3)//admin
                {
                    navBarUsers_ChangePass.Visible = false;
                    navBarUsers_Manage.Visible = true;
                }*/

                navBarUsers_Login.Caption = "Log Out";
                this.Text = string.Format("Bolt Center   [{0} - {1}, {2} / {3}]", 
                    logged_user.username,logged_user.lname, logged_user.fname, GetAuthentication(logged_user.authentication));
            }
        }

        private void ApplyingPrivileges()
        {
            int nViewers = 0, nTrx = 0;
            User_Privileges priv = new User_Privileges();
            priv = privDao.GetPrivileges(logged_user.authentication);

            //Inventory
            if (priv.Inventory_View)
            {
                navBarViewers_Inventory.Visible = true;
                nViewers += 1;
            }
            else
                navBarViewers_Inventory.Visible = false;

            //receipts
            if (priv.Receipt_Add)
            {
                navBarTransactions_IssueReceipt.Visible = true;
                nTrx += 1;
            }
            else
                navBarTransactions_IssueReceipt.Visible = false;

            if (priv.Receipt_View)
            {
                navBarViewers_Receipt.Visible = true;
                nViewers += 1;
            }
            else
                navBarViewers_Receipt.Visible = false;

            //invoices
            if (priv.Invoice_Add)
            {
                navBarTransactions_Invoice_Stockin.Visible = true;
                nTrx += 1;
            }
            else
                navBarTransactions_Invoice_Stockin.Visible = false;

            if (priv.Invoice_View)
            {
                navBarViewers_Invoice_Stockin.Visible = true;
                nViewers += 1;
            }
            else
                navBarViewers_Invoice_Stockin.Visible = false;

            //contacts
            if (priv.Contacts)
            {
                navBarViewers_Contacts.Visible = true;
                nViewers += 1;
            }
            else
                navBarViewers_Contacts.Visible = false;


            //groupees
            if (nViewers > 0)
                navBarViewers.Visible = true;
            else
                navBarViewers.Visible = false;

            if (nTrx > 0)
                navBarTransactions.Visible = true;
            else
                navBarTransactions.Visible = false;

            //specials for each role
            if (logged_user.authentication == 3)//admin
            {
                navBarMaintenance.Visible = true;                
                navBarUsers_Manage.Visible = true;
                navBarUsers_ChangePass.Visible = false;
            }
            else if (logged_user.authentication == 0 || logged_user.authentication ==1)//sales
            {
                navBarMaintenance.Visible = false;
                navBarUsers_Manage.Visible = false;
                navBarUsers_ChangePass.Visible = true;
            }
            
        }

        private string GetAuthentication(int nType)
        {
            string sRet = "";
            switch (nType)
            {
                case 0://sales
                    sRet = "Sales";
                    break;
                case 1:
                    sRet = "Cashier";
                    break;
                case 2:
                    sRet = "Maintenance";
                    break;
                case 3:
                    sRet = "Administrator";
                    break;
                default:
                    break;
            }

            return sRet;
        }

        private void ShowOrHideMenus(bool bValue)
        {
            navBarTransactions.Visible = bValue;
            navBarViewers.Visible = bValue;
            navBarMaintenance.Visible = bValue;

            //always hide user related, only show after login successfull
            navBarUsers_Manage.Visible = false;
            navBarUsers_ChangePass.Visible = false;
        }

        public void navBarViewers_Contacts_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlContact uctl = new uctlContact();
            formUtils.AddToTabbedPage(tabWindow, "tbpgViewContact", "Contacts", DockStyle.Fill, uctl,"Contacts","Add/Edit/Delete contacts");

            lblTitle.Text = "Contacts";
            lblSubTitle.Text = "Add, Edit & Delete contacts";
        }

        private void tabWindow_CloseButtonClick(object sender, EventArgs e)
        {
            XtraTabControl xtab = sender as XtraTabControl;
            ClosePageButtonEventArgs args = e as ClosePageButtonEventArgs;
            XtraTabPage page = args.Page as XtraTabPage;
            int nIndex = xtab.SelectedTabPageIndex;

            page.Controls.Clear();
            if ((nIndex - 1) > 0)
            {
                tabWindow.SelectedTabPageIndex = nIndex - 1;
            }
            else if (nIndex == 0)
            {
                lblTitle.Text = DEFAULT_TITLE;
                lblSubTitle.Text = "";
            }

            xtab.TabPages.Remove(page);
        }

        public void CloseCurrentTabPage()
        {
            if (tabWindow.SelectedTabPage != null)
            {
                int newIndex = tabWindow.SelectedTabPageIndex;
                XtraTabPage currentPage = tabWindow.SelectedTabPage;

                if (tabWindow.TabPages.Count == 1)
                {
                    lblTitle.Text = DEFAULT_TITLE;
                    lblSubTitle.Text = "";
                    //tabWindow.SelectedTabPageIndex = newIndex;
                }
                else
                {
                    if ((newIndex - 1) >= 0)
                    {
                        tabWindow.SelectedTabPageIndex = newIndex - 1;
                    }
                }
                tabWindow.TabPages.Remove(currentPage);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = string.Format("{0}  v{1}", DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"), _versionInfo);
        }

        private void tabWindow_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            XtraTabPage thisPage = tabWindow.SelectedTabPage;
            if (thisPage != null)
            {
                lblTitle.Text = thisPage.TooltipTitle;
                lblSubTitle.Text = thisPage.Tooltip;
            }
        }

        private void navBarViewers_Tools_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlTools uctl = new uctlTools();
            formUtils.AddToTabbedPage(tabWindow, "tbpgViewTools", "Tools", DockStyle.Left, uctl, "Tools", "manage forwarders and invoice types here.");

            lblTitle.Text = "Tools";
            lblSubTitle.Text = "manage forwarders and invoice types here.";
        }

        private void navBarViewers_Inventory_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlViewInventory uctl = new uctlViewInventory();
            formUtils.AddToTabbedPage(tabWindow, "tbpgViewInventory", "View Inventory", DockStyle.Fill, uctl, "View Inventory", "view items");

            lblTitle.Text = "View Inventory";
            lblSubTitle.Text = "view items";
        }

        public void navBarViewers_Invoice_Stockin_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlViewInvoice uctl = new uctlViewInvoice();
            formUtils.AddToTabbedPage(tabWindow, "tbpgViewStockin", "View StockIn", DockStyle.Fill, uctl, "View StockIn", "view or delete existing stockin invoices");

            lblTitle.Text = "View StockIn Invoices";
            lblSubTitle.Text = "view or delete existing stockin invoices";
        }

        public void navBarTransactions_Invoice_Stockin_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlInvoice uctl = new uctlInvoice();
            formUtils.AddToTabbedPage(tabWindow, "tbpgNewStockin", "New StockIn", DockStyle.Fill, uctl, "New StockIn", "creates new stockin invoice");

            lblTitle.Text = "New StockIn Invoice";
            lblSubTitle.Text = "creates new stockin invoice";
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //splash.ShowSplashWindow();
            //this.Enabled = false;
            //timer2.Enabled = true;
            //timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!bConnectORM)
            {
                splash.SetStatus("Connecting to Database...");
                bConnectORM = true;
                ForwardersDao fdao = new ForwardersDao();

                splash.SetStatus("Connected!");
                timer2.Stop();
                timer2.Enabled = false;

                splash.Close();
                this.Enabled = true;
            }
        }

        public void navBarTransactions_IssueReceipt_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlIssueReceipt uctl = new uctlIssueReceipt();
            formUtils.AddToTabbedPage(tabWindow, "tbpgIssueReceipt", "New Receipt", DockStyle.Fill, uctl, "New Receipt", "issue receipt to customers");

            lblTitle.Text = "New Receipt";
            lblSubTitle.Text = "issue receipt to customers";
        }

        private void navBarViewers_Receipt_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlViewReceipts uctl = new uctlViewReceipts();
            formUtils.AddToTabbedPage(tabWindow, "tbpgViewReceipt", "View Receipt", DockStyle.Fill, uctl, "View Receipt", "view or delete existing receipts");

            lblTitle.Text = "View Receipt";
            lblSubTitle.Text = "view or delete existing receipts";
        }

        private void navBarMaintenance_Forwarders_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlManageForwarders uctl = new uctlManageForwarders();
            formUtils.AddToTabbedPage(tabWindow, "tbpgManageForwarders", "Manage Forwarders", DockStyle.Left, uctl, "Manage Forwarders", "add/edit/delete forwarders");

            lblTitle.Text = "Manage Forwarders";
            lblSubTitle.Text = "add/edit/delete forwarders";
        }

        private void navBarMaintenance_Sizes_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlManageSizes uctl = new uctlManageSizes();
            formUtils.AddToTabbedPage(tabWindow, "tbpgManageSizes", "Manage Item Sizes", DockStyle.Left, uctl, "Manage Item Sizes", "add/edit/delete item sizes");

            lblTitle.Text = "Manage Item Sizes";
            lblSubTitle.Text = "add/edit/delete item sizes";
        }

        private void navBarMaintenance_InvoiceType_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlManageInvoiceType uctl = new uctlManageInvoiceType();
            formUtils.AddToTabbedPage(tabWindow, "tbpgManageInvoiceTypes", "Manage Invoice Types", DockStyle.Left, uctl, "Manage Invoice Types", "add/edit/delete invoice types");

            lblTitle.Text = "Manage Invoice Types";
            lblSubTitle.Text = "add/edit/delete invoice types";
        }
        
        private void navBarMaintenance_ItemWeight_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlManageWeight uctl = new uctlManageWeight();
            formUtils.AddToTabbedPage(tabWindow, "tbpgManageItemWeight", "Manage Item Weights", DockStyle.Left, uctl, "Manage Item Weights", "add/edit/delete item weights");

            lblTitle.Text = "Manage Item Weights";
            lblSubTitle.Text = "add/edit/delete item weights";
        }

        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            tabWindow.TabPages.Clear();
            lblTitle.Text = "BOLT CENTER";
            lblSubTitle.Text = "";
        }

        private void navBarUsers_Manage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlManageUsers uctl = new uctlManageUsers();
            formUtils.AddToTabbedPage(tabWindow, "tbpgManageUsers", "Manage Users", DockStyle.Left, uctl, "Manage Users", "add/edit/delete users");

            lblTitle.Text = "Manage Users";
            lblSubTitle.Text = "add/edit/delete users";
        }

        private void navBarUsers_Login_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            if (navBarUsers_Login.Caption == "Log In")
                UserLogin();
            else
            {
                btnCloseAll.PerformClick();
                logged_user = null;
                ShowOrHideMenus(false);
                navBarUsers_Login.Caption = "Log In";
                this.Text = "Bolt Center";
                //Thread.Sleep(2000);
                UserLogin();
            }
        }

        private void navBarTransactions_Retail_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlRetailInvoice uctl = new uctlRetailInvoice();
            formUtils.AddToTabbedPage(tabWindow, "tbpgRetail", "Retail Invoice", DockStyle.Left, uctl, "Retail Invoice", "add/edit retail invoice");

            lblTitle.Text = "Retail Invoice";
            lblSubTitle.Text = "add/edit retail invoice";
        }

        private void navBarViewers_Series_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            uctlViewRetailSeries uctl = new uctlViewRetailSeries();
            formUtils.AddToTabbedPage(tabWindow, "tbpgRetailSeries", "Retail Series", DockStyle.Left, uctl, "Retail Series", "view list of series");

            lblTitle.Text = "Retail Series";
            lblSubTitle.Text = "view list of series";
        }      

    }
}