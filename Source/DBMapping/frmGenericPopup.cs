using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JimBoltCenter.Forms
{
    public partial class frmGenericPopup : Form
    {
        public frmGenericPopup()
        {
            InitializeComponent();
        }

        public void ShowCtl(Control ctl)
        {
            ctl.Visible = false;
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ctl);
            this.Bounds = ctl.Bounds;
            ctl.Dock = DockStyle.Fill;
            ctl.Visible = true;
            this.ShowDialog();
        }
    }
}
