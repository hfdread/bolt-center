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
    public partial class uctlManageForwarders : UserControl
    {
        private ForwardersDao _forwardersDao = null;

        private BindingSource bSrc = null;
        private DataSet dsData = null;
        private DataTable tblData = null;

        private bool bAddForwarder;

        public uctlManageForwarders()
        {
            InitializeComponent();

            _forwardersDao = new ForwardersDao();
        }

        private void uctlManageForwarders_Load(object sender, EventArgs e)
        {
            Skin.SetGridFont(grdvForwarders);
            Skin.SetGridSelectionColor(148, 183, 224, grdvForwarders);
            bAddForwarder = false;

            LoadForwarderSource();
            EditDataSource();
        }

        private void btnAddForwarder_Click(object sender, EventArgs e)
        {
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();

            DataColumn dataCol1 = new DataColumn();
            DataColumn dataCol2 = new DataColumn();
            dataCol1.ColumnName = "CompanyName";
            dataCol2.ColumnName = "Details";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new DataTable[] { tblData });

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol1,
                dataCol2
            });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdvForwarders.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdForwarders.DataSource = bSrc;

            bAddForwarder = true;
            btnEditForwarder.Text = "&Save";
            btnDeleteForwarder.Text = "&Cancel";
            btnAddForwarder.Enabled = false;

            EditDataSource(true);
        }

        private void btnEditForwarder_Click(object sender, EventArgs e)
        {
            if (btnEditForwarder.Text == "&Edit")//edit
            {
                btnEditForwarder.Text = "&Save";
                btnDeleteForwarder.Text = "&Cancel";
                btnAddForwarder.Enabled = false;

                EditDataSource(true);
            }
            else//save
            {
                try
                {
                    if (bAddForwarder)//trx is to save added forwarders
                    {
                        if (tblData.Rows.Count > 0)
                        {
                            foreach (DataRow row in tblData.Rows)
                            {
                                Forwarders forwarder = new Forwarders();
                                forwarder.CompanyName = row[0].ToString().Trim();
                                forwarder.Details = row[1].ToString();
                                _forwardersDao.Save(forwarder);
                            }
                        }
                        else
                        {
                            cUtils.ShowMessageExclamation("No forwarder(s) to be saved.", "Save Forwarder(s)");
                            return;
                        }

                        bAddForwarder = false;
                    }
                    else  //save edite forwarders
                    {
                        IList<Forwarders> forwarderList = (List<Forwarders>)grdForwarders.DataSource;
                        foreach (Forwarders F in forwarderList)
                        {
                            if (F.isDirty)
                            {
                                _forwardersDao.Save(F);
                            }
                        }
                    }

                    cUtils.ShowMessageInformation("Forwarder(s) saved!", "Save Forwarder(s)");
                }
                catch (Exception ex)
                {
                    string error;
                    if (ex.InnerException != null)
                        error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Save Forwarder(s)");
                }

                btnEditForwarder.Text = "&Edit";
                btnDeleteForwarder.Text = "&Delete";
                btnAddForwarder.Enabled = true;

                EditDataSource();
            }

            LoadForwarderSource();
        }

        private void LoadForwarderSource()
        {
            grdvForwarders.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdForwarders.DataSource = _forwardersDao.getAllRecords();
        }

        private void btnDeleteForwarder_Click(object sender, EventArgs e)
        {
            if (btnDeleteForwarder.Text == "&Cancel")//cancel
            {
                btnEditForwarder.Text = "&Edit";
                btnDeleteForwarder.Text = "&Delete";
                btnAddForwarder.Enabled = true;
                bAddForwarder = false;

                EditDataSource();
            }
            else//delete
            {
                Forwarders F = grdvForwarders.GetRow(grdvForwarders.FocusedRowHandle) as Forwarders;

                if (F == null)
                    return;

                if (cUtils.ShowMessageQuestion("Do you want to delete the selected Forwarder?", "Delete Forwarder") == DialogResult.Yes)
                {
                    try
                    {
                        _forwardersDao.Delete(F);
                    }
                    catch (Exception ex)
                    {
                        string error;
                        if (ex.InnerException != null)
                            error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                        else
                            error = string.Format("Error:\n{0}", ex.Message);

                        cUtils.ShowMessageError(error, "Delete Forwarder");
                    }
                }
            }

            LoadForwarderSource();
        }

        private void grdvForwarders_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!bAddForwarder)
            {
                Forwarders F = grdvForwarders.GetRow(e.RowHandle) as Forwarders;

                if (F == null)
                    return;

                if (e.Column.FieldName == "CompanyName" || e.Column.FieldName == "Details")
                {
                    bool dirty = (bool)grdvForwarders.GetRowCellValue(e.RowHandle, "isDirty");

                    if (!dirty)
                        grdvForwarders.SetRowCellValue(e.RowHandle, "isDirty", true);
                }
            }
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvForwarders.Columns["CompanyName"].OptionsColumn.AllowEdit = bEdit;
            grdvForwarders.Columns["CompanyName"].OptionsColumn.AllowFocus = bEdit;

            grdvForwarders.Columns["Details"].OptionsColumn.AllowEdit = bEdit;
            grdvForwarders.Columns["Details"].OptionsColumn.AllowFocus = bEdit;

        }
    }
}
