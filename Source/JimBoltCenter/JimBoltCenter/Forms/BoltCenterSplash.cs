using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Threading;


namespace JimBoltCenter.Forms
{
    public partial class BoltCenterSplash : SplashScreen
    {
        public BoltCenterSplash()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void BoltCenterSplash_Load(object sender, EventArgs e)
        {
           
        }

        private void BoltCenterSplash_Shown(object sender, EventArgs e)
        {
            
        }

        public void ShowSplashWindow()
        {
            this.Show();
            return;
        }

        public void SetStatus(string text)
        {
            lblStatus.Text = "";
            lblStatus.Text = text;
        }
    }
}