using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;

namespace JimBoltCenter.UI_Controls.Maintenance
{
    public partial class uctlManageWeight : UserControl
    {
        private WeightDao _weightDao = null;
        private BindingSource bSrc = null;
        private DataSet dsData = null;
        private DataTable tblData = null;

        private bool bAddWeight;

        private enum eFld
        {
            ID=0,
            Measurement,
            Converter,
            Description
        }
        
        public uctlManageWeight()
        {
            InitializeComponent();
            _weightDao = new WeightDao();
        }

        private void uctlManageWeight_Load(object sender, EventArgs e)
        {
            bAddWeight = false;
            Skin.SetGridFont(grdvWeight);
            Skin.SetGridSelectionColor(148, 183, 224, grdvWeight);
            Skin.SetButtonFont(btnAdd);
            Skin.SetButtonFont(btnEdit);
            Skin.SetButtonFont(btnDelete);

            LoadWeightSource();
            EditDataSource();
        }

        private void LoadWeightSource()
        {
            grdvWeight.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdWeight.DataSource = _weightDao.getAllRecords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();

            DataColumn dataCol1 = new DataColumn();
            DataColumn dataCol2 = new DataColumn();
            DataColumn dataCol4 = new DataColumn();
            DataColumn dataCol3 = new DataColumn();

            dataCol1.ColumnName = "ID";
            dataCol2.ColumnName = "Measurement";
            dataCol3.ColumnName = "Converter";
            dataCol4.ColumnName = "Description";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new DataTable[] { tblData });

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol1,
                dataCol2,
                dataCol3,
                dataCol4
            });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdvWeight.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdWeight.DataSource = bSrc;

            bAddWeight = true;
            btnEdit.Text = "&Save";
            btnDelete.Text = "&Cancel";
            btnAdd.Enabled = false;

            EditDataSource(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "&Edit")//edit
            {
                btnEdit.Text = "&Save";
                btnDelete.Text = "&Cancel";
                btnAdd.Enabled = false;

                EditDataSource(true);
            }
            else//save
            {
                try
                {
                    if (bAddWeight)//trx is to save added sizes
                    {
                        if (tblData.Rows.Count > 0)
                        {
                            foreach (DataRow row in tblData.Rows)
                            {
                                Weight W = new Weight();
                                W.Measurement = row[(int)eFld.Measurement].ToString().Trim();
                                W.Converter = cUtils.ConvertToInteger(row[(int)eFld.Converter].ToString().Trim());
                                W.Description = row[(int)eFld.Description].ToString().Trim();

                                _weightDao.Save(W);
                            }
                        }
                        else
                        {
                            cUtils.ShowMessageExclamation("No Weight(s) to be saved.", "Save Weight(s)");
                            return;
                        }

                        bAddWeight = false;
                    }
                    else
                    {
                        IList<Weight> weightList = (List<Weight>)grdvWeight.DataSource;
                        foreach (Weight w in weightList)
                        {
                            if (w.isDirty)
                            {
                                _weightDao.Save(w);
                            }
                        }
                    }

                    cUtils.ShowMessageInformation("Weight(s) saved!", "Save Weight(s)");
                }
                catch (Exception ex)
                {
                    string error;
                    if (ex.InnerException != null)
                        error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Save Weight(s)");
                }

                btnEdit.Text = "&Edit";
                btnDelete.Text = "&Delete";
                btnAdd.Enabled = true;

                EditDataSource();
            }

            LoadWeightSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "&Cancel")//cancel
            {
                btnEdit.Text = "&Edit";
                btnDelete.Text = "&Delete";
                btnAdd.Enabled = true;
                bAddWeight = false;

                EditDataSource();
            }
            else//delete
            {
                Weight weight = grdvWeight.GetRow(grdvWeight.FocusedRowHandle) as Weight;

                if (weight == null)
                    return;

                if (cUtils.ShowMessageQuestion("Do you want to delete the selected Weight?", "Delete Weight") == DialogResult.Yes)
                {
                    try
                    {
                        _weightDao.Delete(weight);
                    }
                    catch (Exception ex)
                    {
                        string error;
                        if (ex.InnerException != null)
                            error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                        else
                            error = string.Format("Error:\n{0}", ex.Message);

                        cUtils.ShowMessageError(error, "Delete Weight");
                    }
                }
            }

            LoadWeightSource();
        }

        private void grdvWeight_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!bAddWeight)
            {
                Weight weight = grdvWeight.GetRow(e.RowHandle) as Weight;

                if (weight == null)
                    return;

                if (e.Column.FieldName == "Description" || e.Column.FieldName == "Converter" || e.Column.FieldName == "Measurement")
                {
                    bool dirty = (bool)grdvWeight.GetRowCellValue(e.RowHandle, "isDirty");

                    if (!dirty)
                        grdvWeight.SetRowCellValue(e.RowHandle, "isDirty", true);
                }
            }
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvWeight.Columns[(int)eFld.Measurement].OptionsColumn.AllowEdit = bEdit;
            grdvWeight.Columns[(int)eFld.Measurement].OptionsColumn.AllowFocus = bEdit;

            grdvWeight.Columns[(int)eFld.Converter].OptionsColumn.AllowEdit = bEdit;
            grdvWeight.Columns[(int)eFld.Converter].OptionsColumn.AllowFocus = bEdit;

            grdvWeight.Columns[(int)eFld.Description].OptionsColumn.AllowEdit = bEdit;
            grdvWeight.Columns[(int)eFld.Description].OptionsColumn.AllowFocus = bEdit;
        }

    }
}
