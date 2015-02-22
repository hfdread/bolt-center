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
using JimBoltCenter.Utils;
using DevExpress.XtraGrid.Views.Grid;

namespace JimBoltCenter.UI_Controls.Maintenance
{
    public partial class uctlManageSizes : UserControl
    {
        private ItemSizesDao _sizesDao = null;
        private BindingSource bSrc = null;
        private DataSet dsData = null;
        private DataTable tblData = null;

        private bool bAddSize;

        public uctlManageSizes()
        {
            InitializeComponent();
            _sizesDao = new ItemSizesDao();
        }

        private void btnAddSize_Click(object sender, EventArgs e)
        {
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();

            DataColumn dataCol = new DataColumn();
            dataCol.ColumnName = "Description";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new DataTable[] { tblData });

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol
            });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdvSize.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdSize.DataSource = bSrc;

            bAddSize = true;
            btnEditSize.Text = "&Save";
            btnDeleteSize.Text = "&Cancel";
            btnAddSize.Enabled = false;

            EditDataSource(true);
        }

        private void uctlManageSizes_Load(object sender, EventArgs e)
        {
            bAddSize = false;
            Skin.SetGridFont(grdvSize);
            Skin.SetGridSelectionColor(148, 183, 224, grdvSize);
            Skin.SetButtonFont(btnSearch);
            Skin.SetTextEditFont(txtSearch);

            btnSearch.PerformClick();
            EditDataSource();
        }

        private void btnEditSize_Click(object sender, EventArgs e)
        {
            if (btnEditSize.Text == "&Edit")//edit
            {
                btnEditSize.Text = "&Save";
                btnDeleteSize.Text = "&Cancel";
                btnAddSize.Enabled = false;

                EditDataSource(true);
            }
            else//save
            {
                IList<ItemSizes> sizeExist = new List<ItemSizes>();
                try
                {
                    if (bAddSize)//trx is to save added sizes
                    {
                        if (tblData.Rows.Count > 0)
                        {
                            foreach (DataRow row in tblData.Rows)
                            {
                                ItemSizes Size = new ItemSizes();
                                Size.Description = row[0].ToString().Trim();

                                _sizesDao.Save(Size);

                                if (Size.bSizeExist)
                                {
                                    sizeExist.Add(Size);
                                }
                            }
                        }
                        else
                        {
                            cUtils.ShowMessageExclamation("No size(s) to be saved.", "Save Size(s)");
                            return;
                        }

                        bAddSize = false;
                    }
                    else//edit size
                    {
                        sizeExist.Clear();//we make sure this variable is clear for edit operations
                        IList<ItemSizes> sizeList = (List<ItemSizes>)grdvSize.DataSource;
                        foreach (ItemSizes size in sizeList)
                        {
                            if (size.isDirty)
                            {
                                _sizesDao.EditSave(size);
                            }
                        }
                    }

                    if (sizeExist.Count == 0)
                        cUtils.ShowMessageInformation("Size(s) Saved Successfully!", "Save Size(s)");
                    else
                    {
                        string sFormat = "";
                        foreach (ItemSizes sz in sizeExist)
                        {
                            sFormat += string.Format(" - {0}\n", sz.Description);
                        }
                        cUtils.ShowMessageInformation(string.Format("Size(s) Saved Successfully!\n But there were size(s) that are duplicates, and were not saved:\n{0}", sFormat), "Save Size(s)");
                    }
                }
                catch (Exception ex)
                {
                    string error;
                    if (ex.InnerException != null)
                        error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Save Size(s)");
                }

                btnEditSize.Text = "&Edit";
                btnDeleteSize.Text = "&Delete";
                btnAddSize.Enabled = true;

                EditDataSource();
            }

            btnSearch.PerformClick();
        }

        //private void LoadItemSizeSource()
        //{
            
        //    grdSize.DataSource = _sizesDao.SearchSizes(txtSearch.Text);
        //}

        private void btnDeleteSize_Click(object sender, EventArgs e)
        {
            if (btnDeleteSize.Text == "&Cancel")//cancel
            {
                btnEditSize.Text = "&Edit";
                btnDeleteSize.Text = "&Delete";
                btnAddSize.Enabled = true;
                bAddSize = false;

                EditDataSource();
            }
            else//delete
            {
                ItemSizes size = grdvSize.GetRow(grdvSize.FocusedRowHandle) as ItemSizes;

                if (size == null)
                    return;

                if (cUtils.ShowMessageQuestion("Do you want to delete the selected size?", "Delete Item Size") == DialogResult.Yes)
                {
                    try
                    {
                        _sizesDao.Delete(size);
                    }
                    catch (Exception ex)
                    {
                        string error;
                        if (ex.InnerException != null)
                            error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                        else
                            error = string.Format("Error:\n{0}", ex.Message);

                        cUtils.ShowMessageError(error, "Delete Size");
                    }
                }
            }


            btnSearch.PerformClick();
        }

        private void grdvSize_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!bAddSize)
            {
                ItemSizes size = grdvSize.GetRow(e.RowHandle) as ItemSizes;

                if (size == null)
                    return;

                if (e.Column.FieldName == "Description")
                {
                    bool dirty = (bool)grdvSize.GetRowCellValue(e.RowHandle, "isDirty");

                    if (!dirty)
                        grdvSize.SetRowCellValue(e.RowHandle, "isDirty", true);
                }
            }
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvSize.Columns["Description"].OptionsColumn.AllowEdit = bEdit;
            grdvSize.Columns["Description"].OptionsColumn.AllowFocus = bEdit;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            grdvSize.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdSize.DataSource = _sizesDao.SearchSizes(txtSearch.Text);
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }
    }
}
