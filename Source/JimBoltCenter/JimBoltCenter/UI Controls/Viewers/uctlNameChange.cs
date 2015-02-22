using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Utils;
using JimBoltCenter.UI_Controls.Viewers;
using DBMapping.BOL;
using DBMapping.DAL;

namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlNameChange : UserControl
    {
        public IList<Item> selItems { get; set; }
        private ItemDao m_ItemDao = null;

        public uctlNameChange()
        {
            InitializeComponent();
            m_ItemDao = new ItemDao();
        }

        private void uctlNameChange_Load(object sender, EventArgs e)
        {
            Skin.SetLabelFont(labelControl1);
            Skin.SetTextEditFont(txtNewItemName);
            Skin.SetButtonFont(btnCancel);
            Skin.SetButtonFont(btnOK);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach(Item _item in selItems)
            {
                _item.Name = txtNewItemName.Text.Trim();
                m_ItemDao.Save(_item);
            }

            cUtils.ShowMessageInformation("Name(s) changed successfully!", "Item Name Change");
            btnCancel.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}
