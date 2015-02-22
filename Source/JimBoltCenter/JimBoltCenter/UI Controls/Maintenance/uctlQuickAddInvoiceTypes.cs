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

namespace JimBoltCenter.UI_Controls.Maintenance
{
    public partial class uctlQuickAddInvoiceTypes : UserControl
    {
        private InvoiceTypeDao m_invoicetypeDao = null;
        
        public uctlQuickAddInvoiceTypes()
        {
            InitializeComponent();
            m_invoicetypeDao = new InvoiceTypeDao();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim().Length == 0)
            {
                cUtils.ShowMessageInformation("Missing Code!", "Add Invoice Type");
                txtCode.Focus();
                return;
            }

            try
            {
                InvoiceType I = new InvoiceType();
                I.Code = txtCode.Text.Trim();
                I.Type = txtDesc.Text.Trim();

                m_invoicetypeDao.Save(I);
                btnCancel.PerformClick();
            }
            catch (Exception ex)
            {
                string error = "";
                if (ex.InnerException != null)
                    error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                else
                    error = string.Format("Error:\n{0}", ex.Message);

                cUtils.ShowMessageError(error, "Add Invoice Type");
            }
        }
    }
}
