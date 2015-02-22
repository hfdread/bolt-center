using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using JimBoltCenter.Utils;
using DBMapping.BOL;
using DBMapping.DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlRetailInvoice : UserControl
    {
        private RetailSeries m_series;
        private RetailSeriesDao seriesDao;
        private RetailInvoiceDao rInvoiceDao;
        private RetailInvoice m_rInvoice;
        private ItemDao itemDao;

        private BindingSource bSrc = null;
        private DataTable tblData = null;
        private DataSet dsData = null;
        private bool bIsLoading;

        private enum eFlds
        { 
            _qty=0,
            itemname,
            itemsize,
            itemunit,
            price,
            subtotal,
            ID
        }

        public uctlRetailInvoice()
        {
            InitializeComponent();
            bIsLoading = true;
            m_series = new RetailSeries();
            seriesDao = new RetailSeriesDao();
            rInvoiceDao = new RetailInvoiceDao();
            itemDao = new ItemDao();
            m_rInvoice = new RetailInvoice();
        }

        private void uctlRetailInvoice_Load(object sender, EventArgs e)
        {
            Skin.SetTextEditFont(txtEnd);
            Skin.SetTextEditFont(txtStart);
            Skin.SetComboBoxEditFont(cboType);
            Skin.SetButtonFont(btnStart);
            Skin.SetButtonFont(btnStop);
            Skin.SetLabelFont(labelControl1);
            Skin.SetLabelFont(labelControl3);
            Skin.SetLabelFont(labelControl4);
            Skin.SetLabelFont(labelControl5);
            Skin.SetGridFont(grdvRetail);
            Skin.SetGridFont(grdvRetailDetails);
            Skin.SetButtonFont(btnSave);

            bIsLoading = false;
            btnSave.Enabled = false;
            //base source for details grid
            bSrc = new BindingSource();
            dsData = new DataSet();
            tblData = new DataTable();

            DataColumn dCol1 = new DataColumn();
            DataColumn dCol2 = new DataColumn();
            DataColumn dCol3 = new DataColumn();
            DataColumn dCol4 = new DataColumn();
            DataColumn dCol5 = new DataColumn();
            DataColumn dCol6 = new DataColumn();
            DataColumn dCol7 = new DataColumn();

            dCol1.ColumnName = "_qty";
            dCol2.ColumnName = "itemname";
            dCol3.ColumnName = "price";
            dCol4.ColumnName = "subtotal";
            dCol5.ColumnName = "ID";
            dCol6.ColumnName = "itemsize";
            dCol7.ColumnName = "itemunit";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new System.Data.DataTable[] { tblData });

            tblData.Columns.AddRange(new System.Data.DataColumn[] { 
                    dCol1,
                    dCol2,
                    dCol6,
                    dCol7,
                    dCol3,
                    dCol4,
                    dCol5,
                });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;

            grdRetailDetails.DataSource = bSrc;
            //+end details grid

            //load invoice type
            InvoiceTypeDao invTypeDao = new InvoiceTypeDao();
            IList<InvoiceType> lstType = invTypeDao.getAllRecords();

            foreach (InvoiceType item in lstType)
            {
                cboType.Properties.Items.Add(item);
            } 

            if (lstType.Count > 1)
                cboType.SelectedIndex = 0;
            
            //check any pending series
            RetailSeries seriesCheck = seriesDao.LoadLatestSeries();

            if (seriesCheck != null)
            {
                cboType.SelectedItem = seriesCheck.Type;
                dte1.DateTime = seriesCheck.dte;
                txtStart.Text = seriesCheck.Start_series.ToString(cUtils.FMT_ORNUMBER);
                txtEnd.Text = seriesCheck.End_series.ToString(cUtils.FMT_ORNUMBER);

                m_series = seriesCheck;
                btnStart.Enabled = false;

                //load sources for retail grids
                RefreshRetailDataGrids();

                EditDataSource(true);
                CreateRetailInvoice();

                //focus on current_in_stack
                grdvRetail.FocusedRowHandle = grdvRetail.RowCount - 1;
                btnSave.Enabled = true;
            }
            else
            {
                dte1.DateTime = DateTime.Now;
                btnStop.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ComputeTotalAmountDue();

            if (tblData.Rows.Count > 0)
            {
                m_rInvoice.details.Clear();
                foreach (DataRow row in tblData.Rows)
                {
                    RetailInvoiceDetails detail = new RetailInvoiceDetails();
                    if (row[(int)eFlds.ID].ToString().Trim() != "")
                        detail.ID = cUtils.ConvertToInteger(row[(int)eFlds.ID].ToString());

                    detail.ID = cUtils.ConvertToInteger(row[(int)eFlds.ID].ToString());
                    detail.retailinvoice = m_rInvoice;
                    detail.QTY = cUtils.ConvertToInteger(row[(int)eFlds._qty].ToString());
                    detail.UnitPrice = cUtils.ConvertToDouble(row[(int)eFlds.price].ToString());
                    detail.Amount = cUtils.ConvertToDouble(row[(int)eFlds.subtotal].ToString());

                    detail.item = itemDao.GetItem(row[(int)eFlds.itemname].ToString().Trim(),
                                                            row[(int)eFlds.itemsize].ToString().Trim(),
                                                            row[(int)eFlds.itemunit].ToString().Trim());

                    if (detail.item == null)//check if item has no size
                    {
                        detail.item = itemDao.CheckItemNoSize(row[(int)eFlds.itemname].ToString().Trim(), 
                                                            row[(int)eFlds.itemunit].ToString().Trim());
                    }

                    m_rInvoice.details.Add(detail);
                }

                try
                {
                    m_rInvoice.Customer = txtCustomer.Text;
                    m_rInvoice.Address = txtAddress.Text;
                    m_rInvoice.BusinessStyle = txtBusStyle.Text;
                    m_rInvoice.OSCA_PWD_ID = txtpwdID.Text;
                    m_rInvoice.Terms = txtTerms.Text;
                    m_rInvoice.TIN = txtTIN.Text;
                    m_rInvoice.VAT = cUtils.ConvertToDouble(txtTotalVat.Text);
                    m_rInvoice.TOTAL = cUtils.ConvertToDouble(txtTotalAmtDue.Text);

                    rInvoiceDao.SaveRetail(m_rInvoice);

                    cUtils.ShowMessageInformation(string.Format("Retail Invoice #{0} saved successfully!",m_rInvoice.or_number.ToString(cUtils.FMT_ORNUMBER)), "Retail Invoice Saved");

                    //only create the next retail invoice
                    //if the one being saved is the last in stack
                    if (m_rInvoice.or_number == m_series.Current)
                    {
                        //create next inline on the series
                        if (m_series.Current < m_series.End_series)
                        {
                            m_series.Current += 1;
                            seriesDao.SaveSeries(m_series);

                            CreateRetailInvoice();
                            RefreshRetailDataGrids();
                            grdvRetail.FocusedRowHandle = grdvRetail.RowCount - 1;
                            ResetData();
                        }
                    }
                    else
                    {
                        grdvRetail.FocusedRowHandle += 1;
                    }
                }
                catch (Exception ex)
                {
                    string error = "";
                    if (ex.InnerException != null)
                        error = string.Format("Error;\n{0}\n[Inner Exception]\n{1}", ex.Message, ex.InnerException.Message);
                    else
                        error = string.Format("Error:\n{0}", ex.Message);

                    cUtils.ShowMessageError(error, "Save Invoice");
                    cUtils.CreateCrashLog(error, "Save Invoice", "Stock In Invoice");
                }
            }
        }

        private void ResetData()
        {
            tblData.Rows.Clear();
            txtCustomer.Text = "";
            txtTIN.Text = "";
            txtAddress.Text = "";
            txtBusStyle.Text = "";
            txtTerms.Text = "Cash / Charge";
            txtpwdID.Text = "";
            txtTotalAmtDue.Text = "";
            txtTotalVat.Text = "";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string sMessage="";
            if (CheckValidFields(ref sMessage))
            {
                RetailSeries seriesCheck = seriesDao.GetSeries(cUtils.ConvertToInteger(txtStart.Text.Trim()));
                if(seriesCheck != null)
                {
                    if (cUtils.ShowMessageQuestion(string.Format("A series exist with range {0}-{1}.\nDo you want to edit this series?", seriesCheck.Start_series.ToString(cUtils.FMT_ORNUMBER), seriesCheck.End_series.ToString(cUtils.FMT_ORNUMBER))
                                        , "Series Already Exist") == DialogResult.Yes)
                    {
                        m_series = seriesCheck;
                        txtStart.Text = m_series.Start_series.ToString(cUtils.FMT_ORNUMBER);
                        txtEnd.Text = m_series.End_series.ToString(cUtils.FMT_ORNUMBER);
                    }
                    else
                    {
                        return;
                    }
                }
                else// if(m_series.ID <= 0)//new series
                {
                    //check if series is in collition
                    IList<RetailSeries> seriesList = seriesDao.CheckSeriesCollition(cUtils.ConvertToInteger(txtStart.Text.Trim()), cUtils.ConvertToInteger(txtEnd.Text.Trim()));
                    if (seriesList.Count > 0)
                    {
                        sMessage = "Series is in Collition with other series below:\n";
                        foreach (RetailSeries s in seriesList)
                        {
                            sMessage += string.Format("{0}-{1}\n", s.Start_series.ToString(cUtils.FMT_ORNUMBER), s.End_series.ToString(cUtils.FMT_ORNUMBER));
                        }

                        cUtils.ShowMessageExclamation(sMessage, "Series Collition");
                        txtStart.Focus();
                        return;
                    }
                    m_series.Start_series = cUtils.ConvertToInteger(txtStart.Text);
                    m_series.End_series = cUtils.ConvertToInteger(txtEnd.Text);
                    m_series.dte = dte1.DateTime;
                    m_series.Type = cboType.SelectedItem as InvoiceType;
                    m_series.Current = m_series.Start_series;

                    txtStart.Text = m_series.Start_series.ToString(cUtils.FMT_ORNUMBER);
                    txtEnd.Text = m_series.End_series.ToString(cUtils.FMT_ORNUMBER);
                }

                //if old series, just update the status
                m_series.Status = true;

                seriesDao.SaveSeries(m_series);

                //create or start again on the current series
                CreateRetailInvoice();

                btnStop.Enabled = true;
                btnStart.Enabled = false;
                btnSave.Enabled = true;

                //show all data for the series
                RefreshRetailDataGrids();

                EditDataSource(true);
                grdvRetail.FocusedRowHandle = 0;
            }
            else
            {
                cUtils.ShowMessageInformation(sMessage, "Field Error");
            }
        }

        public bool CheckValidFields(ref string sMessage)
        {
            bool bRet = true;

            if (txtStart.Text.Trim() == "")
            {
                sMessage = "Start series not valid";
                bRet = false;
            }
            else if (txtEnd.Text.Trim() == "")
            {
                sMessage = "End series not valid";
                bRet = false;
            }
            else if (cboType.SelectedIndex < 0)
            {
                sMessage = "Invoice Type not valid";
                bRet = false;
            }

            return bRet;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_series.Status = false;

            seriesDao.SaveSeries(m_series);
            EditDataSource(false);

            m_series = null;
            m_series = new RetailSeries();

            txtStart.Text = "";
            txtEnd.Text = "";
            grdRetail.DataSource = null;
            tblData.Rows.Clear();
            grdvRetail.FocusedRowHandle = -1;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnSave.Enabled = false;
        }

        private void CreateRetailInvoice()
        {
            if (m_series.Current > 0)
            {
                RetailInvoice rInvoice;

                rInvoice = rInvoiceDao.FindORNumber(m_series.Current);
                if (rInvoice == null)
                {
                    rInvoice = new RetailInvoice();
                    rInvoice.or_number = m_series.Current;
                    rInvoice.or_date = dte1.DateTime;
                    rInvoice.Terms = "Cash / Charge";
                    rInvoice.VAT = 0;
                    rInvoice.TOTAL = 0;
                    rInvoiceDao.SaveRetailPartially(rInvoice);
                }

                m_rInvoice = rInvoice;
            }
        }

        private void RefreshRetailDataGrids()
        {
            //grdRetail.DataSource = null;

            IList<RetailInvoice> list = rInvoiceDao.GetRetailList(m_series.Start_series, m_series.End_series);
            grdRetail.DataSource = list;

            grdRetail.RefreshDataSource();
        }

        private void EditDataSource(bool bEnable)
        {
            grdvRetailDetails.Columns["_qty"].OptionsColumn.AllowEdit = bEnable;
            grdvRetailDetails.Columns["_qty"].OptionsColumn.AllowFocus = bEnable;

            grdvRetailDetails.Columns["itemname"].OptionsColumn.AllowEdit = bEnable;
            grdvRetailDetails.Columns["itemname"].OptionsColumn.AllowFocus = bEnable;

            grdvRetailDetails.Columns["itemsize"].OptionsColumn.AllowEdit = bEnable;
            grdvRetailDetails.Columns["itemsize"].OptionsColumn.AllowFocus = bEnable;

            grdvRetailDetails.Columns["itemunit"].OptionsColumn.AllowEdit = bEnable;
            grdvRetailDetails.Columns["itemunit"].OptionsColumn.AllowFocus = bEnable;

            grdvRetailDetails.Columns["price"].OptionsColumn.AllowEdit = bEnable;
            grdvRetailDetails.Columns["price"].OptionsColumn.AllowFocus = bEnable;

            grdvRetailDetails.Columns["subtotal"].OptionsColumn.AllowEdit = bEnable;
            grdvRetailDetails.Columns["subtotal"].OptionsColumn.AllowFocus = bEnable;

            if (bEnable)
                grdvRetailDetails.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            else
                grdvRetailDetails.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }

        private void grdvRetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (bIsLoading)
                return;

            if (grdvRetail.FocusedRowHandle < 0)
                return;

            RetailInvoice rinv = rInvoiceDao.GetByORNumber(cUtils.ConvertToInteger( grdvRetail.GetFocusedRowCellValue("or_number").ToString()));

            if (rinv == null)
                return;

            m_rInvoice = rinv;
            rinv.details = rInvoiceDao.GetRetailDetails(rinv.ID);

            tblData.Rows.Clear();

            foreach (RetailInvoiceDetails item in rinv.details)
            {
                DataRow row = tblData.NewRow();
                row[(int)eFlds._qty] = item.QTY;
                row[(int)eFlds.itemname] = item.item.Name;
                row[(int)eFlds.itemsize] = item.item.Size.Description;
                row[(int)eFlds.itemunit] = item.item.Unit;
                row[(int)eFlds.price] = item.UnitPrice;
                row[(int)eFlds.subtotal] = item.Amount;
                row[(int)eFlds.ID] = item.ID;

                tblData.Rows.Add(row);
            }

            txtCustomer.Text = m_rInvoice.Customer;
            txtTIN.Text = m_rInvoice.TIN;
            txtAddress.Text = m_rInvoice.Address;
            txtBusStyle.Text = m_rInvoice.BusinessStyle;
            txtTerms.Text = m_rInvoice.Terms;
            txtpwdID.Text = m_rInvoice.OSCA_PWD_ID;
        }

        private void grdvRetailDetails_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bIsLoading)
                return;

            bool bIsChanging = false;

            if (e.Column.FieldName == "itemname")
            {
                bIsChanging = true;
                grdvRetailDetails.SetRowCellValue(e.RowHandle, "itemsize", "");
                repoCboSize.Items.Clear();
                string itemName = grdvRetailDetails.GetRowCellValue(e.RowHandle, "itemname").ToString() ;
                IList<ItemSizes> sizeList = itemDao.GetSizes(itemName);

                if (sizeList[0] != null)
                {
                    IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);
                    foreach (SortedSize size in sortedSizeList)
                    {
                        repoCboSize.Items.Add(size.Description);
                    }
                }
            }
            else if (e.Column.FieldName == "itemsize")
            {
                if (bIsChanging)
                    return;

                string itemsize = grdvRetailDetails.GetFocusedRowCellValue("itemsize").ToString().Trim();
                string itemname = grdvRetailDetails.GetFocusedRowCellValue("itemname").ToString().Trim();
                IList<string> strList = itemDao.GetUnitList(itemname, itemsize);
                repoCboUnit.Items.Clear();

                if (strList.Count > 0)
                {
                    foreach (string str in strList)
                    {
                        repoCboUnit.Items.Add(str);
                    }
                }
            }
            else if (e.Column.FieldName == "itemunit")
            {
                if (bIsChanging)
                    return;

                //get the item
                string itemsize, itemname, itemunit;
                itemsize = grdvRetailDetails.GetFocusedRowCellValue("itemsize").ToString().Trim();
                itemname = grdvRetailDetails.GetFocusedRowCellValue("itemname").ToString().Trim();
                itemunit = grdvRetailDetails.GetFocusedRowCellValue("itemunit").ToString().Trim();

                Item I = itemDao.GetItem(itemname, itemsize, itemunit);

                if (itemsize.Trim() == "")//try checking if item has no size
                {
                    I = itemDao.CheckItemNoSize(itemname, itemunit);
                }

                if (I == null)
                    return;

                //set the price
                grdvRetailDetails.SetRowCellValue(e.RowHandle, "price", I.RetailPrice);
            }
            else if (e.Column.FieldName == "price" || e.Column.FieldName == "_qty")
            {
                int qty = 0;
                double price = 0, subtotal =0;
                qty = cUtils.ConvertToInteger(grdvRetailDetails.GetFocusedRowCellValue("_qty").ToString().Trim());
                price = cUtils.ConvertToDouble(grdvRetailDetails.GetFocusedRowCellValue("price").ToString().Trim());
                subtotal = qty * price;

                grdvRetailDetails.SetRowCellValue(e.RowHandle, "subtotal", subtotal);
            }
            else if (e.Column.FieldName == "subtotal")
            {
                ComputeTotalAmountDue();
            }
        }

        private void repoCboItem_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cboEdit = sender as ComboBoxEdit;
            if (cboEdit.Text.Trim().Length > 2)
            {
                IList<string> nameList = new List<string>();
                nameList = itemDao.GetUniqueItemNames(cboEdit.Text.Trim());
                cboEdit.Properties.Items.Clear();//clear first
                foreach (string str in nameList)
                {
                    cboEdit.Properties.Items.Add(str);
                }
            }
        }

        private void grdvRetailDetails_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int qty = 0;
            string sItemdesc = "", sItemSize = "", itemunit = "";
            double price = 0;
            bool bNoSize = false;

            GridView view = sender as GridView;
            GridColumn grdQty = view.Columns[(int)eFlds._qty];
            GridColumn grdItemName = view.Columns[(int)eFlds.itemname];
            GridColumn grdSize = view.Columns[(int)eFlds.itemsize];
            GridColumn grdPrice = view.Columns[(int)eFlds.price];
            GridColumn grdUnit = view.Columns[(int)eFlds.itemunit];

            //clear error values first
            view.ClearColumnErrors();

            qty = cUtils.ConvertToInteger(grdvRetailDetails.GetFocusedRowCellValue("_qty").ToString());
            sItemdesc = grdvRetailDetails.GetFocusedRowCellValue("itemname").ToString();
            sItemSize = grdvRetailDetails.GetFocusedRowCellValue("itemsize").ToString();
            itemunit = grdvRetailDetails.GetFocusedRowCellValue("itemunit").ToString();
            price = cUtils.ConvertToDouble(grdvRetailDetails.GetFocusedRowCellValue("price").ToString());

            Item I = itemDao.GetItem(sItemdesc, sItemSize, itemunit);

            if (qty <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdQty, "Invalid Quantity entered.");
                view.FocusedColumn = grdQty;
                view.ShowEditor();
            }
            else if (sItemdesc.Trim().Length <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdItemName, "You must first select an item.");
                view.FocusedColumn = grdItemName;
                view.ShowEditor();
            }
            else if (sItemSize.Trim().Length <= 0 && !bNoSize)//size field is empty and item searched has available sizes
            {
                IList<Item> listItem = itemDao.GetItemList(sItemdesc);

                if (listItem[0].Size != null)
                {
                    e.Valid = false;
                    view.SetColumnError(grdSize, "Please select a size");
                    view.FocusedColumn = grdSize;
                    view.ShowEditor();
                }
            }
            else if (itemunit.Trim().Length <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdUnit, "Please select a unit");
                view.FocusedColumn = grdUnit;
                view.ShowEditor();
            }
            else if (price <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdPrice, "Invalid Price.");
                view.FocusedColumn = grdPrice;
                view.ShowEditor();
            }
            else if (I == null)
            {
                e.Valid = false;
                view.SetColumnError(grdItemName, "Item does not exist! Either the item name or size does not exist");
                view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                view.FocusedColumn = grdItemName;
                view.ShowEditor();
            }
            else
            {
                e.Valid = true;
            }
        }

        private void ComputeTotalAmountDue()
        {
            double total = 0;
            foreach (DataRow row in tblData.Rows)
            {
                total += cUtils.ConvertToDouble( row[(int)eFlds.subtotal].ToString());
            }

            txtTotalAmtDue.Text = total.ToString(cUtils.FMT_CURRENCY_AMT);
            txtTotalVat.Text = cUtils.GetVATAmount(total).ToString(cUtils.FMT_CURRENCY_AMT);
        }

        private void grdvRetailDetails_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grdvRetailDetails_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (bIsLoading)
                return;

            string val = e.Value as string;
            if (e.Column.FieldName == "price" || e.Column.FieldName == "subtotal")
            {
                double dVal = cUtils.ConvertToDouble(val);
                e.DisplayText = string.Format("{0}", dVal.ToString(cUtils.FMT_CURRENCY_AMT));
            }
            else if (e.Column.FieldName == "_qty")
            {
                int iVal = cUtils.ConvertToInteger(val);
                e.DisplayText = string.Format("{0}", iVal.ToString(cUtils.FMT_NUMBER));
            }
        }

        private void grdvRetailDetails_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            ComputeTotalAmountDue();
        }
    }
}
