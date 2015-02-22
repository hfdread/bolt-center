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
    public partial class uctlQuickAddForwarder : UserControl
    {
        private ForwardersDao forwarderDao = null;

        public uctlQuickAddForwarder()
        {
            InitializeComponent();
            forwarderDao = new ForwardersDao();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCompanyName.Text.Trim().Length == 0)
            {
                cUtils.ShowMessageInformation("Missing Company Name!", "Add Forwarder");
                txtCompanyName.Focus();
                return;
            }

            try
            {
                Forwarders F = new Forwarders();
                F.CompanyName = txtCompanyName.Text.Trim();
                F.Details = txtDetails.Text;

                forwarderDao.Save(F);
                btnCancel.PerformClick();
            }
            catch (Exception ex)
            {
                string error = "";
                if (ex.InnerException != null)
                    error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                else
                    error = string.Format("Error:\n{0}", ex.Message);

                cUtils.ShowMessageError(error, "Add Forwarder");
            }
        }
    }
}
