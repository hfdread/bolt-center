using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JimBoltCenter.Forms;
using JimBoltCenter.Utils;
using JimBoltCenter.UI_Controls.Maintenance;
using DBMapping.BOL;
using DBMapping.DAL;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using JimBoltCenter.Reports;


namespace JimBoltCenter.UI_Controls.Viewers
{
    public partial class uctlViewInventory : UserControl
    {
        private BindingSource bSrc = null;
        private bool bIsloading;
        private DataSet dsData = null;
        public DataTable tblData = null;
        private ItemDao itemDao = null;
        private ItemSizesDao sizeDao = null;
        private WeightDao weightDao = null;

        private bool bFromAdd;
        private bool bView;

        //public vars
        public int rowHandle;

        private enum eFlds
        {
            Name = 0,
            Code,
            Size,
            Unit,
            Unit2,
            RetailPrice,
            UnitPrice,
            UnitPrice2,
            LastPrice,
            OnHand,
            OnHandWeight,
            LowThreshold,
            UpdateDate,
            isDirty
        }

        public uctlViewInventory()
        {
            InitializeComponent();
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();
            itemDao = new ItemDao();
            sizeDao = new ItemSizesDao();
            weightDao = new WeightDao();

            bIsloading = true;
            bFromAdd = false;
            rowHandle = -1;
        }

        private void uctlViewInventory_Load(object sender, EventArgs e)
        {
            bIsloading = false;
            bView = true;
            mruItemName.Focus();
            Skin.SetGridFont(grdvItems);
            Skin.SetGridSelectionColor(148, 183, 224, grdvItems);
            Skin.SetButtonFont(btnMultipleSizes);
            Skin.SetButtonFont(btnChangeName);
            Skin.SetButtonFont(btnConvert);
            Skin.SetLookUpEditFont(leditSize);
            Skin.SetTextEditFont(txtsUnit);

            LoadSizeChoices();
            btnMerge.Enabled = false;
            btnSearch.PerformClick();
        }

        private void LoadNameAutoSuggest()
        {
            repoCboName.Items.Clear();
            IList<string> sNameList = new List<string>();
            sNameList = itemDao.GetItemNames();

            repoCboName.Items.AddRange(sNameList.ToArray<string>());
            //foreach (string sName in sNameList)
            //{
            //    repoCboName.Items.Add(sName);
            //}
        }

        private void LoadSizeChoices()
        {
            leditSize.Properties.DisplayMember = "Description";
            leditSize.Properties.ValueMember = "ID";
            leditSize.EditValue = "";

            IList<ItemSizes> lst = new List<ItemSizes>();
            lst = sizeDao.getAllRecords();
            IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(lst);

            leditSize.Properties.DataSource = sortedSizeList;

            #region old code
            //cboItemSizes.Properties.Items.Clear();
            
            //IList<ItemSizes> lst = new List<ItemSizes>();
            //lst = sizeDao.getAllRecords();
            //IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(lst);

            
            //foreach (SortedSize sort in sortedSizeList)
            //{
            //    cboItemSizes.Properties.Items.Add(sizeDao.GetByDescription(sort.Description));
            //}
            #endregion
        }

        private void LoadSizeData(bool bItemAsString = false)
        {
            repoCbo.Items.Clear();
            IList<ItemSizes> lst = sizeDao.getAllRecords();
            //IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(lst);

            //foreach (SortedSize sort in sortedSizeList)
            //{
            //    if (bItemAsString)
            //        repoCbo.Items.Add(sort.Description.Trim());
            //    else
            //        repoCbo.Items.Add(sizeDao.GetById(sort.ID));
            //}
            foreach (ItemSizes size in lst)
            {
                if (bItemAsString)
                    repoCbo.Items.Add(size.ToString());
                else
                    repoCbo.Items.Add(size);
            }
        }

        private void LoadWeightData()
        {
            repoCbo2.Items.Clear();
            IList<Weight> lst = weightDao.getAllRecords();

            foreach (Weight W in lst)
            {
                repoCbo2.Items.Add(W.ToString());
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "&Edit")
            {
                EditDataSource(true);
                btnEdit.Text = "&Save";
                btnDelete.Text = "&Cancel";
                btnAdd.Enabled = false;
                bView = false;
                LoadNameAutoSuggest();
                LoadSizeData();
                LoadWeightData();
            }
            else //save
            {
                if (bFromAdd)//add item(s)
                {
                    IList<Item> lst = new List<Item>();

                    foreach (DataRow row in tblData.Rows)
                    {
                        //if (Convert.ToBoolean(row[(int)eFlds.isDirty].ToString()) == true)//check if row is added
                        //{
                        ItemSizes sizeCheck = sizeDao.GetByDescription(row[(int)eFlds.Size].ToString().Trim());
                        if (row[(int)eFlds.Name].ToString().Trim() != "")// && sizeCheck != null)
                        {
                            Item I = new Item();
                            I.Name = row[(int)eFlds.Name].ToString();
                            I.Description = I.Name;
                            I.Code = row[(int)eFlds.Code].ToString();
                            I.Unit = row[(int)eFlds.Unit].ToString();
                            if(sizeCheck != null)
                                I.Size = sizeCheck;
                            I.RetailPrice = cUtils.ConvertToDouble(row[(int)eFlds.RetailPrice].ToString());
                            I.UnitPrice = cUtils.ConvertToDouble(row[(int)eFlds.UnitPrice].ToString());
                            I.UnitPrice2 = cUtils.ConvertToDouble(row[(int)eFlds.UnitPrice2].ToString());
                            I.OnHand = cUtils.ConvertToInteger(row[(int)eFlds.OnHand].ToString());
                            I.CreateDate = DateTime.Now;
                            I.UpdateDate = I.CreateDate;
                            I.LowThreshold = cUtils.ConvertToInteger(row[(int)eFlds.LowThreshold].ToString());
                            I.Unit2 = row[(int)eFlds.Unit2].ToString();
                            I.OnHandWeight = cUtils.ConvertToInteger(row[(int)eFlds.OnHandWeight].ToString());

                            lst.Add(I);//add item to list
                        }
                        //}
                    }
                    
                    if (lst.Count > 0)
                    {
                        try
                        {
                            //get duplicate items
                            StringBuilder sbDuplicates = new StringBuilder();
                            foreach (Item i in lst)
                            {
                                Item temp = new Item();
                                if (i.Size != null)
                                {
                                    if (i.Unit.Trim() != "")
                                        temp = itemDao.GetItem(i, false);
                                    else
                                        temp = itemDao.GetItem(i, true);

                                    if (temp != null)
                                        sbDuplicates.AppendFormat("-{0} {1}\n", i.Name, i.Size.Description);
                                }
                                else
                                {
                                    temp = itemDao.GetItem(i.Name);
                                    if (temp != null)
                                        sbDuplicates.AppendFormat("-{0}\n", i.Name);
                                }
                            }

                            itemDao.SaveBatch(lst);

                            if (sbDuplicates.Length > 0)
                            {
                                cUtils.ShowMessageInformation(string.Format("Item(s) Added Successfully!\nExcept Items below, they are already existing.\n{0}", sbDuplicates.ToString())
                                                            , "Item Registration");
                            }
                            else
                            {
                                cUtils.ShowMessageInformation("Item(s) Added Successfully!", "Item Registration");
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = "";
                            if (ex.InnerException != null)
                                error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                            else
                                error = string.Format("Error:\n{0}", ex.Message);

                            cUtils.ShowMessageError(error, "Item Registration");
                            cUtils.CreateCrashLog(error, "Item Registration", "Inventory");
                        }
                    }

                    grdvItems.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    grdItems.ContextMenuStrip = null;
                }
                else//edit item(s)
                {
                    IList<Item> items = (List<Item>)grdItems.DataSource;
                    try
                    {
                        foreach (Item I in items)
                        {
                            if (I.isDirty)
                            {
                                I.UpdateDate = DateTime.Now;
                                itemDao.Save(I);
                            }
                        }

                        cUtils.ShowMessageInformation("Item(s) Updated Successfully!", "Item Update");
                    }
                    catch (Exception ex)
                    {
                        string error = "";
                        if (ex.InnerException != null)
                            error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                        else
                            error = string.Format("Error:\n{0}", ex.Message);

                        cUtils.ShowMessageError(error, "Item Update");
                        cUtils.CreateCrashLog(error, "Item Update", "Inventory");
                    }
                }

                ResetVariables();
                btnSearch.PerformClick();
                EditDataSource();
                btnEdit.Text = "&Edit";
                btnDelete.Text = "&Delete";
                btnAdd.Enabled = true;
                bView = true;
            }
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvItems.Columns["Name"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["Name"].OptionsColumn.AllowFocus = bEdit;
            //code
            grdvItems.Columns["Code"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["Code"].OptionsColumn.AllowFocus = bEdit;
            //unit
            grdvItems.Columns["Unit"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["Unit"].OptionsColumn.AllowFocus = bEdit;
            //size
            grdvItems.Columns["Size"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["Size"].OptionsColumn.AllowFocus = bEdit;
            //unit price
            grdvItems.Columns["UnitPrice"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["UnitPrice"].OptionsColumn.AllowFocus = bEdit;
            //on hand
            grdvItems.Columns["OnHand"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["OnHand"].OptionsColumn.AllowFocus = bEdit;

            //threshold
            grdvItems.Columns["LowThreshold"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["LowThreshold"].OptionsColumn.AllowFocus = bEdit;

            //unit2
            grdvItems.Columns["Unit2"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["Unit2"].OptionsColumn.AllowFocus = bEdit;

            //onhandweight
            grdvItems.Columns["OnHandWeight"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["OnHandWeight"].OptionsColumn.AllowFocus = bEdit;

            grdvItems.Columns["UnitPrice2"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["UnitPrice2"].OptionsColumn.AllowFocus = bEdit;

            //retail price
            grdvItems.Columns["RetailPrice"].OptionsColumn.AllowEdit = bEdit;
            grdvItems.Columns["RetailPrice"].OptionsColumn.AllowFocus = bEdit;

            //dirty 
            grdvItems.Columns["isDirty"].OptionsColumn.AllowEdit = bEdit;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "&Delete")
            {
                Item I = grdvItems.GetRow(grdvItems.FocusedRowHandle) as Item;

                if (I == null)
                    return;

                bool bItemHistoryCheck = itemDao.CheckItemHistory(I);

                if (bItemHistoryCheck)
                {
                    cUtils.ShowMessageInformation("Item already has a transaction history.\nYou can not delete this item.", "Delete Item");
                    return;
                }

                string msgFormat = string.Format("Do you want to delete item {0}", I.Name);
                if (cUtils.ShowMessageQuestion(msgFormat, "Delete Item") == DialogResult.Yes)
                {
                    try
                    {
                        itemDao.Delete(I);
                        btnSearch.PerformClick();
                    }
                    catch(Exception ex)
                    {
                        cUtils.ShowMessageError(ex.Message, "Delete Item");
                        cUtils.CreateCrashLog(ex.Message, "Delete Item", "Inventory");
                    }
                }
                
            }
            else //cancel
            {
                EditDataSource();
                btnDelete.Text = "&Delete";
                btnEdit.Text = "&Edit";
                btnAdd.Enabled = true;
                bView = true;
                ResetVariables();

                grdItems.ContextMenuStrip = null;
                grdvItems.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                btnSearch.PerformClick();
            }
        }

        private void ResetVariables()
        {
            //default values
            bFromAdd = false;
            rowHandle = -1;
        }

        private void grdvItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bIsloading)
                return;

            if (!bFromAdd)
            {
                Item I = grdvItems.GetRow(e.RowHandle) as Item;

                if (I == null)
                    return;

                if (e.Column.FieldName == "Name" || e.Column.FieldName == "Code" || e.Column.FieldName == "Unit" ||
                    e.Column.FieldName == "Size" || e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "OnHand" ||
                    e.Column.FieldName == "LowThreshold" || e.Column.FieldName == "Unit2" || 
                    e.Column.FieldName == "OnHandWeight" || e.Column.FieldName == "UnitPrice2" || e.Column.FieldName == "RetailPrice")
                {
                    bool dirty = (bool)grdvItems.GetRowCellValue(e.RowHandle, "isDirty");

                    if (!dirty)
                    {
                        grdvItems.SetRowCellValue(e.RowHandle, "isDirty", true);
                        rowHandle = e.RowHandle;
                        grdvItems.RefreshRow(e.RowHandle);
                    }
                }
            }
        }

        private void grdvItems_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (bIsloading)
                return;

            if (!bFromAdd)
            {
                if (e.Column.FieldName == "UnitPrice")
                {
                    string sUnit = grdvItems.GetRowCellValue(e.RowHandle, "Unit").ToString();
                    double newPrice = Convert.ToDouble(grdvItems.GetRowCellValue(e.RowHandle, "UnitPrice"));
                    double lastPrice = Convert.ToDouble(grdvItems.GetRowCellValue(e.RowHandle, "LastPrice"));

                    if (sUnit.Trim() != "")
                    {

                        if (newPrice > lastPrice)
                            e.Appearance.ForeColor = Color.Green;
                        else if (newPrice < lastPrice)
                            e.Appearance.ForeColor = Color.Red;
                        else if (newPrice == lastPrice)
                            e.Appearance.ForeColor = Color.Black;
                    }
                }
                else if (e.Column.FieldName == "UnitPrice2")
                {
                    string sUnit = grdvItems.GetRowCellValue(e.RowHandle, "Unit2").ToString();
                    double newPrice = Convert.ToDouble(grdvItems.GetRowCellValue(e.RowHandle, "UnitPrice2"));
                    double lastPrice = Convert.ToDouble(grdvItems.GetRowCellValue(e.RowHandle, "LastPrice"));

                    if (sUnit.Trim() != "")
                    {

                        if (newPrice > lastPrice)
                            e.Appearance.ForeColor = Color.Green;
                        else if (newPrice < lastPrice)
                            e.Appearance.ForeColor = Color.Red;
                        else if (newPrice == lastPrice)
                            e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ItemSizes selSize = new ItemSizes();
            //selSize = cboItemSizes.SelectedItem as ItemSizes;
            if (leditSize.Text != "")
                selSize = sizeDao.GetById(cUtils.ConvertToInteger(leditSize.EditValue.ToString()));
            else
                selSize = null;

            if (mruItemName.Text.Trim() == "" && selSize == null && txtsUnit.Text.Trim() == "")//all search
            {
                grdItems.DataSource = itemDao.GetAllRecordsOrdered();
            }
            else if(mruItemName.Text.Trim() != "" && selSize != null && txtsUnit.Text.Trim() == "")//item and size
            {
                grdItems.DataSource = itemDao.GetSearchedItem(mruItemName.Text, selSize);
            }
            else if (mruItemName.Text.Trim() != "" && selSize == null && txtsUnit.Text.Trim() == "")//item
            {
                grdItems.DataSource = itemDao.GetSearchedItem2(mruItemName.Text);
            }
            else if (mruItemName.Text.Trim() == "" && selSize != null && txtsUnit.Text.Trim() == "" )//size
            {
                grdItems.DataSource = itemDao.GetSearchedItem(selSize);
            }
            else if (mruItemName.Text.Trim() != "" && selSize != null && txtsUnit.Text.Trim() != "")//item, size and unit
            {
                grdItems.DataSource = itemDao.GetSearchedItem(mruItemName.Text, selSize, txtsUnit.Text);
            }
            else if (mruItemName.Text.Trim() != "" && selSize == null && txtsUnit.Text.Trim() != "")//item and unit
            {
                grdItems.DataSource = itemDao.GetSearchedItem(mruItemName.Text, txtsUnit.Text);
            }
            else if (mruItemName.Text.Trim() == "" && selSize != null && txtsUnit.Text.Trim() != "")//size and unit
            {
                grdItems.DataSource = itemDao.GetSearchedItem(selSize, txtsUnit.Text);
            }
            else if (mruItemName.Text.Trim() == "" && selSize == null && txtsUnit.Text.Trim() != "")
            {
                grdItems.DataSource = itemDao.GetSearchItemByUnit(txtsUnit.Text);
            }

        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (!bFromAdd)
            {
                grdItems.ContextMenuStrip = cmMenu;
                btnAdd.Enabled = false;
                btnEdit.Text = "&Save";
                btnDelete.Text = "&Cancel";
                LoadSizeData(true);
                LoadWeightData();
                bFromAdd = true;
                bView = false;
            }

            tblData = new DataTable();
            dsData = new DataSet();
            
            //Columns
            DataColumn dataCol1 = new DataColumn();
            DataColumn dataCol2 = new DataColumn();
            DataColumn dataCol3 = new DataColumn();
            DataColumn dataCol4 = new DataColumn();
            DataColumn dataCol5 = new DataColumn();
            DataColumn dataCol6 = new DataColumn();
            DataColumn dataCol7 = new DataColumn();
            DataColumn dataCol8 = new DataColumn();
            DataColumn dataCol9 = new DataColumn();
            DataColumn dataCol10 = new DataColumn();
            DataColumn dataCol11 = new DataColumn();
            DataColumn dataCol12 = new DataColumn();
            DataColumn dataCol13 = new DataColumn();
            DataColumn dataCol14 = new DataColumn();

            dataCol1.ColumnName = "Name";
            dataCol2.ColumnName = "Code";
            dataCol3.ColumnName = "Unit";
            dataCol11.ColumnName = "Unit2";
            dataCol14.ColumnName = "RetailPrice";
            dataCol14.DataType = typeof(double);
            dataCol4.ColumnName = "Size";
            dataCol5.ColumnName = "UnitPrice";
            dataCol5.DataType = typeof(double);
            dataCol6.ColumnName = "LastPrice";
            dataCol7.ColumnName = "OnHand";
            dataCol12.ColumnName = "OnHandWeight";
            dataCol8.ColumnName = "UpdateDate";
            dataCol9.ColumnName = "isDirty";
            dataCol9.DataType = typeof(bool);
            dataCol10.ColumnName = "LowThreshold";
            dataCol13.ColumnName = "UnitPrice2";
            dataCol13.DataType = typeof(double);
            

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new System.Data.DataTable[] { tblData });

            tblData.Columns.AddRange(new System.Data.DataColumn[] { 
                dataCol1,
                dataCol2,
                dataCol4,
                dataCol3,
                dataCol11,
                dataCol14,
                dataCol5,
                dataCol13,
                dataCol6,
                dataCol7,
                dataCol12,
                dataCol10,
                dataCol8,
                dataCol9
            });
            
            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdItems.DataSource = null;
            grdvItems.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdItems.DataSource = bSrc;
            EditDataSource(true);
            LoadNameAutoSuggest();
        }

        private void mruItemName_TextChanged(object sender, EventArgs e)
        {
            if (mruItemName.Text.Trim().Length >= 2)
            {
                IList<string> nameList = new List<string>();
                //nameList = itemDao.GetItemNameSuggestion(mruItemName.Text.Trim());
                nameList = itemDao.GetUniqueItemNames(mruItemName.Text.Trim());
                foreach (string str in nameList)
                {
                    mruItemName.Properties.Items.Add(str);
                }
            }
        }

        private void mruItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnQuickAddSize_Click(object sender, EventArgs e)
        {
            uctlAddSizes uctl = new uctlAddSizes();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Quick Add Size";
            frm.ShowCtl(uctl);
            LoadSizeChoices();
        }

        private void cmMenu_Opening(object sender, CancelEventArgs e)
        {
            if (grdvItems.FocusedRowHandle < 0)
            {
                mnuDelete.Enabled = false;
            }
            else
            {
                mnuDelete.Enabled = true;
            }
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            tblData.Rows.RemoveAt(grdvItems.GetDataSourceRowIndex(grdvItems.FocusedRowHandle));
            grdItems.RefreshDataSource();
        }

        private void grdvItems_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (bIsloading)
                return;

            if (rowHandle == -1)
                return;

            if (!bFromAdd)
            {
                if (e.RowHandle == rowHandle)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        private void btnMultipleSizes_Click(object sender, EventArgs e)
        {
            //btnAdd.PerformClick();
            uctlMultiAddItem uctl = new uctlMultiAddItem();
            frmGenericPopup frm = new frmGenericPopup();

            frm.Text = "Add Multiple sizes";
            frm.ControlBox = true;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            uctl.inventory = this;
            frm.ShowCtl(uctl);

            //btnAdd.PerformClick();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            uctlItemBreakDown uctl = new uctlItemBreakDown();
            frmGenericPopup frm = new frmGenericPopup();

            int ID = (int)grdvItems.GetRowCellValue(grdvItems.FocusedRowHandle, "ID");
            uctl.weightItem = itemDao.GetById(ID);

            frm.Text = "Item Merge";
            frm.ShowCtl(uctl);

            btnSearch.PerformClick();
        }
        

        private void grdvItems_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (bView)
            {
                int ID = (int)grdvItems.GetRowCellValue(grdvItems.FocusedRowHandle, "ID");

                Item I = itemDao.GetById(ID);

                if (I != null)
                {
                    if (I.Unit2.Trim() != "" && I.OnHandWeight > 0)
                    {
                        btnConvert.Enabled = true;
                    }
                    else
                    {
                        btnConvert.Enabled = false;
                    }
                }
            }
        }

        private void cboItemSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            uctlNameChange uctl = new uctlNameChange();
            frmGenericPopup frm = new frmGenericPopup();

            IList<Item> selected = new List<Item>();
            foreach (int _rowhandle in grdvItems.GetSelectedRows())
            {
                Item i = grdvItems.GetRow(_rowhandle) as Item;
                selected.Add(i);
            }

            uctl.selItems = selected;
            frm.Text = "Item Name Change";
            frm.ShowCtl(uctl);

            btnSearch.PerformClick();
        }

        private void leditSize_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void txtsUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch.PerformClick();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptViewInventory rpt = new rptViewInventory();
            IList<Item> lstSource = grdvItems.DataSource as List<Item>;

            rpt.DataSource = lstSource;

            

            rpt.ShowPreviewDialog();
        }

        //private void uctlViewInventory_Enter(object sender, EventArgs e)
        //{
        //    btnSearch.PerformClick();
        //}

       
       
    }
}
