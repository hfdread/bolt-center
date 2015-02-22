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
using JimBoltCenter.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace JimBoltCenter.UI_Controls.Maintenance
{
    public partial class uctlTools : UserControl
    {
        private ForwardersDao mForwarderDao = null;
        private InvoiceTypeDao mInvoiceTypeDao = null;
        private ItemSizesDao mSizeDao = null;

        //binding source
        private BindingSource bSrc = null;
        private DataTable tblData = null;
        private DataSet dsData = null;

        private Forwarders m_forwarder { get; set; }
        private InvoiceType m_invoicetype { get; set; }

        //flags
        private bool bAddSize;
        private bool bAddForwarder;
        private bool bAddInvoiceType;

        public uctlTools()
        {
            InitializeComponent();
            mForwarderDao = new ForwardersDao();
            mInvoiceTypeDao = new InvoiceTypeDao();
            mSizeDao = new ItemSizesDao();
            bAddSize = false;
            bAddForwarder = false;
            bAddInvoiceType = false;
        }

        private void uctlTools_Load(object sender, EventArgs e)
        {
            //skins
            Skin.SetGridFont(grdvSize);
            Skin.SetGridFont(grdvForwarders);
            Skin.SetGridFont(grdvInvoice);
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
        }

        private void btnEditSize_Click(object sender, EventArgs e)
        {
            if (btnEditSize.Text == "&Edit")//edit
            {
                btnEditSize.Text = "&Save";
                btnDeleteSize.Text = "&Cancel";
                btnAddSize.Enabled = false;
            }
            else//save
            {
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
                                mSizeDao.Save(Size);
                            }
                        }
                        else
                        {
                            cUtils.ShowMessageExclamation("No size(s) to be saved.", "Save Size(s)");
                            return;
                        }

                        bAddSize = false;
                    }
                    else
                    {
                        IList<ItemSizes> sizeList = (List<ItemSizes>)grdvSize.DataSource;
                        foreach (ItemSizes size in sizeList)
                        {
                            if (size.isDirty)
                            {
                                mSizeDao.Save(size);
                            }
                        }
                    }

                    cUtils.ShowMessageInformation("Size(s) saved!", "Save Size(s)");
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
            }

            LoadItemSizeSource();
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

                    if(!dirty)
                        grdvSize.SetRowCellValue(e.RowHandle, "isDirty", true);
                }
            }
        }

        private void tabTools_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           if( e.Page.Name == "tbpgSizes")
           {
               LoadItemSizeSource();
           }
           else if (e.Page.Name == "tbpgForwarders")
           {
               LoadForwarderSource();
           }
           else if (e.Page.Name == "tbpgInvoiceType")
           {
               LoadInvoiceSource();
           }
        }

        private void btnDeleteSize_Click(object sender, EventArgs e)
        {
            if (btnDeleteSize.Text == "&Cancel")//cancel
            {
                btnEditSize.Text = "&Edit";
                btnDeleteSize.Text = "&Delete";
                btnAddSize.Enabled = true;
                bAddSize = false;
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
                        mSizeDao.Delete(size);
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

            LoadItemSizeSource();
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

            dsData.DataSetName = "NewDataSet1";
            tblData.TableName = "tblData1";

            dsData.Tables.AddRange(new DataTable[] { tblData });

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol1,
                dataCol2
            });

            bSrc.DataMember = "tblData1";
            bSrc.DataSource = dsData;

            grdvForwarders.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdForwarders.DataSource = bSrc;

            bAddForwarder = true;
            btnEditForwarder.Text = "&Save";
            btnDeleteForwarder.Text = "&Cancel";
            btnAddForwarder.Enabled = false;
        }

        private void btnEditForwarder_Click(object sender, EventArgs e)
        {
            if (btnEditForwarder.Text == "&Edit")//edit
            {
                btnEditForwarder.Text = "&Save";
                btnDeleteForwarder.Text = "&Cancel";
                btnAddForwarder.Enabled = false;
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
                                mForwarderDao.Save(forwarder);
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
                        foreach (Forwarders  F in forwarderList)
                        {
                            if (F.isDirty)
                            {
                                mForwarderDao.Save(F);
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
            }

            LoadForwarderSource();
        }

        private void btnDeleteForwarder_Click(object sender, EventArgs e)
        {
            if (btnDeleteForwarder.Text == "&Cancel")//cancel
            {
                btnEditForwarder.Text = "&Edit";
                btnDeleteForwarder.Text = "&Delete";
                btnAddForwarder.Enabled = true;
                bAddForwarder = false;
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
                        mForwarderDao.Delete(F);
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

        private void LoadForwarderSource()
        {
            grdvForwarders.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdForwarders.DataSource = mForwarderDao.getAllRecords();
        }

        private void LoadItemSizeSource()
        {
            grdvSize.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdSize.DataSource = mSizeDao.getAllRecords();
        }

        private void LoadInvoiceSource()
        {
            grdvInvoice.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            grdInvoice.DataSource = mInvoiceTypeDao.getAllRecords();
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

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();

            DataColumn dataCol1 = new DataColumn();
            DataColumn dataCol2 = new DataColumn();
            dataCol1.ColumnName = "Type";
            dataCol2.ColumnName = "Code";

            dsData.DataSetName = "NewDataSet2";
            tblData.TableName = "tblData2";

            dsData.Tables.AddRange(new DataTable[] { tblData });

            tblData.Columns.AddRange(new DataColumn[] {
                dataCol1,
                dataCol2
            });

            bSrc.DataMember = "tblData2";
            bSrc.DataSource = dsData;

            grdvInvoice.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdInvoice.DataSource = bSrc;

            bAddInvoiceType = true;
            btnEditInvoice.Text = "&Save";
            btnDeleteInvoice.Text = "&Cancel";
            btnAddInvoice.Enabled = false;
        }

        private void btnEditInvoice_Click(object sender, EventArgs e)
        {
            if (btnEditInvoice.Text == "&Edit")//edit
            {
                btnEditInvoice.Text = "&Save";
                btnDeleteInvoice.Text = "&Cancel";
                btnAddInvoice.Enabled = false;
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
                                mInvoiceTypeDao.Save(I);
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
                                mInvoiceTypeDao.Save(type);
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
            }

            LoadInvoiceSource();
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (btnDeleteInvoice.Text == "&Cancel")//cancel
            {
                btnEditInvoice.Text = "&Edit";
                btnDeleteInvoice.Text = "&Delete";
                btnAddInvoice.Enabled = true;
                bAddInvoiceType = false;
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
                        mInvoiceTypeDao.Delete(type);
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
       
    }
}
