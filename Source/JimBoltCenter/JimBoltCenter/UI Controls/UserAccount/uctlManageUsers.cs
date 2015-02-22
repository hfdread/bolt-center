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


namespace JimBoltCenter.UI_Controls.UserAccount
{
    public partial class uctlManageUsers : UserControl
    {
        private UserDao m_userDao = null;
        private bool bEdit;

        public uctlManageUsers()
        {
            InitializeComponent();
            m_userDao = new UserDao();
            bEdit = false;
        }

        private void uctlManageUsers_Load(object sender, EventArgs e)
        {
            Skin.SetLabelFont(labelControl1);
            Skin.SetLabelFont(labelControl2);
            Skin.SetLabelFont(labelControl3);
            Skin.SetLabelFont(labelControl4);
            Skin.SetLabelFont(labelControl5);
            Skin.SetLabelFont(labelControl6);
            Skin.SetLabelFont(labelControl7);
            Skin.SetLabelFont(labelControl8);
            Skin.SetLabelFont(labelControl9);
            
            
            Skin.SetTextEditFont(txtUsername);
            Skin.SetTextEditFont(txtPassword);
            Skin.SetTextEditFont(txtConfirmPass);
            Skin.SetTextEditFont(txtFname);
            Skin.SetTextEditFont(txtLname);
            Skin.SetTextEditFont(txtChange_OldPass);
            Skin.SetTextEditFont(txtChange_NewPass);
            Skin.SetTextEditFont(txtChange_ConfirmPass);

            Skin.SetComboBoxEditFont(cboUserType);
            Skin.SetComboBoxEditFont(cboRole);            

            Skin.SetButtonFont(btnChangePass);
            Skin.SetButtonFont(btnAdd);
            Skin.SetButtonFont(btnEdit);
            Skin.SetButtonFont(btnDelete);

            Skin.SetGridFont(grdvUsers);

            grdUsers.DataSource = m_userDao.GetUserList(GetUserType());
            EnableFields(false);

            User _user = new User();
            _user = grdvUsers.GetFocusedRow() as User;

            if (_user != null)
            {
                txtUsername.Text = _user.username;
                txtPassword.Text = _user.password;
                txtFname.Text = _user.fname;
                txtLname.Text = _user.lname;
            }
        }

        private void EnableFields(bool bEnable, bool bFromEdit = false)
        {
            txtUsername.Enabled = bEnable;
            if (bFromEdit)
            {
                txtPassword.Enabled = false;
                txtConfirmPass.Enabled = false;
            }
            else
            {
                txtPassword.Enabled = bEnable;
                txtConfirmPass.Enabled = bEnable;
            }
            txtFname.Enabled = bEnable;
            txtLname.Enabled = bEnable;
            cboRole.Enabled = bEnable;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "&Add")
            {
                btnEdit.Text = "&Save";
                btnDelete.Text = "&Cancel";
                btnAdd.Enabled = false;

                EnableFields(true);
                btnChangePass.Enabled = false;

                //clear fields for adding
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtConfirmPass.Text = "";
                txtFname.Text = "";
                txtLname.Text = "";

                grdUsers.Enabled = false;
            }
        }

        private bool Valid()
        {
            bool bComplete = true;

            if (txtUsername.Text.Trim() == "")
            {
                cUtils.ShowMessageError("Username not provided!", "User Management");
                bComplete = false;
            }
            else if (txtPassword.Text != txtConfirmPass.Text && txtPassword.Enabled)
            {
                cUtils.ShowMessageError("Password does not match!", "User Management");
                bComplete = false;
            }
            else if (txtPassword.Text.Trim() == "" && txtPassword.Enabled)
            {
                cUtils.ShowMessageError("Password not provided!", "User Management");
                bComplete = false;
            }

            return bComplete;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            User _user = new User();

            if (btnEdit.Text == "&Edit" )
            {
                _user = grdvUsers.GetFocusedRow() as User;

                if (_user != null)
                {
                    btnEdit.Text = "&Save";
                    btnDelete.Text = "&Cancel";
                    btnAdd.Enabled = false;
                    EnableFields(true, true);

                    btnChangePass.Enabled = true;

                    bEdit = true;
                    grdUsers.Enabled = false;
                }
            }
            else//Save
            {
                if (bEdit)
                {
                    if (txtUsername.Text.Trim() == "")
                    {
                        cUtils.ShowMessageError("Username not provided!", "User Management");
                        return;
                    }

                    _user = grdvUsers.GetFocusedRow() as User;
                    if (_user != null)
                    {
                        _user.username = txtUsername.Text;
                        _user.fname = txtFname.Text;
                        _user.lname = txtLname.Text;
                        _user.authentication = GetRole();

                        m_userDao.Save(_user);
                        cUtils.ShowMessageInformation("User Edited successfully!", "User Management");
                    }
                }
                else//Add
                {
                    if (Valid())
                    {
                        _user = new User();
                        _user.username = txtUsername.Text;

                        if (txtPassword.Enabled)
                            _user.password = txtPassword.Text;

                        _user.fname = txtFname.Text;
                        _user.lname = txtLname.Text;
                        _user.authentication = GetRole();

                        m_userDao.Save(_user);
                        string msg = "User Added successfully!";

                        cUtils.ShowMessageInformation(msg, "User Management");
                    }

                    txtPassword.Text = "";
                    txtConfirmPass.Text = "";
                    txtUsername.Text = "";
                    txtFname.Text = "";
                    txtLname.Text = "";
                }
                txtChange_ConfirmPass.Text = "";
                txtChange_NewPass.Text = "";
                txtChange_OldPass.Text = "";

                grdUsers.DataSource = m_userDao.GetUserList(GetUserType());
                EnableFields(false);

                btnAdd.Enabled = true;
                btnEdit.Text = "&Edit";
                btnDelete.Text = "&Delete";

                grdUsers.Enabled = true;
            }
        }

        public int GetRole()
        {
            int nRet = -1;
            switch (cboRole.SelectedIndex)
            { 
                case 0://administrator
                    nRet = 3;
                    break;
                case 1://cashier
                    nRet = 1;
                    break;
                case 2://sales
                    nRet = 0;
                    break;
                default:
                    nRet = 99;
                    break;
            }

            return nRet;
        }

        public int GetUserType()
        {
            int nRet = -1;
            switch (cboUserType.SelectedIndex)
            {
                case 0://administrator
                    nRet = 3;
                    break;
                case 1://cashier
                    nRet = 1;
                    break;
                case 2://sales
                    nRet = 0;
                    break;
                default:
                    nRet = 99;
                    break;
            }

            return nRet;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            User _user = new User();

            if (btnDelete.Text == "&Cancel")//Cancel
            {
                btnAdd.Enabled = true;
                btnEdit.Text = "&Edit";
                btnDelete.Text = "&Delete";
                EnableFields(false);
                btnChangePass.Enabled = false;

                _user = new User();
                _user = grdvUsers.GetFocusedRow() as User;

                if (_user != null)
                {
                    txtUsername.Text = _user.username;
                    txtPassword.Text = _user.password;
                    txtFname.Text = _user.fname;
                    txtLname.Text = _user.lname;
                }

                grdUsers.Enabled = true;
            }
            else//Delete
            {
                _user = new User();
                _user = grdvUsers.GetFocusedRow() as User;
                if (_user != null)
                {
                    if (cUtils.ShowMessageQuestion("Do yo want to Delete Selected User?", "User Delete Confirmation")
                        == DialogResult.Yes)
                    {
                        m_userDao.Delete(_user);
                        grdUsers.DataSource = m_userDao.GetUserList(GetUserType());
                    }
                }
            }
        }


        private void grdvUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            

        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            User _user = new User();
            _user = grdvUsers.GetFocusedRow() as User;

            if(_user != null)
            {
                if (txtChange_OldPass.Text != _user.password)
                {
                    cUtils.ShowMessageError("Old Password is incorrect!", "Change User Password");
                    return;
                }
                else if (txtChange_NewPass.Text != txtChange_ConfirmPass.Text)
                {
                    cUtils.ShowMessageError("New Password does not match!", "Change User Password");
                    return;
                }


                _user.password = txtChange_NewPass.Text;
                m_userDao.Save(_user);
                cUtils.ShowMessageInformation("Password change successfully!", "Change User Password");

                txtChange_OldPass.Text = "";
                txtChange_NewPass.Text = "";
                txtChange_ConfirmPass.Text = "";

                grdUsers.DataSource = m_userDao.GetUserList(GetUserType());
            }
        }

        private void cboUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboRole.SelectedIndex = cboUserType.SelectedIndex;

            grdUsers.DataSource = m_userDao.GetUserList(GetUserType());
        }

        private void grdvUsers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            User _user = new User();
            _user = grdvUsers.GetFocusedRow() as User;

            if (_user != null)
            {
                txtUsername.Text = _user.username;
                txtPassword.Text = _user.password;
                txtFname.Text = _user.fname;
                txtLname.Text = _user.lname;
            }
        }

    }
}
