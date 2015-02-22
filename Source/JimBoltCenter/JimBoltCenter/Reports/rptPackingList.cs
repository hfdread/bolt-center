using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using JimBoltCenter.Utils;

namespace JimBoltCenter.Reports
{
    public partial class rptPackingList : DevExpress.XtraReports.UI.XtraReport
    {
        public string TrxDate { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string sPackingListNo { get; set; }
        public string Terms { get; set; }
        public string Total { get; set; }

        public rptPackingList()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            xrlblUnit.DataBindings.Add("Text", DataSource, "item.Unit");
            xrlblQty.DataBindings.Add("Text", DataSource, "QTY", "{0:#,##0}");
            xrlblName.DataBindings.Add("Text", DataSource, "item.Name");
            xrlblSize.DataBindings.Add("Text", DataSource, "item.Size.Description");
            xrlblPrice.DataBindings.Add("Text", DataSource, "_ItemPriceDiscount");
            xrlblAmount.DataBindings.Add("Text", DataSource, "SubTotal", "{0:#,##0.000}");
        }   

        private void rptPackingList_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }



    }
}
