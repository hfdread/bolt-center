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
using JimBoltCenter.UI_Controls.Viewers;
using JimBoltCenter.Utils;
using JimBoltCenter.Forms;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlItemBreakDown : UserControl
    {
        private ItemDao itemDao = null;
        private ItemSizesDao sizeDao = null;

        public Item weightItem { get; set; }

        public uctlItemBreakDown()
        {
            InitializeComponent();

            itemDao = new ItemDao();
            sizeDao = new ItemSizesDao();
        }

        private void uctlItemBreakDown_Load(object sender, EventArgs e)
        {
            Skin.SetGridFont(grdvItems);
            Skin.SetGridSelectionColor(148, 183, 224, grdvItems);
            Skin.SetLabelFont(labelControl1);
            Skin.SetTextEditFont(txtSearch);
            Skin.SetButtonFont(btnSelect);
            Skin.SetButtonFont(btnCancel);

            grdItems.DataSource = itemDao.GetLimitedRecordOnly_NonWeight(100);
            txtSearch.Focus();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Item I = grdvItems.GetRow(grdvItems.FocusedRowHandle) as Item;

            if (I != null)
            {
                //get details of the weight item
                uctlEnterConverted uctl = new uctlEnterConverted();
                frmGenericPopup frm = new frmGenericPopup();

                uctl.W_item = this.weightItem;
                uctl.S_item = I;

                frm.Text = "Enter QTY to Convert";
                frm.ShowCtl(uctl);

                LoadDataSource();
            }
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataSource();
        }

        private void LoadDataSource()
        {
            if (txtSearch.Text.Trim() == "")
                grdItems.DataSource = itemDao.GetLimitedRecordOnly_NonWeight(100);
            else
                grdItems.DataSource = itemDao.GetRecords_NonWeight(txtSearch.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}
