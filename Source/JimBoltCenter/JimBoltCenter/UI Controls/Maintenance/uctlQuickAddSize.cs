using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Utils;
using DBMapping.BOL;
using DBMapping.DAL;

namespace JimBoltCenter.UI_Controls.Maintenance
{
    public partial class uctlQuickAddSize : UserControl
    {
        private ItemSizesDao sizeDao = null;

        public uctlQuickAddSize()
        {
            InitializeComponent();
            sizeDao = new ItemSizesDao();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDesc.Text.Trim().Length == 0)
            {
                cUtils.ShowMessageInformation("Missing Description!", "Add Item Sizes");
                txtDesc.Focus();
                return;
            }

            try
            {
                ItemSizes I = new ItemSizes();
                I.Description = txtDesc.Text.Trim();

                sizeDao.Save(I);
                cUtils.ShowMessageInformation("Size Added successfully!", "Size Saved");
                txtDesc.Text = "";
                txtDesc.Focus();
                //btnCancel.PerformClick();
            }
            catch (Exception ex)
            {
                string error = "";
                if (ex.InnerException != null)
                    error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                else
                    error = string.Format("Error:\n{0}", ex.Message);

                cUtils.ShowMessageError(error, "Add Item Sizes");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}
