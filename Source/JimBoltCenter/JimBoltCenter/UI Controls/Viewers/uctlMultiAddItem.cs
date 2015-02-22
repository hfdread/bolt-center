using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBMapping.BOL;
using DBMapping.DAL;
using DevExpress.XtraEditors.Controls;
using JimBoltCenter.Utils;
using JimBoltCenter.Forms;
using DevExpress.XtraEditors;


namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlMultiAddItem : UserControl
    {
        private ItemSizesDao m_sizesDao = null;
        private ItemSizes m_sizes = null;
        private WeightDao m_weightDao = null;
        private SortedSizeDao m_sortedDao = null;

        public uctlViewInventory inventory { get; set; }

        public uctlMultiAddItem()
        {
            InitializeComponent();
            m_sizes = new ItemSizes();
            m_sizesDao = new ItemSizesDao();
            m_weightDao = new WeightDao();
            m_sortedDao = new SortedSizeDao();
        }

        private void uctlMultiAddItem_Load(object sender, EventArgs e)
        {
            Skin.SetCheckedListBoxFont(chklst_Source);
            Skin.SetCheckedListBoxFont(chklst_Target);
            Skin.SetCheckEditFont(chkSrc);
            Skin.SetCheckEditFont(chkTarget);
            Skin.SetButtonFont(btnAdd);
            Skin.SetButtonFont(btnRemove);
            Skin.SetButtonFont(btnAddtoInventory);
            Skin.SetTextEditFont(txtSearch);
            Skin.SetTextEditFont(txtItemName);
            Skin.SetTextEditFont(txtUnit1);
            Skin.SetComboBoxEditFont(cboUnit2);
            Skin.SetLabelFont(lblItemName);
            Skin.SetLabelFont(lblUnit1);
            Skin.SetLabelFont(lblUnit2);
            Skin.SetLookUpEditFont(leditUnit2);
            
            IList<Weight> weightList = m_weightDao.getAllRecords();
            cboUnit2.Properties.Items.Clear();

            leditUnit2.Properties.DisplayMember = "Measurement";
            leditUnit2.Properties.DataSource = weightList;
            //foreach (Weight W in weightList)
            //{
            //    cboUnit2.Properties.Items.Add(W.de);
            //}

            ItemSize_Search(txtSearch.Text);
            txtSearch.Focus();
        }

        private void ItemSize_Search(string sSearch)
        {
            IList<ItemSizes> list = null;
            if (sSearch.Trim() == "")
            {
                list = m_sizesDao.getAllRecords();
            }
            else
            {
                list = m_sizesDao.SearchSizes(sSearch);
            }

            chklst_Source.Items.Clear();
            IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(list);
            /*foreach (ItemSizes size in list)
            {
                chklst_Source.Items.Add(size.ToString(), false);
            }*/
            //foreach (SortedSize sort in sortedSizeList)
            //{
            //    chklst_Source.Items.Add(m_sizesDao.GetByDescription(sort.Description));
            //    chklst_Source.DataSource = m_sizes
            //}

            chklst_Source.DisplayMember = "Description";
            chklst_Source.ValueMember = "ID";
            chklst_Source.DataSource = m_sortedDao.SetSourceCheckedFalse(sortedSizeList);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (chklst_Source.CheckedItems.Count <= 0)
                return;


            bool btargetSourceNull = true;

            foreach (SortedSize s in chklst_Source.CheckedItems)
            {
                s.bChecked = true;
            }

            IList<SortedSize> source = chklst_Source.DataSource as IList<SortedSize>;
            IList<SortedSize> target = new List<SortedSize>();

            target = source
                    .Where(Q => Q.bChecked == true)
                    .ToList<SortedSize>();

            chklst_Target.DisplayMember = "Description";
            chklst_Target.ValueMember = "ID";

            chklst_Source.DataSource = m_sortedDao.RemoveCheckedFromSource(source);

            if (chklst_Target.DataSource != null)
                btargetSourceNull = false;

            foreach (SortedSize size in target)
            {
                size.bChecked = false;
            }

            if (btargetSourceNull)
            {
                chklst_Target.DataSource = target;
            }
            else//not empty
            {
                IList<SortedSize> oldTargetSource = chklst_Target.DataSource as IList<SortedSize>;

                foreach (SortedSize item in target)
                {
                    oldTargetSource.Add(item);
                }

                chklst_Target.DataSource = oldTargetSource;
            }

            chklst_Target.Refresh();
            chklst_Source.Refresh();
            #region old code
            ////save the old data source
            //List<object> srcObj = new List<object>();
            //foreach (object objItem in chklst_Target.Items)
            //{
            //    srcObj.Add(objItem);
            //}

            //foreach (CheckedListBoxItem item in chklst_Source.CheckedItems)
            //{
            //    //chklst_Target.Items.Add(item.Value.ToString(), false);
            //    srcObj.Add(item);
            //}

            //ArrayList arr = new ArrayList(chklst_Source.CheckedIndices);
            //List<object> listObj = new List<object>();//list of objects that some of will be deleted

            //foreach (object obj in chklst_Source.Items)
            //{
            //    listObj.Add(obj);
            //}

            //foreach (int index in arr)
            //{
            //    object objItem = chklst_Source.GetItem(index);
            //    listObj.Remove(objItem);
            //}

            //chklst_Source.Items.Clear();
            //chklst_Target.Items.Clear();

            //foreach (object objItem1 in listObj)
            //{
            //    chklst_Source.Items.Add(objItem1, false);
            //}

            //foreach (object objItem2 in srcObj)
            //{
            //    chklst_Target.Items.Add(objItem2, false);
            //}

            //ClearCheckBoxesAll();
            #endregion old code
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (chklst_Target.CheckedItems.Count <= 0)
                return;

            foreach (SortedSize s in chklst_Target.CheckedItems)
            {
                s.bChecked = true;
            }

            IList<SortedSize> Target = chklst_Target.DataSource as IList<SortedSize>;
            
            //get checked items
            var query = Target
                        .Where(Q => Q.bChecked == true)
                        .ToList<SortedSize>();

            //datasource removing all checked items
            chklst_Target.DataSource = m_sortedDao.RemoveCheckedFromSource(Target);

            IList<SortedSize> Source = chklst_Source.DataSource as IList<SortedSize>;
            foreach (SortedSize item in query)
            {
                item.bChecked = false;
                Source
                .Add(item);
            }

            chklst_Source.DataSource = m_sortedDao.RemoveCheckedFromSource(Source);

            chklst_Target.Refresh();
            chklst_Source.Refresh();

            #region old code
            //if (chklst_Target.CheckedItemsCount <= 0)
            //    return;
            ////save the old data source
            //List<object> srcObj = new List<object>();
            //foreach (object objItem in chklst_Source.Items)
            //{
            //    srcObj.Add(objItem);
            //}

            //foreach (CheckedListBoxItem item in chklst_Target.CheckedItems)
            //{
            //    //chklst_Source.Items.Add(item.Value.ToString(), false);
            //    srcObj.Add(item);
            //}

            //ArrayList arr = new ArrayList(chklst_Target.CheckedIndices);
            //List<object> listObj = new List<object>();

            //foreach (object obj in chklst_Target.Items)
            //{
            //    listObj.Add(obj);
            //}

            //foreach (int index in arr)
            //{
            //    object objItem = chklst_Target.GetItem(index);
            //    listObj.Remove(objItem);
            //}

            //chklst_Target.Items.Clear();
            //chklst_Source.Items.Clear();

            //foreach (object objItem1 in listObj)
            //{
            //    chklst_Target.Items.Add(objItem1, false);
            //}

            //foreach (object objItem2 in srcObj)
            //{
            //    chklst_Source.Items.Add(objItem2, false);
            //}

            //ClearCheckBoxesAll();
            #endregion
        }

        public void RefreshDataSources()
        {
            chklst_Source.UnCheckAll();
            chklst_Target.UnCheckAll();
        }

        private void chkSrc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSrc.Checked)
            {
                //for (int i = 0; i < chklst_Source.ItemCount; i++)
                //{
                //    chklst_Source.SetItemChecked(i, true);
                //}
                chklst_Source.CheckAll();
            }
            else
            {
                //for (int i = 0; i < chklst_Source.ItemCount ; i++)
                //{
                //    chklst_Source.SetItemChecked(i, false);
                //}
                chklst_Source.UnCheckAll();
            }
        }

        private void chkTarget_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTarget.Checked)
            {
                //for (int i = 0; i < chklst_Target.ItemCount; i++)
                //{
                //    chklst_Target.SetItemChecked(i, true);
                //}
                chklst_Target.CheckAll();
            }
            else
            {
                chklst_Target.UnCheckAll();
                //for (int i = 0; i < chklst_Target.ItemCount; i++)
                //{
                //    chklst_Target.SetItemChecked(i, false);
                //}
            }
        }

        private void btnAddtoInventory_Click(object sender, EventArgs e)
        {
            if (chklst_Target.ItemCount <= 0)
                return;


            string msgVal = "";
            bool bComplete = true;
            Weight wItem = new Weight();
            
            //wItem = m_weightDao.GetById(cUtils.ConvertToInteger(leditUnit2.EditValue)); //cboUnit2.SelectedItem as Weight;
            if (leditUnit2.Enabled)
                wItem = leditUnit2.GetSelectedDataRow() as Weight;
            else
                wItem = null;

            if (txtItemName.Text.Trim() == "")
            {
                bComplete = false;
                msgVal = "You have not specified an Item Name!\n Continue?";
            }
            else if (txtUnit1.Text.Trim() == "" && wItem == null)//see if user selected an item weight
            {
                bComplete = false;
                msgVal = "You have not specified a Unit!\n Continue?";
            }
            else if (txtUnit1.Text.Trim() != "" && wItem != null)//prompt the user that the unit will be used is pieces/unit1
            {
                bComplete = false;
                msgVal = "You have specified a Unit1, this will be used and Unit2 value will be disregarded.\n Continue?";
                wItem = null;
            }

            if (!bComplete)
            {
                if (cUtils.ShowMessageQuestion(msgVal, "Confirmation") == DialogResult.No)
                {
                    return;
                }
            }

            inventory.btnAdd.PerformClick();

            var query = (from tblSource in chklst_Target.DataSource as IList<SortedSize>
                         select tblSource);
            foreach (SortedSize objItem in query)
            {
                inventory.tblData.Rows.Add(txtItemName.Text.Trim(),
                                            "",
                                            objItem,
                                            txtUnit1.Text.Trim(),
                                            wItem,
                                            "0.00",
                                            "0.00",
                                            "0.00",
                                            "0",
                                            "0",
                                            "0",
                                            "",
                                            true);

            }

            this.ParentForm.Close();
        }

        private void ClearCheckBoxesAll()
        {
            chkSrc.Checked = false;
            chkTarget.Checked = false;
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            ItemSize_Search(txtSearch.Text);
        }


        private void txtUnit1_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUnit1.Text.Trim().Length > 0)
            {
                //cboUnit2.Enabled = false;
                leditUnit2.Enabled = false;
            }
            else 
            {
                //cboUnit2.Enabled = true;
                leditUnit2.Enabled = true;
            }
        }

    }
}
