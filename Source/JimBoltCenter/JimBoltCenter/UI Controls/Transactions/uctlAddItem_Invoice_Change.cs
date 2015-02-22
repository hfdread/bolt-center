using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBMapping.BOL;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlAddItem_Invoice_Change : UserControl
    {
        public uctlInvoice ParentCtl { get; set; }
        public SalesInvoiceDetails details { get; set; }

        public uctlAddItem_Invoice_Change()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cUtils.ConvertToInteger(txtQty.Text) > 0 && cUtils.ConvertToDouble(txtUnitPrice.Text) > 0)
            {
                details.QTY = cUtils.ConvertToInteger(txtQty.Text);
                details.Discount = txtDiscount.Text;
                details.Price = cUtils.ConvertToDouble(txtUnitPrice.Text);
                //ParentCtl.updated_details = details;
                btnCancel.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void uctlAddItem_Invoice_Change_Load(object sender, EventArgs e)
        {
            if (details != null)
            {
                lblItemName.Text = details.item.Name;
                lblItemDesc.Text = details.item.Description;
                lblItemSize.Text = details.item.Size.Description;

                txtQty.Text = Convert.ToString(details.QTY);
                txtDiscount.Text = details.Discount;
                txtUnitPrice.Text = details.Price.ToString(cUtils.AMOUNT_FMT);

                txtQty.Focus();
            }
        }

        private void uctlAddItem_Invoice_Change_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }
    }
}
