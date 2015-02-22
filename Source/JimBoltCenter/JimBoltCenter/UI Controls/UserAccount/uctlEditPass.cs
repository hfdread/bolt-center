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
using JimBoltCenter.UI_Controls;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.UserAccount
{
    public partial class uctlEditPass : UserControl
    {
        private User_PrivilegesDao privDao = null;
        private UserDao m_userDao = null;

        public bool bAuthenticate;

        public uctlEditPass()
        {
            InitializeComponent();
            privDao = new User_PrivilegesDao();
            m_userDao = new UserDao();
            bAuthenticate = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                cUtils.ShowMessageError("Invalid username!", "Authentication");
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text.Trim() == "")
            {
                cUtils.ShowMessageError("Invalid password!", "Authentication");
                txtPassword.Focus();
                return;
            }

            User _user = m_userDao.LogIn(txtUsername.Text, txtPassword.Text);

            if (_user != null)//user found
            {
                if (_user.authentication != 3)
                {
                    cUtils.ShowMessageError("User not an Administrator!", "Authentication");
                    txtUsername.Focus();
                }
                else
                {
                    bAuthenticate = true;
                    btnCancel.PerformClick();
                }
            }
            else//no user found
            {
                cUtils.ShowMessageError("Username and password does not match!", "Authentication");
                txtUsername.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void uctlEditPass_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.PerformClick();
        }
    }
}
