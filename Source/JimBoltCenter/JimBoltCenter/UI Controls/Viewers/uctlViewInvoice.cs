using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Utils;
using JimBoltCenter.Forms;
using JimBoltCenter.UI_Controls.Transactions;
using JimBoltCenter.Reports;
using DBMapping.BOL;
using DBMapping.DAL;
using DevExpress.XtraEditors.Controls;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlViewInvoice : UserControl
    {
        private SalesInvoiceDao mInvoiceDao = null;
        private SalesInvoiceDetailsDao mInvoiceDetailsDao = null;
        private ItemDao mItemDao = null;
        private ContactsDao mContactDao = null;
        private ItemSizesDao mSizesDao = null;
        private UserDao userDao = null;
        private bool bIsLoading = true;

        public uctlViewInvoice()
        {
            InitializeComponent();
            mInvoiceDao = new SalesInvoiceDao();
            mInvoiceDetailsDao = new SalesInvoiceDetailsDao();
            mItemDao = new ItemDao();
            mContactDao = new ContactsDao();
            mSizesDao = new ItemSizesDao();
            userDao = new UserDao();
        }

        private void uctlViewInvoice_Load(object sender, EventArgs e)
        {
            bIsLoading = false;
            mruItemName.Focus();
            //grdInvoice.DataSource = mInvoiceDao.getAllRecords();

            Skin.SetGridSelectionColor(148, 183, 224, grdvInvoice);
              
            //load combos
            Contacts cTemp = new Contacts();
            cTemp.CompanyName = "ALL";
            cTemp.FirstName = "";
            cTemp.MiddleName = "";
            cTemp.LastName = "";
            IList<Contacts> listContact = mContactDao.GetContactList(1);
            listContact.Insert(0, cTemp);

            ItemSizes cSize = new ItemSizes();
            cSize.Description = "ALL";
            IList<ItemSizes> listSize = new List<ItemSizes>();
            listSize.Insert(0, cSize);

            IList<ItemSizes> tmp = new List<ItemSizes>();
            tmp = mSizesDao.getAllRecords();
            IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(tmp);
            SortedSize sortTemp = new SortedSize();
            sortTemp.Description = "ALL";
            sortTemp.sort1=0;
            sortedSizeList.Insert(0, sortTemp);
            //foreach (SortedSize sort in sortedSizeList)
            //{
            //    listSize.Add(mSizesDao.GetByDescription(sort.Description));
            //}

            lookupSizes.Properties.DataSource = sortedSizeList;
            lookupContacts.Properties.DataSource = listContact;

            btnSearch.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            uctlPassword uctl = new uctlPassword();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Delete Password";
            frm.ShowCtl(uctl);

            if (!uctl.bValidPass)
                return;

            SalesInvoice invoice = grdvInvoice.GetRow(grdvInvoice.FocusedRowHandle) as SalesInvoice;
            if (invoice != null)
            {
                try
                {
                    invoice.Deleted = true;
                    mInvoiceDao.SetInvoiceDeleted(invoice);
                    cUtils.ShowMessageInformation("Invoice Deleted Successfully!", "Delete Invoice");
                }
                catch (Exception ex)
                {
                    string error = "";
                    if (ex.InnerException != null)
                        error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Delete Invoice");
                }
            }

            btnSearch.PerformClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SalesInvoice invoice = grdvInvoice.GetRow(grdvInvoice.FocusedRowHandle) as SalesInvoice;
            if (invoice != null)
            {
                uctlInvoice uctl = new uctlInvoice();
                uctl.m_ViewInvoice = invoice;
                uctl.m_ViewInvoice.details = mInvoiceDetailsDao.GetDetails(invoice);
                uctl.bEditInvoice = false;

                MainForm m = this.ParentForm as MainForm;

                formUtils.AddToTabbedPage(m.tabWindow, "tbpgInvoice" + invoice.InvoiceID, "Invoice #"+invoice.InvoiceID, DockStyle.Fill, uctl, "Invoice #" + invoice.InvoiceID, "viewing selected invoice");

                m.lblTitle.Text = "Inovoice #" + invoice.InvoiceID;
                m.lblSubTitle.Text = "viewing selected invoice";
            }
        }

        private void RefreshData()
        {
            grdvInvoice.RefreshData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (bIsLoading)
                return;

            int contactID=0;
            ItemSizes size = new ItemSizes();
            SortedSize sorted = grdvLookupSizes.GetRow(grdvLookupSizes.FocusedRowHandle) as SortedSize;

            if (sorted != null)
                size = mSizesDao.GetById(sorted.ID);
            else
                size = null;
            
            if (size != null)
            {
                if (size.ID == 0)
                    size = null;
            }

            Contacts contact = grdvLookupContacts.GetRow(grdvLookupContacts.FocusedRowHandle) as Contacts;
            if (contact != null)
            {
                if (contact.ID != 0)
                    contactID = contact.ID;
            }

            string sItemFilter = GenerateItemFilter(mruItemName.Text.Trim(), size);
            grdInvoice.DataSource = mInvoiceDao.Search(sItemFilter, contactID, dteRange.getDateFrom(), dteRange.getDateTo(), chkDeleted.Checked);
            grdvInvoice.RefreshData();
        }

        private string GenerateItemFilter(string sItemName, ItemSizes size)
        {
            string filter = "";
            if (sItemName != "")
                filter += string.Format("(sivd.item.Name like '%{0}%' or sivd.item.Code like '%{0}%')", sItemName);

            if (size != null && sItemName != "")
                filter += string.Format(" and sivd.item.Size.ID={0}", size.ID);
            else if(size != null && sItemName == "")
                filter += string.Format(" sivd.item.Size.ID={0}", size.ID);

            return filter;
        }

        private void chkDeleted_CheckedChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void dteRange_DateSelectionChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        //private void grdvLookupContacts_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        //{
        //    Contacts c = grdvLookupContacts.GetRow(e.ListSourceRowIndex) as Contacts;
        //    if (c != null)
        //    {
        //        if (e.IsGetData && e.Column.FieldName == "name")
        //            e.Value = string.Format("{0} {1} {2}", c.FirstName, c.MiddleName, c.LastName);
        //    }
        //}

        private void lookupSizes_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void lookupContacts_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch.PerformClick();
        }

        private void mruItemName_TextChanged(object sender, EventArgs e)
        {
            if (mruItemName.Text.Trim().Length >= 2)
            {
                IList<string> nameList = new List<string>();
                //nameList = mItemDao.GetItemNameSuggestion(mruItemName.Text.Trim());
                nameList = mItemDao.GetUniqueItemNames(mruItemName.Text.Trim());
                foreach (string str in nameList)
                {
                    mruItemName.Properties.Items.Add(str);
                }
            }
        }

        private void mruItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void grdvInvoice_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (bIsLoading)
                return;


            SalesInvoice SI = grdvInvoice.GetRow(e.ListSourceRowIndex) as SalesInvoice;

            if (e.IsGetData && e.Column.FieldName == "freight")
            {
                if (SI.FreightAmount > 0)
                    e.Value = true;
                else
                    e.Value = false;
            }
            else if (e.IsGetData && e.Column.FieldName == "username")
            {
                e.Value = userDao.GetUserName(SI.User);
            }
        }

        private void grdvInvoice_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column.FieldName == "InvoiceID")
            //{
            //    btnView.PerformClick();
            //}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SalesInvoice invoice = grdvInvoice.GetRow(grdvInvoice.FocusedRowHandle) as SalesInvoice;
            if (invoice != null)
            {
                uctlInvoice uctl = new uctlInvoice();
                uctl.m_ViewInvoice = invoice;
                uctl.m_ViewInvoice.details = mInvoiceDetailsDao.GetDetails(invoice);
                uctl.bEditInvoice = true;

                MainForm m = this.ParentForm as MainForm;

                formUtils.AddToTabbedPage(m.tabWindow, "tbpgEditInvoice" + invoice.InvoiceID, "Invoice #" + invoice.InvoiceID, DockStyle.Fill, uctl, "Invoice #" + invoice.InvoiceID, "edit selected invoice");

                m.lblTitle.Text = "Edit Inovoice #" + invoice.InvoiceID;
                m.lblSubTitle.Text = "edit selected invoice";
            }
        }

        private void grdvLookupContacts_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (bIsLoading)
                return;

            Contacts C = grdvLookupContacts.GetRow(e.ListSourceRowIndex) as Contacts;

            if (e.Column.FieldName == "_Supplier")
            {
                e.Value = string.Format("{0}, {1} {2} {3}", C.CompanyName, C.FirstName, C.MiddleName, C.LastName);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptViewInvoice rpt = new rptViewInvoice();

            if (dteRange.getDateFrom().ToShortDateString() == dteRange.getDateTo().ToShortDateString())
                rpt.rptDate = string.Format("as of {0}", dteRange.getDateFrom().ToShortDateString());
            else
                rpt.rptDate = string.Format("from {0} to {1}", dteRange.getDateFrom().ToShortDateString(), dteRange.getDateTo().ToShortDateString());

            string sFilters = "";
            if (mruItemName.Text.Trim() != "")
                sFilters += "'" + mruItemName.Text + "'";

            SortedSize size = grdvLookupSizes.GetRow(grdvLookupSizes.FocusedRowHandle) as SortedSize;
            if (size != null)
                sFilters += " '" + size.Description + "'";

            Contacts C = grdvLookupContacts.GetRow(grdvLookupContacts.FocusedRowHandle) as Contacts;
            if (C != null)
                sFilters = " '" + C.ToString() + "'";

            IList<SalesInvoice> lstData = grdInvoice.DataSource as IList<SalesInvoice>;
            foreach (SalesInvoice S in lstData)
            {
                User U = userDao.GetById(S.User);

                if (U != null)
                    S._userEditedBy = U.username;
                else
                    S._userEditedBy = " ";
            }

            rpt.filters = sFilters;
            rpt.DataSource = lstData;

            rpt.ShowPreviewDialog();

        }

        /*private void uctlViewInvoice_Enter(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }*/
    }
}
