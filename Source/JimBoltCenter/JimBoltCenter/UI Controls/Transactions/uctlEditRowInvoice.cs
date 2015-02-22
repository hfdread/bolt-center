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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlEditRowInvoice : UserControl
    {

        public SalesInvoice m_invoice { get; set; }
        public SalesInvoiceDetails invoiceDetails;
        public bool bCancel;

        private SalesInvoiceDao invoiceDao = null;
        private SalesInvoiceDetailsDao detailsDao = null;
        private bool bIsLoading;
        private ItemDao mItemDao = null;

        private BindingSource bSrc = null;
        private DataTable tblData = null;
        private DataSet dsData = null;

        private enum eCol
        {
            QTY = 0,
            itemdesc,
            itemSizeDesc,
            itemUnit,
            Price,
            Discount,
            discountPrice,
            unitTotal
        }

        public uctlEditRowInvoice()
        {
            InitializeComponent();
            invoiceDao = new SalesInvoiceDao();
            detailsDao = new SalesInvoiceDetailsDao();
            bIsLoading = true;
            bCancel = false;
            mItemDao = new ItemDao();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bCancel = true;
            this.ParentForm.Close();
        }

        private void uctlEditRowInvoice_Load(object sender, EventArgs e)
        {
            bIsLoading = false;
            Skin.SetButtonFont(btnOK);
            Skin.SetButtonFont(btnCancel);
            Skin.SetGridFont(grdvData);


            //
            bSrc = new BindingSource();
            tblData = new DataTable();
            dsData = new DataSet();

            //columns
            DataColumn dtCol1 = new DataColumn();
            DataColumn dtCol2 = new DataColumn();
            DataColumn dtCol3 = new DataColumn();
            DataColumn dtCol4 = new DataColumn();
            DataColumn dtCol5 = new DataColumn();
            DataColumn dtCol6 = new DataColumn();
            DataColumn dtCol7 = new DataColumn();
            DataColumn dtCol8 = new DataColumn();

            dtCol1.ColumnName = "QTY";
            dtCol2.ColumnName = "itemUnit";
            dtCol3.ColumnName = "itemdesc";
            dtCol4.ColumnName = "itemSizeDesc";
            dtCol5.ColumnName = "Price";
            dtCol6.ColumnName = "Discount";
            dtCol7.ColumnName = "discountPrice";
            dtCol8.ColumnName = "unitTotal";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new System.Data.DataTable[] { tblData });

            tblData.Columns.AddRange(new System.Data.DataColumn[] { 
                    dtCol1,
                    dtCol3,
                    dtCol4,
                    dtCol2,
                    dtCol5,
                    dtCol6,
                    dtCol7,
                    dtCol8
                });

            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;
            //grdvData.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grdData.DataSource = bSrc;

            //
            SalesInvoiceDetails sid = detailsDao.GetDetails(m_invoice.nEdit_DetailsID,m_invoice);
            DataRow row = tblData.NewRow();
            row[(int)eCol.QTY] = sid.QTY.ToString(cUtils.FMT_NUMBER);
            row[(int)eCol.Discount] = sid.Discount;
            row[(int)eCol.itemdesc] = sid.item.Name;

            if (sid.item.Size != null)
                row[(int)eCol.itemSizeDesc] = sid.item.Size.Description;
            else
                row[(int)eCol.itemSizeDesc] = "";

            if (sid.item.Unit.Trim() != "")
            {
                row[(int)eCol.itemUnit] = sid.item.Unit;
            }
            else if (sid.item.Unit2.Trim() != "")
            {
                row[(int)eCol.itemUnit] = sid.item.Unit2;
            }

            row[(int)eCol.Price] = sid.Price.ToString(cUtils.FMT_CURRENCY_AMT);
            double subTotal = sid.QTY * cUtils.GetLastDiscountPrice(sid.Discount,sid.Price);
            row[(int)eCol.unitTotal] =  subTotal.ToString(cUtils.FMT_CURRENCY_AMT);

            row[(int)eCol.discountPrice] = cUtils.GetDiscountedPrices(sid.Discount, sid.Price);

            tblData.Rows.Add(row);

            //selections
            LoadSizes();
            LoadUnit();
        }

        private void grdvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bIsLoading)
                return;

            bool bChange = false;

            try
            {
                if (e.Column.FieldName == "itemdesc")
                {
                    bChange = true;
                    grdvData.SetRowCellValue(e.RowHandle, "itemSizeDesc", "");
                    repoCbo.Items.Clear();
                    try
                    {
                        //string qty = grdvSalesItem.GetFocusedRowCellValue("QTY").ToString();
                        string itemName = "";
                        itemName = grdvData.GetFocusedRowCellValue("itemdesc").ToString();
                        IList<ItemSizes> sizeList = mItemDao.GetSizes(itemName);

                        if (sizeList[0] != null)
                        {
                            IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);

                            foreach (SortedSize size in sortedSizeList)
                            {
                                repoCbo.Items.Add(size.Description);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        cUtils.ShowMessageError(ex.Message, "Invoice Change Item");
                        cUtils.CreateCrashLog(ex.Message, "Invoice Change Item", "Stock In Invoice");
                    }
                }
                else if (e.Column.FieldName == "itemUnit")
                {
                    //get the item
                    string itemsize, itemname, itemunit;
                    itemsize = grdvData.GetFocusedRowCellValue("itemSizeDesc").ToString().Trim();
                    itemname = grdvData.GetFocusedRowCellValue("itemdesc").ToString().Trim();
                    itemunit = grdvData.GetFocusedRowCellValue("itemUnit").ToString().Trim();

                    Item I = mItemDao.GetItem(itemname, itemsize, itemunit);

                    if (I == null)
                        return;


                    if (I.Unit2.Trim() == "")//non-weight item
                    {
                        grdvData.SetRowCellValue(e.RowHandle, "Price", I.UnitPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                    }
                    else//weight item
                    {
                        grdvData.SetRowCellValue(e.RowHandle, "Price", I.UnitPrice2.ToString(cUtils.FMT_CURRENCY_AMT));
                    }
                }
                else if (e.Column.FieldName == "itemSizeDesc")
                {
                    if (bChange)
                        return;

                    string itemsize = grdvData.GetFocusedRowCellValue("itemSizeDesc").ToString().Trim();
                    string itemname = grdvData.GetFocusedRowCellValue("itemdesc").ToString().Trim();

                    if (itemsize.Trim().Length <= 0)
                        return;

                    IList<string> strList = mItemDao.GetUnitList(itemname, itemsize);

                    repoCboUnit.Items.Clear();

                    if (strList.Count > 0)
                    {
                        foreach (string str in strList)
                        {
                            repoCboUnit.Items.Add(str);
                        }
                    }
                }
                else if (e.Column.FieldName == "Discount" || e.Column.FieldName == "Price")
                {
                    string discount = "";
                    discount = grdvData.GetFocusedRowCellValue("Discount").ToString().Trim();

                    //discounted prices
                    //cUtils.GetDiscountedPrices(details.Discount, details.Price);
                    string discountedPrices = cUtils.GetDiscountedPrices(discount,
                                        cUtils.ConvertToDouble(grdvData.GetFocusedRowCellValue("Price").ToString()));

                    grdvData.SetRowCellValue(e.RowHandle, "discountPrice", discountedPrices);

                    //unit total
                    //qty * cUtils.GetLastDiscountPrice(details.Discount, details.Price);
                    int qty = cUtils.ConvertToInteger(grdvData.GetFocusedRowCellValue("QTY").ToString());
                    double unitTotal = qty * cUtils.GetLastDiscountPrice(discount,
                                           cUtils.ConvertToDouble(grdvData.GetFocusedRowCellValue("Price").ToString()));

                    grdvData.SetRowCellValue(e.RowHandle, "unitTotal", unitTotal.ToString(cUtils.FMT_CURRENCY_AMT));

                    //ComputeTotal();
                }
                else if (e.Column.FieldName == "QTY")
                {
                    int qty = 0;
                    double price = 0;
                    string discountPrice = "", discount = "";
                    qty = cUtils.ConvertToInteger(grdvData.GetFocusedRowCellValue("QTY").ToString().Trim());
                    price = cUtils.ConvertToDouble(grdvData.GetFocusedRowCellValue("Price").ToString().Trim());
                    discountPrice = grdvData.GetFocusedRowCellValue("discountPrice").ToString().Trim();
                    discount = grdvData.GetFocusedRowCellValue("Discount").ToString().Trim();
                    if (qty > 0)
                    {
                        if (price > 0)
                        {
                            double unitTotal = qty * cUtils.GetLastDiscountPrice(discount, price);

                            grdvData.SetRowCellValue(e.RowHandle, "unitTotal", unitTotal.ToString(cUtils.FMT_CURRENCY_AMT));

                            //ComputeTotal();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void LoadSizes()
        {
            string itemName = "";
            itemName = grdvData.GetFocusedRowCellValue("itemdesc").ToString();
            IList<ItemSizes> sizeList = mItemDao.GetSizes(itemName);

            if (sizeList[0] != null)
            {
                IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);

                foreach (SortedSize size in sortedSizeList)
                {
                    repoCbo.Items.Add(size.Description);
                }
            }
        }

        private void LoadUnit()
        {
            string itemSize = grdvData.GetFocusedRowCellValue("itemSizeDesc").ToString().Trim();
            string itemName = grdvData.GetFocusedRowCellValue("itemdesc").ToString().Trim();


            IList<string> strList = mItemDao.GetUnitList(itemName, itemSize);

            repoCboUnit.Items.Clear();

            if (strList.Count > 0)
            {
                foreach (string str in strList)
                {
                    repoCboUnit.Items.Add(str);
                }
            }
        }

        private void rpoCboItemName_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cboEdit = sender as ComboBoxEdit;
            if (cboEdit.Text.Trim().Length > 2)
            {
                IList<string> nameList = new List<string>();
                //nameList = mItemDao.GetItemNameSuggestion(cboEdit.Text.Trim());
                nameList = mItemDao.GetUniqueItemNames(cboEdit.Text.Trim());
                cboEdit.Properties.Items.Clear();//clear first
                foreach (string str in nameList)
                {
                    cboEdit.Properties.Items.Add(str);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in tblData.Rows)
            {
                SalesInvoiceDetails sid = new SalesInvoiceDetails();
                invoiceDetails = detailsDao.GetDetails(m_invoice.nEdit_DetailsID, m_invoice);
                invoiceDetails.bEdited = false;

                sid.QTY = cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString());
                sid.Price = cUtils.ConvertToDouble(row[(int)eCol.Price].ToString());
                sid.Discount = row[(int)eCol.Discount].ToString();
                sid.item = mItemDao.GetItem(row[(int)eCol.itemdesc].ToString().Trim(),
                                                    row[(int)eCol.itemSizeDesc].ToString().Trim(),
                                                    row[(int)eCol.itemUnit].ToString().Trim() );
                if (sid.item == null)//for items with no sizes
                {
                    sid.item = mItemDao.CheckItemNoSize(row[(int)eCol.itemdesc].ToString().Trim(), row[(int)eCol.itemUnit].ToString().Trim());
                }

                if (sid.item.ID != invoiceDetails.item.ID)
                {
                    if (cUtils.ShowMessageQuestion("You are changing an Item, it will affect current stock count.\nContinue?", "Item Change")
                                        == DialogResult.No)
                    {
                        return;
                    }
                }

                if (CheckChanges(sid, invoiceDetails))
                {
                    invoiceDetails.QTY = sid.QTY;
                    invoiceDetails.Price = sid.Price;
                    invoiceDetails.Discount = sid.Discount;
                    invoiceDetails.item = sid.item;
                    invoiceDetails.bEdited = true;
                }                
            }

            this.ParentForm.Close();
        }

        private bool CheckChanges(SalesInvoiceDetails now, SalesInvoiceDetails old)
        {
            bool bRet = false;

            if (now.QTY != old.QTY)
                bRet = true;
            else if (now.Price != old.Price)
                bRet = true;
            else if (now.Discount != old.Discount)
                bRet = true;
            else if (now.item != old.item)
                bRet = true;

            return bRet;
        }

        private void grdvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (bIsLoading)
                return;

            string val = e.Value as string;

            if (e.Column.FieldName == "Price")
            {
                double price = cUtils.ConvertToDouble(val);
                e.DisplayText = string.Format("{0}", price.ToString(cUtils.FMT_CURRENCY_AMT));
            }
            else if (e.Column.FieldName == "QTY")
            {
                int qty = cUtils.ConvertToInteger(val);
                e.DisplayText = string.Format("{0}", qty.ToString(cUtils.FMT_NUMBER));
            }
        }

        private void grdvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grdvData_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int qty = 0;
            string sItemdesc = "", sItemSize = "", sunit="";
            double price = 0;

            GridView view = sender as GridView;
            GridColumn grdQty = view.Columns[(int)eCol.QTY];
            GridColumn grdItemName = view.Columns[(int)eCol.itemdesc];
            GridColumn grdSize = view.Columns[(int)eCol.itemSizeDesc];
            GridColumn grdPrice = view.Columns[(int)eCol.Price];
            GridColumn grdUnit = view.Columns[(int)eCol.itemUnit];

            //clear error values first
            view.ClearColumnErrors();

            qty = cUtils.ConvertToInteger(grdvData.GetFocusedRowCellValue("QTY").ToString());
            sItemdesc = grdvData.GetFocusedRowCellValue("itemdesc").ToString();
            sItemSize = grdvData.GetFocusedRowCellValue("itemSizeDesc").ToString();
            price = cUtils.ConvertToDouble(grdvData.GetFocusedRowCellValue("Price").ToString());
            sunit = grdvData.GetFocusedRowCellValue("itemUnit").ToString();

            Item I = mItemDao.GetItem(sItemdesc, sItemSize, sunit);


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
            else if (sItemSize.Trim().Length <= 0)
            {
                IList<Item> listItem = mItemDao.GetItemList(sItemdesc);

                if (listItem[0].Size != null)
                {
                    e.Valid = false;
                    view.SetColumnError(grdSize, "Please select a size");
                    view.FocusedColumn = grdSize;
                    view.ShowEditor();
                }
            }
            else if (price <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdPrice, "Invalid Price.");
                view.FocusedColumn = grdPrice;
                view.ShowEditor();
            }
            else if (sunit.Trim().Length == 0)
            {
                e.Valid = false;
                view.SetColumnError(grdUnit, "Please choose a unit");
                view.FocusedColumn = grdUnit;
                view.ShowEditor();
            }
            else if (I == null)
            {
                //try
                I = mItemDao.GetItem(sItemdesc);
                if (I == null)
                {
                    e.Valid = false;
                    view.SetColumnError(grdItemName, "Item does not exist! Either the item name or size does not exist");
                    view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                    view.FocusedColumn = grdItemName;
                    view.ShowEditor();
                }
                else
                    e.Valid = true;
            }
            else
            {
                e.Valid = true;
            }
        }

    }
}
