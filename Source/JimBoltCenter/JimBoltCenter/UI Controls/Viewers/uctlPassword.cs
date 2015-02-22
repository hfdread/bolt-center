using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlPassword : UserControl
    {
        public bool bValidPass;

        public uctlPassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void uctlPassword_Load(object sender, EventArgs e)
        {
            Skin.SetTextEditFont(txtPass);
            Skin.SetButtonFont(btnOk);
            Skin.SetButtonFont(btnCancel);
            bValidPass = false;
            txtPass.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ValidatePassword();

            if (bValidPass)
                btnCancel.PerformClick();
            else
                cUtils.ShowMessageError("Password Invalid!", "Delete Password");
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValidatePassword();

                if (bValidPass)
                    btnCancel.PerformClick();
                else
                    cUtils.ShowMessageError("Password Invalid!", "Delete Password");
            }
        }


        private void ValidatePassword()
        {
            string uPassword = txtPass.Text;

            if (uPassword == "123456")
                bValidPass = true;
        }

    }
}
