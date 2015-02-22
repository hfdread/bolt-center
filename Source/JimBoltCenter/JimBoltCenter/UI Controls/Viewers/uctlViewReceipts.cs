using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;
using JimBoltCenter.UI_Controls.Transactions;
using JimBoltCenter.UI_Controls.CustomControls;
using JimBoltCenter.Forms;
using JimBoltCenter.Reports;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlViewReceipts : UserControl
    {
        private bool bIsLoading;
        private ReceiptDao m_receiptDao = null;
        private ContactsDao m_ContactDao = null;
        private ItemSizesDao m_SizesDao = null;
        private ItemDao m_ItemDao = null;

        public uctlViewReceipts()
        {
            InitializeComponent();
            bIsLoading = true;
            m_receiptDao = new ReceiptDao();
            m_ContactDao = new ContactsDao();
            m_SizesDao = new ItemSizesDao();
            m_ItemDao = new ItemDao();
        }

        private void uctlViewReceipts_Load(object sender, EventArgs e)
        {
            bIsLoading = false;
            mruItemName.Focus();
            Skin.SetGridFont(grdvReceiptItems, new Font("Tahoma", 12));
            Skin.SetGridSelectionColor(148, 183, 224, grdvReceiptItems);
            
            //load combos
            Contacts cTemp = new Contacts();
            cTemp.CompanyName = "ALL";
            cTemp.FirstName = "";
            cTemp.MiddleName = "";
            cTemp.LastName = "";
            IList<Contacts> listContact = m_ContactDao.GetContactList(2);//customer
            listContact.Insert(0, cTemp);

            Contacts aTemp = new Contacts();
            aTemp.CompanyName = "ALL";
            aTemp.FirstName = "";
            aTemp.MiddleName = "";
            aTemp.LastName = "";
            IList<Contacts> agentList = m_ContactDao.GetContactList(3);//agent
            agentList.Insert(0, cTemp);

            ItemSizes cSize = new ItemSizes();
            cSize.Description = "ALL";
            IList<ItemSizes> listSize = new List<ItemSizes>();
            listSize.Insert(0, cSize);

            IList<ItemSizes> tmp = new List<ItemSizes>();
            tmp = m_SizesDao.getAllRecords();
            IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(tmp);
            SortedSize sortTemp = new SortedSize();
            sortTemp.Description = "ALL";
            sortTemp.sort1 = 0;
            sortedSizeList.Insert(0, sortTemp);
            //foreach (SortedSize sort in sortedSizeList)
            //{
            //    listSize.Add(m_SizesDao.GetByDescription(sort.Description));
            //}

            lookupSizes.Properties.DataSource = sortedSizeList;
            lookupContacts.Properties.DataSource = listContact;
            lookupAgent.Properties.DataSource = agentList;

            btnSearch.PerformClick();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (bIsLoading)
                return;

            int contactID = 0, agentID=0;
            ItemSizes size = new ItemSizes();
            SortedSize sorted =  grdvLookupSizes.GetFocusedRow() as SortedSize;

            if (sorted != null)
                size = m_SizesDao.GetById(sorted.ID);
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

            Contacts agent = grdvLookupAgent.GetRow(grdvLookupAgent.FocusedRowHandle) as Contacts;
            if (agent != null)
            {
                if (agent.ID != 0)
                    agentID = agent.ID;
            }

            string sItemFilter = GenerateItemFilter(mruItemName.Text.Trim(),size);
            grdReceiptItems.DataSource = m_receiptDao.Search(sItemFilter, contactID, agentID, dteRange.getDateFrom(), dteRange.getDateTo());
            grdvReceiptItems.RefreshData();
        }

        public static string GenerateItemFilter(string sItemName, ItemSizes size)
        {
            string filter = "";
            if (sItemName != "")
                filter += string.Format("(rd.item.Name like '%{0}%' or rd.item.Code like '%{0}%')", sItemName);

            if (size != null && sItemName != "")
                filter += string.Format(" and rd.item.Size.ID={0}", size.ID);
            else if (size != null && sItemName == "")
                filter += string.Format(" rd.item.Size.ID={0}", size.ID);

            return filter;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Receipt r = grdvReceiptItems.GetRow(grdvReceiptItems.FocusedRowHandle) as Receipt;

            if (r == null)
                return;

            uctlIssueReceipt uctl = new uctlIssueReceipt();
            uctl.m_selectedReceipt = r;
            uctl.m_selectedReceipt.details = m_receiptDao.GetReceiptDetails(r);
            uctl.bEditData = false;

            MainForm m = this.ParentForm as MainForm;

            formUtils.AddToTabbedPage(m.tabWindow, "tbpgInvoice" + r.ID, "Receipt #" + r.ID.ToString(cUtils.FMT_ID), DockStyle.Fill, uctl, "Receipt #" + r.ID.ToString(cUtils.FMT_ID), "viewing selected receipt");

            m.lblTitle.Text = "Receipt #" + r.ID.ToString(cUtils.FMT_ID);
            m.lblSubTitle.Text = "viewing selected receipt";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            uctlPassword uctl = new uctlPassword();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Receipt Delete";
            frm.ShowCtl(uctl);

            if (!uctl.bValidPass)
                return;
            
            Receipt r = grdvReceiptItems.GetRow(grdvReceiptItems.FocusedRowHandle) as Receipt;

            if (r == null)
                return;

            string info = string.Format("This action will delete this receipt permanently\nDo you with to cotinue?\n\nOR Number:{0}\nReceipt Amount:{1}", r.ID.ToString(cUtils.FMT_ID),r.ReceiptAmount);

            if (cUtils.ShowMessageQuestion(info, "Delete Receipt") == DialogResult.Yes)
            {
                try
                {
                    m_receiptDao.SetReceiptDeleted(r);
                    btnSearch.PerformClick();
                    cUtils.ShowMessageInformation("Receipt has been deleted successfully!", "Delete Receipt");
                }
                catch (Exception ex)
                {
                    string error = "";
                    if (ex.InnerException != null)
                        error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Delete Receipt");
                }
            }
        }

        private void dteRange_DateSelectionChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch.PerformClick();
        }

        private void lookupSizes_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void lookupContacts_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void lookupAgent_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void mruItemName_TextChanged(object sender, EventArgs e)
        {
            if (mruItemName.Text.Trim().Length >= 2)
            {
                IList<string> nameList = new List<string>();
                //nameList = m_ItemDao.GetItemNameSuggestion(mruItemName.Text.Trim());
                nameList = m_ItemDao.GetUniqueItemNames(mruItemName.Text.Trim());
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

       private void btnEdit_Click(object sender, EventArgs e)
        {
            Receipt r = grdvReceiptItems.GetRow(grdvReceiptItems.FocusedRowHandle) as Receipt;

            if (r == null)
                return;

            uctlIssueReceipt uctl = new uctlIssueReceipt();
            uctl.m_selectedReceipt = r;
            uctl.m_selectedReceipt.details = m_receiptDao.GetReceiptDetails(r);
            uctl.bEditData = true;

            MainForm m = this.ParentForm as MainForm;

            formUtils.AddToTabbedPage(m.tabWindow, "tbpgEditInvoice" + r.ID, "Receipt #" + r.ID.ToString(cUtils.FMT_ID), DockStyle.Fill, uctl, "Receipt #" + r.ID.ToString(cUtils.FMT_ID), "viewing selected receipt");

            m.lblTitle.Text = "Edit Receipt #" + r.ID.ToString(cUtils.FMT_ID);
            m.lblSubTitle.Text = "viewing selected receipt";
        }

        private void grdvLookupContacts_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (bIsLoading)
                return;

            Contacts C = grdvLookupContacts.GetRow(e.ListSourceRowIndex) as Contacts;

            if (e.Column.FieldName == "_Customer")
            {
                e.Value = string.Format("{0}, {1} {2} {3}", C.CompanyName, C.FirstName, C.MiddleName, C.LastName);
            }
        }

        private void grdvLookupAgent_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (bIsLoading)
                return;

            Contacts C = grdvLookupAgent.GetRow(e.ListSourceRowIndex) as Contacts;

            if (e.Column.FieldName == "_Agent")
            {
                e.Value = string.Format("{0}, {1} {2} {3}", C.CompanyName, C.FirstName, C.MiddleName, C.LastName);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptViewReceipt rpt = new rptViewReceipt();

            if (dteRange.getDateFrom().ToShortDateString() == dteRange.getDateTo().ToShortDateString())
                rpt.rptDate = string.Format("as of {0}", dteRange.getDateFrom().ToShortDateString());
            else
                rpt.rptDate = string.Format("from {0} to {1}", dteRange.getDateFrom().ToShortDateString(), dteRange.getDateTo().ToShortDateString());

            string sfilters = "";

            if (mruItemName.Text.Trim() != "")
                sfilters += " '" + mruItemName.Text + "'";

            SortedSize size = grdvLookupSizes.GetRow(grdvLookupSizes.FocusedRowHandle) as SortedSize;
            if (size != null)
                sfilters += " '" + size.Description + "'";

            Contacts Customer = grdvLookupContacts.GetRow(grdvLookupContacts.FocusedRowHandle) as Contacts;
            if (Customer != null)
                sfilters += " '" + Customer.ToString() + "'";

            Contacts Agent = grdvLookupAgent.GetRow(grdvLookupAgent.FocusedRowHandle) as Contacts;
            if (Agent != null)
                sfilters += " '" + Agent.ToString() + "'";

            rpt.sFilters = sfilters;

            rpt.DataSource = grdvReceiptItems.DataSource;
            rpt.ShowPreviewDialog();
        }
    }
}
