using System;
using System.Linq;
using System.Text;
using DevExpress.XtraTab;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace JimBoltCenter.Utils
{
    public static class formUtils
    {
        private static Timer timer1;

        //
        private static string strTimeConsumed = "";
        private static int nTimerTick;

        public static void AddToTabbedPage(XtraTabControl tabControl, string tabName, string tabTitle, DockStyle dockStyle, UserControl uctl, string toolTipTitle, string toolTip)
        {
            var ctl = (from page in tabControl.TabPages
                      where page.Name == tabName
                      select page).SingleOrDefault();

            if(ctl != null)
            {
                XtraTabPage selectedPage = ctl as XtraTabPage;
                tabControl.SelectedTabPage = selectedPage;
                return;
            }

            XtraTabPage tabPage = new XtraTabPage();
            tabPage.TooltipTitle = toolTipTitle;
            tabPage.Tooltip = toolTip;
            tabPage.Name = tabName;
            tabPage.Text = tabTitle;
            uctl.Dock = dockStyle;
            tabPage.Controls.Add(uctl);

            int nCount =0;
            foreach (XtraTabPage page in tabControl.TabPages)
            {
                if (page.Name == tabName)
                    nCount += 1;
            }

            if (nCount == 0)
                tabControl.TabPages.Add(tabPage);

            tabControl.SelectedTabPage = tabPage;
        }

        public static void LoadComboBoxEdit<T>(ComboBoxEdit combo, IList<T> list, bool Sorted, bool clearList = true)
        {
            combo.Properties.BeginUpdate();
            combo.Properties.Sorted = false;

            if (clearList)
                combo.Properties.Items.Clear();

            foreach (object item in list)
            {
                combo.Properties.Items.Add(item);
            }

            combo.Properties.EndUpdate();

            if (Sorted)
            {
                combo.Properties.Sorted = Sorted;
            }
        }
    }
}