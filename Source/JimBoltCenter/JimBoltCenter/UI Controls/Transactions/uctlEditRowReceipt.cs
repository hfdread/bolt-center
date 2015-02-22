using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DBMapping.BOL;
using DBMapping.DAL;
using JimBoltCenter.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace JimBoltCenter.UI_Controls.Transactions
{
    public partial class uctlEditRowReceipt : UserControl
    {
        public Receipt m_receipt { get; set; }
        public Int32 nEditDetailID { get; set; }
        public ReceiptDetails receiptDetails { get; set; }
        public IList<ReceiptDetails> m_DetailsAddSource;
        public ReceiptDetails RecentlyAddedDetail { get; set; }
        public bool bCancel;
        public bool bAdd;

        private ReceiptDao m_receiptDao = null;
        private ItemDao mItemDao = null;

        private BindingSource bSrc = null;
        public DataTable tblData = null;
        private DataSet dsData = null;

        private bool bIsLoading;

        private enum eCol
        {
            ItemIndex = 0,
            itemName,
            itemSize,
            Unit,
            UnitPrice,
            Discount,
            DiscountedPrice,
            QTY,
            SubTotal,
            OrigPrice
        }

        public uctlEditRowReceipt()
        {
            InitializeComponent();
            bIsLoading = true;
            bCancel = false;
            m_receiptDao = new ReceiptDao();
            mItemDao = new ItemDao();
            m_DetailsAddSource = new List<ReceiptDetails>();
        }

        private void uctlEditRowReceipt_Load(object sender, EventArgs e)
        {
            bIsLoading = false;
            Skin.SetButtonFont(btnOK);
            Skin.SetButtonFont(btnCancel);
            Skin.SetGridFont(grdvReceiptItems);

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
            DataColumn dtCol9 = new DataColumn();
            DataColumn dtCol10 = new DataColumn(); //unit [] index 3 after itemsize

            dtCol1.ColumnName = "ItemIndex";
            dtCol2.ColumnName = "itemName";
            dtCol3.ColumnName = "itemSize";
            dtCol4.ColumnName = "UnitPrice";
            dtCol5.ColumnName = "Discount";
            dtCol6.ColumnName = "DiscountedPrice";
            dtCol7.ColumnName = "QTY";
            dtCol8.ColumnName = "SubTotal";
            dtCol9.ColumnName = "OrigPrice";
            dtCol10.ColumnName = "_Unit";

            dsData.DataSetName = "NewDataSet";
            tblData.TableName = "tblData";

            dsData.Tables.AddRange(new System.Data.DataTable[] { tblData });

            tblData.Columns.AddRange(new System.Data.DataColumn[] { 
                    dtCol1,
                    dtCol2,
                    dtCol3,
                    dtCol10,
                    dtCol4,
                    dtCol5,
                    dtCol6,
                    dtCol7,
                    dtCol8,
                    dtCol9
                });


            bSrc.DataMember = "tblData";
            bSrc.DataSource = dsData;
            grdReceiptItems.DataSource = bSrc;

            if (!bAdd)//user clicked edit
            {
                repoTextEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                repoTextEdit.DisplayFormat.FormatString = "c2";

                ReceiptDetails rd;
                if (nEditDetailID > 0)
                    rd = m_receiptDao.GetReceiptDetails(nEditDetailID, m_receipt.ID);
                else
                    rd = RecentlyAddedDetail;

                DataRow row = tblData.NewRow();

                rd.itemName = rd.item.Name;

                if (rd.item.Size != null)
                    rd.itemSize = rd.item.Size.Description;
                else
                    rd.itemSize = "";

                if (rd.item.Unit.Trim() != "")
                {
                    rd._Unit = rd.item.Unit;
                    rd.OrigPrice = rd.item.UnitPrice;
                }
                else
                {
                    rd._Unit = rd.item.Unit2;
                    rd.OrigPrice = rd.item.UnitPrice2;
                }

                rd.DiscountedPrice = cUtils.GetDiscountedPrices(rd.Discount, rd.UnitPrice);

                row[(int)eCol.Discount] = rd.Discount;
                row[(int)eCol.DiscountedPrice] = rd.DiscountedPrice;
                row[(int)eCol.ItemIndex] = rd.ItemIndex;
                row[(int)eCol.itemName] = rd.itemName;
                row[(int)eCol.itemSize] = rd.itemSize;
                row[(int)eCol.OrigPrice] = rd.OrigPrice;
                row[(int)eCol.QTY] = rd.QTY;
                row[(int)eCol.SubTotal] = rd.UnitPrice * rd.QTY;
                row[(int)eCol.UnitPrice] = rd.UnitPrice;
                row[(int)eCol.Unit] = rd._Unit;

                tblData.Rows.Add(row);

                //selections
                LoadSizes();
                LoadUnit();
            }
            else
            {
                grdvReceiptItems.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                this.Height = 240;
            }
        }

        private void LoadSizes()
        {
            string itemName = "";
            itemName = grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString();
            IList<ItemSizes> sizeList = mItemDao.GetSizes(itemName);

            if (sizeList[0] != null)
            {
                IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);

                foreach (SortedSize size in sortedSizeList)
                {
                    repoCboSize.Items.Add(size.Description);
                }
            }
        }

        private void LoadUnit()
        {
            string itemName = "", itemdesc = "";
            itemName = grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString().Trim();
            itemdesc = grdvReceiptItems.GetFocusedRowCellValue("itemSize").ToString().Trim();

            IList<string> strList = mItemDao.GetUnitList(itemName, itemdesc);

            repoCboUnit.Items.Clear();

            if (strList.Count > 0)
            {
                foreach (string str in strList)
                {
                    repoCboUnit.Items.Add(str);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bCancel = true;
            this.ParentForm.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ReceiptDetails rd;

            if (bAdd)//user clicked add
            {
                int nIndex = m_receipt.details.Count + 1;
                foreach (DataRow row in tblData.Rows)
                {
                    rd = new ReceiptDetails();
                    rd.item = mItemDao.GetItem(row[(int)eCol.itemName].ToString(), row[(int)eCol.itemSize].ToString(), row[(int)eCol.Unit].ToString());
                    if (rd.item == null)//try if item has no size
                    {
                        rd.item = mItemDao.CheckItemNoSize(row[(int)eCol.itemName].ToString(), row[(int)eCol.Unit].ToString());
                    }

                    rd.QTY = cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString());
                    rd.UnitPrice = cUtils.ConvertToDouble(row[(int)eCol.UnitPrice].ToString());
                    rd.Discount = row[(int)eCol.Discount].ToString();
                    rd.SubTotal = cUtils.ConvertToDouble(row[(int)eCol.SubTotal].ToString());

                    if (rd.item == null)
                        return;

                    rd.ItemIndex = nIndex;
                    nIndex += 1;

                   m_DetailsAddSource.Add(rd);
                }
            }
            else//user clicked edit
            {
                foreach (DataRow row in tblData.Rows)
                {
                    rd = new ReceiptDetails();
                    if (nEditDetailID > 0)
                        receiptDetails = m_receiptDao.GetReceiptDetails(nEditDetailID, m_receipt.ID);
                    else
                        receiptDetails = RecentlyAddedDetail;

                    receiptDetails.bEdited = false;

                    rd.ItemIndex = receiptDetails.ItemIndex;
                    rd.item = mItemDao.GetItem(row[(int)eCol.itemName].ToString(), row[(int)eCol.itemSize].ToString(), row[(int)eCol.Unit].ToString());
                    if (rd.item == null)//try if item has no size
                    {
                        rd.item = mItemDao.CheckItemNoSize(row[(int)eCol.itemName].ToString(), row[(int)eCol.Unit].ToString());
                    }

                    rd.QTY = cUtils.ConvertToInteger(row[(int)eCol.QTY].ToString());
                    rd.UnitPrice = cUtils.ConvertToDouble(row[(int)eCol.UnitPrice].ToString());
                    rd.Discount = row[(int)eCol.Discount].ToString();
                    rd.SubTotal = cUtils.ConvertToDouble(row[(int)eCol.SubTotal].ToString());

                    if (rd.item == null)
                        return;

                    //ask if they want to continue
                    if (rd.item.ID != receiptDetails.item.ID && nEditDetailID > 0)
                    {
                        if (cUtils.ShowMessageQuestion("You are changing an Item, it will affect current stock count.\nCotinue?", "Item Change")
                                            == DialogResult.No)
                        {
                            return;
                        }
                    }

                    if (CheckChanges(rd, receiptDetails))
                    {
                        receiptDetails.QTY = rd.QTY;
                        receiptDetails.UnitPrice = rd.UnitPrice;
                        receiptDetails.Discount = rd.Discount;
                        receiptDetails.item = rd.item;
                        receiptDetails.SubTotal = rd.SubTotal;
                        receiptDetails.bEdited = true;
                    }
                }
            }
            this.ParentForm.Close();
        }

        private bool CheckChanges(ReceiptDetails now, ReceiptDetails old)
        {
            bool bRet = false;

            if (now.QTY != old.QTY)
                bRet = true;
            else if (now.UnitPrice != old.UnitPrice)
                bRet = true;
            else if (now.Discount != old.Discount)
                bRet = true;
            else if (now.item != old.item)
                bRet = true;

            return bRet;
        }

        private void grdvReceiptItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bIsLoading)
                return;

            bool bChange = false;

            string messageErr = "";
            if (e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "QTY" || e.Column.FieldName == "Discount")
            {
                int qty = 0;
                string discount = "", discountedprices = "";
                double unitprice = 0, subtotal = 0;
                qty = cUtils.ConvertToInteger(grdvReceiptItems.GetFocusedRowCellValue("QTY").ToString());
                discount = grdvReceiptItems.GetFocusedRowCellValue("Discount").ToString();
                unitprice = cUtils.ConvertToDouble(grdvReceiptItems.GetFocusedRowCellValue("UnitPrice").ToString());

                subtotal = qty * cUtils.GetLastDiscountPrice(discount, unitprice);
                discountedprices = cUtils.GetDiscountedPrices(discount, unitprice);

                grdvReceiptItems.SetFocusedRowCellValue("SubTotal", subtotal.ToString(cUtils.FMT_CURRENCY_AMT));
                grdvReceiptItems.SetFocusedRowCellValue("DiscountedPrice", discountedprices);

                //ComputeTotal();
            }
            else if (e.Column.FieldName == "itemSize")
            {
                if (bChange)
                    return;
                messageErr = "Size is invalid!";
                string itemDesc = grdvReceiptItems.GetFocusedRowCellValue("itemSize").ToString().Trim();

                IList<string> strList = mItemDao.GetUnitList(grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString().Trim(), itemDesc);

                repoCboUnit.Items.Clear();

                if (strList.Count > 0)
                {
                    foreach (string str in strList)
                    {
                        repoCboUnit.Items.Add(str);
                    }
                }

            }
            else if (e.Column.FieldName == "_Unit")
            {
                messageErr = "Unit is invalid!";
                string name, size, unit;
                name = grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString().Trim();
                size = grdvReceiptItems.GetFocusedRowCellValue("itemSize").ToString().Trim();
                unit = grdvReceiptItems.GetFocusedRowCellValue("_Unit").ToString().Trim();

                Item I = mItemDao.GetItem(name, size, unit);

                if (size.Trim() == "")
                {
                    I = mItemDao.CheckItemNoSize(name, unit);
                }

                if (I == null)
                    return;

                //if (I.Unit.Trim() != "")//non-weight
                //{
                grdvReceiptItems.SetFocusedRowCellValue("UnitPrice", I.RetailPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                grdvReceiptItems.SetFocusedRowCellValue("OrigPrice", I.RetailPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                //}
                //else//weight item
                //{
                //    grdvReceiptItems.SetFocusedRowCellValue("UnitPrice", I.RetailPrice.ToString(cUtils.FMT_CURRENCY_AMT));
                //    grdvReceiptItems.SetFocusedRowCellValue("OrigPrice", I.UnitPrice2.ToString(cUtils.FMT_CURRENCY_AMT));
                //}

            }
            else if (e.Column.FieldName == "itemName")
            {
                messageErr = "Item Name is invalid!";
                bChange = true;
                grdvReceiptItems.SetFocusedRowCellValue("itemSize", "");
                repoCboSize.Items.Clear();
                try
                {
                    string itemName = "";
                    itemName = grdvReceiptItems.GetFocusedRowCellValue("itemName").ToString();
                    IList<ItemSizes> sizeList = mItemDao.GetSizes(itemName);

                    if (sizeList[0] != null)
                    {
                        IList<SortedSize> sortedSizeList = cUtils.GetSortedSizeTable(sizeList);


                        foreach (SortedSize size in sortedSizeList)
                        {
                            repoCboSize.Items.Add(size.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    cUtils.ShowMessageError(messageErr, "Receipts Add Item\n\nItem Name!");
                    cUtils.CreateCrashLog(ex.Message, "Add Item", "Receipt");
                }
            }
        }

        private void repoCboItemName_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cboEdit = sender as ComboBoxEdit;
            IList<string> nameList = new List<string>();
            nameList = mItemDao.GetItemNames(cboEdit.Text.Trim());

            cboEdit.Properties.Items.Clear();
            cboEdit.Properties.Items.AddRange(nameList.ToList<string>());
            cboEdit.ShowPopup();

            //if (cboEdit.Text.Trim().Length > 2)
            //{
            //    IList<string> nameList = new List<string>();
            //    //nameList = mItemDao.GetItemNameSuggestion(cboEdit.Text.Trim());
            //    nameList = mItemDao.GetUniqueItemNames(cboEdit.Text.Trim());
            //    cboEdit.Properties.Items.Clear();//clear first
            //    foreach (string str in nameList)
            //    {
            //        cboEdit.Properties.Items.Add(str);
            //    }
            //}
        }

        private void grdvReceiptItems_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn grdQTY = view.Columns[(int)eCol.QTY];
            GridColumn grdItemName = view.Columns[(int)eCol.itemName];
            GridColumn grdSize = view.Columns[(int)eCol.itemSize];
            GridColumn grdPrice = view.Columns[(int)eCol.UnitPrice];
            GridColumn grdSubtotal = view.Columns[(int)eCol.SubTotal];
            GridColumn grdUnit = view.Columns[(int)eCol.Unit];

            //clear error values first
            view.ClearColumnErrors();

            int qty = 0;
            string itemname = "", itemsize = "", itemunit = "";
            double price = 0, subtotal = 0;
            qty = cUtils.ConvertToInteger(view.GetFocusedRowCellValue(grdQTY).ToString());
            itemname = view.GetFocusedRowCellValue(grdItemName).ToString().Trim();
            itemsize = view.GetFocusedRowCellValue(grdSize).ToString().Trim();
            price = cUtils.ConvertToDouble(view.GetFocusedRowCellValue(grdPrice).ToString());
            subtotal = cUtils.ConvertToDouble(view.GetFocusedRowCellValue(grdSubtotal).ToString());
            itemunit = view.GetFocusedRowCellValue(grdUnit).ToString();

            Item I = mItemDao.GetItem(itemname, itemsize, itemunit);

            if (string.IsNullOrWhiteSpace(itemname))
            {
                e.Valid = false;
                view.SetColumnError(grdItemName, "You must first select an item.");
                view.FocusedColumn = grdItemName;
                view.ShowEditor();
            }
            else if (string.IsNullOrWhiteSpace(itemsize))
            {
                IList<Item> listItem = mItemDao.GetItemList(itemname);

                if (listItem.Count > 0)
                {
                    if (listItem[0].Size != null)
                    {
                        e.Valid = false;
                        view.SetColumnError(grdSize, "Please select a size");
                        view.FocusedColumn = grdSize;
                        view.ShowEditor();
                    }
                }
                else
                {
                    e.Valid = false;
                    view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                    view.FocusedColumn = grdSize;
                    view.ShowEditor();
                }
            }
            else if (mItemDao.GetItem(itemname.Trim(), itemsize.Trim()) == null)
            {
                e.Valid = false;
                view.SetColumnError(grdSize, "Item does not exist! Either the item name or size does not exist");
                view.SetColumnError(grdItemName, "Item does not exist! Either the item name or size does not exist");
                view.FocusedColumn = grdSize;
                view.ShowEditor();
            }
            else if (string.IsNullOrWhiteSpace(itemunit))
            {
                //check if the item has no unit
                var items = mItemDao.SearchForSingleItem(itemname, itemsize, itemunit);

                if (items.Count > 1)
                {
                    e.Valid = false;
                    view.SetColumnError(grdUnit, "Please choose a unit");
                    view.FocusedColumn = grdUnit;
                    view.ShowEditor();
                }
                else if (items.Count == 1 && !string.IsNullOrWhiteSpace(items[0].Unit))
                {
                    e.Valid = false;
                    view.SetColumnError(grdUnit, "Please choose a unit");
                    view.FocusedColumn = grdUnit;
                    view.ShowEditor();
                }
            }
            else if (I == null)
            {
                var itemList = mItemDao.SearchForSingleItem(itemname, itemsize, itemunit);

                if (itemList.Count > 1 || itemList.Count == 0)
                {
                    e.Valid = false;
                    view.SetColumnError(grdItemName, "Item does not exist! Either the item name, size or unit does not exist");
                    view.SetColumnError(grdSize, "Item does not exist! Either the item name, size or unit does not exist");
                    view.SetColumnError(grdUnit, "Item does not exist! Either the item name, size or unit does not exist");
                    view.FocusedColumn = grdItemName;
                    view.ShowEditor();
                }
            }

            if (price <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdPrice, "Invalid Price.");
                view.FocusedColumn = grdPrice;
                view.ShowEditor();
            }
            else if (qty <= 0)
            {
                e.Valid = false;
                view.SetColumnError(grdQTY, "Invalid Quantity entered.");
                view.FocusedColumn = grdQTY;
                view.ShowEditor();
            }
            else if (subtotal == 0)
            {
                e.Valid = false;
                view.SetColumnError(grdSubtotal, "Invalid Subtotal.");
                view.FocusedColumn = grdSubtotal;
                view.ShowEditor();
            }
        }

        private void grdvReceiptItems_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grdvReceiptItems_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (bIsLoading)
                return;

            string val = e.Value as string;
            if (e.Column.FieldName == "UnitPrice")
            {
                double price = cUtils.ConvertToDouble(val);
                e.DisplayText = string.Format("{0}", price.ToString(cUtils.FMT_CURRENCY_AMT));
            }
            else if (e.Column.FieldName == "QTY")
            {
                int qty = cUtils.ConvertToInteger(val);
                e.DisplayText = string.Format("{0}", qty.ToString(cUtils.FMT_NUMBER));
            }
            else if (e.Column.FieldName == "SubTotal")
            {
                double stotal = cUtils.ConvertToDouble(val);
                e.DisplayText = string.Format("{0}", stotal.ToString(cUtils.FMT_CURRENCY_AMT));
            }
            else if (e.Column.FieldName == "OrigPrice")
            {
                double oPrice = cUtils.ConvertToDouble(val);
                e.DisplayText = string.Format("{0}", oPrice.ToString(cUtils.FMT_CURRENCY_AMT));
            }
        }
    }
}
