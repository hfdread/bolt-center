using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBMapping;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;

namespace JimBoltCenter.Forms
{
    public partial class frmDBConnect : Form
    {
        public frmDBConnect()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtServer.Text.Trim() == "")
            {
                cUtils.ShowMessageError("Invalid server name!", "DB Connection");
                txtServer.Focus();
            }
            else if (txtPassword.Text.Trim() == "")
            {
                cUtils.ShowMessageError("Invalid password!", "DB Connection");
                txtPassword.Focus();
            }
            else
            {
                clsDbConnect db = new clsDbConnect();
                db.DB_SERVER = txtServer.Text;
                db.DB_PWD = txtPassword.Text;
                db.DB_USER = "root";
                db.SaveSettings();

                //try connection
                txtServer.Enabled = false;
                txtPassword.Enabled = false;
                btnCancel.Enabled = false;
                btnConnect.Enabled = false;

                UserDao dao = new UserDao();
                dao.getSession();

                if (!dao.IsInitialized())
                {
                    cUtils.ShowMessageError(string.Format("DB Connection error!\nErrorMsg: {0}", dao.ErrorMessage), "DB Setup");
                    txtServer.Enabled = true;
                    txtPassword.Enabled = true;
                    btnCancel.Enabled = true;
                    btnConnect.Enabled = true;
                    txtServer.SelectAll();
                    txtServer.Focus();
                }
                else
                {
                    dao.CreateAdmin();
                    this.Hide();
                    MainForm main = new MainForm();
                    main.ShowDialog();
                    this.Close();                    
                }
            }

        }

        private void frmDBConnect_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
