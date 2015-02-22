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
    public partial class uctlQuickAddContact : UserControl
    {
        private ContactsDao m_contactDao = null;
        public Contacts m_Contact { get; set; }
        

        public uctlQuickAddContact()
        {
            InitializeComponent();
            m_contactDao = new ContactsDao();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                m_Contact.FirstName = txtFirstName.Text.Trim();
                m_Contact.CompanyName = txtCompanyName.Text.Trim();
                m_Contact.LastName = txtLastName.Text.Trim();

                m_contactDao.Save(m_Contact);
                btnCancel.PerformClick();
            }
            catch (Exception ex)
            {
                string error = "";
                if (ex.InnerException != null)
                    error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                else
                    error = string.Format("Error:\n{0}", ex.Message);

                cUtils.ShowMessageError(error, "Error");
            }
        }

        private void uctlQuickAddContact_Load(object sender, EventArgs e)
        {
            txtCompanyName.Focus();
        }
    }
}
