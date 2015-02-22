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
    public partial class uctlManageInvoiceType : UserControl
    {
        private InvoiceTypeDao _invoicetypeDao = null;
        private BindingSource bSrc = null;
        private DataSet dsData = null;
        private DataTable tblData = null;

        private bool bAddInvoiceType;
        public uctlManageInvoiceType()
        {
            InitializeComponent();

            _invoicetypeDao = new InvoiceTypeDao();
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();

            DataColumn dataCol1 = new DataColumn();
            DataColumn dataCol2 = new DataColumn();
            dataCol1.ColumnName = "Type";
            dataCol2.ColumnName = "Code";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new DataTable[] { tblData });

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol1,
                dataCol2
            });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdvInvoice.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdInvoice.DataSource = bSrc;

            bAddInvoiceType = true;
            btnEditInvoice.Text = "&Save";
            btnDeleteInvoice.Text = "&Cancel";
            btnAddInvoice.Enabled = false;

            EditDataSource(true);
        }

        private void uctlManageInvoiceType_Load(object sender, EventArgs e)
        {
            bAddInvoiceType = false;
            Skin.SetGridFont(grdvInvoice);
            Skin.SetGridSelectionColor(148, 183, 224, grdvInvoice);

            LoadInvoiceSource();
            EditDataSource();
        }

        private void btnEditInvoice_Click(object sender, EventArgs e)
        {
            if (btnEditInvoice.Text == "&Edit")//edit
            {
                btnEditInvoice.Text = "&Save";
                btnDeleteInvoice.Text = "&Cancel";
                btnAddInvoice.Enabled = false;

                EditDataSource(true);
            }
            else//save
            {
                try
                {
                    if (bAddInvoiceType)//trx is to save added sizes
                    {
                        if (tblData.Rows.Count > 0)
                        {
                            foreach (DataRow row in tblData.Rows)
                            {
                                InvoiceType I = new InvoiceType();
                                I.Type = row[0].ToString().Trim();
                                I.Code = row[1].ToString().Trim();
                                _invoicetypeDao.Save(I);
                            }
                        }
                        else
                        {
                            cUtils.ShowMessageExclamation("No Invoice Type(s) to be saved.", "Save Invoice Type(s)");
                            return;
                        }

                        bAddInvoiceType = false;
                    }
                    else
                    {
                        IList<InvoiceType> typeList = (List<InvoiceType>)grdvInvoice.DataSource;
                        foreach (InvoiceType type in typeList)
                        {
                            if (type.isDirty)
                            {
                                _invoicetypeDao.Save(type);
                            }
                        }
                    }

                    cUtils.ShowMessageInformation("Invoice Type(s) saved!", "Save Invoice Type(s)");
                }
                catch (Exception ex)
                {
                    string error;
                    if (ex.InnerException != null)
                        error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Save Invoice Type(s)");
                }

                btnEditInvoice.Text = "&Edit";
                btnDeleteInvoice.Text = "&Delete";
                btnAddInvoice.Enabled = true;

                EditDataSource();
            }

            LoadInvoiceSource();
        }

        private void LoadInvoiceSource()
        {
            grdvInvoice.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdInvoice.DataSource = _invoicetypeDao.getAllRecords();
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (btnDeleteInvoice.Text == "&Cancel")//cancel
            {
                btnEditInvoice.Text = "&Edit";
                btnDeleteInvoice.Text = "&Delete";
                btnAddInvoice.Enabled = true;
                bAddInvoiceType = false;

                EditDataSource();
            }
            else//delete
            {
                InvoiceType type = grdvInvoice.GetRow(grdvInvoice.FocusedRowHandle) as InvoiceType;

                if (type == null)
                    return;

                if (cUtils.ShowMessageQuestion("Do you want to delete the selected invoice type?", "Delete Invoice Type") == DialogResult.Yes)
                {
                    try
                    {
                        _invoicetypeDao.Delete(type);
                    }
                    catch (Exception ex)
                    {
                        string error;
                        if (ex.InnerException != null)
                            error = string.Format("Error:\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                        else
                            error = string.Format("Error:\n{0}", ex.Message);

                        cUtils.ShowMessageError(error, "Delete Invoice Type");
                    }
                }
            }

            LoadInvoiceSource();
        }

        private void grdvInvoice_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!bAddInvoiceType)
            {
                InvoiceType type = grdvInvoice.GetRow(e.RowHandle) as InvoiceType;

                if (type == null)
                    return;

                if (e.Column.FieldName == "Type" || e.Column.FieldName == "Code")
                {
                    bool dirty = (bool)grdvInvoice.GetRowCellValue(e.RowHandle, "isDirty");

                    if (!dirty)
                        grdvInvoice.SetRowCellValue(e.RowHandle, "isDirty", true);
                }
            }
        }

        private void EditDataSource(bool bEdit = false)
        {
            grdvInvoice.Columns["Type"].OptionsColumn.AllowEdit = bEdit;
            grdvInvoice.Columns["Type"].OptionsColumn.AllowFocus = bEdit;

            grdvInvoice.Columns["Code"].OptionsColumn.AllowEdit = bEdit;
            grdvInvoice.Columns["Code"].OptionsColumn.AllowFocus = bEdit;
        }
    }
}
