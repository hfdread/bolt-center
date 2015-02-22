using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DBMapping.DAL;
using DBMapping.BOL;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlContact : UserControl
    {
        private ContactsDao m_ContactsDao = null;
        private Contacts m_SelectedContact;
        private int nSelectedRowHandle;

        private string errorText { get; set; }
        private string errorTitle { get; set; }

        public uctlContact()
        {
            InitializeComponent();
            m_ContactsDao = new ContactsDao();
            m_SelectedContact = null;
            nSelectedRowHandle = -1;
        }

        private void uctlContact_Load(object sender, EventArgs e)
        {
            Skin.SetGridSelectionColor(148, 183, 224, grdvContacts);
            //add contact type
            cboType.Properties.Items.Add("Supplier");
            cboType.Properties.Items.Add("Customer");
            cboType.Properties.Items.Add("Agent");
            cboType.SelectedIndex = 0;

            cboTypeField.Properties.Items.Add("Supplier");
            cboTypeField.Properties.Items.Add("Customer");
            cboTypeField.Properties.Items.Add("Agent");

            RefreshContactList(cboType.SelectedIndex + 1);
            EnableData(false);
        }

        private void grdvContacts_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            Contacts c = (Contacts) grdvContacts.GetRow(e.ListSourceRowIndex) ;

            if (e.IsGetData)
            {
                if(e.Column.FieldName == "ContactName")
                    e.Value = c.ToString();
            }

        }

        private void btnAdd_Click (object sender, EventArgs e)
        {
            EnableDisableButtons(btnAdd.Text);
            m_SelectedContact = null;
            EnableData(true,null,true);
            cboTypeField.SelectedIndex = -1;
            cboTypeField.Focus();
        }

        private void EnableDisableButtons(string btnName)
        {
            if(btnName.Equals("&Add") || btnName.Equals("&Edit"))
            {
                btnAdd.Enabled = false;
                btnEdit.Text = "&Save";
                btnDelete.Text = "&Cancel";
            }
            else if (btnName.Equals("&Save") || btnName.Equals("&Cancel"))
            {
                btnAdd.Enabled = true;
                btnEdit.Text = "&Edit";
                btnDelete.Text = "&Delete";
            }
        }

        private void EnableData(bool bEnable, Contacts c = null, bool bClearText = false)
        {
            if (c != null)
            {
                txtTIN.Text = c.TIN;
                txtSEC.Text = c.SEC;
                txtBIR.Text = c.BIR;
                txtNonVat.Text = c.NonVat;
                txtVat.Text = c.Vat;

                txtCompanyAddress.Text = c.CompanyAddress;
                txtCompanyName.Text = c.CompanyName;
                txtCompanyTelNo.Text = c.CompanyContact;

                txtAddress.Text = c.Address;
                txtAgent.Text = c.Agent;
                txtFaxNo.Text = c.FaxNo;
                txtFirstName.Text = c.FirstName;
                txtLandLineNo.Text = c.PhoneNo;
                txtLastName.Text = c.LastName;
                txtMiddleName.Text = c.MiddleName;
                txtMobileNo.Text = c.MobileNo;
                txtDescription.Text = c.Description;

                cboTypeField.SelectedIndex = c.Type - 1;
            }

            txtTIN.Enabled = bEnable;
            txtSEC.Enabled = bEnable;
            txtBIR.Enabled = bEnable;
            txtNonVat.Enabled = bEnable;
            txtVat.Enabled = bEnable;

            cboTypeField.Enabled = bEnable;

            if (bClearText)
            {
                txtTIN.Text = "";
                txtSEC.Text = "";
                txtBIR.Text = "";
                txtNonVat.Text = "";
                txtVat.Text = "";
            }

            foreach (Control control in grpCompanyInfo.Controls)
            {
                if (control is TextEdit)
                {
                    control.Enabled = bEnable;
                    if (bClearText)
                        control.Text = "";
                }
            }

            foreach (Control control in grpPersonalInfo.Controls)
            {
                if (control is TextEdit)
                {
                    control.Enabled = bEnable;
                    if (bClearText)
                        control.Text = "";
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Equals("&Save"))
            {
                if (ValidData())
                {
                    Contacts contact = null;

                    if (m_SelectedContact != null)
                        contact = m_SelectedContact;
                    else
                        contact = new Contacts();

                    contact.CompanyName = txtCompanyName.Text.Trim();
                    contact.CompanyAddress = txtCompanyAddress.Text.Trim();
                    contact.CompanyContact = txtCompanyTelNo.Text.Trim();
                    contact.FirstName = txtFirstName.Text.Trim();
                    contact.LastName = txtLastName.Text.Trim();
                    contact.MiddleName = txtMiddleName.Text.Trim();
                    contact.Address = txtAddress.Text.Trim();
                    contact.Agent = txtAgent.Text.Trim();
                    contact.MobileNo = txtMobileNo.Text.Trim();
                    contact.PhoneNo = txtLandLineNo.Text.Trim();
                    contact.FaxNo = txtFaxNo.Text.Trim();
                    contact.Description = txtDescription.Text.Trim();
                    contact.TIN = txtTIN.Text.Trim();
                    contact.BIR = txtBIR.Text.Trim();
                    contact.SEC = txtSEC.Text.Trim();
                    contact.NonVat = txtNonVat.Text.Trim();
                    contact.Vat = txtVat.Text.Trim();
                    contact.Type = cboTypeField.SelectedIndex + 1;

                    try
                    {
                        m_ContactsDao.Save(contact);
                        cUtils.ShowMessageInformation("Contact saved successfully!", "Save Contact");
                        RefreshContactList(cboType.SelectedIndex + 1);

                        //refresh value for global contact data;
                        m_SelectedContact = null;
                        m_SelectedContact = contact;

                        EnableData(false, contact, false);

                        grdContacts.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        string error = string.Format("Error:{0}", ex.Message);
                        MessageBox.Show(error, "Save Contact", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    EnableDisableButtons(btnEdit.Text);
                }
                else
                {
                    MessageBox.Show(errorText, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (btnEdit.Text == "&Edit")
            {
                if (m_SelectedContact != null)
                {
                    grdContacts.Enabled = false;
                    EnableData(true, m_SelectedContact, false);
                    cboTypeField.Focus();
                }
                EnableDisableButtons(btnEdit.Text);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "&Delete")
            {
                if (MessageBox.Show("Delete Selected Contact?", "Delete Contact!", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        m_ContactsDao.Delete(m_SelectedContact);
                        grdvContacts.DeleteRow(nSelectedRowHandle);
                        m_SelectedContact = null;
                        EnableData(false, null, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else if (btnDelete.Text == "&Cancel")
            {
                grdContacts.Enabled = true;
                EnableData(false);
                //if (m_SelectedContact == null)
                //    btnEdit.Enabled = false;
            }

            EnableDisableButtons(btnDelete.Text);
        }

        private void RefreshContactList(int type)
        {
            grdContacts.DataSource = m_ContactsDao.GetContactList(type);
            grdContacts.RefreshDataSource();

            grdvContacts.FocusedRowHandle = grdvContacts.RowCount - 1;
        }

        private void grdvContacts_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Contacts C = (Contacts)grdvContacts.GetRow(grdvContacts.FocusedRowHandle);
            if (C != null)
            {
                nSelectedRowHandle = grdvContacts.FocusedRowHandle;
                m_SelectedContact = C;
                EnableData(false, m_SelectedContact, false);

                if (!btnEdit.Enabled)
                    btnEdit.Enabled = true;
            }
            else
            {
                EnableData(false, null, false);
            }
        }

        private bool ValidData()
        {
            if (txtCompanyName.Text.Trim().Length < 0 || txtFirstName.Text.Trim().Length < 0
                || txtLastName.Text.Trim().Length < 0)
            {
                errorText = "Please fill at least 1 of the required fields.\n -Company Name\n -First Name\n -Last Name";
                errorTitle = "Required Fields";
                return false;
            }
            else if (cboTypeField.SelectedIndex < 0)
            {
                errorText = "Please select Type for Contact!";
                errorTitle = "Select Contact Type";
                return false;
            }
            else
                return true;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex >= 0)
                RefreshContactList(cboType.SelectedIndex + 1);
        }

        private void cboTypeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTypeField.SelectedIndex == 2)
                txtAgent.Enabled = false;
            else
                txtAgent.Enabled = true;
        }
    }
}
