using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Utils;
using JimBoltCenter.UI_Controls.Maintenance;
using JimBoltCenter.Forms;
using DBMapping.BOL;
using DBMapping.DAL;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using JimBoltCenter.Reports;

namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlIssueReceipt : UserControl
    {
        private Contacts m_Customers { get; set; }
        private Contacts m_Agents { get; set; }
        private Receipt m_Receipt { get; set; }
        private ReceiptDao receiptDao = null;
        private ContactsDao contactDao = null;
        private ItemDao mItemDao = null;
        private ItemSizesDao m_sizesDao = null;

        public Receipt m_selectedReceipt { get; set; }
        public bool bEditData { get; set; }

        private BindingSource bSrc = null;
        private DataTable tblData = null;
        private DataSet dsData = null;

        private bool bIsLoading;
        private int nItemCount;
        private int nRow;

        private enum eCol
        {
            RowCount=0,
            ItemIndex,
            itemName,
            itemSize,
            Unit,
            UnitPrice,
            Discount,
            DiscountedPrice,
            QTY,
            SubTotal,
            OrigPrice
        }

        public uctlIssueReceipt()
        {
            InitializeComponent();
            receiptDao = new ReceiptDao();
            contactDao = new ContactsDao();
            mItemDao = new ItemDao();
            m_sizesDao = new ItemSizesDao();
            bIsLoading = true;
        }

        private void uctlIssueReceipt_Load(object sender, EventArgs e)
        {
            bIsLoading = false;
            Skin.SetGridFont(grdvReceiptItems, new Font("Tahoma", 12));
            Skin.SetGridSelectionColor(148, 183, 224, grdvReceiptItems);
            Skin.SetTextBoxReadOnly(txtAddress);
            //Skin.SetTextBoxReadOnly(txtTotalAmount);
            
            RefreshContactList();

            if (m_selectedReceipt != null)
            {
                dte1.DateTime = m_selectedReceipt.ReceiptDate;

                grdReceiptItems.ContextMenuStrip = null;
                EditDataSource();

                cboCustomer.Text = m_selectedReceipt.Customer.ToString();
                if(m_selectedReceipt.Agent != null)
                    cboAgent.Text = m_selectedReceipt.Agent.ToString();

                lblORNumber.Text = m_selectedReceipt.ID.ToString(cUtils.FMT_ID);
                dte1.DateTime = m_selectedReceipt.ReceiptDate;
                txtTotalAmount.Text = m_selectedReceipt.ReceiptAmount.ToString(cUtils.AMOUNT_FMT);
                txtAmountPaid.Text = m_selectedReceipt.PaidAmount.ToString(cUtils.AMOUNT_FMT);
                txtPO.Text = m_selectedReceipt.PO;

                foreach (ReceiptDetails rd in m_selectedReceipt.details)
                {
                    rd.itemName = rd.item.Name;
                    if (rd.item.Size != null)
                        rd.itemSize = rd.item.Size.Description;
                    else
                        rd.itemSize = "";
                    rd.DiscountedPrice = cUtils.GetDiscountedPrices(rd.Discount, rd.UnitPrice);

                    if (rd.item.Unit.Trim() != "")//non-weight
                    {
                        rd._Unit = rd.item.Unit;
                        rd.OrigPrice = rd.item.UnitPrice;
                    }
                    else//weight item
                    {
                        rd._Unit = rd.item.Unit2;
                        rd.OrigPrice = rd.item.UnitPrice2;
                    }
                }

                lblItemCount.Text = string.Format("Item Count: {0}", m_selectedReceipt.ItemCount());

                grdReceiptItems.DataSource = m_selectedReceipt.details;
                //grdvReceiptItems.RefreshData();

                btnAddItem.Enabled = false;
                btnQuickAgent.Visible = false;
                btnQuickCustomer.Visible = false;
                btRemoveItem.Enabled = false;
                btnUndo.Enabled = false;

                if (bEditData)
                {
                    btnSave.Enabled = true;
                    btnEdit.Visible = true;
                    btnAdd.Visible = true;
                    btnPrintPreview.Visible = false;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnPrintPreview.Visible = true;
                    btnEdit.Visible = false;
                    btnAdd.Visible = false;
                }

                //Skin.SetTextBoxReadOnly(txtTotalAmount);
                //Skin.SetTextBoxReadOnly(txtAmountPaid);
            }
            else
            {
                //set date
                dte1.DateTime = DateTime.Now;

                EditDataSource(true);
                grdReceiptItems.ContextMenuStrip = cmMenu;
                //set OR Number
                lblORNumber.Text = receiptDao.GetORNumber().ToString(cUtils.FMT_ID);
                nItemCount = 0;
                lblItemCount.Text = string.Format("Item Count: {0}", nItemCount);

                bSrc = new BindingSource();
                tblData = new DataTable();
                dsData = new DataSet();

                //columns
                DataColumn dtColRow = new DataColumn();
                DataColumn dtCol1 = new DataColumn();
                DataColumn dtCol2 = new DataColumn();
                DataColumn dtCol3 = new DataColumn();
                DataColumn dtCol4 = new DataColumn();
                DataColumn dtCol5 = new DataColumn();
                DataColumn dtCol6 = new DataColumn();
                DataColumn dtCol7 = new DataColumn();
                DataColumn dtCol8 = new DataColumn();
                DataColumn dtCol9 = new DataColumn();
                DataColumn dtCol10 = new DataColumn();

                dtColRow.ColumnName = "grdcolRow";
                dtCol1.ColumnName = "ItemIndex";
                dtCol2.ColumnName = "itemName";
                dtCol3.ColumnName = "itemSize";
                dtCol4.ColumnName = "UnitPrice";
                dtCol5.ColumnName = "Discount";
                dtCol6.ColumnName = "DiscountedPrice";
                dtCol7.ColumnName = "QTY";
                dtCol8.ColumnName = "SubTotal";
                dtCol9.ColumnName = "OrigPrice";
                dtCol10.ColumnName = "_Unit";

                dsData.DataSetName = "NewDataSet";
                tblData.TableName = "tblData";

                dsData.Tables.AddRange(new System.Data.DataTable[] { tblData });

                tblData.Columns.AddRange(new System.Data.DataColumn[] { 
                    dtColRow,
                    dtCol1,
                    dtCol2,
                    dtCol3,
                    dtCol10,
                    dtCol4,
                    dtCol5,
                    dtCol6,
                    dtCol7,
                    dtCol8,
                    dtCol9
                });


                bSrc.DataMember = "tblData";
                bSrc.DataSource = dsData;
                grdvReceiptItems.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                grdReceiptItems.DataSource = bSrc;

                repoTextEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                repoTextEdit.DisplayFormat.FormatString = "c2";

                cboCustomer.Focus();
            }
        }

        private void RefreshContactList()
        {
            cboCustomer.Properties.Items.Clear();
            cboAgent.Properties.Items.Clear();

            IList<Contacts> cList = contactDao.GetContactList(Convert.ToInt32(Contacts.cType.Customer));
            foreach (Contacts C in cList)
            {
                cboCustomer.Properties.Items.Add(C);
            }

            IList<Contacts> aList = contactDao.GetContactList(Convert.ToInt32(Contacts.cType.Agent));
            foreach (Contacts C in aList)
            {
                cboAgent.Properties.Items.Add(C);
            }
        }

        private void btnQuickCustomer_Click(object sender, EventArgs e)
        {
            Contacts c = new Contacts();
            c.Type = 2;//for customers 2

            uctlQuickAddContact uctl = new uctlQuickAddContact();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Customer Registration";
            uctl.m_Contact = c;
            frm.ShowCtl(uctl);

            RefreshContactList();
        }

        private void btnQuickAgent_Click(object sender, EventArgs e)
        {
            Contacts c = new Contacts();
            c.Type = 3;//for agent 3

            uctlQuickAddContact uctl = new uctlQuickAddContact();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Agent Registration";
            uctl.m_Contact = c;
            frm.ShowCtl(uctl);

            RefreshContactList();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            uctlAddItem_Receipt uctl = new uctlAddItem_Receipt();
            frmGenericPopup frm = new frmGenericPopup();

            if (m_Receipt == null)
            {
                m_Receipt = new Receipt();
                grdReceiptItems.DataSource = m_Receipt.details;
            }

            uctl.ParentCtl = this;
            frm.Text = "Add Item";
            frm.ShowCtl(uctl);
        }

        public void AddItem(ReceiptDetails details)
        {
            m_Receipt.details.Add(details);
            RefreshDataSource();
        }

        public void RefreshDataSource()
        {
            grdvReceiptItems.RefreshData();
            grdvReceiptItems.FocusedRowHandle = grdvReceiptItems.RowCount - 1;

            ComputeTotal();
        }

        //private void grdvReceiptItems_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        //{
        //    if (bIsLoading)
        //        return;

        //    ReceiptDetails details = grdvReceiptItems.GetRow(e.ListSourceRowIndex) as ReceiptDetails;

        //    if (details == null)
        //        return;

        //    if (e.IsGetData && e.Column.FieldName == "DiscountedPrice")
        //    {
        //        string discount = grdvReceiptItems.GetRowCellValue(e.ListSourceRowIndex, "Discount").ToString();
        //        double price = Convert.ToDouble(grdvReceiptItems.GetRowCellValue(e.ListSourceRowIndex, "UnitPrice"));
        //        if (discount.Trim().Length > 0)
        //        {
        //            e.Value = cUtils.GetDiscountedPrices(discount,price);
        //        }
        //        else
        //            e.Value = "";
        //    }
        //}

        private void grdvReceiptItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bIsLoading)
                return;

            bool bChange = false;

            string messageErr = "";
            if (e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "QTY" || e.Column.FieldName == "Discount")
            {
                int qty =0;
                string discount = "", discountedprices = "";
                double unitprice = 0, subtotal = 0;
                qty = cUtils.ConvertToInteger(grdvReceiptItems.GetFocusedRowCellValue("QTY").ToString());
                discount = grdvReceiptItems.GetFocusedRowCellValue("Discount").ToString();
                unitprice = cUtils.ConvertToDouble(grdvReceiptItems.GetFocusedRowCellValue("UnitPrice").ToString());

                subtotal = qty * cUtils.GetLastDiscountPrice(discount, unitprice);
                discountedprices = cUtils.GetDiscountedPrices(discount, unitprice);

                grdvReceiptItems.SetFocusedRowCellValue("SubTotal", subtotal.ToString(cUtils.FMT_CURRENCY_AMT));
                grdvReceiptItems.SetFocusedRowCellValue("DiscountedPrice", discountedprices);
                
                //ComputeTotal();
            }
            else if (e.Column.FieldName == "itemSize")
            {
                if (bChange)
                    return;
                messageErr = "Size is invalid!";
                string itemDesc = grdvReceiptItems.GetFocusedRowCellValue("itemSize").ToString().Trim();

               IList<string> strList = mItemDao.GetUnitList(grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString().Trim(), itemDesc);

                repoCboUnit.Items.Clear();
                
                if (strList.Count > 0)
                {
                    foreach (string str in strList)
                    {
                        repoCboUnit.Items.Add(str);
                    }
                }
 
            }
            else if (e.Column.FieldName == "_Unit")
            {
                messageErr = "Unit is invalid!";
                string name, size, unit;
                name = grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString().Trim();
                size = grdvReceiptItems.GetFocusedRowCellValue("itemSize").ToString().Trim();
                unit = grdvReceiptItems.GetFocusedRowCellValue("_Unit").ToString().Trim();

                Item I = mItemDao.GetItem(name, size, unit);

                if (size.Trim() == "")
                {
                    I = mItemDao.CheckItemNoSize(name, unit);
                }

                if (I == null)
                    return;

                //if (I.Unit.Trim() != "")//non-weight
                //{
                    grdvReceiptItems.SetFocusedRowCellValue("UnitPrice", I.RetailPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                    grdvReceiptItems.SetFocusedRowCellValue("OrigPrice", I.RetailPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                //}
                //else//weight item
                //{
                //    grdvReceiptItems.SetFocusedRowCellValue("UnitPrice", I.RetailPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                //    grdvReceiptItems.SetFocusedRowCellValue("OrigPrice", I.UnitPrice2.ToString(cUtils.FMT_CURRENCY_AMT));
                //}

            }
            else if (e.Column.FieldName == "itemName")
            {
                messageErr = "Item Name is invalid!";
                bChange = true;
                grdvReceiptItems.SetFocusedRowCellValue("itemSize", "");
                repoCboSize.Items.Clear();
                try
                {
                    string itemName = "";
                    itemName = grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString();
                    IList<ItemSizes> sizeList = mItemDao.GetSizes(itemName);

                    if (sizeList[0] != null)
                    {
                        IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);


                        foreach (SortedSize size in sortedSizeList)
                        {
                            repoCboSize.Items.Add(size.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    cUtils.ShowMessageError(messageErr, "Receipts Add Item\n\nItem Name!");
                    cUtils.CreateCrashLog(ex.Message, "Add Item", "Receipt");
                }
            }
            else if (e.Column.FieldName == "SubTotal")
            {
                ComputeTotal();
            }

            //ReceiptDetails details = grdvReceiptItems.GetRow(e.RowHandle) as ReceiptDetails;

            //if (details == null)
            //    return;

            //if (e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "QTY" || e.Column.FieldName == "Discount")
            //{
            //    details.SubTotal = details.QTY * cUtils.GetLastDiscountPrice(details.Discount,details.UnitPrice);
            //}
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            ComputeTotal(true);
            RefreshDataSource();
        }

        public void ComputeTotal(bool bUndo=false)
        {
            double subTotal = 0;

            if (m_selectedReceipt != null)
            {
                foreach (ReceiptDetails rd in m_selectedReceipt.details)
                {
                    subTotal += rd.SubTotal;
                }
            }
            else
            {
                foreach (DataRow row in tblData.Rows)
                {
                    if (bUndo)
                    {
                        //d.SubTotal = d.QTY * d.UnitPrice;
                        row[(int)eCol.SubTotal] = cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString()) *
                                                    cUtils.ConvertToDouble(row[(int)eCol.UnitPrice].ToString());
                    }

                    subTotal += cUtils.ConvertToDouble(row[(int)eCol.SubTotal].ToString());
                }
            }

            txtTotalAmount.Text = subTotal.ToString(cUtils.AMOUNT_FMT);
        }

        private void btRemoveItem_Click(object sender, EventArgs e)
        {
            //ReceiptDetails selDetails = grdvReceiptItems.GetRow(grdvReceiptItems.FocusedRowHandle) as ReceiptDetails;

            //if(selDetails == null)
            //    return;

            //m_Receipt.details.Remove(selDetails);
            //RefreshDataSource();
            tblData.Rows.RemoveAt(grdvReceiptItems.GetDataSourceRowIndex(grdvReceiptItems.FocusedRowHandle));
            grdReceiptItems.RefreshDataSource();
            ComputeTotal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComputeTotal();
                
                if (cboCustomer.SelectedItem == null)
                {
                    cUtils.ShowMessageExclamation("Please select a customer", "Save Receipt");
                    cboCustomer.Focus();
                    return;
                }

                if (Convert.ToDouble(txtAmountPaid.Text.Trim()) <= 0)
                {
                    cUtils.ShowMessageExclamation("No Amount Paid!", "Save Receipt");
                    txtAmountPaid.Focus();
                    return;
                }


                if (m_selectedReceipt != null)//edit receipt
                {
                    //m_selectedReceipt.Agent = (Contacts)cboAgent.SelectedItem;
                    //m_selectedReceipt.Customer = (Contacts)cboCustomer.SelectedItem;
                    m_selectedReceipt.ReceiptAmount = Convert.ToDouble(txtTotalAmount.Text.Trim());
                    m_selectedReceipt.PaidAmount = Convert.ToDouble(txtAmountPaid.Text.Trim());
                    
                    receiptDao.SaveChanges(m_selectedReceipt);

                    cUtils.ShowMessageInformation("Receipt edited successfully!", "Edit Receipt");
                    cUtils.getMainForm().CloseCurrentTabPage();
                }
                else// NEW RECEIPT
                {
                    if (tblData.Rows.Count == 0)
                    {
                        cUtils.ShowMessageExclamation("You have not added any item", "Save Receipt");
                        return;
                    }

                    if (CheckTotalAmountOverrided())
                    {
                        if (cUtils.ShowMessageQuestion("Proceed with total amount override?", "Save Receipt") == DialogResult.No)
                            return;
                    }

                    m_Receipt = new Receipt();
                    m_Receipt.Agent = (Contacts)cboAgent.SelectedItem;
                    m_Receipt.Customer = (Contacts)cboCustomer.SelectedItem;
                    m_Receipt.isDeleted = false;
                    m_Receipt.ReceiptAmount = Convert.ToDouble(txtTotalAmount.Text.Trim());
                    m_Receipt.PaidAmount = Convert.ToDouble(txtAmountPaid.Text.Trim());
                    m_Receipt.ReceiptDate = dte1.DateTime;
                    m_Receipt.PO = txtPO.Text;

                    //manually add each row to details
                    //item index set to 0
                    int itemIndex = 1;
                    foreach (DataRow row in tblData.Rows)
                    {
                        ReceiptDetails rd = new ReceiptDetails();
                        rd.ItemIndex = itemIndex;
                        rd.item = mItemDao.GetItem(row[(int)eCol.itemName].ToString(), row[(int)eCol.itemSize].ToString(), row[(int)eCol.Unit].ToString());
                        if (rd.item == null)//try if item has no size
                        {
                            rd.item = mItemDao.CheckItemNoSize(row[(int)eCol.itemName].ToString(), row[(int)eCol.Unit].ToString());
                        }
                        rd.QTY = cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString());
                        rd.UnitPrice = cUtils.ConvertToDouble(row[(int)eCol.UnitPrice].ToString());
                        rd.Discount = row[(int)eCol.Discount].ToString();
                        rd.SubTotal = cUtils.ConvertToDouble(row[(int)eCol.SubTotal].ToString());
                        itemIndex += 1;

                        m_Receipt.details.Add(rd);
                    }

                    receiptDao.SaveReceipt(m_Receipt);
                    cUtils.ShowMessageInformation("Receipt saved successfully!", "Save Receipt");
                    cUtils.getMainForm().CloseCurrentTabPage();
                    cUtils.getMainForm().navBarTransactions_IssueReceipt_LinkClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                string error = "";
                if (ex.InnerException != null)
                    error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                else
                    error = string.Format("Error:\n{0}", ex.Message);

                cUtils.ShowMessageError(error, "Save Receipt");
                cUtils.CreateCrashLog(error, "Save Receipt", "Receipt");
            }
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            txtAmountPaid.Text = (sender as TextEdit).Text;
        }

        private bool CheckTotalAmountOverrided()
        { 
            double total=0, subtotal =0;
            //foreach (ReceiptDetails rd in m_Receipt.details)
            //{
            //    total += rd.SubTotal;
            //}

            foreach (DataRow row in tblData.Rows)
            {
                //get true receipt amount
                total += cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString()) *
                                            cUtils.ConvertToDouble(row[(int)eCol.UnitPrice].ToString());

                //get the subtotal
                subtotal += cUtils.ConvertToDouble(row[(int)eCol.SubTotal].ToString());
            }

            if (total != subtotal)
                return true;
            else
                return false;
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(bIsLoading)
                return;

            Contacts Customer =(Contacts) cboCustomer.SelectedItem;

            if (Customer != null)
            {
                if (Customer.Address == null)
                    txtAddress.Text = "";
                else if (Customer.Address.Trim().Length > 0)
                    txtAddress.Text = Customer.Address;
                else if (Customer.CompanyAddress.Trim().Length > 0)
                    txtAddress.Text = Customer.CompanyAddress;
            }
        }

        private void repoCboItemName_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cboEdit = sender as ComboBoxEdit;
            IList<string> nameList = new List<string>();
            nameList = mItemDao.GetItemNames(cboEdit.Text.Trim());

            cboEdit.Properties.Items.Clear();
            cboEdit.Properties.Items.AddRange(nameList.ToList<string>());
            cboEdit.ShowPopup();
            //if (cboEdit.Text.Trim().Length > 2)
            //{
            //    IList<string> nameList = new List<string>();
            //    //nameList = mItemDao.GetItemNameSuggestion(cboEdit.Text.Trim());
            //    nameList = mItemDao.GetItemNames(cboEdit.Text.Trim());
            //    cboEdit.Properties.Items.AddRange(nameList.ToList<string>());
            //    //cboEdit.Properties.Items.Clear();//clear first
            //    //foreach (string str in nameList)
            //    //{
            //    //    cboEdit.Properties.Items.Add(str);
            //    //}
            //}
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvReceiptItems.Columns["QTY"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["QTY"].OptionsColumn.AllowFocus = bEdit;

            grdvReceiptItems.Columns["itemName"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["itemName"].OptionsColumn.AllowFocus = bEdit;

            grdvReceiptItems.Columns["itemSize"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["itemSize"].OptionsColumn.AllowFocus = bEdit;

            grdvReceiptItems.Columns["UnitPrice"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["UnitPrice"].OptionsColumn.AllowFocus = bEdit;

            grdvReceiptItems.Columns["Discount"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["Discount"].OptionsColumn.AllowFocus = bEdit;

            grdvReceiptItems.Columns["SubTotal"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["SubTotal"].OptionsColumn.AllowFocus = bEdit;

            grdvReceiptItems.Columns["_Unit"].OptionsColumn.AllowEdit = bEdit;
            grdvReceiptItems.Columns["_Unit"].OptionsColumn.AllowFocus = bEdit;
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                tblData.Rows.RemoveAt(grdvReceiptItems.GetDataSourceRowIndex(grdvReceiptItems.FocusedRowHandle));
                grdReceiptItems.RefreshDataSource();
                ComputeTotal();
                nItemCount -= 1;
                lblItemCount.Text = string.Format("Item Count: {0}", nItemCount);
            }
            catch (Exception ex)
            {
                cUtils.ShowMessageError("This row is not yet committed.\nTo commit row, complete required fields first.\nOr press [ESC] to undo."
                                            , "Delete Row");
            }
        }

        private void grdvReceiptItems_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn grdQTY = view.Columns[(int)eCol.QTY];
            GridColumn grdItemName = view.Columns[(int)eCol.itemName];
            GridColumn grdSize = view.Columns[(int)eCol.itemSize];
            GridColumn grdPrice = view.Columns[(int)eCol.UnitPrice];
            GridColumn grdSubtotal = view.Columns[(int)eCol.SubTotal];
            GridColumn grdUnit = view.Columns[(int)eCol.Unit];

            //clear error values first
            view.ClearColumnErrors();

            int qty = 0;
            string itemname = "", itemsize = "", itemunit = "";
            double price = 0, subtotal = 0;
            qty =cUtils.ConvertToInteger(view.GetFocusedRowCellValue(grdQTY).ToString());
            itemname = view.GetFocusedRowCellValue(grdItemName).ToString().Trim();
            itemsize = view.GetFocusedRowCellValue(grdSize).ToString().Trim();
            price = cUtils.ConvertToDouble(view.GetFocusedRowCellValue(grdPrice).ToString());
            subtotal = cUtils.ConvertToDouble(view.GetFocusedRowCellValue(grdSubtotal).ToString());
            itemunit = view.GetFocusedRowCellValue(grdUnit).ToString();

            Item I = mItemDao.GetItem(itemname, itemsize, itemunit);
            
            if (string.IsNullOrWhiteSpace(itemname))
            {
                e.Valid = false;
                view.SetColumnError(grdItemName,"You must first select an item.");
                view.FocusedColumn = grdItemName;
                view.ShowEditor();
            }
            else if (string.IsNullOrWhiteSpace(itemsize))
            {
                IList<Item> listItem = mItemDao.GetItemList(itemname);

                if (listItem.Count > 0)
                {
                    if (listItem[0].Size != null)
                    {
                        e.Valid = false;
                        view.SetColumnError(grdSize, "Please select a size");
                        view.FocusedColumn = grdSize;
                        view.ShowEditor();
                    }
                }
                else
                {
                    e.Valid = false;
                    view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                    view.FocusedColumn = grdSize;
                    view.ShowEditor();
                }
            }
            else if (mItemDao.GetItem(itemname.Trim(),itemsize.Trim()) == null)
            {
                e.Valid = false;
                view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                view.SetColumnError(grdItemName, "Item does not exist! Either the item name or size does not exist");
                view.FocusedColumn = grdSize;
                view.ShowEditor();
            }
            else if (string.IsNullOrWhiteSpace(itemunit))
            {
                //check if the item has no unit
                var items = mItemDao.SearchForSingleItem(itemname, itemsize, itemunit);

                if (items.Count > 1)
                {
                    e.Valid = false;
                    view.SetColumnError(grdUnit, "Please choose a unit");
                    view.FocusedColumn = grdUnit;
                    view.ShowEditor();
                }
                else if (items.Count == 1 && !string.IsNullOrWhiteSpace(items[0].Unit)) 
                {
                    e.Valid = false;
                    view.SetColumnError(grdUnit, "Please choose a unit");
                    view.FocusedColumn = grdUnit;
                    view.ShowEditor();
                }
            }
            else if (I == null)
            {
                //try
                //I = mItemDao.GetItem(itemname);
                var itemList = mItemDao.SearchForSingleItem(itemname, itemsize, itemunit);

                if (itemList.Count > 1 || itemList.Count == 0)
                {
                    e.Valid = false;
                    view.SetColumnError(grdItemName, "Item does not exist! Either the item name, size or unit does not exist");
                    view.SetColumnError(grdSize, "Item does not exist! Either the item name, size or unit does not exist");
                    view.SetColumnError(grdUnit, "Item does not exist! Either the item name, size or unit does not exist");
                    view.FocusedColumn = grdItemName;
                    view.ShowEditor();
                }
                //else
                //{
                //    int nRow = -1;
                //    bool bCheck = CheckItemEntryDuplicate(itemname, itemsize, itemunit, ref nRow);
                //    if (!bCheck)//does not exist
                //    {
                //        e.Valid = true;
                //        nItemCount += 1;
                //    }
                //    else
                //    {
                //        e.Valid = false;
                //        cUtils.ShowMessageInformation(string.Format("Item already added at row {0}", nRow), "Item Duplicate");
                //    }
                //}
            }

            if (price <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdPrice, "Invalid Price.");
                view.FocusedColumn = grdPrice;
                view.ShowEditor();
            }
            else if (qty <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdQTY, "Invalid Quantity entered.");
                view.FocusedColumn = grdQTY;
                view.ShowEditor();
            }
            else if (subtotal == 0)
            {
                e.Valid = false;
                view.SetColumnError(grdSubtotal, "Invalid Subtotal.");
                view.FocusedColumn = grdSubtotal;
                view.ShowEditor();
            }
            //else
            //{
            //    int nRow = -1;
            //    bool bCheck = CheckItemEntryDuplicate(itemname, itemsize, itemunit, ref nRow);
            //    if (!bCheck)//does not exist
            //    {
            //        e.Valid = true;
            //        nItemCount += 1;
            //    }
            //    else
            //    {
            //        e.Valid = false;
            //        cUtils.ShowMessageInformation(string.Format("Item already added at row {0}", nRow), "Item Duplicate");
            //    }
            //}

            if (nItemCount > 24)
            {
                cUtils.ShowMessageInformation("You have reached the maximum number of items allowable in a receipt\nPlease create another receipt to record the additional items.",
                                                "Maximum Item Threshold reached");
                e.Valid = false;
            }
            else if(e.Valid)
            {
                //lblItemCount.Text = string.Format("Item Count: {0}", nItemCount);
                int nRow = -1;
                bool bCheck = CheckItemEntryDuplicate(itemname, itemsize, itemunit, ref nRow);
                if (!bCheck)//does not exist
                {
                    e.Valid = true;
                    nItemCount += 1;
                    lblItemCount.Text = string.Format("Item Count: {0}", nItemCount);
                }
                else
                {
                    e.Valid = false;
                    cUtils.ShowMessageInformation(string.Format("Item already added at row {0}", nRow), "Item Duplicate");
                }

            }
        }

        private void grdvReceiptItems_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grdvReceiptItems_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (bIsLoading)
                return;

            if (m_selectedReceipt == null)
            {
                string val = e.Value as string;
                if (e.Column.FieldName == "UnitPrice")
                {
                    double price = cUtils.ConvertToDouble(val);
                    e.DisplayText = string.Format("{0}", price.ToString(cUtils.FMT_CURRENCY_AMT));
                }
                else if (e.Column.FieldName == "QTY")
                {
                    int qty = cUtils.ConvertToInteger(val);
                    e.DisplayText = string.Format("{0}", qty.ToString(cUtils.FMT_NUMBER));
                }
                else if (e.Column.FieldName == "SubTotal")
                {
                    double stotal = cUtils.ConvertToDouble(val);
                    e.DisplayText = string.Format("{0}", stotal.ToString(cUtils.FMT_CURRENCY_AMT));
                }
            }

            if (e.Column.Name == "grdcolRow")
            {
                if (e.RowHandle >= 0)
                {
                    int n = e.RowHandle;
                    e.DisplayText = string.Format("{0}", n + 1);
                }
            }
        }

        private void grdvReceiptItems_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            ComputeTotal();
        }

        private void repoTxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void repoTxtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void repoTxtSubTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void repoTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
                e.Handled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ReceiptDetails rd = grdvReceiptItems.GetRow(grdvReceiptItems.FocusedRowHandle) as ReceiptDetails;

            if (rd != null)
            {
                uctlEditRowReceipt uctl = new uctlEditRowReceipt();
                frmGenericPopup frm = new frmGenericPopup();

                uctl.m_receipt = m_selectedReceipt;

                if (rd.ID == 0)
                    uctl.RecentlyAddedDetail = rd;
                
                uctl.nEditDetailID = rd.ID;
                uctl.bAdd = false;

                frm.Text = "Edit Receipt Data";
                frm.ShowCtl(uctl);

                if (!uctl.bCancel)
                {
                    if (uctl.receiptDetails.bEdited)
                    {
                        ReceiptDetails e_rd;
                        e_rd = (from lst in m_selectedReceipt.details
                                where lst.ID == uctl.nEditDetailID
                                select lst).SingleOrDefault<ReceiptDetails>();

                        var query = (from items in m_selectedReceipt.details
                                     where items.item.ID == uctl.receiptDetails.item.ID
                                     select items).SingleOrDefault<ReceiptDetails>();

                        if (query != null)
                        {
                            if (query.ItemIndex != uctl.receiptDetails.ItemIndex)
                            {
                                cUtils.ShowMessageInformation(string.Format("Item already added at row {0}", query.ItemIndex), "Item Duplicate");
                                return;
                            }
                        }

                        if (e_rd != null)
                        {
                            e_rd.QTY = uctl.receiptDetails.QTY;
                            e_rd.UnitPrice = uctl.receiptDetails.UnitPrice;
                            e_rd.Discount = uctl.receiptDetails.Discount;
                            e_rd.item = uctl.receiptDetails.item;

                            e_rd.SubTotal = uctl.receiptDetails.SubTotal;
                            e_rd.bEdited = true;

                            e_rd.itemName = e_rd.item.Name;
                            if (e_rd.item.Size != null)
                                e_rd.itemSize = e_rd.item.Size.Description;
                            else
                                e_rd.itemSize = "";

                            e_rd.DiscountedPrice = cUtils.GetDiscountedPrices(e_rd.Discount, e_rd.UnitPrice);

                            if (e_rd.item.Unit.Trim() != "")
                            {
                                e_rd._Unit = e_rd.item.Unit;
                                e_rd.OrigPrice = e_rd.item.UnitPrice;
                            }
                            else
                            {
                                e_rd._Unit = e_rd.item.Unit2;
                                e_rd.OrigPrice = e_rd.item.UnitPrice2;
                            }
                        }
                    }
                }
            }

            RefreshDataSource();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            rptPackingList rpt = new rptPackingList();

            rpt.xrlblDate.Text = m_selectedReceipt.ReceiptDate.ToShortDateString();
            rpt.xrlblPackListNo.Text = m_selectedReceipt.ID.ToString(cUtils.FMT_ID);
            rpt.xrlblCustomer.Text = m_selectedReceipt.Customer.ToString();

            if (m_selectedReceipt.Agent != null)
                rpt.xrlblAgent.Text = m_selectedReceipt.Agent.ToString();
            else
                rpt.xrlblAgent.Text = "";

            rpt.xrlblPO.Text = m_selectedReceipt.PO;
            rpt.xrlblTotalAmount.Text = m_selectedReceipt.ReceiptAmount.ToString(cUtils.FMT_CURRENCY_AMT);
            if (m_selectedReceipt.Customer.Address != "")
                rpt.xrlblAddress.Text = m_selectedReceipt.Customer.Address;
            else
                rpt.xrlblAddress.Text = m_selectedReceipt.Customer.CompanyAddress;


            if (m_selectedReceipt.Customer.Address != "")
                rpt.Address = m_selectedReceipt.Customer.Address;
            else
                rpt.Address = m_selectedReceipt.Customer.CompanyAddress;

            IList<ReceiptDetails> lstSource = receiptDao.GetReceiptDetails(m_selectedReceipt);
            foreach (ReceiptDetails r in lstSource)
            {
                if (r.Discount.Trim() != "")
                    r._ItemPriceDiscount = string.Format("{0}\nless: {1}", r.UnitPrice.ToString(cUtils.FMT_CURRENCY_AMT), r.Discount);
                else
                    r._ItemPriceDiscount = string.Format("{0}", r.UnitPrice.ToString(cUtils.FMT_CURRENCY_AMT));
            }

            rpt.DataSource = lstSource;

            rpt.ShowPreviewDialog();
        }

        private bool CheckItemEntryDuplicate(string itemname, string itemsize, string unit, ref int nRowIndex)
        {
            bool bExist = false;
            string sCheckName, sCheckSize, sCheckUnit;

            int i = 0;
            foreach (DataRow row in tblData.Rows)
            {
                sCheckName = row[(int)eCol.itemName].ToString();
                sCheckSize = row[(int)eCol.itemSize].ToString();
                sCheckUnit = row[(int)eCol.Unit].ToString();

                if (sCheckUnit == unit && sCheckName == itemname && sCheckSize == itemsize)
                {
                    bExist = true;
                    nRowIndex = i + 1;
                    break;
                }

                i += 1;
            }

            return bExist;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ReceiptDetails rd = grdvReceiptItems.GetRow(grdvReceiptItems.FocusedRowHandle) as ReceiptDetails;

            bool bHasDuplicates = false;
            string sDuplicates = "These items are not added:\nReason: Duplicates\n";
            if (rd != null)
            {
                uctlEditRowReceipt uctl = new uctlEditRowReceipt();
                frmGenericPopup frm = new frmGenericPopup();

                uctl.m_receipt = m_selectedReceipt;
                uctl.nEditDetailID = rd.ID;
                uctl.bAdd = true;

                frm.Text = "Edit Receipt Data";
                frm.ShowCtl(uctl);

                if (!uctl.bCancel)
                {
                    if (uctl.m_DetailsAddSource.Count > 0)
                    {
                        foreach (ReceiptDetails dt in uctl.m_DetailsAddSource)
                        {
                            var query = (from items in m_selectedReceipt.details
                                         where items.item.ID == dt.item.ID
                                         select items).SingleOrDefault<ReceiptDetails>();

                            if (query != null)
                            {
                                if (query.ItemIndex != dt.ItemIndex)
                                {
                                    //cUtils.ShowMessageInformation(string.Format("Item already added at row {0}", query.ItemIndex), "Item Duplicate");
                                    //return;
                                    if (!bHasDuplicates)
                                        bHasDuplicates = true;

                                    sDuplicates += string.Format("- {0} {1}\n", dt.item.Description, dt.item.Size.Description);
                                    dt.item = null;
                                }
                            }

                            if (dt.item != null)
                            {
                                dt.itemName = dt.item.Name;
                                dt.itemSize = dt.item.Size.Description;
                                dt.DiscountedPrice = cUtils.GetDiscountedPrices(dt.Discount, dt.UnitPrice);
                                dt.receipt = m_selectedReceipt;

                                if (dt.item.Unit.Trim() != "")
                                {
                                    dt._Unit = dt.item.Unit;
                                    dt.OrigPrice = dt.item.UnitPrice;
                                }
                                else
                                {
                                    dt._Unit = dt.item.Unit2;
                                    dt.OrigPrice = dt.item.UnitPrice2;
                                }

                                dt.bEdited = true;
                                m_selectedReceipt.details.Add(dt);
                            }
                        }//foreach end

                        if (bHasDuplicates)
                        {
                            cUtils.ShowMessageError(sDuplicates, "Duplicate Items");
                        }
                    }
                }
            }

            lblItemCount.Text = string.Format("Item Count: {0}", m_selectedReceipt.ItemCount());
            RefreshDataSource();
        }
    }
}
