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
using DBMapping.BOL;
using DBMapping.DAL;

namespace JimBoltCenter.UI_Controls.UserAccount
{
    public partial class uctlLogin : UserControl
    {
        private UserDao m_userDao = null;
        private User m_User;

        public uctlLogin()
        {
            InitializeComponent();
            m_userDao = new UserDao();
            m_User = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                txtUsername.Focus();
                lblmsg.Text = "Invalid username!";
                lblmsg.Visible = true;
                return;
            }

            if (txtPassword.Text.Trim() == "")
            {
                txtPassword.Focus();
                lblmsg.Text = "Invalid password!";
                lblmsg.Visible = true;
                return;
            }

            m_User = m_userDao.LogIn(txtUsername.Text, txtPassword.Text);

            if (m_User != null)
            {
                MainForm.m_FormInstance.logged_user = m_User;
                btnCancel.PerformClick();
            }
            else 
            {
                txtUsername.Focus();
                lblmsg.Text = "Username and Password does not match!";
                lblmsg.Visible = true;
            }
        }

        private void uctlLogin_Load(object sender, EventArgs e)
        {
            lblmsg.Visible = false;
            Skin.SetLabelFont(labelControl1);
            Skin.SetLabelFont(labelControl2);
            Skin.SetLabelFont(lblmsg);

            Skin.SetButtonFont(btnLogin);
            Skin.SetButtonFont(btnCancel);

            Skin.SetTextEditFont(txtUsername);
            Skin.SetTextEditFont(txtPassword);

            txtUsername.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }
    }
}
