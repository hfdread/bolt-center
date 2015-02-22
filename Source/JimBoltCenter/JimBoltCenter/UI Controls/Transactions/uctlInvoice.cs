using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;
using JimBoltCenter.Forms;
using JimBoltCenter.UI_Controls.Maintenance;
using JimBoltCenter.UI_Controls.UserAccount;
using JimBoltCenter.Reports;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;


namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlInvoice : UserControl
    {
        private ContactsDao mContactDao = null;
        private InvoiceTypeDao mInvoiceTypeDao = null;
        private ForwardersDao mForwadersDao = null;
        private SalesInvoiceDao mInvoiceDao = null;
        private ItemDao mItemDao = null;
        private User_PrivilegesDao privDao = null;
        private SalesInvoice m_InvoiceCopy;

        //public SalesInvoiceDetails updated_details { get; set; }
        public SalesInvoice m_salesinvoice = null;
        public SalesInvoice m_ViewInvoice { get; set; }
        public bool bEditInvoice;

        private BindingSource bSrc = null;
        private DataTable tblData = null;
        private DataSet dsData = null;

        private bool bIsloading;

        private enum eCol
        {
            QTY=0,
            itemdesc,
            itemSizeDesc,
            itemUnit,
            Price,
            Discount,
            discountPrice,
            unitTotal
        }
        
        public uctlInvoice()
        {
            InitializeComponent();
            mContactDao = new ContactsDao();
            mInvoiceTypeDao = new InvoiceTypeDao();
            mForwadersDao = new ForwardersDao();
            mInvoiceDao = new SalesInvoiceDao();
            mItemDao = new ItemDao();
            bIsloading = true;
            privDao = new User_PrivilegesDao();
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvSalesItem.Columns["QTY"].OptionsColumn.AllowEdit = bEdit;
            grdvSalesItem.Columns["QTY"].OptionsColumn.AllowFocus = bEdit;

            grdvSalesItem.Columns["itemdesc"].OptionsColumn.AllowEdit = bEdit;
            grdvSalesItem.Columns["itemdesc"].OptionsColumn.AllowFocus = bEdit;

            grdvSalesItem.Columns["itemSizeDesc"].OptionsColumn.AllowEdit = bEdit;
            grdvSalesItem.Columns["itemSizeDesc"].OptionsColumn.AllowFocus = bEdit;

            grdvSalesItem.Columns["Price"].OptionsColumn.AllowEdit = bEdit;
            grdvSalesItem.Columns["Price"].OptionsColumn.AllowFocus = bEdit;

            grdvSalesItem.Columns["Discount"].OptionsColumn.AllowEdit = bEdit;
            grdvSalesItem.Columns["Discount"].OptionsColumn.AllowFocus = bEdit;

            grdvSalesItem.Columns["itemUnit"].OptionsColumn.AllowEdit = bEdit;
            grdvSalesItem.Columns["itemUnit"].OptionsColumn.AllowFocus = bEdit;
        }

        private void uctlInvoice_Load(object sender, EventArgs e)
        {
            bIsloading = false;
            Skin.SetGridSelectionColor(148, 183, 224, grdvSalesItem);
            Skin.SetButtonFont(btnEdit);
            Skin.SetButtonFont(btnPrint);

            LoadLists();
            if (m_ViewInvoice != null)//view
            {
                m_InvoiceCopy = new SalesInvoice();
                m_InvoiceCopy = m_ViewInvoice;
                m_InvoiceCopy.details = m_ViewInvoice.details;


                EditDataSource();
                cboForwarders.SelectedItem = mForwadersDao.GetById(m_ViewInvoice.ForwarderID);
                cboInvoiceType.SelectedItem = mInvoiceDao.GetById(m_ViewInvoice.InvoiceType);

                txtViewSupplier.Visible = true;
                lookupSupplier.Visible = false;
                txtViewSupplier.Location = new Point(73, 8);
                txtViewSupplier.Text = m_ViewInvoice.Supplier.ToString();

                dteInvoice.DateTime = m_ViewInvoice.InvoiceDate;
                dteArrived.DateTime = m_ViewInvoice.ArrivalDate;

                txtInvoiceNo.Text = m_ViewInvoice.InvoiceID.ToString();
                txtARNo.Text = m_ViewInvoice.AR_Number.ToString();
                txtCart.Text = m_ViewInvoice.QTY_Cart.ToString();
                txtFreightAmt.Text = m_ViewInvoice.FreightAmount.ToString(cUtils.AMOUNT_FMT);
                txtTin.Text = m_ViewInvoice.TIN;
                txtFor.Text = m_ViewInvoice.STORE;

                lblTotalAmount.Text = m_ViewInvoice.Invoice_Amount.ToString(cUtils.AMOUNT_FMT);

                foreach (SalesInvoiceDetails details in m_ViewInvoice.details)
                {
                    if (details.item.Unit.Trim() == "")
                    {
                        details.itemUnit = details.item.Unit2;
                    }
                    else
                    {
                        details.itemUnit = details.item.Unit;
                    }

                    details.itemdesc = details.item.Name;
                    if (details.item.Size != null)
                        details.itemSizeDesc = details.item.Size.Description;
                    else
                        details.itemSizeDesc = "";

                    details.discountPrice = cUtils.GetDiscountedPrices(details.Discount, details.Price);
                    double unitTotal = details.QTY * cUtils.GetLastDiscountPrice(details.Discount, details.Price);
                    details.unitTotal = unitTotal.ToString(cUtils.FMT_CURRENCY_AMT);
                }

                grdSalesItem.DataSource = m_ViewInvoice.details;
                RefreshDataSource();

                btnAddItem.Enabled = false;
                if (bEditInvoice)
                {
                    btnSaveInvoice.Enabled = true;
                    btnEdit.Visible = true;
                }
                else
                {
                    btnSaveInvoice.Enabled = false;
                    btnEdit.Visible = false;
                }

                btnRemoveItem.Enabled = false;
                btnQuickAddInvoiceType.Visible = false;
                btQuickAddSupplier.Visible = false;
                btnQuickForwarders.Visible = false;
                btnPrint.Visible = true;
                txtFreightAmt.Focus();
            }
            else
            {
                //set date fields
                dteArrived.DateTime = DateTime.Now;
                dteInvoice.DateTime = DateTime.Now;

                bSrc = new BindingSource();
                tblData = new DataTable();
                dsData = new DataSet();

                //columns
                DataColumn dtCol1 = new DataColumn();
                DataColumn dtCol2 = new DataColumn();
                DataColumn dtCol3 = new DataColumn();
                DataColumn dtCol4 = new DataColumn();
                DataColumn dtCol5 = new DataColumn();
                DataColumn dtCol6 = new DataColumn();
                DataColumn dtCol7 = new DataColumn();
                DataColumn dtCol8 = new DataColumn();

                dtCol1.ColumnName = "QTY";
                dtCol2.ColumnName = "itemUnit";
                dtCol3.ColumnName = "itemdesc";
                dtCol4.ColumnName = "itemSizeDesc";
                dtCol5.ColumnName = "Price";
                dtCol6.ColumnName = "Discount";
                dtCol7.ColumnName = "discountPrice";
                dtCol8.ColumnName = "unitTotal";

                dsData.DataSetName = "NewDataSet";
                tblData.TableName = "tblData";

                dsData.Tables.AddRange(new System.Data.DataTable[] { tblData });

                tblData.Columns.AddRange(new System.Data.DataColumn[] { 
                    dtCol1,
                    dtCol3,
                    dtCol4,
                    dtCol2,
                    dtCol5,
                    dtCol6,
                    dtCol7,
                    dtCol8
                });

                bSrc.DataMember = "tblData";
                bSrc.DataSource = dsData;
                grdvSalesItem.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                grdSalesItem.DataSource = bSrc;

                lookupSupplier.Focus();
            }
        }

        private void LoadLists()
        {
            //supplier list
            lookupSupplier.Properties.DataSource = mContactDao.GetContactList(1);

            //invoicetype list
            cboInvoiceType.Properties.Items.Clear();
            IList<InvoiceType> invoicetypeList = mInvoiceTypeDao.getAllRecords();
            formUtils.LoadComboBoxEdit<InvoiceType>(cboInvoiceType, invoicetypeList, true);
            cboInvoiceType.SelectedIndex = 0;

            //forwarders list
            cboForwarders.Properties.Items.Clear();
            IList<Forwarders> forwarderList = mForwadersDao.getAllRecords();
            formUtils.LoadComboBoxEdit<Forwarders>(cboForwarders, forwarderList, true);
            cboForwarders.SelectedIndex = 0;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            uctlAddItem_Invoice ctl = new uctlAddItem_Invoice();
            frmGenericPopup frm = new frmGenericPopup();

            if (m_salesinvoice == null)
            {
                m_salesinvoice = new SalesInvoice();
                grdSalesItem.DataSource = m_salesinvoice.details;
            }

            ctl.ParentCtl = this;
            frm.Text = "Select Item";
            frm.ShowCtl(ctl);
        }

        public void AddItem(SalesInvoiceDetails newDetails)
        {
            
            m_salesinvoice.details.Add(newDetails);
            RefreshDataSource();

            //ComputeTotal();
        }

        private void RefreshDataSource()
        {
            grdvSalesItem.RefreshData();
            grdvSalesItem.FocusedRowHandle = grdvSalesItem.RowCount - 1;
        }

        //private void grdvSalesItem_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        //{
        //    if (bIsloading)
        //        return;

        //    //SalesInvoiceDetails details = (SalesInvoiceDetails)grdvSalesItem.GetRow(e.ListSourceRowIndex);

        //    //if (e.IsGetData && e.Column.FieldName == "desc")
        //    //{
        //    //    e.Value = details.item.Name + ", " + details.item.Description + "/" + details.item.Code;
        //    //}
        //    //else if (e.IsGetData && e.Column.FieldName == "discountPrice")
        //    //{
        //    //    e.Value = cUtils.GetDiscountedPrices(details.Discount, details.Price);
        //    //}
        //    //else if (e.IsGetData && e.Column.FieldName == "unitTotal")
        //    //{
        //    //    e.Value = details.QTY * cUtils.GetLastDiscountPrice(details.Discount, details.Price);
        //    //}
        //}

        public void ComputeTotal()
        {
            try
            {
                double subTotal = 0;
                if (m_ViewInvoice != null)//view
                {
                    foreach (SalesInvoiceDetails sid in m_ViewInvoice.details)
                    {
                        subTotal += sid.QTY * sid.Price;
                    }
                }
                else
                {
                    foreach (DataRow row in tblData.Rows)
                    {
                        subTotal += cUtils.ConvertToDouble(row[(int)eCol.unitTotal].ToString());
                    }
                }
                lblTotalAmount.Text = subTotal.ToString(cUtils.FMT_CURRENCY_AMT);
            }
            catch(Exception ex)
            {}
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
           
            if (m_ViewInvoice != null) //edit invoice data for freight
            {
                try
                {
                    bool bAuthenticate = true;
                    User_Privileges priv = privDao.GetPrivileges(MainForm.m_FormInstance.logged_user.authentication);

                    if (!priv.Invoice_Edit)
                    {
                        uctlEditPass uctl = new uctlEditPass();
                        frmGenericPopup frm = new frmGenericPopup();

                        frm.Text = "Authenticate";
                        frm.ShowCtl(uctl);

                        bAuthenticate = uctl.bAuthenticate;
                    }

                    if (bAuthenticate)
                    {
                        m_ViewInvoice.FreightAmount = cUtils.ConvertToDouble(txtFreightAmt.Text);
                        m_ViewInvoice.UpdateDate = DateTime.Now;
                        m_ViewInvoice.User = MainForm.m_FormInstance.logged_user.ID;
                        m_ViewInvoice.EditedDate = m_ViewInvoice.UpdateDate;
                        m_ViewInvoice.QTY_Cart = cUtils.ConvertToInteger(txtCart.Text);
                        m_ViewInvoice.AR_Number = cUtils.ConvertToInteger(txtARNo.Text);

                        //+issue #75
                        m_ViewInvoice.TIN = txtTin.Text;
                        m_ViewInvoice.STORE = txtFor.Text;
                        //-issue #75


                        mInvoiceDao.SaveEditedInvoice(m_ViewInvoice);
                    
                        cUtils.ShowMessageInformation("Invoice Successfully Saved!", "Save Invoice");
                        cUtils.getMainForm().CloseCurrentTabPage();
                    }
                }
                catch (Exception ex)
                {
                    string error = "";
                    if (ex.InnerException != null)
                        error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Edit Invoice");
                    cUtils.CreateCrashLog(error, "Edit Invoice", "Stock In Invoice");
                }
            }
            else //new invoice
            {
                ComputeTotal();
                if (tblData.Rows.Count > 0)
                {
                    m_salesinvoice = new SalesInvoice();
                    
                    if (txtInvoiceNo.Text.Trim().Length > 0)
                        m_salesinvoice.InvoiceID = Convert.ToInt32(txtInvoiceNo.Text.Trim());
                    else
                    {
                        cUtils.ShowMessageExclamation("Please provide an Invoice No.", "Save Invoice");
                        txtInvoiceNo.Focus();
                        return;
                    }

                    Contacts c = gridLookUpEdit1View.GetRow(gridLookUpEdit1View.FocusedRowHandle) as Contacts;
                    if (c != null)
                        m_salesinvoice.Supplier = c;
                    else
                    {
                        cUtils.ShowMessageExclamation("Please select a Supplier", "Save Invoice");
                        gridLookUpEdit1View.Focus();
                        return;
                    }

                    InvoiceType inv = cboInvoiceType.SelectedItem as InvoiceType;
                    if (inv == null)
                    {
                        cUtils.ShowMessageExclamation("Please provide an Invoice Type", "Save Invoice");
                        cboInvoiceType.Focus();
                        return;
                    }

                    Forwarders fw = cboForwarders.SelectedItem as Forwarders;
                    if (fw == null)
                    {
                        cUtils.ShowMessageExclamation("Please provide a Forwarder", "Save Invoice");
                        cboForwarders.Focus();
                        return;
                    }

                    m_salesinvoice.InvoiceType = mInvoiceTypeDao.GetSelectedInvoiceType(inv);
                    m_salesinvoice.ForwarderID = mForwadersDao.GetSelectedForwarder(fw);
                    m_salesinvoice.InvoiceDate = dteInvoice.DateTime;
                    m_salesinvoice.CreateDate = DateTime.Now;
                    m_salesinvoice.UpdateDate = DateTime.Now;
                    m_salesinvoice.ArrivalDate = dteArrived.DateTime;

                    if (txtFreightAmt.Text.Trim().Length > 0)
                        m_salesinvoice.FreightAmount = cUtils.ConvertToDouble(txtFreightAmt.Text.Trim());
                    else
                        m_salesinvoice.FreightAmount = 0;

                    if (txtARNo.Text.Trim().Length > 0)
                        m_salesinvoice.AR_Number = cUtils.ConvertToInteger(txtARNo.Text.Trim());
                    else
                        m_salesinvoice.AR_Number = 0;

                    if (txtCart.Text.Trim().Length > 0)
                        m_salesinvoice.QTY_Cart = cUtils.ConvertToInteger(txtCart.Text.Trim());
                    else
                        m_salesinvoice.QTY_Cart = 0;


                    m_salesinvoice.Invoice_Amount = cUtils.ConvertToDouble(lblTotalAmount.Text.Trim());
                    
                    //+issue #75
                    m_salesinvoice.TIN = txtTin.Text;
                    m_salesinvoice.STORE = txtFor.Text;
                    //-issue #75

                    m_salesinvoice.Deleted = false;

                    //manually add each row to details
                    foreach (DataRow row in tblData.Rows)
                    {
                        SalesInvoiceDetails sidetails = new SalesInvoiceDetails();

                        sidetails.QTY = cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString());
                        sidetails.Price = cUtils.ConvertToDouble(row[(int)eCol.Price].ToString());
                        sidetails.Discount = row[(int)eCol.Discount].ToString();
                        sidetails.item = mItemDao.GetItem(row[(int)eCol.itemdesc].ToString().Trim(),
                                                            row[(int)eCol.itemSizeDesc].ToString().Trim(),
                                                            row[(int)eCol.itemUnit].ToString().Trim());

                        if (sidetails.item == null)//check if item has no size
                        {
                            sidetails.item = mItemDao.CheckItemNoSize(row[(int)eCol.itemdesc].ToString().Trim(), row[(int)eCol.itemUnit].ToString().Trim());
                        }

                        m_salesinvoice.details.Add(sidetails);
                    }

                    try
                    {
                        mInvoiceDao.SaveInvoice(m_salesinvoice);
                        cUtils.ShowMessageInformation("Invoice Successfully Saved!", "Save Invoice");
                        cUtils.getMainForm().CloseCurrentTabPage();
                        cUtils.getMainForm().navBarTransactions_Invoice_Stockin_LinkClicked(null, null);
                    }
                    catch (Exception ex)
                    {
                        string error = "";
                        if (ex.InnerException != null)
                            error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                        else
                            error = string.Format("Error:\n{0}", ex.Message);

                        cUtils.ShowMessageError(error, "Save Invoice");
                        cUtils.CreateCrashLog(error, "Save Invoice", "Stock In Invoice");
                    }
                }
                else
                {
                    cUtils.ShowMessageInformation("No Invoice Data to save!", "Save Invoice");
                }
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (m_salesinvoice != null)
            {
                SalesInvoiceDetails details = (SalesInvoiceDetails) grdvSalesItem.GetRow(grdvSalesItem.FocusedRowHandle);

                m_salesinvoice.details.Remove(details);
                RefreshDataSource();

               // ComputeTotal();
            }
        }

        private void gridLookUpEdit1View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            Contacts c = gridLookUpEdit1View.GetRow(e.ListSourceRowIndex) as Contacts;
            if (c != null)
            {
                if (e.IsGetData && e.Column.FieldName == "name")
                    e.Value = string.Format("{0} {1} {2}", c.FirstName, c.MiddleName, c.LastName);
            }
        }

        private void btQuickAddSupplier_Click(object sender, EventArgs e)
        {
            Contacts c = new Contacts();
            c.Type = 1;//for agent 3

            uctlQuickAddContact uctl = new uctlQuickAddContact();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Supplier Registration";
            uctl.m_Contact = c;
            frm.ShowCtl(uctl);

            lookupSupplier.Properties.DataSource = mContactDao.GetContactList(1);
        }

        private void btnQuickAddInvoiceType_Click(object sender, EventArgs e)
        {
            uctlQuickAddInvoiceTypes uctl = new uctlQuickAddInvoiceTypes();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Add New Invoice Type";
            frm.ShowCtl(uctl);

            //invoicetype list
            cboInvoiceType.Properties.Items.Clear();
            IList<InvoiceType> invoicetypeList = mInvoiceTypeDao.getAllRecords();
            formUtils.LoadComboBoxEdit<InvoiceType>(cboInvoiceType, invoicetypeList, true);
            cboInvoiceType.SelectedIndex = 0;
        }

        private void btnQuickForwarders_Click(object sender, EventArgs e)
        {
            uctlQuickAddForwarder uctl = new uctlQuickAddForwarder();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Add Forwarder";
            frm.ShowCtl(uctl);

            //forwarders list
            cboForwarders.Properties.Items.Clear();
            IList<Forwarders> forwarderList = mForwadersDao.getAllRecords();
            formUtils.LoadComboBoxEdit<Forwarders>(cboForwarders, forwarderList, true);
            cboForwarders.SelectedIndex = 0;
        }

        private void grdvSalesItem_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bIsloading)
                return;

            bool bChange = false;
            string msgErr = "";
            try//try to catch errors
            {
                if (e.Column.FieldName == "itemdesc")
                {
                    msgErr = "Item Name is invalid!";
                    bChange = true;
                    grdvSalesItem.SetRowCellValue(e.RowHandle, "itemSizeDesc", "");
                    repoCbo.Items.Clear();
                    try
                    {
                        //string qty = grdvSalesItem.GetFocusedRowCellValue("QTY").ToString();
                        string itemName = "";
                        itemName = grdvSalesItem.GetFocusedRowCellValue("itemdesc").ToString();
                        IList<ItemSizes> sizeList = mItemDao.GetSizes(itemName);

                        if (sizeList[0] != null)
                        {
                            IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);

                            foreach (SortedSize size in sortedSizeList)
                            {
                                repoCbo.Items.Add(size.Description);
                            }
                        }

                        //foreach (ItemSizes size in sizeList)
                        //{
                        //    repoCbo.Items.Add(size.Description);
                        //}
                    }
                    catch (Exception ex)
                    {
                        cUtils.ShowMessageError(msgErr, "Invoice Add Item");
                        cUtils.CreateCrashLog(ex.Message, "Invoice Add Item", "Stock In Invoice");
                    }
                }
                else if (e.Column.FieldName == "itemUnit")
                {
                    //get the item
                    string itemsize, itemname, itemunit;
                    msgErr = "Unit is invalid!";
                    itemsize = grdvSalesItem.GetFocusedRowCellValue("itemSizeDesc").ToString().Trim();
                    itemname = grdvSalesItem.GetFocusedRowCellValue("itemdesc").ToString().Trim();
                    itemunit = grdvSalesItem.GetFocusedRowCellValue("itemUnit").ToString().Trim();

                    Item I = mItemDao.GetItem(itemname, itemsize,itemunit);

                    if (itemsize.Trim() == "")//try checking if item has no size
                    {
                        I = mItemDao.CheckItemNoSize(itemname, itemunit);
                    }

                    if (I == null)
                        return;


                    if (I.Unit2.Trim() == "")//non-weight item
                    {
                        grdvSalesItem.SetRowCellValue(e.RowHandle, "Price", I.UnitPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                    }
                    else//weight item
                    {
                        grdvSalesItem.SetRowCellValue(e.RowHandle, "Price", I.UnitPrice2.ToString(cUtils.FMT_CURRENCY_AMT));
                    }
                }
                else if (e.Column.FieldName == "itemSizeDesc")
                {
                    if (bChange)
                        return;

                    string itemsize = grdvSalesItem.GetFocusedRowCellValue("itemSizeDesc").ToString().Trim();
                    string itemname = grdvSalesItem.GetFocusedRowCellValue("itemdesc").ToString().Trim();

                   
                    IList<string> strList = mItemDao.GetUnitList(itemname, itemsize);

                    repoCboUnit.Items.Clear();

                    if (strList.Count > 0)
                    {
                        foreach (string str in strList)
                        {
                            repoCboUnit.Items.Add(str);
                        }
                    }
                }
                else if (e.Column.FieldName == "Discount" || e.Column.FieldName == "Price")
                {
                    string discount = "";
                    discount = grdvSalesItem.GetFocusedRowCellValue("Discount").ToString().Trim();
                    //if (discount != "")
                    //{
                    //discounted prices
                    //cUtils.GetDiscountedPrices(details.Discount, details.Price);
                    string discountedPrices = cUtils.GetDiscountedPrices(discount,
                                        cUtils.ConvertToDouble(grdvSalesItem.GetFocusedRowCellValue("Price").ToString()));

                    grdvSalesItem.SetRowCellValue(e.RowHandle, "discountPrice", discountedPrices);
                    //}

                    //unit total
                    //qty * cUtils.GetLastDiscountPrice(details.Discount, details.Price);
                    int qty = cUtils.ConvertToInteger(grdvSalesItem.GetFocusedRowCellValue("QTY").ToString());
                    double unitTotal = qty * cUtils.GetLastDiscountPrice(discount,
                                           cUtils.ConvertToDouble(grdvSalesItem.GetFocusedRowCellValue("Price").ToString()));

                    grdvSalesItem.SetRowCellValue(e.RowHandle, "unitTotal", unitTotal.ToString(cUtils.FMT_CURRENCY_AMT));

                    ComputeTotal();
                }
                else if (e.Column.FieldName == "QTY")
                {
                    int qty = 0;
                    double price = 0;
                    string discountPrice = "", discount = "";
                    qty = cUtils.ConvertToInteger(grdvSalesItem.GetFocusedRowCellValue("QTY").ToString().Trim());
                    price = cUtils.ConvertToDouble(grdvSalesItem.GetFocusedRowCellValue("Price").ToString().Trim());
                    discountPrice = grdvSalesItem.GetFocusedRowCellValue("discountPrice").ToString().Trim();
                    discount = grdvSalesItem.GetFocusedRowCellValue("Discount").ToString().Trim();
                    if (qty > 0)
                    {
                        if (price > 0)
                        {
                            double unitTotal = qty * cUtils.GetLastDiscountPrice(discount, price);

                            grdvSalesItem.SetRowCellValue(e.RowHandle, "unitTotal", unitTotal.ToString(cUtils.FMT_CURRENCY_AMT));

                            ComputeTotal();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //cUtils.ShowMessageError(ex.Message, "Error on Data Input");
                //cUtils.CreateCrashLog(ex.Message, "Error on Data Input", "Stock In Invoice");
            }
        }

        private void rpoCboItemName_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cboEdit = sender as ComboBoxEdit;
            if (cboEdit.Text.Trim().Length > 2)
            {
                IList<string> nameList = new List<string>();
                //nameList = mItemDao.GetItemNameSuggestion(cboEdit.Text.Trim());
                nameList = mItemDao.GetUniqueItemNames(cboEdit.Text.Trim());
                cboEdit.Properties.Items.Clear();//clear first
                foreach (string str in nameList)
                {
                    cboEdit.Properties.Items.Add(str);
                }
            }
        }

        //*********************************************
        // commented this one out, validation will be done on the row. to ease user experience
        //private void grdvSalesItem_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        //{
        //    bool bValid = true;
        //    if (grdvSalesItem.FocusedColumn.FieldName == "QTY")
        //    {
        //        if (cUtils.ConvertToInteger(e.Value) <= 0 || e.Value == null)
        //        {
        //            e.ErrorText = "Invalid QTY";
        //            bValid = false;
        //        }
        //    }
        //    else if (grdvSalesItem.FocusedColumn.FieldName == "itemdesc")
        //    {
        //        if (e.Value.ToString().Trim().Length == 0 || e.Value == null)
        //        {
        //            e.ErrorText = "You must first select an item";
        //            bValid = false;
        //        }
        //    }
        //    else if (grdvSalesItem.FocusedColumn.FieldName == "itemSizeDesc")
        //    {
        //        if (e.Value.ToString().Trim().Length == 0 || e.Value == null)
        //        {
        //            e.ErrorText = "Please select a size";
        //            bValid = false;
        //        }
        //    }
        //    else if (grdvSalesItem.FocusedColumn.FieldName == "Price")
        //    {
        //        if (cUtils.ConvertToDouble(e.Value) <= 0 || e.Value == null)
        //        {
        //            e.ErrorText = "Invalid Price";
        //            bValid = false;
        //        }
        //    }

        //    e.Valid = bValid;
        //} 
        //*********************************************

        private void cmMenu_Opening(object sender, CancelEventArgs e)
        {
            if (grdvSalesItem.FocusedRowHandle < 0)
            {
                mnuDelete.Enabled = false;
            }
            else
            {
                mnuDelete.Enabled = true;
            }
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            tblData.Rows.RemoveAt(grdvSalesItem.GetDataSourceRowIndex(grdvSalesItem.FocusedRowHandle));
            grdSalesItem.RefreshDataSource();
            ComputeTotal();
        }

        private void grdvSalesItem_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int qty = 0;
            string sItemdesc = "", sItemSize = "", itemunit ="";
            double price = 0;
            bool bNoSize = false;

            GridView view = sender as GridView;
            GridColumn grdQty = view.Columns[(int)eCol.QTY];
            GridColumn grdItemName = view.Columns[(int)eCol.itemdesc];
            GridColumn grdSize = view.Columns[(int)eCol.itemSizeDesc];
            GridColumn grdPrice = view.Columns[(int)eCol.Price];
            GridColumn grdUnit = view.Columns[(int)eCol.itemUnit];

            //clear error values first
            view.ClearColumnErrors();

            qty = cUtils.ConvertToInteger(grdvSalesItem.GetFocusedRowCellValue("QTY").ToString());
            sItemdesc = grdvSalesItem.GetFocusedRowCellValue("itemdesc").ToString();
            sItemSize = grdvSalesItem.GetFocusedRowCellValue("itemSizeDesc").ToString();
            price = cUtils.ConvertToDouble(grdvSalesItem.GetFocusedRowCellValue("Price").ToString());
            itemunit = grdvSalesItem.GetFocusedRowCellValue("itemUnit").ToString();

            Item I = mItemDao.GetItem(sItemdesc, sItemSize, itemunit);
            

            if (qty <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdQty,"Invalid Quantity entered.");
                view.FocusedColumn = grdQty;
                view.ShowEditor();
            }
            else if (sItemdesc.Trim().Length <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdItemName, "You must first select an item.");
                view.FocusedColumn = grdItemName;
                view.ShowEditor();
            }
            else if (sItemSize.Trim().Length <= 0 && !bNoSize)//size field is empty and item searched has available sizes
            {
                IList<Item> listItem = mItemDao.GetItemList(sItemdesc);

                if (listItem[0].Size != null)
                {
                    e.Valid = false;
                    view.SetColumnError(grdSize, "Please select a size");
                    view.FocusedColumn = grdSize;
                    view.ShowEditor();
                }
            }
            else if (itemunit.Trim().Length <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdUnit, "Please select a unit");
                view.FocusedColumn = grdUnit;
                view.ShowEditor();
            }
            else if (price <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdPrice, "Invalid Price.");
                view.FocusedColumn = grdPrice;
                view.ShowEditor();
            }
            else if (I == null)
            {
                e.Valid = false;
                view.SetColumnError(grdItemName, "Item does not exist! Either the item name or size does not exist");
                view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                view.FocusedColumn = grdItemName;
                view.ShowEditor();
            }
            else
            {
                e.Valid = true;
            }

            ComputeTotal();
        }

        private void grdvSalesItem_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grdvSalesItem_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (bIsloading)
                return;

            if (m_ViewInvoice == null)
            {
                string val = e.Value as string;

                if (e.Column.FieldName == "Price")
                {
                    double price = cUtils.ConvertToDouble(val);
                    e.DisplayText = string.Format("{0}", price.ToString(cUtils.FMT_CURRENCY_AMT));
                }
                else if (e.Column.FieldName == "QTY")
                {
                    int qty = cUtils.ConvertToInteger(val);
                    e.DisplayText = string.Format("{0}", qty.ToString(cUtils.FMT_NUMBER));
                }
            }
        }

        private void repoTxtEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
                e.Handled = true;
        }

        private void repoPriceEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void repoTxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtCart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtARNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtFreightAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void grdvSalesItem_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            ComputeTotal();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SalesInvoiceDetails sid = grdvSalesItem.GetRow(grdvSalesItem.FocusedRowHandle) as SalesInvoiceDetails;

            if (sid != null)
            {
                uctlEditRowInvoice uctl = new uctlEditRowInvoice();
                frmGenericPopup frm = new frmGenericPopup();

                uctl.m_invoice = m_ViewInvoice;
                uctl.m_invoice.nEdit_DetailsID = sid.ID;

                frm.Text = "Edit Invoice Data";
                frm.ShowCtl(uctl);

                if (!uctl.bCancel)//peroform only when you click OK
                {
                    if (uctl.invoiceDetails.bEdited)//if there are changes
                    {
                        SalesInvoiceDetails e_sid;
                        //IList<SalesInvoiceDetails> oldList = new List<SalesInvoiceDetails>();
                        //oldList =

                        //oldList.Remove
                        e_sid = (from lst in m_ViewInvoice.details
                                 where lst.ID == uctl.invoiceDetails.ID
                                 select lst).SingleOrDefault<SalesInvoiceDetails>();

                        if (e_sid != null)
                        {
                            e_sid.QTY = uctl.invoiceDetails.QTY;
                            e_sid.Price = uctl.invoiceDetails.Price;
                            e_sid.Discount = uctl.invoiceDetails.Discount;
                            e_sid.item = uctl.invoiceDetails.item;
                            e_sid.bEdited = true;

                            e_sid.itemdesc = uctl.invoiceDetails.item.Name;

                            if (uctl.invoiceDetails.item.Unit.Trim() != "")
                                e_sid.itemUnit = uctl.invoiceDetails.item.Unit;
                            else if (uctl.invoiceDetails.item.Unit2.Trim() != "")
                                e_sid.itemUnit = uctl.invoiceDetails.item.Unit2;

                            if (e_sid.item.Size != null)
                                e_sid.itemSizeDesc = uctl.invoiceDetails.item.Size.Description;
                            else
                                e_sid.itemSizeDesc = "";

                            double uTotal = sid.QTY * cUtils.GetLastDiscountPrice(uctl.invoiceDetails.Discount, uctl.invoiceDetails.Price);
                            e_sid.unitTotal = uTotal.ToString(cUtils.FMT_CURRENCY_AMT);
                            e_sid.discountPrice = cUtils.GetDiscountedPrices(uctl.invoiceDetails.Discount, uctl.invoiceDetails.Price);
                        }

                        //m_ViewInvoice.details = oldList;
                    }
                }
            }

            RefreshDataSource();
            ComputeTotal();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptInvoice rpt = new rptInvoice();

            IList<SalesInvoiceDetails> lstData = m_ViewInvoice.details;

            foreach (SalesInvoiceDetails dt in lstData)
            {
                //dt.discountPrice = cUtils.GetDiscountedPrices(dt.Discount, dt.Price);
                dt._SubTotal = dt.QTY * cUtils.GetLastDiscountPrice(dt.Discount, dt.Price);
            }

            rpt.DataSource = lstData;
            rpt.m_invoice = m_ViewInvoice;
            rpt.ShowPreviewDialog();
        }

    }
}