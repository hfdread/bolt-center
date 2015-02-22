using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBMapping.DAL;
using DBMapping.BOL;
using JimBoltCenter.UI_Controls.Transactions;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlAddItem_Receipt : UserControl
    {
        public uctlIssueReceipt ParentCtl { get; set; }
        private ItemDao mItemDao = null;
        private ItemSizesDao mSizesDao = null;
        private bool bIsLoading = true;

        public uctlAddItem_Receipt()
        {
            InitializeComponent();
            mItemDao = new ItemDao();
            mSizesDao = new ItemSizesDao();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (bIsLoading)
                return;

            ItemSizes size = gridLookUpEdit1View.GetRow(gridLookUpEdit1View.FocusedRowHandle) as ItemSizes;
            if (size != null)
            {
                if (size.ID == 0)
                    size = null;
            }

            string sFilter = cUtils.GenerateItemFilter(txtSearch.Text.Trim(),size);
            grdItems.DataSource = mItemDao.Search(sFilter);
            if (grdvItems.RowCount <= 0)
                cUtils.ShowMessageInformation(string.Format("No Item found with search string:{0}", txtSearch.Text), "Add Item Invoice");
        }

        private void grdvItems_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            Item I = (Item)grdvItems.GetRow(e.ListSourceRowIndex);

            if (e.IsGetData && e.Column.FieldName == "desc")
                e.Value = I.Name + "," + I.Description;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Item selItem = (Item)grdvItems.GetRow(grdvItems.FocusedRowHandle);
            if (selItem != null)
            {
                if (txtQty.Text.Trim().Length > 0)
                {
                    ReceiptDetails details = new ReceiptDetails();

                    details.Discount = txtDiscount.Text.Trim();
                    details.item = selItem;
                    details.QTY = Convert.ToInt32(txtQty.Text);
                    if (txtPrice.Text.Trim().Length > 0 && cUtils.ConvertToDouble(txtPrice.Text.Trim()) > 0)
                        details.UnitPrice = cUtils.ConvertToDouble(txtPrice.Text.Trim());
                    else
                    {
                        cUtils.ShowMessageInformation("Please provide Item Price", "Add Item Receipt");
                        return;
                    }

                    details.SubTotal = details.QTY * cUtils.GetLastDiscountPrice(details.Discount,details.UnitPrice);

                    ParentCtl.AddItem(details);
                }
                else
                {
                    cUtils.ShowMessageInformation("Please provide quantity", "Add Item Receipt");
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void uctlAddItem_Invoice_Load(object sender, EventArgs e)
        {
            bIsLoading = false;

            //load sizes
            ItemSizes cSize = new ItemSizes();
            cSize.Description = "ALL";
            IList<ItemSizes> listSize = mSizesDao.getAllRecords();
            listSize.Insert(0, cSize);
            lookupSizes.Properties.DataSource = listSize;
            lookupSizes.SelectedText = "ALL";

            txtSearch.Focus();
            btnSearch.PerformClick();
            grdvItems.FocusedRowHandle = grdvItems.RowCount - (grdvItems.RowCount -1);
        }

        private void grdvItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Item i = grdvItems.GetRow(grdvItems.FocusedRowHandle) as Item;
            if (i != null)
            {
                txtPrice.Text = i.UnitPrice.ToString(cUtils.AMOUNT_FMT);
            }

            txtQty.Focus();
            txtQty.Select(0, txtQty.Text.Trim().Length);
        }

        
    }
}
